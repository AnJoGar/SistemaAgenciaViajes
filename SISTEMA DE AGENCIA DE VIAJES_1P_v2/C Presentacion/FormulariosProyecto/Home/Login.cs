using C_Presentacion.Utils;
using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Data;
using System.Windows.Forms;

namespace C_Presentacion.Home
{
    public partial class Login : Form
    {
        E_users e_Users = new E_users();
        N_users n_Users = new N_users();
        public static string usuario_nombre;
    public static string area;
    public Login()
        {
            InitializeComponent();
        }

        void authenticate()
        {
      if (string.IsNullOrEmpty(gunaUsername.Text) || string.IsNullOrEmpty(gunaPassword.Text))
      {
        lbl_Error.Text = "Por favor, complete todos los campos";
        System.Media.SystemSounds.Exclamation.Play(); // Reproduce el sonido de error
        return;
      }

      DataTable dt = new DataTable();
      e_Users.usuario = gunaUsername.Text;
      e_Users.clave = gunaPassword.Text;

      dt = n_Users.N_user(e_Users);

      if (dt.Rows.Count > 0)
      {
        string nombre = dt.Rows[0][1].ToString();
        string usuario = dt.Rows[0][2].ToString();
        byte[] avatar = null;
       
        


        MessageBox.Show("Bienvenido " + usuario, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

        UserCache.usuario = usuario;
        UserCache.nombre = nombre;
        UserCache.perfil = avatar;
        area = dt.Rows[0][0].ToString();

        PantallaPrincipal pantallaPrincipal = new PantallaPrincipal();
        pantallaPrincipal.Show();

        //MainForm mainForm = new MainForm();
        //mainForm.Show();

        this.Hide(); // Oculta el formulario actual en lugar de cerrarlo
      }
      else
      {
        lbl_Error.Text = "Credenciales incorrectas";
        System.Media.SystemSounds.Exclamation.Play(); // Reproduce el sonido de error
      }

      gunaUsername.Clear();
      gunaPassword.Clear();

    }

        private void gunaLogin_Click(object sender, EventArgs e)
        {
            authenticate();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void gunaFields_TextChanged(object sender, EventArgs e)
        {
            lbl_Error.Text = string.Empty;
        }

        private void gunaFields_MouseEnter(object sender, EventArgs e)
        {
            lbl_Error.Text = string.Empty;
        }
    }
}
