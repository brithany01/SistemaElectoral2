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
    public partial class AdminForm : Form
    {
      
        Panel panelTop;
        Panel panelButtons;
        Panel panelFooter;

        Label lblTitulo;
        Label lblFooter;

        Button btnAgregar;
        Button btnEditar;
        Button btnEliminar;
        Button btnRefrescar;
        Button btnSalir;
        Button btnVotar;

        DataGridView dgvPlanchas;
        Usuarios usuarioActual;

        public AdminForm(Usuarios usuario)
        {
            InitializeComponent();

            usuarioActual = usuario;

            CrearDiseno();

            CargarPlanchas();

        }
        private void CrearDiseno()
        {
            // ================= FORM =================

            this.Text = "Panel Administrativo";
            this.BackColor = Color.FromArgb(250, 240, 245);
            this.Size = new Size(1400, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // ================= PANEL TOP =================

            panelTop = new Panel();
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 90;
            panelTop.BackColor = Color.HotPink;

            this.Controls.Add(panelTop);

            // ================= TITULO =================

            lblTitulo = new Label();

            lblTitulo.Text = "PANEL ADMINISTRATIVO";

            lblTitulo.Dock = DockStyle.Fill;

            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            lblTitulo.Font = new Font("Segoe UI", 24, FontStyle.Bold);

            lblTitulo.ForeColor = Color.White;

            panelTop.Controls.Add(lblTitulo);

            // ================= PANEL BOTONES =================

            panelButtons = new Panel();

            panelButtons.Dock = DockStyle.Top;

            panelButtons.Height = 80;

            panelButtons.BackColor = Color.White;

            this.Controls.Add(panelButtons);

            // ================= BOTON AGREGAR =================

            btnAgregar = CrearBoton("Agregar", 20);

            btnAgregar.Click += BtnAgregar_Click;

            // ================= BOTON EDITAR =================

            btnEditar = CrearBoton("Editar", 190);

            btnEditar.Click += BtnEditar_Click;

            // ================= BOTON ELIMINAR =================

            btnEliminar = CrearBoton("Eliminar", 360);

            btnEliminar.Click += BtnEliminar_Click;

            // ================= BOTON REFRESCAR =================

            btnRefrescar = CrearBoton("Refrescar", 530);

            btnRefrescar.Click += BtnRefrescar_Click;

            // ================= BOTON VOTAR =================

            btnVotar = CrearBoton("Ir a Votar", 700);

            btnVotar.BackColor = Color.DeepPink;

            btnVotar.Click += BtnVotar_Click;

            // ================= BOTON SALIR =================

            btnSalir = CrearBoton("Cerrar Sesión", 870);

            btnSalir.BackColor = Color.PaleVioletRed;

            btnSalir.Click += BtnSalir_Click;

            // ================= AGREGAR BOTONES =================

            panelButtons.Controls.Add(btnAgregar);

            panelButtons.Controls.Add(btnEditar);

            panelButtons.Controls.Add(btnEliminar);

            panelButtons.Controls.Add(btnRefrescar);

            panelButtons.Controls.Add(btnVotar);

            panelButtons.Controls.Add(btnSalir);

            // ================= DATAGRIDVIEW =================

            dgvPlanchas = new DataGridView();

            dgvPlanchas.Dock = DockStyle.Fill;

            dgvPlanchas.BackgroundColor = Color.White;

            dgvPlanchas.BorderStyle = BorderStyle.None;

            dgvPlanchas.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvPlanchas.RowTemplate.Height = 35;

            dgvPlanchas.Font = new Font("Segoe UI", 10);

            dgvPlanchas.EnableHeadersVisualStyles = false;

            dgvPlanchas.ColumnHeadersDefaultCellStyle.BackColor =
                Color.HotPink;

            dgvPlanchas.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvPlanchas.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 11, FontStyle.Bold);

            dgvPlanchas.DefaultCellStyle.SelectionBackColor =
                Color.LightPink;

            dgvPlanchas.DefaultCellStyle.SelectionForeColor =
                Color.Black;

            this.Controls.Add(dgvPlanchas);

            // ================= FOOTER =================

            panelFooter = new Panel();

            panelFooter.Dock = DockStyle.Bottom;

            panelFooter.Height = 45;

            panelFooter.BackColor = Color.HotPink;

            this.Controls.Add(panelFooter);

            // ================= LABEL FOOTER =================

            lblFooter = new Label();

            lblFooter.Text = "Sistema Electoral 2025 ©";

            lblFooter.Dock = DockStyle.Fill;

            lblFooter.TextAlign = ContentAlignment.MiddleCenter;

            lblFooter.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            lblFooter.ForeColor = Color.White;

            panelFooter.Controls.Add(lblFooter);
        }

        private Button CrearBoton(string texto, int x)
        {
            Button btn = new Button();

            btn.Text = texto;

            btn.Width = 150;

            btn.Height = 45;

            btn.Location = new Point(x, 18);

            btn.BackColor = Color.HotPink;

            btn.ForeColor = Color.White;

            btn.FlatStyle = FlatStyle.Flat;

            btn.FlatAppearance.BorderSize = 0;

            btn.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            return btn;
        }

        private void CargarPlanchas()
        {
            using (SistemaElectoralEntities db =
                new SistemaElectoralEntities())
            {
                var lista = db.Planchas
                    .Select(p => new
                    {
                        p.Id,
                        p.Nombre,
                        p.Logo,
                        p.Presidente,
                        p.Vicepresidente,
                        p.Tesorero
                    })
                    .ToList();

                dgvPlanchas.DataSource = lista;
            }
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            AgregarPlanchaForm frm =
                new AgregarPlanchaForm();

            frm.ShowDialog();

            CargarPlanchas();
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvPlanchas.CurrentRow == null)
            {
                MessageBox.Show("Selecciona una plancha");
                return;
            }

            int id = Convert.ToInt32(
                dgvPlanchas.CurrentRow.Cells["Id"].Value);

            EditarPlanchaForm frm =
                new EditarPlanchaForm(id);

            frm.ShowDialog();

            CargarPlanchas();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPlanchas.CurrentRow == null)
            {
                MessageBox.Show("Selecciona una plancha");
                return;
            }

            DialogResult resultado =
                MessageBox.Show(
                    "¿Seguro que deseas eliminar esta plancha?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                int id = Convert.ToInt32(
                    dgvPlanchas.CurrentRow.Cells["Id"].Value);

                using (SistemaElectoralEntities db =
                    new SistemaElectoralEntities())
                {
                    var plancha = db.Planchas.Find(id);

                    db.Planchas.Remove(plancha);

                    db.SaveChanges();
                }

                MessageBox.Show("Plancha eliminada");

                CargarPlanchas();
            }
        }

        private void BtnRefrescar_Click(object sender, EventArgs e)
        {
            CargarPlanchas();

            MessageBox.Show(
                "Datos actualizados correctamente 💖");
        }

        private void BtnVotar_Click(object sender, EventArgs e)
        {
            this.Hide();

            VotacionForm voto =
       new VotacionForm(usuarioActual);

            voto.ShowDialog();

            this.Show();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            DialogResult resultado =
                MessageBox.Show(
                    "¿Deseas cerrar sesión?",
                    "Cerrar Sesión",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                this.Hide();

                LoginForm login = new LoginForm();

                login.Show();

                this.Close();
            }
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

        }
    }
}