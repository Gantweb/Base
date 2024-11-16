Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing

Namespace AppTools.Docx

    Friend NotInheritable Class AgaDocx

        Friend Enum FormatoCarta
            A0
            A1
            A2
            A3
            A4
            A5
        End Enum

        Private docBody As Body
        Private LastPar As AgaParagraph
        Friend LastTable As AgaTable

        Friend Property Doc As WordprocessingDocument

        Friend Sub New(pth As String, Optional formato As FormatoCarta = FormatoCarta.A4, Optional orientamento As PageOrientationValues = Nothing, Optional margini As Integer() = Nothing)
            If margini Is Nothing Then
                margini = New Integer() {20, 20, 20, 20} ' Valori di default in mm
            End If
            If IsNothing(orientamento) Then
                orientamento = PageOrientationValues.Portrait ' Assegna il valore predefinito se non fornito
            End If
            Setup(pth, formato, orientamento, margini)
        End Sub

        Private Sub Setup(pth As String, formato As FormatoCarta, orientamento As PageOrientationValues, margini As Integer())
            Doc = WordprocessingDocument.Create(pth, WordprocessingDocumentType.Document, True)
            Doc.AddMainDocumentPart()
            Doc.MainDocumentPart.Document = New Document()
            docBody = New Body()
            Doc.MainDocumentPart.Document.Append(docBody)
            ImpostaPagina(formato, orientamento, margini)
        End Sub

        Friend Function AddImage(pth As String, Optional wid As Decimal = 25.4, Optional hei As Decimal = 25.4,
                             Optional ali As Wordprocessing.JustificationValues? = Nothing) As AgaDocx
            Dim lastImage As New AgaImage(Doc.MainDocumentPart, pth, wid, hei, ali)
            docBody.Append(lastImage.GetImg())
            Return Me
        End Function

        Friend Function AddParag(txt As String) As AgaDocx
            LastPar = New AgaParagraph(txt)
            docBody.Append(LastPar.GetPar())
            Return Me
        End Function

        Friend Function AddTable() As AgaDocx
            LastTable = New AgaTable()
            docBody.Append(LastTable.Table)
            Return Me
        End Function

        Friend Function Margini(lf As Integer, tp As Integer, rg As Integer, bt As Integer) As AgaDocx
            Dim secProps As New SectionProperties()
            Dim pageMargin As New PageMargin With {
                .Top = CUInt(tp * 56.7),
                .Bottom = CUInt(bt * 56.7),
                .Left = CUInt(lf * 56.7),
                .Right = CUInt(rg * 56.7)
            }
            secProps.AppendChild(pageMargin)
            docBody.Append(secProps)
            Return Me
        End Function

        Friend Function ParBorder(Optional bor As Decimal = 1, Optional col As String = "Auto") As AgaDocx
            If LastPar Is Nothing Then
                Throw New InvalidOperationException("Non è stato trovato alcun paragrafo da bordare.")
            End If
            LastPar.Border(bor, col)
            Return Me
        End Function

        Friend Function ParColore(Optional col As String = "Auto", Optional fil As String = "") As AgaDocx
            If LastPar Is Nothing Then
                Throw New InvalidOperationException("Non è stato trovato alcun paragrafo da colorare.")
            End If
            LastPar.Colore(col, fil)
            Return Me
        End Function

        Friend Function ParFormat(Optional fos As Integer = 12, Optional bld As Boolean = False, Optional ita As Boolean = False, Optional und As Boolean = False, Optional ali As JustificationValues = Nothing) As AgaDocx
            If LastPar Is Nothing Then
                Throw New InvalidOperationException("Non è stato trovato alcun paragrafo da formattare.")
            End If
            LastPar.Format(fos, bld, ita, und, ali)
            Return Me
        End Function

        Friend Sub Salva()
            Doc.MainDocumentPart.Document.Save()
            Doc.Dispose()
        End Sub

        Private Shared Function CalcolaAltezza(dimensioni As (Width As Integer, Height As Integer), orientamento As PageOrientationValues) As Integer
            Return CUInt(If(orientamento = PageOrientationValues.Landscape, dimensioni.Width, dimensioni.Height) * 56.69)
        End Function

        Private Shared Function CalcolaLarghezza(dimensioni As (Width As Integer, Height As Integer), orientamento As PageOrientationValues) As Integer
            Return CUInt(If(orientamento = PageOrientationValues.Landscape, dimensioni.Height, dimensioni.Width) * 56.69)
        End Function

        Private Shared Sub ImpostaMargini(sp As SectionProperties, margini As Integer())
            Dim marginiPage As New PageMargin With {
                .Left = CUInt(margini(0) * 56.7),
                .Right = CUInt(margini(1) * 56.7),
                .Top = CUInt(margini(2) * 56.7),
                .Bottom = CUInt(margini(3) * 56.7)
            }
            sp.RemoveAllChildren(Of PageMargin)()
            sp.Append(marginiPage)
        End Sub

        Private Sub ImpostaPagina(formato As FormatoCarta, orientamento As PageOrientationValues, margini As Integer())
            Dim dimensioni As (Width As Integer, Height As Integer) = OttieniDimensioniFormato(formato)

            Dim sp As New SectionProperties()
            Dim ps As New PageSize With {
                .Width = CUInt(CalcolaLarghezza(dimensioni, orientamento)),
                .Height = CUInt(CalcolaAltezza(dimensioni, orientamento)),
                .Orient = orientamento
            }
            sp.Append(ps)

            ImpostaMargini(sp, margini)

            docBody.Append(sp)
        End Sub

        Private Shared Function OttieniDimensioniFormato(formato As FormatoCarta) As (Integer, Integer)
            Select Case formato
                Case FormatoCarta.A0
                    Return (841, 1189)
                Case FormatoCarta.A1
                    Return (594, 841)
                Case FormatoCarta.A2
                    Return (420, 594)
                Case FormatoCarta.A3
                    Return (297, 420)
                Case FormatoCarta.A4
                    Return (210, 297)
                Case FormatoCarta.A5
                    Return (148, 210)
                Case Else
                    Throw New ArgumentOutOfRangeException(NameOf(formato), "Formato non valido.")
            End Select
        End Function

    End Class

End Namespace
