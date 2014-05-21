<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_userprofile.ascx.cs" Inherits="IntellidateUser.web.uc.uc_userprofile" %>
<asp:DataList ID="lstUserDetails" runat="server">
    <ItemTemplate>
        <div class="col-lg-3" style="padding-left: 30px;">
            <div class="row" style="padding-top: 8px;">
                <div class="col-lg-12">
                    <span class="profile-name-title"><%# Eval("FullName") %></span>
                    <br>
                    <span class="profile-details"><%#Eval("Age") %>&nbsp;,<%#Eval("Height") %><br>
                        <%#Eval("MaritalStatus") %><br>
                        <%#Eval("Ethnicity") %> &nbsp;,<%#Eval("Religion") %><br>
                        <%#Eval("Job") %></span>
                </div>
            </div>
            <div class="row" style="padding-top: 30px;">
                <div class="col-lg-12">
                    <span class="profile-lookingfor-title">Looking For</span>
                    <br>
                    <span class="profile-lookingfor">32 - 40 years<br>
                        5’ 7” - 6’ 3”<br>
                        Single/Divorced
                            <br>
                        White/caucasian </span>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:DataList>


