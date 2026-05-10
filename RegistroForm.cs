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
    public partial class RegistroForm : Form
    {

        TextBox txtNombre, txtCurso, txtSeccion, txtMatricula, txtPassword;
        ComboBox cmbRol, cmbPlancha;
        Button btnGuardar;
        public RegistroForm()
        {
            InitializeComponent();
            CrearControles();
        }

        private void CrearControles()
        {
            this.Text = "Registro ";
            this.Size = new Size(400, 500);
            this.BackColor = Color.FromArgb(255, 240, 245);

            Label titulo = new Label();
            titulo.Text = "Registro de Usuario";
            titulo.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            titulo.ForeColor = Color.HotPink;
            titulo.Location = new Point(90, 20);
            titulo.AutoSize = true;
            
            txtNombre = new TextBox() { Location = new Point(50, 80), Width = 280 };
            txtCurso = new TextBox() { Location = new Point(50, 120), Width = 280 };
            txtSeccion = new TextBox() { Location = new Point(50, 160), Width = 280 };
            txtMatricula = new TextBox() { Location = new Point(50, 200), Width = 280 };
            txtPassword = new TextBox() { Location = new Point(50, 240), Width = 280, UseSystemPasswordChar = true };
            Label lblNombre = new Label() { Text = "Nombre", Location = new Point(50, 60) };
            Label lblCurso = new Label() { Text = "Curso", Location = new Point(50, 100) };
            Label lblSeccion = new Label() { Text = "Sección", Location = new Point(50, 140) };
            Label lblMatricula = new Label() { Text = "Matrícula", Location = new Point(50, 180) };
            Label lblPassword = new Label() { Text = "Contraseña", Location = new Point(50, 220) };
            Label lblRol = new Label() { Text = "Rol", Location = new Point(50, 260) };
            Label lblPlancha = new Label() { Text = "Plancha", Location = new Point(50, 300) };
            cmbRol = new ComboBox() { Location = new Point(50, 280), Width = 280 };
            cmbRol.Items.AddRange(new string[] { "Votante", "Admin" });

            cmbPlancha = new ComboBox() { Location = new Point(50, 320), Width = 280 };

            btnGuardar = new Button()
            {
                Text = "Guardar 💾",
                Location = new Point(50, 370),
                Width = 280,
                BackColor = Color.HotPink,
                ForeColor = Color.White
            };

            btnGuardar.Click += btnGuardar_Click;

            // TÍTULO
            this.Controls.Add(titulo);

            // LABELS 👇 (ESTO TE FALTABA)
            this.Controls.Add(lblNombre);
            this.Controls.Add(lblCurso);
            this.Controls.Add(lblSeccion);
            this.Controls.Add(lblMatricula);
            this.Controls.Add(lblPassword);
            this.Controls.Add(lblRol);
            this.Controls.Add(lblPlancha);

            // INPUTS 👇
            this.Controls.Add(txtNombre);
            this.Controls.Add(txtCurso);
            this.Controls.Add(txtSeccion);
            this.Controls.Add(txtMatricula);
            this.Controls.Add(txtPassword);
            this.Controls.Add(cmbRol);
            this.Controls.Add(cmbPlancha);

            // BOTÓN
            this.Controls.Add(btnGuardar);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            using (var db = new SistemaElectoralEntities())
            {
                cmbPlancha.DataSource = db.Planchas.ToList();
                cmbPlancha.DisplayMember = "Nombre";
                cmbPlancha.ValueMember = "Id";
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtMatricula.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Completa los campos");
                return;
            }

            using (var db = new SistemaElectoralEntities())
            {
                var existe = db.Usuarios
                    .FirstOrDefault(u => u.Matricula == txtMatricula.Text);

                if (existe != null)
                {
                    MessageBox.Show("La matrícula ya existe.");
                    return;
                }

                Usuarios nuevo = new Usuarios();

                nuevo.Nombre = txtNombre.Text;
                nuevo.Curso = txtCurso.Text;
                nuevo.Seccion = txtSeccion.Text;
                nuevo.Matricula = txtMatricula.Text;
                nuevo.Password = txtPassword.Text;
                nuevo.Rol = cmbRol.Text;
                nuevo.YaVoto = false;

                db.Usuarios.Add(nuevo);
                db.SaveChanges();

                MessageBox.Show("Usuario registrado correctamente");

                this.Hide();

                if (nuevo.Rol == "Admin")
                {
                    AdminForm admin = new AdminForm(nuevo);
                    admin.Show();
                }
                else
                {
                    MenuForm menu = new MenuForm(nuevo);
                    menu.Show();
                }
            }
        }
        private void RegistroForm_Load(object sender, EventArgs e)
        {

        }
    }
}
