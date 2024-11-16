Imports System.ComponentModel

Namespace MVVM.Validation

    Public Class DataErrorInfoBase
        Implements INotifyPropertyChanged, IDataErrorInfo

        Private ReadOnly _errors As New Dictionary(Of String, String)

        Public ReadOnly Property [Error] As String Implements IDataErrorInfo.Error
            Get
                Return String.Join(Environment.NewLine, _errors.Values)
            End Get
        End Property

        Public ReadOnly Property Item(columnName As String) As String Implements IDataErrorInfo.Item
            Get
                If _errors.ContainsKey(columnName) Then
                    Return _errors(columnName)
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Protected Sub SetValidationError(propertyName As String, errorMessage As String)
            _errors(propertyName) = errorMessage
            OnPropertyChanged(NameOf([Error]))
            OnPropertyChanged(propertyName)
        End Sub

        Protected Sub ClearValidationError(propertyName As String)
            If _errors.ContainsKey(propertyName) Then
                _errors.Remove(propertyName)
                OnPropertyChanged(NameOf([Error]))
                OnPropertyChanged(propertyName)
            End If
        End Sub

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Sub OnPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

    End Class

End Namespace
