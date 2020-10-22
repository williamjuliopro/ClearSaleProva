using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class Itens
    {

        [Key]
        public int id { get; set; }

        public string descricao { get; set; }

        public double valor { get; set; }
    }
}
