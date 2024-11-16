Imports System.Collections.ObjectModel
Imports System.Globalization

Namespace MVVM.Converters
    Public Class ValidationErrorConverter
        Implements IMultiValueConverter

        Public Function Convert(values As Object(), targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
            Dim hasErrors As Boolean = DirectCast(values(0), Boolean)
            If hasErrors Then
                Dim validationErrors As ReadOnlyObservableCollection(Of ValidationError) = DirectCast(values(1), ReadOnlyObservableCollection(Of ValidationError))
                If validationErrors IsNot Nothing AndAlso validationErrors.Count > 0 Then
                    Return validationErrors(0).ErrorContent
                End If
            End If
            Return Nothing
        End Function

        Public Function ConvertBack(value As Object, targetTypes As Type(), parameter As Object, culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace