$(document).ready(function () {
        var _elem = document.getElementById(DIVSMALLRANKNAME).innerHTML;
        _elem = _elem.replace(ELEM1, ELEM2);

        var _div = document.getElementById(DIVSMALLREPRESENT);
        _div.className = SMALLCLASSNAME + _elem + ELEM2;
});
function GetSelectValues(select) {

    var _result = null;
    var _options = select && select.options;
    var _opt;

    for (var i = 0, iLen = _options.length; i < iLen; i++) {
        _opt = _options[i];

        if (_opt.selected) {
            _result = _result + "," + _opt.text;
        }
    }

    return _result;
}