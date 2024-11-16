Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

Namespace MVVM

    Public Class AppObservable
        Implements INotifyPropertyChanged, IDataErrorInfo

        Private ReadOnly _IsNotify As Boolean

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public ReadOnly Property [Error] As String Implements IDataErrorInfo.[Error]
            Get
                ' Ritorna il primo errore riscontrato tra le proprietà
                Dim errors = From prop In Me.GetType().GetProperties()
                             Let x = Me(prop.Name)
                             Where Not String.IsNullOrEmpty(x)
                             Select x

                Return If(errors.Count() > 0, errors(0), Nothing)
            End Get
        End Property

        Public ReadOnly Property IsNotify As Boolean
            Get
                Return _IsNotify
            End Get
        End Property

        Public Sub New(Optional isNotify As Boolean = True)
            _IsNotify = isNotify
        End Sub

        Default Public ReadOnly Property Item(columnName As String) As String Implements IDataErrorInfo.Item
            Get
                ' Ottiene la proprietà della classe con il nome specificato
                Dim prop = Me.GetType().GetProperty(columnName)
                If prop IsNot Nothing Then
                    ' Valida la proprietà utilizzando le data annotation
                    Dim results As New List(Of ValidationResult)()
                    Dim context As New ValidationContext(Me) With {.MemberName = columnName}
                    Dim value = prop.GetValue(Me)
                    Dim isValid = Validator.TryValidateProperty(value, context, results)

                    ' Ritorna il messaggio di errore se la validazione fallisce
                    If Not isValid AndAlso results.Count > 0 Then
                        Return results(0).ErrorMessage
                    End If
                End If
                Return Nothing
            End Get
        End Property

        ' Metodo per notificare i cambiamenti delle proprietà
        Protected Sub OnPropertyChanged(propertyName As String)
            If _IsNotify Then
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
            End If
        End Sub

        ' Metodo per validare tutte le proprietà dell'oggetto utilizzando le data annotation
        Public Function Validate() As List(Of ValidationResult)
            Dim results As New List(Of ValidationResult)()
            Dim context As New ValidationContext(Me)

            Validator.TryValidateObject(Me, context, results, validateAllProperties:=True)

            Return results
        End Function

    End Class

End Namespace