using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.RabbitMQ.ContractResolvers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Polly;
using Polly.Contrib.WaitAndRetry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Cegeka.Guild.Pokeverse.RabbitMQ
{
    internal sealed class RabbitMqMessageBus :IMessageBus
    {
        private static Random Random = new Random(DateTime.Now.Millisecond);

        private readonly IModel model;
        private readonly IServiceProvider provider;

        public RabbitMqMessageBus(IModel model, IServiceProvider provider)
        {
            this.model = model;
            this.provider = provider;
        }

        public Task Publish<T>(T message) where T : IMessage
        {
            var exchangeName = message.GetType().GetExchangeName();

            model.ExchangeDeclare(exchangeName, ExchangeType.Fanout);
            var body = Encoding.UTF8.GetBytes(message.ToJson());

            var properties = model.CreateBasicProperties();
            properties.Persistent = true;
            model.BasicPublish(exchangeName, string.Empty, properties, body);

            return Task.CompletedTask;
        }

        public Task Subscribe<T>() where T : IMessage
        {
            using (var scope = provider.CreateScope())
            {
                var handlers = scope.ServiceProvider.GetServices<IMessageHandler<T>>();
                foreach (var handler in handlers)
                {
                    SubscribeHandler<T>(handler.GetType());
                }
                
                return Task.CompletedTask;
            }
        }
        
        private void SubscribeHandler<T>(Type handler) 
            where T : IMessage
        {
            var topic = typeof(T).GetExchangeName();

            model.ExchangeDeclare(topic, ExchangeType.Fanout);
            model.BasicQos(0, 1, true);

            var routingKey = $"{handler.FullName}__{topic}";
            var queueName = model.QueueDeclare(routingKey, true, false, false).QueueName;
            model.QueueBind(queueName, topic, routingKey);

            var consumer = new EventingBasicConsumer(model);
            consumer.Received += async (_, args) => await ConsumeWithRetries<T>(handler, args);

            model.BasicConsume(queueName, false, consumer);
        }

        private async Task ConsumeWithRetries<T>(Type handlerType, BasicDeliverEventArgs args)
            where T : IMessage
        {
            var waitAndRetry = Policy.Handle<Exception>()
                .WaitAndRetryAsync(BackOffDelay, (e, t, c) => Console.WriteLine($"Retrying message {args.DeliveryTag} from exchange {args.Exchange} due to exception {e.Message} at {e.StackTrace}"));

            await Policy.Handle<Exception>()
                .FallbackAsync(
                    _ => Task.Run(() => model.BasicNack(args.DeliveryTag, false, false), _),
                    e => Task.Run(() => Console.WriteLine($"Nacked event {args.DeliveryTag} from exchange {args.Exchange} due to exception {e.Message} at {e.StackTrace}")))
                .WrapAsync(waitAndRetry)
                .ExecuteAsync(async () =>
                {
                    await Consume<T>(handlerType, args);
                    //if (Random.Next(0, 100) <= 50)
                    //{
                    //    throw new InvalidOperationException("You failed, try again");
                    //}

                    model.BasicAck(args.DeliveryTag, false);

                    Console.WriteLine($"Event {args.DeliveryTag} from exchange {args.Exchange} successfully handled");
                });
        }

        private async Task Consume<T>(Type handlerType, BasicDeliverEventArgs args)
            where T : IMessage
        {
            var json = args.Body.ToArray().ToUtf8String();
            var jsonEvent = json.DeserializerObject<T>();

            using (var scope = provider.CreateScope())
            {
                var handler = scope.ServiceProvider.GetService(handlerType) as IMessageHandler<T>;
                if (handler == null)
                {
                    throw new InvalidOperationException($"Handler {handlerType.Name} not registered!");
                }

                await handler.Handle(jsonEvent);
            }
        }

        private IEnumerable<TimeSpan> BackOffDelay => Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 3);
    }


    internal static class MessageBusExtensions
    {
        public static string GetExchangeName(this Type messageType)
        {
            return messageType.GetFriendlyName();
        }

        public static string ToJson(this IMessage message)
        {
            return JsonConvert.SerializeObject(message, new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = new PrivateCamelCasePropertyContractResolver(),
            });
        }

        public static T DeserializerObject<T>(this string objectAsString)
        {
            return JsonConvert.DeserializeObject<T>(objectAsString, new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = new PrivateCamelCasePropertyContractResolver(),
            });
        }
    }
}