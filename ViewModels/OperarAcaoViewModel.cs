using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarteiraSafra.ViewModels
{
    public class OperarAcaoViewModel
    {
        [Required]
        public string CodigoAcao { get; set; }
        [Required]
        public string TipoOperacao { get; set; }
        [Required]
        public double QuantidadeOperacao { get; set; }        
    }
}
