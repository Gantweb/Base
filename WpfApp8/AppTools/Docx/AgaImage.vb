Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Drawing
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing

Namespace AppTools.Docx

    Friend NotInheritable Class AgaImage

        Private Shared _imageCounter As Integer
        Private ReadOnly _ali As JustificationValues?
        Private ReadOnly _height As Integer
        Private ReadOnly _ids As String
        Private ReadOnly _name As String
        Private ReadOnly _relationshipId As String
        Private ReadOnly _width As Integer

        Friend Sub New(mp As MainDocumentPart, pth As String, Optional wid As Decimal = 25.4, Optional hei As Decimal = 25.4, Optional ali As JustificationValues? = Nothing)
            If Not File.Exists(pth) Then
                Throw New FileNotFoundException("Il file immagine specificato non esiste.", pth)
            End If

            _ali = ali
            Dim imagePart As ImagePart = mp.AddImagePart(GetImagePartType(pth))
            Using stream As New FileStream(pth, FileMode.Open, FileAccess.Read)
                imagePart.FeedData(stream)
            End Using
            _relationshipId = mp.GetIdOfPart(imagePart)

            SyncLock GetType(AgaImage)
                _imageCounter += 1
                _ids = _imageCounter.ToString(CultureInfo.InvariantCulture)
            End SyncLock

            _name = IO.Path.GetFileName(pth) & _ids
            Dim emuXmm = 914400 / 25.4

            Using img As New Bitmap(pth)
                Dim aspectRatio = img.Width / img.Height

                If (wid / hei) > aspectRatio Then
                    ' Mantieni l'altezza, ridimensiona la larghezza
                    _height = hei * emuXmm
                    _width = _height * aspectRatio
                Else
                    ' Mantieni la larghezza, ridimensiona l'altezza
                    _width = wid * emuXmm
                    _height = _width / aspectRatio
                End If
            End Using
        End Sub

        Friend Function GetImg() As Wordprocessing.Paragraph
            Dim element = New Wordprocessing.Drawing(
            New DocumentFormat.OpenXml.Drawing.Wordprocessing.Inline(
                New Wordprocessing.Extent() With {.Cx = _width, .Cy = _height},
                New Wordprocessing.EffectExtent() With {.LeftEdge = 0L, .TopEdge = 0L, .RightEdge = 0L, .BottomEdge = 0L},
                New Wordprocessing.DocProperties() With {.Id = _ids, .Name = _name},
                New Wordprocessing.NonVisualGraphicFrameDrawingProperties(New GraphicFrameLocks() With {.NoChangeAspect = True}),
                New Graphic(New GraphicData(New Pictures.Picture(
                    New Pictures.NonVisualPictureProperties(
                        New Pictures.NonVisualDrawingProperties() With {.Id = 0, .Name = _name},
                        New Pictures.NonVisualPictureDrawingProperties()),
                    New Pictures.BlipFill(New Blip(
                        New BlipExtensionList(New BlipExtension() With {.Uri = Guid.NewGuid().ToString()})) With {
                            .Embed = _relationshipId, .CompressionState = BlipCompressionValues.Print},
                        New Stretch(New FillRectangle())),
                    New Pictures.ShapeProperties(New Transform2D(
                        New Offset() With {.X = 0L, .Y = 0L},
                        New Extents() With {.Cx = _width, .Cy = _height}),
                        New PresetGeometry(New AdjustValueList()) With {.Preset = ShapeTypeValues.Rectangle})
                )) With {.Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture"})
            ) With {
                .DistanceFromTop = 0UI,
                .DistanceFromBottom = 0UI,
                .DistanceFromLeft = 0UI,
                .DistanceFromRight = 0UI
})

            Dim p1 As New AgaParagraph(element)
            p1.Format(,,,, _ali)
            Return p1.GetPar()
        End Function

        Private Shared Function GetImagePartType(fileName As String) As PartTypeInfo
            Dim ext = IO.Path.GetExtension(fileName).Replace(".", "", StringComparison.Ordinal).ToLower(CultureInfo.CurrentCulture)
            Select Case ext
                Case "bmp" : Return ImagePartType.Bmp
                Case "emf" : Return ImagePartType.Emf
                Case "gif" : Return ImagePartType.Gif
                Case "ico" : Return ImagePartType.Icon
                Case "jpg" : Return ImagePartType.Jpeg
                Case "pcx" : Return ImagePartType.Pcx
                Case "png" : Return ImagePartType.Png
                Case "tif" : Return ImagePartType.Tiff
                Case "wmf" : Return ImagePartType.Wmf
                Case Else : Return ImagePartType.Bmp
            End Select
        End Function

    End Class

End Namespace