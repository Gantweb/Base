Imports DocumentFormat.OpenXml.Wordprocessing
Imports WpfApp8.AppTools
Imports WpfApp8.AppTools.Docx
Imports WpfApp8.AppTools.Interfacce
Imports WpfApp8.Model.App

Public Module Program

    Public ReadOnly _hasher As IHasher = New Hasher()
    Public ReadOnly _b64 As IBase64Util = New Base64Util()
    Public ReadOnly _memoryStreamProvider As IMemoryStreamProvider = New MemoryStreamProvider()
    Public ReadOnly _cryptoStreamProvider As ICryptoStreamProvider = New CryptoStreamProvider()

    Public ReadOnly Base As New AppBase()

    Private ReadOnly aesService As New AesCryptoService()
    Public ReadOnly _compressor As New StringCompressor()
    Public ReadOnly _encryptor As New StringEncryptor(aesService, Base("nome"))

    Public ReadOnly Init As New AppConfig()
    Public ReadOnly Logs As New AppLog()

    Public Sub Imposta()
        'Mains()

        Init("ext") = $"File {Base("nome")} (*.a01)|*.a01"
        Init("ragsoc") = $"Ragione Sociale Azienda"
        Init("indiri") = $"Indirizzo Azienda"
        Init("iban") = $"iban Azienda"
        Init("urlsito") = $"https://localhost:44369"

        Application.StartCountdown()
    End Sub

End Module



Module Program1
    Sub Mains()
        ' Percorso del file DOCX
        Dim filePath As String = "d:\EsempioDoc.docx"

        ' Crea il documento con formato A4, orientamento verticale e margini personalizzati
        Dim doc As New Docx.AgaDocx(filePath, Docx.AgaDocx.FormatoCarta.A4, PageOrientationValues.Portrait, {20, 20, 20, 20})

        ' Aggiungi un paragrafo con testo semplice
        doc.AddParag("Questo è un esempio di paragrafo.")

        ' Aggiungi un paragrafo con formattazione
        doc.AddParag("Paragrafo con formattazione.").ParFormat(fos:=14, bld:=True, ita:=True, und:=True, ali:=JustificationValues.Center)

        ' Aggiungi un paragrafo con colore e riempimento
        doc.AddParag("Paragrafo con colore e riempimento.").ParColore(col:="FF0000", fil:="FFFF00")

        ' Aggiungi un paragrafo con bordo
        doc.AddParag("Paragrafo con bordo.").ParBorder(bor:=2, col:="0000FF")

        ' Aggiungi una tabella
        Dim table As AgaTable = doc.AddTable().LastTable
        table.AggiungiCella(0, 0, "Cella 1,1", "FFC0CB", adattaContenuto:=True)
        table.AggiungiCella(0, 1, "Cella 1,2", "FFC0CB")
        table.AggiungiCella(0, 2, "Cella 1,3", "FFC0CB", adattaContenuto:=True)
        table.AggiungiCella(0, 3, "Cella 1,4", "FFC0CB")
        table.AggiungiCella(1, 0, "Cella 2,1", "ADD8E6")
        table.AggiungiCella(1, 1, "Cella 2,2", "ADD8E6")
        table.AggiungiCella(1, 2, "Cella 2,3", "ADD8E6", adattaContenuto:=True)
        table.AggiungiCella(1, 3, "Cella 1,4", "ADD8E6")
        table.ImpostaBordi(BorderValues.Single, 1, "000000")

        ' Aggiungi un'immagine
        doc.AddImage("D:\PROGRAMMAZIONE\WINFORM\_OLD\BASEWPF\WpfApp9\Img\splash.png", wid:=75, hei:=50, ali:=JustificationValues.Right)

        ' Salva il documento
        doc.Salva()
    End Sub

End Module
