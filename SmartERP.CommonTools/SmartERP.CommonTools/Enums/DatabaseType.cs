using System.ComponentModel;

namespace SmartERP.CommonTools.Enums
{
    public enum DatabaseType
    {
        [Description("Unknown")]
        Unknown = 0,
        [Description("SQL Server")]
        SQLServer = 1,
        [Description("PostgreSQL")]
        PostgreSQL = 2
    }
}
