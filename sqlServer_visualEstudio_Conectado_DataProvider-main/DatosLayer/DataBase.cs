using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

// Define un espacio de nombres llamado DatosLayer
namespace DatosLayer
{
    // Declara una clase pública llamada DataBase
    public class DataBase
    {
        // Propiedad estática pública que devuelve la cadena de conexión
        public static string ConnectionString {
            get
            {
                // Obtiene la cadena de conexión del archivo de configuración
                string CadenaConexion = ConfigurationManager
                    .ConnectionStrings["NWConnection"]
                    .ConnectionString;

                // Crea un objeto SqlConnectionStringBuilder con la cadena de conexión obtenida
                SqlConnectionStringBuilder conexionBuilder = 
                    new SqlConnectionStringBuilder(CadenaConexion);

                // Establece el nombre de la aplicación en la cadena de conexión
                // Decimos que si 'ApplicationName' es null, mantiene el valor existente
                conexionBuilder.ApplicationName = 
                    ApplicationName ?? conexionBuilder.ApplicationName;

                // Establece el tiempo de espera de la conexión
                // Declaramos que si 'ConnectionTimeout' es mayor que 0, lo usa; de lo contrario,
                // mantiene el valor existente
                conexionBuilder.ConnectTimeout = ( ConnectionTimeout > 0 ) 
                    ? ConnectionTimeout : conexionBuilder.ConnectTimeout;

                // Devuelve la cadena de conexión modificada como una cadena
                return conexionBuilder.ToString();
            }


        }

        // Creamos una propiedad estática pública para establecer
        // y obtener el tiempo de espera de la conexión
        public static int ConnectionTimeout { get; set; }

        // Propiedad estática pública para establecer
        // y obtener el nombre de la aplicación
        public static string ApplicationName { get; set; }

        // Se crea un método estático público que devuelve una conexión SQL abierta
        public static SqlConnection GetSqlConnection()
        {
            // Crea una nueva conexión SQL utilizando la cadena de conexión
            SqlConnection conexion = new SqlConnection(ConnectionString);

            // Abre la conexión
            conexion.Open();

            // Devuelve la conexión abierta
            return conexion;
            
        } 
    }
}
