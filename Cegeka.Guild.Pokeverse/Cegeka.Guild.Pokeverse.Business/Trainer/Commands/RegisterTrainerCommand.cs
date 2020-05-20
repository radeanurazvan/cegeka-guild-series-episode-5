using CSharpFunctionalExtensions;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business
{
    public sealed class RegisterTrainerCommand : IRequest<Result>
    {
        public RegisterTrainerCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}