using MySql.Data.MySqlClient;
using Npgsql;
using PlatinDashboard.Domain.Farmacia.Interfaces.StoredProcedures;
using System;
using System.Data;
using System.Text;

namespace PlatinDashboard.Infra.Data.Farmacia.StoredProcedures
{
    public class StoredProcedureBase : IStoredProcedureBase
    {
        public DataTable ExecuteProcedure(string procedureName, object[] parameterList, string connectionString)
        {
            DataTable outputDataTable;
            using (var sqlConnection = new NpgsqlConnection(connectionString))
            {
                var parameterNames = "";
                if (parameterList != null)
                {
                    for (int i = 0; i < parameterList.Length; i = i + 2)
                    {
                        string parameterName = parameterList[i].ToString();
                        parameterNames += parameterNames != "" ? $", :p{parameterName}" : $":p{parameterName}";
                    }
                }
                var consulta = $"SELECT * from web.{procedureName}({parameterNames})";
                using (var npgsqlCommand = new NpgsqlCommand(consulta, sqlConnection))
                {
                    npgsqlCommand.CommandType = CommandType.Text;
                    if (parameterList != null)
                    {
                        for (int i = 0; i < parameterList.Length; i = i + 2)
                        {
                            string parameterName = parameterList[i].ToString();
                            object parameterValue = parameterList[i + 1];
                            npgsqlCommand.Parameters.AddWithValue($":p{parameterName}", parameterValue);
                        }
                    }

                    var npgsqlDataAdapter = new NpgsqlDataAdapter(npgsqlCommand);
                    DataSet outputDataSet = new DataSet();
                    try
                    {
                        npgsqlDataAdapter.Fill(outputDataSet, "resultset");
                    }
                    catch (SystemException systemException)
                    {
                        // The source table is invalid.
                        throw systemException; // to be handled as appropriate by calling function
                    }
                    outputDataTable = outputDataSet.Tables["resultset"];
                }
            }
            return outputDataTable;
        }

        public DataTable ExecuteProcedure(string procedureName, object[] parameterList, string filter, string connectionString)
        {
            DataTable outputDataTable;
            using (var sqlConnection = new NpgsqlConnection(connectionString))
            {
                var parameterNames = "";
                if (parameterList != null)
                {
                    for (int i = 0; i < parameterList.Length; i = i + 2)
                    {
                        string parameterName = parameterList[i].ToString();
                        parameterNames += parameterNames != "" ? $", :p{parameterName}" : $":p{parameterName}";
                    }
                }
                var consulta = $"SELECT * from web.{procedureName}({parameterNames}) {filter}";
                using (var npgsqlCommand = new NpgsqlCommand(consulta, sqlConnection))
                {
                    npgsqlCommand.CommandType = CommandType.Text;
                    if (parameterList != null)
                    {
                        for (int i = 0; i < parameterList.Length; i = i + 2)
                        {
                            string parameterName = parameterList[i].ToString();
                            object parameterValue = parameterList[i + 1];
                            npgsqlCommand.Parameters.AddWithValue($":p{parameterName}", parameterValue);
                        }
                    }

                    var npgsqlDataAdapter = new NpgsqlDataAdapter(npgsqlCommand);
                    DataSet outputDataSet = new DataSet();
                    try
                    {
                        npgsqlDataAdapter.Fill(outputDataSet, "resultset");
                    }
                    catch (SystemException systemException)
                    {
                        // The source table is invalid.
                        throw systemException; // to be handled as appropriate by calling function
                    }
                    outputDataTable = outputDataSet.Tables["resultset"];
                }
            }
            return outputDataTable;
        }

        public DataTable ExecuteProcedure(string procedureName, object[] parameterList, string connectionString, bool isMysql)
        {
            DataTable outputDataTable;
            using (var sqlConnection = new MySqlConnection(connectionString))
            {
                var consulta = $"{procedureName}";
                using (var mysqlCommand = new MySqlCommand(consulta, sqlConnection))
                {
                    mysqlCommand.CommandType = CommandType.StoredProcedure;
                    if (parameterList != null)
                    {
                        for (int i = 0; i < parameterList.Length; i = i + 2)
                        {
                            string parameterName = parameterList[i].ToString();
                            object parameterValue = parameterList[i + 1];
                            mysqlCommand.Parameters.AddWithValue($"@{parameterName}", parameterValue);
                        }
                    }

                    var mysqlDataAdapter = new MySqlDataAdapter(mysqlCommand);
                    DataSet outputDataSet = new DataSet();
                    try
                    {
                        mysqlDataAdapter.Fill(outputDataSet, "resultset");
                    }
                    catch (SystemException systemException)
                    {
                        // The source table is invalid.
                        throw systemException; // to be handled as appropriate by calling function
                    }
                    outputDataTable = outputDataSet.Tables["resultset"];
                }
            }
            return outputDataTable;
        }

        public DataTable ExecuteProcedure(string procedureName, object[] parameterList, string filter, string connectionString, bool isMySql)
        {
            //Método para executar procedures da base MySql
            DataTable outputDataTable;
            using (var sqlConnection = new MySqlConnection(connectionString))
            {
                var parameterNames = "";
                if (parameterList != null)
                {
                    for (int i = 0; i < parameterList.Length; i = i + 2)
                    {
                        string parameterName = parameterList[i].ToString();
                        parameterNames += parameterNames != "" ? $", :p{parameterName}" : $":p{parameterName}";
                    }
                }
                var consulta = $"CALL {procedureName}({parameterNames}) {filter}";
                using (var mysqlCommand = new MySqlCommand(consulta, sqlConnection))
                {
                    mysqlCommand.CommandType = CommandType.Text;
                    if (parameterList != null)
                    {
                        for (int i = 0; i < parameterList.Length; i = i + 2)
                        {
                            string parameterName = parameterList[i].ToString();
                            object parameterValue = parameterList[i + 1];
                            mysqlCommand.Parameters.AddWithValue($":p{parameterName}", parameterValue);
                        }
                    }

                    var mysqlDataAdapter = new MySqlDataAdapter(mysqlCommand);
                    DataSet outputDataSet = new DataSet();
                    try
                    {
                        mysqlDataAdapter.Fill(outputDataSet, "resultset");
                    }
                    catch (SystemException systemException)
                    {
                        // The source table is invalid.
                        throw systemException; // to be handled as appropriate by calling function
                    }
                    outputDataTable = outputDataSet.Tables["resultset"];
                }
            }
            return outputDataTable;
        }

        protected string FormatUads(int[] uads)
        {
            var uadList = new StringBuilder();
            for (int i = 0; i < uads.Length; i++)
            {
                uadList.Append(uads[i]);
                if (i < uads.Length - 1)
                    uadList.Append(",");
            }
            return uadList.ToString();
        }
    }
}
