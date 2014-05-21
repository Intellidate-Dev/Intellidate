<%@ Page Title="" Language="C#" MasterPageFile="~/Secured/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AdminModule.Secured.Admins.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="objHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="objContentPlaceHolder" runat="server">
    <div style="float: left; display: block; border-left: 1px solid; border-bottom: 0px solid; width: 330px; min-height: 500px;">
        <!--Create Admin -->
        <div style="display: block; height: 10px;">&nbsp;</div>
        <div style="display: block; padding: 4px; font-size: 22px; color: #888;" class="Transformer" id="lblTitle">Create Admin</div>
        <div style="display: block; padding: 8px;">
            <div style="display: block;">
                <div style="font-weight: bold;" id="ttlAdminName">Admin Name: </div>
                <div>
                    <input type="text" id="txtAdminName" maxlength="200" style="width: 300px;" />
                </div>
            </div>
            <div style="display: block; padding-top: 4px;">
                <div style="font-weight: bold;" id="ttlEmailAddress">Email Address: </div>
                <div>
                    <input type="text" id="txtEmailAddress" maxlength="50" style="width: 300px;" />
                </div>
            </div>
            <div style="display: block; padding-top: 4px;">
                <div style="font-weight: bold;" id="ttlLoginID">Login ID: </div>
                <div>
                    <input type="text" id="txtLoginID" value=" " maxlength="20" style="width: 300px;" />
                </div>
            </div>
            <div style="display: block; padding-top: 4px;">
                <div style="font-weight: bold;" id="ttlPassword">Password: </div>
                <div>
                    <input type="password" id="txtPassword" maxlength="20" style="width: 300px;" />
                </div>
            </div>
            <div style="display: block; padding-top: 4px;">
                <div style="font-weight: bold;" id="ttlRetypePassword">Retype Password: </div>
                <div>
                    <input type="password" id="txtRetypePassword" maxlength="20" style="width: 300px;" />
                </div>
            </div>
            <div style="display: block; padding-top: 4px;">
                <div style="font-weight: bold;">Privilages: </div>
                <div>
                    <asp:CheckBoxList ID="chkRoles" runat="server" CssClass="PrivilegeCheckboxes" ClientIDMode="Static" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="2"></asp:CheckBoxList>
                </div>
            </div>

            <div style="display: block; padding-top: 4px;">
                <div style="font-weight: bold;">Forums: </div>
                <div>
                    <asp:CheckBoxList ID="chkForums" runat="server" ClientIDMode="Static" CssClass="ForumsCheckboxes" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="2"></asp:CheckBoxList>
                </div>
            </div>

            <div style="display: block; padding-top: 10px;">
                <input type="button" id="btnCreateAdmin" value="Create Admin" />
                <input type="hidden" id="hdnAdminID" />
                <input type="button" id="btnCancel" value="Cancel" />
            </div>
            <div style="display: block; margin-top: 10px; background-color: #cccccc; color:red;" id="lblErrorMessage">&nbsp;</div>
        </div>

    </div>
    <div style="float: left; width: 10px;">&nbsp;</div>
    <div style="float: left; display: block; border-left: 1px solid; border-bottom: 0px solid; width: 400px; min-height: 500px; margin-left:10px;">
        <div style="display: block; height: 10px;">&nbsp;</div>
        <div style="display: block; padding: 4px; font-size: 22px; color: #888;" class="Transformer">List Admins</div>
        <div id="divAdminList" style="display: block; width: 650px;" data-bind="template: { name: 'template-adminlist', foreach: AllAdmins }"></div>
    </div>
    <div style="clear: both;"></div>
    <script type="text/html" id="template-adminlist">
        <div style="display: block; padding: 6px; background-color: #ffffff; font-family: Arial; font-size: 14px; border: 1px solid #ccc;">
            <div style="float: left; width: 200px; padding: 4px;" data-bind="text: AdminName"></div>
            <div style="float: left; width: 220px; padding: 4px;" data-bind="text: EmailID"></div>
            <div style="float: left; width: 100px;">
                <input type="button" data-bind="event: { click: EditAdmin }" value="Edit Admin" />
            </div>
            <div style="float: left; width: 100px;">
                <input type="button" data-bind="event: { click: DeleteAdmin }" value="Delete Admin" />
            </div>
            <div style="clear: both;"></div>
        </div>
    </script>
    <script src="Script.js" type="text/javascript"></script>
</asp:Content>
