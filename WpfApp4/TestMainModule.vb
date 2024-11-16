Module TestMainModule

    Sub Base64ModuleMain()
        Dim originalString As String = "Hello, World!"
        Dim base64String As String = AppTools.StringToBase64(originalString)
        Debug.WriteLine("Base64: " & base64String)

        Dim decodedString As String = AppTools.Base64ToString(base64String)
        Debug.WriteLine("Decoded: " & decodedString)

        Dim originalBytes As Byte() = AppTools.StringToBytes(originalString)
        Dim base64Bytes As String = AppTools.BytesToBase64(originalBytes)
        Debug.WriteLine("Base64 Bytes: " & base64Bytes)

        Dim decodedBytes As Byte() = AppTools.Base64ToBytes(base64Bytes)
        Debug.WriteLine("Decoded Bytes: " & AppTools.BytesToString(decodedBytes))
    End Sub

    Sub AppJsonObservableMain()
        Dim sample As New MVVM.AppJsonObservable()
        sample("name") = "John Doe"
        sample("age") = 30
        sample("isactive") = True

        ' Esporta a JSON
        Dim jsonString As String = sample.ToJson()
        Debug.WriteLine("JSON: " & jsonString)

        ' Carica da JSON
        Dim newSample As New MVVM.AppJsonObservable()
        newSample.FromJson(jsonString)
        Debug.WriteLine("Loaded Name: " & newSample("name"))
        Debug.WriteLine("Loaded Age: " & newSample("age"))
        Debug.WriteLine("Loaded IsActive: " & newSample("isactive"))
    End Sub

    Sub EncryptionHelperMain()
        ' Definisce la password per la crittografia e la decrittografia
        Dim password As String = "mypassword"

        ' Crea un'istanza di EncryptionHelper
        Dim helper As New AppTools.EncryptionHelper(password)

        ' Testo da crittografare e decrittografare
        Dim plainText As String = "Questo è un testo di esempio da crittografare."

        ' Esegui la crittografia
        Dim cipherText As String = helper.Encrypt(plainText)
        Debug.WriteLine($"Testo originale: {plainText}")
        Debug.WriteLine($"Testo crittografato: {cipherText}")

        ' Esegui la decrittografia
        Dim decryptedText As String = helper.Decrypt(cipherText)
        Debug.WriteLine($"Testo decrittografato: {decryptedText}")

        ' Verifica che il testo decrittografato sia uguale al testo originale
        If plainText = decryptedText Then
            Debug.WriteLine("Il test di crittografia e decrittografia è riuscito!")
        Else
            Debug.WriteLine("Il test di crittografia e decrittografia è fallito.")
        End If

    End Sub

    Sub CompressionModuleMain()
        ' Testo da comprimere e decomprimere
        Dim originalText As String = "Questo è un testo di esempio da comprimere."

        ' Esegui la compressione
        Dim compressedText As String = AppTools.CompressString(originalText)
        Debug.WriteLine($"Testo originale: {originalText}")
        Debug.WriteLine($"Testo compresso: {compressedText}")

        ' Esegui la decompressione
        Dim decompressedText As String = AppTools.DecompressString(compressedText)
        Debug.WriteLine($"Testo decompresso: {decompressedText}")

        ' Verifica che il testo decompresso sia uguale al testo originale
        If originalText = decompressedText Then
            Debug.WriteLine("Il test di compressione e decompressione è riuscito!")
        Else
            Debug.WriteLine("Il test di compressione e decompressione è fallito.")
        End If

    End Sub

End Module