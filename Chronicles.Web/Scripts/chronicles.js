function GetUrl(relUrl)
{
    if(typeof(baseUrl) == 'undefined')
        baseUrl = '/';
    
    return baseUrl + relUrl;
}

function ShowQuickLinks() {
    var pw = $('#quicklinks').parent().width();
    var w = $('#quicklinks').width();
    $('#quicklinks').css({ left: (pw - w) / 2 + 'px'});
}

function SearchBoxWatermark() {
    $('.watermark-on').focus(function() {
        $(this).filter(function() {
            return $(this).val() == "" || $(this).val() == "search"

        }).removeClass("watermark-on").val("");
    });

    $('.watermark-on').blur(function() {
        $(this).filter(function() {
            
            return $(this).val() == ""

        }).addClass("watermark-on").val("search");
    });
}

function AttachSearchBoxSubmitEventHandler() {
    $('#searchbox').submit(function(e) {
        alert('Sorry! not yet implemented..');
        e.preventDefault();
    });
}

$(function() {
    ShowQuickLinks();
    SearchBoxWatermark();
    AttachSearchBoxSubmitEventHandler();
});

function AjaxPost(url, data, callback)
{
    $.post(GetUrl(url), data, callback,'json');
}