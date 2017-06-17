<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RemunerationsReport.aspx.cs" Inherits="RemunerationsReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="stylesheet" type="text/css" media='screen,print' href="CSS/Styles2.css" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            left: 100px;
            top: 150px;
            width: 606px;
        }
        .auto-style2 {
            left: 930px;
            top: 65px;
            width: 90px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" >
    <div class="container">
        <asp:Label ID="topLabel" runat="server" class="textLabel" Text="TOP N : "></asp:Label>
        <asp:TextBox ID="inputTop" class="input" runat="server" OnTextChanged="inputTop_TextChanged"></asp:TextBox>

        <asp:Label ID="startDateLabel" runat="server" class="textLabel"  Text="De : "></asp:Label>

         <asp:TextBox ID="startDay" class="date" runat="server"></asp:TextBox>
        <asp:TextBox ID="startMonth" class="date" runat="server"></asp:TextBox>
        <asp:TextBox ID="startYear" class="date" runat="server"></asp:TextBox>

        <asp:Label ID="endDateLabel" runat="server" class="textLabel"  Text="Hasta : "></asp:Label>

        <asp:TextBox ID="endDay" class="date" runat="server"></asp:TextBox>
        <asp:TextBox ID="endMonth" class="date" runat="server"></asp:TextBox>
        <asp:TextBox ID="endYear" class="date" runat="server"></asp:TextBox>


        <asp:Table ID="tableTemp" runat="server" CssClass="auto-style1">



        </asp:Table>


        <asp:Button ID="makeConsult" runat="server" Text="Aplicar" CssClass="auto-style2" OnClick="makeConsult_Click" />


    </div>

        <asp:Button ID="comeBack2" runat="server" Text="Regresar" OnClick="comeBack2_Click" />

    </form>
</body>
</html>
