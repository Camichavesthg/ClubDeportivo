<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="menuempleados.aspx.cs" Inherits="clubdeportivo.menuempleados" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Instalacion"></asp:Label>
            <asp:ListBox ID="ListBox1" runat="server" Width="162px"></asp:ListBox>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Buscar" style="height: 26px" />
            <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
            <asp:Label ID="mensajemple" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Table ID="Table1" runat="server" Height="87px" Visible="False" Width="258px" BorderColor="Black" BorderStyle="Solid" GridLines="Both">
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">Numero de Reserva</asp:TableCell>
                    <asp:TableCell runat="server">Hora de Inicio</asp:TableCell>
                    <asp:TableCell runat="server">Hora de Fin</asp:TableCell>
                    <asp:TableCell runat="server">Numero de Socio</asp:TableCell>
                    <asp:TableCell runat="server">Nombre Socio</asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </form>
</body>
</html>
