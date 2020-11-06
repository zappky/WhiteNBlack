$("#title").blur(function () {
    if ($("#title").val() == "") {
        $("#titleVali").css("display", "block");
    } else {
        $("#titleVali").css("display", "none");
    }
});
$("#body").on('summernote.blur', function () {
    if ($("#body").summernote('isEmpty')) {
        $("#bodyVali").css("display", "block");
    } else {
        $("#bodyVali").css("display", "none");
    }
});
$('#body').on('summernote.keyup', function (e) {
    debugger;
    var text = $(this).next('.note-editor').find('.note-editable').text();
    var length = text.length;
    var num = 5000 - length;

    if (length > 5000) {
        $('#withinRange').text(length - 5000).show();
        $('#withinRange').css("color","red");
        $("#summernote").summernote("code", text.substring(0, 5000));
    }
    else {
        $('#withinRange').text(5000 - length).show();
    }

});

$('#createForm').on('submit', function (e) {
    var body = $('#body').next('.note-editor').find('.note-editable').text();
    if (body.length < 500 && $("#title").val() == "" && $("#ImageFile").val() == "") {
        $("#titleVali").css("display", "block");
        $("#bodyVali").css("display", "block");
        $("#imgVali").css("display", "block");
        e.preventDefault();
    } else if (body.length < 500) {
        $("#titleVali").css("display", "none");
        $("#imgVali").css("display", "none");
        $("#bodyVali").css("display", "block");
        e.preventDefault();
    } else if ($("#title").val() == "") {
        $("#titleVali").css("display", "block");
        $("#bodyVali").css("display", "none");
        $("#imgVali").css("display", "none");
        e.preventDefault();
    } else if ($("#ImageFile").val()=="") {
        $("#titleVali").css("display", "none");
        $("#bodyVali").css("display", "none");
        $("#imgVali").css("display", "block");
    } return true;
});

$(document).keypress(
    function (event) {
        if (event.which == '13') {
            event.preventDefault();
        }
    });