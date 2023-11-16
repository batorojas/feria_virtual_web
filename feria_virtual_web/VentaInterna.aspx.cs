using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using Telegram.Bot;
using System.Net.Http;


namespace feria_virtual_web
{
    public partial class VentaInterna : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
        }



        private void BindGrid()
        {
            string connectionString = "Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;";

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("select ID_PRODUCTO,NOMBRE_PRODUCTO,PRECIO,(select stock from stock_producto where id_producto =stock_producto.id_producto)stock from producto;", con);
                con.Open();
                OracleDataAdapter sda = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                GridViewVentaInterna.DataSource = dt;
                GridViewVentaInterna.DataBind();
            }
        }


        protected void GridViewVentaInterna_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}