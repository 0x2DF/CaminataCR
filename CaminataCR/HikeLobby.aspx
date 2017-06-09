<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HikeLobby.aspx.cs" Inherits="HikeLobby" %>

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

                                        // Display
                                        for (var i = 0; i < count; i++) {
                                            var longitud = parseFloat(cont[i]);
                                            var latitud = parseFloat(contLat[i]);
                                            var uluru2 = { lat: latitud, lng: longitud };

                                            var marker = new google.maps.Marker({
                                                position: uluru2,
                                                map: map
                                            });
                                        }

                                        google.maps.event.addListener(map, 'click', function (event) {
                                            alert("[SET] Latitude: " + event.latLng.lat() + " " + ", longitude: " + event.latLng.lng());
                                            document.getElementById("tb_filter_latitud").value = event.latLng.lat();
                                            document.getElementById("tb_filter_longitud").value = event.latLng.lng();

                                            document.getElementById("tb_init_latitud").value = event.latLng.lat();
                                            document.getElementById("tb_init_longitud").value = event.latLng.lng();
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
                                <i class="fa fa-list-alt fa-fw"></i> Caminatas
                            </div>
                            <!-- /.panel-heading -->
                            <div class="panel-body">
                                <div runat="server" id="hikeFilterContainer" class="list-group">

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
				                                <a data-toggle="collapse" data-parent="#accordion" href="#Filter"><i class="fa fa-filter fa-fw"></i> Filtrar Caminata</a>
			                                </h4>
		                                </div>
		                                <div id="Filter" class="panel-collapse collapse">
			                                <div class="panel-body">
				                                <div class="list-group">
					                                <div class="row">
						                                <div class="col-lg-12">
							                                <div role="form">
								                                <div class="form-group">
									                                <asp:label runat="server" class="checkbox-inline">
                                                                        <asp:CheckBox runat="server" ID="cb_filter_name"></asp:CheckBox> <asp:label runat="server"> Nombre del Lugar</asp:label>
                                                                    </asp:label>
                                                                    <asp:TextBox ID="tb_filter_name" class="form-control" runat="server"></asp:TextBox>
								                                </div>
								                                <div class="form-group">
									                                <asp:label runat="server" class="checkbox-inline">
                                                                         <asp:CheckBox runat="server" ID="cb_filter_direction"></asp:CheckBox> <asp:label runat="server"> Direccion Exacta</asp:label>
                                                                    </asp:label>
                                                                    <asp:DropDownList ID="dd_filter_province" runat="server" class="form-control">
                                                                    </asp:DropDownList>
                                                                     <br />
                                                                    <asp:Button ID="btn_filter_canton" runat="server" Text="Llenar" OnClick="FillFilterCanton" class="btn btn-default" causesvalidation="false"/>
                                                                    <br /><br />
                                                                    <asp:DropDownList ID="dd_filter_canton" runat="server" class="form-control">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator id="RFV_filter_Canton"
                                                                        controltovalidate="dd_filter_canton"
                                                                        validationgroup="dropdown"
                                                                        errormessage="Seleccione un canton primero."
                                                                        ForeColor="Red"
                                                                        runat="Server">
                                                                    </asp:RequiredFieldValidator>
                                                                    <br />
                                                                    <asp:Button ID="btn_filter_distrito" runat="server" Text="Llenar" OnClick="FillFilterDistrict" class="btn btn-default" causesvalidation="true" ValidationGroup="dropdown"/>
                                                                    <br /><br />
                                                                    <asp:DropDownList ID="dd_filter_district" runat="server" class="form-control">
                                                                    </asp:DropDownList>
								                                </div>
								                                <div class="form-group">
									                                <asp:label runat="server" class="checkbox-inline">
                                                                         <asp:CheckBox runat="server" ID="cb_filter_initpoint"></asp:CheckBox> <asp:label runat="server"> Punto de Partida</asp:label>
                                                                    </asp:label>
                                                                    <asp:TextBox ID="tb_filter_longitud" class="form-control" placeholder="Longitud" runat="server"></asp:TextBox>
									                                <br />
                                                                    <asp:TextBox ID="tb_filter_latitud" class="form-control" placeholder="Latitud" runat="server"></asp:TextBox>
								                                </div>

								                                <div class="form-group">
									                                <asp:label runat="server" class="checkbox-inline">
                                                                         <asp:CheckBox runat="server" ID="cb_filter_hiketype"></asp:CheckBox> <asp:label runat="server"> Tipo de Caminata</asp:label>
                                                                    </asp:label>
									                                <asp:DropDownList ID="dd_filter_hiketype" runat="server" class="form-control">
                                                                    </asp:DropDownList>
								                                </div>

								                                <div class="form-group">
									                                <asp:label runat="server" class="checkbox-inline">
                                                                         <asp:CheckBox runat="server" ID="cb_filter_difficultylevel"></asp:CheckBox> <asp:label runat="server"> Nivel de Dificultad</asp:label>
                                                                    </asp:label>
									                               <asp:DropDownList ID="dd_filter_difficultylevel" runat="server" class="form-control">
                                                                    </asp:DropDownList>
								                                </div>

                                                                <div class="form-group">
                                                                    <asp:label runat="server" class="checkbox-inline">
                                                                         <asp:CheckBox runat="server" ID="cb_filter_pricelevel"></asp:CheckBox> <asp:label runat="server"> Nivel de Precio</asp:label>
                                                                    </asp:label>
									                                <asp:DropDownList ID="dd_filter_pricelevel" runat="server" class="form-control">
                                                                    </asp:DropDownList>
								                                </div>

                                                                <div class="form-group">
									                                <asp:label runat="server" class="checkbox-inline">
                                                                         <asp:CheckBox runat="server" ID="cb_filter_qualitylevel"></asp:CheckBox> <asp:label runat="server"> Nivel de Calidad</asp:label>
                                                                    </asp:label>
									                                <asp:DropDownList ID="dd_filter_qualitylevel" runat="server" class="form-control">
                                                                    </asp:DropDownList>
								                                </div>

                                                                <div class="form-group">
                                                                    <asp:Label ID="FilterErrors" runat="server" Text="" CssClass="text-danger"></asp:Label>
                                                                </div>
							                                </div>
						                                </div>
						                                <!-- /.col-lg-6 (nested) -->
					                                </div>
					                                <!-- /.row (nested) -->
				                                </div>
				                                <!-- /.list-group -->
                                                <asp:Button ID="filter_btn" runat="server" Text="Filtrar" OnClick="filterHikes" class="btn btn-default btn-block" causesvalidation="false" />
			                                </div>
		                                </div>
	                                </div>
	                                <div class="panel panel-default">
		                                <div class="panel-heading">
			                                <h4 class="panel-title">
				                                <a data-toggle="collapse" data-parent="#accordion" href="#Initiate"><i class="fa fa-cog fa-fw"></i> Iniciar Caminata</a>
			                                </h4>
		                                </div>
		                                <div id="Initiate" class="panel-collapse collapse">
			                                <div class="panel-body">
				                                <div class="list-group">
					                                <div class="row">
						                                <div class="col-lg-12">
								                            <div class="form-group">
									                            <asp:label runat="server">Nombre del Lugar</asp:label>
                                                                <asp:TextBox ID="tb_init_name" class="form-control" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator id="RFV_name"
                                                                    controltovalidate="tb_init_name"
                                                                    validationgroup="init"
                                                                    errormessage="Ingrese el nombre del lugar de la Caminata."
                                                                    ForeColor="Red"
                                                                    runat="Server">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
								                            <div class="form-group">
									                            <asp:label runat="server">Direccion exacta</asp:label>
									                            <asp:DropDownList ID="dd_init_province" runat="server" class="form-control">
                                                                </asp:DropDownList>
									                            <asp:RequiredFieldValidator id="RFV_province"
                                                                    controltovalidate="dd_init_province"
                                                                    validationgroup="init"
                                                                    errormessage="Ingrese la provincia de la Caminata."
                                                                    ForeColor="Red"
                                                                    runat="Server">
                                                                </asp:RequiredFieldValidator>
                                                                <br />
                                                                <asp:Button ID="btn_init_canton" runat="server" Text="Llenar" OnClick="FillInitCanton" class="btn btn-default" causesvalidation="false"/>
									                            <br />
                                                                <asp:RequiredFieldValidator id="RFV_canton"
                                                                    controltovalidate="dd_init_canton"
                                                                    validationgroup="init"
                                                                    errormessage="Ingrese el canton de la Caminata."
                                                                    ForeColor="Red"
                                                                    runat="Server">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:DropDownList ID="dd_init_canton" runat="server" class="form-control">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator id="RFV_init_canton"
	                                                                controltovalidate="dd_init_canton"
	                                                                validationgroup="dropdown_init"
	                                                                errormessage="Seleccione un canton primero."
	                                                                ForeColor="Red"
	                                                                runat="Server">
                                                                </asp:RequiredFieldValidator>
                                                                <br />
                                                                <asp:Button ID="btn_init_district" runat="server" Text="Llenar" OnClick="FillInitDistrict" class="btn btn-default" causesvalidation="true" ValidationGroup="dropdown_init"/>
									                            <br /><br />
                                                                <asp:DropDownList ID="dd_init_district" runat="server" class="form-control">
                                                                </asp:DropDownList>
									                            <asp:RequiredFieldValidator id="RFV_district"
                                                                    controltovalidate="dd_init_district"
                                                                    validationgroup="init"
                                                                    errormessage="Ingrese el distrito de la Caminata."
                                                                    ForeColor="Red"
                                                                    runat="Server">
                                                                </asp:RequiredFieldValidator>
									                            <asp:TextBox ID="tb_init_details" class="form-control" rows="3" placeholder="Detalles" TextMode="multiline" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator id="RFV_details"
                                                                    controltovalidate="tb_init_details"
                                                                    validationgroup="init"
                                                                    errormessage="Ingrese los detalles de la Caminata."
                                                                    ForeColor="Red"
                                                                    runat="Server">
                                                                </asp:RequiredFieldValidator>
								                            </div>

								                            <div class="form-group">
									                            <asp:label runat="server">Punto de Partida</asp:label>
                                                                <asp:TextBox ID="tb_init_longitud" placeholder="Longitud" class="form-control" runat="server"></asp:TextBox>
									                            <asp:RequiredFieldValidator id="RFV_longitud"
                                                                    controltovalidate="tb_init_longitud"
                                                                    validationgroup="init"
                                                                    errormessage="Ingrese una longitud."
                                                                    ForeColor="Red"
                                                                    runat="Server">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:TextBox ID="tb_init_latitud" placeholder="Latitud" class="form-control" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator id="RFV_latitud"
                                                                    controltovalidate="tb_init_latitud"
                                                                    validationgroup="init"
                                                                    errormessage="Ingrese una latitud."
                                                                    ForeColor="Red"
                                                                    runat="Server">
                                                                </asp:RequiredFieldValidator>
								                            </div>

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

                                                            <div class="form-group">
                                                                <asp:Label ID="InitErrors" runat="server" Text="" CssClass="text-danger"></asp:Label>
                                                            </div>
						                                </div>
						                                <!-- /.col-lg-6 (nested) -->
					                                </div>
					                                <!-- /.row (nested) -->
				                                </div>
				                                <!-- /.list-group -->
                                                <asp:Button ID="init_btn" runat="server" Text="Iniciar Caminata" OnClick="InitHike" class="btn btn-default btn-block" causesvalidation="true" ValidationGroup="init"/>
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