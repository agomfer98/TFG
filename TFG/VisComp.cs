using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TFG
{
    public partial class VisComp : Form
    {
        Componente componente= new Componente();
        private MySqlCommand Comando;
        private MySqlDataReader Leer;
        public static String CadenaConexion = "server=remotemysql.com;user id=jGrZrsWHux;password=mPl4x3OA5S;database=jGrZrsWHux";
        String budqueda;

        public static MySqlConnection Conexion = new MySqlConnection(CadenaConexion);
        public VisComp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            RellenarTabla();
            tabControl1.SelectedTab = tabPage1;
            button2.Visible = true;
            button3.Visible = true;
            button6.Visible = true;
            textBox2.Visible= true;
        }

        private void RellenarTabla()
        {
            int contador_colores = 0;
            dataGridView1.Visible = true;

            Conexion.Close();
            String query = "SELECT * FROM componentes ";
            Comando = new MySqlCommand(query, Conexion);
            Conexion.Open();

            Leer = Comando.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (Leer.Read())
            {
                contador_colores++;
                dataGridView1.Rows.Add(Leer.GetValue(1), Leer.GetValue(2), Leer.GetValue(4), Leer.GetValue(5), Leer.GetValue(3), Leer.GetValue(6));

            }
            Conexion.Close();
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("En este buscador podemos introducir tanto el nombre, apellidos o instrumento.");

           

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

        private void EliminarComponente()
        {
            try{
                String nombre = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                String apellidos = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                String mensaje = "¿Desea eliminar al componente " + nombre + " " + apellidos + "?";
                MessageBoxButtons button = MessageBoxButtons.YesNo;
                DialogResult resultado = MessageBox.Show(mensaje, "", button);

                if (resultado == System.Windows.Forms.DialogResult.Yes)
                {
                    if (nombre.Length > 0)
                    {
                        String query = "DELETE FROM componentes WHERE Nombre='" + nombre + "'&& Apellidos='" + apellidos + "'";
                        Comando = new MySqlCommand(query, Conexion);
                        Conexion.Open();
                        Comando.ExecuteNonQuery();


                        Conexion.Close();
                        MessageBox.Show("Componente " + nombre + " eliminado");
                    }
                    else
                    {
                        MessageBox.Show("No se ha seleccionado a ningún componente");
                    }
                }
            }catch (NullReferenceException ex)
            {

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pictureBox1.Enabled = true;
            pictureBox2.Enabled = true;
            textBox2.Text = "";
            textBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString() + "   " + dataGridView1.CurrentRow.Cells[1].Value.ToString();
            try
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Eliminar")
                {
                    EliminarComponente();
                    RellenarTabla();
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Modificar")
                {
                    ModificarUsuario();
                    RellenarTabla();
                }
            }
            catch (Exception ex)
            {
               

            }
        }

        private void ModificarUsuario()
        {
            String usuario = dataGridView1.CurrentRow.Cells[5].Value.ToString();

            String consulta = "SELECT Id, Nombre, Apellidos, Direccion, Cuerda, Instrumento, Usuario, Contraseña, Tipo FROM componentes WHERE Usuario='" + usuario + "'";

            String Nombre = "";
            String Apellidos = "";
            String Direccion = "";
            String Cuerda = "";
            String Instrumento = "";
            String User = "";
            String Contraseña = "";
            String Tipo = "";
            int Id = 0;
            Comando = new MySqlCommand(consulta, Conexion);
            Conexion.Open();

            Leer = Comando.ExecuteReader();

            while (Leer.Read())
            {
                try
                {
                    Id = Leer.GetInt32(0);
                    Nombre = Leer.GetString(1);
                    Apellidos = Leer.GetString(2);
                    Direccion = Leer.GetString(3);
                    Cuerda = Leer.GetString(4);
                    Instrumento = Leer.GetString(5);
                    User = Leer.GetString(6);
                    Contraseña = Leer.GetString(7);
                    Tipo = Leer.GetString(8);
                }catch (Exception ex)
                {

                }


            }
            Conexion.Close();

            ModificarUsuarios m = new ModificarUsuarios(Id, Nombre, Apellidos, Direccion, User, Contraseña, Tipo, Cuerda, Instrumento);
            //  MessageBox.Show(Id+Nombre+Apellidos+Direccion+User+Contraseña+Tipo+Cuerda+Instrumento);

            m.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            label1.Visible = true;
            button4.Visible = true;
            label1.Text="Introduzca el Nombre";
            budqueda = "Nombre";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            label1.Visible = true;
            button4.Visible = true;
            label1.Text = "Introduzca el Apellido";
            budqueda = "Apellidos";

        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            label1.Visible = true;
            button4.Visible = true;
            label1.Text = "Introduzca el Instrumento";
            budqueda = "Instrumento";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            String query = "SELECT * FROM componentes WHERE "+budqueda+" LIKE '%"+textBox1.Text+"%'";
            Comando = new MySqlCommand(query, Conexion);
            Conexion.Open();

            Leer = Comando.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (Leer.Read())
            {
                dataGridView1.Rows.Add(Leer.GetValue(1), Leer.GetValue(2), Leer.GetValue(4), Leer.GetValue(5), Leer.GetValue(3), Leer.GetValue(6));

            }
            Conexion.Close();
        }
        public void obtenerusuario()
            {
            
          
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            EliminarComponente();
            RellenarTabla();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ModificarUsuario();
            RellenarTabla();
        }
    }
}
