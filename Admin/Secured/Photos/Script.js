
//ready function default values setting
$(document).ready(function () {

    document.getElementById("rdoRequested").checked = true;
    GetPhotosBasedonStatus(0);
    $("#dvSearch").fadeOut(300);

})


//---------------Photos count setting---------------------//
$(document).ready(function () {

    $("#dvOneImg").hide();

    SetPhotosCount();

    $("#btnGo").click(function () {
        var _formData = "UserName=" + $("#txtUser").val();
        $.postDATA("Service.ashx?method=GetPhotosBasedOnUserName", _formData, function (_data) {
            RemoveAndReBind(_data);
        });
    });

});


function SetPhotosCount() {
    $.postDATA("Service.ashx?method=GetPhotosCount", "", function (_data) {
        $("#lblRequested").text("[" + _data.Requested + "]");
        $("#lblReported").text("[" + _data.Reported + "]");
        $("#lblRejected").text("[" + _data.Rejected + "]");
        $("#lblPending").text("[" + _data.Pending + "]");
        $("#lblApproved").text("[" + _data.Approved + "]");
    });
}

//---------------Ended Photos count setting---------------------//


//showing please wait image
function ShowWait() {
    $("#imgPleaseWait").removeClass("displaynone");
}



//hide please wait image
function HideWait() {
    $("#imgPleaseWait").addClass("displaynone");
}


//----------------------Photos View Model-----------------------
function PhotosModel(_obj, AStatus, RStatus) {
    var _self = this;
    _self._id = ko.observable(_obj._id);
    _self.PhotoId = ko.observable(_obj.PhotoId);
    _self.AlbumId = ko.observable(_obj.AlbumId);
    _self.GetAdminPhotoReport = ko.observable(_obj.GetAdminPhotoReport);
    _self.UserId = ko.observable(_obj.UserId);
    _self.AttachmentId = ko.observable(_obj.AttachmentId);
    _self.IsUserDefault = ko.observable(_obj.IsUserDefault);
    _self.IsAlbumDefault = ko.observable(_obj.IsAlbumDefault);
    _self.CreatedDate = ko.observable(_obj.CreatedDate);
    _self.Status = ko.observable(_obj.Status);
    _self.PhotoSize = ko.observable(_obj.PhotoSize);
    _self.StatusPrimary = ko.observable(_obj.StatusPrimary);
    _self.StatusSecondary = ko.observable(_obj.StatusSecondary);
    _self.GetAttachments = ko.observable(_obj.GetAttachments);
    _self.isOpen = ko.observable(false);
    _self.isRejected = ko.observable(RStatus);
    _self.isApproved = ko.observable(AStatus);
    _self.Selected = ko.observable(false);
    _self.SelectedChoices = ko.observableArray([]);

    _self.AttachmentPath = ko.computed(function () {
        if (_self.GetAttachments() != null) {
            return _self.GetAttachments().AttachmentPath;
        }
        return "";
    }, this);

    _self.AttachmentId = ko.computed(function () {
        if (_self.GetAttachments() != null) {
            return _self.GetAttachments()._RefID;
        }
        return "";
    }, this);

}



//jquery popup with knockout binding
ko.bindingHandlers.dialog = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        var options = ko.utils.unwrapObservable(valueAccessor()) || {};
        //do in a setTimeout, so the applyBindings doesn't bind twice from element being copied and moved to bottom
        setTimeout(function () {
            options.close = function () {
                allBindingsAccessor().dialogVisible(false);
            };

            $(element).dialog(options);
        }, 0);

        //handle disposal (not strictly necessary in this scenario)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).dialog("destroy");
        });
    },
    update: function (element, valueAccessor, allBindingsAccessor) {
        var shouldBeOpen = ko.utils.unwrapObservable(allBindingsAccessor().dialogVisible),
            $el = $(element),
            dialog = $el.data("uiDialog") || $el.data("dialog");

        //don't call open/close before initilization
        if (dialog) {
            $el.dialog(shouldBeOpen ? "open" : "close");
        }
    }
};



