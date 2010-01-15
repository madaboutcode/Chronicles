<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CommentDetails>" %>
<div class="comment-box" id="comment-<%=Model.Id %>">
    <img src="<%= Html.Gravatar(Model.UserEmail) %>" />
    
    <%if (!string.IsNullOrEmpty(Model.UserWebSite))
      { %>
        <a href="<%= Model.UserWebSite %>"><span class="user-info-name"><%=Html.Encode(Model.UserName)%></span></a> 
    <%}
      else
      { %>
        <span class="user-info-name"><%=Html.Encode(Model.UserName)%></span>
    <%} %>
    <span class="user-info-date"><%= Model.Date.ToString("MMM dd \\\'yy hh:mm tt (EST)") %></span>
    <div class="comment-text">
        <%= Html.AsciiToHtml(Model.Text) %>
    </div>
    <% if (this.IsAdmin())
    {%>
        <a href="#delete-<%= Model.Id %>" class="deletebutton">Delete</a> 
    <%} %>
</div>

