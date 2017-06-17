<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClassificationOfRoutes.aspx.cs" Inherits="ClassificationOfRoutes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="stylesheet" type="text/css" media='screen,print' href="CSS/Styles2.css" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            left: 100px;
            top: 200px;
            width: 369px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">



    <div class="container" id="temp1">


        <asp:CheckBoxList ID="groupList" runat="server">
            <asp:ListItem Value="idtipoDeCaminata">Tipo de Caminata</asp:ListItem>
            <asp:ListItem Value="idNivelDePrecio">Nivel de precio</asp:ListItem>
            <asp:ListItem Value="idNivelDeDificultad">nivel de dificultad</asp:ListItem>
            <asp:ListItem Value="idNivelDeCalidad">Nivel de calidad</asp:ListItem>
        </asp:CheckBoxList>

    
        <asp:Table ID="tableClassification" runat="server" CssClass="auto-style1">
        </asp:Table>

    
        <asp:Button ID="aply" runat="server" Text="Aplicar" OnClick="aply_Click" />

    
        <asp:Table ID="TableIdtipoDeCaminata" runat="server" Class="tempTable"> 
        </asp:Table>
        <asp:Table ID="TableIdNivelDePrecio" runat="server" Class="tempTable">
        </asp:Table>
        <asp:Table ID="TableIdNivelDeDificultad" runat="server" Class="tempTable">
        </asp:Table>
        <asp:Table ID="TableIdNivelDeCalidad" runat="server" Class="tempTable">
        </asp:Table>

    
    </div>




    </form>
</body>
</html>
