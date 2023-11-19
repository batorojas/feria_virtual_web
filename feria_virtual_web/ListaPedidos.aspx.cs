using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;

namespace feria_virtual_web
{
    public partial class ListaPedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            string connectionString = "Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;";

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("SELECT CS.ID_CABECERA_SUBASTA, CS.FECHA_LIMITE_ENTREGA, CS.FECHA_DESPACHO_REALIZADO, CO.NOMBRE_COMUNA, CS.ID_CABECERA_PV, PV.RUT_CLIENTE, ES.DESC_ESTADO FROM CABECERA_SUBASTA CS JOIN COMUNA CO ON CS.ID_COMUNA = CO.ID JOIN ESTADO_SUBASTA ES ON CS.ID_ESTADO_SUBASTA = ES.ID_ESTADO_SUBASTA JOIN CABECERA_PV PV ON CS.ID_CABECERA_PV = PV.ID_CABECERA_PV WHERE CS.FECHA_DESPACHO_REALIZADO IS NOT NULL", con);
                con.Open();
                OracleDataAdapter sda = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                GridViewListaPedidos.DataSource = dt;
                GridViewListaPedidos.DataBind();
            }
        }

    }
}