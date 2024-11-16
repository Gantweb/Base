Imports System.Security.Cryptography
Imports WpfApp3.AppTools.Wrapper

Namespace AppTools.Helper

    Public Class CriptoHelper
        Private ReadOnly Ck As Byte()
        Private ReadOnly Iv As Byte()
        Private bs64 As Base64Helper
        Private hash As Byte()
        Private encryptedBytes As Byte()
        Private encryptor As ICryptoTransform
        Private decryptor As ICryptoTransform
        Private aes As New AesWrapper()


        Public Delegate Function OperazioneDelegate(dati As String) As String

        Public Sub New(Optional log As String = Nothing)
            bs64 = New Base64Helper(log)
            If String.IsNullOrEmpty(log) Then
                log = bs64.DecodeStringFromBase64(Base("nome"))
            End If
            Dim A_2 As New SHA256Wrapper()
            hash = A_2.ComputeHash(log)
            Ck = hash.Take(16).ToArray()
            Iv = hash.Skip(16).Take(16).ToArray()
        End Sub

        Public Function Cripta(dati As String) As String
            Return EseguiCripta(dati)
        End Function

        Private Function EseguiCripta(dati As String) As String
            encryptor = aes.CreateEncryptor(Ck, Iv)
            Dim A_2 As String
            Using A_3 As New MemoryStreamWrapper()
                Using A_4 As New CryptoStreamWrapper(A_3.GetStream(), encryptor, CryptoStreamMode.Write)
                    Using A_5 As New StreamWriterWrapper(A_4.GetStream())
                        A_5.Write(dati)
                    End Using
                    encryptedBytes = A_3.ToArray()
                    A_2 = bs64.UseBase64Delegate(encryptedBytes, AddressOf bs64.EncodeToBase64)
                End Using
            End Using
            Return A_2
        End Function

        Public Function Decripta(datiCifrati As String) As String
            Return EseguiDecripta(datiCifrati)
        End Function

        Private Function EseguiDecripta(datiCifrati As String) As String
            encryptedBytes = bs64.DecodeFromBase64(datiCifrati)
            decryptor = Aes.CreateDecryptor(Ck, Iv)
            Dim A_2 As String
            Using A_3 As New MemoryStreamWrapper(encryptedBytes)
                Using A_4 As New CryptoStreamWrapper(A_3.GetStream(), decryptor, CryptoStreamMode.Read)
                    Using A_5 As New StreamReaderWrapper(A_4.GetStream())
                        A_2 = A_5.ReadToEnd()
                    End Using
                End Using
            End Using
            Return A_2
        End Function

        Public Function EseguiOperazioneCritica(dati As String, crittografa As Boolean) As String
            Dim A_3 As String
            If crittografa Then
                A_3 = Cripta(dati)
            Else
                A_3 = Decripta(dati)
            End If
            Return A_3
        End Function

        Public Function UsaDelegate(dati As String, operazione As OperazioneDelegate) As String
            Return operazione(dati)
        End Function

        Public Sub CodiceInutile()
            Dim x As Integer = 0
            For i As Integer = 0 To 100
                x += i
            Next
        End Sub

        Public Class ObfuscationLayer
            Private dato As String
            Private parent As CriptoHelper

            Public Sub New(d As String, p As CriptoHelper)
                dato = d
                parent = p
            End Sub

            Public Function VirtualizzaOperazione() As String
                parent.CodiceInutile()
                Dim risultato = parent.EseguiOperazioneCritica(dato, True)
                parent.CodiceInutile()
                Return risultato
            End Function

        End Class

    End Class

End Namespace