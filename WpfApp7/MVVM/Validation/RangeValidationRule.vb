Imports System.Globalization
Imports System.Windows.Controls

Namespace MVVM.Validation

    Public Class RangeValidationRule
        Inherits ValidationRule

        Public Property Minimum As Double
        Public Property Maximum As Double
        Public Property ErrorMessage As String

        Public Overrides Function Validate(value As Object, cultureInfo As CultureInfo) As ValidationResult
            Dim input As Double

            If Not Double.TryParse(TryCast(value, String), input) Then
                Return New ValidationResult(False, "Il valore deve essere un numero.")
            End If

            If input < Minimum OrElse input > Maximum Then
                Return New ValidationResult(False, ErrorMessage)
            End If

            Return ValidationResult.ValidResult
        End Function

    End Class

End Namespace
