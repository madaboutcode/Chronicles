using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Text;

namespace Chronicles.Web.Utility
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString GetTitle(this HtmlHelper html, string title)
        {
            const string titlePostfix = "Mad about code : Personal Blog of Ajeesh Mohan";

            string finalTitle = titlePostfix;

            if(!string.IsNullOrEmpty(title))
                finalTitle = title + " - " + finalTitle;

            return MvcHtmlString.Create(finalTitle);
        }

        public static String GetTextForUrl(this HtmlHelper html, string text)
        {
            return UrlHelpers.GetTextForUrl(text);
        }

        public static MvcHtmlString AsciiToHtml(this HtmlHelper html, string text)
        {
            if (string.IsNullOrEmpty(text))
                return MvcHtmlString.Empty;

            StringBuilder outputHtml = new StringBuilder();

            string[] lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < lines.Length; i++)
            {
                outputHtml.AppendFormat("<p>{0}</p>", html.Encode(lines[i]));
            }

            return MvcHtmlString.Create(outputHtml.ToString());
        }

        public static string Gravatar(this HtmlHelper html, string emailId)
        {
            return GravatarProvider.GetIcon(emailId);
        }
    }
}
