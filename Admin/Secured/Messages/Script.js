
//hiding div's function
function hidedivs() {
            $("#dvSearch").hide();
            $("#dvLocation").hide();
            $("#dvText").hide();
            $("#dvAge").hide();
            $("#dvEthnicity").hide();
            $("#dvReligion").hide();
            $("#dvEducation").hide();
            $("#dvHavechildren").hide();
            $("#dvDrink").hide();
            $("#dvSmoke").hide();
            $("#dvBodytype").hide();
            $("#dvHoroscope").hide(); 
            $("#dvManageMsgs").hide();
}

//showing please wait image
function ShowWait() {
    $("#imgPleaseWait").removeClass("displaynone");
}



//hide please wait image
function HideWait() {
    $("#imgPleaseWait").addClass("displaynone");
}


// Ready function for go button click and jquery text editor
$(document).ready(function () {

    //calling jquert text editor 
    $('.jqte-test').jqte();
    
    //Go button click function
    $("#btnGo").click(function () {
        var _Inputs = JSON.stringify(GetAllCheckedCheckBoxValues());
        
        $.postJSON("Service.ashx?method=GetUsersBasedOnSearch", "SearchData=" + _Inputs, function (data) {
            var usersCount = data.length;
            $("#lblUsers").text("Total Users Count : " + usersCount);
            $("#hdnUserIds").val(data);
        });
    });
});

// Ready function for go binding ko and slideer (age)
$(document).ready(function () {
    //calling hide divs function
    hidedivs();
    //insert some dummy data into model
    var _self = new Object();
    _self._id = "";
    _self._RefID = "";
    _self.LoginName = "";
    _self.EmailAddress = "";
    var _dummyArray = new Array();
    _dummyArray.push(_self);
    //bind ko 
    ko.applyBindings(new ListUserNewModel(_dummyArray), document.getElementById('dvUsersList'));
    $("#dvslider").slider({ range: true });
    $("#dvslider").slider({ min: 18 });
    $("#dvslider").slider({ max: 100 });
    $("#dvslider").slider({ values: [18, 26] });
    $("#lblminAge").text("18");
    $("#lblmaxAge").text("26");
    $("#dvslider").slider({
        change: function (event, ui) {
            var values = $("#dvslider").slider("values");
            $("#lblminAge").text(values[0]);
            $("#lblmaxAge").text(values[1]);
                    
        }

    });
});

