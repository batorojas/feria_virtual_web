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

                    // Consultar la base de datos
                    string selectQuery = "SELECT cp.ID_CABECERA_PV, cp.FECHA_EMISION, cp.OBS_PV, cp.RUT_CLIENTE, cp.ESTADO_PV, cp.EMPRESA_TRANS, cp.ID_TIPO_VENTA " +
                                         "FROM cabecera_pv cp " +
                                         "JOIN cliente c ON cp.RUT_CLIENTE = c.RUT " +
                                         "WHERE c.RUT = :RutCliente";

                    using (OracleCommand cmd = new OracleCommand(selectQuery, con))
                    {
                        cmd.Parameters.Add(new OracleParameter("RutCliente", rutCliente));

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            // Iterar sobre los resultados y realizar las acciones necesarias
                            while (reader.Read())
                            {
                                // Acceder a los valores de las columnas, por ejemplo:
                                int idCabeceraPV = reader.GetInt32(reader.GetOrdinal("ID_CABECERA_PV"));
                                // Puedes acceder a otras columnas de la misma manera
                            }
                        }
                    }

                    // Puedes mostrar un mensaje de éxito o realizar otras acciones si es necesario
                    Response.Write("Consulta realizada exitosamente.");

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // Manejar errores aquí (mostrar un mensaje de error, registrar el error, etc.)
                Response.Write("Error al realizar la consulta: " + ex.Message);
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

        private void BindGrid()
        {
            string connectionString = "Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;";
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                // Utiliza un JOIN para obtener datos de DETALLE_PV y cabecera_pv
                OracleCommand cmd = new OracleCommand("SELECT dp.ID_DETALLE_PV, dp.ID_PRODUCTO, dp.CANTIDAD, dp.PRECIO_UNITARIO, dp.ID_CABECERA_PV " +
                                                     "FROM DETALLE_PV dp " +
                                                     "JOIN cabecera_pv cp ON dp.ID_CABECERA_PV = cp.ID_CABECERA_PV", con);
                con.Open();
                OracleDataAdapter sda = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dgDetallesPV.DataSource = dt;
                dgDetallesPV.DataBind();
            }
        }

        protected void dgDetallesPV_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnAgregarPedido_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener valores de TextBox y Label fuera del DataGrid
                string cantidad = txtCantidad.Text;
                string idProducto = lblIDProducto.Text;
                string precioUnitario = lblPrecioUnitario.Text;
                string idCabeceraPV = lblIDCabeceraPV.Text;

                // Insertar en la base de datos (código de inserción depende de tu estructura de tablas)
                string insertQuery = "INSERT INTO DETALLE_PV (ID_DETALLE_PV,ID_PRODUCTO, CANTIDAD, PRECIO_UNITARIO, ID_CABECERA_PV) " +
                                     "VALUES (DETALLE_PV_SEQ.NEXTVAL,:IdProducto, :Cantidad, :PrecioUnitario, :IdCabeceraPV)";

                using (OracleConnection con = new OracleConnection("Data Source=localhost:1521/xe;User Id=maipogrande;Password=123;"))
                {
                    con.Open();

                    using (OracleCommand cmd = new OracleCommand(insertQuery, con))
                    {
                        cmd.Parameters.Add(new OracleParameter("IdProducto", idProducto));
                        cmd.Parameters.Add(new OracleParameter("Cantidad", cantidad));
                        cmd.Parameters.Add(new OracleParameter("PrecioUnitario", precioUnitario));
                        cmd.Parameters.Add(new OracleParameter("IdCabeceraPV", idCabeceraPV));

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

        protected void dgDetallesPV_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "PagarDetalle")
            {
                // Mensaje de depuración
                System.Diagnostics.Debug.WriteLine("Botón Pagar presionado en la fila " + e.Item.ItemIndex);

                // URL de la página externa a la que deseas redireccionar
                string urlExterna = "https://www.sandbox.paypal.com/checkoutnow?token=537040496W707163N";

                // Redireccionar a la página externa
                Response.Redirect(urlExterna);
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
    }

    }