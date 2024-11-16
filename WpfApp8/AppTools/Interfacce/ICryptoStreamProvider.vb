Imports System.IO
Imports System.Security.Cryptography

Namespace AppTools.Interfacce

    Public Interface ICryptoStreamProvider

        Function CreateCryptoStream(stream As Stream, transform As ICryptoTransform, mode As CryptoStreamMode) As CryptoStream

    End Interface

End Namespace