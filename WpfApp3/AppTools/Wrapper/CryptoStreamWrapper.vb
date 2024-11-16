Imports System.IO
Imports System.Security.Cryptography

Namespace AppTools.Wrapper

    Public Class CryptoStreamWrapper
        Implements IDisposable

        Private cs As CryptoStream

        Public Sub New(ms As MemoryStream, transform As ICryptoTransform, mode As CryptoStreamMode)
            cs = New CryptoStream(ms, transform, mode)
        End Sub

        Public Function GetStream() As CryptoStream
            Return cs
        End Function

        Public Sub Dispose() Implements IDisposable.Dispose
            cs.Dispose()
        End Sub

    End Class

End Namespace