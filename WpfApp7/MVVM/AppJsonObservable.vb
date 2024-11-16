Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Text.Json.Nodes
Imports WpfApp6.AppTools.Interfacce
Imports WpfApp7.AppTools.Interfacce

Namespace MVVM

    Public Class AppJsonObservable
        Implements INotifyPropertyChanged

        ' Evento necessario per l'implementazione di INotifyPropertyChanged
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        ' JsonObject per memorizzare le proprietà
        Protected properties As New JsonObject()

        Private ReadOnly _base64Helper As IBase64

        Public Sub New(base64Helper As IBase64)
            _base64Helper = base64Helper
        End Sub

        ' Metodo per sollevare l'evento PropertyChanged
        Protected Sub OnPropertyChanged(<CallerMemberName> Optional propertyName As String = Nothing)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

        ' Proprietà predefinita per accedere alle proprietà tramite nome
        Default Public Property Item(propertyName As String) As Object
            Get
                Try
                    Dim encodedPropertyName As String = _base64Helper.StringToBase64(propertyName)
                    If properties.ContainsKey(encodedPropertyName) Then
                        Dim decodedValue As String = _base64Helper.Base64ToString(properties(encodedPropertyName).ToString())
                        Dim numberValue As Double
                        Dim booleanValue As Boolean
                        If Double.TryParse(decodedValue.ToString(), numberValue) Then
                            Return numberValue
                        ElseIf Boolean.TryParse(decodedValue.ToString(), booleanValue) Then
                            Return booleanValue
                        Else
                            Return decodedValue.ToString()
                        End If
                    Else
                        Return Nothing
                    End If
                Catch ex As Exception
                    Debug.WriteLine($"Errore nella proprietà Item: {ex.Message}")
                    Return Nothing
                End Try
            End Get
            Set(value As Object)
                Try
                    Dim encodedPropertyName As String = _base64Helper.StringToBase64(propertyName)
                    Dim encodedValue As String = _base64Helper.StringToBase64(value.ToString())
                    If Not properties.ContainsKey(encodedPropertyName) OrElse
                        Not Equals(properties(encodedPropertyName).ToString(), encodedValue) Then
                        properties(encodedPropertyName) = JsonValue.Create(encodedValue)
                        OnPropertyChanged(encodedPropertyName)
                    End If
                Catch ex As Exception
                    Debug.WriteLine($"Errore nell'impostazione della proprietà Item: {ex.Message}")
                End Try
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
