using System.Threading.Tasks;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    public interface ISeedService
    {
        Task Seed();
    }
}