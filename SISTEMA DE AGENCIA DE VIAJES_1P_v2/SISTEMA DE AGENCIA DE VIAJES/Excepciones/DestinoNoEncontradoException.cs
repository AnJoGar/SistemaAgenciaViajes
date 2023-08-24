using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Excepciones
{
    
     // Clase de excepción personalizada para el manejo de errores cuando no se encuentra un origen turístico
    public class DestinoNoEncontradoException : Exception
    {
        
	// Constructor vacío por defecto
	public DestinoNoEncontradoException()
        {


        }

	// Constructor que recibe un mensaje personalizado y lo envía a la clase base Exception para su manejo
        // También llama al método privado EscribirErrores
        public DestinoNoEncontradoException(string message) : base(message)
        {
            EscribirErrores(message);
        }

	
	// Constructor que recibe un mensaje personalizado y una excepción interna, y los envía a la clase base Exception para su manejo
        // También llama al método privado EscribirErrores
        public DestinoNoEncontradoException(string message, Exception innerException)
        : base(message, innerException)
        {
            EscribirErrores(message);
        }


	// Método estático que verifica si un DataTable está vacío y, si es así, lanza una excepción "OrigenNoEncontradoException" con un mensaje de error.
        public static void OrigenNoEncontrado(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                throw new DestinoNoEncontradoException("No se encontraron destinos turísticos con el dato especificado");
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
