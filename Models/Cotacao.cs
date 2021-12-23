using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarteiraSafra.Models
{
    public class Cotacao : Acao
    {
        public double PrecoCompra { get; set; }
        public double PrecoVenda { get; set; }

    }
}
