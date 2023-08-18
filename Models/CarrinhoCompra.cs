using Microsoft.EntityFrameworkCore;
using SellSnacks.Context;

namespace SellSnacks.Models;

public class CarrinhoCompra
{
    private readonly AppDbContext _context;
    public string CarrinhoCompraId { get; set; }
    public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

    public CarrinhoCompra(AppDbContext context)
    {
        _context = context;
    }

    public static CarrinhoCompra GetCarrinho(IServiceProvider services)
    {
        //Definindo uma sessão. Obs: o codigo " ?." verifica se existe uma sessão, se o resultado nao for null ele retorna a session
        ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
        //obtem um serviço do tipo do nosso contexto
        var context = services.GetService<AppDbContext>();
        //obtem ou gera o Id do carrinho
        string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();
        //atribui o id do carrinho na Sessão
        session.SetString("CarrinhoId", carrinhoId);
        return new CarrinhoCompra(context)
        {
            CarrinhoCompraId = carrinhoId
        };
    }

    public void AdicionarAoCarrinho(Lanche lanche)
    {
        var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(x => x.Lanche.LancheId == lanche.LancheId
        && x.CarrinhoCompraId == CarrinhoCompraId);

        if (carrinhoCompraItem == null)
        {
            carrinhoCompraItem = new CarrinhoCompraItem
            {
                CarrinhoCompraId = CarrinhoCompraId,
                Lanche = lanche,
                Quantidade = 1
            };
            _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
        }
        else
        {
            carrinhoCompraItem.Quantidade++;
        }
        _context.SaveChanges();
    }

    public int RemoverDoCarrinho(Lanche lanche)
    {
        var carrinho = _context.CarrinhoCompraItens.SingleOrDefault(
            x => x.Lanche.LancheId == lanche.LancheId && x.CarrinhoCompraId == CarrinhoCompraId);

        var quantidadeTotal = 0;
        if (carrinho.Quantidade > 0)
        {
            carrinho.Quantidade--;
            quantidadeTotal = carrinho.Quantidade;
        }
        else
        {
            _context.CarrinhoCompraItens.Remove(carrinho);
        }
        _context.SaveChanges();
        return quantidadeTotal;

    }

    public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
    {
        return CarrinhoCompraItens ?? (CarrinhoCompraItens = _context.CarrinhoCompraItens
            .Where(x => x.CarrinhoCompraId == CarrinhoCompraId)
            .Include(x => x.Lanche).ToList());
    }

    public void LimparCarrinho()
    {
        var carrinho = _context.CarrinhoCompraItens
            .Where(x => x.CarrinhoCompraId == CarrinhoCompraId);
        _context.CarrinhoCompraItens.RemoveRange(carrinho);
        _context.SaveChanges();
    }

    public decimal GetCarrinhoCompraTotal()
    {
        var total = _context.CarrinhoCompraItens
            .Where(x => x.CarrinhoCompraId == CarrinhoCompraId)
            .Select(x => x.Lanche.Preco * x.Quantidade).Sum();
        return total;
    }
}
