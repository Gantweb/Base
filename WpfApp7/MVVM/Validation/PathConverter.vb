Imports System
Imports System.Windows.Data

Namespace MVVM.Validation

    Public Class PathConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            ' Logica di conversione personalizzata
            Return value?.ToString()?.Replace("/", "\")
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            ' Logica di conversione inversa personalizzata
            Return value?.ToString()?.Replace("\", "/")
        End Function

    End Class

End Namespace
