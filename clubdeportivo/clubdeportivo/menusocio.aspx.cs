using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace clubdeportivo
{
    public partial class menusocio : System.Web.UI.Page
    {

        private int Buscar_pos_instalacion(int numinstalacion)
        {

            int i = 0;
            while ((i < login.Instalaciones.Count) && (login.Instalaciones[i].Numero != numinstalacion))
            {
                i++;
            }
            return i;
        }

        private string GetFecha(string dia, string mes, string anio)
        {
            string c = string.Concat(dia, "/", mes, "/", anio);
            return c;
        }

        private bool Valida()
        {


            lbmensaje.Visible = true;

           
            string[] caden = SeleccionHora.SelectedValue.Split(':');
            string[] cadena = ListBoxHorafIn.SelectedValue.Split(':');
            TimeSpan inicial = new TimeSpan(int.Parse(caden[0]), int.Parse(caden[1]), 00);
            TimeSpan final = new TimeSpan(int.Parse(cadena[0]), int.Parse(cadena[1]), 00);
            if (Calendar1.SelectedDate.Date == DateTime.MinValue.Date)
            {
                lbmensaje.Text = "Seleccione una fecha";
            }

            
            if (SeleccionHora.SelectedIndex < 0 || ListBoxHorafIn.SelectedIndex < 0)
            {
                 Calendar1.SelectedDate = DateTime.MinValue.Date;
                lbmensaje.Text = "Seleccione hora";
                return false;
            }
            if (ListBox1.SelectedIndex < 0)
            {
                lbmensaje.Text = "Seleccione instalación";
                return false;
            }
            if (inicial.Minutes != 00 && inicial.Minutes != 30)
            {
                lbmensaje.Text = "Solo se puede reservar con minutos 00 o 30";
                return false;
            }
            if (inicial.Hours < 9 || inicial.Hours > 23)
            {
                lbmensaje.Text = "Solo se puede reservar con hora inicial entre 9:00 y 23:30";
                return false;
            }
            TimeSpan n = new TimeSpan(9, 30, 00);
            if (final < n || final.Hours > 24)
            {
                lbmensaje.Text = "Solo se puede reservar con hora final entre 9:30 y 00:00";
                return false;
            }

            DateTime fechingresada = new DateTime(Calendar1.SelectedDate.Year, Calendar1.SelectedDate.Month, Calendar1.SelectedDate.Day, int.Parse(caden[0]), int.Parse(caden[1]), 00);
            DateTime hoy = DateTime.Now;

            if (fechingresada < hoy)
            {
                lbmensaje.Text = "El tiempo de la reserva ya paso";
                return false;
            }

            if (inicial >= final)
            {
                lbmensaje.Text = "La hora final es menor que la hora inicial";
                return false;
            }

            TimeSpan d = new TimeSpan(3, 00, 00);
            if (final.Subtract(inicial) > d)
            {

                lbmensaje.Text = "No se puede reservar mas de 3 horas conseutivas";
                return false;
            }

            

            TimeSpan HoraInicial = new TimeSpan(int.Parse(caden[0]), int.Parse(caden[1]), 00);
            TimeSpan HoraFinal = new TimeSpan(int.Parse(cadena[0]), int.Parse(cadena[1]), 00);
            for (int i = 0; i < login.Reservas.Count(); i++)
            {
                string[] hic = login.Reservas[i].HoraInicio.Split(':');
                string[] hfc = login.Reservas[i].HoraFin.Split(':');
                TimeSpan HoraInicialTabla = new TimeSpan(int.Parse(hic[0]), int.Parse(hic[1]), 00);
                TimeSpan HoraFinalTabla = new TimeSpan(int.Parse(hfc[0]), int.Parse(hfc[1]), 00);

                string[] fech = login.Reservas[i].FechaReserva.Split('/');
                DateTime fechareserva = new DateTime(int.Parse(fech[2]), int.Parse(fech[1]), int.Parse(fech[0]));
                
                if (login.Reservas[i].Instalacion.Numero == int.Parse(ListBox1.SelectedValue) && fechareserva.Date == fechingresada.Date)
                {



                    if (HoraInicial < HoraInicialTabla && HoraFinal > HoraInicialTabla)
                    {
                        lbmensaje.Text = "Horario Ocupado";
                        return false;
                    }

                    if (HoraFinal > HoraFinalTabla && HoraInicial < HoraFinalTabla)
                    {
                        lbmensaje.Text = "Horario Ocupado";
                        return false;
                    }

                }
            }

            return true;
        }



        private bool ValidaConsulta()
        {
            if (Calendar1.SelectedDate.Date == DateTime.MinValue.Date)
            {
                lbmensaje.Text = "Seleccione una fecha";
            }


            if (ListBox1.SelectedIndex < 0)
            {
                lbmensaje.Visible = true;
                lbmensaje.Text = "Seleccione instalacion";
                return false;
            }
            return true;
        }
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
        static bool b = false;
        List<Intervalo> intervalos = new List<Intervalo>();
        static bool Calen;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (b == false)
            {
                CargaListBox();
                b = true;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            HoraInicio.Visible = false;
            HoraFin.Visible = false;
            SeleccionHora.Visible = false;
            ListBoxHorafIn.Visible = false;
            SelecInstala.Visible = false;
            ButReserva.Visible = false;
            lbmensaje.Visible = false;
            Button4.Visible = false;
            TablaDisponibles.Visible = false;
            Label1.Visible = false;
            Calendar1.Visible = false;
            ListBox1.Visible = false;
            if (login.Yo.reservas.Count > 0) { 
            


            TablaMisReservas.Visible = true;
            Socio yo = login.Yo;

            for (int i = 0; i < login.Reservas.Count(); i++)
            {
                if (login.Reservas[i].Socio.Numero == yo.Numero)
                {
                    TableRow r = new TableRow();
                    TableCell c = new TableCell();
                    c.Text = login.Reservas[i].Numero.ToString();
                    r.Cells.Add(c);


                    TableCell c2 = new TableCell();
                    string[] fec = login.Reservas[i].FechaReserva.Split(' ');
                    c2.Text = fec[0];
                    r.Cells.Add(c2);

                    TableCell c3 = new TableCell();
                    c3.Text = login.Reservas[i].HoraInicio;
                    r.Cells.Add(c3);

                    TableCell c4 = new TableCell();
                    c4.Text = login.Reservas[i].HoraFin;
                    r.Cells.Add(c4);


                    TableCell c5 = new TableCell();
                    c5.Text = login.Reservas[i].Instalacion.Numero.ToString();
                    r.Cells.Add(c5);

                    TablaMisReservas.Rows.Add(r);


                }
            }
            }
            else { lbmensaje.Visible = true;
                lbmensaje.Text = "No posee reservas";
            }
            }

        protected void Button3_Click(object sender, EventArgs e)
        {
            lbmensaje.Visible = false;
            Calendar1.SelectedDate = DateTime.MinValue.Date;
            Calen = false;
            SelecInstala.Visible = false;
            ButReserva.Visible = false;
            SeleccionHora.Visible = false;
            ListBoxHorafIn.Visible = false;
            HoraInicio.Visible = false;
            HoraFin.Visible = false;
            Label1.Visible = true;
            Calendar1.Visible = true;
            ListBox1.Visible = true;

            TablaMisReservas.Visible = false;
            Button4.Visible = true;


        }
        private void horariosdisponibles()
        {

            intervalos.Clear();
            for (int i = 9; i < 23; i++)
            {
                Intervalo I = new Intervalo(new TimeSpan(i, 0, 0), new TimeSpan(i, 30, 0));
                Intervalo I2 = new Intervalo(new TimeSpan(i, 30, 0), new TimeSpan(i + 1, 0, 0));
                intervalos.Add(I);
                intervalos.Add(I2);
            }
            Intervalo I23 = new Intervalo(new TimeSpan(23, 0, 0), new TimeSpan(23, 30, 0));
            Intervalo I24 = new Intervalo(new TimeSpan(23, 30, 0), new TimeSpan(23, 59, 59));
            intervalos.Add(I23);
            intervalos.Add(I24);

            //cree la lista con los horarios

            List<Reserva> Reservas = login.Reservas;
            for (int i = 0; i < Reservas.Count(); i++)
            {
                string[] cade = Reservas[i].FechaReserva.Split('/');
                DateTime Reserv = new DateTime(int.Parse(cade[2]), int.Parse(cade[1]), int.Parse(cade[0]));

                if (Reservas[i].Instalacion.Numero == int.Parse(ListBox1.SelectedValue) && Reserv == Calendar1.SelectedDate.Date)
                {
                    for (int j = 0; j < intervalos.Count(); j++)
                    {
                        string[] horai = Reservas[i].HoraInicio.Split(':');
                        TimeSpan HoraInicio = new TimeSpan(int.Parse(horai[0]), int.Parse(horai[1]), 00);

                        string[] horaf = Reservas[i].HoraFin.Split(':');
                        TimeSpan HoraFin = new TimeSpan(int.Parse(horaf[0]), int.Parse(horaf[1]), 00);



                        if (intervalos[j].Getini >= HoraInicio && (intervalos[j].Getfin <= HoraFin || HoraFin.Hours == 00))
                        {
                            intervalos[j].Anular();
                        }
                    }


                }


            }
        }
        protected void Button4_Click(object sender, EventArgs e)
        {

            if (ValidaConsulta())
            {
                horariosdisponibles();
                bool band = true;
                TimeSpan cero = new TimeSpan(00, 00, 00);
                TimeSpan ini=new TimeSpan();
                TimeSpan fini = new TimeSpan();
                //hacer visible tabla
                TablaDisponibles.Visible = true;
                int i;
                for ( i = 0; i < intervalos.Count(); i++)
                {
                    if (band)
                    {
                        if (intervalos[i].Getini != cero)
                        {
                            ini = intervalos[i].Getini;
                            band = false;
                        }
                    }
                    else
                    {
                        if(intervalos[i].Getini == cero)
                        {
                            fini = intervalos[i - 1].Getfin;
                            band = true;
                            TableRow r = new TableRow();
                            TableCell c = new TableCell();
                            c.Text = ini.ToString()+"--"+fini.ToString();
                            r.Cells.Add(c);

                            TablaDisponibles.Rows.Add(r);


                        }

                    }
                }

                if (band == false)
                {
                    fini = intervalos[i - 1].Getfin;
                    band = true;
                    TableRow r = new TableRow();
                    TableCell c = new TableCell();
                    c.Text = ini.ToString() + "--" + fini.ToString();
                    r.Cells.Add(c);

                    TablaDisponibles.Rows.Add(r);

                }
            }



        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            HoraInicio.Visible = false;
            HoraFin.Visible = false;
            Calendar1.SelectedDate = DateTime.MinValue.Date;
            Calen = true;
            SelecInstala.Visible = false;
            TablaDisponibles.Visible = false;

            Label1.Visible = false;
            Calendar1.Visible = false;
            ListBox1.Visible = false;


            Button4.Visible = false;
            TablaMisReservas.Visible = false;
            Socio yo = login.Yo;
            List<Reserva> Reservas = login.Reservas;
            lbmensaje.Visible = true;
            if (yo.Moroso)
            {
                Calendar1.SelectedDate = DateTime.MinValue.Date;
                lbmensaje.Text = "Un socio moroso no puede reservar";
            }
            else
            {
                lbmensaje.Visible = false;
                int con = 0;
                for (int i = 0; i < Reservas.Count(); i++)
                {
                    if (Reservas[i].Socio.Numero == yo.Numero)
                    {
                        con++;
                    }
                }
                lbmensaje.Visible = true;
                if (con > 2) { Calendar1.SelectedDate = DateTime.MinValue.Date; lbmensaje.Text = "Ya posee 3 reservas vigentes"; }
                else
                {
                    TablaDisponibles.Visible = false;

                    lbmensaje.Visible = false;
                    Label1.Visible = true;
                    ListBox1.Visible = true;

                    TablaMisReservas.Visible = false;
                    Button4.Visible = false;
                    SelecInstala.Visible = true;
                }

            }



        }

        protected void ButReserva_Click(object sender, EventArgs e)
        {

            TablaDisponibles.Visible = false;
            lbmensaje.Visible = true;
            if (ListBox1.SelectedIndex < 0)
            {
                Calendar1.SelectedDate = DateTime.MinValue.Date;
                lbmensaje.Text = "Seleccione instalación";

            }
            else
            {
                if (SeleccionHora.SelectedIndex < 0 || ListBoxHorafIn.SelectedIndex < 0)
                {
                    
                    lbmensaje.Text = "Seleccione horario";
                }
                else
                {
                    if (Valida())
                    {


                        string fech = string.Concat(Calendar1.SelectedDate.Day, "/", Calendar1.SelectedDate.Month, "/", Calendar1.SelectedDate.Year);
                        string h1 = SeleccionHora.SelectedValue;
                        string hf = ListBoxHorafIn.SelectedValue;
                        int indice = Buscar_pos_instalacion(int.Parse(ListBox1.SelectedValue));
                        Reserva c = new Reserva(login.Reservas.Count() + 1, fech, h1, hf, login.Yo, login.Instalaciones[indice]);
                        login.Reservas.Add(c);
                        login.Yo.reservas.Add(c);
                        login.Instalaciones[indice].reservas.Add(c);

                        OleDbConnection conn = new OleDbConnection();
                        conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\Users\Camila\Desktop\Datos.mdb" + ";Persist Security Info=False;";

                        // guarda en el archivo excel
                        string insert = "Insert into Reserva (numero,fechareserva,horainicio,horafin,numeroinstalacion,numerosocio) Values (?,?,?,?,?,?)";
                        OleDbCommand insertCommand = new OleDbCommand(insert, conn);
                        lbmensaje.Visible = true;
                        try
                        {
                            insertCommand.Parameters.Add("numero", OleDbType.VarChar).Value = c.Numero;
                            insertCommand.Parameters.Add("fechareserva", OleDbType.VarChar).Value = c.FechaReserva;
                            insertCommand.Parameters.Add("horainicio", OleDbType.VarChar).Value = c.HoraInicio;
                            insertCommand.Parameters.Add("horafin", OleDbType.VarChar).Value = c.HoraFin;
                            insertCommand.Parameters.Add("numeroinstalacion", OleDbType.VarChar).Value = ListBox1.SelectedValue;
                            insertCommand.Parameters.Add("numerosocio", OleDbType.VarChar).Value = login.Yo.Numero;
                            conn.Open();
                            int count = insertCommand.ExecuteNonQuery();
                            lbmensaje.Text = "Instalacion reservada";
                        }
                        catch (OleDbException ex)
                        {
                            lbmensaje.Text = ex.Message;
                        }
                        catch (Exception ex)
                        {
                            lbmensaje.Text = ex.Message;

                        }
                        finally
                        {
                            conn.Close();                         // reestablece los controles del formulario                         txb_nombre.Text = "";                         txb_registro.Text = "";                         lsb_carrera.SelectedIndex = 0;                     }   
                        }
                        if(lbmensaje.Text == "Instalacion reservada")
                        {
                            SeleccionHora.Visible = false;
                            ListBoxHorafIn.Visible = false;
                            Calendar1.SelectedDate = DateTime.MinValue.Date;
                            HoraInicio.Visible = false;
                            HoraFin.Visible = false;
                            ButReserva.Visible = false;
                        }
                    }
                    
                }
            }
            
        }






    



    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
            lbmensaje.Visible = false;
            if (Calen == true)
            {
                HoraInicio.Visible = true;
                HoraFin.Visible = true;
                ButReserva.Visible = true;
                horariosdisponibles();
                SeleccionHora.Items.Clear();
                ListBoxHorafIn.Items.Clear();
                SeleccionHora.Visible = true;
                ListBoxHorafIn.Visible = true;
                for (int i = 0; i < intervalos.Count(); i++)
                {

                    if (intervalos[i].Getfin.Hours != 0)
                    {
                        ListItem g = new ListItem();
                        g.Text = intervalos[i].Getini.ToString();
                        g.Value = intervalos[i].Getini.ToString();
                        SeleccionHora.Items.Add(g);

                    }

                    if (intervalos[i].Getfin.Hours != 0)
                    {
                        ListItem g = new ListItem();
                        g.Text = intervalos[i].Getfin.ToString();
                        g.Value = intervalos[i].Getfin.ToString();
                        ListBoxHorafIn.Items.Add(g);
                    }


                }
            }
            else
            {
                TablaDisponibles.Visible = false;
            }
    }


        protected void SelecInstala_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedIndex > 0)
            {
                Calendar1.Visible = true;

            }
        }
    }
}
    
    
