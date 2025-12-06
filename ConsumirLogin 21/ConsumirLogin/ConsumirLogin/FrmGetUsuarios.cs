using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsumirLogin
{
    public partial class FrmGetUsuarios : Form
    {
        private readonly HttpClient cliente = new HttpClient();
        public FrmGetUsuarios()
        {
            InitializeComponent();

            cliente.BaseAddress = new Uri("https://localhost:7201/api/");
            cliente.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async void FrmGetUsuarios_Load(object sender, EventArgs e)
        {
            var response = await cliente.GetAsync("Usuarios");

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("Error fetching users");
                return;
            }

            string json = await response.Content.ReadAsStringAsync();
            var usuarios = JsonConvert.DeserializeObject<List<Usuarios>>(json);

            dgvUsuarios.DataSource = usuarios;
        }
    }
}
