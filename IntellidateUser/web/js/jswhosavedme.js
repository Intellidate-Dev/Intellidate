

$(document).ready(function () {
    debugger;
    var count = $(HDN_COUNT).val();
    count = count.replace("'", "");
    count = count.replace("'", "");
    $(HDN_COUNTSAVED).html(count);
    $('.timeago').timeago();
});



