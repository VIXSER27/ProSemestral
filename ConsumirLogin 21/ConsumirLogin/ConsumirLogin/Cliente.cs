using ConsumirLogin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratorio_4_DSIV
{
    public partial class Cliente : Form
    {
        private readonly HttpClient cliente = new HttpClient();
        private List<ItemCarrito> carrito = new List<ItemCarrito>();

        public Cliente ()
        {
            InitializeComponent();

            cliente.BaseAddress = new Uri("https://localhost:7201/api/");
            cliente.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            CargarProductos();
        }

        public void RefrescarCatalogo()
        {
            CargarProductos();
        }

        private async void CargarProductos()
        {
            flowLayoutPanelCatalogo.Controls.Clear();

            try
            {
                var response = await cliente.GetAsync("medicamentos");

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Error al cargar medicamentos desde la API.");
                    return;
                }

                string json = await response.Content.ReadAsStringAsync();
                List<Medicamentos> lista = JsonConvert.DeserializeObject<List<Medicamentos>>(json);

                foreach (var item in lista.Where(m => m.cantidad_disponible > 0))
                {
                    flowLayoutPanelCatalogo.Controls.Add(CrearPanelProducto(item));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error API: " + ex.Message);
            }
        }

        private Panel CrearPanelProducto(Medicamentos m)
        {
            int id = m.id;
            string nombre = m.nombre;
            decimal precio = m.precio_unitario;
            int cantidad = m.cantidad_disponible;
            string imagenPath = m.imagen;

            Panel card = new Panel();
            card.Width = 220;
            card.Height = 310;
            card.Margin = new Padding(10);
            card.BackColor = Color.White;
            card.Padding = new Padding(8);

            card.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle,
                    Color.LightGray, 2, ButtonBorderStyle.Solid,
                    Color.LightGray, 2, ButtonBorderStyle.Solid,
                    Color.LightGray, 2, ButtonBorderStyle.Solid,
                    Color.LightGray, 2, ButtonBorderStyle.Solid);
            };

            card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(245, 245, 245);
            card.MouseLeave += (s, e) => card.BackColor = Color.White;

            // IMAGEN
            PictureBox pic = new PictureBox();
            pic.Width = 200;
            pic.Height = 150;
            pic.Top = 5;
            pic.Left = 5;
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            pic.BackColor = Color.FromArgb(250, 250, 250);

            try
            {
                string rutaImagen = Path.Combine(Application.StartupPath, "Img", imagenPath);

                if (!string.IsNullOrEmpty(imagenPath) && File.Exists(rutaImagen))
                {
                    pic.Image = Image.FromFile(rutaImagen);
                }
                else
                {
                    pic.Image = Image.FromFile(
                        Path.Combine(Application.StartupPath, "Img", "imagen_no_disponible.png")
                    );
                }
            }
            catch
            {
                pic.Image = Image.FromFile(
                    Path.Combine(Application.StartupPath, "Img", "imagen_no_disponible.png")
                );
            }

            card.Controls.Add(pic);

            // NOMBRE
            Label lblNombre = new Label();
            lblNombre.Text = nombre;
            lblNombre.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblNombre.Width = 200;
            lblNombre.Top = 165;
            lblNombre.Left = 5;
            lblNombre.TextAlign = ContentAlignment.MiddleCenter;
            card.Controls.Add(lblNombre);

            // PRECIO
            Label lblPrecio = new Label();
            lblPrecio.Text = $"${precio:F2}";
            lblPrecio.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblPrecio.ForeColor = Color.FromArgb(0, 120, 215);
            lblPrecio.Width = 200;
            lblPrecio.Top = 190;
            lblPrecio.Left = 5;
            lblPrecio.TextAlign = ContentAlignment.MiddleCenter;
            card.Controls.Add(lblPrecio);

            // STOCK
            Label lblStock = new Label();
            lblStock.Text = $"Stock disponible: {cantidad}";
            lblStock.Font = new Font("Segoe UI", 9);
            lblStock.Width = 200;
            lblStock.Top = 210;
            lblStock.Left = 5;
            lblStock.TextAlign = ContentAlignment.MiddleCenter;
            lblStock.ForeColor = Color.Gray;
            card.Controls.Add(lblStock);

            // NUMERIC UP DOWN
            NumericUpDown nudCantidad = new NumericUpDown();
            nudCantidad.Minimum = 1;
            nudCantidad.Maximum = cantidad;
            nudCantidad.Width = 70;
            nudCantidad.Top = 235;
            nudCantidad.Left = 75;
            card.Controls.Add(nudCantidad);

            // BOTÓN AGREGAR
            Button btn = new Button();
            btn.Text = "Agregar";
            btn.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btn.Width = 180;
            btn.Height = 32;
            btn.Top = 270;
            btn.Left = 20;
            btn.BackColor = Color.FromArgb(0, 150, 90);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;

            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(0, 120, 70);
            btn.MouseLeave += (s, e) => btn.BackColor = Color.FromArgb(0, 150, 90);

            btn.Click += (s, e) =>
            {
                carrito.Add(new ItemCarrito()
                {
                    Id = id,
                    Nombre = nombre,
                    Cantidad = (int)nudCantidad.Value,
                    PrecioUnitario = precio
                });

                MessageBox.Show($"{nudCantidad.Value}x {nombre} agregado al carrito.");
            };

            card.Controls.Add(btn);

            return card;
        }

        private void btnCarrito_Click(object sender, EventArgs e)
        {
            Carrito frm = new Carrito(carrito, this);
            frm.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            FrmLogin l = new FrmLogin();
            l.ShowDialog();
        }
    }
}
