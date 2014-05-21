
//ready function for binding dummy data
$(document).ready(function () {

    //insert some dummy data into model
            var _self = new Object();
            _self._id = "";
            _self._RefID = "";
            _self.LoginName = "";
            _self.FullName = "";
            _self.EmailAddress = "";
            _self.Password = "";
            _self.UserGender = "";
            _self.DateOfBirth = "";
            _self.UserAgeInYears = "";
            _self.UserCreatedDate = "";
            _self.LastOnlineTime = "";
            _self.Status = "";
            _self.availableOptions = ko.observableArray(['Male', 'Female', 'Other']);
            _self.ShowPopup = false;
            var _dummyArray = new Array();
            _dummyArray.push(_self);
            //bind ko 
            ko.applyBindings(new ListUserModel(_dummyArray), document.getElementById('dvUsers'));

});


//ready function for button events
$(document).ready(function () {

            $("#btnClose").click(function () {
                $(".popup").css("display", "none");
            });

            $("#cmdUpdateUser").click(function () {
                UpdateDetails($("#hdndata").val());
            });

        });

//create a user view model
function UserModel(_obj) {

    var _self = this;
    _self._id = ko.observable(_obj._id);
    _self._RefID = ko.observable(_obj._RefID);
    _self.LoginName = ko.observable(_obj.LoginName);
    _self.FullName = ko.observable(_obj.FullName);
    _self.EmailAddress = ko.observable(_obj.EmailAddress);
    _self.Password = ko.observable(_obj.Password);
    _self.UserGender = ko.observable(_obj.UserGender);
    _self.DateOfBirth = ko.observable(_obj.DateOfBirth);
    _self.UserAgeInYears = ko.observable(_obj.UserAgeInYears);
    _self.UserCreatedDate = ko.observable(_obj.UserCreatedDate);
    _self.LastOnlineTime = ko.observable(_obj.LastOnlineTime);
    _self.Status = ko.observable(_obj.Status);
    _self.ShowPopup = ko.observable(false);
    _self.checkEmail = ko.observable(true);
    _self.availableOptions = ko.observableArray(['Male', 'Female', 'Other']);
    _self.Gender = ko.computed(function () {
        if (_self.UserGender() == 1) {
            return "Male"
        }
        if (_self.UserGender() == 2) {
            return "Female"
        }
        if (_self.UserGender() == 3) {
            return "Other"
        }


    }, this);

    _self.dob = ko.computed(function () {
        if (_self.DateOfBirth().trim() != "") {
            //date format  month , date , year
            var date = _self.DateOfBirth().split('-')[1] + "/" + _self.DateOfBirth().split('-')[2].split('T')[0] + "/" + _self.DateOfBirth().split('-')[0];
            return date;
        }
        else {
            return "";
        }
    }, this);


    _self.userCreated = ko.computed(function () {
        if (_self.DateOfBirth().trim() != "") {
            var date = _self.UserCreatedDate().split('-')[1] + "/" + _self.UserCreatedDate().split('-')[2].split('T')[0] + "/" + _self.UserCreatedDate().split('-')[0];
            return date;
        }
        else {
            return "";
        }
    }, this);

    _self.LastOnline = ko.computed(function () {
        if (_self.DateOfBirth().trim() != "") {
            var date = _self.LastOnlineTime().split('-')[1] + "/" + _self.LastOnlineTime().split('-')[2].split('T')[0] + "/" + _self.LastOnlineTime().split('-')[0];
            return date;
        }
        else {
            return "";
        }
    }, this);

    _self.StatusCheck = ko.computed(function () {
        if (_self.Status() == "A") {
            return true;
        } else {
            return false;
        }

    }, this);


}