//create a list view model
function ListPhotosModel(_list) {

    var self = this;
    self.AllPhotos = ko.observableArray();
    for (var i = 0; i < _list.length; i++) {
        self.AllPhotos.push(new PhotosModel(_list[i]));
    }


    RemoveAndReBind = function (data) {
        $("#btnApproveSelected").show();
        $("#btnRejectSelected").show();
        self.AllPhotos.removeAll();
        for (var i = 0; i < data.length; i++) {
            self.AllPhotos.push(new PhotosModel(data[i], true, true));
        }
    }


    ReBindApproved = function (data) {
        self.AllPhotos.removeAll();
        $("#btnRejectSelected").show();
        $("#btnApproveSelected").hide();
        for (var i = 0; i < data.length; i++) {
            self.AllPhotos.push(new PhotosModel(data[i], false, true));
        }
    }


    ReBindRejected = function (data) {
        self.AllPhotos.removeAll();
        $("#btnApproveSelected").show();
        $("#btnRejectSelected").hide();
        self.isApproved = ko.observable(false);
        for (var i = 0; i < data.length; i++) {
            self.AllPhotos.push(new PhotosModel(data[i], true, false));
        }
    }


    RemoveAllPhotos = function () {
        self.AllPhotos.removeAll();
    }


    RemoveOneObject = function (data) {
        self.AllPhotos.remove(data);
    }


    open = function () {
        this.isOpen(true);
    }


    close = function () {
        this.isOpen(false);
    }


    SelectAll = ko.computed({
        read: function () {
            var item = ko.utils.arrayFirst(self.AllPhotos(), function (item) {
                return !item.Selected();
            });
            return item == null;
        },
        write: function (value) {
            ko.utils.arrayForEach(self.AllPhotos(), function (person) {
                person.Selected(value);
            });
        }
    });


    GetSelectedPhots = function () {
        var arrauids = new Array();
        var j = 0;
        for (var i = 0; i < self.AllPhotos().length; i++) {
            if (self.AllPhotos()[i].Selected()) {
                arrauids[j] = self.AllPhotos()[i].PhotoId();
                j++;
            }
        }
        return arrauids;
    }


    GetSelectedPhotoObjects = function () {
        self.AllSelectedPhotos = ko.observableArray();
        for (var i = 0; i < self.AllPhotos().length; i++) {
            if (self.AllPhotos()[i].Selected()) {
                self.AllSelectedPhotos.push(self.AllPhotos()[i]);
            }
        }
        return self.AllSelectedPhotos();
    }


    ApprovePhoto = function (data) {
        var _comment = "";
        $("#dvImgs").html("");
        $("#dvalreadyImgs").html("");
        $("#lblalready").text("");
        $("#lblMsg").text("");
        // alert(data.isRejected());
        if (G_AdminID == "SUPERADMIN") {
            G_AdminID = "0";
        }
        var _formData = "AdminId=" + G_AdminID + "&PhotoId=" + data.PhotoId() + "&Status=true&Comment=" + _comment + "&IsRejected=" + data.isRejected();
        $.postDATA("Service.ashx?method=ApproveOrRejectPhoto", _formData, function (_data) {
            $("#dvOneImg").show();
            $("#dvImgs").hide();
            SetPhotosCount();
            if (JSON.stringify(_data.PhotoId) == "0") {
                $("#lblMsg").text("You have already approved this photo");
                $("#imgLast").attr("src", data.AttachmentPath());
                data.isOpen(false);
            } else {
                $("#lblMsg").text("Your last approved photo");
                $("#imgLast").attr("src", data.AttachmentPath());
                data.isOpen(false);
                self.AllPhotos.remove(data);
            }
        });

    }


    RejectPhoto = function (data) {
        if (G_AdminID == "SUPERADMIN") {
            G_AdminID = "0";
        }
        var person = prompt("Please enter your comment", "");
        $("#dvImgs").html("");
        $("#dvalreadyImgs").html("");
        $("#lblalready").text("");
        $("#lblMsg").text("");
        if (person != null) {
            var _PhotoId = $("#hdnPhotoId").val();
            var _comment = person;
            var _formData = "AdminId=" + G_AdminID + "&PhotoId=" + data.PhotoId() + "&Status=false&Comment=" + _comment + "&IsRejected=" + data.isRejected();;
            ShowWait();
            $.postDATA("Service.ashx?method=ApproveOrRejectPhoto", _formData, function (_data) {
                $("#lblMsg").text("Your last rejected photo");
                $("#dvOneImg").show();
                $("#dvImgs").hide();
                $("#imgLast").attr("src", data.AttachmentPath());
                data.isOpen(false);
                self.AllPhotos.remove(data);
                SetPhotosCount();
            });
            HideWait();
        }
    }


    ApproveSelected = function (data) {
        GetSelectedPhotoObjects();
        if (self.AllSelectedPhotos().length > 0) {
            if (G_AdminID == "SUPERADMIN") {
                G_AdminID = "0";
            }
            $("#dvImgs").show();
            var obj = GetSelectedPhotoObjects();
            var _formData = "AdminId=" + G_AdminID + "&PhotoId=" + GetSelectedPhots() + "&Status=true&Comment=0&IsRejected=" + JSON.stringify(self.AllSelectedPhotos()[0].isRejected());
            ShowWait();
            $("#dvImgs").html("");
            $("#dvalreadyImgs").html("");
            $("#lblalready").text("");
            $("#lblMsg").text("");
            $.postDATA("Service.ashx?method=ApproveOrRejectBulkPhotos", _formData, function (_data) {
                HideWait();
                //   alert(_data);
                if (_data.length > 0) {
                    var inHtml = "";
                    var outhtml = "";
                    for (var i = 0; i < _data.length; i++) {
                        if (_data[i].PhotoId != 0) {
                            $("#dvImgs").css('height', '300px');
                            if (inHtml == "") {
                                $("#lblMsg").text("Your last approved photos");
                                inHtml = "<div  style='float: left; width: 75px; height: 75px; border: 1px solid #ccc; border-radius: 6px; margin-left: 2px; text-align: center; padding: 5px;'><img alt='' style='float: left; width: 75px; height: 75px;' src='" + _data[i].GetAttachments.AttachmentPath + "'/></div>";
                            }
                            else {
                                inHtml = inHtml + "<div  style='float: left; width: 75px; height: 75px; border: 1px solid #ccc; border-radius: 6px; margin-left: 2px; text-align: center; padding: 5px;'><img alt='' style='float: left; width: 75px; height: 75px;' src='" + _data[i].GetAttachments.AttachmentPath + "'/></div>";
                            }
                        }
                        else {
                            $("#dvalreadyImgs").css('height', '300px');
                            if (_data[i].PhotoId != self.AllSelectedPhotos()[i].PhotoId())
                                if (outhtml == "") {
                                    $("#lblalready").text("Your already approved photos");
                                    outhtml = "<div  style='float: left; width: 75px; height: 75px; border: 1px solid #ccc; border-radius: 6px; margin-left: 2px; text-align: center; padding: 5px;'><img alt='' style='float: left; width: 75px; height: 75px;' src='" + self.AllSelectedPhotos()[i].AttachmentPath() + "'/></div>";
                                }
                                else {
                                    outhtml = outhtml + "<div  style='float: left; width: 75px; height: 75px; border: 1px solid #ccc; border-radius: 6px; margin-left: 2px; text-align: center; padding: 5px;'><img alt='' style='float: left; width: 75px; height: 75px;' src='" + self.AllSelectedPhotos()[i].AttachmentPath() + "'/></div>";
                                }

                        }
                    }
                    self.AllPhotos.removeAll(self.AllSelectedPhotos());
                    $("#dvImgs").html(inHtml);
                    $("#dvalreadyImgs").html(outhtml);
                    $("#dvOneImg").hide();
                    SetPhotosCount();
                }
            });
        }
        else {
            alert("Please select photos");
        }
    }


    RejectSelected = function (data) {
        GetSelectedPhotoObjects();
        if (self.AllSelectedPhotos().length > 0) {
            if (G_AdminID == "SUPERADMIN") {
                G_AdminID = "0";
            }
            $("#dvImgs").show();
            $("#dvImgs").html("");
            $("#dvalreadyImgs").html("");
            $("#lblalready").text("");
            $("#lblMsg").text("");
            var obj = GetSelectedPhotoObjects();
            var person = prompt("Please enter your comment", "");

            if (person != "") {
                var _formData = "AdminId=" + G_AdminID + "&PhotoId=" + GetSelectedPhots() + "&Status=false&Comment=" + person + "&IsRejected=true";
                ShowWait();
                $.postDATA("Service.ashx?method=ApproveOrRejectBulkPhotos", _formData, function (_data) {
                    HideWait();
                    $("#lblMsg").text("Your last Rejected photos");
                    var inHtml = "";
                    for (var i = 0; i < _data.length; i++) {
                        if (_data[i].PhotoId != 0) {

                            if (inHtml == "") {
                                inHtml = "<div  style='float: left; width: 75px; height: 75px; border: 1px solid #ccc; border-radius: 6px; margin-left: 2px; text-align: center; padding: 5px;'><img alt='' style='float: left; width: 75px; height: 75px;' src='" + _data[i].GetAttachments.AttachmentPath + "'/></div>";
                            }
                            else {
                                inHtml = inHtml + "<div  style='float: left; width: 75px; height: 75px; border: 1px solid #ccc; border-radius: 6px; margin-left: 2px; text-align: center; padding: 5px;'><img alt='' style='float: left; width: 75px; height: 75px;' src='" + _data[i].GetAttachments.AttachmentPath + "'/></div>";
                            }
                        }
                    }
                    $("#dvImgs").html(inHtml);
                    $("#dvOneImg").hide();
                    SetPhotosCount();
                    self.AllPhotos.removeAll(self.AllSelectedPhotos());
                });
            }
            else {
                $("#lblMsg").text("Please enter good reason for reject");
            }
        }
        else {
            alert("Please select photos");
        }
    }


}



