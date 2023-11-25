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
    public partial class Venta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnGenerarSolicitud_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;";

                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    con.Open();

                    // Obtener el RUT del cliente ingresado en el campo txtRutCliente
                    string rutCliente = txtRutCliente.Text.Trim();

                    // Obtener la observación del campo txtObservacion
                    string observacion = txtObservacion.Text.Trim();

                    // Consultar la base de datos
                    string insertQuery = "INSERT INTO CABECERA_PV (\"ID_CABECERA_PV\", \"FECHA_EMISION\",\"OBS_PV\",\"RUT_CLIENTE\",\"ESTADO_PV\",\"EMPRESA_TRANS\", \"ID_TIPO_VENTA\") " +
                                         "VALUES (CABECERA_PV_SEQ.NEXTVAL, SYSDATE, :Observacion, :RutCliente, 1, 1, 1)";

                    using (OracleCommand cmd = new OracleCommand(insertQuery, con))
                    {
                        cmd.Parameters.Add(new OracleParameter("Observacion", observacion));
                        cmd.Parameters.Add(new OracleParameter("RutCliente", rutCliente));

                        // Ejecutar la inserción
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Puedes mostrar un mensaje de éxito o realizar otras acciones si es necesario
                            Response.Write("Solicitud generada exitosamente.");
                        }
                        else
                        {
                            Response.Write("Error al generar la solicitud.");
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // Manejar errores aquí (mostrar un mensaje de error, registrar el error, etc.)
                Response.Write("Error al generar la solicitud: " + ex.Message);
            }
        }

        // Obtener el IdTipoVenta desde la tabla cliente basado en el RUT
        private int ObtenerIdTipoVenta(string rutCliente, OracleConnection con)
        {
            // Crear la consulta SELECT para obtener el IdTipoVenta desde la tabla cliente
            string query = "SELECT ID_TIPO FROM CLIENTE WHERE RUT_CLIENTE = :RutCliente";

            // Crear el comando Oracle
            using (OracleCommand cmd = new OracleCommand(query, con))
            {
                // Agregar el parámetro para el RUT
                cmd.Parameters.Add(new OracleParameter("RutCliente", OracleDbType.Varchar2)).Value = rutCliente;

                // Ejecutar la consulta SELECT
                object result = cmd.ExecuteScalar();

                // Verificar si se obtuvo un resultado válido
                if (result != null && result != DBNull.Value)
                {
                    // Convertir el resultado a entero
                    return Convert.ToInt32(result);
                }

                // Si no se obtiene un resultado, puedes devolver un valor predeterminado o lanzar una excepción según tus necesidades
                return 0; // Cambia esto según tu lógica
            }
        }

        protected void gvDetallesPV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Lógica del evento
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
                gvDetallesPV.DataSource = dt;
                gvDetallesPV.DataBind();
            }
        }

        protected void gvDetallesPV_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnAgregarPedido_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener valores de TextBox y Label fuera del DataGrid
                string idProducto = id_producto.Text;
                string cantidad = cantidadde.Text;

                // Insertar en la base de datos (código de inserción depende de tu estructura de tablas)
                string insertQuery = "INSERT INTO DETALLE_PV (ID_DETALLE_PV, ID_PRODUCTO, CANTIDAD, PRECIO_UNITARIO, ID_CABECERA_PV) " +
                                     $"VALUES (DETALLE_PV_SEQ.NEXTVAL, :IdProducto, :Cantidad, " +
                                     $"(SELECT p.precio FROM producto p WHERE p.id_producto = :IdProducto), " +
                                     "(SELECT MAX(id_cabecera_pv) FROM CABECERA_PV))";

                using (OracleConnection con = new OracleConnection("Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;"))
                {
                    con.Open();

                    using (OracleCommand cmd = new OracleCommand(insertQuery, con))
                    {
                        cmd.Parameters.Add(new OracleParameter("IdProducto", idProducto));
                        cmd.Parameters.Add(new OracleParameter("Cantidad", cantidad));

                        // Ejecutar la consulta INSERT
                        cmd.ExecuteNonQuery();
                    }

                    Response.Write("Pedido agregado exitosamente.");
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error al agregar el pedido: " + ex.Message);
            }
        }



        // Método ficticio para obtener el token desde la base de datos
        private string ObtenerTokenDesdeLaBaseDeDatos(string idProducto)
        {
            // Aquí implementa la lógica para obtener el token asociado al producto desde tu base de datos
            // Puedes consultar la base de datos y devolver el token correspondiente
            // En este ejemplo, simplemente devolveré un token de ejemplo
            return "537040496W707163N";
        }

        protected void btnPaypal_Click(object sender, EventArgs e)
        {
            // URL a la que deseas redireccionar
            string urlExterna = "https://www.sandbox.paypal.com/checkoutnow?token=34C92048HK7174418";

            // Redireccionar a la página externa
            Response.Redirect(urlExterna);
        }


        protected void btnDefault_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void btnCompra_Click(object sender, EventArgs e)
        {
            Response.Redirect("compra.aspx");
        }



    }

}