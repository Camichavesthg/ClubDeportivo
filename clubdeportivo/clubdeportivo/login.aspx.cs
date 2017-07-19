using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace clubdeportivo
{
    public partial class login : System.Web.UI.Page
    {
        static public List<Socio> Socios = new List<Socio>();
        static public List<Reserva> Reservas = new List<Reserva>();
        static public List<Instalacion> Instalaciones = new List<Instalacion>();
        static public Socio Yo = new Socio();

        private string Encriptar(string str)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        private int Buscar_pos_instalacion(int numinstalacion)
        {
            
            int i = 0;
            while ((i < Instalaciones.Count) && (Instalaciones[i].Numero != numinstalacion))
            {
                i++;
            }
            return i;
        }



        private int Buscar_pos_socio(int numsocio)
        {
            int i = 0;
            while ((i < Socios.Count) && (Socios[i].Numero != numsocio))
            {
                i++;
            }
            return i;
        }

        private void cargadatos()
        {
            //Cargo Socios
            OleDbConnection conexion1 = new OleDbConnection();
            conexion1.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\Users\Camila\Desktop\Datos.mdb" + ";Persist Security Info=False;";
            conexion1.Open();
            OleDbDataAdapter adaptador1;
            adaptador1 = new OleDbDataAdapter("Select * from Socio", conexion1);

            DataTable tabla1 = new DataTable();
            adaptador1.Fill(tabla1);

            for (int i = 0; i < tabla1.Rows.Count; i++)
            {
                DataRow fila = tabla1.Rows[i];
                Socio c = new Socio(int.Parse(fila["numero"].ToString()), fila["nombre"].ToString(), fila["apellido"].ToString(), fila["direccion"].ToString(), fila["telefono"].ToString(), bool.Parse(fila["moroso"].ToString()), fila["clave"].ToString());
                string prueba = fila["moroso"].ToString();
                Socios.Add(c);
            }
            conexion1.Close();
            conexion1.Dispose();




            //Cargo Instalaciones

            OleDbConnection conexion3 = new OleDbConnection();
            conexion3.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\Users\Camila\Desktop\Datos.mdb" + ";Persist Security Info=False;";
            conexion3.Open();
            OleDbDataAdapter adaptador3;
            adaptador3 = new OleDbDataAdapter("Select * from Instalacion", conexion3);

            DataTable tabla3 = new DataTable();
            adaptador3.Fill(tabla3);

            for (int i = 0; i < tabla3.Rows.Count; i++)
            {
                DataRow fila = tabla3.Rows[i];
                Instalacion c = new Instalacion(int.Parse(fila["numero"].ToString()), fila["nombre"].ToString(), fila["descripcion"].ToString());
                Instalaciones.Add(c);
            }
            conexion3.Close();
            conexion3.Dispose();


            // Cargi reservas
            OleDbConnection conexion2 = new OleDbConnection();
            conexion2.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\Users\Camila\Desktop\Datos.mdb" + ";Persist Security Info=False;";
            conexion2.Open();
            OleDbDataAdapter adaptador2;
            adaptador2 = new OleDbDataAdapter("Select * from Reserva", conexion2);

            DataTable tabla2 = new DataTable();
            adaptador2.Fill(tabla2);

            for (int i = 0; i < tabla2.Rows.Count; i++)
            {

                DataRow fila = tabla2.Rows[i];
                string[] cad = fila["fechareserva"].ToString().Split(' ');
                string[] cadhorainicio = fila["horainicio"].ToString().Split(' ');
                string[] cadhorafin = fila["horafin"].ToString().Split(' ');
                //

                if (cadhorainicio[2] == "p.m." && cadhorainicio[1]!="12:00:00" && cadhorainicio[1] != "12:30:00")
                {
                    string[] cadenita = cadhorainicio[1].Split(':');
                    int numero = int.Parse(cadenita[0]) + 12;
                    cadhorainicio[1] = string.Concat(numero.ToString(), ":", cadenita[1]);
                }

                if (cadhorafin[2] == "p.m." && cadhorafin[1] != "12:00:00" && cadhorafin[1] != "12:30:00")
                {
                    string[] cadenita = cadhorafin[1].Split(':');
                    int numero = int.Parse(cadenita[0]) + 12;
                    cadhorafin[1] = string.Concat(numero.ToString(), ":", cadenita[1]);
                }
                int indicesocio = Buscar_pos_socio(int.Parse(fila["numerosocio"].ToString()));
                int indiceinstalacion = Buscar_pos_instalacion(int.Parse(fila["numeroinstalacion"].ToString()));
                Reserva c = new Reserva(int.Parse(fila["numero"].ToString()), cad[0], cadhorainicio[1], cadhorafin[1], Socios[indicesocio], Instalaciones[indiceinstalacion]);
                Socios[indicesocio].reservas.Add(c);
                Instalaciones[indiceinstalacion].reservas.Add(c);
                Reservas.Add(c);
                int d = Reservas.Count();
            }
            conexion2.Close();
            conexion2.Dispose();
            Reservas.Count();

        }
        static bool b = false;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (b == false) { cargadatos(); b = true; }
                           
            
            Reservas.Count();

        }

        

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Ingresa Empleado
            int bandera = 0;
            int i = 0;
            if(botusuario.Text.Trim()=="" || Password1.Value.ToString() == "")
            {
                MensajeSocio.Text = "Ingrese todos los datos";
            }
            else
            {
                while(bandera==0 && i < Socios.Count())
                {
                    if (Socios[i].Numero == int.Parse(botusuario.Text.Trim()))
                    {
                        
                        

                        if (Encriptar(Password1.Value.ToString()) == Socios[i].Clave )
                        {
                            //Todo correcto, entro el socio al programa
                            Yo = Socios[i];
                            

                            bandera = 1;
                        }
                    }
                    i++;
                }
               
                if (bandera == 1) Response.Redirect("menusocio.aspx");
                if (bandera == 0) MensajeSocio.Text = "Datos incorrectos";
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (Password2.Value.ToString() == "")
            {
                MensajeEmpleado.Text = "Ingrese la contraseña";
            }else if (Password2.Value.ToString() == "clubclub")
            {
              
                Response.Redirect("menuempleados.aspx");

            }
            else { MensajeEmpleado.Text = "Clave incorrecta"; }
        }


    }
}