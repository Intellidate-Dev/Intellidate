<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogOut.aspx.cs" Inherits="IntellidateUser.web.LogOut" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title id="objTitle" runat="server"></title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <link href="css/bootstrap.css" type="text/css" rel="stylesheet" />
    <link href="css/bootstrap-theme.css" type="text/css" rel="stylesheet" />
    <link href="css/myStyle.css" type="text/css" rel="stylesheet" />
    <link href='http://fonts.googleapis.com/css?family=Lobster' rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:300' rel='stylesheet' type='text/css' />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container">
                <div class="row">
                    <!--signout msg-->
                    <div class="col-lg-8  col-xs-offset-2" style="text-align: center; margin-top: 50px;">
                        <h2 style="color: #707070;">You've successfully signed out of Intellidate.com</h2>
                    </div>
                </div>
                <!--end of Signout msg-->


                <div class="row">
                    <!--formContainer row-->
                    <div class="col-lg-8  col-xs-offset-2 bottom-rounded-corners top-rounded-corners" style="background-color: #eaeff3; height: auto; min-height: 150px; margin-top: 50px; padding-top: 30px;">
                        <!--Parent column-->
                        <form role="form">

                            <div class="row">
                                <!--nested row for reLogin msg-->
                                <div class="col-lg-8 col-xs-offset-3">
                                    <div><span style="font-size: 20px; margin-left: 20px; color: #175F9A;">Click button below to Sign-in again</span> </div>
                                </div>
                            </div>
                            <!--end of nested row for reLogin msg-->

                            <div class="row">
                                <!--nested row for submit button-->
                                <div class="col-lg-8 col-xs-offset-4" style="margin-top: 15px; margin-left: 255px;">
                                    <button type="button" class="btn btn-primary" style="width: 220px;" id="btnLogin"><span class="login-button">L O G I N </span></button>
                                </div>
                            </div>
                            <!--end of nested row for forgot submit button-->
                        </form>
                    </div>
                    <!--end of Parent column-->
                </div>
                <!--end of formContainer row-->

                <div class="row">
                    <!--signout msg-->
                    <div class="col-lg-12  " style="text-align: center; margin-top: 50px;">
                        <h2 style="color: #a3bd96;">Statement Needed (Site promotion or App related)</h2>
                    </div>
                </div>
                <!--end of Signout msg-->

            </div>
            <!--end of main container-->
        </div>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
        <script src="//netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js"></script>
        <script src="js/jsconst.js"></script>
        <script src="js/jslogout.js"></script>
        <script src="../js/jsBackendData" type="text/javascript"></script>
    </form>
</body>
</html>
