using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TFG
{
    public partial class BuscarPartituras : Form
    {
        public int num = 0;
        private MySqlCommand Comando;
        private MySqlDataReader Leer;
        public static String CadenaConexion = "server=remotemysql.com;user id=jGrZrsWHux;password=mPl4x3OA5S;database=jGrZrsWHux";

        public static MySqlConnection Conexion = new MySqlConnection(CadenaConexion);

        public BuscarPartituras()
        {
            InitializeComponent();
        }
        public BuscarPartituras(int num1)
        {
            InitializeComponent();
            num= num1;

        }

        private void BuscarPartituras_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            String query = "SELECT Nombre, Autor, Categoria,Id, URL FROM directorio ORDER BY Categoria";
           

            Comando = new MySqlCommand(query, Conexion);
            Conexion.Open();

            Leer = Comando.ExecuteReader();

            while (Leer.Read())
            {
                dataGridView1.Rows.Add(Leer.GetValue(0), Leer.GetValue(1), Leer.GetValue(2),Leer.GetValue(3),Leer.GetValue(4));
            }
            Conexion.Close();

            if (num == 1)
            {
                dataGridView1.Columns[5].Visible = false;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.Visible = true;
            panel2.Visible = true;
            textBox2.Visible = true;
            label11.Visible = true;
            dataGridView2.Rows.Clear();
            String query = "SELECT Instrumento,Id FROM partituras WHERE Carpeta='" + dataGridView1.CurrentRow.Cells[3].Value + "'";



            Comando = new MySqlCommand(query, Conexion);
            Conexion.Open();

            Leer = Comando.ExecuteReader();

            while (Leer.Read())
            {
                dataGridView2.Rows.Add(Leer.GetValue(0), Leer.GetValue(1));
            }
            Conexion.Close();

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Column7")
            {
               


                    String mensaje = "¿Desea eliminar la partitura" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + " de " + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "?";
                    MessageBoxButtons button = MessageBoxButtons.YesNo;
                    DialogResult resultado = MessageBox.Show(mensaje, "", button);

                    if (resultado == System.Windows.Forms.DialogResult.Yes)
                    {
                      
                        String query2 = "DELETE FROM partituras WHERE Carpeta="+ dataGridView1.CurrentRow.Cells[3].Value + "";
                        String query3 = "DELETE FROM directorio WHERE Nombre='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "' && Autor='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "'";
                        Comando = new MySqlCommand(query2, Conexion);
                        Conexion.Open();
                        int a = Comando.ExecuteNonQuery();

                        Conexion.Close();
                        Comando = new MySqlCommand(query3, Conexion);
                        Conexion.Open();
                        int b = Comando.ExecuteNonQuery();

                        Conexion.Close();
                        MessageBox.Show("Se ha elimiado la partitura");
                        BuscarPartituras_Load(sender, e);
                        dataGridView2.Rows.Clear();
                    
                }
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


            String query = "SELECT Direccion FROM partituras WHERE Id='" + dataGridView2.CurrentRow.Cells[1].Value + "'";
            


            Comando = new MySqlCommand(query, Conexion);
            Conexion.Open();

            Leer = Comando.ExecuteReader();

            while (Leer.Read())
            {
                axAcroPDF1.src = Leer.GetValue(0).ToString();
                
            }
            Conexion.Close();


            axAcroPDF1.Visible = true;
           
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String query = "SELECT Partitura FROM partituras WHERE Id='" + dataGridView2.CurrentRow.Cells[1].Value + "'";
            Comando= new MySqlCommand(query, Conexion);
            Conexion.Open();
            Leer = Comando.ExecuteReader();

        
            while (Leer.Read())
            {


                byte[] byt = (byte[])Leer.GetValue(0);
                String blobstring=Encoding.UTF8.GetString(byt);
                byte[] blob = new byte[blobstring.Length];
                blob=Convert.FromBase64String(blobstring);
                System.IO.File.WriteAllBytes(Directory.GetCurrentDirectory()+"\\tfg.pdf",blob);
                String CurrentDirectory = Directory.GetCurrentDirectory();
                axAcroPDF1.src = Directory.GetCurrentDirectory() + "\\tfg.pdf";
                axAcroPDF1.Visible = true;
               

            }
            Conexion.Close();
            panel3.Visible = true;

          
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            String query = "SELECT Nombre, Autor, Categoria,Id, URL FROM directorio WHERE Nombre LIKE '%" + textBox1.Text + "%' OR Autor LIKE '%" + textBox1.Text + "%'";
            dataGridView1.Rows.Clear();

            Comando = new MySqlCommand(query, Conexion);
            Conexion.Open();

            Leer = Comando.ExecuteReader();

            while (Leer.Read())
            {
                dataGridView1.Rows.Add(Leer.GetValue(0), Leer.GetValue(1), Leer.GetValue(2), Leer.GetValue(3), Leer.GetValue(4));
            }
            Conexion.Close();


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            String query = "SELECT Instrumento URL FROM partituras WHERE Instrumento LIKE '%" + textBox2.Text + "%' AND Carpeta="+dataGridView1.CurrentRow.Cells[3].Value;
            dataGridView2.Rows.Clear();
            Comando = new MySqlCommand(query, Conexion);
            Conexion.Open();

            Leer = Comando.ExecuteReader();

            while (Leer.Read())
            {
                dataGridView2.Rows.Add(Leer.GetValue(0));
            }
            Conexion.Close();
        }
    }
}
