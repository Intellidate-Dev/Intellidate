$(document).ready(function () {
    $("#chkIncludePreferAll").change(function () {
        if ($(this).prop('checked') == true) {
            if ($("#divIncludeAllText").hasClass("displaynone") == true) {
                $("#divIncludeAllText").removeClass("displaynone");
                $("#txtIncludeAllText").focus();
            }
        }
        else {
            if ($("#divIncludeAllText").hasClass("displaynone") == false) {
                $("#divIncludeAllText").addClass("displaynone");
            }
        }
    });
});

$(document).ready(function () {
    jQuery('textarea').elastic();

    $("#btnReset").click(function () {
        $("#lblError").html('');
        $("#txtCriteriaName").val('');
        //$("#txtCriteriaUserQuestion").val('');
        $("#txtCriteriaPreferenceQuestion").val('');
        $("#txtMismatchText").val('');
        $('select>option:eq(0)').attr('selected', true);
        $("#cboCriteriaType").trigger("change");
    }); 

    $("#btnCreate").click(function () {
        // Check if the critera name is not entered
        var _CriteriaName = $("#txtCriteriaName").val();
        if (_CriteriaName.trim() == "") {
            $("#lblError").html("Please enter the Criteria name.");
            return;
        }

        var _CriteriaType = $("#cboCriteriaType").val();
        if (_CriteriaType.trim() == "") {
            $("#lblError").html("Please select the Criteria type.");
            return;
        }

        var _items;
        if (_CriteriaType == '1' || _CriteriaType == '2') {
            _items = GetItems();
            if (_items.length == 0) {
                $("#lblError").html("Please enter some options.");
                return;
            }
        }
        

        var _UserQuestion = _CriteriaName; //$("#txtCriteriaUserQuestion").val();
        /*if (_UserQuestion.trim() == "") {
            $("#lblError").html("Please enter the User Question.");
            return;
        }*/

        var _PreferenceQuestion = $("#txtCriteriaPreferenceQuestion").val();
        if (_PreferenceQuestion.trim() == "") {
            $("#lblError").html("Please enter the Preference Question.");
            return;
        }

        var _MismatchText = $("#txtMismatchText").val();
        if (_MismatchText.trim() == "") {
            $("#lblError").html("Please enter the Mismatch text.");
            return;
        }

        var _IncludeAllPreference = false;
        if ($("#chkIncludePreferAll").prop("checked") == true) {
            _IncludeAllPreference = true;
        }

        var _IncludeAllPreferenceText = "";
        if (_IncludeAllPreference) {
            _IncludeAllPreferenceText = $("#txtIncludeAllText").val();
        }

        var _ShowMatch = false;
        if ($("#chkShowMatch").prop("checked") == true) {
            _ShowMatch = true;
        }

        var _PostObject = new Object();
        _PostObject.CriteriaName = _CriteriaName;
        _PostObject.CriteriaType = _CriteriaType;
        _PostObject.UserQuestion = _UserQuestion;
        _PostObject.PreferenceQuestion = _PreferenceQuestion;
        _PostObject.MismatchText = _MismatchText;

        if (_CriteriaType == '1' || _CriteriaType == '2') {
            _PostObject.Items = _items;
            _PostObject.IncludeAllPreference = _IncludeAllPreference;
            _PostObject.IncludeAllPreferenceText = _IncludeAllPreferenceText;
        } else {

            _PostObject.RangeType = $("#cboRangeType").val();
            _PostObject.MinValue = $("#txtMinValue").val();
            _PostObject.MaxValue = $("#txtMaxValue").val();
        }

        _PostObject.ShowMatch = _ShowMatch;
        _PostObject.RefID = $("#hdnCriteriaID").val();

        $.postJSON("/Secured/Criteria/Service.ashx?f=A", _PostObject, function (msg) {
            AddNewCriteria(msg);
            $("#lblError").html('');
            $("#txtCriteriaName").val('');
            //$("#txtCriteriaUserQuestion").val('');
            $("#txtCriteriaPreferenceQuestion").val('');
            $("#txtMismatchText").val('');
            $('select>option:eq(0)').attr('selected', true);
            $("#cboCriteriaType").trigger("change");
        });

    });

    $("#cboCriteriaType").change(function () {
        var _selectedOption = $(this).val();

        // -----------
        if (_selectedOption == 1 || _selectedOption == 2) {
            if ($(".ListOfOptions").hasClass("displaynone") == true) {
                $(".ListOfOptions").removeClass("displaynone");
            }
        } else {
            if ($(".ListOfOptions").hasClass("displaynone") == false) {
                $(".ListOfOptions").addClass("displaynone");
                DeleteAllOptions();
            }
        }


        if (_selectedOption == 3) {
            //
            if ($(".RangeOptions").hasClass("displaynone") == true) {
                $(".RangeOptions").removeClass("displaynone");
            }
        }
        else {
            if ($(".RangeOptions").hasClass("displaynone") == false) {
                $(".RangeOptions").addClass("displaynone");
            }
        }

    });
});

