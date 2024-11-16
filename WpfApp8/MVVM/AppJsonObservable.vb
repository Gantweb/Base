Imports System.ComponentModel
Imports System.Text.Json
Imports System.Text.Json.Nodes

Namespace MVVM

    Public Class AppJsonObservable
        Implements INotifyPropertyChanged

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected _items As JsonObject = New JsonObject()

        Default Public Property Item(propertyName As String) As String
            Get
                Return If(_items.ContainsKey(_b64.StringToBase64(propertyName)), _b64.Base64ToString(_items(_b64.StringToBase64(propertyName)).ToString()), Nothing)
            End Get
            Set(value As String)
                If Not _items.ContainsKey(_b64.StringToBase64(propertyName)) OrElse _items(_b64.StringToBase64(propertyName)).ToString() <> _b64.StringToBase64(value) Then
                    _items(_b64.StringToBase64(propertyName)) = _b64.StringToBase64(value)
                    OnPropertyChanged(propertyName)
                End If
            End Set
        End Property

        Public Function ToJson() As String
            Return JsonSerializer.Serialize(_items)
        End Function

        Public Sub FromJson(jsonString As String)
            Try
                _items = JsonSerializer.Deserialize(Of JsonObject)(jsonString)
            Catch ex As JsonException
                Throw New InvalidOperationException("Errore nella deserializzazione del JSON.", ex)
            End Try
        End Sub

        Protected Sub OnPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

    End Class

End Namespace