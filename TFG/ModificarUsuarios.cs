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
    
    public partial class ModificarUsuarios : Form
    {
    
        private MySqlCommand Comando;
        private MySqlDataReader Leer;
        public static String CadenaConexion = "server=remotemysql.com;user id=jGrZrsWHux;password=mPl4x3OA5S;database=jGrZrsWHux";
        public static MySqlConnection Conexion = new MySqlConnection(CadenaConexion);
        String tipo;
        String instrumento="";
        String usuario;
        String cuerda;
      private Componente componente = new Componente();
        public ModificarUsuarios()
        {
            InitializeComponent();
        }

        public ModificarUsuarios(int id,String nombre, String apellidos, String direccion, String usuario, String pass, String tipo, String cuerda, String Inatrumento)
        {


            InitializeComponent();
            componente.Id = id;
            componente.Nombre = nombre;
            componente.Apellidos = apellidos;
            componente.Direccion = direccion;
            componente.Username = usuario;
            componente.Contraseña = pass;
            componente.Tipo = tipo;
            componente.Cuerda = cuerda;
            componente.Instrumento = Inatrumento;
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void ModificarUsuarios_Load(object sender, EventArgs e)
        {
            textBox1.Text=componente.Nombre;
            textBox2.Text=componente.Apellidos;
            textBox3.Text=componente.Direccion;
            textBox4.Text=componente.Username;
            textBox5.Text = componente.Contraseña;

            if (componente.Tipo.Equals("Músico"))
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox2.Checked = true;
            }
           if(componente.Cuerda.Equals("Viento Madera"))
            {
                checkBox4.Checked = true;
            }else if(componente.Cuerda.Equals("Viento Metal"))
            {
                checkBox3.Checked = true;
            }else if (componente.Cuerda.Equals("Percusión"))
            {
                checkBox5.Checked = true;
            }



           comboBox2.Text=componente.Instrumento;
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
               
                checkBox2.Checked = false;
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
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
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

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
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
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
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

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            instrumento = comboBox2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1 != null && textBox2 != null && textBox3 != null && textBox4 != null && textBox5 != null && tipo.Length > 0 && instrumento.Length > 0 && cuerda.Length > 0)
            {


                String comando = "UPDATE componentes SET Nombre= '" + textBox1.Text + "', Apellidos='"+textBox2.Text+"', Direccion='"+textBox3.Text+"', Usuario='"+textBox4.Text+"', Contraseña='"+textBox5.Text+"', Tipo='"+tipo+"', Cuerda='"+cuerda+"', Instrumento='"+comboBox2.Text+"' WHERE Usuario='"+textBox4.Text+"'";
                Comando = new MySqlCommand(comando, Conexion);
                Conexion.Open();

                int cant;
                cant = Comando.ExecuteNonQuery();
                MessageBox.Show("Usuario Modificado");
                Conexion.Close();
                this.Close();
                ModificarUsuarios_Load(sender, e);

            }
            else
            {
                MessageBox.Show("No ha introducido bien los datos.");
            }
        }
    }
}
