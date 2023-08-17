using SellSnacks.Models;

namespace SellSnacks.Repositories.Interfaces
{
    public interface ILancheRepository
    {
        IEnumerable<Lanche> Lanches { get;}
        IEnumerable<Lanche> LanchesPreferidos { get;}
        Lanche GetLancheById(int lancheId);
    }
}
