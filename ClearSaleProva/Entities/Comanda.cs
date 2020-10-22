using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi
{
    [Table("comanda")]
    public class Comanda
    {       
        public Comanda()
        {
            itens = new List<Comanda_itens>();
        }

        [Key]
        [Column("id")]
        public int id { get; set; }

        //valor da comanda
        [Column("valor")]
        public double? valor { get; set; }

        //desconto da comanda
        [NotMapped]
        public double? desconto { get; set; }

        //[ForeignKey("fk_comanda_id")]
        //public int comanda_id { get; }

        [NotMapped]
        public ICollection<Comanda_itens> itens { get; set; }

    }
}
