$(function () { //jQuery Stuff
    function processLogin() {
        var usr = $("#frmLogin #username").val();
        var pwd = $("#frmLogin #password").val();

        //alert('usr1: ' + usr + '\npwd: ' + pwd + '\nver: 3');

        var mb = $("#messageBox");
        mb.attr("title", mbLoginTitle);

        $("#message").html(mbLoginMessage);
        $("#waitBar").show();
        $(":button:contains('Close')").attr("disabled", "disabled").addClass("ui-state-disabled");
        mb.dialog("open");

        var qs = window.location.search.substring(1); //pass along querystring for utm and other parms

        $.ajax({
            type: "POST",
            url: "../response/login-response.aspx",
            data: "txtUsername=" + usr + "&txtPassword=" + pwd + "&" + qs,
            dataType: "text",
            success: function (msg) {
                $("#waitBar").hide();
                var ret = msg.split('|');
                var ok = ret[0];
                var errmsg = ret[1];
                if (ok == '0') {
                    //mb.dialog("close");
                    document.location = "../account/accountInfo.aspx";
                } else {
                    $("#message").html(errmsg);
                    $(":button:contains('Close')").removeAttr("disabled").removeClass("ui-state-disabled");
                }
            },
            error: function () {//
            }
        });
    }

    $("#loginDialog").keydown(function (e) {
        if (e.keyCode == $.ui.keyCode.ENTER) {
            processLogin();
        }
    });

    $("#loginDialog").dialog({
        autoOpen: false,
        height: 300,
        width: 400,
        modal: true,
        open: "slide",
        buttons: {
            "": {
                className: "testclass",
                text: btnLogin, //localized in login.ascx.cs
                click: function (event) {
                    event.preventDefault(); //prevent normal postback

                    processLogin();
                }
            }
        },
        close: function () {
            allFields.val("").removeClass("ui-state-error");
        }
    });

});

