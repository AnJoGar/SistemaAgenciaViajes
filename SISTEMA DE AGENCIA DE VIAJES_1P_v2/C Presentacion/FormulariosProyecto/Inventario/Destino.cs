using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace C_Presentacion.FormulariosProyecto.Inventario
{
    public partial class Destino : Form

    {


        // Creación de una nueva instancia de E_Destino_Turisticos
        E_Destino_Turisticos objent = new E_Destino_Turisticos();

        // Creación de una nueva instancia de N_Destinos_Turisticos
        N_Destinos_Turisticos objneg = new N_Destinos_Turisticos();

        // Creación de una nueva instancia de Validaciones
        Validaciones Validacion = new Validaciones();
        public Destino()
        {
            InitializeComponent();
            cbBuscar.Items.Add("Destino");
            cbBuscar.Items.Add("Origen");

        }
        public void ShowErrorMessage(string message)
        {
            //Muestra un mensaje de error
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        // Método para ingresar un nuevo destino turístico
        void ingresar(String accion)
        {

            // Comprobación si el precio no está vacío y es un decimal válido
            if (!Validacion.NoVacio(txtPrecio.Text, "Precio", ShowErrorMessage) || !Validacion.EsDecimal(txtPrecio.Text, ShowErrorMessage))
            {
                return;
            }
      if (!Validacion.NoVacio(txtNum_viajes.Text, "Numero_Viajeros", ShowErrorMessage) || !Validacion.EsDecimal11(txtNum_viajes.Text, ShowErrorMessage))
      {
        return;
      }

      // Comprobación de los campos de origen y destino que no están vacíos y contienen solo letras
      if (!Validacion.NoVacio(txtOrigen.Text, "Origen", ShowErrorMessage) || !Validacion.SoloLetras(txtOrigen.Text, "Origen", ShowErrorMessage)) //||
                //!Validacion.NoVacio(txtDestino.Text, "Destino", ShowErrorMessage) || !Validacion.SoloLetras(txtDestino.Text, "Destino", ShowErrorMessage))
            {
                return;
            }

      if (!Validacion.NoVacio(txtHotel.Text, "Hotel", ShowErrorMessage)) //||
                                                                                                                                                 //!Validacion.NoVacio(txtDestino.Text, "Destino", ShowErrorMessage) || !Validacion.SoloLetras(txtDestino.Text, "Destino", ShowErrorMessage))
      {
        return;
      }

      if (!Validacion.NoVacio(txtDestino.Text, "Destino", ShowErrorMessage) ) //||
                                                                                                                                             //!Validacion.NoVacio(txtDestino.Text, "Destino", ShowErrorMessage) || !Validacion.SoloLetras(txtDestino.Text, "Destino", ShowErrorMessage))
      {
        return;
      }
      objent.codigo = txtCodigoDestino.Text;
            objent.origen = txtOrigen.Text;
            objent.destino = txtDestino.Text;
      objent.hotel = txtHotel.Text;
      objent.numero_viajeros = Convert.ToInt32(txtNum_viajes.Text);
      objent.precio = Convert.ToDecimal(txtPrecio.Text);
            objent.accion = accion;
            String Registar_Destinos_Turisticos = objneg.N_Registar_Destinos_Turisticos(objent);
            MessageBox.Show(Registar_Destinos_Turisticos, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Método para actualizar un nuevo destino turístico
        void actualizar(String accion)
        {

            // Comprobación si el precio no está vacío y es un decimal válido

            if (!Validacion.NotEmpty(txtPrecio.Text, error => MessageBox.Show(error)))
            {
                return;
            }
            if (!Validacion.NoVacio(txtOrigen.Text, "Origen", ShowErrorMessage) || !Validacion.SoloLetras(txtOrigen.Text, "Origen", ShowErrorMessage)) // ||
               //!Validacion.NoVacio(txtDestino.Text, "Destino", ShowErrorMessage) || !Validacion.SoloLetras(txtDestino.Text, "Destino", ShowErrorMessage))
            {
                return;
            }
      if (!Validacion.NoVacio(txtHotel.Text, "Hotel", ShowErrorMessage) ) //||
                                                                                                                                             //!Validacion.NoVacio(txtDestino.Text, "Destino", ShowErrorMessage) || !Validacion.SoloLetras(txtDestino.Text, "Destino", ShowErrorMessage))
      {
        return;
      }
      if (!Validacion.NoVacio(txtNum_viajes.Text, "Numero_Viajeros", ShowErrorMessage) || !Validacion.EsDecimal11(txtNum_viajes.Text, ShowErrorMessage))
      {
        return;
      }
      objent.codigo = txtCodigoDestino.Text;
            objent.origen = txtOrigen.Text;
            objent.destino = txtDestino.Text;
      objent.hotel = txtHotel.Text;
      objent.numero_viajeros = Convert.ToInt32(txtNum_viajes.Text);
      objent.precio = Convert.ToDecimal(txtPrecio.Text);
            objent.accion = accion;
            String Registar_Destinos_Turisticos = objneg.N_Registar_Destinos_Turisticos(objent);
            MessageBox.Show(Registar_Destinos_Turisticos, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }
        void MostrarDestinoT()
        {
            // Se crea un nuevo DataTable y luego lo llena con los datos de los destinos turísticos
            DataTable dt = new DataTable();

            dt = objneg.N_Listar_Destinos_Turisticos();

            dgvDestino.DataSource = dt;

        }
        //Método para mostrar un destino en específico mediante el filtro código
        void MostrarDestinoTuristico()
        {
            if (cbBuscar.SelectedItem != null)
            {
                if (txtBuscarDestino.Text != "")
                {
                    objent.destino = txtBuscarDestino.Text;
                    objent.origen = txtBuscarDestino.Text;
                    objent.valorBusqueda = cbBuscar.SelectedItem.ToString();



                    // Se crea un nuevo DataTable y luego lo llena con los datos de los destinos turísticos
                    DataTable dt = new DataTable();

                    dt = objneg.N_buscar_DestinoTuristicos(objent);

                    dgvDestino.DataSource = dt;
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

        // Método para registrar los destinos en la base de datos
        void AgregarDestinosTuristicos()
        {
            // Muestra un mensaje de confirmación antes de realizar el registro
            if (MessageBox.Show("¿Deseas registrar a " + txtCodigoDestino.Text + "?", "Mensaje",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
            {
                //metodo para registrar destinos turísticos
                ingresar("1");
                limpiar();
                MostrarDestinoT();

            }

        }

        void CargarDestinoTuristicos()
        {
            // Obtiene la fila seleccionada en el DataGridView
            int fila = dgvDestino.CurrentCell.RowIndex;

            // Asigna los valores de la fila seleccionada a los TextBox
            txtCodigoDestino.Text = dgvDestino[0, fila].Value.ToString();
            txtOrigen.Text = dgvDestino[1, fila].Value.ToString();
            txtDestino.Text = dgvDestino[2, fila].Value.ToString();
      txtHotel.Text = dgvDestino[3, fila].Value.ToString();
      txtNum_viajes.Text = dgvDestino[4, fila].Value.ToString();
      txtPrecio.Text = dgvDestino[5, fila].Value.ToString();
        }
        //Método para limpiar las cajas de texto al registrar destinos turísticos
        private void limpiar()
        {
            txtCodigoDestino.Clear();
            txtOrigen.Clear();
            txtDestino.Clear();
      txtHotel.Clear();
      txtNum_viajes.Clear();
            txtPrecio.Clear();

        }


        private void btnMostrarDestinos_Click(object sender, EventArgs e)
        {
            //Llama al método MostrarDestinoTuristico para cargar los datos en la tabla
            MostrarDestinoTuristico();
        }

        private void btnAgregarDestinos_Click(object sender, EventArgs e)
        {
            // Llama al método agregar destino que contien la acción "1" para registrar
            AgregarDestinosTuristicos();

        }

        private void btnActualizarDestino_Click(object sender, EventArgs e)
        {

            // Llama al método ingresar con la acción "2" para actualizar

            actualizar("2");
            limpiar();
            MostrarDestinoT();
        }

        private void NuevoDestino_Load(object sender, EventArgs e)
        {
            // Carga los destinos turísticos en el DataGridView al iniciar el formulario
            dgvDestino.DataSource = objneg.N_Listar_Destinos_Turisticos();
      cbBuscar.SelectedItem = "Destino";

    }


        private void dgvDestino_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            //Se define una variable de ToolTip
            ToolTip toolTip = new ToolTip();
            //Se establece que si el puntero está en una determinada celda, entonces se le presente al usuario un mensaje 
            //que le indique que debe dar doble click para pasar los datos del datagridview hacia los textbox
            if (e.RowIndex > -1)
            {
                toolTip.SetToolTip(this.dgvDestino, "Presione doble click para llenar el formulario de edición");
                //Se establece un tiempo de delay tras aparecer el mensaje, esto con la finalidad de evitar multiples mensajes en la pantalla
                toolTip.AutoPopDelay = 1000;
                //toolTip.ReshowDelay = 500;
            }
        }

        private void dgvDestino_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            //Se establece que si el puntero sale de una determinada celda, entonces se cambie el color de fondo y del control en sí.
            if (e.RowIndex > -1)
            {
                dgvDestino.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                dgvDestino.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void dgvDestino_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Se establece que si el puntero se encuentra en una determinada celda, entonces se cambie el color de fondo y del control en sí.
            if (e.RowIndex > -1)
            {
                dgvDestino.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Orange;
                dgvDestino.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
            }
        }

        private void dgvDestino_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Llama al método CargarDestinoTuristicos al hacer clic en la opción actualizar
            CargarDestinoTuristicos();
        }

        private void dgvDestino_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtBuscarDestino_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbBuscar.SelectedItem.ToString())
            {
                case "Destino":
                    txtBuscarDestino.Text = "";
                    break;
                case "Origen":
                    txtBuscarDestino.Text = "";
                    break;

            }

        }

        private void btnMTodo_Click(object sender, EventArgs e)
        {
            // Se crea un nuevo DataTable y luego lo llena con los datos de los destinos turísticos
            MostrarDestinoT();
        }

    private void label1_Click(object sender, EventArgs e)
    {

    }
  }
}