// Ready function for go check box events
$(document).ready(function () {

    $("#chkAge").change(function () {
        if ($('#chkAge').is(":checked")) {
            $("#dvAge").fadeIn(300);
            var values = $("#dvslider").slider("values");
                   
        } else {
            $("#dvAge").fadeOut(300);
            var values = $("#dvslider").slider("values");
        }
    })

    $("#chkLocation").change(function () {
               
        if ($('#chkLocation').is(":checked")) {
            $("#dvLocation").fadeIn(300);
            $.postDATA("Service.ashx?method=GetLocations", "{}", function (data) {
                if ($("#dvLocation").text().trim() == "") {
                    ko.applyBindings(new ListLocationModel(data), document.getElementById('dvLocation'));
                }
            });
        } else {
            $("#dvLocation").fadeOut(300);
        }
    })

    $("#chkEthnicity").change(function () {
        if ($('#chkEthnicity').is(":checked")) {
            $("#dvEthnicity").fadeIn(300);
            $.postDATA("Service.ashx?method=GetEthnicity", "{}", function (data) {
                    if ($("#dvEthnicity").text().trim() == "") {
                        ko.applyBindings(new ListEthnicityModel(data), document.getElementById('dvEthnicity'));
                    }
            });
        } else {
            $("#dvEthnicity").fadeOut(300);
        }
    })

    $("#chkReligion").change(function () {
        if ($('#chkReligion').is(":checked")) {
            $("#dvReligion").fadeIn(300);
            $.postDATA("Service.ashx?method=GetReligion", "{}", function (data) {
                    if ($("#dvReligion").text().trim() == "") {
                        ko.applyBindings(new ListReligionModel(data), document.getElementById('dvReligion'));
                    }
            });
        } else {
            $("#dvReligion").fadeOut(300);
        }
    })

    $("#chkEducation").change(function () {
        if ($('#chkEducation').is(":checked")) {
            $("#dvEducation").fadeIn(300);
            $.postDATA("Service.ashx?method=GetEducationDetails", "{}", function (data) {              
                    if ($("#dvEducation").text().trim() == "") {
                        ko.applyBindings(new ListEducationModel(data), document.getElementById('dvEducation'));
                    }
            });
        } else {
            $("#dvEducation").fadeOut(300);
        }
    })



    //-----------------Have children-------------------------
    $("#chkHavechildren").change(function () {
        if ($('#chkHavechildren').is(":checked")) {
            $("#dvHavechildren").fadeIn(300);
            $.postDATA("Service.ashx?method=GetHaveChildrenDetails", "{}", function (data) {     
                    if ($("#dvHavechildren").text().trim() == "") {
                        ko.applyBindings(new ListHaveChildrenModel(data), document.getElementById('dvHavechildren'));
                    }
            });
        } else {
            $("#dvHavechildren").fadeOut(300);
        }
    })

    //----------------------Drink----------------------------
    $("#chkDrink").change(function () {
        if ($('#chkDrink').is(":checked")) {
            $("#dvDrink").fadeIn(300);
            $.postDATA("Service.ashx?method=GetDrinkDetails", "{}", function (data) { 
                    if ($("#dvDrink").text().trim() == "") {
                        ko.applyBindings(new ListDrinkModel(data), document.getElementById('dvDrink'));
                    }
            });
        } else {
            $("#dvDrink").fadeOut(300);
        }
    })

    //---------------------smoke-----------------------------
    $("#chkSmoke").change(function () {
        if ($('#chkSmoke').is(":checked")) {
            $("#dvSmoke").fadeIn(300);
            $.postDATA("Service.ashx?method=GetSmokeDetails", "{}", function (data) { 
                    if ($("#dvSmoke").text().trim() == "") {
                        ko.applyBindings(new ListSmokeModel(data), document.getElementById('dvSmoke'));
                    }
            });
        } else {
            $("#dvSmoke").fadeOut(300);
        }
    })

    //----------------------BodyType-------------------------
    $("#chkBodytype").change(function () {
        if ($('#chkBodytype').is(":checked")) {
            $("#dvBodytype").fadeIn(300);
            $.postDATA("Service.ashx?method=GetBodyTypeDetails", "{}", function (data) { 
                    if ($("#dvBodytype").text().trim() == "") {
                        ko.applyBindings(new ListBodytypeModel(data), document.getElementById('dvBodytype'));
                    }
            });
        } else {
            $("#dvBodytype").fadeOut(300);
        }
    })

    //---------------------------Horoscope-------------------
    $("#chkHoroscope").change(function () {
        if ($('#chkHoroscope').is(":checked")) {
            $("#dvHoroscope").fadeIn(300);
            $.postDATA("Service.ashx?method=GetHoroscopeDetails", "{}", function (data) { 
                    if ($("#dvHoroscope").text().trim() == "") {
                        ko.applyBindings(new ListHoroscopeModel(data), document.getElementById('dvHoroscope'));
                    }
            });
        } else {
            $("#dvHoroscope").fadeOut(300);
        }
    })

});

