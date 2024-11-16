Imports System.Globalization
Imports System.Windows.Controls

Namespace MVVM.Validation

    Public Class StringLengthValidationRule
        Inherits ValidationRule

        Public Property MinimumLength As Integer
        Public Property MaximumLength As Integer

        Public Overrides Function Validate(value As Object, cultureInfo As CultureInfo) As ValidationResult
            Dim input As String = TryCast(value, String)

            If String.IsNullOrEmpty(input) Then
                Return New ValidationResult(False, "Il campo è obbligatorio.")
            End If

            If input.Length < MinimumLength OrElse input.Length > MaximumLength Then
                Return New ValidationResult(False, $"La lunghezza deve essere tra {MinimumLength} e {MaximumLength} caratteri.")
            End If

            Return ValidationResult.ValidResult
        End Function

    End Class

End Namespace
