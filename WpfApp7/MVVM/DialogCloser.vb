Imports System.Windows

Namespace MVVM

    Public Class DialogCloser

        Public Shared ReadOnly DialogResultProperty As DependencyProperty =
            DependencyProperty.RegisterAttached(
                "DialogResult",
                GetType(Nullable(Of Boolean)),
                GetType(DialogCloser),
                New PropertyMetadata(AddressOf OnDialogResultChanged))

        Private Shared Sub OnDialogResultChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
            Dim window = TryCast(d, Window)
            If window IsNot Nothing Then
                window.DialogResult = DirectCast(e.NewValue, Nullable(Of Boolean))
                If window.DialogResult.HasValue Then
                    window.Close()
                End If
            End If
        End Sub

        Public Shared Function GetDialogResult(target As DependencyObject) As Nullable(Of Boolean)
            Return DirectCast(target.GetValue(DialogResultProperty), Nullable(Of Boolean))
        End Function

        Public Shared Sub SetDialogResult(target As DependencyObject, value As Nullable(Of Boolean))
            target.SetValue(DialogResultProperty, value)
        End Sub

    End Class

End Namespace
