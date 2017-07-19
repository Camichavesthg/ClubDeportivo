<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="menusocio.aspx.cs" Inherits="clubdeportivo.menusocio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">

                        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Disponibilidad de Horarios" />
                        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Mis Reservas" />
                        <asp:Button ID="Button1" runat="server" Text="Reservar" Width="209px" OnClick="Button1_Click" />
                        <br />
                        <br />
                        <br />
                        <asp:Table ID="TablaMisReservas" runat="server" Visible="False" BorderColor="Black" BorderStyle="Solid" GridLines="Both">
                            <asp:TableRow runat="server">
                                <asp:TableCell runat="server">Numero</asp:TableCell>
                                <asp:TableCell runat="server">Fecha</asp:TableCell>
                                <asp:TableCell runat="server">Hora de Inicio</asp:TableCell>
                                <asp:TableCell runat="server">Hora de Fin</asp:TableCell>
                                <asp:TableCell runat="server">Instalacion  Numero</asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                        <asp:Label ID="Label1" runat="server" Text="Instalacion" Visible="False"></asp:Label>
                        <asp:ListBox ID="ListBox1" runat="server" Visible="False" Width="225px" Height="43px">
                            <asp:ListItem></asp:ListItem>
                        </asp:ListBox>
                        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Buscar" Visible="False" />
                        <asp:Button ID="SelecInstala" runat="server" OnClick="SelecInstala_Click" Text="Seleccionar Instalacion" Visible="False" />
                        <br />
                        <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" Visible="False"></asp:Calendar>
                        <asp:Label ID="HoraInicio" runat="server" Text="Hora Inicio:" Visible="False"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="HoraFin" runat="server" Text="Hora Fin:" Visible="False"></asp:Label>
                        <br />
                        <asp:Label ID="lbmensaje" runat="server"></asp:Label>
                        <br />
                        <asp:ListBox ID="SeleccionHora" runat="server" Visible="False" Height="194px"></asp:ListBox>
                        &nbsp;&nbsp;&nbsp;
                        <asp:ListBox ID="ListBoxHorafIn" runat="server" Visible="False" Height="195px"></asp:ListBox>
                        <asp:Table ID="TablaDisponibles" runat="server" Visible="False" BorderStyle="Solid" GridLines="Both">
                            <asp:TableRow runat="server">
                                <asp:TableCell runat="server">Horarios Disponibles</asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                        <br />
                        <asp:Button ID="ButReserva" runat="server" Text="Reservar" Visible="False" OnClick="ButReserva_Click" />
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <br />
                        <br />
        </div>
    </form>
</body>
</html>
