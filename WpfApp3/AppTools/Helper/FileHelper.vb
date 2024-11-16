Imports System.IO

Namespace AppTools.Helper

    Public Class FileHelper
        Private log As String

        Public Sub New(Optional logString As String = Nothing)
            log = logString
        End Sub

        Public Sub ScriviFile(percorso As String, dati As String)
            File.WriteAllText(percorso, dati)
        End Sub

        Public Function LeggiFile(percorso As String) As String
            Return File.ReadAllText(percorso)
        End Function

        Public Sub ScriviFileCompresso(percorso As String, dati As String)
            Dim A_3 As String = New CompressionHelper(log).Comprimi(dati)
            File.WriteAllText(percorso, A_3)
        End Sub

        Public Function LeggiFileCompresso(percorso As String) As String
            Dim A_2 As String = File.ReadAllText(percorso)
            Return New CompressionHelper(log).Decomprimi(A_2)
        End Function

        Public Sub ScriviFileCompressoCriptato(percorso As String, dati As String)
            Dim A_3 As String = New CompressionHelper(log).Comprimi(dati)
            Dim A_4 As String = New CriptoHelper(log).Cripta(A_3)
            File.WriteAllText(percorso, A_4)
        End Sub

        Public Function LeggiFileCompressoCriptato(percorso As String) As String
            Dim A_2 As String = File.ReadAllText(percorso)
            Dim A_3 As String = New CriptoHelper(log).Decripta(A_2)
            Return New CompressionHelper(log).Decomprimi(A_3)
        End Function

    End Class

End Namespace