using SistemaElectoral.DAl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaElectoral
{
    public partial class AgregarPlanchaForm : Form
    {
        TextBox txtNombre;
        TextBox txtLogo;
        TextBox txtPresidente;
        TextBox txtVice;
        TextBox txtTesorero;

        Button btnGuardar;
        public AgregarPlanchaForm()
        {
            InitializeComponent();
            CrearDiseño();
        }

        private void CrearDiseño()
        {
            this.Text = "Agregar Plancha";
            this.Size = new Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(255, 240, 245);

            Label lblTitulo = new Label();
            lblTitulo.Text = "Nueva Plancha";
            lblTitulo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitulo.ForeColor = Color.HotPink;
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(140, 20);

            this.Controls.Add(lblTitulo);
            // Nombre
            Label lblNombre = new Label();
            lblNombre.Text = "Nombre Plancha";
            lblNombre.Location = new Point(50, 90);

            txtNombre = new TextBox();
            txtNombre.Location = new Point(50, 115);
            txtNombre.Width = 350;

            // Logo
            Label lblLogo = new Label();
            lblLogo.Text = "Logo";
            lblLogo.Location = new Point(50, 150);

            txtLogo = new TextBox();
            txtLogo.Location = new Point(50, 175);
            txtLogo.Width = 350;

            // Presidente
            Label lblPresi = new Label();
            lblPresi.Text = "Presidente";
            lblPresi.Location = new Point(50, 210);

            txtPresidente = new TextBox();
            txtPresidente.Location = new Point(50, 235);
            txtPresidente.Width = 350;

            // Vice
            Label lblVice = new Label();
            lblVice.Text = "Vicepresidente";
            lblVice.Location = new Point(50, 270);

            txtVice = new TextBox();
            txtVice.Location = new Point(50, 295);
            txtVice.Width = 350;

            // Tesorero
            Label lblTeso = new Label();
            lblTeso.Text = "Tesorero";
            lblTeso.Location = new Point(50, 330);

            txtTesorero = new TextBox();
            txtTesorero.Location = new Point(50, 355);
            txtTesorero.Width = 350;

            // Botón
            btnGuardar = new Button();
            btnGuardar.Text = "Guardar 💖";
            btnGuardar.BackColor = Color.HotPink;
            btnGuardar.ForeColor = Color.White;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Location = new Point(120, 410);
            btnGuardar.Size = new Size(220, 40);

            btnGuardar.Click += BtnGuardar_Click;

            this.Controls.Add(lblNombre);
            this.Controls.Add(txtNombre);

            this.Controls.Add(lblLogo);
            this.Controls.Add(txtLogo);

            this.Controls.Add(lblPresi);
            this.Controls.Add(txtPresidente);

            this.Controls.Add(lblVice);
            this.Controls.Add(txtVice);

            this.Controls.Add(lblTeso);
            this.Controls.Add(txtTesorero);

            this.Controls.Add(btnGuardar);
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            using (SistemaElectoralEntities db = new SistemaElectoralEntities())
            {
                Planchas nueva = new Planchas();

                nueva.Nombre = txtNombre.Text;
                nueva.Logo = txtLogo.Text;
                nueva.Presidente = txtPresidente.Text;
                nueva.Vicepresidente = txtVice.Text;
                nueva.Tesorero = txtTesorero.Text;

                db.Planchas.Add(nueva);

                db.SaveChanges();

                MessageBox.Show("Plancha agregada correctamente");

                this.Close();
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();

            abrir.Filter = "Imagenes|*.jpg;*.png;*.jpeg";

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                txtLogo.Text = abrir.FileName;
            }
        }

        private void AgregarPlanchaForm_Load(object sender, EventArgs e)
        {

        }
    }
}
