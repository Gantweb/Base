Imports System.IO
Imports System.Security.Cryptography

Namespace AppTools

    Public Class StringEncryptor
        Implements AppTools.Interfacce.IStringEncryptor

        Private ReadOnly aesService As AppTools.Interfacce.IAesCryptoService
        Private ReadOnly password As String
        Private result As String

        Public Sub New(aesService As AppTools.Interfacce.IAesCryptoService, Optional password As String = "prova")
            Me.aesService = aesService
            Me.password = password
        End Sub

        Public Function Encrypt(input As String) As String Implements AppTools.Interfacce.IStringEncryptor.Encrypt
            result = ""
            Dim aes As Aes = aesService.CreateAes(password)
            Using A_2 As ICryptoTransform = aesService.CreateEncryptor()
                Dim A_3 As Byte() = _b64.StringToBytes(input)
                Using A_4 = _memoryStreamProvider.CreateMemoryStream()
                    Using A_5 = _cryptoStreamProvider.CreateCryptoStream(A_4, A_2, CryptoStreamMode.Write)
                        A_5.Write(A_3, 0, A_3.Length)
                        A_5.FlushFinalBlock()
                        result = _b64.BytesToBase64(A_4.ToArray())
                    End Using
                End Using
            End Using
            Return result
        End Function

        Public Function Decrypt(encrypted As String) As String Implements AppTools.Interfacce.IStringEncryptor.Decrypt
            result = ""
            Dim aes As Aes = aesService.CreateAes(password)
            Using A_1 As ICryptoTransform = aesService.CreateDecryptor()
                Dim A_2 As Byte() = _b64.Base64ToBytes(encrypted)
                Using A_3 = _memoryStreamProvider.CreateMemoryStream(A_2)
                    Using A_4 = _cryptoStreamProvider.CreateCryptoStream(A_3, A_1, CryptoStreamMode.Read)
                        Using A_5 As New StreamReader(A_4)
                            result = A_5.ReadToEnd()
                        End Using
                    End Using
                End Using
            End Using
            Return result
        End Function

    End Class

End Namespace