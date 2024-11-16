Imports System.IO
Imports System.IO.Compression
Imports WpfApp5.AppTools.Interfacce

Namespace AppTools

    Public Class CompressionHelper
        Implements ICompression

        Private ReadOnly _base64Helper As IBase64

        Public Sub New(base64Helper As IBase64)
            _base64Helper = base64Helper
        End Sub

        ' Funzione per comprimere una stringa utilizzando GZipStream con il livello di compressione massimo
        Public Function CompressString(input As String) As String Implements ICompression.CompressString
            Dim bytes As Byte() = _base64Helper.StringToBytes(input)
            Using ms As New MemoryStream()
                Using gzip As New GZipStream(ms, CompressionLevel.SmallestSize)
                    gzip.Write(bytes, 0, bytes.Length)
                End Using
                Return _base64Helper.BytesToBase64(ms.ToArray())
            End Using
        End Function

        ' Funzione per decomprimere una stringa utilizzando GZipStream
        Public Function DecompressString(input As String) As String Implements ICompression.DecompressString
            Dim bytes As Byte() = _base64Helper.Base64ToBytes(input)
            Using ms As New MemoryStream(bytes)
                Using gzip As New GZipStream(ms, CompressionMode.Decompress)
                    Using output As New MemoryStream()
                        gzip.CopyTo(output)
                        Return _base64Helper.BytesToString(output.ToArray())
                    End Using
                End Using
            End Using
        End Function

    End Class

End Namespace