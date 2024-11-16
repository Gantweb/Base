Imports System.Reflection

Namespace Model.App

    Public Class LavoroBase
        Inherits MVVM.AppObservable

        Private _Nome As String = "Nuovo Lavoro"
        Private _Pat As String = ""
        Private _Titolo As String = ""

        <ObfuscationAttribute(Exclude:=True)>
        Public Property Nome As String
            Get
                Return _Nome
            End Get
            Private Set
                SetVal(_Nome, Value)
                Titolo = _Nome
            End Set
        End Property

        <ObfuscationAttribute(Exclude:=True)>
        Public Property Pat As String
            Get
                Return _Pat
            End Get
            Set
                SetVal(_Pat, Value)
                Nome = IO.Path.GetFileNameWithoutExtension(Value)
            End Set
        End Property

        <ObfuscationAttribute(Exclude:=True)>
        Public Property Titolo As String
            Get
                _Titolo = $"{Base("nome")} [{_Nome}]"
                Return _Titolo
            End Get
            Private Set
                SetVal(_Titolo, Value)
            End Set
        End Property

    End Class

End Namespace