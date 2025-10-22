using FluentValidation;
using Marten;
namespace Shows.Api.Models;

public record ShowCreateModel
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StreamingService { get; set; } = string.Empty;

}

public class ShowsCreateModelValidator : AbstractValidator<ShowCreateModel>
{
    public ShowsCreateModelValidator()
    {

        RuleFor(x => x.Title).NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleFor(x => x.Description).NotEmpty().MinimumLength(10).MaximumLength(500);
        RuleFor(x => x.StreamingService).NotEmpty();
    }
}
/*
 * public record VendorCreateModel
{
   
    public string Name { get; set; } = string.Empty;
    public VendorPointOfContact PointOfContact { get; set; } = new();
}


public class VendorCreateModelValidator : AbstractValidator<VendorCreateModel>
{
    public VendorCreateModelValidator()
    {
        
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleFor(x => x.PointOfContact).NotNull().SetValidator(validator: new VendorPointOfContactValidator());
    }
}
*/