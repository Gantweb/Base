Imports System.Globalization
Imports System.Windows.Controls

Namespace MVVM.Validation

    Public Class RegexValidationRule
        Inherits ValidationRule

        Public Property Pattern As String
        Public Property ErrorMessage As String

        Public Overrides Function Validate(value As Object, cultureInfo As CultureInfo) As ValidationResult
            Dim input As String = TryCast(value, String)

            If String.IsNullOrEmpty(input) Then
                Return New ValidationResult(False, "Il campo è obbligatorio.")
            End If

            If Not System.Text.RegularExpressions.Regex.IsMatch(input, Pattern) Then
                Return New ValidationResult(False, ErrorMessage)
            End If

            Return ValidationResult.ValidResult
        End Function

    End Class

End Namespace
