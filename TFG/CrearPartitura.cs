using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Specialized;
using MySqlConnector;
using System.IO;

namespace TFG
{
    public partial class CrearPartitura : Form
    {

        String guion;
        String Autor;
        String Partitura;
        String[] Clarinetes = new String[5];
        String[] Saxo = new String[5];
        String Categoria;
        String sAttr;
        String url_carpeta;
        private MySqlCommand Comando;
        private MySqlDataReader Leer;
        public static String CadenaConexion = "server=remotemysql.com;user id=jGrZrsWHux;password=mPl4x3OA5S;database=jGrZrsWHux";

        public static MySqlConnection Conexion = new MySqlConnection(CadenaConexion);
        public CrearPartitura()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                Categoria = "Marchas";
            groupBox2.Visible = true;
            groupBox1.Enabled = false;
            groupBox2.Enabled = true;
            buttongui.Visible = true;
            groupBox9.Enabled = true;


            label2.Text = "Nombre de la Marcha";


        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                Categoria = "Pasodobles";
            groupBox2.Visible = true;
            groupBox1.Enabled = false;
            groupBox2.Enabled = true;
            buttongui.Visible = true;
            groupBox9.Enabled = true;

            label2.Text = "Nombre del Paodoble ";

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
                Categoria = "Obras";
            groupBox2.Visible = true;
            groupBox1.Enabled = false;
            groupBox2.Enabled = true;
            buttongui.Visible = true;
            groupBox9.Enabled = true;

            label2.Text = "Nombre de la Obra";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            button3.Visible = true;
            button4.Visible = true;
            groupBox8.Visible = true;


            if (textBox4.Text.Length > 0 && textBox5.Text.Length > 0) {
                Partitura = textBox5.Text;
                Autor = textBox4.Text;

                String query = "SELECT * FROM directorio WHERE Nombre='" + Partitura + "' AND Autor='" + Autor + "'";

                Comando = new MySqlCommand(query, Conexion);
                Conexion.Open();


                Leer = Comando.ExecuteReader();
                int id = -1;
                while (Leer.Read())
                {
                    id = Leer.GetInt32(0);
                }
                Conexion.Close();
                if (id == -1)
                {
                    crearcarpeta();
                    MessageBox.Show("Carpeta creada");
                    crearblob(guion, label6.Text);
                    finpag1();

                }
                else
                {
                    MessageBox.Show("Ya existe esa carpeta");
                }


            } else
            {
                MessageBox.Show("Rellene el campo Nombre y Autor");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Length > 0 && textBox5.Text.Length > 0)
            {
                button1.Enabled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text.Length > 0 && textBox4.Text.Length > 0)
            {
                button1.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            




            String mensaje = "¿desea empezar de nuevo?";
            MessageBoxButtons button = MessageBoxButtons.YesNo;
            DialogResult resultado = MessageBox.Show(mensaje, "", button);
            if ((resultado == System.Windows.Forms.DialogResult.Yes))
            {
                textBox4.Text = "";
                textBox5.Text = "";
                groupBox2.Visible = false;
                groupBox1.Enabled = true;
                groupBox9.Enabled = false;
                    Eliminar();
                    RestaurarTextbox();

            }
        }

        private void Eliminar()
        {



            String query2 = "DELETE FROM partituras WHERE Carpeta=(SELECT Id FROM directorio WHERE Nombre='" + Partitura + "' && Autor='" + Autor + "')";
            String query = "DELETE FROM directorio WHERE Nombre='" + Partitura + "' && Autor='" + Autor + "'";
            Comando = new MySqlCommand(query2, Conexion);
            Conexion.Open();
            int a = Comando.ExecuteNonQuery();

            Conexion.Close();
            Comando = new MySqlCommand(query, Conexion);
            Conexion.Open();
            int b = Comando.ExecuteNonQuery();

            Conexion.Close();

            MessageBox.Show("Al resetear se ha borrado la carpeta que ha creado.");
        }


        public void finpag1()
        {
            tabControl1.SelectedIndex = 1;
            groupBox2.Enabled = false;
            groupBox3.Visible = true;
            groupClar.Visible = true;
            groupSax.Visible = true;


        }
        public void crearcarpeta()
        {

            String cat = "";
            if (Categoria.Equals("Marchas"))
            {
                cat = "Marcha";
            } else if (Categoria.Equals("Pasodobles"))
            {
                cat = "Pasodoble";
            } else if (Categoria.Equals("Obras"))
            {
                cat = "Obra";
            }


            String date = DateTime.Now.Date.ToString("yyyy/MM/dd");
            String consulta = "INSERT INTO directorio (Nombre, Autor,Fecha_alta,Categoria,URL) " +
                   "VALUES ('" + textBox5.Text + "','" + textBox4.Text + "','" + date + "','" + cat + "','')";


            Comando = new MySqlCommand(consulta, Conexion);
            Conexion.Open();
            int a = Comando.ExecuteNonQuery();
            Conexion.Close();

        }

