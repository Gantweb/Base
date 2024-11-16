Imports System.ComponentModel.DataAnnotations
Imports WpfApp6.MVVM
Imports WpfApp7.MVVM

Namespace ViewModel

    Public Class Customer
        Inherits AppObservable

        Private _name As String
        Private _age As Double

        <Display(Name:="Nome")>
        <Required(ErrorMessage:="Il nome è obbligatorio")>
        <StringLength(1, ErrorMessage:="Il nome non può superare i 50 caratteri")>
        Public Property Name As String
            Get
                Return _name
            End Get
            Set(value As String)
                _name = value
                OnPropertyChanged(NameOf(Name))
            End Set
        End Property

        <Display(Name:="Eta")>
        <Required(ErrorMessage:="L'eta è obbligatoria")>
        <Range(5, 6, ErrorMessage:="Il nome non può superare i 50 caratteri")>
        Public Property Age As Double
            Get
                Return _age
            End Get
            Set(value As Double)
                _age = value
                OnPropertyChanged(NameOf(Age))
            End Set
        End Property

        Public Sub New(Optional isNotify As Boolean = True)
            MyBase.New(isNotify)
        End Sub

    End Class

End Namespace