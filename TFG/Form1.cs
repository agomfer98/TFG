using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;


namespace TFG
{
    public partial class Form1 : Form
    {

    
        private MySqlCommand Comando;
        private MySqlDataReader Leer;
        public static String CadenaConexion = "server=remotemysql.com;user id=jGrZrsWHux;password=mPl4x3OA5S;database=jGrZrsWHux";

        public static MySqlConnection Conexion = new MySqlConnection(CadenaConexion);
        public Form1()
        {

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SeleccionarUsuario();
        }

        private void SeleccionarUsuario()
        {
            Componente usuario = new Componente();
            String query = "SELECT * FROM componentes WHERE Usuario= '" + textBox1.Text + "'";

            Comando = new MySqlCommand(query, Conexion);
            Conexion.Open();
            Leer = Comando.ExecuteReader();



            while (Leer.Read())
            {

                usuario.Id = Leer.GetInt32(0);
                usuario.Nombre = Leer.GetValue(1).ToString();
                usuario.Apellidos = Leer.GetValue(2).ToString();
                usuario.Direccion = Leer.GetValue(3).ToString();
                usuario.Username = Leer.GetValue(6).ToString();
                usuario.Contraseña = Leer.GetValue(7).ToString();
                usuario.Cuerda = Leer.GetValue(4).ToString();
                usuario.Instrumento = Leer.GetValue(5).ToString();
                usuario.Tipo = Leer.GetValue(8).ToString();

            }
            ComprobarUsuario(usuario);


            Conexion.Close();
        }

        private void ComprobarUsuario(Componente usuario)
        {
            if (usuario.Username!=null)
            {
                if (usuario.Contraseña != null)
                {
                    if (usuario.Contraseña.Equals(textBox2.Text.ToString()))
                    {

                        if (usuario.Tipo.Equals("Encargado"))
                        {
                            Inicio i = new Inicio();
                            i.ShowDialog();
                            label3.Text = "";
                        }
                        if (usuario.Tipo.Equals("Músico"))
                        {
                            BuscarPartituras b = new BuscarPartituras(1);
                          
                            b.ShowDialog();
                            label3.Text = "";
                        }

                    }
                    else
                    {
                        label3.Text = "Contraseña incorrecta";
                    }
                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show(" No hay ningún Componente con ese usuario.");

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            String query = "SELECT * FROM componentes";

            Comando = new MySqlCommand(query, Conexion);
            Conexion.Open();

            Leer = Comando.ExecuteReader();

         if(Leer == null)
            {
                MessageBox.Show("No hay ningún usuario creado." +
                    "Para poder acceder cree un usuario Encargado. " +
                    "De esta manera podrá obtener acceso a todas las funciones");
                Conexion.Close();
                NuevoComp n = new NuevoComp();
                n.ShowDialog();
               
            }
            Conexion.Close();

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
           if(e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                SeleccionarUsuario();
            }
        }
    }
}
