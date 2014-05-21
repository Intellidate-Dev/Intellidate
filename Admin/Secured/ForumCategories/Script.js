$(document).ready(function () {
    // 

    $("#btnCancel").click(function () {
        _isEdit = false;
        _id = "";
        $("#lblTitle").html("Create Forum Category");
    });

    $("#btnSubmit").bind("click", function () {
        var _CategoryName = $("#txtCategoryName").val();
        if (_CategoryName.trim() == "") {
            return;
        }

        $(this).attr('disabled', 'disabled');
        var _prevTitle = $(this).val();
        $("#btnCreateForumCatg").val("Please wait...");

        var _data = "CategoryName=" + _CategoryName;
       
        if (_isEdit) {
            _data = _data + "&_id=" + _id;
            _url = "Service.ashx?f=E";
        } else {
            var _url = "Service.ashx?f=A";
        }
        $.postDATA(_url, _data, function (data) {
            setTimeout(function () {
                $("#btnSubmit").val(_prevTitle);
                $("#btnSubmit").removeAttr('disabled');
                $("#txtCategoryName").val('');
                if (_isEdit) {
                    EditForumCategory(data);
                } else {
                    AddForumCategory(data);
                }


            }, 500);

        });

    });
});

function VMForumCategory(_catg) {
    var self = this;
    self._id = ko.observable(_catg._id);
    self.CategoryName = ko.observable(_catg.CategoryName);
    self.NumberOfPosts = ko.observable(_catg.NumberOfPosts);
    self.NumberOfUsers = ko.observable(_catg.NumberOfUsers);

}

var _isEdit = false;
var _id = "";
function VMListForumCategory(_list) {
    var self = this;
    self.AllCategories = ko.observableArray();
    for (var i = 0; i < _list.length; i++) {
        self.AllCategories.push(new VMForumCategory(_list[i]));
    }

    AddForumCategory = function (_newcat) {
        self.AllCategories.push(new VMForumCategory(_newcat));
    };

    EditForumCategory = function (_editCatg) {
        var _pos = -1;
        for (var i = 0; i < self.AllCategories().length; i++) {
            if (self.AllCategories()[i]._id() == _editCatg._id) {
                _pos = i;
                break;
            };
        }

        if (_pos > -1) {
            self.AllCategories()[_pos].CategoryName(_editCatg.CategoryName);
        }
    };

    EditCategory = function (_catg) {
        _isEdit = true;
        _id = _catg._id();
        $("#txtCategoryName").val(_catg.CategoryName());
        $("#lblTitle").html("Edit Forum Category");
    };

    DeleteCategory = function (_delcat) {
        $.postDATA("/Secured/ForumCategories/Service.ashx?f=D", "_id=" + _delcat._id(), function (data) {
            var _pos = -1;
            for (var i = 0; i < self.AllCategories().length; i++) {
                if (self.AllCategories()[i]._id() == _delcat._id()) {
                    _pos = i;
                    break;
                }
            }
            self.AllCategories.remove(self.AllCategories()[_pos]);
        });
    };
}

$(document).ready(function () {
    $.postDATA("/Secured/ForumCategories/Service.ashx?f=G", "", function (data) {
        ko.applyBindings(new VMListForumCategory(data), document.getElementById('divCategories'));
    });
});