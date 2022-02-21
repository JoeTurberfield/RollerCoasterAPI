using System;
using System.Data;

namespace RollerCoasterAPI.Models.Response
{
    public class DBResponse
    {
        public bool IsSuccess 
        { 
            get 
            { 
                return ResponseCode == 0; 
            }
        }
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public DataSet Ds { get; set; }

        public static DBResponse SetResponse(int responseCode = -1, string responseMessage = "")
        {
            return new DBResponse
            {
                ResponseCode = responseCode,
                ResponseMessage = responseMessage
            };
        }

        public static DBResponse SetResponse(DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return new DBResponse
                {
                    ResponseCode = -1,
                    ResponseMessage = "No data found"
                };
            }

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];

                return new DBResponse
                {
                    ResponseCode = Convert.ToInt32(row["ResponseCode"]),
                    ResponseMessage = row["ResponseCode"].ToString(),
                    Ds = ds
                };
            }

            return new DBResponse
            {
                ResponseCode = -1,
                ResponseMessage = "An unexpected error has occured"
            };
        }
    }

}
