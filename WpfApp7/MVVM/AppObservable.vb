Imports System.ComponentModel

Namespace MVVM

    Public Class AppObservable
        Implements INotifyPropertyChanged

        Private ReadOnly _values As New Dictionary(Of String, Object)
        Private ReadOnly _IsNotify As Boolean

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Default Public Property Item(key As String) As Object
            Get
                If _values.ContainsKey(key) Then
                    Return _values(key)
                Else
                    Return Nothing
                End If
            End Get
            Set(value As Object)
                If Not Equals(_values(key), value) Then
                    _values(key) = value
                    OnPropertyChanged(key)
                End If
            End Set
        End Property
        Public ReadOnly Property IsNotify As Boolean
            Get
                Return _IsNotify
            End Get
        End Property

        Public Sub New(Optional isNotify As Boolean = True)
            _IsNotify = isNotify
        End Sub

        Protected Sub OnPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

    End Class

End Namespace
