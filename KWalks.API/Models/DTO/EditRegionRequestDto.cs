namespace KWalks.API.Models.DTO
{
    public class EditRegionRequestDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionUrl { get; set; }  //Means Nullible
    }
}
