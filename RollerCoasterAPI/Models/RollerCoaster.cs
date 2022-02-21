namespace RollerCoasterAPI.Models
{
    public class RollerCoaster
    {
        public static readonly string POV_dir = @"\\crystalfs\Fileshares\ProductVideosIncoming\VOD_CDN_upload";

        public int? Id { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ThemeParkId { get; set; }
        public int ManufacturerId { get; set; }
        public string YearOpened { get; set; }
        public decimal Height { get; set; }
        public decimal TrackLength { get; set; }
        public decimal MaxSpeed { get; set; }
        public int OperatingStatusId { get; set; }
        public decimal Cost { get; set; }
        public int TrainTypeId { get; set; }
    }
}
