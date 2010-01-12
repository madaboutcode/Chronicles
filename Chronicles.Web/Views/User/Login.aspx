<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Login
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <% using (Html.BeginForm(MVC.User.Login(), FormMethod.Post, new { @class = "section dataform" }))
        { %>
        <h2>Login</h2>
        <%= Html.ValidationSummary() %>
        <fieldset>
            <p>User Name<span class="required">*</span></p> <input type="text" name="username" id="username" class="required"/>
            <p>Password<span class="required">*</span></p> <input type="password" name="password" id="password" class="required"/>
        </fieldset>
        <fieldset>
            <button id="submit" value="Login" class="button">Login</button>
        </fieldset>
      <%} %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MetaContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>