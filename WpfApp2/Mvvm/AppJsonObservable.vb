Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Text.Json.Nodes
Imports WpfApp2.AppTools.Wrapper

Namespace Mvvm

    Public MustInherit Class AppJsonObservable
        Implements INotifyPropertyChanged

        ' Evento per la notifica delle modifiche delle proprietà
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        ' JsonObject per memorizzare le proprietà dinamiche
        Private _dynamicProperties As New JsonObject()

        ' Proprietà esempio con notifica delle modifiche
        Private _exampleProperty As String

        ' Indicizzatore per accedere alle proprietà dinamiche
        Default Public Property Item(propertyName As String) As Object
            Get
                If _dynamicProperties.ContainsKey(Base64Helper.EncodeStringToBase64(propertyName)) Then
                    Return Base64Helper.DecodeStringFromBase64(_dynamicProperties(Base64Helper.EncodeStringToBase64(propertyName)))
                Else
                    Return Nothing
                End If
            End Get
            Set(value As Object)
                If _dynamicProperties.ContainsKey((Base64Helper.EncodeStringToBase64(propertyName))) Then
                    _dynamicProperties(Base64Helper.EncodeStringToBase64(propertyName)) = JsonValue.Create(Base64Helper.EncodeStringToBase64(value))
                Else
                    _dynamicProperties.Add(Base64Helper.EncodeStringToBase64(propertyName), JsonValue.Create(Base64Helper.EncodeStringToBase64(value)))
                End If
                OnPropertyChanged(Base64Helper.EncodeStringToBase64(propertyName))
            End Set
        End Property

        Public Shared Function FromJson(Of T As AppJsonObservable)(json As String) As T
            Dim instance As T = Activator.CreateInstance(Of T)()
            instance._dynamicProperties = JsonNode.Parse(json).AsObject()
            Return instance
        End Function

        ' Metodi per serializzare e deserializzare l'oggetto
        Public Function ToJson() As String
            Return _dynamicProperties.ToJsonString()
        End Function

        ' Metodo per sollevare l'evento PropertyChanged
        Protected Sub OnPropertyChanged(<CallerMemberName> Optional propertyName As String = Nothing)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

    End Class

End Namespace