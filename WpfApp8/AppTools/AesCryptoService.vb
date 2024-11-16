Imports System.Security.Cryptography
Imports WpfApp8.AppTools.Interfacce

Namespace AppTools

    Public Class AesCryptoService
        Implements IAesCryptoService

        Private aes As Aes
        Private aesKey As Byte()
        Private aesIV As Byte()

        Public Function CreateAes(Optional password As String = "prova") As Aes Implements IAesCryptoService.CreateAes
            password = If(String.IsNullOrEmpty(password), "prova", password)
            Dim A_2 As Byte() = ComputeHashBytes(password)
            aesKey = A_2.Take(32).ToArray()
            aesIV = A_2.Skip(32).Take(16).ToArray()

            aes = Aes.Create()
            aes.Key = aesKey
            aes.IV = aesIV

            Return aes
        End Function

        Public Function CreateEncryptor() As ICryptoTransform Implements IAesCryptoService.CreateEncryptor
            Return aes.CreateEncryptor(aes.Key, aes.IV)
        End Function

        Public Function CreateDecryptor() As ICryptoTransform Implements IAesCryptoService.CreateDecryptor
            Return aes.CreateDecryptor(aes.Key, aes.IV)
        End Function

        Public ReadOnly Property Key As Byte() Implements IAesCryptoService.Key
            Get
                Return aesKey
            End Get
        End Property

        Public ReadOnly Property IV As Byte() Implements IAesCryptoService.IV
            Get
                Return aesIV
            End Get
        End Property

        Private Shared Function ComputeHashBytes(input As String) As Byte()
            Dim A_2 As Byte()
            Using A_3 As SHA512 = SHA512.Create()
                A_2 = A_3.ComputeHash(_b64.StringToBytes(input))
            End Using
            Return A_2
        End Function

    End Class

End Namespace