using System.Collections.Generic;

namespace RollerCoasterAPI.Models
{
    public class DropDownList
    {
        public List<DropDownItem> _DropDownList { get; set; } = new List<DropDownItem>();
    }

    public class DropDownItem
    {
        public int Value { get; set; }
        public string Name { get; set; }
    }
}
