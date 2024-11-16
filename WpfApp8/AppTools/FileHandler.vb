Imports System.IO
Imports System.Text.Json
Imports WpfApp8.AppTools.Interfacce

Namespace AppTools

    Public Class FileHandler
        Implements IFileHandler

        Public Sub New()
        End Sub

        ' Metodo per leggere e scrivere file con opzioni di compressione e crittografia
        Public Function ReadWriteFile(Of T)(filePath As String, content As Object, Optional compress As Boolean = False, Optional encrypt As Boolean = False) As T Implements IFileHandler.ReadWriteFile
            If content IsNot Nothing AndAlso Not String.IsNullOrEmpty(content.ToString()) Then
                ' Serializza l'oggetto in una stringa JSON
                Dim jsonString As String = JsonSerializer.Serialize(content)

                ' Applica la compressione se richiesta
                If compress Then
                    jsonString = _compressor.Compress(jsonString)
                End If

                ' Applica la crittografia se richiesta
                If encrypt Then
                    jsonString = _encryptor.Encrypt(jsonString)
                End If

                ' Scrive il contenuto nel file
                File.WriteAllText(filePath, jsonString)
                Return Nothing
            Else
                ' Legge il contenuto dal file
                Dim fileContent As String = File.ReadAllText(filePath)

                ' Applica la decrittazione se necessario
                If encrypt Then
                    fileContent = _encryptor.Decrypt(fileContent)
                End If

                ' Applica la decompressione se necessario
                If compress Then
                    fileContent = _compressor.Decompress(fileContent)
                End If

                ' Deserializza il contenuto letto in un oggetto di tipo T
                Return JsonSerializer.Deserialize(Of T)(fileContent)
            End If
        End Function









        'Public Function ReadWriteFile(filePath As String, content As String, Optional compress As Boolean = False, Optional encrypt As Boolean = False) As String Implements IFileHandler.ReadWriteFile
        '    Return ReadWriteFile(filePath, JsonSerializer.Serialize(content), compress, encrypt)
        'End Function
        'Public Function ReadWriteFileOb(Of T)(filePath As String, content As Object, Optional compress As Boolean = False, Optional encrypt As Boolean = False) As String Implements IFileHandler.ReadWriteFileOb
        '    Dim tmp = ReadWriteFile(filePath, "", compress, encrypt)
        '    Return JsonSerializer.Deserialize(tmp)
        'End Function

        'Public Function ReadWriteFile(filePath As String, content As String, Optional compress As Boolean = False, Optional encrypt As Boolean = False) As String Implements IFileHandler.ReadWriteFile
        '    If Not String.IsNullOrEmpty(content) Then
        '        If compress Then
        '            content = _compressor.Compress(content)
        '        End If

        '        If encrypt Then
        '            content = _encryptor.Encrypt(content)
        '        End If

        '        File.WriteAllText(filePath, content)
        '        Return "File saved successfully."
        '    Else
        '        Dim fileContent As String = File.ReadAllText(filePath)

        '        If encrypt Then
        '            fileContent = _encryptor.Decrypt(fileContent)
        '        End If

        '        If compress Then
        '            fileContent = _compressor.Decompress(fileContent)
        '        End If

        '        Return fileContent
        '    End If
        'End Function

    End Class

End Namespace