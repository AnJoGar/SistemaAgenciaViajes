using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace Excepciones
{

    // Se define una nueva excepción personalizada que hereda de la clase Exception
    public class AerolineaNoEncontradaException : Exception
    {
	// Constructor por defecto que no realiza ninguna acción
      public AerolineaNoEncontradaException()
        {


	// Constructor que recibe un mensaje personalizado y lo envía a la clase base Exception para su manejo
        // También llama al método privado EscribirErrores
       }
       public AerolineaNoEncontradaException(string message) : base(message)
        {
            EscribirErrores(message);
        }


	// Constructor que recibe un mensaje personalizado y una excepción interna, y los envía a la clase base Exception para su manejo
        // También llama al método privado EscribirErrores
        public AerolineaNoEncontradaException(string message, Exception innerException)
        : base(message, innerException)
        {
            EscribirErrores(message);
        }


	// Método estático que verifica si la tabla de datos DataTable tiene filas
        // Si la tabla no tiene filas, se lanza una excepción de tipo AerolineaNoEncontradaException con un mensaje personalizado
        public static void AerolineaNoEncontrada(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                throw new DestinoNoEncontradoException("No se encontraron Aerolineas con el dato especificado");
            }
        }

	 // Método privado que es llamado por los constructores de la excepción y recibe un mensaje de error
        // Su propósito no está claro, pero devuelve un valor booleano que siempre es verdadero
        private bool EscribirErrores(string error)
        {

            return true;
        }


    }
}