$(document).ready(function () {

    $('input:radio[name="rdomsg"]').change(function () {
        hidedivs();
        $("#dvMsgBox").fadeIn(300);
        if ($(this).val() == 'A') {                 
            $.postDATA("Service.ashx?method=GetAllUsers", "{}", function (data) { 
                    $("#lblUsers").text("Total Users Count : " + data.length);
            });

        } else {
            $('input:checkbox').removeAttr('checked');
            $("#lblUsers").text("");
        }
        if ($(this).val() == 'B') {
            $("#dvText").fadeIn(300);
        }
        else {
            $("#dvText").fadeOut();
            $('input:checkbox').removeAttr('checked');
            $("#lblUsers").text("");

        }
        if (($(this).val() == 'C')) {
            $("#dvSearch").fadeIn(300);
        } else {
            $("#dvSearch").fadeOut();
            $('input:checkbox').removeAttr('checked');
            $("#lblUsers").text("");

        }
    });

    //setting tokenInput function to textbox
    $("#txtUser").tokenInput("Service.ashx?method=SearchUser", {
        onResult: function (results) {
            $.each(results, function (index, value) {
                value.name = value.LoginName;
                value.id = value._RefID;
            });
            return results;
        },
        onAdd: function (item) {
            var objects = $("#txtUser").tokenInput("get");
            $("#lblUsers").text("Total Users Count : " + objects.length);
        },
        onDelete: function (item) {
            var objects = $("#txtUser").tokenInput("get");
            $("#lblUsers").text("Total Users Count : " + objects.length);
        }               
    });


    $("#btnSendMessage").click(function () {
      
        //first check the textbox is empty or not
        if ($("#txtMessage").val()!=""){
            //for multiple specific users
            if ($("#rdoMulty").is(":checked")) {
                var selectedArrayIds = new Array();
                var objects = $("#txtUser").tokenInput("get");
                for (var i = 0; i < objects.length; i++) {
                    selectedArrayIds[i] = objects[i].id;
                }
                if (G_AdminID == "SUPERADMIN") {
                    G_AdminID = "0";
                }
                var frmdata = "AdminId=" + G_AdminID + "&RecipientId=" + selectedArrayIds + "&Subject=" + $("#txtSubject").val() + "&Message=" + escape($("#txtMessage").val());
                ShowWait();
                $.postDATA("Service.ashx?method=SendMessage", frmdata, function (data) {
                     HideWait();
                    if (data) {
                            $("#lblMsg").text("Your message has been sent.");
                            $("#txtMessage").val("");
                        }
                        else {
                            $("#lblMsg").text("Message sending field");
                        }
                });
            }
            else {
                // $("#lblMsg").text("Plese choose any one");
            }
            //for all users
            if ($("#rdoAllUsers").is(":checked")) {
                // SendMessageToAllUsers
                if (G_AdminID == "SUPERADMIN") {
                    G_AdminID = "0";
                }
                ShowWait();
                var _frmdata= "AdminId=" + G_AdminID + "&Subject=" + $("#txtSubject").val() + "&Message=" + escape($("#txtMessage").val());                   
                $.postDATA("Service.ashx?method=SendMessageToAllUsers", _frmdata, function (data) { 
                    HideWait();
                    if (data) {
                            $("#lblMsg").text("Your message has been sent.");
                            $("#txtMessage").val("");
                        }
                        else {
                            $("#lblMsg").text("Message sending field");
                        }
                });
            } else {

            }
            //for admin search options
            if ($("#rdoSearch").is(":checked")) {
                // SendMessage to search users
                if (G_AdminID == "SUPERADMIN") {
                    G_AdminID = "0";
                }
                var userRefids = new Array();
                userRefids = $("#hdnUserIds").val().split(',');
                // alert(userRefids);
                ShowWait();
                var _frmdata= "AdminId=" + AdminId + "&RecipientId=" + userRefids + "&Subject=" + $("#txtSubject").val() + "&Message=" + escape($("#txtMessage").val());                 
                $.postDATA("Service.ashx?method=SendMessage", _frmdata, function (data) { 
                    HideWait();
                    if (data) {
                            $("#lblMsg").text("Your message has been sent.");
                            $("#txtMessage").val("");
                        }
                        else {
                            $("#lblMsg").text("Message sending field");
                        }
                });
            } else {

            }
        } else {
            $("#lblMsg").text("Please enter your message");
        }
    });
         
});

//-------------Creating view models----------------------------

//-------------location view model-----------------------------
function LocationModel(_obj) {
    var _self = this;
    _self._id = ko.observable(_obj._id);
    _self._RefID = ko.observable(_obj._RefID);
    _self.LocationName = ko.observable(_obj.LocationName);
    _self.Status = ko.observable(_obj.Status);
}

//create a list view model for location
function ListLocationModel(_list) {
    var self = this;
    self.AllLocations = ko.observableArray();
    for (var i = 0; i < _list.length; i++) {
        self.AllLocations.push(new LocationModel(_list[i]));

    }

    LocationChange = function (data) {
        //Location checked list
        var _locations = $('input[name=LocationList]:checked').map(function () {
            return $(this).val();
        }).get();
        // alert(_locations);
        if ($('input[name=LocationList]').is(":checked")) {

        } else {

        }
        var _userids = GetAllUserRefIds();

        if (_userids.length > 1) {
            //   alert(_userids);
        }
    }


}



//----------Ethnicity View Model-------------------------------
function EthnicityModel(_obj) {
    var _self = this;
    _self._id = ko.observable(_obj._id);
    _self._RefID = ko.observable(_obj._RefID);
    _self.EthnicityName = ko.observable(_obj.EthnicityName);
    _self.Status = ko.observable(_obj.Status);
}

//create a list view model for Ethnicity
function ListEthnicityModel(_list) {
    var self = this;
    self.AllEthnicites = ko.observableArray();
    for (var i = 0; i < _list.length; i++) {
        self.AllEthnicites.push(new EthnicityModel(_list[i]));

    }
}


//----------Religion View Model-------------------------------
function ReligionModel(_obj) {
    var _self = this;
    _self._id = ko.observable(_obj._id);
    _self._RefID = ko.observable(_obj._RefID);
    _self.ReligionType = ko.observable(_obj.ReligionType);
    _self.Status = ko.observable(_obj.Status);
}

//create a list view model
function ListReligionModel(_list) {
    var self = this;
    self.AllReligions = ko.observableArray();
    for (var i = 0; i < _list.length; i++) {
        self.AllReligions.push(new ReligionModel(_list[i]));

    }
}


