Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Text.Json.Nodes
Imports WpfApp3.AppTools.Helper

Namespace Mvvm

    Public MustInherit Class AppJsonObservable
        Implements INotifyPropertyChanged

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Private _dynamicProperties As New JsonObject()

        Private _exampleProperty As String
        Private bs64 As New Base64Helper

        Default Public Property Item(propertyName As String) As Object
            Get
                If _dynamicProperties.ContainsKey(bs64.EncodeStringToBase64(propertyName)) Then
                    Dim aa = _dynamicProperties(bs64.EncodeStringToBase64(propertyName))
                    Dim bb = bs64.DecodeStringFromBase64(_dynamicProperties(bs64.EncodeStringToBase64(propertyName)))
                    'Dim cc = bs64.DecodeStringFromBase64(bb)

                    Return _dynamicProperties(bs64.EncodeStringToBase64(propertyName)).ToString
                Else
                    Return Nothing
                End If
            End Get
            Set(value As Object)
                If _dynamicProperties.ContainsKey(bs64.EncodeStringToBase64(propertyName)) Then
                    _dynamicProperties(bs64.EncodeStringToBase64(propertyName)) = JsonValue.Create(value)
                Else
                    _dynamicProperties.Add(bs64.EncodeStringToBase64(propertyName), JsonValue.Create(value))
                End If
                OnPropertyChanged(bs64.EncodeStringToBase64(propertyName))
            End Set
        End Property

        Public Sub FromJson(json As String)
            Try
                _dynamicProperties = JsonNode.Parse(json).AsObject()
            Catch ex As Exception
                _dynamicProperties = New JsonObject()
            End Try
        End Sub

        Public Function ToJson() As String
            Return _dynamicProperties.ToJsonString()
        End Function

        Protected Sub OnPropertyChanged(<CallerMemberName> Optional propertyName As String = Nothing)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

    End Class

End Namespace