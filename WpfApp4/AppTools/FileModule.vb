Imports System.IO

Namespace AppTools
    Module FileModule

        ' Funzione per leggere e scrivere un file con opzioni di compressione e crittografia
        Public Function ReadWriteFile(filePath As String, content As String, Optional compress As Boolean = False, Optional encrypt As Boolean = False, Optional password As String = "") As String
            Dim result As String

            If Not String.IsNullOrEmpty(content) Then
                ' Scrittura del file
                If compress Then
                    content = CompressString(content)
                End If

                If encrypt Then
                    Dim encryptionHelper As New EncryptionHelper(password)
                    content = encryptionHelper.Encrypt(content)
                End If

                WriteFile(filePath, content)
                result = "File scritto con successo."
            Else
                ' Lettura del file
                result = ReadFile(filePath)

                If encrypt Then
                    Dim encryptionHelper As New EncryptionHelper(password)
                    result = encryptionHelper.Decrypt(result)
                End If

                If compress Then
                    result = DecompressString(result)
                End If
            End If

            Return result
        End Function

        ' Funzione per leggere il contenuto di un file
        Private Function ReadFile(filePath As String) As String
            If Not File.Exists(filePath) Then
                Throw New FileNotFoundException($"Il file '{filePath}' non è stato trovato.")
            End If
            Return File.ReadAllText(filePath)
        End Function

        ' Funzione per scrivere il contenuto in un file
        Private Sub WriteFile(filePath As String, content As String)
            File.WriteAllText(filePath, content)
        End Sub

    End Module
End Namespace