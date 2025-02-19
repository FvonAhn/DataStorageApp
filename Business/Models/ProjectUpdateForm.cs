namespace Business.Models;
public class ProjectUpdateForm
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime? EndDate { get; set; }
}
