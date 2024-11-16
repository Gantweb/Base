Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Text.Json.Nodes
Imports WpfApp1.AppTools.Wrapper

Namespace AppTools
Friend Class AppJsonObservable
    Implements INotifyPropertyChanged

    Protected job As New JsonObject()

    Friend Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Friend Property IsChanged As Boolean

    Default Friend Property Item(key As String) As Object
        Get
            Return GetItem(key)
        End Get
        Set(value As Object)
            SetItem(key, value)
        End Set
    End Property

    Private Function GetItem(key As String) As Object
        Try
            Dim base64Key As String = ToBase64String(key)
            If job.ContainsKey(base64Key) Then
                Return FromBase64ToString(job(base64Key).ToString())
            End If
        Catch ex As Exception
            ' Gestione delle eccezioni
            Console.WriteLine($"Errore in GetItem: {ex.Message}")
        End Try
        Return Nothing
    End Function

    Private Sub SetItem(key As String, value As Object)
        Try
            Dim base64Key As String = ToBase64String(key)
            job(base64Key) = JsonValue.Create(ToBase64String(value.ToString()))
            OnPropertyChanged(key)
        Catch ex As Exception
            ' Gestione delle eccezioni
            Console.WriteLine($"Errore in SetItem: {ex.Message}")
        End Try
    End Sub

    Protected Overridable Sub OnPropertyChanged(<CallerMemberName> Optional propertyName As String = Nothing)
        Try
            If propertyName IsNot Nothing Then
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
                IsChanged = True
            End If
        Catch ex As Exception
            ' Gestione delle eccezioni
            Console.WriteLine($"Errore in OnPropertyChanged: {ex.Message}")
        End Try
    End Sub

    Public Overrides Function ToString() As String
        Return job.ToJsonString()
    End Function

    ' Metodo per ottenere l'oggetto JSON completo
    Friend Function GetJsonObject() As JsonObject
        Return job
    End Function
End Class
End Namespace