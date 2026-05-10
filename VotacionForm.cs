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
using System.IO;

namespace SistemaElectoral
{
    public partial class VotacionForm : Form
    {
        private Usuarios usuarioActual;

        FlowLayoutPanel panel;

        public VotacionForm(Usuarios usuario)
        {
            InitializeComponent();
            usuarioActual = usuario;

            CrearDiseño();
        }

        private void CrearDiseño()
        {
            this.Text = "Sistema Electoral 2025";
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(248, 242, 246);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.AutoScroll = true;

            // ================= FONDO SUPERIOR =================

            Panel top = new Panel();
            top.Size = new Size(1600, 170);
            top.Location = new Point(0, 0);
            top.BackColor = Color.White;

            Label titulo = new Label();
            titulo.Text = "🗳️  SELECCIONA TU PLANCHA";
            titulo.Font = new Font("Segoe UI", 32, FontStyle.Bold);
            titulo.ForeColor = Color.HotPink;
            titulo.AutoSize = true;
            titulo.Location = new Point(350, 35);

            Label sub = new Label();
            sub.Text = "Elige la plancha de tu preferencia para votar";
            sub.Font = new Font("Segoe UI", 14);
            sub.ForeColor = Color.Gray;
            sub.AutoSize = true;
            sub.Location = new Point(520, 100);

            top.Controls.Add(titulo);
            top.Controls.Add(sub);

            this.Controls.Add(top);

            Button btnNulo = new Button();

            btnNulo.Text = "❌ VOTO NULO";

            btnNulo.Size = new Size(250, 55);

            btnNulo.Location = new Point(620, 900);

            btnNulo.BackColor = Color.DarkRed;

            btnNulo.ForeColor = Color.White;

            btnNulo.FlatStyle = FlatStyle.Flat;

            btnNulo.FlatAppearance.BorderSize = 0;

            btnNulo.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            btnNulo.Cursor = Cursors.Hand;

            btnNulo.Click += BtnNulo_Click;

            this.Controls.Add(btnNulo);



            // ================= TARJETAS =================

            CrearTarjeta(
                "PRM",
                Color.FromArgb(0, 102, 255),
                Color.FromArgb(230, 240, 255),
                new Point(90, 210)
            );

            CrearTarjeta(
                "PLD",
                Color.FromArgb(130, 30, 200),
                Color.FromArgb(245, 230, 255),
                new Point(540, 210)
            );

            CrearTarjeta(
                "Fuerza del Pueblo",
                Color.FromArgb(0, 160, 70),
                Color.FromArgb(230, 255, 235),
                new Point(990,210)
            );

  

            

            // ================= FOOTER =================

            Panel footer = new Panel();
            footer.Size = new Size(1520, 55);
            footer.Location = new Point(0, 930);
            footer.BackColor = Color.HotPink;

            Label lblFooter = new Label();
            lblFooter.Text = "Sistema Electoral - Elecciones 2025";
            lblFooter.ForeColor = Color.White;
            lblFooter.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblFooter.AutoSize = true;
            lblFooter.Location = new Point(620, 15);

            footer.Controls.Add(lblFooter);

            // ================= PANEL INFORMACION =================

            Panel info = new Panel();
            info.Size = new Size(1500, 90);

            info.Location = new Point(20, 830);

            info.BackColor = Color.White;

            info.BorderStyle = BorderStyle.FixedSingle;



            // ===== ITEM 1 =====

            Label lblInfo1 = new Label();

            lblInfo1.Text = "🛡️  TU VOTO ES SECRETO\nNadie sabrá por quién votaste.";

            lblInfo1.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            lblInfo1.ForeColor = Color.HotPink;

            lblInfo1.AutoSize = true;

            lblInfo1.Location = new Point(40, 20);



            // ===== ITEM 2 =====

            Label lblInfo2 = new Label();

            lblInfo2.Text = "✔️  VOTA CON CONFIANZA\nTu voto ayuda a construir el futuro.";

            lblInfo2.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            lblInfo2.ForeColor = Color.HotPink;

            lblInfo2.AutoSize = true;

            lblInfo2.Location = new Point(390, 20);



            // ===== ITEM 3 =====

            Label lblInfo3 = new Label();

            lblInfo3.Text = "🔒  SEGURO Y CONFIABLE\nSistema protegido y transparente.";

            lblInfo3.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            lblInfo3.ForeColor = Color.HotPink;

            lblInfo3.AutoSize = true;

            lblInfo3.Location = new Point(760, 20);



            // ===== ITEM 4 =====

            Label lblInfo4 = new Label();

            lblInfo4.Text = "👤  UN VOTO, UNA VOZ\nTu decisión hace la diferencia.";

            lblInfo4.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            lblInfo4.ForeColor = Color.HotPink;

            lblInfo4.AutoSize = true;

            lblInfo4.Location = new Point(1130, 20);



            info.Controls.Add(lblInfo1);

            info.Controls.Add(lblInfo2);

            info.Controls.Add(lblInfo3);

            info.Controls.Add(lblInfo4);



            this.Controls.Add(info);

            this.Controls.Add(footer);
        }

