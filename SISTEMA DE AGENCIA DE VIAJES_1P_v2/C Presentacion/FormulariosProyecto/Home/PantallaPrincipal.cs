
using C_Presentacion.FormulariosProyecto.Inventario;
using C_Presentacion.FormulariosProyecto.Reserva;
using C_Presentacion.FormulariosProyecto.Reservas;
using C_Presentacion.Properties;
using C_Presentacion.Utils;
using FontAwesome.Sharp;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
// Esta es la clase principal para la pantalla principal de la aplicación.
// Contiene funciones para personalizar la apariencia de la pantalla, mostrar y ocultar menús y submenús, abrir y cerrar formularios hijos, y manejar eventos de mouse para cambiar la apariencia de los botones.
namespace C_Presentacion.Home
{
    public partial class PantallaPrincipal : Form
    {
        private IconButton currentbtn;
        private Panel LeftBorderBtn;
        private Form FormularioActivo = null;

        string nombre = UserCache.nombre;
        string usuario = UserCache.usuario;
        byte[] perfil = UserCache.perfil;

        private struct RGBCOLORS
        {
            public static Color azulbajo = Color.FromArgb(24, 161, 251);
        }

        public PantallaPrincipal()
        {
            InitializeComponent();
            customdesign();
            // Creación de un panel para el borde izquierdo de los botones de navegación
            LeftBorderBtn = new Panel();
            panelOpciones.Controls.Add(LeftBorderBtn);
            setUserData(usuario, nombre, perfil);
      //this.KeyPreview = true;
      //this.KeyDown += PantallaPrincipal_KeyDown;
      this.PreviewKeyDown += PantallaPrincipal_PreviewKeyDown;
    }



    private void PantallaPrincipal_KeyDown(object sender, KeyEventArgs e)
    {
      // Verificar si se presionó la tecla Tab
      if (e.KeyCode == Keys.Tab)
      {
        // Obtener el índice del formulario hijo actual
        int currentIndex = -1;
        for (int i = 0; i < gunaPanelForms.Controls.Count; i++)
        {
          if (gunaPanelForms.Controls[i] == FormularioActivo)
          {
            currentIndex = i;
            break;
          }
        }

        // Obtener el índice del siguiente formulario hijo
        int nextIndex = (currentIndex + 1) % gunaPanelForms.Controls.Count;

        // Abrir el siguiente formulario hijo
        abrirhijoform((Form)gunaPanelForms.Controls[nextIndex]);
      }
    }
    private void PantallaPrincipal_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
    {

      // Verificar si se presionó la tecla Tab
      if (e.KeyCode == Keys.Tab)
      {
        // Obtener el índice del formulario hijo actual
        int currentIndex = -1;
        for (int i = 0; i < gunaPanelForms.Controls.Count; i++)
        {
          if (gunaPanelForms.Controls[i] == FormularioActivo)
          {
            currentIndex = i;
            break;
          }
        }

        // Obtener el índice del siguiente formulario hijo
        int nextIndex = (currentIndex + 1) % gunaPanelForms.Controls.Count;

        // Abrir el siguiente formulario hijo
        abrirhijoform((Form)gunaPanelForms.Controls[nextIndex]);

        // Llamar al evento KeyPress del formulario hijo en foco (presionar Enter)
        gunaPanelForms.Controls[nextIndex].Focus();
        SendKeys.Send("{ENTER}");
      }
    }
    private void PantallaPrincipal_Load(object sender, EventArgs e)
    {
      if (Login.area == "A0001") {
        this.BtnInventario.Visible = true;
        this.BtnClientes.Visible = false;
        this.iconButton2.Visible = false;
        //this.bt.Visible = true;
      }
        //admin
        // Al cargar la pantalla, se hacen visibles los botones para el usuario con permisos de administrador
        
            //this.BtnReservas.Visible = true;
        }

        // Método para personalizar el diseño de la pantalla
        private void customdesign()
        {
            this.OpcReservas.Visible = false;
            this.OpcClientes.Visible = false;
      this.panel5.Visible = false;
            this.OpcInventario.Visible = false;
        }

