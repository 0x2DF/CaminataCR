<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowHike.aspx.cs" Inherits="ShowHike" %>

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
        #map {
        height: 400px;
        width: 100%;
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
                        <h1 class="page-header">Caminata</h1>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->
                <div class="row">
                    <div class="col-lg-8">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <i class="fa fa-globe fa-fw"></i> Mapa
                                <div class="pull-right">
                                    <div class="btn-group">
                                        <button type="button" class="btn btn-default btn-xs dropdown-toggle" data-toggle="dropdown">
                                            Actions
                                            <span class="caret"></span>
                                        </button>
                                        <ul class="dropdown-menu pull-right" role="menu">
                                            <li><a href="#">Action</a>
                                            </li>
                                            <li><a href="#">Another action</a>
                                            </li>
                                            <li><a href="#">Something else here</a>
                                            </li>
                                            <li class="divider"></li>
                                            <li><a href="#">Separated link</a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <!-- /.panel-heading -->
                            <div class="panel-body">
                                <div id="map" class="auto-style8"></div>
                                <script>
                                    function initMap(arreglo) {
            
                                        var uluru = {lat: 10, lng: -84};
                                        var map = new google.maps.Map(document.getElementById('map'), {
                                            zoom: 7,
                                            center: uluru
                                        });

                                        //List of Lengths
                                        var k = 0;
                                        var len = [];
                                        var n = "<%=sJSON3%>";
                                        var temp = "";
                                        for (var i = 1; i < n.length - 1; ++i)
                                        {
                                            if(n[i] == ',')
                                            {
                                                len[k] = temp;
                                                temp = "";
                                                k++;
                                            }else
                                            {
                                                temp = temp + n[i];
                                            }
                                        }
                                        len[k] = temp;
                                        
                                        //Init Arrays
                                        var l = "<%=numMarcadores%>";
                                        var listOfRoutes = new Array();
                                        var listOfRoutes2 = new Array();
                                        for (var i = 0; i < l; ++i)
                                        {
                                            listOfRoutes[i] = new Array();
                                            listOfRoutes2[i] = new Array();
                                        }

                                        //Longitud(s)
                                        k = 0;
                                        var n = "<%=sJSON%>";
                                        var temp = "";
                                        for (var i = 1; i < n.length - 1; ++i)
                                        {
                                            if(n[i] == '[')
                                            {
                                                for(var j = 0; j < len[k];)
                                                {
                                                    i++;
                                                    if(n[i] == ',')
                                                    {
                                                        listOfRoutes[k][j] = temp;
                                                        temp = "";
                                                        j++;
                                                    }else if(n[i] == ']')
                                                    {
                                                        listOfRoutes[k][j] = temp;
                                                        temp = "";
                                                        j++;
                                                    } else
                                                    {
                                                        temp = temp + n[i];
                                                    }
                                                }
                                            }else if(n[i] == ',')
                                            {
                                                k++;
                                            }
                                        }

                                        //Latituds(s)
                                        k = 0;
                                        var n = "<%=sJSON2%>";
                                        var temp = "";
                                        for (var i = 1; i < n.length - 1; ++i)
                                        {
                                            if(n[i] == '[')
                                            {
                                                for(var j = 0; j < len[k];)
                                                {
                                                    i++;
                                                    if(n[i] == ',')
                                                    {
                                                        listOfRoutes2[k][j] = temp;
                                                        temp = "";
                                                        j++;
                                                    }else if(n[i] == ']')
                                                    {
                                                        listOfRoutes2[k][j] = temp;
                                                        temp = "";
                                                        j++;
                                                    } else
                                                    {
                                                        temp = temp + n[i];
                                                    }
                                                }
                                            }else if(n[i] == ',')
                                            {
                                                k++;
                                            }
                                        }
                                        
                                        // Display
                                        for (var i = 0; i < l; i++) {
                                            for (var j = 0; j < len[i]; j++) {
                                                var longitud = parseFloat(listOfRoutes[i][j]);
                                                var latitud = parseFloat(listOfRoutes2[i][j]);
                                                var uluru2 = { lat: latitud, lng: longitud };
                                                if (i == 0)
                                                {
                                                    var marker = new google.maps.Marker({
                                                        position: uluru2,
                                                        map: map,
                                                        label: "*"
                                                    });
                                                }else{
                                                    var marker = new google.maps.Marker({
                                                        position: uluru2,
                                                        map: map,
                                                        label: i.toString()+"."+j.toString()
                                                    });
                                                }
                                                
                                            }
                                        }

                                        // Zoom to 9 when clicking on marker
                                        google.maps.event.addListener(marker, 'click', function () {
                                            map.setZoom(9);
                                            map.setCenter(marker.getPosition());
                                        });
                                    }
                                </script>
                                <script async defer
                                src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDNiuZBgQltcbPmeB4uSTb_-c2tMjIs888&callback=initMap">
                                </script>
                            </div>
                            <!-- /.panel-body -->
                        </div>
                        <!-- /.panel -->
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <i class="fa fa-list-alt fa-fw"></i> Rutas
                            </div>
                            <!-- /.panel-heading -->
                            <div class="panel-body">
                                <div runat="server" id="hikeContainer" class="list-group">

                                    <!-- Caminatas -->

                                </div>
                                <!-- /.list-group -->
                            </div>
                            <!-- /.panel-body -->
                        </div>
                        <!-- /.panel -->
                    </div>
                    <!-- /.col-lg-8 -->
                    <div class="col-lg-4">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <i class="fa fa-th-list fa-fw"></i> Indice
                            </div>
                            <!-- /.panel-heading -->
                            <div class="panel-body">
                                <div class="panel-group" id="accordion">
	                                <div class="panel panel-default">
		                                <div class="panel-heading">
			                                <h4 class="panel-title">
				                                <a data-toggle="collapse" data-parent="#accordion" href="#Initiate"><i class="fa fa-plus-circle  fa-fw"></i> Nueva Ruta</a>
			                                </h4>
		                                </div>
		                                <div id="Initiate" class="panel-collapse collapse">
			                                <div class="panel-body">
				                                <div class="list-group">
					                                <div class="row">
						                                <div class="col-lg-12">
								                            <div class="form-group">
									                            <asp:label runat="server">Tipo de Caminata</asp:label>
									                            <asp:DropDownList ID="dd_init_hiketype" runat="server" class="form-control">
                                                                </asp:DropDownList>
									                            <asp:RequiredFieldValidator id="RFV_hiketype"
                                                                    controltovalidate="dd_init_hiketype"
                                                                    validationgroup="init"
                                                                    errormessage="Seleccione el tipo de caminata."
                                                                    ForeColor="Red"
                                                                    runat="Server">
                                                                </asp:RequiredFieldValidator>
								                            </div>

								                            <div class="form-group">
									                            <asp:label runat="server">Nivel de Dificultad</asp:label>
									                            <asp:DropDownList ID="dd_init_difficultylevel" runat="server" class="form-control">
                                                                </asp:DropDownList>
									                            <asp:RequiredFieldValidator id="RFV_difficultylevel"
                                                                    controltovalidate="dd_init_difficultylevel"
                                                                    validationgroup="init"
                                                                    errormessage="Seleccione el nivel de dificultad."
                                                                    ForeColor="Red"
                                                                    runat="Server">
                                                                </asp:RequiredFieldValidator>
								                            </div>

								                            <div class="form-group">
									                            <asp:label runat="server">Fotografia</asp:label>
                                                                <asp:FileUpload ID="imageupload" runat="server"  />
								                            </div>
						                                </div>
						                                <!-- /.col-lg-6 (nested) -->
					                                </div>
					                                <!-- /.row (nested) -->
				                                </div>
				                                <!-- /.list-group -->
                                                <asp:Button ID="init_btn" runat="server" Text="Iniciar Ruta" OnClick="InitRoute" class="btn btn-default btn-block" causesvalidation="true" ValidationGroup="init"/>
			                                </div>
		                                </div>
	                                </div>
                                </div>
                            </div>
                            <!-- /.panel-body -->
                        </div>
                        <!-- /.panel -->
                    </div>
                    <!-- /.col-lg-4 -->
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