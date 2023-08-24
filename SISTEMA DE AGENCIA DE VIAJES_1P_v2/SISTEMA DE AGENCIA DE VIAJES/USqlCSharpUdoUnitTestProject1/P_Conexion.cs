using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Interfaces.Streaming;
using Microsoft.Analytics.Types.Sql;
using Microsoft.Analytics.UnitTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Capa_Datos;
using System.Data;

namespace USqlCSharpUdoUnitTestProject1
{
    [TestClass]
    public class P_Conexion
    {

        Conexion_Desconexion_bd conexion = new Conexion_Desconexion_bd();

        [TestMethod]
        public void TestAbrirConexion()
        {
            // Arrange
            var esperado = ConnectionState.Open;

            // Act
            var resultado = conexion.abrir_conexion().State;

            // Assert
            Assert.AreEqual(esperado, resultado);


            /// <summary>
            ///Gets or sets the test context which provides
            ///information about and functionality for the current test run.
            ///</summary>
        }

            #region Additional test attributes
            //
            // You can use the following additional attributes as you write your tests:
            //
            // Use ClassInitialize to run code before running the first test in the class
            // [ClassInitialize()]
            // public static void MyClassInitialize(TestContext testContext) { }
            //
            // Use ClassCleanup to run code after all tests in a class have run
            // [ClassCleanup()]
            // public static void MyClassCleanup() { }
            //
            // Use TestInitialize to run code before running each test 
            // [TestInitialize()]
            // public void MyTestInitialize() { }
            //
            // Use TestCleanup to run code after each test has run
            // [TestCleanup()]
            // public void MyTestCleanup() { }
            //
            #endregion

            [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var esperado = ConnectionState.Closed;

            // Act
            conexion.abrir_conexion();
            var resultado = conexion.cerrar_conexion().State;

            // Assert
            Assert.AreEqual(esperado, resultado);

            
        }
    }
}
