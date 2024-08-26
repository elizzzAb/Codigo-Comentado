using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConexionEjemplo
{
    // Declara una clase estática interna llamada Program
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>

        // Atributo que indica que este método debe ejecutarse en un
        // único hilo de apartamentos (Single-Threaded Apartment)
        [STAThread]

        // Método principal estático, punto de entrada de la aplicación
        static void Main()
        {
            // Habilita los estilos visuales para la aplicación
            Application.EnableVisualStyles();

            // Establece el valor predeterminado de renderizado de texto compatible
            Application.SetCompatibleTextRenderingDefault(false);

            // Inicia la ejecución de la aplicación, mostrando Form1 como formulario principal
            Application.Run(new Form1());
        }
    }
}
