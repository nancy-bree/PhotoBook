using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhotoBook.Models;
using System.Text.RegularExpressions;

namespace PhotoBook.Services
{
    public static class TagService
    {
        public static List<string> SplitTags(string tags)
        {
            var list = new List<string>(tags.Split(','));
            list.Remove(string.Empty);
            return list;
        }

        public static string GetTagClass(int category, int articles)
        {
            var result = (category * 100) / articles;
            if (result <= 1)
                return "tag1";
            if (result <= 4)
                return "tag2";
            if (result <= 8)
                return "tag3";
            if (result <= 12)
                return "tag4";
            if (result <= 18)
                return "tag5";
            if (result <= 30)
                return "tag6";
            return result <= 50 ? "tag7" : "";
        }
    }
}