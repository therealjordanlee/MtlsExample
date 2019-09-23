using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using MtlsExample.Configurations;

namespace MtlsExample.Middleware
{
    public static class ClientCertMiddlewareExtensions
    {
        public static IApplicationBuilder UseClientCertMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ClientCertificateMiddleware>();
        }

        public static IApplicationBuilder UseClientCertMiddleware(this IApplicationBuilder builder, IOptions<CertificateConfiguration> options)
        {

            return builder.UseMiddleware<ClientCertificateMiddleware>(options);
        }
    }
}
