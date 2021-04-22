using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductTermsControl.Insfrastructure.Helpers
{
    public class DescriptionUserActivity : ActionFilterAttribute
    {
        private readonly string _desc;
        

        public DescriptionUserActivity(string desc)
        {
            _desc = desc;
            
        }
        public async override Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
        {
            context.HttpContext.Response.Headers.Add("ActionDescription", new string[] { _desc });
            var resultContext = await next();
        }
    }
}
