Imports System.ComponentModel.DataAnnotations

Namespace MVVM.Validation

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field, AllowMultiple:=False)>
    Public Class CustomValidationAttribute
        Inherits ValidationAttribute

        Private ReadOnly _validationMethod As Func(Of Object, Boolean)

        Public Sub New(validationMethod As Func(Of Object, Boolean))
            If validationMethod Is Nothing Then
                Throw New ArgumentNullException(NameOf(validationMethod))
            End If

            _validationMethod = validationMethod
        End Sub

        Public Overrides Function IsValid(value As Object) As Boolean
            Return _validationMethod.Invoke(value)
        End Function

        Public Overrides Function FormatErrorMessage(name As String) As String
            Return $"{name} non è valido."
        End Function

    End Class

End Namespace
