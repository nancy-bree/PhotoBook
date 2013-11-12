﻿using System.ComponentModel.DataAnnotations;
using System.Web;

namespace PhotoBook.Services
{
    /// <summary>
    /// Maximum file size to upload.
    /// </summary>
    public class FileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxSize;

        public FileSizeAttribute(int maxSize)
        {
            _maxSize = maxSize;
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            return (value as HttpPostedFileBase).ContentLength <= _maxSize;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("The file size should not exceed {0} MB", _maxSize/1024);
        }
    }
}