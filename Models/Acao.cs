﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarteiraSafra.Models
{
    public class Acao
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string RazaoSocial { get; set; }        
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
