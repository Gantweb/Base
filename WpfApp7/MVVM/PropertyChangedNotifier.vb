Imports System.ComponentModel

Namespace MVVM

    Public Class PropertyChangedNotifier
        Implements INotifyPropertyChanged

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Sub OnPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

        Protected Function SetProperty(Of T)(ByRef storage As T, value As T, propertyName As String) As Boolean
            If Not EqualityComparer(Of T).Default.Equals(storage, value) Then
                storage = value
                OnPropertyChanged(propertyName)
                Return True
            End If
            Return False
        End Function

    End Class

End Namespace
