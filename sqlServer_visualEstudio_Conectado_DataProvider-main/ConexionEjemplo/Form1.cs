using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DatosLayer;
using System.Net;
using System.Reflection;


// Define el espacio de nombres para organizar el código
namespace ConexionEjemplo
{
    // Declara una clase parcial llamada Form1 que hereda de Form
    public partial class Form1 : Form
    {
        // Instancia del repositorio de clientes
        CustomerRepository customerRepository = new CustomerRepository();

        // Constructor del formulario
        public Form1()
        {
            InitializeComponent();
        }

        // Manejador del evento Click del botón "Cargar"
        private void btnCargar_Click(object sender, EventArgs e)
        {
            // Obtiene todos los clientes y los asigna como fuente de datos del DataGrid
            var Customers = customerRepository.ObtenerTodos();
            dataGrid.DataSource = Customers;
        }

        // Manejador del evento TextChanged del TextBox
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            // Código de filtrado comentado:
            // var filtro = Customers.FindAll( X => X.CompanyName.StartsWith(tbFiltro.Text));
            // dataGrid.DataSource = filtro;
        }

        // Manejador del evento Load del formulario
        private void Form1_Load(object sender, EventArgs e)
        {
            // Configuración de la base de datos comentada:

            /*  DatosLayer.DataBase.ApplicationName = "Programacion 2 ejemplo";
             DatosLayer.DataBase.ConnectionTimeout = 30;

             string cadenaConexion = DatosLayer.DataBase.ConnectionString;
               var conxion = DatosLayer.DataBase.GetSqlConnection();
            */
        }

        // Manejador del evento Click del botón "Buscar"
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Busca un cliente por ID y llena los campos del formulario con sus datos
            var cliente = customerRepository.ObtenerPorID(txtBuscar.Text);
            // Asigna el valor de la propiedad CustomerID del
            // cliente encontrado al cuadro de texto tboxCustomerID
            tboxCustomerID.Text = cliente.CustomerID;

            // Asigna el valor de la propiedad CompanyName del
            // cliente encontrado al cuadro de texto tboxCompanyName
            tboxCompanyName.Text = cliente.CompanyName;

            // Asigna el valor de la propiedad ContactName del
            // cliente encontrado al cuadro de texto tboxContacName
            tboxContacName.Text = cliente.ContactName;

            // Asigna el valor de la propiedad ContactTitle del
            // cliente encontrado al cuadro de texto tboxContactTitle
            tboxContactTitle.Text= cliente.ContactTitle;

            // Asigna el valor de la propiedad Address del
            // cliente encontrado al cuadro de texto tboxAddress
            tboxAddress.Text = cliente.Address;

            // Asigna el valor de la propiedad City del
            // cliente encontrado al cuadro de texto tboxCity
            tboxCity.Text = cliente.City;


        }

        // Manejador de evento Click vacío
        private void label4_Click(object sender, EventArgs e)
        {

        }

        // Manejador del evento Click del botón "Insertar"
        private void btnInsertar_Click(object sender, EventArgs e)
        {
            // Declara una variable para almacenar el resultado
            var resultado = 0;

            // Obtiene un nuevo cliente a partir de los datos del formulario
            var nuevoCliente = ObtenerNuevoCliente();


            // hayNull= validarCampoNull(nuevoCliente) ? true : false ;

            /*  if (tboxCustomerID.Text != "" || 
                  tboxCompanyName.Text !="" ||
                  tboxContacName.Text != "" ||
                  tboxContacName.Text != "" ||
                  tboxAddress.Text != ""    ||
                  tboxCity.Text != "")
              {
                  resultado = customerRepository.InsertarCliente(nuevoCliente);
                  MessageBox.Show("Guardado" + "Filas modificadas = " + resultado);
              }
              else {
                  MessageBox.Show("Debe completar los campos por favor");
              }

              */

            /*
            if (nuevoCliente.CustomerID == "") {
                MessageBox.Show("El Id en el usuario debe de completarse");
               return;    
            }

            if (nuevoCliente.ContactName == "")
            {
                MessageBox.Show("El nombre de usuario debe de completarse");
                return;
            }
            
            if (nuevoCliente.ContactTitle == "")
            {
                MessageBox.Show("El contacto de usuario debe de completarse");
                return;
            }
            if (nuevoCliente.Address == "")
            {
                MessageBox.Show("la direccion de usuario debe de completarse");
                return;
            }
            if (nuevoCliente.City == "")
            {
                MessageBox.Show("La ciudad de usuario debe de completarse");
                return;
            }

            */


            // Validación de campos y inserción del cliente
            // Si no hay campos nulos, inserta el cliente
            if (validarCampoNull(nuevoCliente) == false)
            {
                // Inserta el nuevo cliente y almacena el resultado
                resultado = customerRepository.InsertarCliente(nuevoCliente);

                // Muestra un mensaje con el número de filas modificadas
                MessageBox.Show("Guardado" + "Filas modificadas = " + resultado);
            }
            else {
                // Muestra un mensaje si hay campos vacíos
                MessageBox.Show("Debe completar los campos por favor");
            }
        }

        // Método para validar si algún campo del objeto es nulo o vacío
        // si encautnra un null enviara true de lo caontrario false
        public Boolean validarCampoNull(Object objeto) {

            // Itera sobre todas las propiedades del objeto
            foreach (PropertyInfo property in objeto.GetType().GetProperties()) {

                // Obtiene el valor de la propiedad
                object value = property.GetValue(objeto, null);

                // Si el valor es una cadena vacía, retorna true
                if ((string)value == "") {
                    return true;
                }
            }
            // Si no se encontraron campos vacíos, retorna false
            return false;
        }

        // Manejador de evento Click vacío
        private void label5_Click(object sender, EventArgs e)
        {
            // Este método está vacío
        }

        // Manejador del evento Click del botón "Modificar"
        private void btModificar_Click(object sender, EventArgs e)
        {
            // Obtiene un cliente con los datos actuales del formulario
            var actualizarCliente = ObtenerNuevoCliente();

            // Actualiza el cliente y almacena el número de filas afectadas
            int actualizadas = customerRepository.ActualizarCliente(actualizarCliente);

            // Muestra un mensaje con el número de filas actualizadas
            MessageBox.Show($"Filas actualizadas = {actualizadas}");
        }

        // Método para obtener un nuevo objeto Cliente con los datos del formulario
        private Customers ObtenerNuevoCliente() {

            // Crea y retorna un nuevo objeto Customers con los datos de los campos del formulario
            var nuevoCliente = new Customers
            {
                CustomerID = tboxCustomerID.Text,
                CompanyName = tboxCompanyName.Text,
                ContactName = tboxContacName.Text,
                ContactTitle = tboxContactTitle.Text,
                Address = tboxAddress.Text,
                City = tboxCity.Text
            };

            return nuevoCliente;
        }

        // Manejador del evento Click del botón "Eliminar"
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Elimina el cliente con el ID especificado y almacena el número de filas eliminadas
            int elimindas = customerRepository.EliminarCliente(tboxCustomerID.Text);

            // Muestra un mensaje con el número de filas eliminadas
            MessageBox.Show("Filas eliminadas = " + elimindas);
        }
    }
}
