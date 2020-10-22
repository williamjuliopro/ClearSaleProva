using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebClient
{
    public class Comanda_itens
    {       
        [Key]
        public int id { get; set; }
        
        public int comanda_id { get; set; }

        public int itens_id { get; set; }

        public int quantidade { get; set; }

        public string descricao { get; set; }

        public Double valor { get; set; }

    }
}
