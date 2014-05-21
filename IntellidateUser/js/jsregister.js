var _unameChanged = true;
$(document).ready(function () {
    $(REG_USERNAMETXT).bind(EVNT_CHANGE, function () {
        _unameChanged = true;
    });
});
$(document).ready(function () {
    $(REG_USERNAMETXT).bind(EVNT_BLUR, function () {
        if (_unameChanged == false) {
            return;
        }
        _unameChanged = false;
        var _unm = $(this).val().trim();
        if (_unm == NULLSTRNG) {
            // Show enter username error message
            ShowError(REG_ERR_BLNKUNAME, REG_USERERRORDIV, REG_USERERRORFLD);
            SetWrong(REG_USERNAMERESULT);
        }
        else {
            // Check if the User ID already exists
            
            SetPending(REG_USERNAMERESULT);
            var _unmobj = new Object();
            _unmobj.value = _unm;
            _unmobj.type = "uname";
            $.postDATA(APICALLUNAMECHECK, _unmobj, function (ret) {
                $(REG_USERNAMERESULT).empty();
                if (ret) {
                    ShowError(REG_ERR_EXISTUNAME, REG_USERERRORDIV, REG_USERERRORFLD);
                    SetWrong(REG_USERNAMERESULT);
                }
                else {
                    HideError(REG_USERERRORDIV);
                    SetRight(REG_USERNAMERESULT);
                    _unameCheck = true;
                    CheckNextEnable();
                }

            }, function () { })
        }
    });
});
$(document).ready(function () {
    $(REG_EMAILTXT).bind(EVNT_BLUR, function () {
        var _eml = $(this).val().trim();
        if (_eml == NULLSTRNG) {
            ShowError(REG_ERR_BLNKEMAIL, REG_EMAILERRORDIV, REG_EMAILERRORFLD);
            SetWrong(REG_EMAILRESULT);
            return;
        }

        if (!EMAILREGEX.test(_eml)) {
            
            ShowError(REG_ERR_INVEMAIL, REG_EMAILERRORDIV, REG_EMAILERRORFLD);
            SetWrong(REG_EMAILRESULT);
            return;
        }

        // Check if the email already exists
        var _emlobj = new Object();
        _emlobj.value = _eml;
        _emlobj.type = "email";
        SetPending(REG_EMAILRESULT);
        $.postDATA(APICALLEMAILCHECK, _emlobj, function (ret) {
            $(REG_EMAILRESULT).empty();
            if (ret) {
                ShowError(REG_ERR_EXISTEMAIL, REG_EMAILERRORDIV, REG_EMAILERRORFLD);
                SetWrong(REG_EMAILRESULT);
            }
            else {
                
                HideError(REG_EMAILERRORDIV);
                SetRight(REG_EMAILRESULT);
                _emailCheck = true;
                CheckNextEnable();
            }

        }, function () { })

    });
});

$(document).ready(function () {
    $(REG_RETYPEEMAILTXT).bind(EVNT_BLUR, function () {
        var _email = $(REG_EMAILTXT).val();
        var _retypeemail = $(REG_RETYPEEMAILTXT).val();

        if (_retypeemail == NULLSTRNG) {
            ShowError(REG_ERR_EMAILNOMATCH, REG_RETYPEEMAILERRORDIV, REG_RETYPEEMAILERRORFLD);
            SetWrong(REG_RETYPEEMAILRESULT);
            return;
        }

        if (_email != _retypeemail) {
            ShowError(REG_ERR_EMAILNOMATCH, REG_RETYPEEMAILERRORDIV, REG_RETYPEEMAILERRORFLD);
            SetWrong(REG_RETYPEEMAILRESULT);
        }
        else {
            HideError(REG_RETYPEEMAILERRORDIV);
            SetRight(REG_RETYPEEMAILRESULT);
            _remailCheck = true;
            CheckNextEnable();
        }
    });
});

$(document).ready(function () {
    $(REG_PASSWORDTXT).bind(EVNT_BLUR, function () {
        var _password = $(this).val();
        if (_password == NULLSTRNG) {
            ShowError(REG_ERR_BLANKPASSWORD, REG_PASSWORDERRORDIV, REG_PASSWORDERRORFLD);
            SetWrong(REG_PASSWORDRESULT);
            return;
        }

        if (_password.length < 8) {
            ShowError(REG_ERR_SMALLPASSWORD, REG_PASSWORDERRORDIV, REG_PASSWORDERRORFLD);
            SetWrong(REG_PASSWORDRESULT);
            return;
        }
        else {
            HideError(REG_PASSWORDERRORDIV);
            SetRight(REG_PASSWORDRESULT);
            _passwordCheck = true;
            CheckNextEnable();
        }
    });
});

