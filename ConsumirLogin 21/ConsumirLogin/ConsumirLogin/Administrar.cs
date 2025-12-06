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

namespace Laboratorio_4_DSIV
{
    public partial class Administrar : Form
    {
        private readonly HttpClient cliente = new HttpClient();

        public Administrar()
        {
            InitializeComponent();
            cliente.BaseAddress = new Uri("https://localhost:7201/api/");
            cliente.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            ConfigurarDataGridView();
            CargarInventario();
        }

        private void ConfigurarDataGridView()
        {
            dgvInventario.Columns.Clear();

            dgvInventario.Columns.Add("id", "ID");
            dgvInventario.Columns.Add("nombre", "Nombre");
            dgvInventario.Columns.Add("imagen", "Imagen");
            dgvInventario.Columns.Add("cantidad", "Cantidad Disponible");
            dgvInventario.Columns.Add("precio", "Precio por Unidad");

            dgvInventario.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInventario.ReadOnly = true;
            dgvInventario.AllowUserToAddRows = false;

            dgvInventario.CellClick += dgvInventario_CellClick;
        }

        private async void CargarInventario()
        {
            dgvInventario.Rows.Clear();

            try
            {
                var response = await cliente.GetAsync("medicamentos");
                string jsonResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var lista = JsonConvert.DeserializeObject<List<Medicamentos>>(jsonResponse);
                    foreach (var m in lista)
                    {
                        dgvInventario.Rows.Add(m.id, m.nombre, m.imagen, m.cantidad_disponible, m.precio_unitario);
                    }
                }
                else
                {
                    MessageBox.Show("Error al cargar inventario: " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar inventario: " + ex.Message);
            }
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            var nuevo = new Medicamentos
            {
                nombre = txtNombre.Text,
                imagen = txtImagen.Text,
                cantidad_disponible = (int)nudCantidad.Value,
                precio_unitario = nudPrecio.Value
            };

            string json = JsonConvert.SerializeObject(nuevo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("medicamentos", content);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Medicamento agregado correctamente");
                CargarInventario();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al agregar: " + response.ReasonPhrase);
            }
        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Seleccione un medicamento para modificar.");
                return;
            }

            int id = int.Parse(txtId.Text);
            var actualizado = new Medicamentos
            {
                id = id,
                nombre = txtNombre.Text,
                imagen = txtImagen.Text,
                cantidad_disponible = (int)nudCantidad.Value,
                precio_unitario = nudPrecio.Value
            };

            string json = JsonConvert.SerializeObject(actualizado);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync($"medicamentos/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Medicamento modificado correctamente.");
                CargarInventario();
            }
            else
            {
                MessageBox.Show("Error al modificar: " + response.ReasonPhrase);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Seleccione un medicamento para eliminar.");
                return;
            }

            int id = int.Parse(txtId.Text);

            var response = await cliente.DeleteAsync($"medicamentos/{id}");
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Medicamento eliminado correctamente.");
                CargarInventario();
            }
            else
            {
                MessageBox.Show("Error al eliminar: " + response.ReasonPhrase);
            }
        }

        private async void btnReabastecer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Seleccione un medicamento para reabastecer.");
                return;
            }

            int id = int.Parse(txtId.Text);
            var body = new { extra = (int)nudCantidad.Value };

            string json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Usamos SendAsync con HttpMethod("PATCH")
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"medicamentos/{id}/reabastecer")
            {
                Content = content
            };

            var response = await cliente.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Inventario reabastecido");
                CargarInventario();
            }
            else
            {
                MessageBox.Show("Error al reabastecer: " + response.ReasonPhrase);
            }

        }

        private void dgvInventario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtId.Text = dgvInventario.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtNombre.Text = dgvInventario.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtImagen.Text = dgvInventario.Rows[e.RowIndex].Cells[2].Value.ToString();

                int cantidad = Convert.ToInt32(dgvInventario.Rows[e.RowIndex].Cells[3].Value);
                if (cantidad > nudCantidad.Maximum)
                    nudCantidad.Maximum = cantidad;

                nudCantidad.Value = cantidad;
                nudPrecio.Value = Convert.ToDecimal(dgvInventario.Rows[e.RowIndex].Cells[4].Value);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtImagen.Clear();
            nudCantidad.Value = 0;
            nudPrecio.Value = 0;
        }

        private void btnVolverLogin_Click(object sender, EventArgs e)
        {
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            
        }
    }
}