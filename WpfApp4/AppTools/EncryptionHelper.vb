Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports AppTools.Base64Module

Namespace AppTools
    Public Class EncryptionHelper

        ' Chiave e IV per la crittografia e la decrittografia
        Private ReadOnly key As Byte()
        Private ReadOnly iv As Byte()

        Public Sub New(password As String)
            ' Imposta la password a "prova" se è vuota utilizzando l'operatore a punto interrogativo
            password = If(String.IsNullOrEmpty(password), "prova", password)

            ' Calcola la chiave e l'IV dalla stringa passata utilizzando SHA256
            Dim hashBytes As Byte() = ComputeHashBytes(password)

            ' Utilizza i primi 16 byte del hash come chiave
            key = hashBytes.Take(16).ToArray()
            ' Utilizza i secondi 16 byte del hash come IV
            iv = hashBytes.Skip(16).Take(16).ToArray()
        End Sub

        ' Funzione per crittografare una stringa
        Public Function Encrypt(plainText As String) As String
            Using aesAlg As Aes = Aes.Create()
                aesAlg.Key = key
                aesAlg.IV = iv

                Dim encryptor As ICryptoTransform = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV)
                Using msEncrypt As New MemoryStream()
                    Using csEncrypt As New CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)
                        Using swEncrypt As New StreamWriter(csEncrypt)
                            swEncrypt.Write(plainText)
                        End Using
                        Return BytesToBase64(msEncrypt.ToArray())
                    End Using
                End Using
            End Using
        End Function

        ' Funzione per decrittografare una stringa
        Public Function Decrypt(cipherText As String) As String
            Dim cipherBytes As Byte() = Base64ToBytes(cipherText)
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

        ' Funzione shared per calcolare l'hash di una stringa
        Public Shared Function ComputeHash(input As String) As String
            Return BytesToBase64(ComputeHashBytes(input))
        End Function

        ' Funzione shared per calcolare l'hash di una stringa e restituire un array di byte
        Public Shared Function ComputeHashBytes(input As String) As Byte()
            Using sha256 As SHA256 = SHA256.Create()
                Return sha256.ComputeHash(Encoding.UTF8.GetBytes(input))
            End Using
        End Function

    End Class
End Namespace
