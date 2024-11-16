Imports System.Text.Json.Nodes
Imports WpfApp5.AppTools
Imports WpfApp5.AppTools.Interfacce

Namespace Model

    Public Class AppConfig
        Inherits MVVM.AppJsonObservable

        Private ReadOnly _fileHandler As FileHandler
        Private ReadOnly _hasher As IHasher
        Private ReadOnly _base64Helper As IBase64
        Private pth As String

        Public Sub New(base64Helper As IBase64, hasher As IHasher, fileHandler As FileHandler)
            MyBase.New(base64Helper)
            _base64Helper = base64Helper
            _hasher = hasher
            _fileHandler = fileHandler
            pth = $"{Base("inipath")}Config.ini"
            Leggi()
            Item("assembly") = "AgaSoft"
        End Sub

        Public Sub Salva()
            _fileHandler.ReadWriteFile(pth, ToJson(), True, True)
        End Sub

        Public Sub Verifica()
            Base("hash") = _base64Helper.BytesToBase64(_hasher.ComputeHashBytes(Base("info")))
            Base("veri") = (Base("hash") = Item("hash")) AndAlso (Base("code") = Item("code"))
            Base("tipo") = If((Base("hash") = Item("hash")) AndAlso (Base("code") = Item("code")), "FULL", "DEMO")
            ' TODO: Base("imag") = If((Base("hash") = Item("hash")) AndAlso (Base("code") = Item("code")), "FULL", "DEMO")
        End Sub

        Friend Sub Controlla()
            Verifica()
        End Sub

        Private Sub Leggi()
            Try
                FromJson(_fileHandler.ReadWriteFile(pth, "", True, True))
            Catch ex As Exception
                properties = New JsonObject()
            End Try
        End Sub

    End Class

End Namespace