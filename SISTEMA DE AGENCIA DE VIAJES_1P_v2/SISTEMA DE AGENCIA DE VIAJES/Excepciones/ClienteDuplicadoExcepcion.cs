using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{

    // Define una excepción para ser lanzada si se encuentra un cliente duplicado.
    public class ClienteDuplicadoExcepcion : Exception
    {

	// Constructor sin argumentos para la excepción.
        public ClienteDuplicadoExcepcion()
        {


        }


	 // Constructor que acepta un mensaje de error para la excepción.
        // También llama al método EscribirErrores con el mensaje de error proporcionado.
        public ClienteDuplicadoExcepcion(string message) : base(message)
        {
            EscribirErrores(message);
        }


	// Constructor que acepta un mensaje de error y una excepción interna para la excepción.
        // También llama al método EscribirErrores con el mensaje de error proporcionado.
        public ClienteDuplicadoExcepcion(string message, Exception innerException) : base(message, innerException)
        {
            EscribirErrores(message);
        }


	// Método estático que lanza la excepción si se encuentra un cliente duplicado en una tabla de datos.
        // Acepta una tabla de datos como argumento y verifica si la tabla tiene al menos una fila.
        // Si no hay filas, lanza la excepción ClienteDuplicadoExcepcion con un mensaje de error.
        public static void ClienteDuplicado(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                throw new DestinoNoEncontradoException("Cliente existente ");
            }
        }



	 // Método privado que actualmente no hace nada.
        // Parece no ser necesario y puede eliminarse.
        private bool EscribirErrores(string error)
        {

            return true;
        }
    }
}
