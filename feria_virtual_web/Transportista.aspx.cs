using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace feria_virtual_web
{
    public partial class Transportista : System.Web.UI.Page
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
                OracleCommand cmd = new OracleCommand("SELECT * FROM cabecera_subasta", con);
                con.Open();
                OracleDataAdapter sda = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                GridViewSubasta.DataSource = dt;
                GridViewSubasta.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ActualizarFechaEntrega")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewSubasta.Rows[index];
                string idSubasta = row.Cells[0].Text;
                ActualizarFechaDespachoRealizado(idSubasta);
                BindGrid();
            }
        }

        private void ActualizarFechaDespachoRealizado(string idCabeceraSubasta)
        {
            string connectionString = "Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;";
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                string query = "UPDATE cabecera_subasta SET FECHA_DESPACHO_REALIZADO = SYSDATE WHERE ID_CABECERA_SUBASTA = :idCabeceraSubasta";
                using (OracleCommand cmd = new OracleCommand(query, con))
                {
                    cmd.Parameters.Add(":idCabeceraSubasta", OracleDbType.Varchar2).Value = idCabeceraSubasta;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}