Imports System.Security.Cryptography

Namespace AppTools.Interfacce

    Public Interface IAesCryptoService

        Function CreateAes(Optional password As String = "prova") As Aes

        Function CreateEncryptor() As ICryptoTransform

        Function CreateDecryptor() As ICryptoTransform

        ReadOnly Property Key As Byte()
        ReadOnly Property IV As Byte()
    End Interface

End Namespace