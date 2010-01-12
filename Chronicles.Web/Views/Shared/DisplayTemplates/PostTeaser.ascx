<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PostTeaser>" %>
<% 
    DateTime pubDate = Model.PublishedDate;
    
    string postLink = Url.Action(MVC.Posts.ViewPost(pubDate.Year, pubDate.Month, pubDate.Day, Model.Id, Html.GetTextForUrl(Model.Title))); 
%>
<li><a href="<%= postLink %>"><%= Model.Title %></a></li>