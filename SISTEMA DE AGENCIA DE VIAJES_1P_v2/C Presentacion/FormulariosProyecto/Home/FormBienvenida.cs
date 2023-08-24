using C_Presentacion.Home;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_Presentacion.FormulariosProyecto.Home
{
    public partial class FormBienvenida : Form
    {
        private Timer timer;
        private Login login;
        private int objetivo = 100; // Valor objetivo para la barra de progreso

        public FormBienvenida()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 30; // 30 milisegundos, se actualizará frecuentemente para simular el progreso gradual
            timer.Tick += Timer_Tick;
        }

        private void FormBienvenida_Shown(object sender, EventArgs e)
        {
            guna2ProgressBar1.Value = 0; // Establecer el valor inicial de la barra de progreso
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Incrementar el valor actual de la barra de progreso en cada Tick del Timer
            guna2ProgressBar1.Increment(1);

            // Verificar si se alcanzó el valor objetivo
            if (guna2ProgressBar1.Value >= objetivo)
            {
                timer.Stop();
                this.Hide();
                login = new Login();
                login.Show();
                return;
            }
        }
    }
}
