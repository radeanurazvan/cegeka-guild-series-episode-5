using System;
using System.Text;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.RabbitMQ.ContractResolvers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Cegeka.Guild.Pokeverse.RabbitMQ
{
    internal sealed class RabbitMqMessageBus :IMessageBus
    {
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
            var queueName = model.QueueDeclare(handler.FullName, true, false, false).QueueName;
            model.QueueBind(queueName, topic, handler.FullName);

            var consumer = new EventingBasicConsumer(model);
            consumer.Received += Consume<T>(handler);

            model.BasicConsume(queueName, true, consumer);
        }

        private EventHandler<BasicDeliverEventArgs> Consume<T>(Type handlerType)
            where T : IMessage
        {
            return async (_, args) =>
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
            };
        }
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
                ContractResolver = new PrivateCamelCasePropertyContractResolver(),
            });
        }

        public static T DeserializerObject<T>(this string objectAsString)
        {
            return JsonConvert.DeserializeObject<T>(objectAsString, new JsonSerializerSettings
            {
                ContractResolver = new PrivateCamelCasePropertyContractResolver(),
            });
        }
    }
}