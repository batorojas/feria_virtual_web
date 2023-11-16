using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace feria_virtual_web
{
    public class DetallePV
    {
        public int ID_DETALLE_PV { get; set; }
        public int ID_PRODUCTO { get; set; }
        public int CANTIDAD { get; set; }
        public decimal PRECIO_UNITARIO { get; set; }
        public int ID_CABECERA_PV { get; set; }

    }
}