$(document).ready(function () {
    var _Dummy = new Array();
    var _Dummystring = "";
    _Dummy.push(_Dummystring);
    ko.applyBindings(new OptionsListViewModel(_Dummy), document.getElementById('divOptionsSection'));
    DeleteFirst();
    $("#txtPasteOptions").bind("paste", function () {
        var element = this;
        setTimeout(function () {
            var _pastedText = $(element).val();
            for (var i = 0; i < _pastedText.split('\n').length ; i++) {
                AddOption(_pastedText.split('\n')[i]);
            }
            $("#txtPasteOptions").hide();
            if ($("#divOptionsSection").hasClass("displaynone")) {
                $("#divOptionsSection").removeClass("displaynone");
            }

        }, 100);
    });

    $("#btnAddMore").click(function () {
        AddBlank();
    });

    $("#btnAddMore_2").click(function () {
        AddBlank();
    });

    $("#cboRangeType").change(function () {
        var _RangeType = $(this).val();
        

        switch (_RangeType) {
            case "1": {
                $("#txtMinValue").attr("placeholder", "100");
                $("#txtMaxValue").attr("placeholder", "10000");
                $(".units").html("&nbsp;$");
                break;
            }
            case "2": {
                $("#txtMinValue").attr("placeholder", "10");
                $("#txtMaxValue").attr("placeholder", "1000");
                $(".units").html("&nbsp;Kms");
                break;
            }
            case "3": {
                $("#txtMinValue").attr("placeholder", "25");
                $("#txtMaxValue").attr("placeholder", "50");
                $(".units").html("&nbsp;Years");
                break;
            }
            case "4": {
                $("#txtMinValue").attr("placeholder", "4.0");
                $("#txtMaxValue").attr("placeholder", "7.11");
                $(".units").html("&nbsp;Feets &amp; Inches");
                break;
            }
        }
    });
});

function OrderderedOptionViewModel(_opt, _rank) {
    var self = this;
    self.OptionText = ko.observable(_opt);
    self.OptionRank = ko.observable(_rank);
}

function OptionViewModel(_opt) {
    var self = this;
    self.OptionText = ko.observable(_opt);
}

function OrderderedOptionListViewModel(_list) {
    var self = this;
    self.AllOrderedOptions = ko.observableArray();
    for (var i = 0; i < _list.length; i++) {
        self.AllOrderedOptions.push(new OrderderedOptionViewModel(_list[i]));
    }

    AddBlank = function () {
        var _newopt = "";
        self.AllOptions.push(new OrderderedOptionViewModel(_newopt));

        if ($("#divOptionsSection_2").hasClass("displaynone")) {
            $("#divOptionsSection_2").removeClass("displaynone");
            $("#txtPasteOptions_2").hide();
        }
    };
}

