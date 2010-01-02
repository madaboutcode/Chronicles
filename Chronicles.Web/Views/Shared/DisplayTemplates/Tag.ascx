<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Chronicles.Entities.Tag>" %>
<li><a href="<%= Url.Action(MVC.Posts.ViewPostsByTag(Model.TagName,1)) %>"><%= Model.TagName %></a></li>