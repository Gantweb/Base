Imports System.Globalization
Imports System.Windows.Controls

Namespace MVVM.Validation

    Public Class RequiredFieldValidationRule
        Inherits ValidationRule

        Public Property ErrorMessage As String

        Public Overrides Function Validate(value As Object, cultureInfo As CultureInfo) As ValidationResult
            Dim input As String = TryCast(value, String)

            If String.IsNullOrEmpty(input) Then
                Return New ValidationResult(False, ErrorMessage)
            End If

            Return ValidationResult.ValidResult
        End Function

    End Class

End Namespace
