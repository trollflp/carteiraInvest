using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarteiraSafra.Models
{
    public class Operacao 
    {
        [Key]
        public int Id { get; set; }
        public string CodigoAcao { get; set; }
        public string RazaoSocial { get; set; }
        public string TipoOperacao { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public double QuantidadeOperacao { get; set; }
        public double ValorAcao { get; set; }
        public double ValorTotalOperacao { get; set; }
    }
}
