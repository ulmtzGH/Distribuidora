using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Distribuidora.Data.Extensions
{
    public static class SqlParameterExtensions
    {
        public static void AddString(this SqlCommand command,
        string parameterName,
        string value,
        int size)
        {
            command.Parameters.Add(parameterName, SqlDbType.VarChar, size).Value = value;
        }

        public static void AddNullableString(this SqlCommand command,
            string parameterName,
            string? value,
            int size)
        {
            command.Parameters.Add(parameterName, SqlDbType.VarChar, size).Value =
                string.IsNullOrWhiteSpace(value)
                    ? DBNull.Value
                    : value;
        }

        public static void AddInt(this SqlCommand command,
            string parameterName,
            int value)
        {
            command.Parameters.Add(parameterName, SqlDbType.Int).Value = value;
        }

        public static void AddNullableInt(this SqlCommand command,
            string parameterName,
            int? value)
        {
            command.Parameters.Add(parameterName, SqlDbType.Int).Value =
                value.HasValue
                    ? value.Value
                    : DBNull.Value;
        }

        public static void AddDecimal(this SqlCommand command,
            string parameterName,
            decimal value,
            byte precision = 18,
            byte scale = 2)
        {
            var parameter = command.Parameters.Add(parameterName, SqlDbType.Decimal);

            parameter.Precision = precision;
            parameter.Scale = scale;
            parameter.Value = value;
        }

        public static void AddNullableDecimal(this SqlCommand command,
            string parameterName,
            decimal? value,
            byte precision = 18,
            byte scale = 2)
        {
            var parameter = command.Parameters.Add(parameterName, SqlDbType.Decimal);

            parameter.Precision = precision;
            parameter.Scale = scale;
            parameter.Value = value.HasValue
                ? value.Value
                : DBNull.Value;
        }

        public static void AddBoolean(this SqlCommand command,
            string parameterName,
            bool value)
        {
            command.Parameters.Add(parameterName, SqlDbType.Bit).Value = value;
        }
    }
}
