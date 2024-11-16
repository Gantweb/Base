Imports WpfApp3.AppTools.Helper
Imports WpfApp3.Mvvm

Namespace AppTools

    Public Class AppConfig
        Inherits AppJsonObservable

        Private bs64 As New Base64Helper
        Private pth As String
        Dim fh As New FileHelper

        Public Sub New()
            pth = $"{bs64.DecodeStringFromBase64("inipath")}Config.ini"
            FromJson(fh.LeggiFileCompressoCriptato(pth))
        End Sub

        Public Sub Save()
            fh.ScriviFileCompressoCriptato(pth, ToJson)
        End Sub

    End Class

End Namespace