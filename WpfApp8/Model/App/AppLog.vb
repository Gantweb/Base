Imports System.Text.Json.Nodes
Imports WpfApp8.AppTools

Namespace Model.App

    Public Class AppLog
        Inherits MVVM.AppJsonObservable

        Private pth As String

        Dim _fileHandler As New FileHandler()

        Public Sub New()
            pth = $"{Base("inipath")}Error.log"
            Leggi()
            ' TODO: gestione invio errori
        End Sub

        ' Nascondi la proprietà predefinita Item della classe base
        <Obsolete("Questa proprietà è inibita per AppLog.")>
        Default Public Shadows Property Item(propertyName As String) As Object
            Get
                Throw New NotImplementedException("La proprietà Item è inibita per AppLog.")
            End Get
            Set(value As Object)
                Throw New NotImplementedException("La proprietà Item è inibita per AppLog.")
            End Set
        End Property

        Public Sub Log(exc As Exception)
            Dim exceptionDetails As New JsonObject()
            exceptionDetails("Message") = JsonValue.Create(exc.Message)
            exceptionDetails("StackTrace") = JsonValue.Create(exc.StackTrace)
            exceptionDetails("Timestamp") = JsonValue.Create(DateTime.Now.ToString("o"))

            Dim logEntryName As String = $"LogEntry_{DateTime.Now:yyyyMMddHHmmssfff}"

            _items(logEntryName) = exceptionDetails

            OnPropertyChanged(logEntryName)
            Salva()
        End Sub

        Private Sub Leggi()
            Try
                FromJson(_fileHandler.ReadWriteFile(Of String)(pth, "", True, True))
            Catch ex As Exception
                _items = New JsonObject()
            End Try
        End Sub

        Private Sub Salva()
            _fileHandler.ReadWriteFile(Of String)(pth, ToJson(), True, True)
        End Sub

    End Class

End Namespace