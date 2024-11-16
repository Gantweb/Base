Imports System.ComponentModel.DataAnnotations

Namespace MVVM

    Public Class ValidationService

        Private ReadOnly _errors As New Dictionary(Of String, ValidationErrors)

        Public Function ValidateObject(obj As Object) As Boolean
            Dim context As New ValidationContext(obj)
            Dim results As New List(Of ValidationResult)
            Validator.TryValidateObject(obj, context, results, True)

            _errors.Clear()
            For Each validationResult In results
                For Each memberName In validationResult.MemberNames
                    If Not _errors.ContainsKey(memberName) Then
                        _errors(memberName) = New ValidationErrors(memberName)
                    End If
                    _errors(memberName).Add(validationResult.ErrorMessage)
                Next
            Next

            Return results.Count = 0
        End Function

        Public ReadOnly Property Errors As Dictionary(Of String, ValidationErrors)
            Get
                Return _errors
            End Get
        End Property

    End Class

End Namespace
