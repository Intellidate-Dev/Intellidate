$(document).ready(function () {
    $("#btnShowAdd").click(function () {
        $("#btnShowAdd").fadeOut(500);
        $("#divAddPost").hide();
        $("#divAddPost").removeClass("displaynone");
        $("#divAddPost").fadeIn(500);
    })
    $("#btnCloseAddPost").click(function () {
        $("#divAddPost").fadeOut(500);
        $("#divAddPost").addClass("displaynone");
        $("#btnShowAdd").fadeIn(500);
    });
});

$(document).ready(function () {
    //
    $("#txtPost").focus(function () {
        if ($(this).val() == "Enter your message") {
            $(this).val("");
            if ($(this).hasClass("LightColor")) {
                $(this).removeClass("LightColor");
            }
        }
    });

    $("#txtPost").blur(function () {
        if ($(this).val().trim() == "") {
            $(this).val("Enter your message");
            if ($(this).hasClass("LightColor") == false) {
                $(this).addClass("LightColor");
            }
        }
    });

});

$(document).ready(function () {
    $("#flUpload").change(function () {
        if (window.FileReader) {
            reader = new FileReader();
            reader.readAsDataURL(this.files[0]);
        }

        formdata = new FormData();
        if (formdata) {
            formdata.append("files[]", this.files[0]);
        }
        var sUrl = "Service.ashx?f=U";
        //
        $.uploadFILE(sUrl, formdata, function (_data) {
            $("#hdnUploadedFile").val(_data);
            $("#flUpload").hide();
        });
    });
});


$(document).ready(function () {
    $(".ForumCategories").change(function () {
        $(".hdnCurrentCategoryID").val($(this).val());
    });
});

$(document).ready(function () {
    $("#btnSubmitPost").click(function () {

        if ($("#txtTitle").val() == "" || $("#txtPost").val()=="") {
            return;
        }

        var _postJson = new Object();
        _postJson.title = $("#txtTitle").val();
        _postJson.content = $("#txtPost").val();
        _postJson.category = $(".hdnCurrentCategoryID").val();
        _postJson.attachment = $("#hdnUploadedFile").val();

        $("#btnSubmitPost").prop("disabled", true);
        $("#btnSubmitPost").val("Please wait...");

        $.postJSON("Service.ashx?f=A", _postJson, function (data) {
            AddNewPost(data);
            _Live.server.addpost(data);
            $("#btnSubmitPost").val("Submit Post");
            $("#btnSubmitPost").prop("disabled", false);
        });

    });
});
var _lastShownID = "0";
$(document).ready(function () {
    var _category = $(".hdnCurrentCategoryID").val();
    var _data = "category=" + _category + "&lastshownid=" + _lastShownID;
    $.postDATA("Service.ashx?f=G", _data, function (data) {
        ko.applyBindings(new VMPostList(data), document.getElementById('divPosts'));
        _lastShownID = GetLastShownID();
        jQuery(".time").timeago();
    });

});
$(document).ready(function () {
    $(window).scroll(function () {
        if ($(window).scrollTop() + $(window).height() == $(document).height()) {
            LoadMorePosts();
        }
    });
});


function VMPost(_post) {
    var self = this;
    self.highlight = ko.observable(true);
    self.PostID = ko.observable(_post.PostRefID);
    self.PostRefID = ko.observable(_post.PostRefID);
    self.Admin = ko.observable(_post.Admin);
    self.IsAdminOnline = ko.observable(false);
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

    GetLastShownID = function () {
        return self.AllPosts()[self.AllPosts().length - 1].PostID();
    };

    ResetPosts = function (_newposts) {
        var _len = self.AllPosts().length;
        for (var i = 0; i < _len; i++) {
            self.AllPosts.remove(self.AllPosts()[0]);
        }
        for (var i = 0; i < _newposts.length; i++) {
            self.AllPosts.push(new VMPost(_newposts[i]));
        }
    };

    LoadMorePosts = function () {
        var _category = $(".hdnCurrentCategoryID").val();
        var _data = "category=" + _category + "&lastshownid=" + _lastShownID;
        $.postDATA("Service.ashx?f=G", _data, function (data) {
            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    self.AllPosts.push(new VMPost(data[i]));
                }
            }
            _lastShownID = GetLastShownID();
            jQuery(".time").timeago();
        });
    };

    SetAdminOnline = function (_adminid) {
        for (var i = 0; i < self.AllPosts().length; i++) {
            if (self.AllPosts()[i].Admin().AdminId == _adminid) {
                self.AllPosts()[i].IsAdminOnline(true);
            }
        }
    };

    SetAdminOffline = function (_adminid) {
        for (var i = 0; i < self.AllPosts().length; i++) {
            if (self.AllPosts()[i].Admin().AdminId == _adminid) {
                self.AllPosts()[i].IsAdminOnline(false);
            }
        }
    };

    AddNewPost = function (_newpost) {
        self.AllPosts.unshift(new VMPost(_newpost));
        $("#txtTitle").val('');
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
    var _animate = true;
    MakePostAnimation = function (elem) {

        return true;
    };
}