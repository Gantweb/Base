Imports System.ComponentModel

Namespace MVVM

    Public MustInherit Class ViewModelBase
        Implements INotifyPropertyChanged

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Sub OnPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

        ' Esempio di metodo comune
        Public Sub SetProperty(Of T)(ByRef field As T, value As T, propertyName As String)
            If Not EqualityComparer(Of T).Default.Equals(field, value) Then
                field = value
                OnPropertyChanged(propertyName)
            End If
        End Sub

    End Class

End Namespace
