using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFG
{
    internal class Componente
    {
        public int Id { get; set; }
		public String Nombre { get; set; }
		public String Apellidos { get; set; }
		public String Direccion { get; set; }
		public String Username { get; set; }
		public String Contraseña { get; set; } 
		public String Tipo { get; set; }
		public String Cuerda { get; set; }
		public String Instrumento { get; set; }



        public Componente()
        {
           
        }


        public Componente(int id, String nombre, String apellidos,String direccion,String usuario, String contraseña, String tipo, String instrumento, String cuerda)	{
		Id = id;
		Nombre = nombre;
		Apellidos = apellidos;
		Direccion = direccion;
		Username = usuario;
		Contraseña = contraseña;
		Tipo = tipo;
		Instrumento=instrumento;
			Cuerda = cuerda;
		
			
	}
		




    }
}
