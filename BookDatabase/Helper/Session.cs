using BookDatabase.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookDatabase.Helper
{
    public class Session : ISession
    {
        private readonly IHttpContextAccessor _httpContext;

        public Session(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public void CreateUserSession(UserModel user)
        {
            string value = JsonConvert.SerializeObject(user);
            _httpContext.HttpContext.Session.SetString("userLoggedSession", value);
        }

        public UserModel GetUserSession()
        {
            string userSession = _httpContext.HttpContext.Session.GetString("userLoggedSession");

            if (string.IsNullOrEmpty(userSession)) return null;

            return JsonConvert.DeserializeObject<UserModel>(userSession);
        }

        public void RemoveUserSession()
        {
            _httpContext.HttpContext.Session.Remove("userLoggedSession");
        }
    }
}
