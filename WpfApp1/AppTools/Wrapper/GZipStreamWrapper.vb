Imports System.IO
Imports System.IO.Compression

Namespace AppTools.Wrapper
    Friend Class GZipStreamWrapper
        Inherits GZipStream

        Public Sub New(stream As Stream, compressionMode As CompressionMode)
            MyBase.New(stream, compressionMode)
        End Sub

        Public Sub Scrivi(buffer As Byte())
            MyBase.Write(buffer, 0, buffer.Length)
        End Sub

        Public Sub CopyTos(destination As Stream)
            MyBase.CopyTo(destination)
        End Sub
    End Class
End Namespace
