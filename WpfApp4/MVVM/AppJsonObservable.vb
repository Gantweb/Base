Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Text.Json.Nodes
Imports WpfApp4.AppTools

Namespace MVVM

    Public Class AppJsonObservable
        Implements INotifyPropertyChanged

        ' Evento necessario per l'implementazione di INotifyPropertyChanged
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        ' JsonObject per memorizzare le proprietà
        Protected properties As New JsonObject()

        ' Metodo per sollevare l'evento PropertyChanged
        Protected Sub OnPropertyChanged(<CallerMemberName> Optional propertyName As String = Nothing)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

        ' Proprietà predefinita per accedere alle proprietà tramite nome
        Default Public Property Item(propertyName As String) As Object
            Get
                If properties.ContainsKey(StringToBase64(propertyName)) Then
                    Dim value As JsonNode = Base64ToString(properties(StringToBase64(propertyName)))
                    If value Is Nothing Then Return Nothing

                    ' Verifica il tipo di valore
                    Dim numberValue As Double
                    Dim booleanValue As Boolean

                    If Double.TryParse(value.ToString(), numberValue) Then
                        Return numberValue
                    ElseIf Boolean.TryParse(value.ToString(), booleanValue) Then
                        Return booleanValue
                    Else
                        Return value.ToString()
                    End If
                Else
                    Return Nothing
                End If
            End Get
            Set(value As Object)
                Dim encodedPropertyName As String = Base64Module.StringToBase64(propertyName)
                Dim encodedValue As String = Base64Module.StringToBase64(value.ToString())
                If Not properties.ContainsKey(encodedPropertyName) OrElse
                    Not Equals(properties(encodedPropertyName).ToString(), encodedValue) Then
                    properties(encodedPropertyName) = JsonValue.Create(encodedValue)
                    OnPropertyChanged(encodedPropertyName)
                End If
            End Set
        End Property

        ' Funzione per esportare JsonObject in una stringa JSON
        Public Function ToJson() As String
            Return properties.ToJsonString()
        End Function

        ' Funzione per caricare una stringa JSON in JsonObject
        Public Sub FromJson(jsonString As String)
            properties = JsonNode.Parse(jsonString).AsObject()
        End Sub

    End Class

End Namespace