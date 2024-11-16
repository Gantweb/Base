Imports System.IO
Imports System.IO.Compression

Namespace AppTools.Wrapper

    Public Class DeflateWrapper
        Implements IDisposable

        Private deflateStream As DeflateStream

        Public Sub New(ms As MemoryStream, mode As CompressionMode)
            deflateStream = New DeflateStream(ms, mode)
        End Sub

        Public Function GetStream() As DeflateStream
            Return deflateStream
        End Function

        Public Sub Dispose() Implements IDisposable.Dispose
            deflateStream.Dispose()
        End Sub

    End Class

End Namespace