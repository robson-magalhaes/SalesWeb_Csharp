using Microsoft.AspNetCore.Mvc;
using SellSnacks.Models;
using SellSnacks.ViewModels;

namespace SellSnacks.Components;
public class CarrinhoCompraResumo : ViewComponent
{
    private readonly CarrinhoCompra _carrinhoCompra;

    public CarrinhoCompraResumo(CarrinhoCompra carrinhoCompra)
    {
        _carrinhoCompra = carrinhoCompra;
    }

    public IViewComponentResult Invoke()
    {
        var result = _carrinhoCompra.GetCarrinhoCompraItens();
        //var result = new List<CarrinhoCompraItem> {
        //     new CarrinhoCompraItem(),
        //     new CarrinhoCompraItem()
        //};
        _carrinhoCompra.CarrinhoCompraItens = result;

        var carrinho = new CarrinhoCompraViewModel
        {
            CarrinhoCompra = _carrinhoCompra,
            CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
        };
        return View(carrinho);
    }
}
