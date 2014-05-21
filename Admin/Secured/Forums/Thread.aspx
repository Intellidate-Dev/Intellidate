<%@ Page Title="" Language="C#" MasterPageFile="~/Secured/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Thread.aspx.cs" Inherits="AdminModule.Secured.Forums.Thread" %>

<asp:Content ID="Content1" ContentPlaceHolderID="objHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="objContentPlaceHolder" runat="server">
    <div>&nbsp;</div>
    <div id="divPosts" data-bind="template: { name: 'template-PostView', foreach: AllPosts }"></div>
    <div>&nbsp;</div>
    <script type="text/html" id="template-PostView">
        <div style="width: 800px; min-height: 100px; background-color: #fff; box-shadow: 2px 2px 1px #cccccc; border: 1px solid #888888; cursor: pointer;">
            <div style="padding: 4px; color: #888; text-align: right;" class="time" data-bind="attr: { title: LastActionTimeStamp }"></div>
            <div style="padding: 4px; font-size: 18px;" class="Transformer" data-bind="text: Author"></div>
            <div style="padding: 4px; font-weight: bold;" data-bind="text: PostTitle"></div>
            <div style="padding: 4px;" data-bind="html: PostTextRel"></div>
            
        </div>
        <div style="height: 10px;"></div>
    </script>
    <div style="margin-top: 8px; display: block; width: 550px; min-height: 150px; border: 1px solid; border-radius: 8px 6px;">
        <div style="font-size: 20px; padding: 8px;" class="Transformer">Reply</div>
        <div style="padding: 8px;">
            <textarea id="txtPost" style="width: 530px; height: 100px; font-family: Arial; resize: none;" placeholder="Enter your message"></textarea>
        </div>
        <div style="padding: 8px;">
            <div style="float: left; width: 50%;">
                <input type="hidden" id="hdnPostID" class="hdnPostID" runat="server" />
            </div>
            <div style="float: left; width: 50%;">
                <input type="button" id="btnSubmitPost" value="Reply Post" style="font-family: Arial; font-size: 14px;" />
            </div>
        </div>
        <div style="clear: both; height: 10px;">&nbsp;</div>
    </div>
    <script type="text/javascript">
        var _JsonString=<%=JsonString%>
    </script>
    <script type="text/javascript">
        $(document).ready(function(){
            ko.applyBindings(new VMPostList(_JsonString), document.getElementById('divPosts'));
            jQuery(".time").timeago();
        });
    </script>
    <script type="text/javascript">
        function VMPost(_post) {
            var self = this;
            self.highlight = ko.observable(true);
            self.PostID = ko.observable(_post.PostID);
            self.PostRefID = ko.observable(_post.PostRefID);
            self.Admin = ko.observable(_post.Admin);
            self.ForumCategory = ko.observable(_post.ForumCategory);
            self.ParentPost = ko.observable(_post.ParentPost);
            self.PostTitle = ko.observable(_post.PostTitle);
            self.PostText = ko.observable(_post.PostText);
            self.PostTimeStamp = ko.observable(_post.PostTimeStamp);
            self.Status = ko.observable(_post.Status);
            self.ForumAttachments = ko.observable(_post.ForumAttachments);
            self.PostChildren = ko.observable(_post.PostChildren);
            self.LastActionTimeStamp = ko.observable(_post.LastActionTimeStamp);
            
            self.ChildrenCount = ko.computed(function () {
                if (self.PostChildren() == null) {
                    return 0;
                }
                else {
                    return self.PostChildren().length;
                }
            }, this);

            self.PostTextRel = ko.computed(function () {
                if (self.PostText() == null) {
                    return "";
                }
                else {
                    var _return='';
                    _return = self.PostText().replace('\n','<br \\>');
                    _return = _return.replace('\r','<br \\>');
                    return _return
                }
            }, this);

            self.RepliesCount = ko.computed(function () {
                if (self.PostChildren() == null) {
                    return "Replies (0)";
                }
                else {
                    return "Replies (" + self.PostChildren().length + ")";
                }
            }, this);

            self.Author = ko.computed(function () {
                if (self.Admin() == null) {
                    return "";
                }
                else {
                    return self.Admin().AdminName;
                }
            }, this);

        }

        function VMPostList(_postlist) {
            var self = this;
            self.AllPosts = ko.observableArray();
            for (var i = 0; i < _postlist.length; i++) {
                self.AllPosts.push(new VMPost(_postlist[i]));
            }

            ResetPosts = function (_newposts) {
                var _len = self.AllPosts().length;
                for (var i = 0; i < _len; i++) {
                    self.AllPosts.remove(self.AllPosts()[0]);
                }
                for (var i = 0; i < _newposts.length; i++) {
                    self.AllPosts.push(new VMPost(_newposts[i]));
                }
            };

            AddReply = function (_newpost) {
                self.AllPosts.push(new VMPost(_newpost));
                $("#txtPost").val('');
                jQuery(".time").timeago();
            };

            OpenThread = function (_data) {
                window.location.href = "Thread?LSDGNLSDNGKLSDNGKLLK=" + _data.PostID();
            };

            SetHighlight = function () {
                for (var i = 0; i < self.AllPosts().length; i++) {
                    if (i % 2 == 0) {
                        self.AllPosts()[i].highlight(true);
                    }
                    else {
                        self.AllPosts()[i].highlight(false);
                    }
                }
            };
        }

    </script>
    <script>
        $(document).ready(function(){
            $("#btnSubmitPost").click(function () {
                if($("#txtPost").val()==""){
                    return;
                }
                var _content = $("#txtPost").val();
                var _postid = $(".hdnPostID").val();

                var _replyObject = new Object();
                _replyObject.postid=_postid;
                _replyObject.content=_content;

                $.postJSON("Service.ashx?f=RP",_replyObject,function(data){
                    AddReply(data);
                    _Live.server.addreply(data);
                });
            });
        });
    </script>
</asp:Content>
