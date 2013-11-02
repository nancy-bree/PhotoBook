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
    }
}