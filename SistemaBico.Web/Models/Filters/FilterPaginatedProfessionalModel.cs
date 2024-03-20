namespace SistemaBico.Web.Models.Filters
{
    public class FilterPaginatedProfessionalModel : FilterPaginatedBaseModel
    {
        public string? City { get; set; }
        public string? Profession { get; set; }
        public List<string> Especiality { get; set; }
        public int? Area { get; set; }
        public Guid ClientId { get; set; }

        public void RemoveSpacing()
        {
            Profession = Profession != null ? Profession.Trim() : "";
            City = City != null ? City.Trim() : "";
        }
    }
}
