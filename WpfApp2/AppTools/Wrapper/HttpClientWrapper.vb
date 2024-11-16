Imports System.Net.Http

Namespace AppTools.Wrapper

    Public Class HttpClientWrapper
        Implements IDisposable

        Private httpClient As HttpClient

        Public Sub New()
            httpClient = New HttpClient()
        End Sub

        Public Async Function GetAsync(url As String) As Task(Of HttpResponseMessage)
            Return Await httpClient.GetAsync(url)
        End Function

        Public Async Function PostAsync(url As String, content As HttpContent) As Task(Of HttpResponseMessage)
            Return Await httpClient.PostAsync(url, content)
        End Function

        Public Sub Dispose() Implements IDisposable.Dispose
            httpClient.Dispose()
        End Sub

    End Class

End Namespace