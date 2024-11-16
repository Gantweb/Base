Imports System.Net.Http
Imports System.Text
Imports WpfApp2.AppTools.Wrapper

Namespace AppTools.Helper

    Public Class JsonApiHelper

        ' Invia dati JSON all'API
        Public Shared Async Function InviaJson(url As String, jsonData As String) As Task(Of HttpResponseMessage)
            Return Await EseguiInviaJson(url, jsonData)
        End Function

        Private Shared Async Function EseguiInviaJson(url As String, jsonData As String) As Task(Of HttpResponseMessage)
            Using httpClientWrapper As New HttpClientWrapper()
                Dim content As New StringContent(jsonData, Encoding.UTF8, "application/json")
                Return Await httpClientWrapper.PostAsync(url, content)
            End Using
        End Function

        ' Ricevi dati JSON dall'API
        Public Shared Async Function RiceviJson(url As String) As Task(Of String)
            Return Await EseguiRiceviJson(url)
        End Function

        Private Shared Async Function EseguiRiceviJson(url As String) As Task(Of String)
            Using httpClientWrapper As New HttpClientWrapper()
                Dim response As HttpResponseMessage = Await httpClientWrapper.GetAsync(url)
                Return Await response.Content.ReadAsStringAsync()
            End Using
        End Function

    End Class

End Namespace