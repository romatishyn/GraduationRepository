using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Graduation.Web.Services
{
    public static class SessionService
    {
        public static HttpSessionState Session { get { return HttpContext.Current.Session; } }

        public static void Set<TValue>(string key, TValue value)
        {
            if (Session != null)
            {
                Session[key] = value;
            }
        }

        public static TValue Get<TValue>(string key, TValue defaultValue = null)
            where TValue : class
        {
            var value = Session[key] as TValue;
            if (value == null)
            {
                value = defaultValue;
            }
            return value;
        }
    }
}