<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="T4.WEB._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
<title>Transport4</title>

<!-- Start: Favicon -->
<link rel="shortcut icon" type="image/x-icon" href="images/favicon.ico" />
<!-- End: Favicon -->

<!-- Start: Stylesheet-->
<link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" media="all" />
<link href="css/style.css" rel="stylesheet" type="text/css" media="all" />
<link href="css/responsive.css" rel="stylesheet" type="text/css" media="all" />
<link href="https://fonts.googleapis.com/css?family=Roboto:400,300,900,700,500italic,500,400italic,300italic" rel="stylesheet" type="text/css">
<!-- End: Stylesheet-->

</head>
<body>
    <form id="form1" runat="server">
    <div>
            <!-- Start: -->
            <!-- End: -->

            <!-- Start: Wrapper-->
            <div id="wrapper">

            <!-- header Start: -->
            <div id="header">
            <div class="container">
       	 	    <div class="header_top">
                    <style>
                .btnt4{
                font-family: 'Roboto', sans-serif;
                font-size: 13px;
                line-height: 16px;
                color: #FFF !important;
                font-weight: normal;
                text-align: center;
                display: inline-block;
                cursor: pointer;
                margin: 0 auto;
                width: 46px;
                box-shadow: none;
                border: none;
                border-radius: 3px;
	            background-color:#bc191e;
	            transition: all 0.3s ease-out 0s;
                outline: none;
                padding: 0 0 !important;
	            height: 30px;
            }
            .btnt4:hover	{ background-color:#FFF; color:#bc191e !important;}

            </style>
                </div>  
                </div>
                <nav class="navbar navbar-default menu">
                  <div class="container"> 
                    <div class="navbar-header">
                      <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false"> <span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span> <span class="icon-bar"></span> <span class="icon-bar"></span> </button>
                      <a class="navbar-brand" href="index-2.html"> <img src="images/logo.jpg"  alt="TRANSPORT" width="243" height="50"/> </a> </div>
        
                    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                      <ul class="nav navbar-nav navbar-right">
                        <li><a target="_blank" href="pdfs/VBClassCode.pdf">VB Class Code</a></li>
                        <li><a target="_new" href="pdfs/T4SQL.pdf">SQL Code</a></li>
                        <li><a target="_new" href="pdfs/ngWebAPI.pdf">AngularJS Code</a></li>
                        <li><a target="_blank" href="http://transport4.vmobileweb.com/Dashboard.aspx">Goto Dashboard</a></li>
                        <li><a target="_blank" href="http://t4angular.vmobileweb.com/Dashboard.html">AngularJS Demo</a></li>
                                                 
                      </ul>
                    </div>
                  </div>
                </nav>

            <div class="clearfix"></div>

            </div>
            <!-- header End: -->

            <!-- main_container Start: -->
            <div id="main_container">
                   <div class="reliable_section">
                          <div class="container" style="min-height:650px;" >
                              <asp:Button ID="Button1" runat="server" Text="Goto Dashboard" />
                            <h2>T4 DEMO.</h2>
                            <p>This Transport4 DEMO is designed to convey the understanding of the author as it pertains to the T4 business model and the developer skills T4 is seeking.</p>
            <br />
                              <p style="font-size:medium;" >Please know the use of T4 copyright material is only temporary and that this demo site will be removed after the intended audience views it.</p>
                          </div>
                        </div>
        
            </div>
            <!-- main_container End: -->

            <!-- footer Start: -->
            <div id="footer">
	
                <div class="footer_bottom">
    	            <div class="container">
        	            <div class="row">
            	            <div class="col-md-12 col-sm-12 col-xs-12 footer_bottom_cont">
                	            <small>&copy; 2017 Transport4 Demo</small>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- footer End: -->

            </div>
            <!-- End: Wrapper-->

            <!-- jQuery (necessary for Bootstrap's JavaScript plugins) --> 
            <script src="js/jquery.min.js"></script> 
            <!-- Include all compiled plugins (below), or include individual files as needed --> 
            <script src="js/bootstrap.min.js"></script> 

            <script type="text/javascript">
                $(window).scroll(function () {
                    var height = $(window).scrollTop();

                    if (height > 60) {
                        //   $('.navbar.navbar-default.menu').addClass("fixed", 1000);
                    }
                    else {
                        //  $('.navbar.navbar-default.menu').removeClass("fixed", 1000);
                    }
                });
            </script>
            <script src="js/site.js"></script>
    
    </div>
    </form>
</body>
</html>
