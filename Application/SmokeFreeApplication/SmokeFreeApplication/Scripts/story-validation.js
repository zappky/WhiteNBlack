$("#title").blur(function () {
    if ($("#title").val() == "") {
        $("#titleVali").css("display", "block");
    } else {
        $("#titleVali").css("display", "none");
    }
});
$('#body').on('summernote.blur', function () {
    if ($('#body').summernote('isEmpty')) {
        $("#bodyVali").css("display", "block");
    } else {
        $("#bodyVali").css("display", "none");
    }
});
$('#body').on('summernote.keyup', function (e) {
    debugger;
    var text = $(this).next('.note-editor').find('.note-editable').text();
    var length = text.length;
    var num = 1000 - length;

    if (length > 1000) {
        $('#withinRange').hide();
        $("#summernote").summernote("code", text.substring(0, 1000));
    }
    else {
        $('#withinRange').text(1000 - length).show();
    }

});

$('#createForm').on('submit', function (e) {
    var body = $('#body').next('.note-editor').find('.note-editable').text();
    if (body.length < 500 || body.length > 2000 && $("#title").val() == "") {
        $("#titleVali").css("display", "block");
        $("#bodyVali").css("display", "block");
        e.preventDefault();
    } else if (body.length < 500 |||| body.length > 2000 ) {
        $("#titleVali").css("display", "none");
        $("#bodyVali").css("display", "block");
        e.preventDefault();
    } else if ($("#title").val() == "") {
        $("#titleVali").css("display", "block");
        $("#bodyVali").css("display", "none");
        e.preventDefault();
    } return true;
});

$(document).keypress(
    function (event) {
        if (event.which == '13') {
            event.preventDefault();
        }
    });