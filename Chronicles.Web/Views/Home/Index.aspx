﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<Chronicles.Web.ViewModels.PostSummary[]>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%= Html.GetTitle(string.Empty) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.DisplayForModel() %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="robots" content="noarchive,all" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
