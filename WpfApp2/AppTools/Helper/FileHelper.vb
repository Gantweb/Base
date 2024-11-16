Imports System.IO

Namespace AppTools.Helper

    Public Class FileHelper

        ' Scrive dati normali su un file
        Public Shared Sub ScriviFile(percorso As String, dati As String)
            File.WriteAllText(percorso, dati)
        End Sub

        ' Legge dati normali da un file
        Public Shared Function LeggiFile(percorso As String) As String
            Return File.ReadAllText(percorso)
        End Function

        ' Scrive dati compressi su un file
        Public Shared Sub ScriviFileCompresso(percorso As String, dati As String)
            Dim datiCompressi As String = CompressionHelper.Comprimi(dati)
            File.WriteAllText(percorso, datiCompressi)
        End Sub

        ' Legge dati compressi da un file
        Public Shared Function LeggiFileCompresso(percorso As String) As String
            Dim datiCompressi As String = File.ReadAllText(percorso)
            Return CompressionHelper.Decomprimi(datiCompressi)
        End Function

        ' Scrive dati compressi e crittografati su un file
        Public Shared Sub ScriviFileCompressoCriptato(percorso As String, dati As String)
            Dim datiCompressi As String = CompressionHelper.Comprimi(dati)
            Dim datiCriptati As String = CriptoHelper.Cripta(datiCompressi)
            File.WriteAllText(percorso, datiCriptati)
        End Sub

        ' Legge dati compressi e crittografati da un file
        Public Shared Function LeggiFileCompressoCriptato(percorso As String) As String
            Dim datiCriptati As String = File.ReadAllText(percorso)
            Dim datiCompressi As String = CriptoHelper.Decripta(datiCriptati)
            Return CompressionHelper.Decomprimi(datiCompressi)
        End Function

    End Class

End Namespace