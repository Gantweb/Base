Imports System.IO
Imports WpfApp6.AppTools.Interfacce

Namespace AppTools

    Public Class FileHandler
        Private ReadOnly _encryption As IEncryption
        Private ReadOnly _compression As ICompression
        Private ReadOnly _base64Helper As IBase64

        Public Sub New(encryption As IEncryption, compression As ICompression, base64Helper As IBase64)
            _encryption = encryption
            _compression = compression
            _base64Helper = base64Helper
        End Sub

        ' Funzione per leggere e scrivere un file con opzioni di compressione e crittografia
        Public Function ReadWriteFile(filePath As String, content As String, Optional compress As Boolean = False, Optional encrypt As Boolean = False) As String
            Dim result As String

            If Not String.IsNullOrEmpty(content) Then
                ' Scrittura del file
                If compress Then
                    content = _compression.CompressString(content)
                End If

                If encrypt Then
                    content = _encryption.Encrypt(content)
                End If

                WriteFile(filePath, content)
                result = "File scritto con successo."
            Else
                ' Lettura del file
                result = ReadFile(filePath)

                If encrypt Then
                    result = _encryption.Decrypt(result)
                End If

                If compress Then
                    result = _compression.DecompressString(result)
                End If
            End If

            Return result
        End Function

        ' Funzione per leggere il contenuto di un file
        Private Function ReadFile(filePath As String) As String
            If Not File.Exists(filePath) Then
                Throw New FileNotFoundException($"Il file '{filePath}' non è stato trovato.")
            End If
            Return _base64Helper.BytesToString(File.ReadAllBytes(filePath))
        End Function

        ' Funzione per scrivere il contenuto in un file
        Private Sub WriteFile(filePath As String, content As String)
            File.WriteAllBytes(filePath, _base64Helper.StringToBytes(content))
        End Sub

    End Class

End Namespace