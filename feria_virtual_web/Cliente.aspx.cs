using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace feria_virtual_web
{
    public partial class Cliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnDefault_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void btnCompra_Click(object sender, EventArgs e)
        {
            Response.Redirect("compra.aspx");
        }

        protected void btnVenta_Click(object sender, EventArgs e)
        {
            Response.Redirect("venta.aspx");
        }


    }
}