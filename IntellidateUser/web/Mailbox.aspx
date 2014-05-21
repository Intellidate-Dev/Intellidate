<%@ Page Title="" Language="C#" MasterPageFile="~/web/UserSite.Master" AutoEventWireup="true" CodeBehind="Mailbox.aspx.cs" Inherits="IntellidateUser.web.Mailbox" %>

<%@ Register Src="~/web/uc/uc_mailbox.ascx" TagPrefix="uc1" TagName="uc_mailbox" %>
<%@ Register Src="~/web/uc/uc_search.ascx" TagPrefix="uc1" TagName="uc_search" %>



<asp:Content ID="Content1" ContentPlaceHolderID="objHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="objContentPlaceHolder" runat="server">
    <div class="row" style="background-color: #FFFFFF; padding-left: 50px; padding-right: 50px;">
        <!--page title row-->
        <div class="col-lg-12">
            <h3 class="page-title">mail box </h3>
        </div>
    </div>
    <!--end of page title row-->

    <div class="row" style="background-color: #FFFFFF; padding-left: 50px; padding-right: 50px;">
        <!-- title seperatar row-->
        <div class="col-lg-12 title-seperatar"></div>
    </div>
    <!--end of title seperatar row-->

    <div class="row" style="background-color: #FFFFFF; padding-left: 50px; padding-right: 50px;">
        <!--second level title row-->
        <div class="col-lg-1">
            <span class="glyphicon  glyphicon-save my-glyphicon-save"></span>
        </div>
        <div class="col-lg-1">
            <h3 class="page-title">inbox </h3>
        </div>
        <div class="col-lg-1 col-xs-offset-1" style="margin-top: 12px;">
            <button class="btn btn-default disabled">Delete </button>
        </div>

        <div class="col-lg-4 col-xs-offset-4" style="margin-top: 12px;">
            <uc1:uc_search runat="server" id="uc_search" />
            <!-- /input-group -->
        </div>
        <!-- /.col-lg-6 -->

    </div>
    <!--end of second level title row-->


    <div class="row" style="background-color: #FFFFFF; min-height: 500px; height: auto; padding-left: 50px; padding-right: 50px;">
        <!--third row-->
        <uc1:uc_mailbox runat="server" ActiveID="2" id="uc_mailbox" />
        <!--end of LHS column-->

        <div class="col-lg-9 rounded-corners RHS-background" style="width: 822px; min-height: 450px; margin-left: 10px;">
            <!--RHS Column-->
            <div class="row bottom-border-for-heading" style="padding-top: 10px; padding-bottom: 10px; background-color: #f1f5e0">
                <!--RHS main row-->
                <div class="col-lg-1">
                    <input type="checkbox" class="my-checkbox">
                </div>
                <div class="col-lg-2" style="margin-top: 5px; color: #888;"><b>From </b></div>
                <div class="col-lg-2 col-xs-offset-5" style="margin-top: 5px; color: #888;"><b>Overall match</b> </div>
                <div class="col-lg-2" style="margin-top: 5px; color: #888;"><b>Date /Time </b></div>
            </div>
            <!-- End RHS main row-->

            <div class="row" style="padding-bottom: 5px;">
                <!--RHS content row-->
                <div class="col-lg-1">
                    <input type="checkbox" class="my-checkbox" style="vertical-align: -30px;">
                </div>
                <div class="col-lg-2" style="margin: 10px 0;">
                    <img src="images/image-mail.png" height="54" width="54" class="rounded-corners" /></div>
                <div class="col-lg-5" style="margin-top: 5px;"><span style="font-size: 17px; color: #217e7c; font-weight: bold;">Gwyneth Graham</span>
                    <br>
                    Message content goes here. Message content goes here. Message content goes here.  </div>
                <div class="col-lg-2" style="margin-top: 10px; color: #666;"><span class="percentage-title">72</span>% </div>
                <div class="col-lg-1" style="margin-top: 10px; color: #666;"><span style="vertical-align: -18px;">8:42am</span> </div>
                <div class="col-lg-1" style="margin-top: 10px;"><span style="vertical-align: -18px; color: #c02727;" class="glyphicon glyphicon-remove"></span></div>
            </div>
            <!-- End RHS content row-->

            <div class="row">
                <!--RHS main seperator row-->
                <div class="col-lg-12 bottom-border-for-list"></div>
            </div>
            <!-- End RHS main seperator row-->
            <div class="row" style="padding-bottom: 5px;">
                <!--RHS content row-->
                <div class="col-lg-1">
                    <input type="checkbox" class="my-checkbox" style="vertical-align: -30px;">
                </div>
                <div class="col-lg-2" style="margin: 10px 0;">
                    <img src="images/image-mail.png" height="54" width="54" class="rounded-corners" /></div>
                <div class="col-lg-5" style="margin-top: 5px;"><span style="font-size: 17px; color: #217e7c; font-weight: bold;">Gwyneth Graham</span>
                    <br>
                    Message content goes here. Message content goes here. Message content goes here.  </div>
                <div class="col-lg-2" style="margin-top: 10px; color: #666;"><span class="percentage-title">72</span>% </div>
                <div class="col-lg-1" style="margin-top: 10px; color: #666;"><span style="vertical-align: -18px;">8:42am</span> </div>
                <div class="col-lg-1" style="margin-top: 10px;"><span style="vertical-align: -18px; color: #c02727;" class="glyphicon glyphicon-remove"></span></div>
            </div>
            <!-- End RHS content row-->

            <div class="row">
                <!--RHS main seperator row-->
                <div class="col-lg-12 bottom-border-for-list"></div>
            </div>
            <!-- End RHS main seperator row-->
            <div class="row" style="padding-bottom: 5px;">
                <!--RHS content row-->
                <div class="col-lg-1">
                    <input type="checkbox" class="my-checkbox" style="vertical-align: -30px;">
                </div>
                <div class="col-lg-2" style="margin: 10px 0;">
                    <img src="images/image-mail.png" height="54" width="54" class="rounded-corners" /></div>
                <div class="col-lg-5" style="margin-top: 5px;"><span style="font-size: 17px; color: #217e7c; font-weight: bold;">Gwyneth Graham</span>
                    <br>
                    Message content goes here. Message content goes here. Message content goes here.  </div>
                <div class="col-lg-2" style="margin-top: 10px; color: #666;"><span class="percentage-title">72</span>% </div>
                <div class="col-lg-1" style="margin-top: 10px; color: #666;"><span style="vertical-align: -18px;">8:42am</span> </div>
                <div class="col-lg-1" style="margin-top: 10px;"><span style="vertical-align: -18px; color: #c02727;" class="glyphicon glyphicon-remove"></span></div>
            </div>
            <!-- End RHS content row-->

            <div class="row">
                <!--RHS main seperator row-->
                <div class="col-lg-12 bottom-border-for-list"></div>
            </div>
            <!-- End RHS main seperator row-->
            <div class="row" style="padding-bottom: 5px;">
                <!--RHS content row-->
                <div class="col-lg-1">
                    <input type="checkbox" class="my-checkbox" style="vertical-align: -30px;">
                </div>
                <div class="col-lg-2" style="margin: 10px 0;">
                    <img src="images/image-mail.png" height="54" width="54" class="rounded-corners" /></div>
                <div class="col-lg-5" style="margin-top: 5px;"><span style="font-size: 17px; color: #217e7c; font-weight: bold;">Gwyneth Graham</span>
                    <br>
                    Message content goes here. Message content goes here. Message content goes here.  </div>
                <div class="col-lg-2" style="margin-top: 10px; color: #666;"><span class="percentage-title">72</span>% </div>
                <div class="col-lg-1" style="margin-top: 10px; color: #666;"><span style="vertical-align: -18px;">8:42am</span> </div>
                <div class="col-lg-1" style="margin-top: 10px;"><span style="vertical-align: -18px; color: #c02727;" class="glyphicon glyphicon-remove"></span></div>
            </div>
            <!-- End RHS content row-->
            <div class="row">
                <!--RHS main seperator row-->
                <div class="col-lg-12 bottom-border-for-list"></div>
            </div>
            <!-- End RHS main seperator row-->


        </div>
        <!--end of RHS column-->

    </div>
    <!--end of third row-->
</asp:Content>
