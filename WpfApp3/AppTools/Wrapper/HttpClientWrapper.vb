Imports System.Net.Http
Imports System.Threading.Tasks

Namespace AppTools.Wrapper

    Public Class HttpClientWrapper
        Implements IDisposable

        Private httpClient As HttpClient

        Public Sub New()
            httpClient = New HttpClient()
        End Sub

        Public Async Function GetAsync(url As String) As Task(Of HttpResponseMessageWrapper)
            Dim response As HttpResponseMessage = Await httpClient.GetAsync(url)
            Return New HttpResponseMessageWrapper(response)
        End Function

        Public Async Function PostAsync(url As String, content As HttpContent) As Task(Of HttpResponseMessageWrapper)
            Dim response As HttpResponseMessage = Await httpClient.PostAsync(url, content)
            Return New HttpResponseMessageWrapper(response)
        End Function

        Public Sub Dispose() Implements IDisposable.Dispose
            httpClient.Dispose()
        End Sub

    End Class

End Namespace
