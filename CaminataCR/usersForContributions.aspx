<%@ Page Language="C#" AutoEventWireup="true" CodeFile="usersForContributions.aspx.cs" Inherits="usersForContributions" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="stylesheet" type="text/css" media='screen,print' href="CSS/Styles2.css" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            left: 20px;
            top: 150px;
            width: 1053px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mainContainer" class="container">

        <div id="containerTemp">
            
                <asp:Label ID="nameLabel" runat="server" class="textLabel" Text="Nombre:"></asp:Label>
                <asp:TextBox ID="inputDataName" class="input" runat="server"></asp:TextBox>
            
                <asp:Label ID="lastNameLabel" runat="server" class="textLabel" Text="Apellido:"></asp:Label>
                <asp:TextBox ID="inputDataLastName" class="input" runat="server"></asp:TextBox>

                <asp:Label ID="likesLabel" runat="server" class="textLabel" Text="Likes:"></asp:Label>
                <asp:TextBox ID="inputLikes" class="input" runat="server"></asp:TextBox>

                <asp:DropDownList ID="orderBy" runat="server">
                    <asp:ListItem Value="primerNombre" Selected="True">Nombre</asp:ListItem>
                    <asp:ListItem Value="primerApellido">Apellido</asp:ListItem>
                    <asp:ListItem Value="correo">Correo</asp:ListItem>
                    <asp:ListItem Value="telefono">Celular</asp:ListItem>
                    <asp:ListItem Value="cantidadCaminatas">Cantidad De Caminatas</asp:ListItem>
                    <asp:ListItem Value="cantidadPuntos">Cantidad de Puntos</asp:ListItem>
                    <asp:ListItem Value="likes">Likes</asp:ListItem>
                </asp:DropDownList>

                <asp:Label ID="orderByLabel" runat="server" Text="Ordenar Por: "></asp:Label>

        </div>



        <asp:Table ID="infoTable" runat="server" CssClass="auto-style1">
        </asp:Table>


        <asp:Button ID="search" runat="server" Text="Buscar" OnClick="search_Click" />


        <asp:RadioButtonList ID="orderASCDES" runat="server">
            <asp:ListItem Value="asc" Selected="True">Ascendente</asp:ListItem>
            <asp:ListItem Value="desc">Descendente </asp:ListItem>
        </asp:RadioButtonList>


    </div>
        <asp:Button ID="comeBack" runat="server" Text="Regresar" OnClick="comeBack_Click" />

    </form>
</body>
</html>