//----------Education View Model-------------------------------
function EducationModel(_obj) {
    var _self = this;
    _self._id = ko.observable(_obj._id);
    _self._RefID = ko.observable(_obj._RefID);
    _self.Qualification = ko.observable(_obj.Qualification);
    _self.Status = ko.observable(_obj.Status);
}

//create a list view model
function ListEducationModel(_list) {
    var self = this;
    self.AllQualifications = ko.observableArray();
    for (var i = 0; i < _list.length; i++) {
        self.AllQualifications.push(new EducationModel(_list[i]));

    }
}



//----------------------HaveChildren View Model----------------
function HaveChildrenModel(_obj) {
    var _self = this;
    _self._id = ko.observable(_obj._id);
    _self._RefID = ko.observable(_obj._RefID);
    _self.Description = ko.observable(_obj.Description);
    _self.Status = ko.observable(_obj.Status);
}

//create a list view model for HaveChildren
function ListHaveChildrenModel(_list) {
    var self = this;
    self.AllHaveChildrens = ko.observableArray();
    for (var i = 0; i < _list.length; i++) {
        self.AllHaveChildrens.push(new HaveChildrenModel(_list[i]));

    }
}



//-------------------------Drink View Model-------------------
function DrinkModel(_obj) {
    var _self = this;
    _self._id = ko.observable(_obj._id);
    _self._RefID = ko.observable(_obj._RefID);
    _self.Description = ko.observable(_obj.Description);
    _self.Status = ko.observable(_obj.Status);
}

//create a list view model for Drink
function ListDrinkModel(_list) {
    var self = this;
    self.AllDrinkTypes = ko.observableArray();
    for (var i = 0; i < _list.length; i++) {
        self.AllDrinkTypes.push(new DrinkModel(_list[i]));
    }
}



//----------------------Smoke View Model-----------------------
function SmokeModel(_obj) {
    var _self = this;
    _self._id = ko.observable(_obj._id);
    _self._RefID = ko.observable(_obj._RefID);
    _self.SmokeDetails = ko.observable(_obj.SmokeDetails);
    _self.Status = ko.observable(_obj.Status);
}

//create a list view model for smoke
function ListSmokeModel(_list) {
    var self = this;
    self.AllSmokers = ko.observableArray();
    for (var i = 0; i < _list.length; i++) {
        self.AllSmokers.push(new SmokeModel(_list[i]));

    }
}



//-------------Bodytype View Model-----------------------------
function BodytypeModel(_obj) {
    var _self = this;
    _self._id = ko.observable(_obj._id);
    _self._RefID = ko.observable(_obj._RefID);
    _self.Description = ko.observable(_obj.Description);
    _self.Status = ko.observable(_obj.Status);
}

//create a list view model for Bodytype
function ListBodytypeModel(_list) {
    var self = this;
    self.AllBodyTypes = ko.observableArray();
    for (var i = 0; i < _list.length; i++) {
        self.AllBodyTypes.push(new BodytypeModel(_list[i]));

    }
}



//---------------Horoscope View Model---------------------------
function HoroscopeModel(_obj) {
    var _self = this;
    _self._id = ko.observable(_obj._id);
    _self._RefID = ko.observable(_obj._RefID);
    _self.Description = ko.observable(_obj.Description);
    _self.Status = ko.observable(_obj.Status);
}

//create a list view model
function ListHoroscopeModel(_list) {
    var self = this;
    self.AllHoroscope = ko.observableArray();
    for (var i = 0; i < _list.length; i++) {
        self.AllHoroscope.push(new HoroscopeModel(_list[i]));

    }
}


//---------------Search User View Model------------------------
function UserNewModel(_obj) {
    var _self = this;
    _self._id = ko.observable(_obj._id);
    _self._RefID = ko.observable(_obj._RefID);
    _self.LoginName = ko.observable(_obj.LoginName);
    _self.EmailAddress = ko.observable(_obj.EmailAddress);
    _self.GetUserProfile = ko.observableArray();

}

//create a list view model
function ListUserNewModel(_list) {
    var self = this;
    self.AllUsers = ko.observableArray();
    for (var i = 0; i < _list.length; i++) {
        self.AllUsers.push(new UserNewModel(_list[i]));

    }
    RemoveAndReBind = function (list) {
        self.AllUsers.removeAll();
        for (var i = 0; i < list.length; i++) {
            self.AllUsers.push(new UserNewModel(list[i]));
        }
    }
    RemoveListOfUsers = function (list) {
        self.AllUsers.splice(list);
        //alert(list);
    }
    RemoveAllUsers = function () {
        self.AllUsers.removeAll();
    }
    GetAllUserRefIds = function () {
        return self.AllUsers();
    }

}



