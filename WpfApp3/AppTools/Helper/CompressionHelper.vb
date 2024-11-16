Imports System.IO.Compression
Imports WpfApp3.AppTools.Wrapper

Namespace AppTools.Helper

    Public Class CompressionHelper
        Private log As String
        Private bs64 As New Base64Helper(log)
        Private cb As Byte()

        Public Sub New(Optional logString As String = Nothing)
            log = logString
        End Sub

        Public Function Comprimi(dati As String) As String
            Return EseguiComprimi(dati)
        End Function

        Private Function EseguiComprimi(dati As String) As String
            Dim A_2 As String
            Using A_3 As New MemoryStreamWrapper()
                Using A_4 As New DeflateWrapper(A_3.GetStream(), CompressionMode.Compress)
                    Using A_5 As New StreamWriterWrapper(A_4.GetStream())
                        A_5.Write(dati)
                    End Using
                    cb = A_3.ToArray()
                    A_2 = bs64.UseBase64Delegate(cb, AddressOf bs64.EncodeToBase64)
                End Using
            End Using
            Return A_2
        End Function

        Public Function Decomprimi(datiCompressi As String) As String
            Return EseguiDecomprimi(datiCompressi)
        End Function

        Private Function EseguiDecomprimi(datiCompressi As String) As String
            Dim A_2 As String
            cb = bs64.DecodeFromBase64(datiCompressi)
            Using A_3 As New MemoryStreamWrapper(cb)
                Using A_4 As New DeflateWrapper(A_3.GetStream(), CompressionMode.Decompress)
                    Using A_5 As New StreamReaderWrapper(A_4.GetStream())
                        A_2 = A_5.ReadToEnd()
                    End Using
                End Using
            End Using
            Return A_2
        End Function

        Public Function EseguiOperazioneCritica(dati As String, comprim As Boolean) As String
            Dim A_3 As String
            If comprim Then
                A_3 = Comprimi(dati)
            Else
                A_3 = Decomprimi(dati)
            End If
            Return A_3
        End Function

        Public Delegate Function OperazioneDelegate(dati As String) As String

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
            Private parent As CompressionHelper

            Public Sub New(d As String, p As CompressionHelper)
                dato = d
                parent = p
            End Sub

            Public Function VirtualizzaOperazione() As String
                parent.CodiceInutile()
                Dim A_1 = parent.EseguiOperazioneCritica(dato, True)
                parent.CodiceInutile()
                Return A_1
            End Function

        End Class

    End Class

End Namespace