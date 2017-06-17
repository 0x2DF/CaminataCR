<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StrategicInformation.aspx.cs" Inherits="StrategicInformation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="stylesheet" type="text/css" media='screen,print' href="CSS/Styles.css" />

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">


    <div id="container">
        <asp:RadioButtonList ID="options" runat="server" >
            <asp:ListItem Value="0">Consulta de usuarios por aportes</asp:ListItem>
            <asp:ListItem Value="1"> Gustos de rutas</asp:ListItem>
            <asp:ListItem Value="2">Clasificación de rutas</asp:ListItem>
            <asp:ListItem Value="3">Reporte de remuneraciones</asp:ListItem>
        </asp:RadioButtonList>

        <asp:Button ID="aply" runat="server" Text="Aplicar" OnClick="aply_Click" />
    </div>
       
    


    </form>
</body>
</html>
