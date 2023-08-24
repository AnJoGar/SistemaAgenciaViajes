using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{

    // Clase de excepción personalizada para el manejo de errores cuando no se encuentra un cliente
    public class ClienteNoEncontradoExcepcion : Exception
    {

        // Constructor vacío por defecto
        public ClienteNoEncontradoExcepcion()
        {


        }


        // Constructor que recibe un mensaje de error y lo envía al método "EscribirErrores"
        public ClienteNoEncontradoExcepcion(string message) : base(message)
        {
            EscribirErrores(message);
        }


        // Constructor que recibe un mensaje de error y una excepción interna, y los envía al método "EscribirErrores"
        public ClienteNoEncontradoExcepcion(string message, Exception innerException)
        : base(message, innerException)
        {
            EscribirErrores(message);
        }


        // Método estático que verifica si una lista de objetos está vacía y, si es así, lanza una excepción "ClienteNoEncontradoExcepcion" con un mensaje de error.
        public static void ClienteNoEncontrado(DataTable cliente)
        {
            if (cliente.Rows.Count == 0)
            {
                throw new ClienteNoEncontradoExcepcion("No se encontraron clientes con el apellido especificado");
            }
        }


        // Método privado que se encarga de escribir los errores en algún lugar (por ejemplo, en un archivo de log). 
        // En este caso, siempre devuelve verdadero.
        private bool EscribirErrores(string error)
        {

            return true;
        }
    }
}
