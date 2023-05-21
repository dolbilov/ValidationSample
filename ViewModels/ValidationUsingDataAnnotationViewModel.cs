using System.ComponentModel.DataAnnotations;
using ReactiveUI;

namespace ValidationSample.ViewModels;

public class ValidationUsingDataAnnotationViewModel : ViewModelBase
{
    private string? _email;

    [Required]
    [EmailAddress]
    public string? Email
    {
        get => _email;
        set => this.RaiseAndSetIfChanged(ref _email, value);
    }
}