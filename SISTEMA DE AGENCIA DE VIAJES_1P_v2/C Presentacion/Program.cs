using C_Presentacion.FormulariosProyecto.Home;
using C_Presentacion.Home;
using System;
using System.Windows.Forms;

namespace C_Presentacion
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());
            //Application.Run(new Login());
            FormBienvenida bienvenidaForm = new FormBienvenida();
            Application.Run(bienvenidaForm);


            //Login loginForm = new Login();
            //if (bienvenidaForm.Visible)
            //{
            //    bienvenidaForm.Close();
            //    Application.Run(loginForm);
            //}
        }
    }
}
