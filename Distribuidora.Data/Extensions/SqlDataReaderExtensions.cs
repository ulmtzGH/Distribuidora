using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distribuidora.Data.Extensions
{
    public static class SqlDataReaderExtensions
    {
        public static int GetInt32(
        this SqlDataReader reader,
        string columnName)
        {
            return Convert.ToInt32(reader[columnName]);
        }

        public static int? GetNullableInt32(
            this SqlDataReader reader,
            string columnName)
        {
            return reader[columnName] == DBNull.Value
                ? null
                : Convert.ToInt32(reader[columnName]);
        }

        public static string GetString(
            this SqlDataReader reader,
            string columnName)
        {
            return reader[columnName]?.ToString() ?? string.Empty;
        }

        public static bool GetBoolean(
            this SqlDataReader reader,
            string columnName)
        {
            return Convert.ToBoolean(reader[columnName]);
        }

        public static bool? GetNullableBoolean(
            this SqlDataReader reader,
            string columnName)
        {
            return reader[columnName] == DBNull.Value
                ? null
                : Convert.ToBoolean(reader[columnName]);
        }

        public static decimal GetDecimal(
            this SqlDataReader reader,
            string columnName)
        {
            return Convert.ToDecimal(reader[columnName]);
        }

        public static decimal? GetNullableDecimal(
            this SqlDataReader reader,
            string columnName)
        {
            return reader[columnName] == DBNull.Value
                ? null
                : Convert.ToDecimal(reader[columnName]);
        }

        public static DateTime? GetNullableDateTime(
            this SqlDataReader reader,
            string columnName)
        {
            return reader[columnName] == DBNull.Value
                ? null
                : Convert.ToDateTime(reader[columnName]);
        }
    }
}