        private void CrearTarjeta(
            string partido,
            Color colorPrincipal,
            Color colorFondo,
            Point posicion)
        {
            using (var db = new SistemaElectoralEntities())
            {
                var plancha = db.Planchas.FirstOrDefault(p => p.Nombre == partido);

                if (plancha == null)
                    return;

                var candidatos = db.Candidatos
                    .Where(c => c.PlanchaId == plancha.Id)
                    .ToList();

                // ================= TARJETA =================

                Panel card = new Panel();
                card.Size = new Size(410, 570);
                card.Location = posicion;
                card.BackColor = Color.White;
                card.BorderStyle = BorderStyle.FixedSingle;

                // ================= HEADER =================

                Panel header = new Panel();
                header.Size = new Size(410, 165);
                header.Location = new Point(0, 0);
                header.BackColor = colorPrincipal;

                PictureBox logo = new PictureBox();
                logo.Size = new Size(95, 95);
                logo.Location = new Point(20, 25);
                logo.SizeMode = PictureBoxSizeMode.StretchImage;

                string rutaLogo = Path.Combine(Application.StartupPath, plancha.Logo);

                if (File.Exists(rutaLogo))
                {
                    logo.Image = Image.FromFile(rutaLogo);
                }

                Label lblPartido = new Label();

                lblPartido.Text = partido;
                lblPartido.Font = new Font("Segoe UI", 20, FontStyle.Bold);
                lblPartido.ForeColor = Color.White;
                lblPartido.AutoSize = true;
                if (partido == "PRM")
                    lblPartido.Location = new Point(155, 35);

                else if (partido == "PLD")
                    lblPartido.Location = new Point(165, 35);

                else
                    lblPartido.Location = new Point(120, 35);

                Label lblNombrePartido = new Label();

                lblNombrePartido.Text = ObtenerNombrePartido(partido);
                lblNombrePartido.Font = new Font("Segoe UI", 9);
                lblNombrePartido.ForeColor = Color.WhiteSmoke;
                lblNombrePartido.AutoSize = true;
                lblNombrePartido.Location = new Point(165, 85);

                header.Controls.Add(logo);

                header.Controls.Add(lblPartido);

                header.Controls.Add(lblNombrePartido);
                card.Controls.Add(header);

                // ================= CANDIDATOS =================

                int x = 20;

                foreach (var c in candidatos)
                {
                    PictureBox foto = new PictureBox();
                    foto.Size = new Size(110, 135);
                    foto.Location = new Point(x, 220);
                    foto.SizeMode = PictureBoxSizeMode.StretchImage;
                    foto.BorderStyle = BorderStyle.FixedSingle;

                    string rutaFoto = Path.Combine( AppDomain.CurrentDomain.BaseDirectory,c.Foto );

                    if (File.Exists(rutaFoto))
                    {
                        foto.Image = Image.FromFile(rutaFoto);
                    }

                    Label cargo = new Label();
                    cargo.Text = c.Cargo.ToUpper();
                    cargo.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    cargo.ForeColor = colorPrincipal;
                    cargo.AutoSize = true;
                    cargo.Location = new Point(x + 5, 365);

                    Label nombre = new Label();
                    nombre.Text = c.Nombre;
                    nombre.Font = new Font("Segoe UI", 10);
                    nombre.AutoSize = true;
                    nombre.Location = new Point(x, 392);

                    card.Controls.Add(foto);
                    card.Controls.Add(cargo);
                    card.Controls.Add(nombre);

                    x += 125;
                }

                // ================= BOTON =================

                Button btnVotar = new Button();
                btnVotar.Text = "🗳️  VOTAR POR " + partido;
                btnVotar.Size = new Size(340, 58);
                btnVotar.Location = new Point(35, 475);
                btnVotar.BackColor = colorPrincipal;
                btnVotar.ForeColor = Color.White;
                btnVotar.FlatStyle = FlatStyle.Flat;
                btnVotar.FlatAppearance.BorderSize = 0;
                btnVotar.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                btnVotar.Cursor = Cursors.Hand;

                btnVotar.Tag = plancha.Id;
                btnVotar.Click += BtnPlancha_Click;



                card.Controls.Add(btnVotar);

                this.Controls.Add(card);

            }
        }

        private void BtnNulo_Click(object sender, EventArgs e)
        {
            using (var db = new SistemaElectoralEntities())
            {
                var usuario = db.Usuarios.Find(usuarioActual.Id);

                if (usuario.YaVoto == true)
                {
                    MessageBox.Show(
                        "Ya realizaste tu voto.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                Votos voto = new Votos()
                {
                    UsuarioId = usuario.Id,
                    PlanchaId = null,
                    EsNulo = true
                };

                db.Votos.Add(voto);

                usuario.YaVoto = true;

                db.SaveChanges();

                MessageBox.Show(
                    "Tu voto nulo fue registrado.",
                    "Información",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Close();
            }
       
    }
        private string ObtenerNombrePartido(string siglas)
        {
            if (siglas == "PRM")
                return "PARTIDO REVOLUCIONARIO MODERNO";

            if (siglas == "PLD")
                return "PARTIDO DE LA LIBERACIÓN DOMINICANA";

            return "FUERZA DEL PUEBLO";
        }

        private void BtnPlancha_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int planchaId = (int)btn.Tag;

            using (var db = new SistemaElectoralEntities())
            {
                var usuario = db.Usuarios.Find(usuarioActual.Id);

                if (usuario.YaVoto == true)
                {
                    MessageBox.Show(
                        "Este usuario ya realizó su voto.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                DialogResult confirmar = MessageBox.Show(
                    "¿Deseas confirmar tu voto?\n\nEsta acción no se puede deshacer.",
                    "Confirmar voto",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmar == DialogResult.No)
                    return;

                Votos voto = new Votos()
                {
                    UsuarioId = usuario.Id,
                    PlanchaId = planchaId,
                    EsNulo = false
                };

                db.Votos.Add(voto);

                usuario.YaVoto = true;

                db.SaveChanges();

                MessageBox.Show(
                    "Tu voto fue registrado correctamente 🗳️",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Close();
            }
        }

private void VotacionForm_Load(object sender, EventArgs e)
        {

        }
    }
}
