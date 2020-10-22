using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Route("Comanda")]
    public class ComandaController : Controller
    {
        private DatabaseContext db;

        public ComandaController(DatabaseContext _db)
        {
            db = _db;

        }

        //[Route("")]
        //[Route("~/")]
        //[Route("index")]
        //public IActionResult Index()
        //{

        //    ViewBag.comanda = db.Comanda.ToList();
        //    return View();
        //}

        //[HttpGet()]
        //public IActionResult Listar()
        //{
        //    return Ok(db.Comanda.ToList());
        //}



        [HttpGet()]
        public IActionResult abertura(int comanda_id, int itens_id, int itens_quantidade = 1)
        {
            Comanda comanda;
            bool insert = false;

            //pesquisa se já existe a comanda
            var ListaDb = db.Comanda.ToList();
            comanda = ListaDb.FirstOrDefault(x => x.id == comanda_id);

            if (comanda == null)
            {
                comanda = new Comanda();
                insert = true;
            }

            comanda.id = comanda_id;

            //validar item cadastrado
            var cadastroitens = db.Itens.Where(x => x.id == itens_id).FirstOrDefault();
            if (cadastroitens == null)            
                return NotFound("O item não foi encontrado");

            //Insere os itens da comanda
            Comanda_itens comanda_itens = db.Comanda_itens.FirstOrDefault(x => x.comanda_id == comanda_id && x.itens_id == itens_id);


            if (comanda_itens == null)
            {
                comanda_itens = new Comanda_itens { comanda_id = comanda_id, itens_id = itens_id, quantidade = itens_quantidade };
                db.Comanda_itens.Add(comanda_itens);
                //item add          
            }
            else
            {           
                //quantide
                comanda_itens.quantidade++;

            }

            //Só é permitido 3 sucos por comanda.
            if (cadastroitens.descricao == "Suco" && comanda_itens.quantidade > 3)
            {
                return NotFound("Só é permitido 3 sucos por comanda");
            }


            //item add 
            List<Comanda_itens> ListaComanda_itens = new List<Comanda_itens>();
            ListaComanda_itens.Add(comanda_itens);
            comanda.itens = Getitens(ListaComanda_itens);


            //Evitar lançar 2x o mesmo item

            if (insert)
                db.Comanda.Add(comanda);

            //persiste as alterações no banco de dados
            db.SaveChanges();

            return Ok(comanda);
        }
        
    



        [HttpPut()]
        public IActionResult fechamento(int comanda_id)
        {
            Comanda comanda = db.Comanda.Where(x => x.id == comanda_id).FirstOrDefault();
            if (comanda != null)
            {
                comanda.itens = GetComanda_itens(comanda_id);

                comanda.desconto = GetDesconto(comanda);

                comanda.valor = GetValor(comanda.itens); 

            }

            return Ok(comanda);
        }

        private double? GetValor(ICollection<Comanda_itens> comanda_itens)
        {
            double valor = 0;

            foreach (var item in comanda_itens)
            {
                valor += item.valor * item.quantidade;
            }

            return valor;
        }

        private double? GetDesconto(Comanda comanda)
        {
            double desconto = 0;

            //Na compra de 1 cerveja e 1 suco, essa cerveja sai por 3 reais
            var cerveja = comanda.itens.Where(x => x.descricao == "Cerveja").FirstOrDefault();
            var suco = comanda.itens.Where(x => x.descricao == "Suco").FirstOrDefault();

            if (cerveja != null && suco != null)
            {
                if (cerveja.quantidade == 1 && suco.quantidade == 1)
                {
                    desconto = 2;
                    cerveja.valor = 3;
                }


                //desconto += cerveja.valor - 2; //desconto de 2 reais
            }

            //Se o cliente comprar 3 conhaques mais 2 cervejas, poderá pedir uma água de graça.
            var conhaque = comanda.itens.Where(x => x.descricao == "Conhaque").FirstOrDefault();
            if (conhaque != null && cerveja != null)
            {
                if (conhaque.quantidade == 3 && cerveja.quantidade == 2)
                {
                    var agua = comanda.itens.Where(x => x.descricao == "Agua").FirstOrDefault();
                    if (agua != null)
                    {
                        desconto = agua.valor;
                        agua.valor = 0;
                    }
                }
            }

            return desconto;
        }

        [HttpDelete()]
        public IActionResult resetar(int comanda_id)
        {
            Comanda comanda = db.Comanda.Where(x => x.id == comanda_id).FirstOrDefault();
            List<Comanda_itens> ListaComanda_itens = db.Comanda_itens.Where(x => x.comanda_id == comanda_id).ToList();

            foreach (var item in ListaComanda_itens)
            {
                db.Comanda_itens.Remove(item);
            }

            //persiste as alterações no banco de dados
            db.SaveChanges();

            return Ok(db.Comanda.ToList());
        }

        private ICollection<Comanda_itens> GetComanda_itens(int comanda_id)
        {

            var comanda_itens = db.Comanda_itens.Where(x => x.comanda_id == comanda_id).ToList();

            //itens
            List<Itens> itens = db.Itens.ToList();
            foreach (var item in comanda_itens)
            {
                var itensSelecionado = itens.Where(x => x.id == item.itens_id).FirstOrDefault();
                item.descricao = itensSelecionado.descricao;
                item.valor = itensSelecionado.valor;
            }

            return comanda_itens;

        }

        private ICollection<Comanda_itens> Getitens(List<Comanda_itens> comanda_itens)
        {
            //itens
            List<Itens> itens = db.Itens.ToList();
            foreach (var item in comanda_itens)
            {
                var itensSelecionado = itens.Where(x => x.id == item.itens_id).FirstOrDefault();
                if (itensSelecionado != null)
                {
                    item.descricao = itensSelecionado.descricao;
                    item.valor = itensSelecionado.valor;
                }
            }

            return comanda_itens;

        }
    }
}
