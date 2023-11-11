using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace feria_virtual_web
{
    public partial class paypal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Pagar_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://www.sandbox.paypal.com/checkoutnow?token=2FK27307GB1809422");
        
        }
    }
}