Imports System.Collections.ObjectModel

Namespace MVVM

    Public Class ValidationErrors
        Inherits ObservableCollection(Of String)

        Private ReadOnly _propertyName As String

        Public Sub New(propertyName As String)
            _propertyName = propertyName
        End Sub

        Public ReadOnly Property PropertyName As String
            Get
                Return _propertyName
            End Get
        End Property

    End Class

End Namespace
