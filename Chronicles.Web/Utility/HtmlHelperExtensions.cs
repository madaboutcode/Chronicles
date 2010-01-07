using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Text;
using Chronicles.Framework;
using System.Xml.Xsl;
using System.IO;

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
            return StringUtility.GetNormalizedText(text);
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

        private static XslCompiledTransform GetTransform(string transform)
        {
            string cacheKey = string.Format("XslTranform:{0}", transform);

            HttpContext ctx = HttpContext.Current;
            XslCompiledTransform xsl = null;

            if (ctx != null)
            {
                xsl = ctx.Cache.Get(cacheKey) as XslCompiledTransform;
            }

            if (xsl == null)
            {
                xsl = new XslCompiledTransform();
                xsl.Load(transform);

                if (ctx != null)
                    ctx.Cache.Insert(cacheKey, xsl);
            }

            return xsl;
        }

        public static string XslTransform(this HtmlHelper html, string datafile, string transform)
        {
            if (string.IsNullOrEmpty(datafile))
                throw new ArgumentException("datafile");

            if (string.IsNullOrEmpty(transform))
                throw new ArgumentNullException("transform");

            HttpContextBase ctx = html.ViewContext.HttpContext;

            string dataFilePath = null, transformFilePath = null;

            if(ctx!=null)
            {
                dataFilePath = ctx.Server.MapPath(datafile);
                transformFilePath = ctx.Server.MapPath(transform);
            }

            XslCompiledTransform xsl = GetTransform(transformFilePath);

            StringWriter sw = new StringWriter();
            xsl.Transform(dataFilePath, null, sw);

            return sw.ToString();
        }
    }
}
