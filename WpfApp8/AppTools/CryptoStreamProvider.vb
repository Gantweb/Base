Imports System.IO
Imports System.Security.Cryptography

Namespace AppTools

    Public Class CryptoStreamProvider
        Implements AppTools.Interfacce.ICryptoStreamProvider

        Public Function CreateCryptoStream(stream As Stream, transform As ICryptoTransform, mode As CryptoStreamMode) As CryptoStream Implements AppTools.Interfacce.ICryptoStreamProvider.CreateCryptoStream
            Return New CryptoStream(stream, transform, mode)
        End Function

    End Class

End Namespace