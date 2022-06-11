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
    public partial class NuevoComp : Form
    {

        private MySqlCommand Comando;
        private MySqlDataReader Leer;
        public static String CadenaConexion = "server=remotemysql.com;user id=jGrZrsWHux;password=mPl4x3OA5S;database=jGrZrsWHux";
        public static MySqlConnection Conexion = new MySqlConnection(CadenaConexion);

        // VARIABLES.
        String usuario = "";
        String tipo="";
        String cuerda = "";
        String instrumento="";
        public NuevoComp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text.Length>0 && textBox2.Text.Length > 0 && textBox3.Text.Length > 0 && textBox4.Text.Length > 0 && textBox5.Text.Length > 0 && tipo.Length > 0 && instrumento.Length>0 && cuerda.Length > 0)
            {
                

                String query = "SELECT * FROM componentes WHERE Usuario= '" + textBox4.Text + "'";

                Comando = new MySqlCommand(query, Conexion);
                Conexion.Open();

                Leer = Comando.ExecuteReader();

                while (Leer.Read())
                {
                    usuario = Leer.GetValue(2).ToString();
                }
                Conexion.Close();

                if (usuario.Length > 0 ) { 
                
                MessageBox.Show("El nombre de usuario ya existe");
                
                
                }
                else
                {


                    String consulta = "INSERT INTO componentes (Nombre, Apellidos, Direccion, Usuario, Contraseña, Tipo, Instrumento, Cuerda) " +
                    "VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + tipo + "','" + instrumento + "','" + cuerda + "')";

                    Comando = new MySqlCommand(consulta, Conexion);
                    Conexion.Open();
                    int a = Comando.ExecuteNonQuery();
                    MessageBox.Show("Usuario Creado");
                    Conexion.Close();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("No ha introducido bien los datos.");
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            comboBox2.Enabled = true;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            comboBox2.Items.Clear();
            cuerda = "Viento Metal";
            comboBox2.Items.Clear();
            comboBox2.Items.Add("Trompeta 1");
            comboBox2.Items.Add("Trompeta 2");
            comboBox2.Items.Add("Trompeta 3");
            comboBox2.Items.Add("Fliscorno");
            comboBox2.Items.Add("Trompa");
            comboBox2.Items.Add("Bombardino 1");
            comboBox2.Items.Add("Bombardino 2");
            comboBox2.Items.Add("Trombón  1");
            comboBox2.Items.Add("Trombón  2");
            comboBox2.Items.Add("Trombón  3");
            comboBox2.Items.Add("Tuba");
            if (checkBox3.Checked == false)
            {
                comboBox2.Items.Clear();
                comboBox2.Enabled = false;

            }


        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            comboBox2.Enabled = true;
            checkBox3.Checked = false;
            checkBox5.Checked = false;
            comboBox2.Items.Clear();
            cuerda = "Viento Madera";
            comboBox2.Items.Add("Clarinete pral");
            comboBox2.Items.Add("Clarinete 1");
            comboBox2.Items.Add("Clarinete 2");
            comboBox2.Items.Add("Clarinete 3");
            comboBox2.Items.Add("Clarinete Bajo");
            comboBox2.Items.Add("Saxofón Alto 1");
            comboBox2.Items.Add("Saxofón Alto 2");
            comboBox2.Items.Add("Saxofón Tenor");
            comboBox2.Items.Add("Saxofón Barítono");
            comboBox2.Items.Add("Flauta Travesera 1");
            comboBox2.Items.Add("Flauta Travesera 2");
            comboBox2.Items.Add("Oboe");
            comboBox2.Items.Add("Fagot");
            if (checkBox4.Checked == false)
            {
                comboBox2.Items.Clear();
                comboBox2.Enabled = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            comboBox2.Enabled = true;

            checkBox3.Checked = false;
            checkBox4.Checked = false;
            cuerda = "Percusión";
            comboBox2.Items.Clear();
            comboBox2.Items.Add("Caja");
            comboBox2.Items.Add("Bombo");
            comboBox2.Items.Add("Platos");
            comboBox2.Items.Add("Timbales");
            comboBox2.Items.Add("Batería");
            comboBox2.Items.Add("Xilófono");
            if (checkBox5.Checked == false)
            {
                comboBox2.Items.Clear();
                comboBox2.Enabled = false;

            }


        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                label4.Enabled = true;
                label5.Enabled = true;
                tipo = "Encargado";
            checkBox1.Checked = false;
            }
            else
            {
                tipo = "";
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                label4.Enabled = false;
                label5.Enabled = false;
                usuario = "";
                checkBox1.Checked = false;

            }


        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            instrumento = comboBox2.Text;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {


                tipo = "Músico";
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                label4.Enabled = true;
                label5.Enabled = true;
                usuario = "";
                checkBox2.Checked = false;
            }
            else
            {
                tipo = "";
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                label4.Enabled = false;
                label5.Enabled = false;
                usuario = "";
                checkBox2.Checked = false;

            }
         
          
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Space))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Space))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
    }
}
