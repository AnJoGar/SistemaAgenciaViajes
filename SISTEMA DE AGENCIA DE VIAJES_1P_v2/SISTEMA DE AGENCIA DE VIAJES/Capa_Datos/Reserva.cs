using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Capa_Entidad;
using System.Data.SqlClient;
using Excepciones;
using System.Configuration;

namespace Capa_Datos
{
    internal class Reserva
    {

        Conexion_Desconexion_bd c = new Conexion_Desconexion_bd();

        public DataTable D_Listar_Aerolineas()
        {
            // Se crea un nuevo objeto SqlCommand para ejecutar un procedimiento almacenado
            SqlCommand cmd = new SqlCommand("sp_listar_Aerolineas", c.abrir_conexion());
            // Se crea un nuevo objeto SqlDataAdapter para ejecutar el comando y llenar un DataTable con los resultados
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable D_Listar_Destinos_Turisticos()
        {
            SqlCommand cmd = new SqlCommand("sp_listar_destino_turistico", c.abrir_conexion());

            // Se crea un nuevo objeto SqlDataAdapter para ejecutar el comando y llenar un DataTable con los resultados
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            // Devuelve el DataTable con los datos
            return dt;
        }

    }
}
