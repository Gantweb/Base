Imports System.ComponentModel.DataAnnotations

Namespace MVVM.Validation

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field, AllowMultiple:=False)>
    Public Class MinLengthAttribute
        Inherits ValidationAttribute

        Private ReadOnly _minLength As Integer

        Public Sub New(minLength As Integer)
            _minLength = minLength
        End Sub

        Public Overrides Function IsValid(value As Object) As Boolean
            If value Is Nothing Then
                Return True
            End If

            Dim stringValue As String = TryCast(value, String)
            If stringValue IsNot Nothing Then
                Return stringValue.Length >= _minLength
            End If

            Return False
        End Function

        Public Overrides Function FormatErrorMessage(name As String) As String
            Return $"{name} deve essere almeno di {_minLength} caratteri."
        End Function

    End Class

End Namespace
