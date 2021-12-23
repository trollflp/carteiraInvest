using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarteiraSafra.ViewModels
{
    public class CreateAcaoViewModel
    {
        [Required]
        public string RazaoSocial { get; set; }

        [Required]
        public string Codigo { get; set; }
    }
}
