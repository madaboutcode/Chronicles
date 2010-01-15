<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Chronicles.Web.ViewModels.PostSummary>" %>
<% 
    DateTime pubDate = Model.PublishedDate;
    
    string postLink = Url.Action(MVC.Posts.ViewPost(pubDate.Year, pubDate.Month, pubDate.Day, Model.Id, Html.GetTextForUrl(Model.Title))); 
%>
<div class="entry">
    <h1><a href="<%=postLink%>" title="Go to post : <%= Model.Title%>"><%= Model.Title %></a></h1>
    <span class="entry-date">Posted on <%= Model.PublishedDate.ToString("MMMM dd, yyyy hh:mm tt") %></span> 
    <div class="entry-body">
        <%= Model.Summary %>
        <ul class="post-tags">
            <%= Html.DisplayFor(p=>p.Tags) %>
        </ul>
        <div class="comment-count">
        <%if (Model.CommentCount > 0)
          {  %>
        	<a href="<%= postLink + "#comments"%>"><%=Model.CommentCount%> 
        	
        	<%if (Model.CommentCount > 1)
           {%>
        	    comments
        	<%}
           else
            {
            %>
                comment      	    
        	<%}%>    
        	</a>
        <%}
          else
          { %>
            <a href="<%= postLink + "#add-comment-form"%>">Add Comment</a>
        <%} %>
        </div>
    </div>
</div>
