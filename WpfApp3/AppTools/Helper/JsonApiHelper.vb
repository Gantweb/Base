Imports System.Text
Imports WpfApp3.AppTools.Wrapper

Namespace AppTools.Helper

    Public Class JsonApiHelper
        Private log As String

        Public Sub New(Optional logString As String = Nothing)
            log = logString
        End Sub

        Public Async Function InviaJson(url As String, jsonData As String) As Task(Of HttpResponseMessageWrapper)
            Return Await EseguiInviaJson(url, jsonData)
        End Function

        Private Async Function EseguiInviaJson(url As String, jsonData As String) As Task(Of HttpResponseMessageWrapper)
            Using httpClient As New HttpClientWrapper()
                Using content As New StringContentWrapper(jsonData, Encoding.UTF8, "application/json")
                    Dim responseWrapper As HttpResponseMessageWrapper = Await httpClient.PostAsync(url, content.GetContent())
                    Return responseWrapper
                End Using
            End Using
        End Function

        Public Async Function RiceviJson(url As String) As Task(Of String)
            Return Await EseguiRiceviJson(url)
        End Function

        Private Async Function EseguiRiceviJson(url As String) As Task(Of String)
            Using httpClient As New HttpClientWrapper()
                Dim responseWrapper As HttpResponseMessageWrapper = Await httpClient.GetAsync(url)
                Return Await responseWrapper.GetContentAsStringAsync()
            End Using
        End Function

    End Class

End Namespace