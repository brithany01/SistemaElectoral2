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
    public partial class MenuForm : Form
    {
        private Usuarios usuarioActual;
        public MenuForm(Usuarios usuario)
        {
            InitializeComponent();
            usuarioActual = usuario;
            CrearMenu();
        }

        private void CrearMenu()
        {
            this.Text = "Menú Principal";
            this.Size = new Size(400, 400);
            this.BackColor = Color.FromArgb(255, 240, 245);

            Label titulo = new Label();
            titulo.Text = "Bienvenido 💖";
            titulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            titulo.ForeColor = Color.HotPink;
            titulo.AutoSize = true;
            titulo.Location = new Point(120, 30);

            Button btnVotar = new Button();
            btnVotar.Text = "Votar 🗳️";
            btnVotar.Size = new Size(200, 40);
            btnVotar.Location = new Point(100, 100);
            btnVotar.Click += BtnVotar_Click;

            Button btnResultados = new Button();
            btnResultados.Text = "Resultados 📊";
            btnResultados.Size = new Size(200, 40);
            btnResultados.Location = new Point(100, 160);

            Button btnSalir = new Button();
            btnSalir.Text = "Salir ❌";
            btnSalir.Size = new Size(200, 40);
            btnSalir.Location = new Point(100, 220);
            btnSalir.Click += (s, e) => Application.Exit();

            this.Controls.Add(titulo);
            this.Controls.Add(btnVotar);
            this.Controls.Add(btnResultados);
            this.Controls.Add(btnSalir);
        }

        private void BtnVotar_Click(object sender, EventArgs e)
        {
            VotacionForm votar = new VotacionForm(usuarioActual);
            votar.ShowDialog();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {

        }
    }
}
