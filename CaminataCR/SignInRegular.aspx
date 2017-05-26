<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignInRegular.aspx.cs" Inherits="SignInRegular" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <title>Inicio sesion Regular</title>


    <link href='http://fonts.googleapis.com/css?family=Montserrat:400,700' rel='stylesheet' type='text/css'>
    <link href="css/style.css" rel="stylesheet" />

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.4/jquery.min.js"></script>
    <!-- Bootstrap -->
    <link href="css/bootstrap.min.css" rel="stylesheet">

    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->


</head>
<body>
    <form id="form1" runat="server">
        <div class="center">
            <div class="login-block login-block2">
                <h1>Login</h1>
                <asp:TextBox ID="username" placeholder="Usuario"  required runat="server"></asp:TextBox>

                <asp:TextBox ID="password" placeholder="Contraseña" required runat="server" type="password"></asp:TextBox>
                <asp:Button ID="a" runat="server" Text="Iniciar Sesion" Class="login-block button" BackColor="#ffdd01" BorderColor="#ffdd01" ForeColor="White" Font-Bold="True" />
                <asp:Label ID="Errores" runat="server" Text=" <br> " CssClass="text-danger"></asp:Label>    
            </div>
        </div>
    </form>

    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="js/bootstrap.min.js"></script>
</body>


</html>
