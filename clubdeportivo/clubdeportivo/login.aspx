<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="clubdeportivo.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="GamnamStyle.css" type="text/css" rel="stylesheet" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #Password1 {
            width: 94px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server" style="text-align: center">
        <div style="text-align: center">
            <asp:Label ID="Label1" runat="server" BorderColor="WhiteSmoke" Text=" BIENVENIDO " Width="156px"></asp:Label>
            <br />
            <br />
        </div>
        <asp:Label ID="Label2" runat="server" Text="Usuario" ForeColor="#666666"></asp:Label>
        <asp:TextBox ID="botusuario" runat="server" Width="94px"></asp:TextBox>
        <p>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label3" runat="server" Text="Clave" ForeColor="#666666"></asp:Label>
            <input id="Password1" type="password" runat="server" /></p>
        <asp:Label ID="MensajeSocio" runat="server"></asp:Label>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Ingresar" BorderStyle="Solid" ForeColor="#3366CC" />
        <p style="text-align: center">
            <asp:Label ID="Label4" runat="server" BorderColor="#333333" Text="Empleados" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" ForeColor="#666666"></asp:Label>
        </p>
        <asp:Label ID="Label5" runat="server" Text="Contraseña" ForeColor="#666666"></asp:Label>
        <input id="Password2" runat="server" type="password" /><br />
        <asp:Label ID="MensajeEmpleado" runat="server"></asp:Label>
        
        <p>
            <asp:Button ID="Button2" runat="server" Text="Ingresar" OnClick="Button2_Click" BorderStyle="Solid" ForeColor="#3366CC" /><br />
            <img src="logo-deportes.jpg" />
        </p>
    </form>
    <p>
        &nbsp;</p>
    </body>
</html>
