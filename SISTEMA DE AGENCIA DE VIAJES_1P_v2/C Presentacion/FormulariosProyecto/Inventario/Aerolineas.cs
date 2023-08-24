using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace C_Presentacion.FormulariosProyecto.Inventario
{
    public partial class Aerolineas : Form
    {
        // Se crea un nuevo objeto E_Aerolinea y uno de N_Aerolinea
        E_Aerolinea objent = new E_Aerolinea();

        // Creación de una nueva instancia de N_Aerolinea
        N_Aerolinea objneg = new N_Aerolinea();

        //Creación de una nueva instancia de Validaciones
        Validaciones Validacion = new Validaciones();
        public Aerolineas()
        {
            InitializeComponent();
            cbBuscar.Items.Add("Nombre");
            cbBuscar.Items.Add("Siglas");
        }
        //Método para mostrar un mensaje de error
        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        // Método para registrar aerolíneas
        void RegistrarAreolinea(String accion)
        {
            // Comprobación de los campos de nombre y siglas que no están vacíos
            if (!Validacion.NoVacio(txtNombreAerolinea.Text, "Nombre de Aerolínea", ShowErrorMessage) || !Validacion.SoloLetras(txtNombreAerolinea.Text, "Nombre de Aerolínea", ShowErrorMessage) ||
                !Validacion.NoVacio(txtSiglasAerolinea.Text, "Sigla de aerolínea", ShowErrorMessage) || !Validacion.SoloLetras(txtSiglasAerolinea.Text, "Sigla de aerolínea", ShowErrorMessage)||
               // !Validacion.NoVacio(txtCapacidad.Text, "Matricula de Avión", ShowErrorMessage) ||
                 !Validacion.NoVacio(txtBoletos.Text, "Cantidad de Boletos", ShowErrorMessage)|| !Validacion.EsDecimal1(txtBoletos.Text, ShowErrorMessage)
                )
            {
                return;
            }
            objent.codigoAerolinea = txtCodigoAerolinea.Text;
            objent.nombreAerolinea = txtNombreAerolinea.Text;
            objent.siglasAerolinea = txtSiglasAerolinea.Text;
      objent.capacidadAerolinea = txtCapacidad.Text;
      objent.num_Boletos = Convert.ToInt32(txtBoletos.Text);
      // objent.capacidadAerolinea = Convert.ToInt32(txtCapacidad.Text);
      // Llamar al método N_Registrar_Aerolinea de la clase N_Aerolinea y asignar el resultado a la variable Registrar_Aerolinea
      objent.accion = accion;
            String Registrar_Aerolinea = objneg.N_Registrar_Aerolinea(objent);
            MessageBox.Show(Registrar_Aerolinea, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    void ActualizarAreolinea(String accion)
    {
      // Comprobación de los campos de nombre y siglas que no están vacíos
      if (!Validacion.NotEmpty(txtNombreAerolinea.Text, error => MessageBox.Show(error)))
      {
        return;
      }
      if (!Validacion.NoVacio(txtCapacidad.Text, "Capacidad de Aerolinea", ShowErrorMessage))

      {
        return;
      }
      if (!Validacion.NoVacio(txtNombreAerolinea.Text, "Nombre de Aerolínea", ShowErrorMessage) || !Validacion.SoloLetras(txtNombreAerolinea.Text, "Nombre de Aerolínea", ShowErrorMessage) ||
          !Validacion.NoVacio(txtSiglasAerolinea.Text, "Sigla de aerolínea", ShowErrorMessage) || !Validacion.SoloLetras(txtSiglasAerolinea.Text, "Sigla de aerolínea", ShowErrorMessage)||
          !Validacion.NoVacio(txtBoletos.Text, "Cantidad de Boletos", ShowErrorMessage) || !Validacion.EsDecimal1(txtBoletos.Text, ShowErrorMessage)

          )
      {
        return;
      }

      objent.codigoAerolinea = txtCodigoAerolinea.Text;
      objent.nombreAerolinea = txtNombreAerolinea.Text;
      objent.siglasAerolinea = txtSiglasAerolinea.Text;
      objent.capacidadAerolinea =  (txtCapacidad.Text);
      objent.num_Boletos = Convert.ToInt32(txtBoletos.Text);
      //objent.capacidadAerolinea = Convert.ToInt32(txtCapacidad.Text);
      // Llamar al método N_Registrar_Aerolinea de la clase N_Aerolinea y asignar el resultado a la variable Registrar_Aerolinea
      objent.accion = accion;
            String Registrar_Aerolinea = objneg.N_Registrar_Aerolinea(objent);
            MessageBox.Show(Registrar_Aerolinea, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void MostrarAerolinea()
        {
            // Se crea un nuevo DataTable y luego lo llena con los datos de las aerolineas
            DataTable dt = new DataTable();

            dt = objneg.N_Listar_Aerolinea();
            dgvAerolineas.DataSource = dt;

        }


        //Método para buscar aerolínea por el campo nombre.
        void BuscarAerolinea()
        {
            if (cbBuscar.SelectedItem != null)
            {
                if (txtBuscarAeroli.Text != "")
                {
                    objent.nombreAerolinea = txtBuscarAeroli.Text;
                    objent.siglasAerolinea = txtBuscarAeroli.Text;
                    objent.valorBusqueda = cbBuscar.SelectedItem.ToString();
                    // Se crea un nuevo DataTable y luego lo llena con los datos de las aerolineas
                    DataTable dt = new DataTable();
                    //txtBuscarAeroli
                    dt = objneg.N_Buscar_Aerolineas(objent);
                    dgvAerolineas.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Error, campo de búsqueda vacío");
                }
            }
            else
            {
        MessageBox.Show("Error, debe seleccionarse un parámetro de búsqueda");
      }
                
            }



        

        //Método para agregar la aerolínea a la base de datos
        void agregarAerolinea()
        {

            // Se Muestra un mensaje de confirmación para registrar la aerolínea
            if (MessageBox.Show("¿Deseas registrar a " + txtNombreAerolinea.Text + "?", "Mensaje",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
            {
                // Se llama al método RegistrarAreolinea con el valor "1" cuando se haga clic en el botón "Agregar aerolínea"
                RegistrarAreolinea("1");
                limpiar();
                MostrarAerolinea();
            }


        }

        //Método para cargar los datos en la tabla
        void CargarAerolineas()
        {
            // Obtiene la fila seleccionada en el DataGridView
            int fila = dgvAerolineas.CurrentCell.RowIndex;
            // Asigna los valores de la fila seleccionada a los TextBox
            txtCodigoAerolinea.Text = dgvAerolineas[0, fila].Value.ToString();
            txtNombreAerolinea.Text = dgvAerolineas[1, fila].Value.ToString();
            txtSiglasAerolinea.Text = dgvAerolineas[2, fila].Value.ToString();
            txtCapacidad.Text = dgvAerolineas[3, fila].Value.ToString();
      txtBoletos.Text = dgvAerolineas[4, fila].Value.ToString();
    }

        //Método para limpiar las cajas de texto al registrar las aerolíneas
        private void limpiar()
        {
            txtCodigoAerolinea.Clear();
            txtNombreAerolinea.Clear();
            txtSiglasAerolinea.Clear();
            txtCapacidad.Clear();
      txtBoletos.Clear();
     
        }


        private void RegistrarAerolineas_Load(object sender, EventArgs e)
        {
            //Se Asigna como fuente de datos para el DataGridView el resultado del método N_Listar_Aerolinea de la clase N_Aerolinea
            dgvAerolineas.DataSource = objneg.N_Listar_Aerolinea();
      cbBuscar.SelectedItem = "Nombre";

    }

        // Se llama al método RegistrarAreolinea con el valor "1" cuando se haga clic en el botón "Agregar aerolínea"
        private void btnAgregarAerolinea_Click(object sender, EventArgs e)
        {
            agregarAerolinea();
        }



        private void btnMostrarAeolinea_Click(object sender, EventArgs e)
        {
            //Se llama al método de buscar por un filtro opcional	
            BuscarAerolinea();
        }

        private void btnActualizarAerolinea_Click(object sender, EventArgs e)
        {
            // Llama al método ingresar con la acción "2" para actualizar los datos de la aerolinea
            ActualizarAreolinea("2");
            limpiar();
            MostrarAerolinea();
        }

        private void seleccionarFilaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Llama al método CargarAerolineas al hacer clic en la opción actualizar del menú contextual
            //CargarAerolineas();
        }



        private void dgvAerolineas_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Llama al método CargarAerolineas al hacer clic en la opción actualizar
            CargarAerolineas();
        }

        private void dgvAerolineas_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            //Se define una variable de ToolTip
            ToolTip toolTip = new ToolTip();
            //Se establece que si el puntero está en una determinada celda, entonces se le presente al usuario un mensaje 
            //que le indique que debe dar doble click para pasar los datos del datagridview hacia los textbox
            if (e.RowIndex > -1)
            {
                toolTip.SetToolTip(this.dgvAerolineas, "Presione doble click para llenar el formulario de edición");
                //Se establece un tiempo de delay tras aparecer el mensaje, esto con la finalidad de evitar multiples mensajes en la pantalla
                toolTip.AutoPopDelay = 1000;
                //toolTip.ReshowDelay = 500;
            }
        }

        private void dgvAerolineas_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            //Se establece que si el puntero sale de una determinada celda, entonces se cambie el color de fondo y del control en sí.
            if (e.RowIndex > -1)
            {
                dgvAerolineas.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                dgvAerolineas.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void dgvAerolineas_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Se establece que si el puntero se encuentra en una determinada celda, entonces se cambie el color de fondo y del control en sí.
            if (e.RowIndex > -1)
            {
                dgvAerolineas.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Orange;
                dgvAerolineas.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtBuscarAeroli_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {


            switch (cbBuscar.SelectedItem.ToString())
            {
                case "Nombre":
                    txtBuscarAeroli.Text = "";
                    break;
                case "Siglas":
                    txtBuscarAeroli.Text = "";
                    break;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Metodo  que carga los registros existentes en la tabla 
            MostrarAerolinea();
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

    private void dgvAerolineas_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
  }

}
