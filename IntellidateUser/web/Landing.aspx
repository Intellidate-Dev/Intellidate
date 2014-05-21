<%@ Page Title="" Language="C#" MasterPageFile="~/web/UserSite.Master" AutoEventWireup="true" CodeBehind="Landing.aspx.cs" Inherits="IntellidateUser.web.WebForm4" %>
<%@ Register Src="~/web/uc/uc_userprofile.ascx" TagPrefix="uc1" TagName="uc_userprofile" %>
<%@ Register Src="~/web/uc/uc_percentagecircle.ascx" TagPrefix="uc1" TagName="uc_percentagecircle" %>
<%@ Register Src="~/web/uc/uc_viewedprofiles.ascx" TagPrefix="uc1" TagName="uc_viewedprofiles" %>
<%@ Register Src="~/web/uc/uc_smallpercentagecircle.ascx" TagPrefix="uc1" TagName="uc_smallpercentagecircle" %>




<asp:Content ID="Content1" ContentPlaceHolderID="objHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="objContentPlaceHolder" runat="server">
  
     <div class="row" style="background-color: #E6F1EB; min-height: 500px; height: auto;">
        <!--third row-->
        <div class=" col-lg-2" style="background-color: lightgreen;">
            <!--LHS Column-->
            <div style="margin-top: 10px;">
                <!--LHS Ad Column-->
                <img src="images/ad-tower.png" />
            </div>
            <!--end of LHS Ad column-->
        </div>
        <!--end of LHS column-->

        <div class="col-lg-8" style="min-height: 250px; margin-top: 10px; padding-left: 5px;">
            <!--Middle Column-->
            <div class="row">

                <div class="col-lg-4">
                     <uc1:uc_viewedprofiles runat="server" id="uc_viewedprofiles2" Name="MyProfilePic" class="profile-pic-large;" style="width: 266px; height: 266px;" />
                </div>

                <div class="col-lg-3" style="padding-left: 30px;">
                    <uc1:uc_userprofile runat="server" ID="uc_userprofile" ControlName="UserDetails" />
                </div>

                <div class="col-lg-5">
                    <uc1:uc_percentagecircle runat="server" id="uc_percentagecircle" Name="OverAllPercent"/>
                </div>
            </div>

            <div class="row " style="margin-left: 2px;">
                <!--second row-->
                <div class="col-lg-4 best-match-bg" style="padding-left: 0px;">
                    <uc1:uc_viewedprofiles runat="server" id="uc_viewedprofiles1" Name="NextMatches"/>
                </div>
                <!--end second row-->

                <div class="col-lg-3 best-match-bg">
                    <span class="profile-lookingfor-title">other popular matching areas</span>
                </div>
                <!--end second row-->

                <div class="col-lg-5 best-match-bg" style="background-color: white; padding-left: 0px;">
                    <uc1:uc_smallpercentagecircle runat="server" ID="uc_smallpercentagecircle1" Name="CriteriaWisePercents" />
                </div>
                <!--end second row-->
            </div>
            <!-- end second row-->

            <div class="row" style="margin-left: 2px;">
                <!--third row-->
                <div class="col-lg-12 recently-viewed-bg ">
                    <span class="rvp">Recently viewed profiles</span><br>
                    <uc1:uc_viewedprofiles runat="server" id="uc_viewedprofiles" Name="ViewedByMe"/>
                </div>
            </div>
            <!--end of third row-->

        </div>
        <!--End Middle Column-->

        <div class=" col-lg-2" style="background-color: lightgreen; padding-left: 4px;">
            <!--RHS Column-->
            <div style="margin-top: 10px;">
                <!--LHS Ad Column-->
                <img src="images/ad-tower.png" />
            </div>
            <!--end of LHS Ad column-->
        </div>
        <!--end of RHS column-->
    </div>
    <!--end of third row-->

</asp:Content>
