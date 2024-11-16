Imports System.IO
Imports System.IO.Compression

Namespace AppTools
    Module CompressionModule

        ' Funzione per comprimere una stringa utilizzando GZipStream con il livello di compressione massimo
        Public Function CompressString(input As String) As String
            Dim bytes As Byte() = StringToBytes(input)
            Using ms As New MemoryStream()
                Using gzip As New GZipStream(ms, CompressionLevel.SmallestSize)
                    gzip.Write(bytes, 0, bytes.Length)
                End Using
                Return BytesToBase64(ms.ToArray())
            End Using
        End Function

        ' Funzione per decomprimere una stringa utilizzando GZipStream
        Public Function DecompressString(input As String) As String
            Dim bytes As Byte() = Base64ToBytes(input)
            Using ms As New MemoryStream(bytes)
                Using gzip As New GZipStream(ms, CompressionMode.Decompress)
                    Using output As New MemoryStream()
                        gzip.CopyTo(output)
                        Return BytesToString(output.ToArray())
                    End Using
                End Using
            End Using
        End Function

    End Module
End Namespace