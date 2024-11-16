Imports System.Text.Json.Nodes
Imports WpfApp8.AppTools

Namespace Model.App

    Public Class AppConfig
        Inherits MVVM.AppJsonObservable

        Private _pth As String

        Dim _fileHandler As New FileHandler()

        Public Sub New()
            _pth = $"{Base("inipath")}Config.ini"
            Leggi()
            Item("assembly") = "AgaSoft"
        End Sub

        Public Sub Verifica()
            Base("hash") = _b64.BytesToBase64(_hasher.ComputeHash(_b64.StringToBytes(Base("info"))))
            Base("veri") = (Base("hash") = Item("hash")) AndAlso (Base("code") = Item("code"))
            Base("tipo") = If((Base("hash") = Item("hash")) AndAlso (Base("code") = Item("code")), "FULL", "DEMO")
            ' TODO: Base("imag") = If((Base("hash") = Item("hash")) AndAlso (Base("code") = Item("code")), "FULL", "DEMO")
        End Sub

        Friend Sub Controlla()
            Verifica()
        End Sub

        Private Sub Leggi()
            Try
                FromJson(_fileHandler.ReadWriteFile(Of String)(_pth, "", True, True))
            Catch ex As Exception
                _items = New JsonObject()
            End Try
        End Sub

    End Class

End Namespace