using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

namespace feria_virtual_web
{
    public partial class Productor : System.Web.UI.Page
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
            string connectionString = "DATA SOURCE = XE; PASSWORD = 123; USER ID = maipogrande"; 
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM PRODUCTOS", con); 
                con.Open();
                OracleDataAdapter sda = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                mostrar.DataSource = dt;
                mostrar.DataBind();
            }
        }


        protected void eliminar_producto_Click(object sender, EventArgs e)
        {
            using (OracleConnection conexion = new OracleConnection("DATA SOURCE = XE ; PASSWORD = 123 ; USER ID= maipogrande"))
            {
                OracleCommand comando = new OracleCommand("DELETE PRODUCTOS WHERE RUT = :id_producto", conexion);
                comando.Parameters.Add(":id_producto", ID_producto.Text);
                conexion.Open();
                comando.ExecuteNonQuery();
                conexion.Close();
            }
            BindGrid();

        }

        protected void modificar_producto_Click(object sender, EventArgs e)
        {
            using (OracleConnection conexion = new OracleConnection("DATA SOURCE = XE ; PASSWORD = 123 ; USER ID= maipogrande"))
            {

                OracleCommand comando = new OracleCommand("UPDATE PRODUCTO SET RUT = :id_producto, NOMBRE = :id_categoria,NOMBRE_PRODUCTO = :nombre_producto, PRECIO = :precio, CANTIDAD = :id_calidad WHERE RUT = :id_producto", conexion);
                comando.Parameters.Add(":id_producto", ID_producto.Text);
                comando.Parameters.Add(":id_categoria", ID_CATEGORIA.Text);
                comando.Parameters.Add(":nombre_producto", NOMBRE_PRODUCTO.Text);
                comando.Parameters.Add(":precio", PRECIO.Text);
                comando.Parameters.Add(":id_calidad", ID_CALIDAD.Text);
                conexion.Open();
                comando.ExecuteNonQuery();
                conexion.Close();
            }
            BindGrid();

        }

        protected void agregar_produto_Click1(object sender, EventArgs e)
        {
            using (OracleConnection conexion = new OracleConnection("DATA SOURCE = XE ; PASSWORD = 123 ; USER ID= maipogrande"))
            {

                OracleCommand comando = new OracleCommand("INSERT INTO PRODUCTOS (RUT,NOMBRE,NOMBRE_PRODUCTO,PRECIO,CANTIDAD) VALUES (:id_producto,:id_categoria,:nombre_producto,:precio,:id_calidad)", conexion);
                comando.Parameters.Add(":id_producto", ID_producto.Text);
                comando.Parameters.Add(":id_categoria", ID_CATEGORIA.Text);
                comando.Parameters.Add(":nombre_producto", NOMBRE_PRODUCTO.Text);
                comando.Parameters.Add(":precio", PRECIO.Text);
                comando.Parameters.Add(":id_calidad", ID_CALIDAD.Text);
                conexion.Open();
                comando.ExecuteNonQuery();
                conexion.Close();
            }
            BindGrid();
        }

        protected void mostrar_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}