Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Wordprocessing

Namespace AppTools.Docx

    Friend NotInheritable Class AgaTable

        Friend ReadOnly Property Table As Wordprocessing.Table

        Friend Sub New()
            Table = New Wordprocessing.Table()
            ImpostaProprietaTabella()
        End Sub

        Friend Sub AggiungiCella(riga As Integer, colonna As Integer, contenuto As String, Optional coloreSfondo As String = Nothing, Optional adattaContenuto As Boolean = False)
            ControllaValiditaIndici(riga, colonna)

            Dim row As TableRow = GetOrCreateRow(riga)
            Dim cella As TableCell = GetOrCreateCell(row, colonna)

            cella.RemoveAllChildren()

            ' Utilizza AgaParagraph per aggiungere il contenuto
            Dim paragrafo As New AgaParagraph(contenuto)
            cella.Append(paragrafo.GetPar())

            If Not String.IsNullOrEmpty(coloreSfondo) Then
                AggiungiColoreSfondo(cella, coloreSfondo)
            End If

            ' Se adattaContenuto è true, imposta la larghezza della colonna in base al contenuto
            If adattaContenuto Then
                ImpostaLarghezzaColonnaAdatta(cella)
            End If
        End Sub
        Friend Sub ImpostaBordi(bordo As BorderValues, spessore As Integer, colore As String)
            Dim tableBorders As New TableBorders(
                New TopBorder() With {.Val = bordo, .Size = spessore * 8, .Color = colore},
                New BottomBorder() With {.Val = bordo, .Size = spessore * 8, .Color = colore},
                New LeftBorder() With {.Val = bordo, .Size = spessore * 8, .Color = colore},
                New RightBorder() With {.Val = bordo, .Size = spessore * 8, .Color = colore},
                New InsideHorizontalBorder() With {.Val = bordo, .Size = spessore * 8, .Color = colore},
                New InsideVerticalBorder() With {.Val = bordo, .Size = spessore * 8, .Color = colore}
            )

            Dim tableProperties As TableProperties = Table.Elements(Of TableProperties)().FirstOrDefault()
            If tableProperties Is Nothing Then
                tableProperties = New TableProperties()
                Table.AppendChild(tableProperties)
            End If

            Dim existingBorders As TableBorders = tableProperties.Elements(Of TableBorders)().FirstOrDefault()
            If Not IsNothing(existingBorders) Then
                tableProperties.RemoveChild(existingBorders)
            End If
            tableProperties.AppendChild(tableBorders)
        End Sub

        Private Shared Sub ControllaValiditaIndici(riga As Integer, colonna As Integer)
            If riga < 0 OrElse colonna < 0 Then
                Throw New ArgumentOutOfRangeException(NameOf(riga), "Riga e colonna devono essere maggiori o uguali a zero.")
            End If
        End Sub

        Private Function GetOrCreateRow(riga As Integer) As TableRow
            Dim rows As List(Of TableRow) = Table.Elements(Of TableRow)().ToList()

            While rows.Count <= riga
                Dim newRow As New TableRow()
                Table.AppendChild(newRow)
                rows.Add(newRow)
            End While

            Return rows(riga)
        End Function

        Private Shared Function GetOrCreateCell(row As TableRow, colonna As Integer) As TableCell
            While row.Elements(Of TableCell)().Count <= colonna
                row.AppendChild(New TableCell())
            End While

            Return row.Elements(Of TableCell)().ElementAt(colonna)
        End Function

        Private Shared Sub AggiungiColoreSfondo(cella As TableCell, coloreSfondo As String)
            Dim shading As New Shading() With {
                .Val = ShadingPatternValues.Clear,
                .Color = "auto",
                .Fill = coloreSfondo
            }
            cella.AppendChild(New TableCellProperties(shading))
        End Sub

        Private Sub ImpostaProprietaTabella()
            ' Imposta la larghezza della tabella come percentuale per garantire che rientri nei margini
            Dim tableProperties As New TableProperties(New TableWidth() With {
                .Type = TableWidthUnitValues.Pct,
                .Width = "5000" ' 100% della larghezza della pagina
            })
            Table.AppendChild(tableProperties)
        End Sub

        Private Sub ImpostaLarghezzaColonnaAdatta(cella As TableCell)
            Dim cellProperties As TableCellProperties = cella.GetFirstChild(Of TableCellProperties)()
            If cellProperties Is Nothing Then
                cellProperties = New TableCellProperties()
                cella.AppendChild(cellProperties)
            End If

            Dim cellWidth As TableCellWidth = cellProperties.GetFirstChild(Of TableCellWidth)()
            If cellWidth Is Nothing Then
                cellWidth = New TableCellWidth() With {
                    .Type = TableWidthUnitValues.Auto
                }
                cellProperties.AppendChild(cellWidth)
            Else
                cellWidth.Type = TableWidthUnitValues.Auto
            End If
        End Sub

    End Class

End Namespace
