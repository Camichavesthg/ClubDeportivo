using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace clubdeportivo
{
    public class Reserva
    {
        int numero; string fechareserva; string horainicio; string horafin;Socio socio; Instalacion instalacion;
        public Reserva () { }
        public Reserva(int n, string f, string hi, string hf, Socio ns, Instalacion ni)
        {
            numero = n;fechareserva = f;horainicio = hi;horafin = hf;instalacion = ni;socio = ns;
        }

        public int Numero
        {
            get { return numero; }
        }

        public string FechaReserva
        {
            get { return fechareserva; }
        }

        public string HoraInicio
        {
            get { return horainicio; }
        }

        public string HoraFin
        {
            get { return horafin; }
        }

        public Instalacion Instalacion
        {
            get { return instalacion ; }
        }

        public Socio Socio
        {
            get { return socio; }
        }
    }

    public class Socio
    {
        int numero; string nombre;string apellido;string direccion;string telefono; bool moroso;string clave; ArrayList reservasdelsocio;
        public Socio () { }
        public Socio(int n, string nom, string a,  string d, string t,bool m, string c)
        {
            numero = n;nombre = nom;apellido = a;direccion = d;telefono = t;clave = c;
            moroso=m;
            reservasdelsocio = new ArrayList();
        }
        
        public int Numero
        {
            get { return numero; }
        }
        
        public string Nombre
        {
            get { return nombre; }
        }

        public string Apellido
        {
            get { return apellido; }
        }

        public string Direccion
        {
            get { return direccion; }
        }

        public string Telefono
        {
            get { return telefono; }
        }

        public string Clave
        {
            get { return clave; }
        }

        public bool Moroso
        {
            get { return moroso; }
        }

        public ArrayList reservas
        {
            get { return reservasdelsocio; }
        }
    }

    public class Instalacion
    {
        int numero; string nombre;string descripcion;
        ArrayList reservasdelainstalacion;
        public Instalacion() { }
        public Instalacion(int n, string nom, string d)
        {
            numero = n;nombre = nom;descripcion = d;reservasdelainstalacion=new ArrayList();
        }

        public int Numero
        {
            get { return numero; }
        }

        public string Nombre
        {
            get { return nombre; }
        }

        public string Descripcion
        {
            get { return descripcion; }
        }
        public ArrayList reservas
        {
            get { return reservasdelainstalacion; }
        }
    }
     public class Intervalo
    {
        TimeSpan ini;
        TimeSpan fin;
        public Intervalo(TimeSpan i, TimeSpan f)
        {
            ini = i;
            fin = f;
        }
        public void Anular()
        {
            ini = new TimeSpan(0, 0, 0);
            fin = new TimeSpan(0,0,0);
        }
        public TimeSpan Getini
        {
            get { return ini; }
        }
        public TimeSpan Getfin
        {
            get { return fin; }
        }
    }
}