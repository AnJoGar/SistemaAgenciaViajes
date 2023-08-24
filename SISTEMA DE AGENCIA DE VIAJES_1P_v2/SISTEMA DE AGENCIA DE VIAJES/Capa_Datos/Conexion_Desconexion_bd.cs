using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{


    //En la presente clase crearemos una sentencia que se conecte con la base de datos definida en App.Config y se crearan métodos para conectar y desconectar de la base de datos.
    public class Conexion_Desconexion_bd
    {


        public SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["sql"].ConnectionString);

        public Conexion_Desconexion_bd()
        {
           
        }
        //Método par abrir la conexión hacia la base de datos 

        public SqlConnection abrir_conexion()
        {
	    //Comprobar si la conexión está cerrada
            if (conexion.State == ConnectionState.Closed)
            {
		//Abrir conexión
                conexion.Open();
            }
            return conexion;
        }

        //Cerrar conexión hacia la base de datos
        public SqlConnection cerrar_conexion()
        {
	    //Comprobar si la conexión está abierta
            if (conexion.State == ConnectionState.Open)
            {
		//Cerrar Conexión
                conexion.Close();
            }
            return conexion;
        }
    }
}
