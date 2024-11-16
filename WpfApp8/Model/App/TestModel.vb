Imports System.ComponentModel.DataAnnotations
Imports System.Reflection
Imports WpfApp8.MVVM

Namespace Model.App

    Public Class TestModel
        Inherits AppObservable

        Private _doubleProperty As Double
        Private _integerProperty As Integer
        Private _stringProperty As String

        <ObfuscationAttribute(Exclude:=True)>
        <Required(ErrorMessage:="Il valore double è obbligatorio.")>
        <Range(0.1, 10.0, ErrorMessage:="Il valore double deve essere compreso tra 0.1 e 10.0.")>
        <Display(Name:="Valore Double")>
        Public Property DoubleProperty As Double
            Get
                Return _doubleProperty
            End Get
            Set(value As Double)
                SetVal(_doubleProperty, value)
            End Set
        End Property

        <ObfuscationAttribute(Exclude:=True)>
        <Required(ErrorMessage:="Il valore intero è obbligatorio.")>
        <Range(1, 100, ErrorMessage:="Il valore intero deve essere compreso tra 1 e 100.")>
        <Display(Name:="Valore Intero")>
        Public Property IntegerProperty As Integer
            Get
                Return _integerProperty
            End Get
            Set(value As Integer)
                SetVal(_integerProperty, value)
            End Set
        End Property

        <ObfuscationAttribute(Exclude:=True)>
        <Required(ErrorMessage:="La stringa è obbligatoria.")>
        <StringLength(50, ErrorMessage:="La lunghezza della stringa non può superare i 50 caratteri.")>
        <Display(Name:="Testo")>
        Public Property StringProperty As String
            Get
                Return _stringProperty
            End Get
            Set(value As String)
                SetVal(_stringProperty, value)
            End Set
        End Property

    End Class

End Namespace