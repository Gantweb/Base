Imports System.Security.Cryptography
Imports System.Text
Imports WpfApp2.AppTools.Wrapper

Namespace AppTools.Helper

    Public Class CriptoHelper
        Private Shared ReadOnly Ck As Byte() = Encoding.UTF8.GetBytes("ChiaveSegreta123")
        Private Shared ReadOnly Iv As Byte() = Encoding.UTF8.GetBytes("InizialVectoreIV")

        Public Delegate Function OperazioneDelegate(dati As String) As String

        ' Wrapper per la crittografia
        Public Shared Function Cripta(dati As String) As String
            Return EseguiCripta(dati)
        End Function

        Private Shared Function EseguiCripta(dati As String) As String
            Dim aesWrapper As New AesWrapper()
            Dim encryptor = aesWrapper.CreateEncryptor(Ck, Iv)

            Using msEncryptWrapper As New MemoryStreamWrapper()
                Using csEncryptWrapper As New CryptoStreamWrapper(msEncryptWrapper.GetStream(), encryptor, CryptoStreamMode.Write)
                    Using swEncryptWrapper As New StreamWriterWrapper(csEncryptWrapper.GetStream())
                        swEncryptWrapper.Write(dati)
                    End Using
                    Dim encryptedBytes As Byte() = msEncryptWrapper.ToArray()
                    Return Base64Helper.UseBase64Delegate(encryptedBytes, AddressOf Base64Helper.EncodeToBase64)
                End Using
            End Using
        End Function

        ' Wrapper per la decrittografia
        Public Shared Function Decripta(datiCifrati As String) As String
            Return EseguiDecripta(datiCifrati)
        End Function

        Private Shared Function EseguiDecripta(datiCifrati As String) As String
            Dim encryptedBytes As Byte() = Base64Helper.DecodeFromBase64(datiCifrati)
            Dim aesWrapper As New AesWrapper()
            Dim decryptor = aesWrapper.CreateDecryptor(Ck, Iv)

            Using msDecryptWrapper As New MemoryStreamWrapper(encryptedBytes)
                Using csDecryptWrapper As New CryptoStreamWrapper(msDecryptWrapper.GetStream(), decryptor, CryptoStreamMode.Read)
                    Using srDecryptWrapper As New StreamReaderWrapper(csDecryptWrapper.GetStream())
                        Return srDecryptWrapper.ReadToEnd()
                    End Using
                End Using
            End Using
        End Function

        ' Metodo polimorfico per eseguire operazioni crittografiche
        Public Shared Function EseguiOperazioneCritica(dati As String, crittografa As Boolean) As String
            If crittografa Then
                Return Cripta(dati)
            Else
                Return Decripta(dati)
            End If
        End Function

        ' Metodo per l'uso del delegate
        Public Shared Function UsaDelegate(dati As String, operazione As OperazioneDelegate) As String
            Return operazione(dati)
        End Function

        ' Metodo offuscato per generare codice inutile
        Public Shared Sub CodiceInutile()
            Dim x As Integer = 0
            For i As Integer = 0 To 100
                x += i
            Next
        End Sub

        ' Virtualizzazione del Codice con un layer di offuscamento
        Public Class ObfuscationLayer
            Private dato As String

            Public Sub New(d As String)
                dato = d
            End Sub

            Public Function VirtualizzaOperazione() As String
                CriptoHelper.CodiceInutile()
                Dim risultato = CriptoHelper.EseguiOperazioneCritica(dato, True)
                CriptoHelper.CodiceInutile()
                Return risultato
            End Function

        End Class

    End Class

End Namespace