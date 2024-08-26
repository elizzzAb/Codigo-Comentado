using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DatosLayer
{
    public class CustomerRepository
    {

        // Creamos un método para obtener todos los clientes
        public List<Customers> ObtenerTodos() {

            // Crea una conexión a la base de datos
            using (var conexion= DataBase.GetSqlConnection()) {

                // Construye la consulta SQL para seleccionar todos los campos
                // de la tabla Customers
                String selectFrom = "";
                selectFrom = selectFrom + "SELECT [CustomerID] " + "\n";
                selectFrom = selectFrom + "      ,[CompanyName] " + "\n";
                selectFrom = selectFrom + "      ,[ContactName] " + "\n";
                selectFrom = selectFrom + "      ,[ContactTitle] " + "\n";
                selectFrom = selectFrom + "      ,[Address] " + "\n";
                selectFrom = selectFrom + "      ,[City] " + "\n";
                selectFrom = selectFrom + "      ,[Region] " + "\n";
                selectFrom = selectFrom + "      ,[PostalCode] " + "\n";
                selectFrom = selectFrom + "      ,[Country] " + "\n";
                selectFrom = selectFrom + "      ,[Phone] " + "\n";
                selectFrom = selectFrom + "      ,[Fax] " + "\n";
                selectFrom = selectFrom + "  FROM [dbo].[Customers]";

                // Crea un comando SQL con la consulta y la conexión
                using (SqlCommand comando = new SqlCommand(selectFrom, conexion)) {

                    // Ejecuta la consulta y obtiene un SqlDataReader
                    SqlDataReader reader = comando.ExecuteReader();

                    // Crea una lista para almacenar los clientes
                    List<Customers> Customers = new List<Customers>();

                    // Creamos un bucle 'while', que lee cada fila del resultado
                    while (reader.Read())
                    {
                        // Convierte la fila en un objeto Customers
                        var customers = LeerDelDataReader(reader);

                        // Agrega el cliente a la lista
                        Customers.Add(customers);
                    }

                    // Nos devuelve la lista de clientes
                    return Customers;
                }
            }
           
        }

        // Creamos un método para obtener un cliente por su ID
        public Customers ObtenerPorID(string id) {

            // Crea una conexión a la base de datos
            using (var conexion = DataBase.GetSqlConnection()) {

                // Construye la consulta SQL para seleccionar un cliente por ID
                String selectForID = "";
                selectForID = selectForID + "SELECT [CustomerID] " + "\n";
                selectForID = selectForID + "      ,[CompanyName] " + "\n";
                selectForID = selectForID + "      ,[ContactName] " + "\n";
                selectForID = selectForID + "      ,[ContactTitle] " + "\n";
                selectForID = selectForID + "      ,[Address] " + "\n";
                selectForID = selectForID + "      ,[City] " + "\n";
                selectForID = selectForID + "      ,[Region] " + "\n";
                selectForID = selectForID + "      ,[PostalCode] " + "\n";
                selectForID = selectForID + "      ,[Country] " + "\n";
                selectForID = selectForID + "      ,[Phone] " + "\n";
                selectForID = selectForID + "      ,[Fax] " + "\n";
                selectForID = selectForID + "  FROM [dbo].[Customers] " + "\n";
                selectForID = selectForID + $"  Where CustomerID = @customerId";

                // Crea un comando SQL con la consulta y la conexión
                using (SqlCommand comando = new SqlCommand(selectForID, conexion))
                {
                    // Agrega el parámetro customerId al comando
                    comando.Parameters.AddWithValue("customerId", id);

                    // Ejecuta la consulta y obtiene un SqlDataReader
                    var reader = comando.ExecuteReader();
                    Customers customers = null;
                    //validadmos 

                    // Nos permite que, si hay un resultado, lo convierte en un objeto Customers
                    if (reader.Read()) {
                        customers = LeerDelDataReader(reader);
                    }

                    // Devuelve el cliente (o null si no se encontró)
                    return customers;
                }

            }
        }

        // Creamos un método para convertir un SqlDataReader en un objeto Customers
        public Customers LeerDelDataReader( SqlDataReader reader) {

            Customers customers = new Customers();

            // Asigna los valores del reader a las propiedades del objeto Customers
            // Si el valor es DBNull, se asigna una cadena vacía
            customers.CustomerID = reader["CustomerID"] == DBNull.Value ? " " : (String)reader["CustomerID"];
            customers.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (String)reader["CompanyName"];
            customers.ContactName = reader["ContactName"] == DBNull.Value ? "" : (String)reader["ContactName"];
            customers.ContactTitle = reader["ContactTitle"] == DBNull.Value ? "" : (String)reader["ContactTitle"];
            customers.Address = reader["Address"] == DBNull.Value ? "" : (String)reader["Address"];
            customers.City = reader["City"] == DBNull.Value ? "" : (String)reader["City"];
            customers.Region = reader["Region"] == DBNull.Value ? "" : (String)reader["Region"];
            customers.PostalCode = reader["PostalCode"] == DBNull.Value ? "" : (String)reader["PostalCode"];
            customers.Country = reader["Country"] == DBNull.Value ? "" : (String)reader["Country"];
            customers.Phone = reader["Phone"] == DBNull.Value ? "" : (String)reader["Phone"];
            customers.Fax = reader["Fax"] == DBNull.Value ? "" : (String)reader["Fax"];
            return customers;
        }
        //-------------

        // Creamos un método para insertar un nuevo cliente
        public int InsertarCliente(Customers customer) {

            // Crea una conexión a la base de datos
            using (var conexion = DataBase.GetSqlConnection()) {

                // Construye la consulta SQL para insertar un nuevo cliente
                String insertInto = "";
                insertInto = insertInto + "INSERT INTO [dbo].[Customers] " + "\n";
                insertInto = insertInto + "           ([CustomerID] " + "\n";
                insertInto = insertInto + "           ,[CompanyName] " + "\n";
                insertInto = insertInto + "           ,[ContactName] " + "\n";
                insertInto = insertInto + "           ,[ContactTitle] " + "\n";
                insertInto = insertInto + "           ,[Address] " + "\n";
                insertInto = insertInto + "           ,[City]) " + "\n";
                insertInto = insertInto + "     VALUES " + "\n";
                insertInto = insertInto + "           (@CustomerID " + "\n";
                insertInto = insertInto + "           ,@CompanyName " + "\n";
                insertInto = insertInto + "           ,@ContactName " + "\n";
                insertInto = insertInto + "           ,@ContactTitle " + "\n";
                insertInto = insertInto + "           ,@Address " + "\n";
                insertInto = insertInto + "           ,@City)";

                // Crea un comando SQL con la consulta y la conexión
                using (var comando = new SqlCommand( insertInto,conexion )) {

                    // Agrega los parámetros al comando y ejecuta la inserción
                    int insertados = parametrosCliente(customer, comando);
                    return insertados;
                }

            }
        }
        //-------------

        // Creamos un método para actualizar un cliente existente
        public int ActualizarCliente(Customers customer) {

            // Crea una conexión a la base de datos
            using (var conexion = DataBase.GetSqlConnection()) {

                // Construye la consulta SQL para actualizar un cliente
                String ActualizarCustomerPorID = "";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "UPDATE [dbo].[Customers] " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "   SET [CustomerID] = @CustomerID " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[CompanyName] = @CompanyName " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[ContactName] = @ContactName " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[ContactTitle] = @ContactTitle " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[Address] = @Address " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[City] = @City " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + " WHERE CustomerID= @CustomerID";

                // Crea un comando SQL con la consulta y la conexión
                using (var comando = new SqlCommand(ActualizarCustomerPorID, conexion)) {

                    // Agrega los parámetros al comando y ejecuta la actualización
                    int actualizados = parametrosCliente(customer, comando);

                    return actualizados;
                }
            } 
        }

        // Creamos un método para agregar parámetros al comando SQL
        public int parametrosCliente(Customers customer, SqlCommand comando) {

            // Agrega los parámetros del cliente al comando
            comando.Parameters.AddWithValue("CustomerID", customer.CustomerID);
            comando.Parameters.AddWithValue("CompanyName", customer.CompanyName);
            comando.Parameters.AddWithValue("ContactName", customer.ContactName);
            comando.Parameters.AddWithValue("ContactTitle", customer.ContactName);
            comando.Parameters.AddWithValue("Address", customer.Address);
            comando.Parameters.AddWithValue("City", customer.City);

            // Ejecuta el comando y devuelve el número de filas afectadas
            var insertados = comando.ExecuteNonQuery();
            return insertados;
        }

        // Creamos un método para eliminar un cliente por su ID
        public int EliminarCliente(string id) {

            // Crea una conexión a la base de datos
            using (var conexion = DataBase.GetSqlConnection() ){

                // Construye la consulta SQL para eliminar un cliente
                String EliminarCliente = "";
                EliminarCliente = EliminarCliente + "DELETE FROM [dbo].[Customers] " + "\n";
                EliminarCliente = EliminarCliente + "      WHERE CustomerID = @CustomerID";

                // Crea un comando SQL con la consulta y la conexión
                using (SqlCommand comando = new SqlCommand(EliminarCliente, conexion)) {

                    // Agrega el parámetro CustomerID al comando
                    comando.Parameters.AddWithValue("@CustomerID", id);

                    // Ejecuta el comando y devuelve el número de filas eliminadas
                    int elimindos = comando.ExecuteNonQuery();
                    return elimindos;
                }
            }
        }
    }
}
