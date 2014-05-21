<%@ Page Title="" Language="C#" MasterPageFile="~/Secured/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AdminModule.Secured.ForumCatg.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="objHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="objContentPlaceHolder" runat="server">
    <div style="float: left; display: block; border-left: 1px solid; border-bottom: 0px solid; width: 330px; min-height: 500px;">
        <!--Create Admin -->
        <div style="display: block; height: 10px;">&nbsp;</div>
        <div style="display: block; padding: 4px; font-size: 22px; color: #888;" class="Transformer" id="lblTitle">Create Forum Category</div>
        <div style="display: block; padding: 8px;">
            <div style="display: block;">
                <div style="font-weight: bold;">Forum Category Name: </div>
                <div>
                    <input type="text" id="txtCategoryName" style="width: 300px;" />
                </div>
            </div>
            <div style="display: block; padding-top: 10px;">
                <input type="button" id="btnCancel" value="Cancel" style="width:100px;" />
                <input type="button" id="btnSubmit" value="Submit" style="width:100px;" />
            </div>
            <div style="display: block; margin-top: 10px; background-color: #cccccc;">&nbsp;</div>
        </div>

    </div>
    <div style="float: left; width: 10px;">&nbsp;</div>
    <div style="float: left; display: block; border-left: 1px solid; border-bottom: 0px solid; width: 400px; min-height: 500px;">
        <div style="display: block; height: 10px;">&nbsp;</div>
        <div style="display: block; padding: 4px; font-size: 22px; color: #888;" class="Transformer">List Forum Categories</div>
        <div style="display: block;" id="divCategories" data-bind="template: { name: 'forumcategory-template', foreach: AllCategories }"></div>
    </div>
    <div style="clear: both;"></div>
    <script src="Script.js" type="text/javascript"></script>
    <script type="text/html" id="forumcategory-template">
        <div style="display:block;padding:6px;">
            <div style="float:left;width:200px; font-family:Arial;font-size:14px;font-weight:bold;" data-bind="text: CategoryName"></div>
            <div style="float:left;"><input type="button" value="Edit" data-bind="event: { click: EditCategory }" /> </div>
            <div style="float:left;"><input type="button" value="Delete" data-bind="event: {click: DeleteCategory}" /> </div>
        </div>
        <div style="clear:both;"></div>
    </script>
</asp:Content>
