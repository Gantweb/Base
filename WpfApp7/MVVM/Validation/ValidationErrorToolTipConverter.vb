Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.Windows.Controls
Imports System.Windows.Data

Namespace MVVM.Validation

    Public Class ValidationErrorToolTipConverter
        Implements IMultiValueConverter

        Public Function Convert(values As Object(), targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
            Dim errors = TryCast(values(0), ReadOnlyObservableCollection(Of ValidationErrors))
            If errors IsNot Nothing AndAlso errors.Count > 0 Then
                Return errors(0).ErrorMessage ' Usa ErrorMessage invece di ErrorContent
            End If
            Return Nothing
        End Function

        Public Function ConvertBack(value As Object, targetTypes As Type(), parameter As Object, culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

    End Class

End Namespace
