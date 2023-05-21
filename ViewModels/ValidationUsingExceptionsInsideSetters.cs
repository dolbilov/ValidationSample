using System;
using ReactiveUI;

namespace ValidationSample.ViewModels;

public class ValidationUsingExceptionsInsideSetters : ViewModelBase
{
    private string? _email;

    public string? Email
    {
        get => _email;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("This field is required");
            
            if (!value.Contains('@'))
                throw new ArgumentException("Not a valid E-Mail address");

            this.RaiseAndSetIfChanged(ref _email, value);
        }
    }
}