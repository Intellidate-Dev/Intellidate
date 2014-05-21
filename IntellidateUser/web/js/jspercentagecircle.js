
    $(document).ready(function () {
        var _elem = document.getElementById(DIVRANKNAME).innerHTML;
        _elem = _elem.replace(ELEM1, ELEM2);

        var _div = document.getElementById(DIVREPRESENT);
        _div.className = CLASSNAME + _elem + ELEM2;
    });
