
function AttachCommentActionHandlers()
{
    $('.deletebutton')
        .click(DeleteComment)
        .parent('div.comment-box')
            .hover(
                function(){
                    $(this).addClass('highlight');
                }, 
                function(){
                    $(this).removeClass('highlight');
                }
            );
}

function DeleteComment(event)
{
    debugger;
    var href = $(this).attr('href');
    obj = this;
    if(href != null && href.length > 0)
    {
        var id = href.split('-')[1];
        AjaxPost(
            'Posts/DeleteComment'
            , {commentId : id}
            , function(data){
                $(obj).parent('div.comment-box').prev('.hline').hide(1000);
                $(obj).parent('div.comment-box').hide(1000);
            }
        );
    }
    event.preventDefault();
}

$(AttachCommentActionHandlers);