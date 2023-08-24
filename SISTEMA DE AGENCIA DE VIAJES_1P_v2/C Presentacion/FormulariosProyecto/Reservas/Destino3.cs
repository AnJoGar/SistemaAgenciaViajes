using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using Capa_Datos;

namespace C_Presentacion.FormulariosProyecto.Reservas
{
  public partial class Destino3 : Form
  {
    public Destino3()
    {
      InitializeComponent();
    }

    Reserva objent = new Reserva();
    private string destinoSeleccionado = "";
    private void label6_Click(object sender, EventArgs e)
    {

    }

    private void label4_Click(object sender, EventArgs e)
    {

    }

    private void btnMostrarAeolinea_Click(object sender, EventArgs e)
    {

      this.Close();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void button3_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void pictureBox8_Click(object sender, EventArgs e)
    {

    }
  }
}
