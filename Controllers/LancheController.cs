﻿using SellSnacks.Models;
using SellSnacks.Repositories.Interfaces;
using SellSnacks.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers;

public class LancheController : Controller
{
    private readonly ILancheRepository _lancheRepository;

    public LancheController(ILancheRepository lancheRepository)
    {
        _lancheRepository = lancheRepository;
    }

    public IActionResult List(string categoria)
    {
        IEnumerable<Lanche> lanches;
        string categoriaAtual = string.Empty;

        if (string.IsNullOrEmpty(categoria))
        {
            lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
            categoriaAtual = "Todos os lanches";
        }
        else
        {
            //if (string.Equals("Normal", categoria, StringComparison.OrdinalIgnoreCase))
            //{
            //    lanches = _lancheRepository.Lanches
            //        .Where(l => l.Categoria.CategoriaNome.Equals("Normal"))
            //        .OrderBy(l => l.Nome);
            //}
            //else
            //{
            //    lanches = _lancheRepository.Lanches
            //       .Where(l => l.Categoria.CategoriaNome.Equals("Natural"))
            //       .OrderBy(l => l.Nome);
            //}
            lanches = _lancheRepository.Lanches.Where(x => x.Categoria.CategoriaNome.Equals(categoria)).OrderBy(x=>x.Nome);
            categoriaAtual = categoria;
        }

        var lanchesListViewModel = new LancheListViewModel
        {
            Lanches = lanches,
            CategoriaAtual = categoriaAtual
        };

        return View(lanchesListViewModel);
    }

    public IActionResult Details(int lancheId)
    {
        var lanche = _lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
        return View(lanche);
    }

    public ViewResult Search(string searchString)
    {
        IEnumerable<Lanche> lanches;
        string categoriaAtual = string.Empty;

        if (string.IsNullOrEmpty(searchString))
        {
            lanches = _lancheRepository.Lanches.OrderBy(x=>x.LancheId);
            categoriaAtual = "Todos os lanches";
        }
        else
        {
            lanches = _lancheRepository.Lanches.Where(x => x.Nome.ToLower().Contains(searchString.ToLower()));
            if (lanches.Any()) {
                categoriaAtual = "Lanches";
            }
            else
            {
                categoriaAtual = "Nenhum lanches encontrado";
            }
        }

        return View("List", new LancheListViewModel
        {
            Lanches = lanches,
            CategoriaAtual = categoriaAtual
        });
    }
}
