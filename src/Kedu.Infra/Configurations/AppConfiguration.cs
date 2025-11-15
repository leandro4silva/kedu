using System.Diagnostics.CodeAnalysis;

namespace Kedu.Infra.Configurations
{
    [ExcludeFromCodeCoverage]
    public sealed class AppConfiguration
    {
        private const string EnviromentDev = "dev";
        private const string EnvironmentSbx = "sbx";

        public PostgreSqlDbConfiguration? PostgreSql { get; set; }

        public string? Environment { get; set; }

        public bool IsDevelopment =>
            EnviromentDev.Equals(Environment, StringComparison.OrdinalIgnoreCase);

        public bool IsSandbox =>
            EnvironmentSbx.Equals(Environment, StringComparison.OrdinalIgnoreCase);
    }
}
