namespace RollerCoasterAPI.Models.Response
{
    public class ResponseRollerCoaster : Response
    {
        public ResponseRollerCoaster()
        {

        }

        public ResponseRollerCoaster(DBResponse dbRes)
        {
            ResponseCode = dbRes.ResponseCode;
            ResponseMessage = dbRes.ResponseMessage;
        }
    }
}
