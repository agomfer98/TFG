using System;

public class Usuario
{
	public int Id;
	public String Nombre;
	public String Apellidos;
	public String Direccion;
	public String Usuario;
	public String Contraseña;
	
	
	public Usuario()
	{

	}
	public Usuario(int id, String nombre, String apellidos,String direccion,String usuario, String contraseña)
	{
		this.Id = id;
		Nombre = nombre;
		Apellidos = apellidos;
		Direccion = direccion;
		Usuario = usuario;
		Contraseña = contraseña;
			
			
	}

	public int Id { get; set; }	
	public String Nombre { get; set; }
	public String Apellidos { get; set; }
	public String Direccion { get; set; }
	public String Usuario { get; set; }
	public String Contraseña { get; set; }





}
