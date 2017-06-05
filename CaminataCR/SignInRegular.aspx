<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignInRegular.aspx.cs" Inherits="SignInRegular" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <title>Inicio sesion Regular</title>


    <!-- Bootstrap Core CSS -->
    <link href="../vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">

    <!-- MetisMenu CSS -->
    <link href="../vendor/metisMenu/metisMenu.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="../dist/css/sb-admin-2.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="../vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <style>
        body
        {
            background-image: url('/images/fondo.jpg');
        }
        #page-wrapper
        {
            background: rgba(255, 255, 255, 0.1);
        }
        #map {
        height: 400px;
        width: 100%;
       }

    </style>

</head>
<body>

    <div class="container">
        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <div class="login-panel panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Iniciar sesión</h3>
                    </div>
                    <div class="panel-body">
                        <form role="form" runat="server">
                            <fieldset>
                                <div class="form-group">
                                    <asp:TextBox ID="username" class="form-control" maxlength="20" placeholder="Usuario" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator id="RFV_username"
                                      controltovalidate="username"
                                      validationgroup="login"
                                      errormessage="Ingrese su nombre de usuario."
                                      ForeColor="Red"
                                      runat="Server">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="password" class="form-control" placeholder="Contraseña" runat="server" type="password"></asp:TextBox>
                                    <asp:RequiredFieldValidator id="RFV_password"
                                      controltovalidate="password"
                                      validationgroup="login"
                                      errormessage="Ingrese su contraseña."
                                      ForeColor="Red"
                                      runat="Server">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Errors" runat="server" Text="" CssClass="text-danger"></asp:Label>
                                </div>
                                <asp:Button ID="Login_btn" runat="server" Text="Iniciar Sesion" OnClick="SignIn" class="btn btn-lg btn-success btn-block" causesvalidation="true" ValidationGroup="login"/>
                                <asp:Button ID="Register_btn" runat="server" Text="Registrar" OnClick="SignUp" class="btn btn-lg btn-success btn-block" />
                            </fieldset>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- jQuery -->
    <script src="../vendor/jquery/jquery.min.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="../vendor/bootstrap/js/bootstrap.min.js"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="../vendor/metisMenu/metisMenu.min.js"></script>

    <!-- Custom Theme JavaScript -->
    <script src="../dist/js/sb-admin-2.js"></script>

</body>
</html>
