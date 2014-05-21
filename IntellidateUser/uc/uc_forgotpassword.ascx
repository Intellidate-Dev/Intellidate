<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_forgotpassword.ascx.cs" Inherits="IntellidateUser.uc.uc_forgotpassword" %>
<div class="row">
    <!--formContainer row-->
    <div class="col-lg-8  col-xs-offset-2 bottom-rounded-corners top-rounded-corners" style="background-color: #eaeff3; height: auto; min-height: 100px; margin-top: 10px; padding-top: 15px;">
        <!--Parent column-->
        <form role="form">

            <div class="row">
                <!--nested row for submit button-->
                <div class="col-lg-8 ">
                    <input style="width: 500px;" type="email" class="txt-align-left form-control" id="txtEmail" name="email" placeholder="enter email address">
                </div>
                <div class="col-lg-4 " style="margin-top: 15px;">
                    <button type="button" id="btnSendPassword" class="btn btn-primary" style="width: 225px; letter-spacing: 1.5px;"><span class="login-button">Send Password </span></button>
                </div>
            </div>
            <!--end of nested row for forgot submit button-->
        </form>
    </div>
    <!--end of Parent column-->
</div>
<!--end of formContainer row-->
<script src="../js/jsforgotpassword.js"></script>