function OptionsListViewModel(_list) {
    var self = this;
    self.AllOptions = ko.observableArray();
    for (var i = 0; i < _list.length; i++) {
        self.AllOptions.push(new OptionViewModel(_list[i]));
    }
    AddOption = function (_newopt) {
        if (_newopt != "") {
            self.AllOptions.push(new OptionViewModel(_newopt));
        }
    };

    GetItems = function () {
        var _AllItems = new Array();
        for (var i = 0; i < self.AllOptions().length; i++) {
            _AllItems.push(self.AllOptions()[i]);
        }

        return _AllItems;
    };

    AddBlank = function () {
        var _newopt = "";
        self.AllOptions.push(new OptionViewModel(_newopt));

        if ($("#divOptionsSection").hasClass("displaynone")) {
            $("#divOptionsSection").removeClass("displaynone");
            $("#txtPasteOptions").hide();
        }
    };

    DeleteFirst = function () {
        self.AllOptions.remove(self.AllOptions()[0]);
    };

    DeleteLastItem = function () {
        self.AllOptions.remove(self.AllOptions()[self.AllOptions().length-1]);
    };

    DeleteAllOptions = function () {
        var _total = 0;
        _total = self.AllOptions().length;
        for (var i = 0; i < _total; i++) {
            self.AllOptions.remove(self.AllOptions()[0]);
        }

        if (self.AllOptions().length == 0) {
            $("#txtPasteOptions").show();
            if ($("#divOptionsSection").hasClass("displaynone") == false) {
                $("#divOptionsSection").addClass("displaynone");
            }
            $("#txtPasteOptions").val('');
        }
    };

    MoveThisUp = function (_obj) {
        return;
    };

    DeleteThis = function (_obj) {
        var _pos = -1;
        for (var i = 0; i < self.AllOptions().length; i++) {
            if (self.AllOptions()[i].OptionText() == _obj.OptionText()) {
                _pos = i;
                break;
            }
        }
        self.AllOptions.remove(self.AllOptions()[_pos]);
        if (self.AllOptions().length == 0) {
            $("#txtPasteOptions").show();
            if ($("#divOptionsSection").hasClass("displaynone") == false) {
                $("#divOptionsSection").addClass("displaynone");
            }
            $("#txtPasteOptions").val('');
        }
    };
}


$(document).ready(function () {
    $.postDATA("/Secured/Criteria/Service.ashx?f=G", "", function (msg) {
        ko.applyBindings(new VMCriteriaList(msg), document.getElementById("divPreviews"));
    });
});

function VMCriteria(_cr) {

    var self = this;
    self._id = ko.observable(_cr._id);
    self.RefID = ko.observable(_cr.RefID);
    self.CriteriaName = ko.observable(_cr.CriteriaName);
    self.OptionQuestion = ko.observable(_cr.OptionQuestion);
    self.PreferenceQuestion = ko.observable(_cr.PreferenceQuestion);
    self.CriteriaType = ko.observable(_cr.CriteriaType);

    //
    self.UserOptions = ko.observableArray();
    if (_cr.UserOptions != null && _cr.UserOptions.length > 0) {
        for (var i = 0; i < _cr.UserOptions.length; i++) {
            self.UserOptions.push(new VMOption(_cr.UserOptions[i]));
        }
    }

    self.PreferenceOptions = ko.observableArray();
    if (_cr.PreferenceOptions != null && _cr.PreferenceOptions.length > 0) {
        for (var i = 0; i < _cr.PreferenceOptions.length; i++) {
            self.PreferenceOptions.push(new VMOption(_cr.PreferenceOptions[i]));
        }
    }

    self.MismatchText = ko.observable(_cr.MismatchText);
    self.ShowMatch = ko.observable(_cr.ShowMatch);
    self.IncludeAllInPreference = ko.observable(_cr.IncludeAllInPreference);
    self.IncludeAllInPreferenceText = ko.observable(_cr.IncludeAllInPreferenceText);
}

function VMOption(_opt) {
    var self = this;
    self.OptionText = ko.observable(_opt.OptionText);
}

function VMCriteriaList(_list) {
    var self = this;
    self.AllCriterias = ko.observableArray();

    for (var i = 0; i < _list.length; i++) {
        self.AllCriterias.push(new VMCriteria(_list[i]));
    }

    SetCriteriaForEdit = function (_c) {
        $("#txtCriteriaName").val(_c.CriteriaName());

        $("#cboCriteriaType").val(_c.CriteriaType());
        $("#cboCriteriaType").trigger("change");

        $("#txtCriteriaPreferenceQuestion").val(_c.PreferenceQuestion());

        DeleteAllOptions();
        for (var i = 0; i < _c.UserOptions().length; i++) {
            AddOption(_c.UserOptions()[i].OptionText());
        }
        $("#btnAddMore").trigger("click");
        DeleteLastItem();

        
        $("#chkIncludePreferAll").prop("checked", _c.IncludeAllInPreference());
        $("#chkIncludePreferAll").trigger("change");
        $("#txtIncludeAllText").val(_c.IncludeAllInPreferenceText());

        $("#txtMismatchText").val(_c.MismatchText());

        $("#chkShowMatch").prop("checked", _c.ShowMatch());
        $("#hdnCriteriaID").val(_c.RefID());

        $("#btnCreate").val("Edit Criteria");
        
    };

    AddNewCriteria = function (_c) {
        self.AllCriterias.push(new VMCriteria(_c));
    }
}