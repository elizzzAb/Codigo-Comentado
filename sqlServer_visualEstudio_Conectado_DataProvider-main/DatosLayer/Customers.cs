using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosLayer
{
    // Declara una clase pública llamada 'Customers'
    public class Customers
    {
        // Propiedad pública para el ID del cliente
        public string CustomerID { get; set; }

        // Propiedad pública para el nombre de la compañía
        public string CompanyName { get; set; }

        // Propiedad pública para el nombre del contacto
        public string ContactName { get; set; }

        // Propiedad pública para el título del contacto
        public string ContactTitle { get; set; }

        // Propiedad pública para la dirección
        public string Address { get; set; }

        // Propiedad pública para la ciudad
        public string City { get; set; }

        // Propiedad pública para la región
        public string Region { get; set; }

        // Propiedad pública para el código postal
        public string PostalCode { get; set; }

        // Propiedad pública para el país
        public string Country { get; set; }

        // Propiedad pública para el teléfono
        public string Phone { get; set; }

        // Propiedad pública para el fax
        public string Fax { get; set; }
    }
}
