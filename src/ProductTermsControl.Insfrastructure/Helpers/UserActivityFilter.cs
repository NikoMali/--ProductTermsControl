using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using ProductTermsControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTermsControl.Insfrastructure.Helpers
{
    public class UserActivityFilter : IAsyncActionFilter
    {
        private readonly DataContext _context;

        public UserActivityFilter(DataContext context)
        {
            _context = context;
        }
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string data = "";

            var routeData = context.RouteData;
            var controller = routeData.Values["controller"];
            var action = routeData.Values["action"];
            var url = $"{controller}/{action}";
            var user = context.HttpContext.User.Identity.Name;
            var isValidDesc = true;

            if (user == null || 
                url.ToUpper() == ("Users/authenticate").ToUpper() || 
                url.ToUpper() == ("Users/register").ToUpper() ||
                url.ToUpper().Contains(("Users/UserActivity").ToUpper())
                )
            {

                isValidDesc = false;
            }

            if (!string.IsNullOrEmpty(context.HttpContext.Request.QueryString.Value))
            {
                data = context.HttpContext.Request.QueryString.Value;
            }
            else
            {
                var arguments = context.ActionArguments;

                var value = arguments.FirstOrDefault().Value;

                var convertedValue = JsonConvert.SerializeObject(value);
                data = convertedValue;
            }


            var ipAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();


            var result = await next.Invoke();
            
            if (isValidDesc)
            {
                var convertedResult = JsonConvert.SerializeObject(result.Result);
                string actDesc = context.HttpContext.Response.Headers.FirstOrDefault(x => x.Key == "ActionDescription").Value.FirstOrDefault();
                await SaveUserActivity(data, url,Int32.Parse(user), ipAddress,convertedResult, actDesc);
            }
            context.HttpContext.Response.Headers.Clear();
           
        }
        private async Task SaveUserActivity(string data, string url, int user, string ipAddress,string response, string actDesc)
        {
            var userActivity = new UserActivity
            {
                RequestData = data,
                Url = url,
                UserId = user,
                IpAddress = ipAddress,
                response = response,
                ActivityType = actDesc
            };

            _context.UserActivities.Add(userActivity);
           await _context.SaveChangesAsync();
        }
    }
}
