<%@ Page Title="" Language="C#" MasterPageFile="~/Secured/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AdminModule.Secured.Forums.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="objHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="objContentPlaceHolder" runat="server">
    <div style="margin-top: 8px; display: block;">
        <input type="button" id="btnShowAdd" value="Add New Post" />
    </div>
    <div class="displaynone" id="divAddPost" style="margin-top: 8px; width: 550px; min-height: 150px; border: 1px solid; border-radius: 8px 6px;">
        <div style="position: absolute; margin-left: 520px; margin-top: 0px; cursor: pointer;" id="btnCloseAddPost">
            <img src="/Images/close.png" />
        </div>
        <div style="font-size: 20px; padding: 8px;" class="Transformer">Start Thread</div>
        <div style="padding: 8px;">
            <input type="text" id="txtTitle" maxlength="500" style="width: 530px;" placeholder="Enter the title" />
        </div>
        <div style="padding: 8px;">
            <textarea id="txtPost" style="width: 530px; height: 100px; font-family: Arial; resize: none;" placeholder="Enter your message"></textarea>
        </div>
        <div style="padding: 8px;">
            <div style="float: left; width: 33%;">
                <asp:DropDownList ID="cboForumCategories" CssClass="ForumCategories" Font-Size="14px" runat="server"></asp:DropDownList>
                <input type="hidden" id="hdnCurrentCategoryID" class="hdnCurrentCategoryID" runat="server" />
            </div>
            <div style="float: left; width: 33%; text-align: center;">
                <input type="file" id="flUpload" style="display:none;" />
                <input type="hidden" id="hdnUploadedFile" style="display:none;" />
            </div>
            <div style="float: left; width: 66%; text-align: right;">
                <input type="button" id="btnSubmitPost" value="Submit Post" style="font-family: Arial; font-size: 14px;" />
            </div>
        </div>
        <div style="clear: both; height: 10px;">&nbsp;</div>
    </div>
    <div>&nbsp;</div>
    <div style="display: table; width: 800px; border: 1px solid #CCCCCC; background-color:#5B3BB8; padding: 2px;">
        <div style="display: table-cell; width: 150px; font-weight: bold; text-align: center;color:#FFFFFF;">Author</div>
        <div style="display: table-cell; width: 200px; font-weight: bold; text-align: center; border-left: 1px solid #CCCCCC;color:#FFFFFF;">Title</div>
        <div style="display: table-cell; width: 80px; font-weight: bold; text-align: center; border-left: 1px solid #CCCCCC;color:#FFFFFF;">Replies</div>
        <div style="display: table-cell; width: 100px; font-weight: bold; text-align: center; border-left: 1px solid #CCCCCC;color:#FFFFFF;">Added Time</div>
    </div>
    <div id="divPosts" data-bind="template: { name: 'template-PostView', foreach: AllPosts, afterRender: MakePostAnimation }"></div>
    <div>&nbsp;</div>
    <script type="text/html" id="template-PostView">
        <div style="height: 4px;"></div>
        <div style="display: table; width: 800px; border: 1px solid #CCCCCC; padding: 2px; cursor: pointer;" data-bind="event: { click: OpenThread }">
            <div style="display: table-cell; width: 150px; text-align: center;"  data-bind="text: Author"></div>
            <div style="display: table-cell; width: 200px; text-align: center; border-left: 1px solid #CCCCCC;"  data-bind="text: PostTitle"></div>
            <div style="display: table-cell; width: 80px; text-align: center; border-left: 1px solid #CCCCCC;"  data-bind="html: RepliesCount"></div>
            <div style="display: table-cell; width: 100px; text-align: center; border-left: 1px solid #CCCCCC;"  class="time" data-bind="attr: { title: LastActionTimeStamp }"></div>
        </div>
        <!--
        <div style="width: 800px; min-height: 100px; background-color: #fff; box-shadow: 6px 6px 2px #888888; border: 1px solid #888888; cursor: pointer;" data-bind="event: { click: OpenThread }">
            <div style="position: absolute; margin-left: 4px; margin-top: 4px; font-size: 16px; color: gray;" data-bind="text: ForumCategory().CategoryName"></div>
            <div style="padding: 4px; color: #888; text-align: right;" class="time" data-bind="attr: { title: LastActionTimeStamp }"></div>
            <div style="padding: 4px; font-size: 18px;" class="Transformer" data-bind="text: Author"></div>
            <div style="padding: 4px; font-weight: bold;" data-bind="text: PostTitle"></div>
            <div style="padding: 4px; font-size: 14px; color: gray;" data-bind="html: RepliesCount"></div>
        </div>
        -->
    </script>
    <script src="Script.js" type="text/javascript"></script>
</asp:Content>
