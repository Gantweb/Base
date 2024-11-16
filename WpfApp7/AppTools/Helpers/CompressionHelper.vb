Imports System.IO
Imports System.IO.Compression
Imports AppTools.Interfacce
Imports WpfApp7.AppTools.Interfacce

Namespace AppTools.Helpers

    Public Class CompressionHelper
        Implements ICompression

        Private ReadOnly _base64Helper As IBase64

        Public Sub New(base64Helper As IBase64)
            _base64Helper = base64Helper
        End Sub

        Public Function CompressString(input As String) As String Implements ICompression.CompressString
            Dim inputBytes As Byte() = _base64Helper.StringToBytes(input)
            Using outputStream As New MemoryStream()
                Using compressionStream As New GZipStream(outputStream, CompressionMode.Compress)
                    compressionStream.Write(inputBytes, 0, inputBytes.Length)
                End Using
                Return _base64Helper.BytesToBase64(outputStream.ToArray())
            End Using
        End Function

        Public Function DecompressString(compressed As String) As String Implements ICompression.DecompressString
            Dim compressedBytes As Byte() = _base64Helper.Base64ToBytes(compressed)
            Using inputStream As New MemoryStream(compressedBytes)
                Using decompressionStream As New GZipStream(inputStream, CompressionMode.Decompress)
                    Using outputStream As New MemoryStream()
                        decompressionStream.CopyTo(outputStream)
                        Return _base64Helper.BytesToString(outputStream.ToArray())
                    End Using
                End Using
            End Using
        End Function

    End Class

End Namespace
