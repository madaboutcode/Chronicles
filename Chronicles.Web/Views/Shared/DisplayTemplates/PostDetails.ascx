<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Chronicles.Web.ViewModels.PostDetails>" %>
<% 
    DateTime pubDate = Model.PublishedDate;

    string postLink = Url.Action(MVC.Posts.ViewPost(pubDate.Year, pubDate.Month, pubDate.Day, Model.Id, Html.GetTextForUrl(Model.Title))); 
%>
<div class="entry">
    <span class="entry-date">
        <%= Model.PublishedDate.ToString("yyyy-MMM-dd") %></span>
    <h1>
        <a href="<%=postLink%>" title="Go to post : <%= Model.Title%>">
            <%= Model.Title %></a></h1>
    <div class="entry-body">
        <%= Model.Body %>
        <ul class="post-tags">
            <%= Html.DisplayFor(p=>p.Tags) %>
        </ul>
        <% if (Model.Comments != null && Model.Comments.Length > 0)
           { %>
        <div class="comment-count">
            <%=Model.Comments.Length%>
            comments
        </div>
        <% } %>
    </div>
</div>

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

<%using (Html.BeginForm(MVC.Posts.AddComment(), FormMethod.Post, new { @class = "section", id = "add-comment-form" }))
  { %>
<h2>
    Add Comment</h2>
<fieldset class="comment-personal-info">
    <%= Html.Hidden("PostId", Model.Id) %>
    <p>
        Name<span class="required">*</span></p>
    <input type="text" Id="UserName" name="UserName" class="required comment-name" value="<%=name %>" />
    
    <p>
        Email Id<span class="required">*</span></p>
    <input type="text" name="UserEmail" Id="UserEmail" class="required email comment-email" value="<%=email %>" />
    <p>
        Homepage</p>
    <input type="text" name="UserWebSite" Id="WebAddress" class="comment-web url" value="<%=website %>"/>
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