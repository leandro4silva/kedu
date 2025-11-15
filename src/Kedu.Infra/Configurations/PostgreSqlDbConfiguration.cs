using System.Diagnostics.CodeAnalysis;

namespace Kedu.Infra.Configurations
{
    [ExcludeFromCodeCoverage]
    public sealed class PostgreSqlDbConfiguration
    {
        public string? ConnectionString { get; set; }
    }
}
