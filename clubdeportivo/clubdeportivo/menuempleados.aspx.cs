using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace clubdeportivo
{
    public partial class menuempleados : System.Web.UI.Page
    {
       static bool b = false;

        protected void CargaListBox()
        {
            List<Instalacion> Instalaciones = login.Instalaciones;

            for (int i = 0; i < Instalaciones.Count(); i++)
            {

                ListItem c = new ListItem();
                c.Text = Instalaciones[i].Nombre;
                c.Value = Instalaciones[i].Numero.ToString();
                ListBox1.Items.Add(c);

            }
        }


        static bool Vacio=false;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (b == false)
            {
                CargaListBox();
                b = true;
            }
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Vacio = false;
            mensajemple.Visible = false;
            Table1.Visible = false;
            if (Calendar1.SelectedDate.Date==DateTime.MinValue.Date)
            {
                mensajemple.Text = "Ingrese la fecha";

            }
            else if (ListBox1.SelectedIndex < 0) { mensajemple.Text = "Ingrese instalacion"; Calendar1.SelectedDate = DateTime.MinValue.Date;
            } else
            {
                
                List<Reserva> Reservas = login.Reservas;
                List<Socio> Socios = login.Socios;
                List<Instalacion> Instalaciones=login.Instalaciones;
                int i = 0;

                while (i < Instalaciones.Count() && Instalaciones[i].Numero != int.Parse(ListBox1.SelectedValue))
                {
                    i++;
                }
               int d= Instalaciones[i].Numero;

                if(Instalaciones[i].Numero == int.Parse(ListBox1.SelectedValue))
                {
                    foreach (Reserva r in Instalaciones[i].reservas)
                    {
                        string[] cad = r.FechaReserva.Split('/');
                       
                        DateTime reser = new DateTime(int.Parse(cad[2]), int.Parse(cad[1]), int.Parse(cad[0]));
                        if (reser == Calendar1.SelectedDate.Date)
                        {
                            Vacio = true;
                            TableRow k = new TableRow();
                            TableCell c = new TableCell();
                            c.Text = r.Numero.ToString();
                            k.Cells.Add(c);

                            TableCell c3 = new TableCell();
                            c3.Text = r.HoraInicio;
                            k.Cells.Add(c3);

                            TableCell c4 = new TableCell();
                            c4.Text = r.HoraFin;
                            k.Cells.Add(c4);

                            TableCell c5 = new TableCell();
                            c5.Text = r.Socio.Numero.ToString();
                            k.Cells.Add(c5);

                            
                            TableCell c6 = new TableCell();
                            c6.Text = string.Concat(r.Socio.Nombre," ",r.Socio.Apellido);
                            k.Cells.Add(c6);
                                
                            Table1.Rows.Add(k);

                        }

                    }

                    
                }

           }
            if (Vacio == true) { Table1.Visible = true;
            } else
            {
                mensajemple.Visible = true;
                mensajemple.Text = "No hay reservas";
            }
            
            Calendar1.SelectedDate = DateTime.MinValue.Date;
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            Table1.Visible = false;

        }
    }
}