Imports System.IO
Imports AppTools.Interfacce
Imports WpfApp7.AppTools.Interfacce

Namespace AppTools.Helpers

    Public Class FileHandler

        Private ReadOnly _hasher As IHasher
        Private ReadOnly _compression As ICompression
        Private ReadOnly _base64Helper As IBase64

        Public Sub New(hasher As IHasher, compression As ICompression, base64Helper As IBase64)
            _hasher = hasher
            _compression = compression
            _base64Helper = base64Helper
        End Sub

        Public Function ReadWriteFile(filePath As String, content As String, compress As Boolean, encrypt As Boolean) As String
            If String.IsNullOrEmpty(content) Then
                ' Read mode
                Dim fileContent As Byte() = File.ReadAllBytes(filePath)
                If encrypt Then
                    fileContent = _base64Helper.Base64ToBytes(content)
                End If
                If compress Then
                    Return _compression.DecompressString(_base64Helper.BytesToString(fileContent))
                End If
                Return _base64Helper.BytesToString(fileContent)
            Else
                ' Write mode
                Dim fileContent As Byte() = _base64Helper.StringToBytes(content)
                If compress Then
                    content = _compression.CompressString(content)
                End If
                If encrypt Then
                    fileContent = _base64Helper.Base64ToBytes(content)
                End If
                File.WriteAllBytes(filePath, fileContent)
                Return content
            End If
        End Function

    End Class

End Namespace
