using ElmahCore;
using Microsoft.AspNetCore.Http;

namespace ElmahCoreExample.ElmahCustom
{
    public class CmsErrorLogFilter : IErrorFilter
    {
        public void OnErrorModuleFiltering(object sender, ExceptionFilterEventArgs args)
        {
            if (args.Context is HttpContext httpContext)
            {
                if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound) //Hata kodu 404 olanları filtrele ve dismiss et.
                {
                    args.Dismiss();
                }   
            }
        }
    }
}