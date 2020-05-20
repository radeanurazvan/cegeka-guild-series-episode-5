using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Domain;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business
{
    internal sealed class GetAllTrainersQueryHandler : IRequestHandler<GetAllTrainersQuery, IEnumerable<TrainerModel>>
    {
        
        private readonly IReadRepository<Trainer> trainerReadRepository;

        public GetAllTrainersQueryHandler(IReadRepository<Trainer> trainerReadRepository)
        {
            this.trainerReadRepository = trainerReadRepository;
        }

        public async Task<IEnumerable<TrainerModel>> Handle(GetAllTrainersQuery request, CancellationToken cancellationToken)
        {
            return (await this.trainerReadRepository.GetAll()).Select(t => new TrainerModel
            {
                Id = t.Id,
                Name = t.Name,
                Pokemons = t.Pokemons.Select(p => new PokemonModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Abilities = p.Abilities.Select(x => new AbilityModel
                    {
                        Id = x.Id, Name = x.Name
                    }).ToList()
                }).ToList()
            });
        }
    }
}