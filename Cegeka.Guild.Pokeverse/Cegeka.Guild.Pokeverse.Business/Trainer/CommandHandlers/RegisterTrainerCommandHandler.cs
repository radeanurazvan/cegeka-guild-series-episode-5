using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Domain;
using CSharpFunctionalExtensions;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business
{
    internal sealed class RegisterTrainerCommandHandler : IRequestHandler<RegisterTrainerCommand, Result>
    {
        private readonly IRepositoryMediator mediator;

        public RegisterTrainerCommandHandler(IRepositoryMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Result> Handle(RegisterTrainerCommand request, CancellationToken cancellationToken)
        {
            var writeRepository = this.mediator.Write<Trainer>();
            return await Trainer.Create(request.Name)
                .Tap(t => writeRepository.Add(t))
                .Tap(() => writeRepository.Save());
        }
    }
}