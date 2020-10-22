using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication
{
    public class Comanda
    {       
        public Comanda()
        {
            itens = new List<Comanda_itens>();
        }

        [Key]   
        public int id { get; set; }

        //valor da comanda
        public double? valor { get; set; }

        //desconto da comanda        
        public double? desconto { get; set; }
        
        public ICollection<Comanda_itens> itens { get; set; }

    }
}
