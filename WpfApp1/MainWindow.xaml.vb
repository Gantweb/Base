Imports System.IO.Compression
Imports System.Text
Imports WpfApp1.AppTools
Imports WpfApp1.AppTools.Wrapper

Class MainWindow
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        MainModule.Main3()
    End Sub

End Class

Module MainModule

    Friend Sub Main3()
        ' Scrivi un file
        'Dim writeSuccess As Boolean = AppFile.FileScrivi("example.txt", "Questo è un esempio di testo.")
        '    Console.WriteLine($"Scrittura completata: {writeSuccess}")

        '    ' Leggi un file
        '    Dim content As String = AppFile.FileLeggi("example.txt")
        '    Console.WriteLine($"Contenuto del file: {content}")

        '    ' Scrivi un file compresso
        '    Dim zipWriteSuccess As Boolean = AppFile.FileZipScrivi("example.zip", "Questo è un esempio di testo compresso.")
        '    Console.WriteLine($"Scrittura compressa completata: {zipWriteSuccess}")

        '    ' Leggi un file compresso
        '    Dim zipContent As String = AppFile.FileZipLeggi("example.zip")
        '    Console.WriteLine($"Contenuto del file compresso: {zipContent}")
    End Sub

    Sub Main2()
        Dim aa = StringZip("pippo la pappa")
        Dim bb = StringUnzip(aa)
    End Sub

    Sub Main1()
        Dim base As New AppTools.AppJsonObservable()

        ' Imposta le proprietà usando chiavi e valori codificati in Base64
        base(ToBase64String("urlsito")) = ToBase64String("https://localhost:44369")
        base(ToBase64String("isEnabled")) = ToBase64String(True)
        base(ToBase64String("piValue")) = ToBase64String(3.14)

        ' Recupera e converte automaticamente i valori
        Dim retrievedUrl As String = FromBase64ToString(base(ToBase64String("urlsito")))
        Dim retrievedBoolean As Boolean = FromBase64ToString(base(ToBase64String("isEnabled")))
        Dim retrievedDouble As Double = FromBase64ToString(base(ToBase64String("piValue")))

        ' Stampa i valori recuperati
        Debug.WriteLine($"URL recuperato: {retrievedUrl}")
        Debug.WriteLine($"Boolean recuperato: {retrievedBoolean}")
        Debug.WriteLine($"Double recuperato: {retrievedDouble * 2 + 0.5}")

        ' Visualizza l'oggetto JSON aggiornato
        Debug.WriteLine("Oggetto JSON aggiornato:")
        Debug.WriteLine(base.GetJsonObject().ToJsonString())
    End Sub

End Module
