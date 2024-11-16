Imports System.IO.Compression
Imports WpfApp8.AppTools.Interfacce

Namespace AppTools

    Public Class StringCompressor
        Implements IStringCompressor

        Public Function Compress(input As String) As String Implements IStringCompressor.Compress
            Dim inputBytes As Byte() = _b64.StringToBytes(input)
            Using outputStream = _memoryStreamProvider.CreateMemoryStream()
                Using gzipStream As New GZipStream(outputStream, CompressionLevel.SmallestSize)
                    gzipStream.Write(inputBytes, 0, inputBytes.Length)
                End Using
                Return _b64.BytesToBase64(outputStream.ToArray())
            End Using
        End Function

        Public Function Decompress(compressed As String) As String Implements IStringCompressor.Decompress
            Dim compressedBytes As Byte() = _b64.Base64ToBytes(compressed)

            Using inputStream = _memoryStreamProvider.CreateMemoryStream(compressedBytes)
                Using gzipStream As New GZipStream(inputStream, CompressionMode.Decompress)
                    Using outputStream = _memoryStreamProvider.CreateMemoryStream()
                        gzipStream.CopyTo(outputStream)
                        Dim outputBytes As Byte() = outputStream.ToArray()
                        Return _b64.BytesToString(outputBytes)
                    End Using
                End Using
            End Using
        End Function

    End Class

End Namespace