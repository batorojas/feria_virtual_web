using System;
using System.Web.UI;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Web.UI.WebControls;

namespace feria_virtual_web
{
    public partial class CategoriaProducto : Page
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
                OracleCommand cmd = new OracleCommand("SELECT * FROM CATEGORIA", con);
                con.Open();
                OracleDataAdapter sda = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                mostrar.DataSource = dt;
                mostrar.DataBind();
            }
        }

        protected void btnAgregarCategoria_OnClick(object sender, EventArgs e)
        {
            using (OracleConnection conexion =
                   new OracleConnection("Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;"))
            {
                conexion.Open();
                OracleCommand comando = new OracleCommand("INSERT INTO CATEGORIA (ID_CATEGORIA, NOMBRE_CATEGORIA) VALUES (CATEGORIA_SEQ.NEXTVAL, :NOMBRE_CATEGORIA)", conexion);
                comando.Parameters.Add(":descripcion", OracleDbType.Varchar2).Value = NOMBRE_CATEGORIA.Text;
                comando.ExecuteNonQuery();
                conexion.Close();
                string script = "alert('La categoría se ha agregado exitosamente');";
                ClientScript.RegisterStartupScript(this.GetType(), "AgregacionExitosa", script, true);
            }
            BindGrid();
        }
    }
}