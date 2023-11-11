using Oracle.ManagedDataAccess.Client;
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
            string connectionString = "Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;"; 
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM PRODUCTO", con); 
                con.Open();
                OracleDataAdapter sda = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                mostrar.DataSource = dt;
                mostrar.DataBind();
            }
        }
        
        protected void mostrar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Obtén el valor del ID_PRODUCTO en la fila seleccionada
                string idProducto = mostrar.DataKeys[rowIndex]["ID_PRODUCTO"].ToString();

                // Llama al método de eliminación utilizando el ID_PRODUCTO
                EliminarProducto(idProducto);

                // Actualiza el GridView después de la eliminación si es necesario
                // Puedes volver a cargar los datos desde la base de datos o simplemente eliminar la fila del GridView
                mostrar.DataBind();
            }
        }


        private void EliminarProducto(string idProducto)
        {
            using (OracleConnection conexion = new OracleConnection("Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;"))
            {
                using (OracleCommand comando = new OracleCommand("DELETE FROM PRODUCTO WHERE ID_PRODUCTO = :ID_PRODUCTO", conexion))
                {
                    comando.Parameters.Add(":ID_PRODUCTO", OracleDbType.Int32).Value = Convert.ToInt32(idProducto);

                    try
                    {
                        conexion.Open();
                        comando.ExecuteNonQuery();
                        // Puedes agregar código adicional después de ejecutar la consulta
                    }
                    catch (Exception ex)
                    {
                        // Manejar la excepción, puedes mostrar un mensaje o realizar alguna acción específica.
                        // Por ejemplo, puedes agregar un control de errores o registrar la excepción en algún registro.
                    }
                } // La cláusula using se encargará de cerrar el comando automáticamente
            } // La cláusula using se encargará de cerrar la conexión automáticamente
        }


        // protected void modificar_producto_Click(object sender, EventArgs e)
        // {
        //     using (OracleConnection conexion = new OracleConnection("Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;"))
        //     {
        //
        //         OracleCommand comando = new OracleCommand("UPDATE PRODUCTO SET RUT = :id_producto, NOMBRE = :id_categoria,NOMBRE_PRODUCTO = :nombre_producto, PRECIO = :precio, CANTIDAD = :id_calidad WHERE RUT = :id_producto", conexion);
        //         comando.Parameters.Add(":id_producto", ID_producto.Text);
        //         comando.Parameters.Add(":id_categoria", ID_CATEGORIA.Text);
        //         comando.Parameters.Add(":nombre_producto", NOMBRE_PRODUCTO.Text);
        //         comando.Parameters.Add(":precio", PRECIO.Text);
        //         comando.Parameters.Add(":id_calidad", ID_CALIDAD.Text);
        //         conexion.Open();
        //         comando.ExecuteNonQuery();
        //         conexion.Close();
        //     }
        //     BindGrid();
        //
        // }

        protected void agregar_producto_Click1(object sender, EventArgs e)
        {
            using (OracleConnection conexion = new OracleConnection("Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;"))
            {
                OracleCommand comando = new OracleCommand("INSERT INTO PRODUCTO (ID_PRODUCTO, ID_CATEGORIA, NOMBRE_PRODUCTO, PRECIO, ID_CALIDAD, PORCENTAJE_MERMA) VALUES (PRODUCTO_SEQ.NEXTVAL, :ID_CATEGORIA, :NOMBRE_PRODUCTO, :PRECIO, :ID_CALIDAD, :PORCENTAJE_MERMA)", conexion);
                comando.Parameters.Add("ID_CATEGORIA", OracleDbType.Int32).Value = Convert.ToInt32(ID_CATEGORIA.Text);
                comando.Parameters.Add("NOMBRE_PRODUCTO", OracleDbType.Varchar2).Value = NOMBRE_PRODUCTO.Text;
                comando.Parameters.Add("PRECIO", OracleDbType.Decimal).Value = Convert.ToDecimal(PRECIO.Text);
                comando.Parameters.Add("ID_CALIDAD", OracleDbType.Int32).Value = Convert.ToInt32(ID_CALIDAD.Text);
                comando.Parameters.Add("PORCENTAJE_MERMA", OracleDbType.Decimal).Value = Convert.ToDecimal(PORCENTAJE_MERMA.Text);
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