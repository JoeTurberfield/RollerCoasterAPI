﻿using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using RollerCoasterAPI.Models.Response;

namespace RollerCoasterAPI.Data
{
    public sealed class DBHelper
    {
        public static string ConnectionString { get; set; }
        private readonly IConfiguration _config;

        private DBHelper(IConfiguration config)
        {
            _config = config;
            ConnectionString = _config["ConnectionStrings:DbConnection"];
        }

        public static DBResponse ExecuteDataSet(string spName)
        {
            if (ConnectionString == null || ConnectionString.Length == 0)
            {
                throw new ArgumentNullException("DBAccess.ConnectionString");
            }

            if (spName == null || spName.Length == 0)
            {
                throw new ArgumentNullException("DBAccess.spName");
            }

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(spName, connection))
            {
                // Create the DataAdapter & DataSet
                using SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                // Fill the DataSet using default values for DataTable names, etc
                da.Fill(ds);

                // Detach the SqlParameters from the command object, so they can be used again
                cmd.Parameters.Clear();

                return new DBResponse(ds);
            }
        }

        public static DBResponse ExecuteDataSet(string spName, dynamic parameterValues)
        {
            if (ConnectionString == null || ConnectionString.Length == 0)
            {
                throw new ArgumentNullException("DBAccess.ConnectionString");
            }

            if (spName == null || spName.Length == 0)
            {
                throw new ArgumentNullException("DBAccess.spName");
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();

            foreach (var prop in parameterValues.GetType().GetProperties())
            {
                object value = parameterValues.GetType().GetProperty(prop.Name).GetValue(parameterValues, null);
                parameters.Add($"@{prop.Name}", value);
            }

            if ((parameterValues != null) && parameters.Count > 0)
            {
                SqlParameter[] commandParameters = DiscoverSpParameterSet(spName, false);

                if ((commandParameters == null) || (parameterValues == null))
                {
                    // Do nothing if we get no data
                    return new DBResponse(-1, "No parameters or corresponding values found");
                }

                AssignParameterValues(commandParameters, parameters);

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand cmd = new SqlCommand(spName, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (commandParameters != null)
                    {
                        foreach (SqlParameter param in commandParameters)
                        {
                            SqlParameter nameParam = new SqlParameter(param.ParameterName, param.SqlValue);

                            if (param != null)
                            {
                                if (param.Direction == ParameterDirection.InputOutput || param.Direction == ParameterDirection.Input && (param == null))
                                {
                                    param.Value = DBNull.Value;
                                }

                                cmd.Parameters.Add(nameParam);
                            }
                        }
                    }

                    // Create the DataAdapter & DataSet
                    using SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    // Fill the DataSet using default values for DataTable names, etc
                    da.Fill(ds);

                    // Detach the SqlParameters from the command object, so they can be used again
                    cmd.Parameters.Clear();

                    return new DBResponse(ds);
                }
            }

            return new DBResponse();
        }

        private static SqlParameter[] DiscoverSpParameterSet(string spName, bool includeReturnValueParameter)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(spName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();
            SqlCommandBuilder.DeriveParameters(cmd);
            connection.Close();

            if (!includeReturnValueParameter)
            {
                cmd.Parameters.RemoveAt(0);
            }

            SqlParameter[] discoveredParameters = new SqlParameter[cmd.Parameters.Count];

            cmd.Parameters.CopyTo(discoveredParameters, 0);

            // Init the parameters with a DBNull value
            foreach (SqlParameter discoveredParameter in discoveredParameters)
            {
                discoveredParameter.Value = DBNull.Value;
            }

            return discoveredParameters;
        }

        private static void AssignParameterValues(SqlParameter[] commandParameters, Dictionary<string, object> parameterValues)
        {
            // Iterate through the SqlParameters, assigning the values from the corresponding position in the
            // value array
            for (int i = 0; i < commandParameters.Length; i++)
            {
                // If the current array value derives from IDbDataParameter, then assign its Value property
                if (parameterValues.TryGetValue(commandParameters[i].ParameterName, out object theValue) && theValue != null)
                {
                    commandParameters[i].Value = theValue;
                }
                else
                {
                    commandParameters[i].Value = DBNull.Value;
                }
            }
        }
    }
}
