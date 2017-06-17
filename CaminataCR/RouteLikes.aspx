<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RouteLikes.aspx.cs" Inherits="RouteLikes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="stylesheet" type="text/css" media='screen,print' href="CSS/Styles2.css" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <style type="text/css">
        .auto-style2 {
            right: 392px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
    
        <asp:TextBox ID="TopN" class="input" runat="server"></asp:TextBox>
        <asp:Label ID="LabelTopN" runat="server" class="textLabel" Text="Top N:"></asp:Label>
    
        <asp:Label ID="dataRangeLabel" runat="server" class="textLabel" Text="Rango fecha de:"></asp:Label>

        
        <asp:TextBox ID="startDayI" class="date" runat="server"></asp:TextBox>
        <asp:TextBox ID="startMonthI" class="date" runat="server"></asp:TextBox>
        <asp:TextBox ID="startYearI" class="date" runat="server"></asp:TextBox>

       
        <asp:Label ID="dataRangeLabelEnd" runat="server" class="textLabel" Text="Hasta : "></asp:Label>

        <asp:TextBox ID="endDayI" class="date" runat="server"></asp:TextBox>
        <asp:TextBox ID="endMonthI" class="date" runat="server"></asp:TextBox>
        <asp:TextBox ID="endYearI" class="date" runat="server" ></asp:TextBox>


        <asp:RadioButtonList ID="order" runat="server">
            <asp:ListItem Value="asc" Selected="True">Ascendente</asp:ListItem>
            <asp:ListItem Value="desc">Descendente </asp:ListItem>
        </asp:RadioButtonList>
        
        <div id="imageContainer">
        
            
            
                
        </div>

    </div>
    </form>
</body>
</html>
