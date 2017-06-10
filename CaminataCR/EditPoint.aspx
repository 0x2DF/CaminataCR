﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditPoint.aspx.cs" Inherits="EditPoint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Caminata</title>

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
    </style>
</head>

<body>
    <form runat="server">
    <div id="wrapper">
        <!-- #include file ="includes\HeaderRegular.inc" -->
        <!-- Page Content -->
        <div id="page-wrapper">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12">
                        <h1 class="page-header">Donaciones</h1>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->
                <div class="row">
                    <div class="col-lg-4">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                Editar Punto
                            </div>
                            <div class="panel-body">
                                <form role="form">
                                    <div class="form-group input-group">
                                        <label>Comentario</label>
                                        <asp:TextBox ID="tb_add_commentary" class="form-control" rows="3" placeholder="Comentario" TextMode="multiline" required runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group input-group">
                                        <label>Imagen</label>
                                        <asp:FileUpload ID="imageupload" required runat="server"  />
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Errors" runat="server" Text=" <br> " CssClass="text-danger"></asp:Label>
                                    </div>
                                    <asp:Button ID="Edit_btn" runat="server" Text="Editar Punto" OnClick="Edit" class="btn btn-default" />
                                </form>
                            </div>
                            <div class="panel-footer">
                                
                            </div>
                        </div>
                    </div>
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
