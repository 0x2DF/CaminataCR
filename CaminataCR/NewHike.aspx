<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewHike.aspx.cs" Inherits="NewHike" %>


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

                                        // Longitud(s)
                                        var contador=0;
                                        var cont = [];
                                        var a = "<%=sJSON%>";
                                        var temp="";
                                        for (var i = 1; i < a.length-1; i++) {
                                            if (a[i] == ',') {
                                                cont[contador] = temp;
                                                temp = "";
                                                contador = contador + 1;
                                            } else {
                                                temp = temp + a[i];

                                            }
                                        }
                                        cont[contador] = temp;

                                        // Latitud(s)
                                        contador=0;
                                        var contLat = [];
                                        var dataLat = "<%=sJSON2%>";
                                        temp="";

                                        for (var i = 1; i < dataLat.length - 1; i++) {
                                            if (dataLat[i] == ',') {
                                                contLat[contador] = temp;
                                                temp = "";
                                                contador = contador + 1;
                                            }
                                            else {
                                                temp = temp + dataLat[i];
                                            }

                                        }
                                        contLat[contador] = temp;
    
                                        var count = "<%=numMarcadores%>";
                                        var ruta = [];
                                        // Display
                                        for (var i = 0; i < count; i++) {
                                            var longitud = parseFloat(cont[i]);
                                            var latitud = parseFloat(contLat[i]);
                                            var uluru2 = { lat: latitud, lng: longitud };
                                            ruta[i] = uluru2;

                                            var marker = new google.maps.Marker({
                                                position: uluru2,
                                                map: map,
                                                label: i.toString()
                                            });
                                        }
                                        if (count >= 2)
                                        {
                                            var path = new google.maps.Polyline({
                                                path: ruta,
                                                geodesic: true,
                                                strokeColor: '#FF0000',
                                                strokeOpacity: 1.0,
                                                strokeWeight: 2
                                            });
                                        }
                                        
                                        google.maps.event.addListener(map, 'click', function (event) {
                                            alert("[SET] Latitude: " + event.latLng.lat() + " " + ", longitude: " + event.latLng.lng());
                                            document.getElementById("tb_latitud").value = event.latLng.lat();
                                            document.getElementById("tb_longitud").value = event.latLng.lng();
                                        });
                                        
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
                                <i class="fa fa-road fa-fw"></i> Ruta
                            </div>
                            <!-- .panel-heading -->
                            <div class="panel-body">
                                <div runat="server" class="panel-group" id="accordion">
                                    <!-- Puntos -->
                                </div>
                            </div>
                            <!-- .panel-body -->
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
                                <div class="panel-group" id="accordion2">
	                                <div class="panel panel-default">
		                                <div class="panel-heading">
			                                <h4 class="panel-title">
				                                <a data-toggle="collapse" data-parent="#accordion2" href="#AddPoint"><i class="fa fa-map-marker fa-fw"></i> Agregar Punto</a>
			                                </h4>
		                                </div>
		                                <div id="AddPoint" class="panel-collapse collapse in">
			                                <div class="panel-body">
				                                <div class="list-group">
					                                <div class="row">
						                                <div class="col-lg-12">
							                                <div class="form-group">
									                            <asp:label runat="server">Puntos geograficos</asp:label>
                                                                <asp:TextBox ID="tb_longitud" placeholder="Longitud" class="form-control" runat="server"></asp:TextBox>
									                            <asp:RequiredFieldValidator id="RFV_add_longitud"
                                                                    controltovalidate="tb_longitud"
                                                                    validationgroup="add"
                                                                    errormessage="Ingrese una longitud."
                                                                    ForeColor="Red"
                                                                    runat="Server">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:TextBox ID="tb_latitud" placeholder="Latitud" class="form-control" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator id="RFV_latitud"
                                                                    controltovalidate="tb_latitud"
                                                                    validationgroup="add"
                                                                    errormessage="Ingrese una latitud."
                                                                    ForeColor="Red"
                                                                    runat="Server">
                                                                </asp:RequiredFieldValidator>
								                            </div>
                                                            <div class="form-group">
									                            <asp:label runat="server">Comentario</asp:label>
                                                                <asp:TextBox ID="tb_add_commentary" class="form-control" rows="3" placeholder="Comentario" TextMode="multiline" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator id="RFV_add_commentary"
                                                                    controltovalidate="tb_add_commentary"
                                                                    validationgroup="add"
                                                                    errormessage="Ingrese un comentario."
                                                                    ForeColor="Red"
                                                                    runat="Server">
                                                                </asp:RequiredFieldValidator>
								                            </div>
                                                            <div class="form-group">
									                            <asp:label runat="server">Fotografia</asp:label>
                                                                <asp:FileUpload ID="imageupload" runat="server"  />
                                                                <asp:RequiredFieldValidator id="RFV_image"
                                                                    controltovalidate="imageupload"
                                                                    validationgroup="add"
                                                                    errormessage="Inserte una fotografia."
                                                                    ForeColor="Red"
                                                                    runat="Server">
                                                                </asp:RequiredFieldValidator>
								                            </div>
                                                            <div class="form-group">
                                                                <asp:Label ID="AddPointErrors" runat="server" Text="" CssClass="text-danger"></asp:Label>
                                                            </div>
						                                </div>
						                                <!-- /.col-lg-6 (nested) -->
					                                </div>
					                                <!-- /.row (nested) -->
				                                </div>
				                                <!-- /.list-group -->
                                                <asp:Button ID="add_btn" runat="server" Text="Agregar Punto" OnClick="addPoint" class="btn btn-default btn-block" causesvalidation="true" ValidationGroup="add" />
			                                </div>
		                                </div>
	                                </div>
	                                <div class="panel panel-default">
		                                <div class="panel-heading">
			                                <h4 class="panel-title">
				                                <a data-toggle="collapse" data-parent="#accordion" href="#Finish"><i class="fa fa-power-off fa-fw"></i> Finalizar Caminata</a>
			                                </h4>
		                                </div>
		                                <div id="Finish" class="panel-collapse collapse">
			                                <div class="panel-body">
				                                <div class="list-group">
					                                <div class="row">
						                                <div class="col-lg-12">
                                                            <div class="form-group">
									                            <asp:label runat="server">Nivel de Calidad</asp:label>
									                            <asp:DropDownList ID="dd_qualitylevel" runat="server" class="form-control">
                                                                </asp:DropDownList>
									                            <asp:RequiredFieldValidator id="RFV_qualitylevel"
                                                                    controltovalidate="dd_qualitylevel"
                                                                    validationgroup="end"
                                                                    errormessage="Seleccione el nivel de calidad."
                                                                    ForeColor="Red"
                                                                    runat="Server">
                                                                </asp:RequiredFieldValidator>
								                            </div>
								                            <div class="form-group">
									                            <asp:label runat="server">Nivel de Precio</asp:label>
									                            <asp:DropDownList ID="dd_pricelevel" runat="server" class="form-control">
                                                                </asp:DropDownList>
									                            <asp:RequiredFieldValidator id="RFV_pricelevel"
                                                                    controltovalidate="dd_pricelevel"
                                                                    validationgroup="end"
                                                                    errormessage="Seleccione el nivel de precio."
                                                                    ForeColor="Red"
                                                                    runat="Server">
                                                                </asp:RequiredFieldValidator>
								                            </div>
								                            <div class="form-group">
									                            <asp:label runat="server">Comentario</asp:label>
									                            <asp:TextBox ID="tb_end_commentary" class="form-control" rows="3" placeholder="Comentario" TextMode="multiline" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator id="RFV_end_commentary"
                                                                    controltovalidate="tb_end_commentary"
                                                                    validationgroup="end"
                                                                    errormessage="Ingrese los detalles de la Caminata."
                                                                    ForeColor="Red"
                                                                    runat="Server">
                                                                </asp:RequiredFieldValidator>
								                            </div>
                                                            <div class="form-group">
                                                                <asp:Label ID="FinishErrors" runat="server" Text="" CssClass="text-danger"></asp:Label>
                                                            </div>
						                                </div>
						                                <!-- /.col-lg-6 (nested) -->
					                                </div>
					                                <!-- /.row (nested) -->
				                                </div>
				                                <!-- /.list-group -->
                                                <asp:Button ID="end_btn" runat="server" Text="Finalizar Caminata" OnClick="finalizeHike" class="btn btn-default btn-block" causesvalidation="true" ValidationGroup="end"/>
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