﻿using SellSnacks.Context;
using SellSnacks.Models;
using SellSnacks.Repositories.Interfaces;

namespace SellSnacks.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext contexto)
        {
            _context = contexto;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
