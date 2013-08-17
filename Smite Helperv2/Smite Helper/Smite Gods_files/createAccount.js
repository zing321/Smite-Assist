function createAccount() {
	var dlg = $("#createAccountDialog");
	dlg.dialog("open");
	dlg.focus();
}

$(function() { //jQuery Stuff

	function processCreateAccount() {
		var usr = $("#frmCreateAccount #username").val();
		var pwd = $("#frmCreateAccount #password").val();
		var pwc = $("#frmCreateAccount #passwordConfirm").val();
		var email = encodeURIComponent($("#frmCreateAccount #email").val());

		var news = $("#frmCreateAccount #newsletter").attr('checked') == 'checked' ? '1' : '0';
		var teen = $("#frmCreateAccount #teen").attr('checked') == 'checked' ? '1' : '0';

		//alert('usr1: ' + usr + '\npwd: ' + pwd + '\nconf:' + pwc + '\nemail:' + email + '\nnews:' + (news ? '1' : '0') + '\nteen:' + teen);

		var mb = $("#messageBox");
		mb.attr("title", mbCreateTitle);

		$("#message").html(mbCreateMessage);
		$("#waitBar").show();
		$(":button:contains('Close')").attr("disabled", "disabled").addClass("ui-state-disabled");
		mb.dialog("open");

		var qs = window.location.search.substring(1); //pass along querystring for utm and other parms

		$.ajax({
			type: "POST",
			url: responseUrl,
			data: "username=" + usr + "&password=" + pwd + "&passwordConfirm=" + pwc + "&email=" + email + "&newsletter=" + news + "&teen=" + teen + "&" + qs,
			dataType: "text",
			success: function(msg) {
				$("#waitBar").hide();
				var ret = msg.split('|');
				var ok = ret[0];
				var errmsg = ret[1];
				if (ok == '0') {
					//mb.dialog("close");
					document.location = landingUrl;
				} else {
					$("#message").html(errmsg);
					$(":button:contains('Close')").removeAttr("disabled").removeClass("ui-state-disabled");
				}
			},
			error: function() {//
			}
		});
	}

	$("#createAccountDialog").keydown(function(e) {
		if (e.keyCode == $.ui.keyCode.ENTER) {
			processCreateAccount();
		}
	});

	$("#createAccountDialog").dialog({
		autoOpen: false,
		height: 350,
		width: 400,
		modal: true,
		open: "slide",
		position: {
		    my: "left top",
		    at: "center-200 top+200",
		    of: window
		},
		buttons: {
			"": {
				className: "testclass",
				text: btnCreateAccount,
				click: function(event) {
					event.preventDefault(); //prevent normal postback

					processCreateAccount();
				}
			}
		},
		close: function() {
			allFields.val("").removeClass("ui-state-error");
		}
	});
});
