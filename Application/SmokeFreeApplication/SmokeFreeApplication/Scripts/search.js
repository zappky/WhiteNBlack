

$(function () {
    $('#nameRad').click(function () {
        if ($(this).is(':checked')) {
            $('#searchBar').tagsinput('removeALL');
            $('#searchBar').tagsinput('destroy');
            $('#searchBar').removeAttr("data-role");
        }
    });
});

$(function () {
    $('#tagRad').click(function () {
        if ($(this).is(':checked')) {
            $('#searchBar').attr('data-role', 'tagsinput');
            $("#searchBar").tagsinput("refresh");

        }
    });
});
function load() 
    if ($("#nameRad").is(':checked')) {
        $('#searchBar').tagsinput('removeALL');
        $('#searchBar').tagsinput('destroy');
        $('#searchBar').removeAttr("data-role");
    }
    if ($("#tagRad").is(':checked')) {
        $('#searchBar').attr('data-role', 'tagsinput');
        $("#searchBar").tagsinput("refresh");
        $(".bootstrap-tagsinput").add();

    }
   
}



window.onload = load;