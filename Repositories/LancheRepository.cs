using Microsoft.EntityFrameworkCore;
using SellSnacks.Context;
using SellSnacks.Models;
using SellSnacks.Repositories.Interfaces;

namespace SellSnacks.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _context;

        public LancheRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(x => x.Categoria);
        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches.Where(x => x.IsLanchePreferido).Include(x => x.Categoria);
        public Lanche GetLancheById(int lancheId) => _context.Lanches.FirstOrDefault(x=>x.LancheId == lancheId);
    }
}
