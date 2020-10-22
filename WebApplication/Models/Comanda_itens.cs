using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication
{
    [Table("comanda_itens")]
    public class Comanda_itens
    {       
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Column("comanda_id")]
        public int comanda_id { get; set; }
        //[ForeignKey("itens")]
        //public Itens Itens { get; set; }

        [Column("itens_id")]  
        public int itens_id { get; set; }

        [Column("quantidade")]
        public int quantidade { get; set; }
        
        public string descricao { get; set; }        
        public Double valor { get; set; }

    }
}
