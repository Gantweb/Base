Imports System.ComponentModel.DataAnnotations
Imports WpfApp6.MVVM
Imports WpfApp7.MVVM

Namespace ViewModel

    Public Class Order
        Inherits AppObservable

        Private _description As String

        <Required(ErrorMessage:="La descrizione è obbligatoria")>
        <StringLength(100, ErrorMessage:="La descrizione non può superare i 100 caratteri")>
        Public Property Description As String
            Get
                Return _description
            End Get
            Set(value As String)
                _description = value
                OnPropertyChanged(NameOf(Description))
            End Set
        End Property

        Public Sub New(Optional isNotify As Boolean = True)
            MyBase.New(isNotify)
        End Sub

    End Class

End Namespace