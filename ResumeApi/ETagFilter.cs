using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ResumeApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeApi
{
    public class ETagFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Method == "PUT" || context.HttpContext.Request.Method == "PATCH")
            {
                string[] path = context.HttpContext.Request.Path.ToString().Split("/");
                string key = null;
                string etag = null;

                if (path.Length >= 3)
                {
                    key = context.HttpContext.Request.Path.ToString().Split("/")[2];
                    key = key.Replace("%20", String.Empty);

                    if (ResumeRepository.etags.ContainsKey(key))
                    {
                        etag = ResumeRepository.etags[key];
                    }
                }
                else
                {
                    etag = ResumeRepository.etag;
                }

                if (etag != null && context.HttpContext.Request.Headers.Keys.Contains("If-None-Match") && context.HttpContext.Request.Headers["If-None-Match"].ToString() != etag)
                {
                    context.Result = new StatusCodeResult(409);
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Method == "GET" || context.HttpContext.Request.Method == "HEAD")
            {
                if (context.HttpContext.Response.StatusCode == 200)
                {
                    string[] path = context.HttpContext.Request.Path.ToString().Split("/");
                    string key = null;
                    string etag = null;

                    if (path.Length >= 3)
                    {
                        key = context.HttpContext.Request.Path.ToString().Split("/")[2];
                        key = key.Replace("%20", String.Empty);

                        if (ResumeRepository.etags.ContainsKey(key))
                        {
                            etag = ResumeRepository.etags[key];
                        }
                    }
                    else
                    {
                        etag = ResumeRepository.etag;
                    }

                    if (etag != null && context.HttpContext.Request.Headers.Keys.Contains("If-None-Match") && context.HttpContext.Request.Headers["If-None-Match"].ToString() == etag)
                    {
                        context.Result = new StatusCodeResult(304);
                    }
                    context.HttpContext.Response.Headers.Add("ETag", etag);
                }
            }
        }
    }
}
