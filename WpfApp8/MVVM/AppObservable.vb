Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Reflection
Imports System.Runtime.CompilerServices

Namespace MVVM

    Public Class AppObservable
        Implements INotifyPropertyChanged, IDataErrorInfo

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Private ReadOnly _IsNotify As Boolean
        Private Shared ReadOnly _changeTracker As New ChangeTracker()

        Public ReadOnly Property [Error] As String Implements IDataErrorInfo.[Error]
            Get
                Dim errors = From prop In Me.GetType().GetProperties()
                             Let x = Me(prop.Name)
                             Where Not String.IsNullOrEmpty(x)
                             Select x

                Return If(errors.Count() > 0, errors(0), Nothing)
            End Get
        End Property

        Public Property IsChanged As Boolean

        Public ReadOnly Property IsNotify As Boolean
            Get
                Return _IsNotify
            End Get
        End Property

        Public Sub New(Optional isNoti As Boolean = True)
            _IsNotify = isNoti
            If isNoti Then _changeTracker.AddObservable(Me)
        End Sub

        <ObfuscationAttribute(Exclude:=True)>
        Default Public ReadOnly Property Item(columnName As String) As String Implements IDataErrorInfo.Item
            Get
                Dim prop = Me.GetType().GetProperty(columnName)
                If prop IsNot Nothing Then
                    Dim results As New List(Of ValidationResult)()
                    Dim context As New ValidationContext(Me) With {.MemberName = columnName}
                    Dim value = prop.GetValue(Me)
                    Dim isValid = Validator.TryValidateProperty(value, context, results)

                    If Not isValid AndAlso results.Count > 0 Then
                        Return results(0).ErrorMessage
                    End If
                End If
                Return Nothing
            End Get
        End Property

        Public Sub SetVal(Of T)(ByRef field As T, newValue As T, <CallerMemberName> Optional propertyName As String = Nothing)
            If EqualityComparer(Of T).[Default].Equals(field, newValue) Then Return

            field = newValue
            If _IsNotify Then
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
                IsChanged = True
            End If
        End Sub

        Public Function Validate() As List(Of ValidationResult)
            Dim results As New List(Of ValidationResult)()
            Dim context As New ValidationContext(Me)

            Validator.TryValidateObject(Me, context, results, validateAllProperties:=True)

            Return results
        End Function

        Protected Sub OnPropertyChanged(propertyName As String)
            If _IsNotify Then
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
            End If
        End Sub

        Public Shared Function GetChangeTracker() As ChangeTracker
            Return _changeTracker
        End Function

    End Class

End Namespace