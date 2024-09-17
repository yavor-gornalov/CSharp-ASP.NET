using Microsoft.AspNetCore.Http.HttpResults;

namespace SoftUniBazar.Core.Models;

public class AdAllViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string ImageUrl { get; set; } = default!;

    public DateTime CreatedOn { get; set; }

    public string Category { get; set; } = default!;

    public string Description { get; set; } = default!;

    public decimal Price { get; set; }

    public string Owner { get; set; } = default!;

}

