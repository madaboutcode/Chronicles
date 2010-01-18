<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<PostSummary[]>" ContentType="text/xml" %><?xml version="1.0" encoding="UTF-8" ?>
<rss version="2.0" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:trackback="http://madskills.com/public/xml/rss/module/trackback/" xmlns:wfw="http://wellformedweb.org/CommentAPI/" xmlns:slash="http://purl.org/rss/1.0/modules/slash/" xmlns:copyright="http://blogs.law.harvard.edu/tech/rss" xmlns:image="http://purl.org/rss/1.0/modules/image/">
<channel>
    <title><%= Html.GetTitle("") %></title>
    <link>http://www.madaboutcode.com/blog/</link>
    <description>My life as a .net developer and all the mad stuff</description>
    <image>
      <url>/img/logo-text-alone.jpg</url>
      <title><%= Html.GetTitle(null) %></title>
      <link>http://www.madaboutcode.com/blog/</link>
    </image>
    <copyright>Ajeesh Mohan</copyright>
    <lastBuildDate><%= DateTime.Now.ToString("r") %></lastBuildDate>
    <generator>Chronicles blog engine</generator>
    <%
        foreach (var post in Model)
        {
            DateTime pubDate = post.PublishedDate;

            string postLink = string.Format("http://www.madaboutcode.com{0}", Url.Action(MVC.Posts.ViewPost(pubDate.Year, pubDate.Month, pubDate.Day, post.Id, Html.GetTextForUrl(post.Title))));
            string commentLink = postLink + "#comments";
    %>
        <item>
            <title><%= Html.Encode(post.Title) %></title>
            <link><%=postLink %></link>
            <description><%=Html.Encode(post.Summary) %></description>
            <dc:creator>Ajeesh Mohan</dc:creator>
            <guid><%=postLink %></guid>
            <pubDate><%=pubDate.ToString("r") %></pubDate>
            <comments><%=commentLink%></comments>
        </item>
    <%        
        } %>    
</channel>
</rss>