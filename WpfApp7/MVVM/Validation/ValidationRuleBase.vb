Imports System.Globalization
Imports System.Windows.Controls

Namespace MVVM.Validation

    Public Class ValidationRuleBase
        Inherits ValidationRule

        Public Property ErrorMessage As String

        Public Overrides Function Validate(value As Object, cultureInfo As CultureInfo) As ValidationResult
            Dim isValid = ValidateValue(value)
            Return If(isValid, ValidationResult.ValidResult, New ValidationResult(False, ErrorMessage))
        End Function

        Protected Overridable Function ValidateValue(value As Object) As Boolean
            ' Logica di validazione personalizzata da sovrascrivere nelle classi derivate
            Return True
        End Function

    End Class

End Namespace
