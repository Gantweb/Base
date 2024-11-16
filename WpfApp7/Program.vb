Imports WpfApp7.AppTools.Helpers
Imports WpfApp7.AppTools.Interfacce
Imports WpfApp7.Model

Module Program

    ' Crea le dipendenze necessarie
    Private ReadOnly base64Helper As IBase64 = New Base64Helper()

    Private ReadOnly hasher As IHasher = New EncryptionHelper("", base64Helper)
    Private ReadOnly compression As ICompression = New CompressionHelper(base64Helper)
    Private ReadOnly fileHandler As New FileHandler(hasher, compression, base64Helper)

    ' Crea le istanze delle classi con le dipendenze
    Public Base As New AppBase(base64Helper, hasher)

    Public Logs As New AppLog(base64Helper, fileHandler)
    Public Init As New AppConfig(base64Helper, hasher, fileHandler)

    Public Sub Imposta()
        Init("ext") = $"File {Base("nome")} (*.a01)|*.a01"
        Init("ragsoc") = $"Ragione Sociale Azienda"
        Init("indiri") = $"Indirizzo Azienda"
        Init("iban") = $"iban Azienda"
        Init("urlsito") = $"https://localhost:44369"

        Application.StartCountdown()
    End Sub

End Module

Module Test

    Sub MainBase64Module()
        ' Crea un'istanza di Base64Helper
        Dim base64Helper As IBase64 = New Base64Helper()

        ' Stringa di esempio
        Dim originalString As String = "Hello, World!"

        ' Codifica la stringa in Base64
        Dim encodedString As String = base64Helper.StringToBase64(originalString)
        Debug.WriteLine($"Stringa originale: {originalString}")
        Debug.WriteLine($"Stringa codificata in Base64: {encodedString}")

        ' Decodifica la stringa Base64
        Dim decodedString As String = base64Helper.Base64ToString(encodedString)
        Debug.WriteLine($"Stringa decodificata da Base64: {decodedString}")

        ' Verifica che la stringa decodificata sia uguale a quella originale
        If originalString = decodedString Then
            Debug.WriteLine("La decodifica è avvenuta correttamente.")
        Else
            Debug.WriteLine("La decodifica non è avvenuta correttamente.")
        End If
    End Sub

    Sub MainFileHandler()
        ' Crea le dipendenze necessarie
        Dim base64Helper As IBase64 = New Base64Helper()
        Dim hasher As IHasher = New EncryptionHelper("", base64Helper)
        Dim compression As ICompression = New CompressionHelper(base64Helper)
        Dim fileHandler As New FileHandler(hasher, compression, base64Helper)

        ' Percorso del file di test
        Dim testFilePath As String = "d:\testfile.txt"

        ' Contenuto di esempio da scrivere nel file
        Dim originalContent As String = "Questo è un testo di esempio per testare la lettura e scrittura di file."

        ' Scrivi il contenuto nel file con compressione e crittografia
        fileHandler.ReadWriteFile(testFilePath, originalContent, compress:=True, encrypt:=True)

        ' Leggi il contenuto dal file con decompressione e decrittografia
        Dim readContent As String = fileHandler.ReadWriteFile(testFilePath, content:="", compress:=True, encrypt:=True)

        ' Verifica che il contenuto letto sia uguale al contenuto originale
        If originalContent = readContent Then
            Debug.WriteLine("Il test di lettura e scrittura del file è riuscito.")
        Else
            Debug.WriteLine("Il test di lettura e scrittura del file è fallito.")
            Debug.WriteLine($"Contenuto originale: {originalContent}")
            Debug.WriteLine($"Contenuto letto: {readContent}")
        End If

        ' Pulisci il file di test
        If IO.File.Exists(testFilePath) Then
            IO.File.Delete(testFilePath)
        End If
    End Sub

End Module