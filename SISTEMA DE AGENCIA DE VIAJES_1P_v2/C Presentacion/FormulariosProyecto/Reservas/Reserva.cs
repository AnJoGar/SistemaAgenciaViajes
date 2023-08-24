using Capa_Datos;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace C_Presentacion.FormulariosProyecto.Reservas
{
    public partial class Reserva : Form
    {
        public Reserva()
        {
            InitializeComponent();
            fillPatient();
            Destino();
            Cliente();
            ListarDatos();

            gunaCampo.Items.Add("aerolinea");
            gunaCampo.Items.Add("destino");
            gunaCampo.Items.Add("cliente");
            gunaCampo.Items.Add("precio");

            gunaCampo.SelectedIndex = 0;

        }

        //    public SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["sql"].ConnectionString);
        private void fillPatient()
        {
            Conexion_Desconexion_bd c = new Conexion_Desconexion_bd();
            using (SqlConnection Con = c.abrir_conexion())
            {
                SqlCommand cmd = new SqlCommand("select nombre,matricula_avion from Aerolineas", Con);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("nombre", typeof(string));
        dt.Columns.Add("matricula_avion", typeof(string));
        dt.Columns.Add("nombreMatricula", typeof(string), "nombre + ' - ' + matricula_avion");

        dt.Load(rdr);
                gunaAerolinea.ValueMember = "nombreMatricula";
                gunaAerolinea.DisplayMember = "nombreMatricula";
                gunaAerolinea.DataSource = dt;
            } // La 
        }

        private void Destino()
        {
            Conexion_Desconexion_bd c = new Conexion_Desconexion_bd();
            using (SqlConnection Con = c.abrir_conexion())
            {
                SqlCommand cmd = new SqlCommand("select  precio,origen,destino,hotel from DestinoTuristico;", Con);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
        dt.Columns.Add("origen", typeof(string));
        dt.Columns.Add("destino", typeof(string));
        dt.Columns.Add("hotel", typeof(string));
        // dt.Columns.Add("destino", typeof(string));
        
        dt.Columns.Add("precio", typeof(decimal)); // Change the data type to decimal
                 dt.Load(rdr);

        //foreach (DataRow row in dt.Rows)
       // {
        //  decimal precio = (decimal)row["precio"];
         // row["precio"] = precio * 2;
        //}

        dt.Columns.Add("DestinoConOrigen", typeof(string), "origen + ' - ' + destino");
       // dt.Columns.Add("DestinoConOrigen", typeof(string), "destino + ' - ' + origen");
        gunaDestino.ValueMember = "DestinoConOrigen";
                gunaDestino.DisplayMember = "DestinoConOrigen";
        //gunaDestino.DisplayMember = "origen";
        gunaDestino.DataSource = dt;
                gunaPrecio.DataBindings.Add("Text", dt, "precio", true, DataSourceUpdateMode.OnValidation); // Set the DataSourceUpdateMode to OnValidation to handle the FormatException
        txtHotel.DataBindings.Add("Text", dt, "hotel", true, DataSourceUpdateMode.OnValidation);
      }
        }
        private void Cliente()
        {
            Conexion_Desconexion_bd c = new Conexion_Desconexion_bd();
            using (SqlConnection Con = c.abrir_conexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT CONCAT(nombre, ' ', apellido) AS nombre_completo FROM Cliente;", Con);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("nombre_completo", typeof(string));
                dt.Load(rdr);
                gunaComboCliente.ValueMember = "nombre_completo";
                gunaComboCliente.DisplayMember = "nombre_completo";
                gunaComboCliente.DataSource = dt;
            } // La 
        }
    private void EliminarReserva(int reservaID)
    {
      // Conexión a la base de datos
      Conexion_Desconexion_bd c = new Conexion_Desconexion_bd();
      using (SqlConnection Con = c.abrir_conexion())
      {
        // Create the SQL command to delete the reservation with the specified ID
        SqlCommand command = new SqlCommand("DELETE FROM Reserva WHERE ID = @reservaID", Con);
        command.Parameters.AddWithValue("@reservaID", reservaID);

        // Execute the SQL command
        int rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
          MessageBox.Show("¡Reserva eliminada correctamente!", "Eliminación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
          MessageBox.Show("No se encontró la reserva con el ID especificado.", "Reserva no encontrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
      }
    }
    private void RegistrarValor()
    {
      string aerolineaSeleccionada = gunaAerolinea.SelectedValue.ToString();
      string destinoSeleccionado = gunaDestino.SelectedValue.ToString();
      string clienteSeleccionado = gunaComboCliente.SelectedValue.ToString();
      string PrecioSeleccionado = gunaPrecio.Text;
      string PrecioSeleccionado1 = guna2DateTimePicker1.Text;
      string hotelSeleccionado = txtHotel.Text;


      string[] parts = aerolineaSeleccionada.Split(new string[] { " - " }, StringSplitOptions.None);
      string nombreAerolinea = parts[0];

      DateTime fechaReserva = DateTime.Parse(PrecioSeleccionado1);
      if (fechaReserva < DateTime.Today)
      {
        MessageBox.Show("No puedes realizar una reserva en una fecha anterior a la fecha actual.", "Fecha inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return; // Stop further execution
      }

      // Conexión a la base de datos
      Conexion_Desconexion_bd c = new Conexion_Desconexion_bd();
      using (SqlConnection Con = c.abrir_conexion())
      {
        int boletos = 0;
        // Get the count of records for the selected airline and date from the database
        SqlCommand countCommand = new SqlCommand("SELECT COUNT(*) FROM Reserva WHERE aerolinea = @aerolinea AND fecha_reserva = @fecha_reserva", Con);
        countCommand.Parameters.AddWithValue("@aerolinea", aerolineaSeleccionada);
        countCommand.Parameters.AddWithValue("@fecha_reserva", fechaReserva);
        int countAerolinea = (int)countCommand.ExecuteScalar();


        SqlCommand getNumeroViajerosCommand1 = new SqlCommand("SELECT cantidad_boletos FROM Aerolineas WHERE nombre = @nombre", Con);
        getNumeroViajerosCommand1.Parameters.Add("@nombre", nombreAerolinea);
        object numeroViajerosObj1 = getNumeroViajerosCommand1.ExecuteScalar();
        if (numeroViajerosObj1 != null && int.TryParse(numeroViajerosObj1.ToString(), out int Viajeros))
        {
          boletos = Viajeros;
        }
      //  MessageBox.Show($"countAerolinea: {countAerolinea}, boletos: {boletos}");

        // Check if the count for the airline exceeds the limit (four records)
        if (countAerolinea >= boletos)
        {
          // Show message box indicating that the limit is reached
          MessageBox.Show("La aerolínea ya no contiene boletos para este día. Por favor, selecciona otra aerolínea o día para realizar la reserva.", "Límite alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Information);
          return; // Stop further execution
        }


        int numeroViajeros = 0;
        // Get the count of records for the selected destination and date from the database
        SqlCommand countDestinationCommand = new SqlCommand("SELECT COUNT(*) FROM Reserva WHERE destino = @destino AND fecha_reserva = @fecha_reserva", Con);
        countDestinationCommand.Parameters.AddWithValue("@destino", destinoSeleccionado);
        countDestinationCommand.Parameters.AddWithValue("@fecha_reserva", fechaReserva);
        int countDestino = (int)countDestinationCommand.ExecuteScalar();

        SqlCommand getNumeroViajerosCommand = new SqlCommand("SELECT capacidad_viajeros FROM DestinoTuristico WHERE precio = @precio", Con);
        getNumeroViajerosCommand.Parameters.Add("@precio", SqlDbType.Decimal).Value = decimal.Parse(PrecioSeleccionado);
        object numeroViajerosObj = getNumeroViajerosCommand.ExecuteScalar();
        if (numeroViajerosObj != null && int.TryParse(numeroViajerosObj.ToString(), out int parsedNumeroViajeros))
        {
          numeroViajeros = parsedNumeroViajeros;
        }
        // Check if the count for the destination exceeds the limit (five records)
        if (countDestino >= numeroViajeros)
        {
          // Show message box indicating that the limit is reached
          MessageBox.Show("Ya se han realizado " + countDestino +
            " reservas para este destino en este día. El límite es de "
            + numeroViajeros + " viajeros. Por favor, selecciona otro " +
            "destino o día para realizar la reserva.", "Límite alcanzado",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
          return; 
        }
        // Create the SQL command to insert the values into the table
        SqlCommand command = new SqlCommand("INSERT INTO Reserva (aerolinea, destino, cliente, precio, hotel, fecha_reserva) VALUES (@aerolinea, @destino, @cliente, @precio, @hotel, @fecha_reserva)", Con);
        command.Parameters.AddWithValue("@aerolinea", aerolineaSeleccionada);
        command.Parameters.AddWithValue("@destino", destinoSeleccionado);
        command.Parameters.AddWithValue("@cliente", clienteSeleccionado);
        command.Parameters.AddWithValue("@precio", PrecioSeleccionado);
        command.Parameters.AddWithValue("@fecha_reserva", fechaReserva);
        command.Parameters.AddWithValue("@hotel", hotelSeleccionado);
        // Execute the SQL command
        command.ExecuteNonQuery();
      }

      MessageBox.Show("¡La reserva se ha generado correctamente!", "Reserva Generada", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }



    private void ActualizarReserva(int id, string aerolineaSeleccionada, string destinoSeleccionado, string clienteSeleccionado, string PrecioSeleccionado,string hotelSeleccionado, string PrecioSeleccionado1)
    {
      id = int.Parse(guna2TextBox1.Text);
      aerolineaSeleccionada = gunaAerolinea.SelectedValue.ToString();
      destinoSeleccionado = gunaDestino.SelectedValue.ToString();
      clienteSeleccionado = gunaComboCliente.SelectedValue.ToString();
      PrecioSeleccionado = gunaPrecio.Text;
      PrecioSeleccionado1 = guna2DateTimePicker1.Text;
      hotelSeleccionado = txtHotel.Text;
      string[] parts = aerolineaSeleccionada.Split(new string[] { " - " }, StringSplitOptions.None);
      string nombreAerolinea = parts[0];
      // Validate if the selected date is before the current date
      DateTime fechaReserva = DateTime.Parse(PrecioSeleccionado1);
      if (fechaReserva < DateTime.Today)
      {
        MessageBox.Show("No puedes realizar una reserva en una fecha anterior a la fecha actual.", "Fecha inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return; // Stop further execution
      }

      // Conexión a la base de datos
      Conexion_Desconexion_bd c = new Conexion_Desconexion_bd();
      using (SqlConnection Con = c.abrir_conexion())
      {
        // Check if the updated date is before the current date
        SqlCommand dateCheckCommand = new SqlCommand("SELECT fecha_reserva FROM Reserva WHERE id = @id", Con);
        dateCheckCommand.Parameters.AddWithValue("@id", id);
        DateTime existingDate = (DateTime)dateCheckCommand.ExecuteScalar();

        if (existingDate < DateTime.Today)
        {
          MessageBox.Show("No puedes actualizar una reserva con una fecha anterior a la fecha actual.", "Fecha inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return; // Stop further execution
        }
        int boletos = 0;
        // Get the count of records for the selected airline and date from the database
        SqlCommand countCommand = new SqlCommand("SELECT COUNT(*) FROM Reserva WHERE aerolinea = @aerolinea AND fecha_reserva = @fecha_reserva", Con);
        countCommand.Parameters.AddWithValue("@aerolinea", aerolineaSeleccionada);
        countCommand.Parameters.AddWithValue("@fecha_reserva", fechaReserva);
        int countAerolinea = (int)countCommand.ExecuteScalar();


        SqlCommand getNumeroViajerosCommand1 = new SqlCommand("SELECT cantidad_boletos FROM Aerolineas WHERE nombre = @nombre", Con);
        getNumeroViajerosCommand1.Parameters.Add("@nombre", nombreAerolinea);
        object numeroViajerosObj1 = getNumeroViajerosCommand1.ExecuteScalar();
        if (numeroViajerosObj1 != null && int.TryParse(numeroViajerosObj1.ToString(), out int Viajeros))
        {
          boletos = Viajeros;
        }
        // Check if the count for the airline exceeds the limit (four records)
        if (countAerolinea >= boletos)
        {
          // Show message box indicating that the limit is reached
          MessageBox.Show("La aerolínea ya no contiene boletos para este día. Por favor, selecciona otra aerolínea o día para realizar la reserva.", "Límite alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Information);
          return; // Stop further execution
        }
        // Get the count of records for the selected airline and date from the database
        //  SqlCommand countCommand = new SqlCommand("SELECT COUNT(*) FROM Reserva WHERE aerolinea = @aerolinea AND fecha_reserva = @fecha_reserva", Con);
        //countCommand.Parameters.AddWithValue("@aerolinea", aerolineaSeleccionada);
        //countCommand.Parameters.AddWithValue("@fecha_reserva", fechaReserva);
        //int countAerolinea = (int)countCommand.ExecuteScalar();

        // Check if the count for the airline exceeds the limit (four records)
        //if (countAerolinea >= 5)
        //{
        // Show message box indicating that the limit is reached
        // MessageBox.Show("La aerolínea ya ha realizado 5 reservas para este día. Por favor, selecciona otra aerolínea o día para realizar la reserva.", "Límite alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //return; // Stop further execution
        //}
        int numeroViajeros = 0;
        // Get the count of records for the selected destination and date from the database
        SqlCommand countDestinationCommand = new SqlCommand("SELECT COUNT(*) FROM Reserva WHERE destino = @destino AND fecha_reserva = @fecha_reserva", Con);
        countDestinationCommand.Parameters.AddWithValue("@destino", destinoSeleccionado);
        countDestinationCommand.Parameters.AddWithValue("@fecha_reserva", fechaReserva);
        int countDestino = (int)countDestinationCommand.ExecuteScalar();

        SqlCommand getNumeroViajerosCommand = new SqlCommand("SELECT capacidad_viajeros FROM DestinoTuristico WHERE precio = @precio", Con);
        getNumeroViajerosCommand.Parameters.Add("@precio", SqlDbType.Decimal).Value = decimal.Parse(PrecioSeleccionado);
        object numeroViajerosObj = getNumeroViajerosCommand.ExecuteScalar();
        if (numeroViajerosObj != null && int.TryParse(numeroViajerosObj.ToString(), out int parsedNumeroViajeros))
        {
          numeroViajeros = parsedNumeroViajeros;
        }
        // Check if the count for the destination exceeds the limit (five records)
        if (countDestino >= numeroViajeros)
        {
          // Show message box indicating that the limit is reached
          MessageBox.Show("Ya se han realizado " + countDestino +
            " reservas para este destino en este día. El límite es de "
            + numeroViajeros + " viajeros. Por favor, selecciona otro " +
            "destino o día para realizar la reserva.", "Límite alcanzado",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
          return;
        }
        // Create the SQL command to update the reservation with the specified ID
        SqlCommand command = new SqlCommand("UPDATE Reserva SET aerolinea = @aerolinea, destino = @destino, cliente = @cliente, precio = @precio,hotel=@hotel, fecha_reserva = @fecha_reserva WHERE id = @id", Con);
        command.Parameters.AddWithValue("@aerolinea", aerolineaSeleccionada);
        command.Parameters.AddWithValue("@destino", destinoSeleccionado);
        command.Parameters.AddWithValue("@cliente", clienteSeleccionado);
        command.Parameters.AddWithValue("@precio", PrecioSeleccionado);
        command.Parameters.AddWithValue("@fecha_reserva", fechaReserva);
        command.Parameters.AddWithValue("@hotel", hotelSeleccionado);
        command.Parameters.AddWithValue("@id", id);

        // Execute the SQL command
        int rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
          MessageBox.Show("¡Reserva actualizada correctamente!", "Actualización Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
          MessageBox.Show("No se encontró la reserva con el ID especificado.", "Reserva no encontrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
      }
    }







    private void ListarDatos()
    {
      // Conexión a la base de datos
      Conexion_Desconexion_bd c = new Conexion_Desconexion_bd();
      using (SqlConnection Con = c.abrir_conexion())
      {
        // Crear el comando SQL para seleccionar los registros de la tabla
        SqlCommand command = new SqlCommand("SELECT id,cliente,aerolinea, destino,  precio, fecha_reserva, hotel FROM Reserva", Con);

        // Crear un adaptador de datos y un DataTable
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        DataTable dt = new DataTable();

        // Llenar el DataTable con los datos del adaptador
        adapter.Fill(dt);

        // Asignar el DataTable como origen de datos del DataGridView
        dgvDestino.DataSource = dt;
      }
    }

    private void BuscarRegistro(string campo, string valor)
        {
            // Conexión a la base de datos
            Conexion_Desconexion_bd c = new Conexion_Desconexion_bd();
            using (SqlConnection Con = c.abrir_conexion())
            {
                // Crear la consulta dinámica para buscar el registro
                string consulta = "SELECT aerolinea, destino, cliente, precio,fecha_reserva FROM Reserva WHERE " + campo + " LIKE @valor";
                SqlCommand command = new SqlCommand(consulta, Con);
                command.Parameters.AddWithValue("@valor", valor + "%"); // Agregar el carácter comodín % al final del valor de búsqueda

                // Crear un adaptador de datos y un DataTable
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();

                // Llenar el DataTable con los datos del adaptador
                adapter.Fill(dt);

        // Asignar el DataTable como origen de datos del DataGridView
        dgvDestino.DataSource = dt;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            fillPatient();
            ListarDatos();
            gunaBuscar.TextChanged += txtBuscar_TextChanged;
           // gunaCampo.Items.Add("aerolinea");
            //gunaCampo.Items.Add("destino");
            //gunaCampo.Items.Add("cliente");
            //gunaCampo.Items.Add("precio");
            gunaCampo.SelectedIndex = 0;
      gunaCampo.SelectedItem = "cliente";
     // gunaComboCliente.DropDownStyle = ComboBoxStyle.DropDown;

    }


    void CargarAerolineas()
    {
      // Obtiene la fila seleccionada en el DataGridView
      int fila = dgvDestino.CurrentCell.RowIndex;
      guna2TextBox1.Text= dgvDestino[0, fila].Value.ToString();
      // Asigna los valores de la fila seleccionada a los TextBox
      gunaComboCliente.Text = dgvDestino[1, fila].Value.ToString();
      gunaAerolinea.Text = dgvDestino[2, fila].Value.ToString();
      gunaDestino.Text = dgvDestino[3, fila].Value.ToString();
      gunaPrecio.Text = dgvDestino[4, fila].Value.ToString();
    }
    private void aerolinea_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string campo = gunaCampo.Text; // Obtener el nombre del campo seleccionado en un ComboBox
            string valor = gunaBuscar.Text; // Obtener el valor de búsqueda del TextBox

            BuscarRegistro(campo, valor); // Llamar al método BuscarRegistro con el campo y valor especificados
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListarDatos();
        }

        private void btnMostrarAeolinea_Click_1(object sender, EventArgs e)
        {
            RegistrarValor();
     // limpiar();
      ListarDatos();
        }

    private void dgvDestino_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void label10_Click(object sender, EventArgs e)
    {

    }

    private void guna2TextBox2_TextChanged(object sender, EventArgs e)
    {

    }

    private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
    {

    }

    private void panel9_Paint(object sender, PaintEventArgs e)
    {

    }

    private void dgvDestino_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
    {
      CargarAerolineas();
    }

    private void dgvDestino_DoubleClick(object sender, EventArgs e)
    {

    }

    private void dgvDestino_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void dgvDestino_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      CargarAerolineas();
    }
    private void limpiar()
    {
      guna2TextBox1.Clear();
      gunaDestino.SelectedIndex = -1;
      gunaAerolinea.SelectedIndex = -1;
      // Clear the selected value of the combobox
      gunaComboCliente.SelectedIndex = -1; // Clear the selected value of the combobox
     // gunaPrecio.Clear();
      guna2DateTimePicker1.Value = DateTime.Today; // Reset the datepicker to the current date
    }
    private void btnActualizar_Click(object sender, EventArgs e)
    {
      int id = int.Parse(guna2TextBox1.Text);
      string aerolineaSeleccionada = gunaAerolinea.SelectedValue.ToString();
      string destinoSeleccionado = gunaDestino.SelectedValue.ToString();
      string clienteSeleccionado = gunaComboCliente.SelectedValue.ToString();
      string PrecioSeleccionado = gunaPrecio.Text;
      string PrecioSeleccionado1 = guna2DateTimePicker1.Text;
      string hotelSeleccionado = txtHotel.Text;
      // Validate if the selected date is before the current date
      DateTime fechaReserva = DateTime.Parse(PrecioSeleccionado1); // <-- Add the missing closing parenthesis here
      ActualizarReserva(id, aerolineaSeleccionada, destinoSeleccionado, clienteSeleccionado, PrecioSeleccionado, PrecioSeleccionado1, hotelSeleccionado);
      limpiar();
      ListarDatos();

    }

    private void button2_Click(object sender, EventArgs e)
    {
      //destinoImagen newForm = new destinoImagen();

      // Show the new form
      //newForm.Show();

      if (gunaDestino.SelectedItem != null)
      {
        string destinoSeleccionado = gunaDestino.SelectedValue.ToString();

        if (destinoSeleccionado == "Quito - Berlin - Munich")
        {
          // Abre el formulario FormImagenesDestino y pasa la lista de imágenes para Quito-Berlin
          Destino3 formImagenes = new Destino3();
          formImagenes.StartPosition = FormStartPosition.CenterParent;
          formImagenes.ShowDialog();
        }
        else if (destinoSeleccionado == "Quito - El Cairo")
        {
          // Abre el formulario FormImagenesDestino y pasa la lista de imágenes para Quito-Madrid
          Destino2 formImagenes = new Destino2();
          formImagenes.StartPosition = FormStartPosition.CenterParent;
          formImagenes.ShowDialog();
        }
        else if (destinoSeleccionado == "Berlin - Quito")
        {
          // Abre el formulario FormImagenesDestino y pasa la lista de imágenes para Quito-Madrid
          Destino4 formImagenes = new Destino4();
          formImagenes.StartPosition = FormStartPosition.CenterParent;
          formImagenes.ShowDialog();
        }
        else if (destinoSeleccionado == "Quito - Ciudad del Cabo(Sudáfrica)")
        {
          // Abre el formulario FormImagenesDestino y pasa la lista de imágenes para Quito-Madrid
          Destino1 formImagenes = new Destino1();
          formImagenes.StartPosition = FormStartPosition.CenterParent;
          formImagenes.ShowDialog();
        }
        else {
          Destino3 formImagenes = new Destino3();
          formImagenes.StartPosition = FormStartPosition.CenterParent;
          formImagenes.ShowDialog();

        }
        // Agrega más condiciones para otros destinos si es necesario.
      }
    }

    private void gunaDestino_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
  }
}
