var _IsEdit = false;
$(document).ready(function () {
    GetAllAdminUsers();
    $("#btnCreateAdmin").click(function () {

        if (ValidateForm() == false) {
            return false;
        }

        $("#btnCreateAdmin").prop("disabled", true);
        $("#btnCancel").prop("disabled", true);


        var _privileges = "";
        $("[id*=chkRoles] input:checked").each(function () {
            _privileges = _privileges + ',' + $(this).val();
        });
        _privileges = _privileges.substr(1);


        var _forums = "";

        $("[id*=chkForums] input:checked").each(function () {
            _forums = _forums + ',' + $(this).val();
        });
        _forums = _forums.substr(1);

        var _postData = new Object();
        _postData.LoginName = $("#txtLoginID").val();
        _postData.AdminName = $("#txtAdminName").val();
        _postData.AdminPassword = $("#txtPassword").val();
        _postData.AdminType = "1";
        _postData.EmailID = $("#txtEmailAddress").val();
        _postData.AdminPrivileges = _privileges;
        _postData.Forums = _forums;
        _postData.AdminID = $("#hdnAdminID").val();

        $.postJSON("Service.ashx?f=A", _postData, function (data) {
            $("#btnCreateAdmin").prop("disabled", false);
            $("#btnCancel").prop("disabled", false);

            if (_IsEdit) {
                Cancel();
            } else {
                AddNewAdmin(data);
                ResetForm();
            }
        });
    });
    $("#btnCancel").click(function () {
        Cancel();
    });
});

function Cancel() {
    if (_IsEdit) {

        _IsEdit = false;
        ResetForm();
    }
}

function ResetForm() {
    lblTitle.innerHTML = "Create Admin";
    $("#txtAdminName").val("");
    $("#txtEmailAddress").val("");
    $("#txtLoginID").val("");
    $("#txtLoginID").prop("disabled", false);
    $("#txtPassword").val("");
    $("#txtRetypePassword").val("");

    $("[id*=chkRoles] input:checkbox").each(function () {
        $(this).prop("checked", false);
    });

    $("[id*=chkForums] input:checkbox").each(function () {
        $(this).prop("checked", false);
    });

    $("#hdnAdminID").val("");
    $("#btnCreateAdmin").val("Create Admin");
}

function VMAdmin(_admin) {
    var self = this;
    self._id = ko.observable(_admin._id);
    self.AdminRefId = ko.observable(_admin.AdminRefId);
    self.AdminName = ko.observable(_admin.AdminName);
    self.LoginName = ko.observable(_admin.LoginName);
    self.EmailID = ko.observable(_admin.EmailID);
    self.AdminPrivileges = ko.observableArray();
    for (var i = 0; i < _admin.AdminPrivileges.length; i++) {
        self.AdminPrivileges.push(new VMAdminPrivileges(_admin.AdminPrivileges[i]));
    }

    self.ForumPrivileges = ko.observableArray();
    for (var i = 0; i < _admin.ForumPrivileges.length; i++) {
        self.ForumPrivileges.push(new VMForumPrivileges(_admin.ForumPrivileges[i]));
    }
}

function VMForumPrivileges(_forum) {
    var self = this;
    self.Privilege = ko.observable(_forum);
}

function VMAdminPrivileges(_priv) {
    var self = this;
    self.Privilege = ko.observable(_priv);
}

function VMAdminList(_list) {
    var self = this;
    self.AllAdmins = ko.observableArray();
    for (var i = 0; i < _list.length; i++) {
        self.AllAdmins.push(new VMAdmin(_list[i]));
    }

    AddNewAdmin = function (_newadmin) {
        self.AllAdmins.push(new VMAdmin(_newadmin));
    };

    EditAdmin = function (_editadmin) {
        // Set the section editable
        _IsEdit = true;
        lblTitle.innerHTML = "Edit Admin";
        $("#txtAdminName").val(_editadmin.AdminName());
        $("#txtEmailAddress").val(_editadmin.EmailID());
        $("#txtLoginID").val(_editadmin.LoginName());
        $("#txtLoginID").prop("disabled", true);
        $("#txtPassword").val("");
        $("#txtRetypePassword").val("");


        $("[id*=chkRoles] input:checkbox").each(function () {
            //_privileges = _privileges + ',' + $(this).val();
            for (var i = 0; i < _editadmin.AdminPrivileges().length; i++) {
                if ($(this).val() == _editadmin.AdminPrivileges()[i].Privilege()) {
                    $(this).prop("checked", true);
                }
            }
        });

        $("[id*=chkForums] input:checkbox").each(function () {
            //_privileges = _privileges + ',' + $(this).val();
            for (var i = 0; i < _editadmin.ForumPrivileges().length; i++) {
                if ($(this).val() == _editadmin.ForumPrivileges()[i].Privilege()) {
                    $(this).prop("checked", true);
                }
            }
        });

        $("#hdnAdminID").val(_editadmin.AdminRefId());
        $("#btnCreateAdmin").val("Edit Admin");
    };

    DeleteAdmin = function (_deladmin) {
        if (confirm("Are you sure you wante to delete the admin ?")) {
            var _adminID = _deladmin.AdminRefId();
            $.postDATA("Service.ashx?f=D", "AdminID=" + _adminID, function (data) {
                // Remove the admin
                var _pos = -1;
                for (var i = 0; i < self.AllAdmins().length; i++) {
                    if (self.AllAdmins()[i].AdminRefId() == _adminID) {
                        _pos = i;
                        break;
                    }
                }
                self.AllAdmins.remove(self.AllAdmins()[_pos]);
            });
        }

    };
}

function GetAllAdminUsers() {
    $.postDATA("Service.ashx?f=G", "", function (_data) {
        ko.applyBindings(new VMAdminList(_data), document.getElementById('divAdminList'));
    });
}

function ValidateForm() {

    var _Return = true;
    ResetError("#ttlAdminName");
    ResetError("#ttlEmailAddress");
    ResetError("#ttlLoginID");
    ResetError("#ttlPassword");
    ResetError("#ttlRetypePassword");

    if (CheckisEmpty("#txtAdminName", "#ttlAdminName") == false) {
        _Return = false;
    }

    if (CheckisEmpty("#txtEmailAddress", "#ttlEmailAddress") == false) {
        _Return = false;
    }

    if (CheckisEmpty("#txtLoginID", "#ttlLoginID") == false) {
        _Return = false;
    }

    if (CheckisEmpty("#txtPassword", "#ttlPassword") == false) {
        _Return = false;
    }

    if (CheckisEmpty("#txtRetypePassword", "#ttlRetypePassword") == false) {
        _Return = false;
    }

    if (_Return == false) {
        $("#lblErrorMessage").html("Please check the values.");
        return _Return;
    }

    if ($("#txtPassword").val() != $("#txtRetypePassword").val()) {
        SetError("#ttlPassword");
        SetError("#ttlRetypePassword");
        $("#lblErrorMessage").html("Please check the values.");
        return false;
    }

    return true;
}

function ResetError(_Title) {
    if ($(_Title).hasClass("Red")) {
        $(_Title).removeClass("Red");
    }
}

function SetError(_Title) {
    if ($(_Title).hasClass("Red") == false) {
        $(_Title).addClass("Red");
    }
}

function CheckisEmpty(_Object, _Title) {
    if ($(_Object).val().trim() == "") {
        SetError(_Title)
        return false;
    }
    else {
        ResetError(_Title)
        return true;
    }
}