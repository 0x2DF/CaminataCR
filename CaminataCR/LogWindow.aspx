<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogWindow.aspx.cs" Inherits="LogWindow" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>

    <title>Consulta de bitácora</title>

    <!-- Bootstrap Core CSS -->
    <link href="../vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>

    <!-- MetisMenu CSS -->
    <link href="../vendor/metisMenu/metisMenu.min.css" rel="stylesheet"/>

    <!-- Custom CSS -->
    <link href="../dist/css/sb-admin-2.css" rel="stylesheet"/>

    <!-- Custom Fonts -->
    <link href="../vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>

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
                    <a href="LogWindow.aspx" class="navbar-brand" style="color: white">Administradores</a>
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
                                <a href="LogWindow.aspx" style="color: white; background: green"><i class="fa fa-table fa-fw"></i>Bitácora</a>
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

                    <asp:CheckBox ID="ChxActivarFecha" runat="server" Text="Consulta por fecha"/>
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="LblInicio" runat="server" Text="Inicio:"></asp:Label>
                            <asp:TextBox ID="DtInicio" runat="server" Class="form-control" placeholder="Fecha Inicio" type="date"></asp:TextBox>
                            <asp:Label ID="LblFinal" runat="server" Text="Final:"></asp:Label>
                            <asp:TextBox ID="DtFinal" runat="server" Class="form-control" placeholder="Fecha Final" type="date"></asp:TextBox>
                        </div>
                        <!-- /.col-lg-12 -->
                    </div>
                    <!-- /.row -->
                                        
                    <asp:CheckBox ID="ChxActivarHora" runat="server" Text="Consulta por hora"/>
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="LblHora" runat="server" Text="Hora"></asp:Label>
                            <asp:Label ID="LblMin" runat="server" Text="Minuto"></asp:Label>
                            <asp:Label ID="LblSeg" runat="server" Text="Segundo"></asp:Label>    
                        </div>

                        <div class="col-lg-12">
                            <asp:DropDownList ID="DDLstHora" runat="server"></asp:DropDownList>
                            <asp:DropDownList ID="DDLstMin" runat="server"></asp:DropDownList>
                            <asp:DropDownList ID="DDLstSeg" runat="server"></asp:DropDownList>
                        </div>
                        <!-- /.col-lg-12 -->
                    </div>
                    <!-- /.row -->
                    
                    <asp:CheckBox ID="ChxActivarTipoCambio" runat="server" Text="Consulta por tipo de cambio"/>
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="LblTipoCambio" runat="server" Text="Tipo de Cambio"></asp:Label>
                            <asp:DropDownList ID="DDLstTipoCambio" runat="server"></asp:DropDownList>
                        </div>
                        <!-- /.col-lg-12 -->
                    </div>
                    <!-- /.row -->

                    <asp:CheckBox ID="ChxActivarObjeto" runat="server" Text="Consulta por objeto"/>
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="LblObjeto" runat="server" Text="Objeto"></asp:Label>
                            <asp:DropDownList ID="DDLstObjeto" runat="server"></asp:DropDownList>
                        </div>
                        <!-- /.col-lg-12 -->
                    </div>
                    <!-- /.row -->

                    <asp:Button ID="BtnConsulta" runat="server" Text="Consulta" OnClick="Query" />
                                                               
                </div>
                <!-- /.container-fluid -->

                <asp:Table ID="LogTable" runat="server"></asp:Table>

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

