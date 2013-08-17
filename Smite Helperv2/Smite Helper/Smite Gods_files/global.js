function GetQueryStringParams(string) {
    var sPageUrl = window.location.search.substring(1);
    var sUrlVariables = sPageUrl.split('&');
    for (var i = 0; i < sUrlVariables.length; i++) {
        var sParameterName = sUrlVariables[i].split('=');
        if (sParameterName[0] == string) {
            return sParameterName[1];

        }
    }
}

function GetUrlHash() {
    var sPageUrl = window.location.href;
    var sUrlVariables = sPageUrl.split('#');
    for (var i = 1; i < sUrlVariables.length; i++) {
        return sUrlVariables[i];
    }
}