        private void buttoncp_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String url = openFileDialog1.FileName;
                textcp.Text = url;

                textcp.Select(textcp.Text.Length, 0);
                crearblob(url, labelcprin.Text);

            }

        }


        private void buttonc1_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String url = openFileDialog1.FileName;
                textc1.Text = url;
                textc1.Select(textcp.Text.Length, 0);
                crearblob(url, labelc1.Text);

            }
        }

        private void buttonc2_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String url = openFileDialog1.FileName;
                textc2.Text = url;
                textc2.Select(textcp.Text.Length, 0);
                crearblob(url, labelc2.Text);

            }
        }

        private void buttonc3_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String url = openFileDialog1.FileName;
                textc3.Text = url;
                textc3.Select(textcp.Text.Length, 0);
                crearblob(url, labelc3.Text);

            }
        }

        private void buttoncb_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String url = openFileDialog1.FileName;
                textcb.Text = url;
                textcb.Select(textcp.Text.Length, 0);
                crearblob(url, labelcb.Text);

            }
        }

        public void copiar_partitura(String url_partitura, String url_carpeta, String instrumento)
        {
            String[] vs = url_partitura.Split('\\');
            String nombre = vs[vs.Length - 1];
            String url_def = url_carpeta + "\\" + "\\" + nombre;
            System.IO.File.Copy(url_partitura, url_def, true);

            insertarPartitura(url_def, instrumento);



        }

        public void insertarPartitura(String URL, String instrumento)
        {
            int id = 0;
            String query = "SELECT Id FROM directorio WHERE Nombre='" + Partitura + "' && Autor='" + Autor + "'";
            Comando = new MySqlCommand(query, Conexion);
            Conexion.Open();


            Leer = Comando.ExecuteReader();

            while (Leer.Read())
            {
                id = Leer.GetInt32(0);

            }
            Conexion.Close();
            query = "INSERT INTO partituras (Instrumento, Direccion,Carpeta) VALUES ('" + instrumento + "','" + URL + "','" + id + "')";
            Comando = new MySqlCommand(query, Conexion);
            Conexion.Open();
            int a = Comando.ExecuteNonQuery();

            Conexion.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            groupBox3.Enabled = false;
            groupClar.Enabled = false;
            groupSax.Enabled = false;
            groupBox8.Enabled = false;
            groupBox4.Enabled = true;
            groupBox4.Enabled = true;
            button3.Enabled = false;
            button4.Enabled = false;
            button9.Enabled = true;
            button10.Enabled = true;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            groupBox3.Enabled = false;
            groupClar.Enabled = false;
            groupSax.Enabled = false;
            groupBox8.Enabled = true;
        }

        private void buttons1_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String url = openFileDialog1.FileName;
                textBoxs1.Text = url;
                textBoxs1.Select(textcp.Text.Length, 0);
                crearblob(url, labels1.Text);

            }
        }

        private void buttons2_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String url = openFileDialog1.FileName;
                textBoxs2.Text = url;
                textBoxs2.Select(textcp.Text.Length, 0);
                crearblob(url, labels2.Text);

            }
        }
        private void buttonst1_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String url = openFileDialog1.FileName;
                textBoxst1.Text = url;
                textBoxst1.Select(textcp.Text.Length, 0);
                crearblob(url, labelst1.Text);

            }
        }

        private void buttonst2_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String url = openFileDialog1.FileName;
                textBoxst2.Text = url;
                textBoxst2.Select(textcp.Text.Length, 0);
                crearblob(url, labelst2.Text);

            }
        }

        private void buttonsb_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String url = openFileDialog1.FileName;
                textBoxsb.Text = url;
                textBoxsb.Select(textcp.Text.Length, 0);
                crearblob(url, labelsb.Text);

            }
        }

        private void buttonf1_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String url = openFileDialog1.FileName;
                textBoxf1.Text = url;
                textBoxf1.Select(textcp.Text.Length, 0);
                crearblob(url, labelf1.Text);

            }
        }

        private void buttonf2_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String url = openFileDialog1.FileName;
                textBoxf2.Text = url;
                textBoxf2.Select(textcp.Text.Length, 0);
                crearblob(url, labelf2.Text);

            }

        }

        private void buttonob_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String url = openFileDialog1.FileName;
                textBoxob.Text = url;
                textBoxob.Select(textcp.Text.Length, 0);
                crearblob(url, labelob.Text);

            }
        }

        private void buttonfag_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String url = openFileDialog1.FileName;
                textBoxfag.Text = url;
                textBoxfag.Select(textcp.Text.Length, 0);
                crearblob(url, labelfag.Text);

            }



        }

        public void AbrirDialogo(string instrumento, TextBox tb)
        {
            String url = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                url = openFileDialog1.FileName;
                tb.Text = url;
                tb.Select(textBoxfag.Text.Length, 0);
                copiar_partitura(url, sAttr, instrumento);
            }

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String url = openFileDialog1.FileName;
                textBoxt1.Text = url;
                textBoxt1.Select(textcp.Text.Length, 0);
                crearblob(url, labelt1.Text);

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String url = openFileDialog1.FileName;
                textBoxt2.Text = url;
                textBoxt2.Select(textcp.Text.Length, 0);
                crearblob(url, "Trompeta 2º");

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String url = openFileDialog1.FileName;
                textBoxt3.Text = url;
                textBoxt3.Select(textcp.Text.Length, 0);
                crearblob(url, "Trompeta 3º");

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String url = openFileDialog1.FileName;
                textBoxfl.Text = url;
                textBoxfl.Select(textcp.Text.Length, 0);
                crearblob(url, "Fliscorno");

            }
        }

        private void buttontr1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String url = openFileDialog1.FileName;
                textBoxtr1.Text = url;
                textBoxtr1.Select(textcp.Text.Length, 0);
                crearblob(url, "Trombón 1º");

            }
        }

        private void buttontr2_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String url = openFileDialog1.FileName;
                textBoxtr2.Text = url;
                textBoxtr2.Select(textcp.Text.Length, 0);
                crearblob(url, "Trombón 2º");

            }

        }

        private void buttontr3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String url = openFileDialog1.FileName;
                textBoxtr3.Text = url;
                textBoxtr3.Select(textcp.Text.Length, 0);
                crearblob(url, "Trombón 3º");

            }

        }

        private void buttontro_Click(object sender, EventArgs e)
        {
            AbrirDialogo("Trompa", textBoxtro);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String url = openFileDialog1.FileName;
                textBoxtro.Text = url;
                textBoxtro.Select(textcp.Text.Length, 0);
                crearblob(url, "Trompa");

            }

        }

        private void buttonbo_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String url = openFileDialog1.FileName;
                textBoxbo.Text = url;
                textBoxbo.Select(textcp.Text.Length, 0);
                crearblob(url, "Bombardino");

            }

        }

        private void buttontub_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String url = openFileDialog1.FileName;
                textBoxtub.Text = url;
                textBoxtub.Select(textcp.Text.Length, 0);
                crearblob(url, "Tuba");

            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
            groupBox4.Enabled = false;
            MessageBox.Show("Partitura creada con éxito");
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            groupBox4.Enabled = false;
            groupBox3.Enabled = true;
            groupClar.Enabled = true;
            groupSax.Enabled = true;
            groupBox8.Enabled = true;
            groupBox4.Enabled = true;
            groupBox4.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button9.Enabled = true;
            button10.Enabled = true;
        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void buttongui_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                guion = openFileDialog1.FileName;
                textBox3.Text = guion;
                


            }

        }

        public void crearblob(String url, String instrumento)
        {
            int id = 0;
            String query = "SELECT Id FROM directorio WHERE Nombre='" + Partitura + "' && Autor='" + Autor + "'";
            Comando = new MySqlCommand(query, Conexion);
            Conexion.Open();


            Leer = Comando.ExecuteReader();

            while (Leer.Read())
            {
                id = Leer.GetInt32(0);

            }
            Conexion.Close();

            byte[] archivo_byte = System.IO.File.ReadAllBytes(url);

            String archivobase64 = Convert.ToBase64String(archivo_byte);

            String consulta = "insert into partituras (Instrumento, Carpeta, Partitura, Direccion) values (@nombre, @carpeta, @partitura, @direccion)";
            Comando = new MySqlCommand(consulta, Conexion);
            Comando.Parameters.Add("@nombre", DbType.String);
            Comando.Parameters.Add("@carpeta", DbType.Int32);
            Comando.Parameters.Add("@partitura", DbType.String);
            Comando.Parameters.Add("@direccion", DbType.String);
            Comando.Parameters["@nombre"].Value = instrumento;
            Comando.Parameters["@carpeta"].Value = id;
            Comando.Parameters["@partitura"].Value = archivobase64;
            Comando.Parameters["@direccion"].Value = "";
            Conexion.Open();
            int i = Comando.ExecuteNonQuery();
            Conexion.Close();



        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click_1(object sender, EventArgs e)
        {

        }

        private void CrearPartitura_Load(object sender, EventArgs e)
        {

        }


        public  void RestaurarTextbox (){
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBoxbo.Text = "";
            textBoxf1.Text = "";
            textBoxf2.Text = "";
            textBoxfag.Text = "";
            textBoxfl.Text = "";
            textBoxgui.Text = "";
            textBoxob.Text = "";
            textBoxs1.Text = "";
            textBoxs2.Text = "";
            textBoxsb.Text = "";
            textBoxst1.Text = "";
            textBoxst2.Text = "";
            textBoxt1.Text = "";
            textBoxt2.Text = "";
            textBoxt3.Text = "";
            textBoxtr1.Text = "";
            textBoxtr2.Text = "";
            textBoxtr3.Text = "";
            textBoxtro.Text = "";
            textBoxtub.Text = "";
            textc1.Text = "";
            textc2.Text = "";
            textc3.Text = "";
            textcb.Text = "";
            textcp.Text = "";





        }

    }
}
