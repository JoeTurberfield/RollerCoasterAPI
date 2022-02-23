using System;
using System.Data;
using System.Linq;

namespace RollerCoasterAPI.Models.Response
{
    public sealed class DBResponse
    {
        public int ResponseCode 
        {
            get
            {
                if (ErrorCheck)
                {
                    return Convert.ToInt32(_DataSet.Tables[0].Rows[0]["ResponseCode"]);
                }
                else
                {
                    return 0;
                }
            }
            set { }
        }

        public string ResponseMessage 
        {
            get
            {
                if (ErrorCheck && _DataSet.Tables[0].Columns.Contains("ResponseMessage"))
                {
                    return _DataSet.Tables[0].Rows[0]["ResponseMessage"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set { }
        }

        public bool ErrorCheck
        {
            get
            {
                if (_DataSet.Tables[0].Columns.Contains("ResponseCode"))
                {
                    return true;
                }

                return false;
            }
        }

        public DataSet _DataSet { get; set; }

        public bool IsSuccess 
        { 
            get 
            { 
                return !IsEmpty && ResponseCode == 0; 
            }
        }

        public bool IsEmpty
        {
            get
            {
                return _DataSet == null || _DataSet.Tables.Count == 0 || _DataSet.Tables[0].Rows.Count == 0;
            }
        }

        public DBResponse()
        {

        }

        public DBResponse(DataSet ds)
        {
            _DataSet = ds;
        }

        public DBResponse(int responseCode = -1, string responseMessage = "")
        {
            ResponseCode = responseCode;
            ResponseMessage = responseMessage;
        }
    }
}
