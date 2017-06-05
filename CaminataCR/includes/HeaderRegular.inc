<!-- Navigation -->
        <nav class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0;">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a href="Wall.aspx" class="navbar-brand">CaminataCR</a>
            </div>
            <!-- /.navbar-header -->

            <ul class="nav navbar-top-links navbar-right">

                <!-- /.dropdown -->
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                        <i class="fa fa-user fa-fw"></i><i class="fa fa-caret-down"></i>
                    </a>
                    <ul class="dropdown-menu dropdown-user">
                        <li><a href="ControlPanel.aspx"><i class="fa fa-user fa-fw"></i><asp:Label id="LoggedInUsername" runat="server"></asp:Label></a>
                        </li>
                        <li class="divider"></li>
                        <li><a href="SignOut.aspx"><i class="fa fa-sign-out fa-fw"></i>Logout</a>
                        </li>
                    </ul>
                    <!-- /.dropdown-user -->
                </li>
                <!-- /.dropdown -->
            </ul>
            <!-- /.navbar-top-links -->

            <div class="navbar-default sidebar" role="navigation" style="max-height: 10px;">
                <div class="sidebar-nav navbar-collapse" style="background-repeat: no-repeat;">
                    <ul class="nav " id="side-menu" style="background-repeat: no-repeat; background: rgb(255, 255, 255);">
                        <li>
                            <a href="Wall.aspx"><i class="fa fa-home fa-fw"></i> Muro</a>
                        </li>
                        <li>
                            <a href="SearchFriends.aspx"><i class="fa fa-sitemap fa-fw"></i> Amigos<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <a href="SearchFriends.aspx"><i class="fa fa-search-plus fa-fw"></i> Buscar Amigos</a>
                                </li>
                                <li>
                                    <a href="ManageFriends.aspx"><i class="fa fa-search-minus fa-fw"></i> Manejar Amigos</a>
                                </li>
                            </ul>
                        </li>
                        <li>
                            <a href="HikeLobby.aspx"><i class="fa fa-globe fa-fw"></i> Caminata</a>
                        </li>
                        <li>
                          <a href="Donations.aspx"><i class="fa fa-dollar fa-fw"></i> Donacion</a>
                        </li>
                    </ul>
                </div>
                <!-- /.sidebar-collapse -->
            </div>
            <!-- /.navbar-static-side -->
        </nav>