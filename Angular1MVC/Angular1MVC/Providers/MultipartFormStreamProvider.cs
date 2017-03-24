﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Angular1MVC.Providers
{
    public class MultipartFormStreamProvider : MultipartFormDataStreamProvider
    {
        public MultipartFormStreamProvider(string rootPath) : base(rootPath)
        {
        }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            if (headers != null && headers.ContentDisposition != null)
                return headers.ContentDisposition.FileName.TrimEnd('"').TrimStart('"');

            return base.GetLocalFileName(headers);
        }

    }
}