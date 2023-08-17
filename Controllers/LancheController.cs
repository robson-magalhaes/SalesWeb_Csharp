using Microsoft.AspNetCore.Mvc;
using SellSnacks.Repositories.Interfaces;
using SellSnacks.ViewModels;

namespace SellSnacks.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List()
        {
            var lancheLisViewModel = new LancheListViewModel();
            lancheLisViewModel.Lanches = _lancheRepository.Lanches;
            lancheLisViewModel.CategoriaAtual = "Categoria Atual";
            return View(lancheLisViewModel);
        }
    }
}
