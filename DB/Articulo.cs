﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Articulo
    {
        [Key]
        public int id {  get; set; }
        public string nombre { get; set; }

        public string descripcion { get; set; }
        public int totalTienda { get; set; }
        public int tiendaId { get; set; }
        [ForeignKey ("tiendaId")]
        public virtual Tienda tiendas { get; set; }
    }
}
