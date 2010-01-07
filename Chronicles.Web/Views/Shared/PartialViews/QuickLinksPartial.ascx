<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<ul id="quicklinks" class="invisible">
    <%= Html.XslTransform("~/content/xml/quicklinks.xml", Links.xslt.quicklinks_xslt) %>
</ul>