        // Se muestran los submenús al iniciar la aplicación
        private void hidesubmenu()
        {
            if (OpcReservas.Visible)
            {
                OpcReservas.Visible = false;
            }
            if (OpcClientes.Visible)
            {
        OpcClientes.Visible = false;
            }
            if (OpcInventario.Visible)
            {
                OpcInventario.Visible = false;
            }
        }

        // Método para ocultar los submenús
        private void showsubmenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                hidesubmenu();
                submenu.Visible = true;
            }
            else
            {
                submenu.Visible = false;
            }
        }

        // Método para desactivar el botón seleccionado actualmente
        private void desactivarBoton()
        {
            if (currentbtn != null)
            {
                currentbtn.BackColor = Color.FromArgb(11, 53, 103);
                currentbtn.ForeColor = Color.White;
                currentbtn.TextAlign = ContentAlignment.MiddleCenter;
                currentbtn.IconColor = Color.White;
                currentbtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentbtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        public void abrirhijoform(Form FrmHijo)
        {
            //Si existe formulairo abierto lo cerramos
            if (FormularioActivo != null)
            {
                FormularioActivo.Close();
            }
            //almacenamos el form abierto agregamos estilos y mostramos
            FormularioActivo = FrmHijo;
            FrmHijo.TopLevel = false;
            FrmHijo.FormBorderStyle = FormBorderStyle.None;
            FrmHijo.Dock = DockStyle.Fill;
            gunaPanelForms.Controls.Add(FrmHijo);
            gunaPanelForms.Tag = FrmHijo;
            FrmHijo.BringToFront();
            FrmHijo.Show();
            titleAndBreadcrumb(FrmHijo.Text);
        }

        //BtnNuevCli abrirhijoform(new RegistrarCliente());
        private void BtnNuevCli_Click_1(object sender, EventArgs e)
        {
            abrirhijoform(new RegistrarCliente());
        }

        private void BtnReservas_Click(object sender, EventArgs e)
        {
            showsubmenu(OpcReservas);
            //ActivarBoton(sender, RGBCOLORS.);
        }

        private void BtnClientes_Click(object sender, EventArgs e)
        {
            showsubmenu(OpcClientes);
        }

        private void BtnInventario_Click(object sender, EventArgs e)
        {
            showsubmenu(OpcInventario);
        }

        private void BtnNuevoDesT_Click(object sender, EventArgs e)
        {
            abrirhijoform(new Destino());
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            abrirhijoform(new Aerolineas());
        }

        private void btnNuevoCli_Click(object sender, EventArgs e)
        {
            abrirhijoform(new RegistrarCliente());
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            abrirhijoform(new Reserva());
        }

        private void PantallaPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void titleAndBreadcrumb(string text)
        {
            lblBreadcrumb.Text = text;
            lblTitleBar.Text = Settings.Default.appName + " - " + text;
        }

        private void setUserData(string username, string name, byte[] profileImage)
        {
            lblUserUsername.Text = username;
            lblUserName.Text = name;
            if (profileImage != null)
            {
                gunaUserAvatar.Image = Image.FromStream(new MemoryStream(profileImage));
            }
            else
            {
                gunaUserAvatar.Image = Resources.dummy_avatar;
            }
        }

        private void deleteUserCache()
        {
            UserCache.nombre = null;
            UserCache.usuario = null;
            UserCache.perfil = null;
        }

        private void gunaLogout_Click(object sender, EventArgs e)
        {
            // Muestra el formulario de login nuevamente
            Login login = new Login();
            login.Show();
            // Elimina los datos del usuario en caché
            deleteUserCache();
            // Cierra la pantalla actual
            this.Dispose();
        }

    private void iconButton5_Click(object sender, EventArgs e)
    {
      abrirhijoform(new Reserva());
    }

    private void iconButton3_Click(object sender, EventArgs e)
    {
      abrirhijoform(new RegistrarCliente());
    }

    private void iconButton4_Click(object sender, EventArgs e)
    {
      showsubmenu(panel5);
    }

    private void iconButton5_Click_1(object sender, EventArgs e)
    {
      abrirhijoform(new Reserva());
    }
  }
}
