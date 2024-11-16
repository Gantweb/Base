Imports System.Globalization
Imports System.Windows
Imports System.Windows.Data

Namespace MVVM

    Public Class BoolToVisibilityConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is Boolean AndAlso CType(value, Boolean) Then
                Return Visibility.Visible
            Else
                Return Visibility.Collapsed
            End If
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return TypeOf value Is Visibility AndAlso CType(value, Visibility) = Visibility.Visible
        End Function

    End Class

End Namespace
