Imports System.Net.Http
Imports System.Threading.Tasks

Namespace AppTools.Wrapper

    Public Class HttpResponseMessageWrapper
        Private response As HttpResponseMessage

        Public Sub New(response As HttpResponseMessage)
            Me.response = response
        End Sub

        Public Function GetStatusCode() As Integer
            Return CInt(response.StatusCode)
        End Function

        Public Function GetReasonPhrase() As String
            Return response.ReasonPhrase
        End Function

        Public Async Function GetContentAsStringAsync() As Task(Of String)
            Return Await response.Content.ReadAsStringAsync()
        End Function

        Public Sub Dispose()
            response.Dispose()
        End Sub

    End Class

End Namespace
