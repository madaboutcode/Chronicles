<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Chronicles.Web.ViewModels.PostSummary>" %>
<% 
    DateTime pubDate = Model.PublishedDate;
    
    string postLink = Url.Action(MVC.Posts.ViewPost(pubDate.Year, pubDate.Month, pubDate.Day, Model.Id, Html.GetTextForUrl(Model.Title))); 
%>
<div class="entry">
    <span class="entry-date"><%= Model.PublishedDate.ToString("yyyy-MMM-dd") %></span> 
    <h1><a href="<%=postLink%>" title="Go to post : <%= Model.Title%>"><%= Model.Title %></a></h1>
    <div class="entry-body">
        <%= Model.Summary %>
        <ul class="post-tags">
            <%= Html.DisplayFor(p=>p.Tags) %>
        </ul>
        <div class="comment-count">
        <%if (Model.CommentCount > 0)
          {  %>
        	<a href="<%= postLink + "#comments"%>"><%=Model.CommentCount%> comments</a>
        <%}
          else
          { %>
            <a href="<%= postLink + "#add-comment-form"%>">Add Comment</a>
        <%} %>
        </div>
    </div>
</div>
