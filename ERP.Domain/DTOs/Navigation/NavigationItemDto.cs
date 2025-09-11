namespace ERP.Domain.DTOs.Navigation
{
    public class NavigationItemDto
    {
        public string Id { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string? Route { get; set; }
        public string Icon { get; set; } = null!;
        public int Order { get; set; }
        public bool Visible { get; set; }
        public List<NavigationItemDto> Children { get; set; } = new();
    }
}
