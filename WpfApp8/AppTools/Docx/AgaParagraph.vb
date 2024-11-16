Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Wordprocessing

Namespace AppTools.Docx

    Friend NotInheritable Class AgaParagraph

        Private ReadOnly _para As New Wordprocessing.Paragraph
        Private _paraP As New Wordprocessing.ParagraphProperties
        Private ReadOnly _run As New Wordprocessing.Run
        Private _runP As New Wordprocessing.RunProperties
        Private ReadOnly _text As New Wordprocessing.Text With {.Space = SpaceProcessingModeValues.Preserve}

        Friend Sub New(txt As String)
            InizializzaParagrafo(txt)
        End Sub

        Friend Sub New(p As Wordprocessing.Drawing)
            InizializzaDisegno(p)
        End Sub

        Friend Function Border(Optional bor As Decimal = 1, Optional col As String = "Auto") As AgaParagraph
            Dim tmp As ParagraphBorders = OttieniOAppendiBordi()
            ImpostaBordi(tmp, bor, col)
            Return Me
        End Function

        Friend Function Colore(Optional col As String = "Auto", Optional fil As String = "") As AgaParagraph
            If col <> "Auto" Then Color(col)
            If Not String.IsNullOrEmpty(fil) Then Fill(fil)
            Return Me
        End Function

        Friend Function Format(Optional fos As Integer = 12, Optional bld As Boolean = False, Optional ita As Boolean = False, Optional und As Boolean = False,
                           Optional ali As JustificationValues = Nothing) As AgaParagraph
            FontSizes(fos)
            Bolds(bld)
            Italics(ita)
            UnderLine(und)
            Align(ali)
            Return Me
        End Function

        Friend Function GetPar() As Wordprocessing.Paragraph
            Return _para
        End Function

        Private Sub Align(Optional ali As JustificationValues = Nothing)
            Dim tmp As Justification = OttieniOAppendiGiustificazione()
            If Not IsNothing(ali) Then tmp.Val = ali
        End Sub

        Private Function Bolds(Optional bld As Boolean = True) As AgaParagraph
            Dim tmp As Bold = OttieniOAppendiProprietà(Of Bold)(_runP)
            tmp.Val = bld
            Return Me
        End Function

        Private Function Color(Optional col As String = "Auto") As AgaParagraph
            If col <> "Auto" Then
                _runP.Color = New Color() With {.Val = col}
            End If
            Return Me
        End Function

        Private Function Fill(Optional fil As String = "") As AgaParagraph
            Dim tmp As Shading = OttieniOAppendiProprietà(Of Shading)(_paraP)
            tmp.Fill = fil
            Return Me
        End Function

        Private Function FontSizes(Optional fs As Integer = 12) As AgaParagraph
            Dim tmp As FontSize = OttieniOAppendiProprietà(Of FontSize)(_runP)
            tmp.Val = fs * 2
            Return Me
        End Function

        Private Function Italics(Optional ita As Boolean = True) As AgaParagraph
            Dim tmp As Italic = OttieniOAppendiProprietà(Of Italic)(_runP)
            tmp.Val = ita
            Return Me
        End Function

        Private Function UnderLine(Optional und As Boolean = True) As AgaParagraph
            Dim tmp As Wordprocessing.Underline = OttieniOAppendiProprietà(Of Wordprocessing.Underline)(_runP)
            tmp.Val = If(und, UnderlineValues.Single, UnderlineValues.None)
            Return Me
        End Function

        Private Sub InizializzaParagrafo(txt As String)
            _runP = _run.AppendChild(New Wordprocessing.RunProperties)
            _paraP = _para.AppendChild(New Wordprocessing.ParagraphProperties)
            _text.Text = txt
            _run.Append(_text)
            _para.Append(_run)
            ImpostaSpaziatura()
        End Sub

        Private Sub InizializzaDisegno(p As Wordprocessing.Drawing)
            _runP = _run.AppendChild(New Wordprocessing.RunProperties)
            _paraP = _para.AppendChild(New Wordprocessing.ParagraphProperties)
            _run.Append(p)
            _para.Append(_run)
            ImpostaSpaziatura()
        End Sub

        Private Sub ImpostaSpaziatura()
            Dim spacing As New SpacingBetweenLines() With {.After = 0, .Before = 0, .Line = 240, .LineRule = LineSpacingRuleValues.Auto}
            _paraP.Append(spacing)
        End Sub

        Private Function OttieniOAppendiBordi() As ParagraphBorders
            Dim tmp As ParagraphBorders = _paraP.Elements(Of ParagraphBorders)().FirstOrDefault()
            If IsNothing(tmp) Then
                tmp = New ParagraphBorders
                _paraP.Append(tmp)
            End If
            Return tmp
        End Function

        Private Shared Sub ImpostaBordi(tmp As ParagraphBorders, bor As Decimal, col As String)
            tmp.TopBorder = New Wordprocessing.TopBorder() With {.Val = BorderValues.Single, .Size = bor * 6, .Space = 0, .Color = col}
            tmp.BottomBorder = New Wordprocessing.BottomBorder() With {.Val = BorderValues.Single, .Size = bor * 6, .Space = 0, .Color = col}
            tmp.LeftBorder = New Wordprocessing.LeftBorder() With {.Val = BorderValues.Single, .Size = bor * 6, .Space = 0, .Color = col}
            tmp.RightBorder = New Wordprocessing.RightBorder() With {.Val = BorderValues.Single, .Size = bor * 6, .Space = 0, .Color = col}
        End Sub

        Private Function OttieniOAppendiGiustificazione() As Justification
            Dim tmp As Justification = _paraP.Elements(Of Justification)().FirstOrDefault()
            If IsNothing(tmp) Then
                tmp = New Justification
                _paraP.Append(tmp)
            End If
            Return tmp
        End Function

        Private Shared Function OttieniOAppendiProprietà(Of T As OpenXmlElement)(parent As OpenXmlElement) As T
            Dim tmp As T = parent.Elements(Of T)().FirstOrDefault()
            If IsNothing(tmp) Then
                tmp = Activator.CreateInstance(Of T)()
                parent.Append(tmp)
            End If
            Return tmp
        End Function

    End Class

End Namespace
