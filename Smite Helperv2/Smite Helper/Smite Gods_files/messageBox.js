//Required for use with the following web user controls: CreateAccount, Login
$(function() { //jQuery Stuff

	$("#messageBox").keydown(function(e) {//pressing ENTER here is not firing this handler
		if (e.keyCode == $.ui.keyCode.ENTER) {
			$(this).dialog("close");
		}
	});

	$("#messageBox").dialog({
		autoOpen: false,
		height: 200,
		width: 300,
		modal: true,
		hide: "slide",
		buttons: {
			"Close": function() {//Button text "Close" must match button disabling/enabling text elsewhere.
				$(this).dialog("close");
			}
		}
	});

});
