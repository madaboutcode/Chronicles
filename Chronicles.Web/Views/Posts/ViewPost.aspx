<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<Chronicles.Web.ViewModels.PostDetails>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%= Model.Title %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%= Html.DisplayForModel()%>

<%if(Model.Comments !=null && Model.Comments.Length > 2) { %>
<a class="button float-right" href="#add-comment-form">Add Comment </a>
<%} %>

<% if(Model.Comments.Length > 0) { %>
    <div id="comments" class="section">
        <h2>
            Comments</h2>
    <% for (int i = 0; i < Model.Comments.Length; i++)
       { 
            var item = Model.Comments[i];      
    %>
            <%if(i>0) {%>
                <div class="hline"></div>
            <%} %>
            <%= Html.DisplayFor(x=>item) %> 
    <%} %>
    </div>
<% } %>

<%
    string name = Model.EditedComment != null && !string.IsNullOrEmpty(Model.EditedComment.UserName) ? Model.EditedComment.UserName : string.Empty;
    string email = Model.EditedComment != null && !string.IsNullOrEmpty(Model.EditedComment.UserEmail) ? Model.EditedComment.UserEmail : string.Empty;
    string website = Model.EditedComment != null && !string.IsNullOrEmpty(Model.EditedComment.UserWebSite) ? Model.EditedComment.UserWebSite : string.Empty;
    string  text = Model.EditedComment != null && !string.IsNullOrEmpty(Model.EditedComment.Text) ? Model.EditedComment.Text : string.Empty;
%>

<%= Html.ValidationSummary() %>

<%using (Html.BeginForm(MVC.Posts.AddComment(), FormMethod.Post, new { @class = "section dataform", id = "add-comment-form" }))
  { %>
<h2>
    Add Comment</h2>
<fieldset class="comment-personal-info">
    <%= Html.Hidden("PostId", Model.Id) %>
    <p>
        Name<span class="required">*</span></p>
    <input type="text" id="UserName" name="UserName" class="required comment-name" value="<%=name %>" />
    
    <p>
        Email Id<span class="required">*</span></p>
    <input type="text" name="UserEmail" id="UserEmail" class="required email comment-email" value="<%=email %>" />
    <p>
        Homepage</p>
    <input type="text" name="UserWebSite" id="WebAddress" class="comment-web url" value="<%=website %>"/>
</fieldset>
<fieldset class="comment-textarea">
    <p>
        Comment<span class="required">*</span></p>
    <textarea name="Text" cols="60" rows="10" id="comment-text" class="required"><%=text %></textarea>
</fieldset>
<fieldset class="comment-submitbutton">
    <button name="postcomment" type="submit" class="button">
        Post</button>
</fieldset>
<% } %>

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
