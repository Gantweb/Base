Imports System.IO

Namespace AppTools.Wrapper

    Public Class MemoryStreamWrapper
        Implements IDisposable

        Private ms As MemoryStream

        Public Sub New()
            ms = New MemoryStream()
        End Sub

        Public Sub New(buffer As Byte())
            ms = New MemoryStream(buffer)
        End Sub

        Public Function GetStream() As MemoryStream
            Return ms
        End Function

        Public Function ToArray() As Byte()
            Return ms.ToArray()
        End Function

        Public Sub Dispose() Implements IDisposable.Dispose
            ms.Dispose()
        End Sub

    End Class

End Namespace