//---------------Search Messages View Model---------------------
function MessageNewModel(_obj) {
    var _self = this;
    _self._id = ko.observable(_obj._id);
    _self.MessageRefID = ko.observable(_obj.MessageRefID);
    _self.MessageText = ko.observable(_obj.MessageText);
    _self.Status = ko.observable(_obj.Status);
}

//create a list view message model
function ListUserNewModel(_list) {
    var self = this;
    self.AllMessages= ko.observableArray();
    for (var i = 0; i < _list.length; i++) {
        self.AllMessages.push(new MessageNewModel(_list[i]));
    }
    DeleteMessage = function (data) {

    }
}


//getting all checked check boxes valus as an json object
function GetAllCheckedCheckBoxValues() {

    var _searchObject = new Object();
    var values = $("#dvslider").slider("values");
    if ($('#chkAge').is(":checked")) {
        _searchObject.Age=values[0]+"-"+values[1];
    }

    //Premium users 
    if ($('#chkPremium').is(":checked")) {
        _searchObject.Premium = true;
               
    }
    // Free users
    if ($('#chkFree').is(":checked")) {
        _searchObject.Free = false;
    }

    //Location checked list           
    if ($('#chkLocation').is(":checked")) {
        var _locations = $('input[name=LocationList]:checked').map(function () {
            return $(this).val();
        }).get();
        if (_locations.length > 0) {
            _searchObject.Location = _locations;
        } else {
            _searchObject.Location = ["0"];
        }
    }

    //Ethnicity checked list
    if ($('#chkEthnicity').is(":checked")) {
        //Ethnicity  checked list
        var _ethnicitys = $('input[name=EthnicityList]:checked').map(function () {
            return $(this).val();
        }).get();
        if (_ethnicitys.length > 0) {
            _searchObject.Ethnicity = _ethnicitys;
        } else {
            _searchObject.Ethnicity = ["0"];
        }
    }


    //Religion

    if ($('#chkReligion').is(":checked")) {
        //Religion checked list
        var _religion = $('input[name=ReligionList]:checked').map(function () {
            return $(this).val();
        }).get();

        if (_religion.length > 0) {
            _searchObject.Religion = _religion;
        } else {
            _searchObject.Religion = ["0"];
        }
    }

    // Education
    if ($('#chkEducation').is(":checked")) {
        //Education checked list
        var _education = $('input[name=EducationList]:checked').map(function () {
            return $(this).val();
        }).get();
        if (_education.length > 0) {
            _searchObject.Education = _education;
        } else {
            _searchObject.Education = ["0"];
        }
    }

    //Have children
    if ($('#chkHavechildren').is(":checked")) {
        //Have children checked list
        var _children = $('input[name=ChildrenList]:checked').map(function () {
            return $(this).val();
        }).get();
               
        if (_children.length > 0) {
            _searchObject.HaveChildren = _children;
        } else {
            _searchObject.HaveChildren = ["0"];
        }
    }
    //Drink 
    if ($('#chkDrink').is(":checked")) {
        //Drink checked list
        var _drink = $('input[name=DrinkList]:checked').map(function () {
            return $(this).val();
        }).get();
        if (_drink.length > 0) {
            _searchObject.Drink = _drink;
        } else {
            _searchObject.Drink = ["0"];
        }
    }

    //Smoke
    if ($('#chkSmoke').is(":checked")) {
        //Smoke checked list
        var _smoke = $('input[name=SmokeList]:checked').map(function () {
            return $(this).val();
        }).get();
        if (_smoke.length > 0) {
            _searchObject.Smoke = _smoke;
        } else {
            _searchObject.Smoke = ["0"];
        }
    }

    //body type
    if ($('#chkBodytype').is(":checked")) {
        //body type checked list
        var _bodyType = $('input[name=BodyList]:checked').map(function () {
            return $(this).val();
        }).get();    
        if (_bodyType.length > 0) {
            _searchObject.BodyType = _bodyType;
        } else {
            _searchObject.BodyType = ["0"];
        }
    }

    // Horoscope
    if ($('#chkHoroscope').is(":checked")) {              
        //Horoscope checked list
        var _horoscope = $('input[name=HoroscopeList]:checked').map(function () {
            return $(this).val();
        }).get();
        if (_horoscope.length > 0) {
            _searchObject.Horoscope = _horoscope;
        } else {
            _searchObject.Horoscope = ["0"];
        }
    }
    return _searchObject;
}


