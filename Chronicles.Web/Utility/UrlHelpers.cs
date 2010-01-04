﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chronicles.Entities;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace Chronicles.Web.Utility
{
    public static class UrlHelpers
    {
        public static ActionResult GetActionUrl(this Post post)
        {
            DateTime date = post.ScheduledDate;

            return MVC.Posts.ViewPost(date.Year, date.Month, date.Day, post.Id, GetTextForUrl(post.Title));
        }

        private static readonly Regex titleRegex = new Regex("[^a-zA-Z0-9]+", RegexOptions.Compiled);

        public static String GetTextForUrl(string text)
        {
            return titleRegex.Replace(text, "-");
        }
    }
}
