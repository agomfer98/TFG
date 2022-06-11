using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace TFG
{
    public partial class GuardarURL : Form
    {
        public GuardarURL()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        { String url;

           FolderBrowserDialog dialog = new FolderBrowserDialog();
            
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                url = dialog.SelectedPath;
                textBox1.Text = url;
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                foreach (XmlElement element in xmldoc.DocumentElement)
                {
                    if (element.Name.Equals("appSettings"))
                    {
                        MessageBox.Show("appSettings");
                        foreach (XmlNode node in element.ChildNodes)
                        {
                            if(node.Attributes[0].Value== "direccion")
                            {
                                MessageBox.Show("direccion");

                                
                            }
                        }
                    }

                }

                xmldoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                ConfigurationManager.RefreshSection("appSettings");

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
           this.Close();
        }
    }
}
