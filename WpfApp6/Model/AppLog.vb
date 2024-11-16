Imports System.Text.Json.Nodes
Imports WpfApp6.AppTools
Imports WpfApp6.AppTools.Interfacce

Namespace Model

    Public Class AppLog
        Inherits MVVM.AppJsonObservable

        Private ReadOnly _fileHandler As FileHandler
        Private pth As String

        Public Sub New(base64Helper As IBase64, fileHandler As FileHandler)
            MyBase.New(base64Helper)
            _fileHandler = fileHandler
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

        ' Funzione per registrare l'eccezione nel log
        Public Sub Log(exc As Exception)
            ' Crea un oggetto JsonObject per memorizzare i dettagli dell'eccezione
            Dim exceptionDetails As New JsonObject()
            exceptionDetails("Message") = JsonValue.Create(exc.Message)
            exceptionDetails("StackTrace") = JsonValue.Create(exc.StackTrace)
            exceptionDetails("Timestamp") = JsonValue.Create(DateTime.Now.ToString("o"))

            ' Genera un nuovo nome per la proprietà basato sull'ora corrente
            Dim logEntryName As String = $"LogEntry_{DateTime.Now:yyyyMMddHHmmssfff}"

            ' Archivia i dettagli dell'eccezione in properties
            properties(logEntryName) = exceptionDetails

            ' Solleva l'evento PropertyChanged per il nuovo log entry
            OnPropertyChanged(logEntryName)
            Salva()
        End Sub

        Private Sub Leggi()
            Try
                FromJson(_fileHandler.ReadWriteFile(pth, "", True, True))
            Catch ex As Exception
                properties = New JsonObject()
            End Try
        End Sub

        Private Sub Salva()
            _fileHandler.ReadWriteFile(pth, ToJson(), True, True)
        End Sub

    End Class

End Namespace