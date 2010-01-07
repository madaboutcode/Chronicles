<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<Chronicles.Web.ViewModels.PostDetails>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%= Model.Title %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%= Html.DisplayForModel()%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MetaContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server">

    <script src="<%= Links.scripts.jquery_validate_min_js %>" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(function() {
            $('#add-comment-form').validate({
                errorPlacement: function(error, element) {
                    error.addClass('validation-message');
                    $(element).after(error);
                    
                    var pos = '-' + ($(element).height()+30)+'px';
                    error.css({ top: pos });
                }
            });
        });
    </script>
</asp:Content>
