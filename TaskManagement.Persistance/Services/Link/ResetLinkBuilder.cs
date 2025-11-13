using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Persistance.Services.Link
{
    public class ResetLinkBuilder : IResetLinkBuilder
    {
        private readonly IConfiguration _configuration;

        public ResetLinkBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Build(string token)
        {
            var baseUrl = _configuration["FrontendBaseUrl"] ?? "https://localhost:7001";
            var encoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            return $"{baseUrl}/Account/ResetPassword?token={encoded}";
        }
    }
}
