using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MtlsExample.Configurations;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MtlsExample.Middleware
{
    public class ClientCertificateMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly CertificateConfiguration _config;

        public ClientCertificateMiddleware(RequestDelegate next, IOptions<CertificateConfiguration> options)
        {
            _next = next;
            _config = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var certHeader = context.Request.Headers["MS-ASPNETCORE-CLIENTCERT"];
                var certBytes = Convert.FromBase64String(certHeader);
                var cert = new X509Certificate2(certBytes);
                var publicKey = Convert.ToBase64String(cert.GetPublicKey());
                var issuer = cert.GetNameInfo(X509NameType.DnsName, true);
                var subject = cert.GetNameInfo(X509NameType.DnsName, false);

                if (publicKey == _config.PublicKey)
                {
                    await _next.Invoke(context);
                }
            }
            catch
            {
                context.Response.StatusCode = 403;
            }
        }
    }
}
