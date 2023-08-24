using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Interfaces.Streaming;
using Microsoft.Analytics.Types.Sql;
using Microsoft.Analytics.UnitTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

using System.IO;
using Capa_Entidad;
using Capa_Negocio;
using Capa_Datos;
using System.Data;

namespace USqlCSharpUdoUnitTestProject1
{
    [TestClass]
    public class P_DestinoTuristico
    {
        [TestMethod]
        public void TestRegistroDestinosTuristicos()
        {

            // Crea un nuevo objeto de la clase E_Destino_Turisticos con los datos necesarios para ingresar un nuevo destino turístico
            // Arrange: Crear un objeto E_Destino_Turisticos con los datos del destino turístico a insertar
            E_Destino_Turisticos destinoTuristico = new E_Destino_Turisticos();
            destinoTuristico.codigo = "";
            destinoTuristico.origen = "Colombia";
            destinoTuristico.destino = "Ecuador";
            destinoTuristico.precio = 900;
            destinoTuristico.accion = "1";

            // Act: Ejecutar el método D_RegistroDestinosTuristicos() de la clase D_Destinos_Turisticos
            D_Destinos_Turisticos d_destinosTuristicos = new D_Destinos_Turisticos();
            String resultado = d_destinosTuristicos.D_RegistroDestinosTuristicos(destinoTuristico);

            // Assert: Verificar que el resultado comienza con "Se generó el código: "
            Assert.IsTrue(resultado.StartsWith("Se generó el código: "));



        }   }
}
