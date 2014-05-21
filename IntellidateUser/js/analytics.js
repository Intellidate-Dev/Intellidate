$(document).ready(function () {
    saveAnalytics();
});

function saveAnalytics() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(saveDetails);
    }
}
function saveDetails(position) {
    // Crete object
    var _analytics = new Object();
    _analytics.Latitude = position.coords.latitude;
    _analytics.Longitude = position.coords.longitude;
    _analytics.PageName = "Registration";
    //$.postDATA("http://localhost:55555/api/Grab/", _analytics, function () { }, function () { });

}