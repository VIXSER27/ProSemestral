using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Newtonsoft.Json;
using Laboratorio_4_DSIV;

namespace ConsumirLogin
{
    public partial class FrmLogin : Form
    {
        private readonly HttpClient cliente = new HttpClient();

        public FrmLogin()
        {
            InitializeComponent();
            cliente.BaseAddress = new Uri("https://localhost:7201/api/");
            cliente.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var credenciales = new Credenciales
            {
                correo = txtEmail.Text.Trim(),
                contrasena = txtPass.Text.Trim()
            };

            if (string.IsNullOrWhiteSpace(credenciales.correo) ||
                string.IsNullOrWhiteSpace(credenciales.contrasena))
            {
                lblResultado.Text = "Debes llenar todos los campos.";
                lblResultado.Visible = true;
                lblResultado.ForeColor = Color.Red;
                return;
            }

            string json = JsonConvert.SerializeObject(credenciales);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("Usuarios/login", content);
            string jsonResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Usuarios result = JsonConvert.DeserializeObject<Usuarios>(jsonResponse);

                lblResultado.Text = $"Login exitoso de {result.nombre} {result.apellido}";
                lblResultado.Visible = true;
                lblResultado.ForeColor = Color.Green;

                // ==== VALIDAR EL ROL Y REDIRIGIR ====
                if (result.rol != null && result.rol.ToLower() == "admin")
                {
                    Administrar adminForm = new Administrar();  // Usa el nombre correcto del form
                    adminForm.Show();
                    this.Hide();  // Oculta el login
                }

                else
                {
                   Cliente clienteForm = new Cliente();
                    clienteForm.Show();
                    this.Hide();  // Oculta el login
                }
            }
            else
            {
                lblResultado.Text = "Credenciales incorrectas";
                lblResultado.Visible = true;
                lblResultado.ForeColor = Color.Red;
            }
        }

    }
}
