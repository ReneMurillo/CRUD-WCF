using ClienteWS.Clases;
using ClienteWS.WSPersonas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteWS
{
    public partial class Form1 : Form
    {
        int Id;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Personas persona = new Personas();
            persona.Nombre = txtNombre.Text;
            persona.Apellido = txtApellido.Text;
            persona.Edad = Convert.ToInt32(txtEdad.Text);
            persona.DocumentoIdentificacion = txtIdentificacion.Text;

            try
            {
                using (WSPersonasClient cliente = new WSPersonasClient())
                {
                    if(btnBuscar.Text == "Registrar")
                    {
                        var result = cliente.NuevaPersona(persona);

                        if (result != 0)
                        {
                            MessageBox.Show("La persona ha sido guardada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LlenarDataGrid();
                            LimpiarTextBox();
                        }
                        else
                        {
                            MessageBox.Show("Ha ocurrido un error inesperado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        persona.Id = Id;
                        var result = cliente.EditarPersona(persona);

                        if (result != 0)
                        {
                            MessageBox.Show("La persona ha sido modificada correctamente", "Modificada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LlenarDataGrid();
                            LimpiarTextBox();
                            btnBuscar.Text = "Registrar";
                        }
                        else
                        {
                            MessageBox.Show("Ha ocurrido un error inesperado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }                   
                }                
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LlenarDataGrid();
        }

        void LlenarDataGrid()
        {
            List<PersonaView> personas = new List<PersonaView>();

            using (WSPersonasClient cliente = new WSPersonas.WSPersonasClient())
            {
                var lista = cliente.listarPersonas();

                foreach (var item in lista)
                {
                    personas.Add(new PersonaView
                    {
                        Id = item.Id,
                        Nombre = item.Nombre,
                        Apellido = item.Apellido,
                        Edad = item.Edad,
                        DocumentoIdentificacion = item.DocumentoIdentificacion
                    });
                }

                dataGridView1.DataSource = personas;
            }
        }

        void LimpiarTextBox()
        {
            txtNombre.Text = txtApellido.Text = txtEdad.Text = txtIdentificacion.Text = "";
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            txtNombre.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtApellido.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtEdad.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtIdentificacion.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

            btnBuscar.Text = "Guardar cambios";

            btnEliminar.Visible = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult pregunta;
            pregunta = MessageBox.Show("¿Está seguro de eliminar este registro", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(pregunta == DialogResult.Yes)
            {
                using (WSPersonasClient cliente = new WSPersonasClient())
                {
                    var result = cliente.EliminarPersona(Id);

                    if(result != 0)
                    {
                        MessageBox.Show("Persona eliminada correctamente", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LlenarDataGrid();
                        LimpiarTextBox();
                        Id = 0;
                        btnEliminar.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Algo salió mal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
