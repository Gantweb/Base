Imports System.Text.Json.Nodes
Imports WpfApp4.AppTools

Namespace Model

    Public Class AppConfig
        Inherits MVVM.AppJsonObservable

        Private pth As String = $"{Base("inipath")}Config.ini"

        Public Sub New()
            Leggi()
            Item("assembly") = "AgaSoft"
        End Sub

        Public Sub Verifica()
            Base("hash") = EncryptionHelper.ComputeHash(Base("info"))
            Base("veri") = (Base("hash") = Item("hash")) AndAlso (Base("code") = Item("code"))
            Base("tipo") = If((Base("hash") = Item("hash")) AndAlso (Base("code") = Item("code")), "FULL", "DEMO")
            ' TODO: Base("imag") = If((Base("hash") = Item("hash")) AndAlso (Base("code") = Item("code")), "FULL", "DEMO")
        End Sub

        Private Sub Leggi()
            Try
                FromJson(AppTools.ReadWriteFile(pth, "", True, True))
            Catch ex As Exception
                properties = New JsonObject()
            End Try
        End Sub

        Public Sub Salva()
            AppTools.ReadWriteFile(pth, ToJson(), True, True)
        End Sub

        Friend Sub Controlla()
            Verifica()
        End Sub
    End Class

End Namespace