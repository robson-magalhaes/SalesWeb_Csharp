using Microsoft.AspNetCore.Mvc;
using SellSnacks.Repositories.Interfaces;

namespace SellSnacks.Components;

public class CategoriaMenu : ViewComponent
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaMenu(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }
    public IViewComponentResult Invoke()
    {
        var _categorias = _categoriaRepository.Categorias.OrderBy(x => x.CategoriaNome);
        return View(_categorias);
    }
}