//create a list view model
function ListUserModel(_list) {
    var self = this;
    self.AllUsers = ko.observableArray();
    for (var i = 0; i < _list.length; i++) {
        self.AllUsers.push(new UserModel(_list[i]));
    }


    //create event for scroll bar of tbody
    scrolled = function (data, event) {
        var elem = event.target;
        var idArray = new Array();
        for (var i = 0; i < self.AllUsers().length; i++) {
            idArray[i] = self.AllUsers()[i]._RefID();
        }
        if (elem.scrollTop > (elem.scrollHeight - elem.offsetHeight - 50)) {
            GetNextScrollDown(idArray);
        }
    },


    maxId = 0,
    pendingRequest = ko.observable(false);


    ChangeSelection = function (event) {
        alert(event.UserGender());
    };


    //get data form search users
    SearchUsers = function () {
        var _searchKey = $("#txtSearchKey").val().trim();
        if (_searchKey == "") {
            alert("Please enter search key");
            $("#txtSearchKey").focus();
            return false;
        }
        else {
            //ajax call to hit the db server
            $.postDATA("Service.ashx?method=SearchUser", "SearchKey=" + _searchKey, function (data) {                  
                    self.AllUsers.removeAll();
                    for (var i = 0; i < data.length; i++) {
                        self.AllUsers.push(new UserModel(data[i]));
                    }
                    $(".tableCss").css("display", "inline");
            });
        }
    }


    //search by mounth,week day
    SearchRecentUsers = function () {
        //ajax call to hit the db server
        $(".tableCss").css("display", "inline");
        var ddlValue = $("#ddlSearch option:selected").val();
        if (ddlValue != "0") {
            $.postDATA("Service.ashx?method=SearchRecentlyAddedUser", "SearchKey=" + ddlValue, function (data) {     
                    self.AllUsers.removeAll();
                    for (var i = 0; i < data.length; i++) {
                        self.AllUsers.push(new UserModel(data[i]));
                    }
            });
        }
    }


    //deactivate user
    DeleteUser = function (_data) {
        $.postDATA("Service.ashx?method=DeleteUser", "UserId=" + _data._RefID(), function (data) {     
                if (data) {
                    _data.Status("I");
                }
        });
    }


    //activate user
    ActivateUser = function (_data) {
        //ajax call to hit the db server
        $.postDATA("Service.ashx?method=ActivateUser", "UserId=" + _data._RefID(), function (data) {     
                if (data) {
                    _data.Status("A");
                }
        });
    }


    //get paop up
    EditUser = function (_data) {
        _data.ShowPopup(true);

    }


    //update user details
    UpdateDetails = function (_data) {
        var _gender = jQuery("#ddlGender option:selected").text().trim();

        if (_gender == "Male") {
            _gender = "1";
        }
        if (_gender == "Female") {
            _gender = "2";
        }
        if (_gender == "Other") {
            _gender = "3";
        }
        //Validation

        var _loginName = $("#txtLogin").val().trim();
        var _fullName = $("#txtFullName").val().trim();
        var _emailId = $("#txtEmail").val().trim();
        var atpos = _emailId.indexOf("@");
        var dotpos = _emailId.lastIndexOf(".");
        var _password = $("#txtPassword").val().trim();
        var _dobValue = $("#txtDob").val().trim();
        
        //check empty string inputs
        if (_loginName == "") {
            alert("Login name should not be empty");
            return false;
        }
        if (_fullName == "") {
            alert("Full name should not be empty");
            return false;
        } if (_emailId == "") {
            alert("E-mail address should not be empty");
            return false;
        } if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= _emailId.length) {
            alert("Not a valid e-mail address");
            return false;
        } if (_password == "") {
            alert("Password should not be empty");
            return false;
        } if (_dobValue == "") {
            alert("Date of birth should not be empty");
            return false;
        } 
        else {
            if (_data.checkEmail()) {
                //ajax call to hit the db server
                var   _frmdata= "UserId=" + _data._RefID() + "&LoginName=" + _loginName + "&FullName=" + _fullName + "&EmailAddress=" + _emailId + "&Password=" + _password + "&Gender=" + _gender + "&Dob=" + _dobValue;                
                $.postDATA("Service.ashx?method=EditUserDetails",_frmdata , function (data) {     
                        if (data) {
                            //set data into list view model
                            _data.ShowPopup(false);
                            _data.UserGender(data.UserGender);
                            _data.DateOfBirth(data.DateOfBirth);
                            _data.UserCreatedDate(data.UserCreatedDate);
                            _data.LastOnlineTime(data.LastOnlineTime);
                            _data.UserAgeInYears(data.UserAgeInYears);
                        }
                        else {
                            $("#dvMsg").text("User details not updated.");
                        }
                });
            }
        }

    }


    //get next 5 users data and push into list view model
    GetNextScrollDown = function (_UserIds) {
        var _count = 5;
        var _Key = "";
        //ajax call to hit the db server
        var  _frmdata= "UserId=" + _UserIds + "&Count=" + _count + "&Key=" + _Key;
        $.postDATA("Service.ashx?method=GetNextScrollDown",_frmdata , function (data) {     
                for (var i = 0; i < data.length; i++) {
                    self.AllUsers.push(new UserModel(data[i]));
                }
        });
    }


    ClosePopup = function (_data) {
        _data.ShowPopup(false);
    }


    //check Email Address
    CheckEmailId = function (_data) {
        var _UserRefId = $("#hdnRefId").val().trim();
        var _emailId = $("#txtEmail").val().trim();
        //ajax call to hit the db server
        var _frmdata= "Type=U&EmailId=" + _emailId + "&UserId=" + _UserRefId;
        $.postDATA("Service.ashx?method=CheckEmailAddress",_frmdata , function (data) {     
                if (!data) {
                    //show message and set model values
                    $("#dvMsg").text("Email-id already exists");
                    _data.checkEmail(false);
                    return false;
                }
                else {
                    $("#dvMsg").text("");
                    _data.checkEmail(true);
                    return true;
                }
        });
    }


    //bind the date pickers into popup box model
    ko.bindingHandlers.datepicker = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            //initialize datepicker with some optional options
            var options = allBindingsAccessor().datepickerOptions || {};
            $(element).datepicker(options);

            //handle the field changing
            ko.utils.registerEventHandler(element, "change", function () {
                var observable = valueAccessor();
                observable($(element).datepicker("getDate"));
            });

            //handle disposal (if KO removes by the template binding)
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                $(element).datepicker("destroy");
            });

        },
        //update the control when the view model changes

    };

}

//go to create user page
AddNewUser = function () {
    window.location.href = "CreateUser.aspx";
}
