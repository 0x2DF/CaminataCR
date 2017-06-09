<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditDificultyLevel.aspx.cs" Inherits="EditDificultyLevel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Administradores</title>

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

</head>

<body style="background: green">
    <form runat="server">
        <div id="wrapper">

            <!-- Navigation -->
            <nav class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0; background-color: green;">
                <div class="navbar-header" style="background-color: green">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse" style="color: white background:green">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar" style="color: white"></span>
                        <span class="icon-bar" style="color: white"></span>
                        <span class="icon-bar" style="color: white"></span>
                    </button>
                    <a href="CatalogMaintenance.aspx" class="navbar-brand" style="color: white">Administradores</a>
                </div>
                <!-- /.navbar-header -->

                <ul class="nav navbar-top-links navbar-right">

                    <!-- /.dropdown -->
                    <li class="dropdown">
                        <a class="dropdown-toggle" data-toggle="dropdown" href="#" style="color: white; background: green">
                            <i class="fa fa-user fa-fw"></i><i class="fa fa-caret-down"></i>
                        </a>
                        <ul class="dropdown-menu dropdown-user" style="background-color: green">
                            <li><a href="#" style="color: white; background: green"><i class="fa fa-user fa-fw"></i>User Profile</a>
                            </li>
                            <li><a href="#" style="color: white; background: green"><i class="fa fa-gear fa-fw"></i>Settings</a>
                            </li>
                            <li class="divider" style="color: white; background: green"></li>
                            <li><a href="login.html" style="color: white; background: green"><i class="fa fa-sign-out fa-fw"></i>Logout</a>
                            </li>
                        </ul>
                        <!-- /.dropdown-user -->
                    </li>
                    <!-- /.dropdown -->
                </ul>
                <!-- /.navbar-top-links -->

                <div class="navbar-default sidebar" role="navigation" style="background: green; max-height: 10px;">
                    <div class="sidebar-nav navbar-collapse" style="background-color: green; background-repeat: no-repeat;">
                        <ul class="nav " id="side-menu" style="background: green; background-repeat: no-repeat;">
                            <li>
                                <a href="#" style="color: white; background: green"><i class="fa fa-table fa-fw"></i>Bitácora</a>
                            </li>
                            <li>
                                <a href="#" style="color: white; background: green"><i class="fa fa-sitemap fa-fw"></i>Mantenimiento catálogos<span class="fa arrow"></span></a>
                                <ul class="nav nav-second-level">
                                    <li>
                                        <a href="WalkType.aspx" style="color: white; background: green">Tipo de caminata</a>
                                    </li>
                                    <li>
                                        <a href="#" style="color: white; background: green">Niveles <span class="fa arrow"></span></a>
                                        <ul class="nav nav-third-level">
                                            <li>
                                                <a href="DificultyLevel.aspx" style="color: white; background: green">Dificultad</a>
                                            </li>
                                            <li>
                                                <a href="PriceLevel.aspx" style="color: white; background: green">Precio</a>
                                            </li>
                                            <li>
                                                <a href="QualityLevel.aspx" style="color: white; background: green">Calidad</a>
                                            </li>
                                        </ul>
                                        <!-- /.nav-third-level -->
                                    </li>
                                    <li>
                                        <a href="#" style="color: white; background: green">Usuario <span class="fa arrow"></span></a>
                                        <ul class="nav nav-third-level">
                                            <li>
                                                <a href="UserAdministrator.aspx" style="color: white; background: green">Administradores</a>
                                            </li>
                                            <li>
                                                <a href="UserICT.aspx" style="color: white; background: green">ICT</a>
                                            </li>
                                            <li>
                                                <a href="UserRegular.aspx" style="color: white; background: green">Regulares</a>
                                            </li>
                                        </ul>
                                        <!-- /.nav-third-level -->
                                    </li>
                                </ul>
                                <!-- /.nav-second-level -->
                            </li>

                        </ul>
                    </div>
                    <!-- /.sidebar-collapse -->
                </div>
                <!-- /.navbar-static-side -->
            </nav>

            <!-- Page Content -->
            <div id="page-wrapper">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-lg-12">
                            <h1 class="page-header">Editar nivel de dificultad</h1>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" Text="Nivel de dificultad"></asp:Label>

                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="dificultylevel" placeholder="Nivel de dificultad" class="form-control" required runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" Text="Activo"></asp:Label>
                                <div class="checkbox">
                                    <label>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <asp:Button ID="a" runat="server" Text="Agregar" CssClass="btn btn-success btn-outline" BackColor="#269E22" BorderColor="#269E22" ForeColor="White" Font-Bold="True" OnClick="submitEditDificultyLevel"/>
                                    <asp:Button ID="Button1" runat="server" Text="Eliminar" CssClass="btn btn-success btn-outline" BackColor="#269E22" BorderColor="#269E22" ForeColor="White" Font-Bold="True" OnClick="deleteWalkType" />
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="Errors" runat="server" Text=" <br> " CssClass="text-danger"></asp:Label>
                            </div>
                        </div>
                        <!-- /.col-lg-12 -->
                    </div>
                    <!-- /.row -->
                </div>
                <!-- /.container-fluid -->
            </div>
            <!-- /#page-wrapper -->

        </div>
        <!-- /#wrapper -->

        <!-- jQuery -->
        <script src="../vendor/jquery/jquery.min.js"></script>

        <!-- Bootstrap Core JavaScript -->
        <script src="../vendor/bootstrap/js/bootstrap.min.js"></script>

        <!-- Metis Menu Plugin JavaScript -->
        <script src="../vendor/metisMenu/metisMenu.min.js"></script>

        <!-- Custom Theme JavaScript -->
        <script src="../dist/js/sb-admin-2.js"></script>
    </form>

</body>
</html>