//ready function radio button change
$(document).ready(function () {

    $("#dvSearch").hide();

    _Live.client.setAdminID = function (_adminid) {
        G_AdminID = _adminid;
    };

    $('input:radio[name="rdoPhotos"]').change(function () {

        //Requested
        if ($(this).val() == 'A') {
            GetPhotosBasedonStatus(0);
            $("#dvSearch").fadeOut(300);
        }


        //Reported
        if ($(this).val() == 'B') {
            ReportedPhotos();
            $("#dvSearch").fadeOut(300);
        }


        //Rejected
        if ($(this).val() == 'C') {
            GetRejectedPhotos(-1);
            $("#dvSearch").fadeOut(300);
        }


        //Pending
        if ($(this).val() == 'D') {
            GetPhotosBasedonStatus(1);
            $("#dvSearch").fadeOut(300);
        }


        //Approved
        if ($(this).val() == 'E') {
            GetApprovedPhotos(2);
            $("#dvSearch").fadeOut(300);
        }


        //Search by user
        if ($(this).val() == 'F') {
            RemoveAllPhotos();
            $("#dvSearch").fadeIn(300);

        }


    });

});



//ready function for knockout dummy binding
$(document).ready(function () {

    var _self = new Object();
    _self._id = "";
    _self.PhotoId = "";
    _self.AlbumId = "";
    _self.AttachmentId = "";
    _self.IsUserDefault = "";
    _self.IsAlbumDefault = "";
    _self.CreatedDate = "";
    _self.Status = "";
    _self.PhotoSize = "";
    _self.StatusPrimary = "";
    _self.StatusSecondary = "";
    var _dummyArray = new Array();
    _dummyArray.push(_self);

    //bind ko 
    ko.applyBindings(new ListPhotosModel(_dummyArray), document.getElementById('dvPhotosList'));

});



function ReportedPhotos() {

    $.postDATA("Service.ashx?method=GetReportedPhotos", "", function (data) {
        ReBindApproved(data);
    });

}



function GetPhotosBasedonStatus(status) {

    ShowWait();

    $.postDATA("Service.ashx?method=GetPhotosBasedOnStatus", "Status=" + status, function (data) {
        RemoveAndReBind(data);
        HideWait();
    });
}



function GetApprovedPhotos(status) {

    ShowWait();

    $.postDATA("Service.ashx?method=GetPhotosBasedOnStatus", "Status=" + status, function (data) {
        ReBindApproved(data);
        HideWait();
    });

}



function GetRejectedPhotos(status) {

    ShowWait();

    $.postDATA("Service.ashx?method=GetPhotosBasedOnStatus", "Status=" + status, function (data) {
        ReBindRejected(data);
        HideWait();
    });

}
