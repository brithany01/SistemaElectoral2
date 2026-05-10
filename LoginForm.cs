using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaElectoral.DAl;

namespace SistemaElectoral
{
    public partial class LoginForm : Form
    {
        Label lblMatricula;
        TextBox txtMatricula;
        Label lblPassword;
        TextBox txtPassword;
        Button btnLogin;
        Button btnRegistrar;
        public LoginForm()
        {
            InitializeComponent();
            CrearControles();
        }

        private void CrearControles()
        {
            // Fondo del formulario
            this.BackColor = Color.FromArgb(255, 240, 245); // rosado suave
            this.Size = new Size(400, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Sistema Electoral 💖";

            // Título
            Label lblTitulo = new Label();
            lblTitulo.Text = "Iniciar Sesión";
            lblTitulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitulo.ForeColor = Color.HotPink;
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(120, 20);

            // Matrícula
            lblMatricula = new Label();
            lblMatricula.Text = "Matrícula";
            lblMatricula.Font = new Font("Segoe UI", 10);
            lblMatricula.Location = new Point(50, 90);

            txtMatricula = new TextBox();
            txtMatricula.Location = new Point(50, 110);
            txtMatricula.Width = 280;
            txtMatricula.BorderStyle = BorderStyle.FixedSingle;

            // Contraseña
            lblPassword = new Label();
            lblPassword.Text = "Contraseña";
            lblPassword.Font = new Font("Segoe UI", 10);
            lblPassword.Location = new Point(50, 150);

            txtPassword = new TextBox();
            txtPassword.Location = new Point(50, 170);
            txtPassword.Width = 280;
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;

            // Botón Login
            btnLogin = new Button();
            btnLogin.Text = "Entrar 💕";
            btnLogin.BackColor = Color.HotPink;
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Location = new Point(50, 220);
            btnLogin.Width = 280;
            btnLogin.Height = 35;
            btnLogin.Click += btnLogin_Click;

            // Botón Registrar
            btnRegistrar = new Button();
            btnRegistrar.Text = "Registrarse ✨";
            btnRegistrar.BackColor = Color.White;
            btnRegistrar.ForeColor = Color.HotPink;
            btnRegistrar.FlatStyle = FlatStyle.Flat;
            btnRegistrar.Location = new Point(50, 265);
            btnRegistrar.Width = 280;
            btnRegistrar.Height = 35;
            btnRegistrar.Click += btnRegistrar_Click;

            // Agregar controles
            this.Controls.Add(lblTitulo);
            this.Controls.Add(lblMatricula);
            this.Controls.Add(txtMatricula);
            this.Controls.Add(lblPassword);
            this.Controls.Add(txtPassword);
            this.Controls.Add(btnLogin);
            this.Controls.Add(btnRegistrar);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMatricula.Text) ||
        string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Completa todos los campos");
                return;
            }

            using (var db = new SistemaElectoralEntities())
            {
                var usuario = db.Usuarios
                    .FirstOrDefault(u =>
                        u.Matricula == txtMatricula.Text &&
                        u.Password == txtPassword.Text);

                if (usuario == null)
                {
                    MessageBox.Show("Matrícula o contraseña incorrecta");
                    return;
                }

                if (usuario.YaVoto == true && usuario.Rol != "Admin")
                {
                    MessageBox.Show("Este usuario ya votó");
                    return;
                }

                MessageBox.Show("Bienvenido " + usuario.Nombre);

                this.Hide();

                // 🔥 ADMIN
                if (usuario.Rol == "Admin")
                {
                    AdminForm admin = new AdminForm(usuario);
                    admin.ShowDialog();
                }

                // 🔥 USUARIO NORMAL
                else
                {
                    VotacionForm voto = new VotacionForm(usuario);
                    voto.ShowDialog();
                }

                this.Close();
            }
        }



        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            RegistroForm frm = new RegistroForm();
            frm.Show();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
