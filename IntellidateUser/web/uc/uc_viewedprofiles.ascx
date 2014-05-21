<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_viewedprofiles.ascx.cs" Inherits="IntellidateUser.web.uc.uc_viewedprofiles" %>
<asp:DataList ID="lstProfViewed" runat="server" RepeatDirection="Horizontal" RepeatColumns="5">
    <ItemTemplate>
        <div class="image">
            <img alt="<%#Eval("FullName") %>" src="<%#Eval("ImagePath") %>" style="width:78px; height:78px;" class="profile-pic-small"/>
            <div style="float:left;margin-bottom:5px;">
                <p style="font-family:Arial; text-wrap:normal; font-size:9px;"><%#Eval("FullName") %></p>
            </div>
        </div>
    </ItemTemplate>
</asp:DataList>
