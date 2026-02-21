using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Infrastructure.Extensions
{
    public static class DataProtectionExtensions
    {
        public static IServiceCollection AddHmsDataProtection(this IServiceCollection services)
        {
            // Get folder path from environment variable or default
            var keyPath = Environment.GetEnvironmentVariable("DATA_PROTECTION_KEYS")
                          ?? Path.Combine(AppContext.BaseDirectory, "keys");

            var directoryInfo = new DirectoryInfo(keyPath);
            if (!directoryInfo.Exists)
                directoryInfo.Create();

            // Optional: certificate protection
            X509Certificate2? cert = null;
            var certPath = Path.Combine(AppContext.BaseDirectory, "cert.pfx");
            var certPassword = Environment.GetEnvironmentVariable("CERT_PASSWORD");

            if (File.Exists(certPath) && !string.IsNullOrEmpty(certPassword))
            {
                cert = X509CertificateLoader.LoadPkcs12FromFile(certPath, certPassword);
            }

            var builder = services.AddDataProtection()
                .SetApplicationName("HMS-App")
                .PersistKeysToFileSystem(directoryInfo);

            if (cert != null)
            {
                builder.ProtectKeysWithCertificate(cert);
            }

            return services;
        }
    }
}