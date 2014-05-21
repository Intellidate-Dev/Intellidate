<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_register.ascx.cs" Inherits="IntellidateUser.uc.uc_register" %>
<style>
    .displaynone {
        display: none;
    }
    .redColor{
        color:red;
    }
    .nextButton{
        margin-left: 240px; width: 100px;
    } 
</style>
<div class="row" style="margin-top: 10px;">
    <div class="col-lg-3"></div>
    <div class="col-lg-6 top-rounded-corners" style="background-color: #2d6ca2; height: 60px; text-align: center;">
        <h2 class="reg-form-title">Create Your FREE Account</h2>
    </div>
    <div class="col-lg-3"></div>
</div>
<!--end of row-->

<div class="row">
    <div class="col-lg-3"></div>
    <div class="col-lg-6 bottom-rounded-corners" style="background-color: #eaeff3; height: auto; min-height: 382px;">

        <div class="row">
            <div class="form-group">
                <div class="col-lg-5">
                    <label class="help-block txt-align-right">Username </label>
                </div>
                <div class="col-lg-5">
                    <input type="text" style="width: 220px;" class="txtUserName txt-align-left form-control" id="txtUserName" runat="server" placeholder="User Name">
                </div>
                <div class="col-lg-2" style="padding-left: 0px;"><span id="resultUserName" class="displaynone" style="margin-top: 25px;"></span></div>
            </div>
        </div>

        <div class="row displaynone" id="lblUserNameError">
            <div class="col-lg-5 col-xs-offset-5">
                <div class="alert alert-danger" style="width: 220px;" id="lblUserNameErrorMessage"></div>
            </div>
        </div>

        <div class="row">
            <div class="form-group">
                <div class="col-lg-5">
                    <label class="help-block txt-align-right">Email </label>
                </div>
                <div class="col-lg-5">
                    <input type="email" style="width: 220px;" class="txtEmail txt-align-left  form-control" id="txtEmail" runat="server" placeholder="Email Address">
                </div>
                <div class="col-lg-2" style="padding-left: 0px;"><span id="resultEmailAddress" class="displaynone" style="margin-top: 25px;"></span></div>
            </div>
        </div>

        <div class="row displaynone" id="lblEmailError">
            <div class="col-lg-5 col-xs-offset-5">
                <div class="alert alert-danger" style="width: 220px;" id="lblEmailErrorMessage"></div>
            </div>
        </div>


        <div class="row">
            <div class="form-group">
                <div class="col-lg-5">
                    <label class="help-block txt-align-right">Re-enter Email </label>
                </div>
                <div class="col-lg-5">
                    <input type="email" style="width: 220px;" class="txtRetypeEmail txt-align-left  form-control" id="txtRetypeEmail" runat="server" placeholder="Re-enter Email Address">
                </div>
                <div class="col-lg-2" style="padding-left: 0px;"><span id="resultREmailAddress" class="displaynone" style="margin-top: 25px;"></span></div>
            </div>
        </div>

        <div class="row displaynone" id="lblREmailError">
            <div class="col-lg-5 col-xs-offset-5">
                <div class="alert alert-danger" style="width: 220px;" id="lblREmailErrorMessage"></div>
            </div>
        </div>

        <div class="row">
            <div class="form-group">
                <div class="col-lg-5">
                    <label class="help-block txt-align-right">Password </label>
                </div>
                <div class="col-lg-5">
                    <input type="password" style="width: 220px;" class="txtPassword txt-align-left  form-control" id="txtPassword" runat="server" placeholder="Password">
                </div>
                <div class="col-lg-2" style="padding-left: 0px;"><span id="resultPassword" class="displaynone" style="margin-top: 25px;"></span></div>
            </div>
        </div>

        <div class="row displaynone" id="lblPasswordError">
            <div class="col-lg-5 col-xs-offset-5">
                <div class="alert alert-danger" style="width: 220px;" id="lblPasswordErrorMessage"></div>
            </div>
        </div>

        <div class="row">
            <div class="form-group">
                <div class="col-lg-5">
                    <label class="help-block txt-align-right">Re-enter password</label>
                </div>
                <div class="col-lg-5">
                    <input type="password" style="width: 220px;" class="txtRPassword txt-align-left  form-control" id="txtRPassword" runat="server" placeholder="Re-enter Password">
                </div>
                <div class="col-lg-2" style="padding-left: 0px;">
                    <span id="resultRPassword" class="displaynone" style="margin-top: 25px;"></span>
                </div>
            </div>
        </div>


        <div class="row displaynone" id="lblRPasswordError">
            <div class="col-lg-5 col-xs-offset-5">
                <div class="alert alert-danger" style="width: 220px;" id="lblRPasswordErrorMessage"></div>
            </div>
        </div>


        <div class="row">
            <div class="form-group">
                <div class="col-lg-7" style="padding-top: 25px;">
                    <asp:Button ID="btnNext" runat="server" CssClass="nextButton btn btn-primary" OnClick="btnNext_Click" Enabled="false" Text="Next"  />
                </div>
            </div>
        </div>
        <div style="height:20px;">&nbsp;</div>

    </div>
    <div class="col-lg-3"></div>
</div>
<script src="../js/jsregister.js"></script>
<!--end of top row-->
