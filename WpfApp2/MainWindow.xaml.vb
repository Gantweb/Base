Imports System.Net.Http
Imports WpfApp2.AppTools.Helper
Imports WpfApp2.AppTools.Wrapper

Class MainWindow

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim aa = Base64Helper.DecodeStringFromBase64(Base("nome"))
        'Main1()
    End Sub

End Class

Module TestModule

    Sub Main1()
        ' Test per scrittura e lettura normale
        Dim filePathNormale As String = "d:\test_normale.txt"
        Dim contenutoNormale As String = "Questo è un test per la scrittura e lettura normale."
        FileHelper.ScriviFile(filePathNormale, contenutoNormale)
        Debug.WriteLine("Lettura normale: " & FileHelper.LeggiFile(filePathNormale))

        ' Test per scrittura e lettura con compressione
        Dim filePathCompresso As String = "d:\test_compresso.txt"
        Dim contenutoCompresso As String = "Questo è un test per la scrittura e lettura con compressione."
        FileHelper.ScriviFileCompresso(filePathCompresso, contenutoCompresso)
        Debug.WriteLine("Lettura compressa: " & FileHelper.LeggiFileCompresso(filePathCompresso))

        ' Test per scrittura e lettura con compressione e crittografia
        Dim filePathCompressoCriptato As String = "d:\test_compresso_criptato.txt"
        Dim contenutoCompressoCriptato As String = "Questo è un test per la scrittura e lettura con compressione e crittografia."
        FileHelper.ScriviFileCompressoCriptato(filePathCompressoCriptato, contenutoCompressoCriptato)
        Debug.WriteLine("Lettura compressa e criptata: " & FileHelper.LeggiFileCompressoCriptato(filePathCompressoCriptato))
    End Sub

    Async Function Main2() As Task
        ' URL dell'API per il test
        Dim apiUrl As String = "https://jsonplaceholder.typicode.com/posts"

        ' Dati da inviare in formato JSON
        Dim jsonData As String = "{""title"": ""foo"", ""body"": ""bar"", ""userId"": 1}"

        ' Test invio dati JSON
        Dim response As HttpResponseMessage = Await JsonApiHelper.InviaJson(apiUrl, jsonData)
        Dim responseData As String = Await response.Content.ReadAsStringAsync()
        Debug.WriteLine("Risposta invio JSON: " & responseData)

        ' Test ricezione dati JSON
        Dim receivedJson As String = Await JsonApiHelper.RiceviJson(apiUrl)
        Debug.WriteLine("Dati ricevuti JSON: " & receivedJson)

    End Function

End Module