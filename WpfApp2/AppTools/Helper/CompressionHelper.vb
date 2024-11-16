Imports System.IO.Compression
Imports WpfApp2.AppTools.Wrapper

Namespace AppTools.Helper

    Public Class CompressionHelper

        ' Wrapper per la compressione
        Public Shared Function Comprimi(dati As String) As String
            Return EseguiComprimi(dati)
        End Function

        Private Shared Function EseguiComprimi(dati As String) As String
            Using msCompressWrapper As New MemoryStreamWrapper()
                Using dsCompressWrapper As New DeflateWrapper(msCompressWrapper.GetStream(), CompressionMode.Compress)
                    Using swCompressWrapper As New StreamWriterWrapper(dsCompressWrapper.GetStream())
                        swCompressWrapper.Write(dati)
                    End Using
                    Dim compressedBytes As Byte() = msCompressWrapper.ToArray()
                    Return Base64Helper.UseBase64Delegate(compressedBytes, AddressOf Base64Helper.EncodeToBase64)
                End Using
            End Using
        End Function

        ' Wrapper per la decompressione
        Public Shared Function Decomprimi(datiCompressi As String) As String
            Return EseguiDecomprimi(datiCompressi)
        End Function

        Private Shared Function EseguiDecomprimi(datiCompressi As String) As String
            Dim compressedBytes As Byte() = Base64Helper.DecodeFromBase64(datiCompressi)
            Using msDecompressWrapper As New MemoryStreamWrapper(compressedBytes)
                Using dsDecompressWrapper As New DeflateWrapper(msDecompressWrapper.GetStream(), CompressionMode.Decompress)
                    Using srDecompressWrapper As New StreamReaderWrapper(dsDecompressWrapper.GetStream())
                        Return srDecompressWrapper.ReadToEnd()
                    End Using
                End Using
            End Using
        End Function

        ' Metodo polimorfico per eseguire operazioni di compressione/decompressione
        Public Shared Function EseguiOperazioneCritica(dati As String, comprim As Boolean) As String
            If comprim Then
                Return Comprimi(dati)
            Else
                Return Decomprimi(dati)
            End If
        End Function

        ' Metodo per l'uso del delegate
        Public Delegate Function OperazioneDelegate(dati As String) As String

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
                CompressionHelper.CodiceInutile()
                Dim risultato = CompressionHelper.EseguiOperazioneCritica(dato, True)
                CompressionHelper.CodiceInutile()
                Return risultato
            End Function

        End Class

    End Class

End Namespace