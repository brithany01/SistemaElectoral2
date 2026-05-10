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
    public partial class EditarPlanchaForm : Form
    {
        int idPlancha;

        TextBox txtNombre;
        TextBox txtLogo;
        TextBox txtPresidente;
        TextBox txtVice;
        TextBox txtTesorero;

        Button btnGuardar;
        public EditarPlanchaForm(int id)
        {
            InitializeComponent();

            idPlancha = id;

            CrearDiseno();

            CargarDatos();

        }

        private void CrearDiseno()
        {
            this.Text = "Editar Plancha";
            this.BackColor = Color.FromArgb(255, 240, 245);
            this.Size = new Size(600, 650);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label titulo = new Label();
            titulo.Text = "Editar Plancha";
            titulo.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            titulo.ForeColor = Color.HotPink;
            titulo.AutoSize = true;
            titulo.Location = new Point(170, 30);

            this.Controls.Add(titulo);

            int y = 120;

            txtNombre = CrearTextBox("Nombre", y);
            y += 80;

            txtLogo = CrearTextBox("Logo", y);
            y += 80;

            txtPresidente = CrearTextBox("Presidente", y);
            y += 80;

            txtVice = CrearTextBox("Vicepresidente", y);
            y += 80;

            txtTesorero = CrearTextBox("Tesorero", y);

            btnGuardar = new Button();
            btnGuardar.Text = "Guardar Cambios 💖";
            btnGuardar.Size = new Size(280, 45);
            btnGuardar.Location = new Point(150, 540);
            btnGuardar.BackColor = Color.HotPink;
            btnGuardar.ForeColor = Color.White;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            btnGuardar.Click += BtnGuardar_Click;

            this.Controls.Add(btnGuardar);
        }

        private TextBox CrearTextBox(string texto, int y)
        {
            Label lbl = new Label();

            lbl.Text = texto;
            lbl.Location = new Point(70, y);
            lbl.Font = new Font("Segoe UI", 10);

            this.Controls.Add(lbl);

            TextBox txt = new TextBox();

            txt.Size = new Size(420, 30);
            txt.Location = new Point(70, y + 25);

            this.Controls.Add(txt);

            return txt;
        }

        private void CargarDatos()
        {
            using (SistemaElectoralEntities db = new SistemaElectoralEntities())
            {
                var plancha = db.Planchas.Find(idPlancha);

                txtNombre.Text = plancha.Nombre;
                txtLogo.Text = plancha.Logo;
                txtPresidente.Text = plancha.Presidente;
                txtVice.Text = plancha.Vicepresidente;
                txtTesorero.Text = plancha.Tesorero;
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            using (SistemaElectoralEntities db =
      new SistemaElectoralEntities())
            {
                var plancha =
                    db.Planchas.Find(idPlancha);

                plancha.Nombre = txtNombre.Text;

                plancha.Logo = txtLogo.Text;

                plancha.Presidente = txtPresidente.Text;

                plancha.Vicepresidente = txtVice.Text;

                plancha.Tesorero = txtTesorero.Text;

                db.SaveChanges();

                MessageBox.Show(
                    "Plancha actualizada correctamente 💖");

                this.Close();
            }
        }



        private void EditarPlanchaForm_Load(object sender, EventArgs e)
        {

        }
    }
}
