using System.Data.SqlClient;
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
    public sealed class DBAccess
    {
        public static string ConnectionString { get; set; }
        private readonly IConfiguration _config;

        private DBAccess(IConfiguration config)
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

                return DBResponse.SetResponse(ds);
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
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand cmd = new SqlCommand(spName, connection))
                {
                    SqlParameter[] commandParameters = DiscoverSpParameterSet(connection, spName, false);

                    if ((commandParameters == null) || (parameterValues == null))
                    {
                        // Do nothing if we get no data
                        return DBResponse.SetResponse(-1, "No parameters or corresponding values found");
                    }

                    // Iterate through the SqlParameters, assigning the values from the corresponding position in the
                    // value array
                    for (int i = 0; i < commandParameters.Length; i++)
                    {
                        // If the current array value derives from IDbDataParameter, then assign its Value property
                        if (parameters.TryGetValue(commandParameters[i].ParameterName, out object theValue) && theValue != null)
                        {
                            commandParameters[i].Value = theValue;
                        }
                        else
                        {
                            commandParameters[i].Value = DBNull.Value;
                        }
                    }

                    if (commandParameters != null)
                    {
                        foreach (SqlParameter p in commandParameters)
                        {
                            if (p != null)
                            {
                                // Check for derived output value with no value assigned
                                if ((p.Direction == ParameterDirection.InputOutput ||
                                    p.Direction == ParameterDirection.Input) &&
                                    (p.Value == null))
                                {
                                    p.Value = DBNull.Value;
                                }
                                cmd.Parameters.Add(p);
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

                    return DBResponse.SetResponse(ds);
                }
            }

            return DBResponse.SetResponse();
        }

        private static SqlParameter[] DiscoverSpParameterSet(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
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
    }
}
