Imports System.Security.Cryptography

Namespace AppTools.Wrapper

    Public Class AesWrapper
        Private aesAlg As Aes

        Public Sub New()
            aesAlg = Aes.Create()
        End Sub

        Public Function CreateEncryptor(key As Byte(), iv As Byte()) As ICryptoTransform
            aesAlg.Key = key
            aesAlg.IV = iv
            Return aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV)
        End Function

        Public Function CreateDecryptor(key As Byte(), iv As Byte()) As ICryptoTransform
            aesAlg.Key = key
            aesAlg.IV = iv
            Return aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV)
        End Function

    End Class

End Namespace