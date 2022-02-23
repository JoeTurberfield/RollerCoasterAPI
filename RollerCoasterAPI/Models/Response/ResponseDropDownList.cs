using System.Collections.Generic;

namespace RollerCoasterAPI.Models.Response
{
    public class ResponseDropDownList : Response
    {
        public List<DropDownList> DropDownLists { get; set; } = new List<DropDownList>();
    }
}
