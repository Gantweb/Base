Imports System.IO
Imports System.Security.Cryptography
Imports WpfApp5.AppTools.Interfacce

Namespace AppTools

    Public Class EncryptionHelper
        Implements IEncryption, IHasher

        Private ReadOnly key As Byte()
        Private ReadOnly iv As Byte()
        Private ReadOnly _base64Helper As IBase64

        Public Sub New(password As String, base64Helper As IBase64)
            _base64Helper = base64Helper
            password = If(String.IsNullOrEmpty(password), "prova", password)

            Dim hashBytes As Byte() = ComputeHashBytes(password)
            key = hashBytes.Take(16).ToArray()
            iv = hashBytes.Skip(16).Take(16).ToArray()
        End Sub

        ' Funzione per crittografare una stringa
        Public Function Encrypt(plainText As String) As String Implements IEncryption.Encrypt
            Using aesAlg As Aes = Aes.Create()
                aesAlg.Key = key
                aesAlg.IV = iv

                Dim encryptor As ICryptoTransform = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV)
                Using msEncrypt As New MemoryStream()
                    Using csEncrypt As New CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)
                        Using swEncrypt As New StreamWriter(csEncrypt)
                            swEncrypt.Write(plainText)
                        End Using
                        Return _base64Helper.BytesToBase64(msEncrypt.ToArray())
                    End Using
                End Using
            End Using
        End Function

        ' Funzione per decrittografare una stringa
        Public Function Decrypt(cipherText As String) As String Implements IEncryption.Decrypt
            Dim cipherBytes As Byte() = _base64Helper.Base64ToBytes(cipherText)
            Using aesAlg As Aes = Aes.Create()
                aesAlg.Key = key
                aesAlg.IV = iv

                Dim decryptor As ICryptoTransform = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV)
                Using msDecrypt As New MemoryStream(cipherBytes)
                    Using csDecrypt As New CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)
                        Using srDecrypt As New StreamReader(csDecrypt)
                            Return srDecrypt.ReadToEnd()
                        End Using
                    End Using
                End Using
            End Using
        End Function

        ' Funzione per calcolare l'hash di una stringa e restituire un array di byte
        Public Function ComputeHashBytes(input As String) As Byte() Implements IHasher.ComputeHashBytes
            Using sha256 As SHA256 = SHA256.Create()
                Return sha256.ComputeHash(_base64Helper.StringToBytes(input))
            End Using
        End Function

    End Class

End Namespace