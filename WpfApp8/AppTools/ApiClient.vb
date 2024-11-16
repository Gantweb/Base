Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text
Imports System.Text.Json
Imports WpfApp8.AppTools.Interfacce

Namespace AppTools

    Public Class ApiClient
        Implements IApiClient

        Private ReadOnly httpClient As HttpClient
        Private ReadOnly stri As IStringEncryptor

        Public Sub New(Optional password As String = "prova")
            httpClient = New HttpClient()
            httpClient.DefaultRequestHeaders.Accept.Clear()
            httpClient.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            Dim aesServi As New AesCryptoService()
            stri = New StringEncryptor(aesServi, Init("assembly"))
        End Sub

        Public Async Function GetAsync(Of T)(url As String) As Task(Of T) Implements IApiClient.GetAsync
            Dim response = Await httpClient.GetAsync(url)
            response.EnsureSuccessStatusCode()

            Return Await RispostaAsync(Of T)(response)
        End Function

        Public Async Function PostAsync(Of T)(url As String, jsonData As String) As Task(Of String) Implements IApiClient.PostAsync
            Dim encryptedData As String = stri.Encrypt(jsonData)
            Dim content = New StringContent(encryptedData, Encoding.UTF8, "application/json")

            Dim response = Await httpClient.PostAsync(url, content)
            response.EnsureSuccessStatusCode()

            Return Await RispostaAsync(Of String)(response)
        End Function

        Private Async Function RispostaAsync(Of T)(response As HttpResponseMessage) As Task(Of T)
            Dim responseJsonString = Await response.Content.ReadAsStringAsync()
            Dim decryptedResponse As String = stri.Decrypt(responseJsonString)
            Return JsonSerializer.Deserialize(Of T)(decryptedResponse)
        End Function

    End Class

End Namespace