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
                ListarCategoria();
                ListarCalidad();
            }
        }

        private void BindGrid()
        {
            string connectionString = "Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;";
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand("SELECT P.ID_PRODUCTO, P.NOMBRE_PRODUCTO, P.PRECIO, C.DESCRIPCION, TO_CHAR(P.PORCENTAJE_MERMA * 100, '999') || '%' AS PORCENTAJE_MERMA FROM PRODUCTO P INNER JOIN CALIDAD C ON P.ID_CALIDAD = C.ID_CALIDAD", con);
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
                string script = "alert('El producto se ha eliminado exitosamente');";
                ClientScript.RegisterStartupScript(this.GetType(), "EliminacionExitosa", script, true); 
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
            BindGrid();
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

        private void ListarCategoria()
        {
            string connectionString = "Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;";
            string query = "SELECT * FROM CATEGORIA";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    connection.Open();

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        // Limpiar DropDownList antes de agregar nuevas categorías
                        ddlCategoria.Items.Clear();

                        // Agregar cada categoría al DropDownList
                        while (reader.Read())
                        {
                            // Obtener el valor y texto de la columna NOMBRE_CATEGORIA
                            string valorCategoria = reader["NOMBRE_CATEGORIA"].ToString();
                    
                            // Agregar el ListItem al DropDownList con el valor y texto
                            ddlCategoria.Items.Add(new ListItem(valorCategoria, valorCategoria));
                        }
                    }
                }
            }
        }
        
        private void ListarCalidad()
        {
            string connectionString = "Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;";
            string query = "SELECT * FROM CALIDAD";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    connection.Open();

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        // Limpiar DropDownList antes de agregar nuevas categorías
                        ddlCalidad.Items.Clear();

                        // Agregar cada categoría al DropDownList
                        while (reader.Read())
                        {
                            // Obtener el valor y texto de la columna NOMBRE_CATEGORIA
                            string valorCalidad = reader["DESCRIPCION"].ToString();
                    
                            // Agregar el ListItem al DropDownList con el valor y texto
                            ddlCalidad.Items.Add(new ListItem(valorCalidad, valorCalidad));
                        }
                    }
                }
            }
        }
        
        
    


        protected void agregar_producto_Click1(object sender, EventArgs e)
        {
            using (OracleConnection conexion = new OracleConnection("Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;"))
            {
                OracleCommand comando = new OracleCommand("INSERT INTO PRODUCTO (ID_PRODUCTO, ID_CATEGORIA, NOMBRE_PRODUCTO, PRECIO, ID_CALIDAD, PORCENTAJE_MERMA) VALUES (PRODUCTO_SEQ.NEXTVAL, :ID_CATEGORIA, :NOMBRE_PRODUCTO, :PRECIO, :ID_CALIDAD, :PORCENTAJE_MERMA)", conexion);
                int indiceSeleccionado = ddlCategoria.SelectedIndex;
                int indiceSeleccionado2 = ddlCalidad.SelectedIndex;
                // Asignar el índice seleccionado como ID_CATEGORIA
                comando.Parameters.Add("ID_CATEGORIA", OracleDbType.Int32).Value = indiceSeleccionado + 1;
                comando.Parameters.Add("NOMBRE_PRODUCTO", OracleDbType.Varchar2).Value = NOMBRE_PRODUCTO.Text;
                comando.Parameters.Add("PRECIO", OracleDbType.Decimal).Value = Convert.ToDecimal(PRECIO.Text);
                comando.Parameters.Add("ID_CALIDAD", OracleDbType.Int32).Value = indiceSeleccionado2 + 1;
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