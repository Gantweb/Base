Imports System.ComponentModel.DataAnnotations

Namespace MVVM.Validation

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field, AllowMultiple:=False)>
    Public Class MaxLengthAttribute
        Inherits ValidationAttribute

        Private ReadOnly _maxLength As Integer

        Public Sub New(maxLength As Integer)
            _maxLength = maxLength
        End Sub

        Public Overrides Function IsValid(value As Object) As Boolean
            If value Is Nothing Then
                Return True
            End If

            Dim stringValue As String = TryCast(value, String)
            If stringValue IsNot Nothing Then
                Return stringValue.Length <= _maxLength
            End If

            Return False
        End Function

        Public Overrides Function FormatErrorMessage(name As String) As String
            Return $"{name} non può essere più lungo di {_maxLength} caratteri."
        End Function

    End Class

End Namespace
