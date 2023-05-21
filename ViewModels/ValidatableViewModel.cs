using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ReactiveUI;

namespace ValidationSample.ViewModels;

public abstract class ValidatableViewModel : ViewModelBase, INotifyDataErrorInfo
{
    #region Fields

    private Dictionary<string, List<ValidationResult>> _validationErrors = new();

    #endregion


    #region Events

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    #endregion


    #region Properties

    public bool HasErrors => _validationErrors.Count > 0;

    #endregion


    #region Methods

    public IEnumerable GetErrors(string? propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
            return _validationErrors.Values;

        if (_validationErrors.TryGetValue(propertyName, out var errors))
            return errors;

        return Array.Empty<ValidationResult>();
    }

    protected void ClearErrors(string? propertyName = "")
    {
        if (string.IsNullOrEmpty(propertyName))
            _validationErrors.Clear();
        else
            _validationErrors.Remove(propertyName);
        
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        this.RaisePropertyChanged(nameof(HasErrors));
        
    }

    protected void AddError(string propertyName, string errorMessage)
    {
        ArgumentException.ThrowIfNullOrEmpty(propertyName);

        if (!_validationErrors.ContainsKey(propertyName))
            _validationErrors.Add(propertyName, new List<ValidationResult>());

        _validationErrors[propertyName].Add(new ValidationResult(errorMessage));
        
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        this.RaisePropertyChanged(nameof(HasErrors));
    }

    #endregion
}
