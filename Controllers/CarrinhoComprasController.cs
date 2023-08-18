using Microsoft.AspNetCore.Mvc;
using SellSnacks.Models;
using SellSnacks.Repositories.Interfaces;
using SellSnacks.ViewModels;

namespace SellSnacks.Controllers
{
    public class CarrinhoComprasController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoComprasController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
        {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {   
            var result = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItens = result;
            var carrinho = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };
            
            return View(carrinho);
        }
        public RedirectToActionResult AdicionarItemNoCarrinhoCompra (int lancheId)
        {
            var itemSelecionado = _lancheRepository.Lanches.FirstOrDefault(x=> x.LancheId == lancheId);
            if(itemSelecionado !=  null)
            {
                _carrinhoCompra.AdicionarAoCarrinho(itemSelecionado);
            }
            return RedirectToAction("index");
        }
        public IActionResult RemoverItemDoCarrinho(int id)
        {
            var itemSelecionado = _lancheRepository.Lanches.FirstOrDefault(x => x.LancheId == id);
            if (itemSelecionado != null)
            {
                _carrinhoCompra.RemoverDoCarrinho(itemSelecionado);
            }
            return RedirectToAction("index");
        }
    }
}
