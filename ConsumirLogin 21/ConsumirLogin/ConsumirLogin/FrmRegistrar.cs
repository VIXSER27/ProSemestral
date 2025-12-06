using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ConsumirLogin
{
    public partial class FrmRegistrar : Form
    {
        private readonly HttpClient cliente = new HttpClient();

        public FrmRegistrar()
        {
            InitializeComponent();

            cliente.BaseAddress = new Uri("https://localhost:7201/api/");
            cliente.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async void btnRegistrar_Click(object sender, EventArgs e)
        {
            // Validar que NO haya campos vacíos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MostrarMensaje("Debes completar todos los campos.", Color.Red);
                return;
            }

            // Crear el objeto usuario
            var nuevoUsuario = new
            {
                nombre = txtNombre.Text.Trim(),
                apellido = txtApellido.Text.Trim(),
                correo = txtEmail.Text.Trim(),
                contrasena = txtPass.Text.Trim(),

                // Rol predeterminado (solo habrá un admin, creado manualmente)
                rol = "user"
            };

            string json = JsonConvert.SerializeObject(nuevoUsuario);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await cliente.PostAsync("usuarios", content);

                if (response.IsSuccessStatusCode)
                {
                    MostrarMensaje("Usuario registrado con éxito.", Color.Green);
                    LimpiarCampos();
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MostrarMensaje("Error al registrar: " + error, Color.Red);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error de conexión: " + ex.Message, Color.Red);
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEmail.Text = "";
            txtPass.Text = "";
        }

        private void MostrarMensaje(string mensaje, Color color)
        {
            lblResultado.Text = mensaje;
            lblResultado.ForeColor = color;
            lblResultado.Visible = true;

            // Centrado automático
            lblResultado.Left = (panel1.Width - lblResultado.Width) / 2;
        }
    }
}