$(document).ready(function () {
    $(REG_RPASSWORDTXT).bind(EVNT_BLUR, function () {
        var _rpassword = $(this).val();
        var _password = $(REG_PASSWORDTXT).val();

        if (_rpassword == NULLSTRNG) {
            ShowError(REG_ERR_PASSWORDSNOMATCH, REG_RPASSWORDERRORDIV, REG_RPASSWORDERRORFLD);
            SetWrong(REG_RPASSWORDRESULT);
            return;
        }

        if (_rpassword != _password) {
            ShowError(REG_ERR_PASSWORDSNOMATCH, REG_RPASSWORDERRORDIV, REG_RPASSWORDERRORFLD);
            SetWrong(REG_RPASSWORDRESULT);
            return;
        }
        else {
            HideError(REG_RPASSWORDERRORDIV);
            SetRight(REG_RPASSWORDRESULT);
            _rpasswordCheck = true;
            CheckNextEnable();
        }
    });
});

function SetRight(divID) {

    if ($(divID).hasClass(CSSHIDE)) {
        $(divID).removeClass(CSSHIDE);
    }

    if ($(divID).hasClass(CSSGLYPHICON) == false) {
        $(divID).addClass(CSSGLYPHICON);
    }
    
    if ($(divID).hasClass(CSSGLYPHICONREMOVE)) {
        $(divID).removeClass(CSSGLYPHICONREMOVE);
    }

    if ($(divID).hasClass(CSSREDCOLOR)) {
        $(divID).removeClass(CSSREDCOLOR);
    }

    if ($(divID).hasClass(CSSGLYPHICONOK)==false) {
        $(divID).addClass(CSSGLYPHICONOK);
    }

}

function SetWrong(divID) {

    if ($(divID).hasClass(CSSHIDE)) {
        $(divID).removeClass(CSSHIDE);
    }

    if ($(divID).hasClass(CSSGLYPHICON) == false) {
        $(divID).addClass(CSSGLYPHICON);
    }

    if ($(divID).hasClass(CSSGLYPHICONOK)) {
        $(divID).removeClass(CSSGLYPHICONOK);
    }

    if ($(divID).hasClass(CSSREDCOLOR)==false) {
        $(divID).addClass(CSSREDCOLOR);
    }

    if ($(divID).hasClass(CSSGLYPHICONREMOVE) == false) {
        $(divID).addClass(CSSGLYPHICONREMOVE);
    }

}

function SetPending(divID) {

    if ($(divID).hasClass(CSSGLYPHICON)) {
        $(divID).removeClass(CSSGLYPHICON);
    }

    if ($(divID).hasClass(CSSGLYPHICONOK)) {
        $(divID).removeClass(CSSGLYPHICONOK);
    }

    if ($(divID).hasClass(CSSREDCOLOR)) {
        $(divID).removeClass(CSSREDCOLOR);
    }

    if ($(divID).hasClass(CSSGLYPHICONREMOVE)) {
        $(divID).removeClass(CSSGLYPHICONREMOVE);
    }

    $(divID).html(IMGLOADINGIMAGE);

}

function ShowError(errMessage, containerDiv, messageDiv) {
    $(messageDiv).html(errMessage);
    if ($(containerDiv).hasClass(CSSHIDE) == true) {
        $(containerDiv).hide();
        $(containerDiv).removeClass(CSSHIDE);
        $(containerDiv).slideDown(400);
    }
}
function HideError(containerDiv) {
    if ($(containerDiv).hasClass(CSSHIDE) == false) {
        $(containerDiv).slideUp(400);
        setTimeout(function () {
            $(containerDiv).addClass(CSSHIDE);
        }, 400);
    }
}

var _unameCheck = false;
var _emailCheck = false;
var _remailCheck = false;
var _passwordCheck = false;
var _rpasswordCheck = false;
function CheckNextEnable() {
    if (_unameCheck && _emailCheck && _remailCheck && _passwordCheck && _rpasswordCheck) {
        $(REG_NEXTBTN).prop("disabled", false);
    }
    else {
        $(REG_NEXTBTN).prop("disabled", true);
    }
}