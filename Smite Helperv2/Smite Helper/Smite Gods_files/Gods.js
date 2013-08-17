var viewFlag = "";
var previousTab = "";
var godOld = "";
var view;
var sGod = ""
var hash = "";
var godArray = [];

$(document).ready(function () {
    sGod = GetQueryStringParams("god");
    var Array = $('.god').toArray();
    for (var i = 0; i < Array.length; i++) {
        godArray.push(Array[i].innerHTML.toLowerCase());
    }

    if (sGod != undefined) {
        sGod = sGod.toLowerCase();
        if ($.inArray(sGod, godArray) != -1) {
            godSelect(sGod, 'Overview');
            location.hash = "#" + sGod;
        } else {
            godSelect('agni', 'Overview');
        }
    } else {
        hash = GetUrlHash();

        if (hash != undefined) {
            hash = hash.toLowerCase();
            if (hash == "free") {
                var freeGod = "";
                $('.godList').each(function () {
                    if ($(this).find('#lblFree').html().toLowerCase().indexOf("notify") < 0) {
                        $(this).hide();
                    }
                    else {
                        freeGod = $(this).find('#lblGodShort').html().toLowerCase();
                    }
                });
                godSelect(freeGod, 'Overview');
                $('select[name=ddTag]').val('notify');

            } else {
                if ($.inArray(hash, godArray) != -1) {
                    godSelect(hash, 'Overview');
                } else {
                    godSelect('agni', 'Overview');
                }
            }
        } else {
            godSelect('agni', 'Overview');
        }
    }
    var propertyArray = [];
    var pantheonArray = $('.pantheon').toArray();
    for (var i = 0; i < pantheonArray.length; i++) {
        var pantheon = pantheonArray[i].innerHTML.toLowerCase();
        if (pantheon != "") {
            if ($.inArray(pantheon, propertyArray) != -1) {
                continue;
            } else {
                propertyArray.push(pantheon);
            }
        }
    }
    GodFilter = document.getElementById('ddTag');
    var optGroup = document.createElement('optgroup');
    optGroup.setAttribute("label", labelName);
    for (var i = 0; i < propertyArray.length; i++) {
        var opt = document.createElement('option');
        opt.innerHTML = propertyArray[i].toString();
        opt.value = propertyArray[i].toString();
        optGroup.appendChild(opt);
    }
    GodFilter.appendChild(optGroup);


    propertyArray = [];
    var roleArray = $('.roles').toArray();
    for (var i = 0; i < roleArray.length; i++) {
        var role = roleArray[i].innerHTML.toLowerCase();
        if (role != "") {
            if ($.inArray(role, propertyArray) != -1) {
                continue;
            } else {
                propertyArray.push(role);
            }
        }
    }
    GodFilter = document.getElementById('ddTag');
    optGroup = document.createElement('optgroup');
    //optGroup.setAttribute("label", "<%=HirezStudios.Web.Resource.GetString("S000-08") %>")
    for (var i = 0; i < propertyArray.length; i++) {
        var opt = document.createElement('option');
        opt.innerHTML = propertyArray[i].toString();
        opt.value = propertyArray[i].toString();
        optGroup.appendChild(opt);
    }
    GodFilter.appendChild(optGroup);

});
function godSelect(god, viewFlag,message) {
    if (god.indexOf("-") != -1) {
        god.replace("-", "'");
    }
    var godNavNew = $("#" + god);
    var godNavOld = $("#" + godOld);
    view = $("#" + god + "View");
    var viewOld = $("#" + godOld + "View");
    var ability = $("#" + godOld + "Ability");

    if (viewFlag == "Ability") {
        $('#viewHeader').html(message);
        $('input[name=Abilities]').addClass("ui-state-disabled");
        $('input[name=Overview]').removeClass("ui-state-disabled");
        view = $("#" + god + "Ability");
        previousTab = god + "Ability";
    }
    if (viewFlag == "Overview") {
      $('#viewHeader').html(message);
        $('input[name=Overview]').addClass("ui-state-disabled");
        $('input[name=Abilities]').removeClass("ui-state-disabled");
        view = $("#" + god + "View");
        previousTab = god + "View";
    } if (viewFlag == "") {
        if (previousTab.indexOf("Ability") !== -1) {
            view = $("#" + god + "Ability");
            $('input[name=Abilities]').addClass("ui-state-disabled");
            $('input[name=Overview]').removeClass("ui-state-disabled");
            $('#viewHeader').html(message);
        } else {
            previousTab = god + "View";
        }
    }
    if (godNavOld != undefined) {
        if (god != godOld) {
            if (godNavOld.hasClass("activeView")) {
                godNavOld.removeClass("activeView");
                godNavOld.addClass("inactiveView");
            } else
                if (godNavOld.hasClass("activeGod")) {
                    godNavOld.removeClass("activeGod");
                    godNavOld.addClass("inactiveGod");
                }
            godNavNew.addClass("activeGod");

            viewOld.addClass("inactiveView");
            viewOld.removeClass("viewContainer");
        }
        /*Hide Previous View*/
        viewOld.removeClass("viewContainer");
        viewOld.addClass("inactiveView");

        /*Hide Previous Ability*/
        ability.removeClass("viewContainer");
        ability.addClass("inactiveView");

        /*Reveal Current View*/
        view.removeClass("inactiveView");
        view.addClass("viewContainer");
    }
    godOld = god;
}

function viewSelect(newView,message) {
    var viewNav = $("#" + newView);
    viewFlag = "Ability";

    if (newView != view.attr("id")) {
        if (view != undefined) {
            viewNav.removeClass("inactiveView");
            viewNav.addClass("viewContainer");

            view.removeClass("viewContainer");
            view.addClass("inactiveView");

            $('input[name=Abilities]').addClass("ui-state-disabled");
            $('input[name=Overview]').removeClass("ui-state-disabled");
        }
        $('#viewHeader').html(message);
        view = viewNav;
        previousTab = newView;
    }
}