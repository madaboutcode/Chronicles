<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Chronicles.Web.ViewModels.PostDetails>" %>
<% 
    DateTime pubDate = Model.PublishedDate;

    string postLink = Url.Action(MVC.Posts.ViewPost(pubDate.Year, pubDate.Month, pubDate.Day, Model.Id, Html.GetTextForUrl(Model.Title))); 
%>
<div class="entry">
    <h1>
        <a href="<%=postLink%>" title="Go to post : <%= Model.Title%>">
            <%= Model.Title %></a></h1>
        <span class="entry-date">Posted on <%= Model.PublishedDate.ToString("MMMM dd, yyyy hh:mm tt") %></span>         
    <div class="entry-body">
        <%= Model.Body %>
        <ul class="post-tags">
            <%= Html.DisplayFor(p=>p.Tags) %>
        </ul>
        <% if (Model.Comments != null && Model.Comments.Length > 0)
           { %>
        <div class="comment-count">
            <%= Model.Comments.Length %>
        	<%if (Model.Comments.Length > 1)
           {%>
        	    comments
        	<%}
           else
            {
            %>
                comment      	    
        	<%}%>    
        </div>
        <% } %>
    </div>
</div>