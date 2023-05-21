using ReactiveUI;
using System;
using System.Net.Mail;

namespace ValidationSample.ViewModels;

public class ValidationUsingINotifyDataErrorViewModel : ValidatableViewModel
{
    #region Fields

    private string? _email;

    #endregion


    public ValidationUsingINotifyDataErrorViewModel()
    {
        this.WhenAnyValue(vm => vm.Email).Subscribe(_ => ValidateEmailProperty());
    }


    #region Properties

    public string? Email
    {
        get => _email;
        set => this.RaiseAndSetIfChanged(ref _email, value);
    }

    #endregion


    #region Methods

    private void ValidateEmailProperty()
    {
        ClearErrors(nameof(Email));
        
        if (string.IsNullOrEmpty(Email))
            AddError(nameof(Email), "This field is required.");
        else if (!ValidateEmailAddress())
            AddError(nameof(Email), "This is not a valid E-Mail address");
    }

    private bool ValidateEmailAddress()
    {
        try
        {
            var address = new MailAddress(Email!).Address;
            return address == Email;
        }
        catch (Exception)
        {
            return false;
        }
    }

    #endregion
}
