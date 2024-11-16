Imports System.ComponentModel.DataAnnotations
Imports System.Reflection

Namespace MVVM

    Public Class ValidationService

        Public Function CollectErrors(observableObjects As IEnumerable(Of AppObservable), controlMappings As Dictionary(Of String, String)) As List(Of ValidationErrors)
            Dim allErrors As New List(Of ValidationErrors)

            For Each obj In observableObjects
                Dim results = obj.Validate()
                For Each result In results
                    For Each memberName In result.MemberNames
                        Dim controlName As String = Nothing
                        If controlMappings.TryGetValue(memberName, controlName) Then
                            ' Ottieni il DisplayAttribute per la proprietà
                            Dim displayName = memberName
                            Dim propInfo = obj.GetType().GetProperty(memberName)
                            If Not IsNothing(propInfo) Then
                                Dim displayAttr = propInfo.GetCustomAttribute(Of DisplayAttribute)()
                                If Not IsNothing(displayAttr) Then
                                    displayName = displayAttr.Name
                                End If
                            End If

                            allErrors.Add(New ValidationErrors With {
                            .PropertyName = displayName,
                            .ErrorMessage = result.ErrorMessage,
                            .ControlName = controlName
                        })
                        End If
                    Next
                Next
            Next

            Return allErrors
        End Function

    End Class

End Namespace
