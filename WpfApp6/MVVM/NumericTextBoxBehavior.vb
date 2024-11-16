Namespace MVVM

    Public Class NumericTextBoxBehavior
        Inherits DependencyObject

        ' Registrare come AttachedProperty
        Public Shared ReadOnly IsEnabledProperty As DependencyProperty = DependencyProperty.RegisterAttached(
        "IsEnabled", GetType(Boolean), GetType(NumericTextBoxBehavior), New PropertyMetadata(False, AddressOf OnIsEnabledChanged))

        ' Getter per l'AttachedProperty
        Public Shared Function GetIsEnabled(d As DependencyObject) As Boolean
            Return CType(d.GetValue(IsEnabledProperty), Boolean)
        End Function

        ' Setter per l'AttachedProperty
        Public Shared Sub SetIsEnabled(d As DependencyObject, value As Boolean)
            d.SetValue(IsEnabledProperty, value)
        End Sub

        ' Gestore del cambiamento di IsEnabled
        Private Shared Sub OnIsEnabledChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
            Dim textBox As TextBox = TryCast(d, TextBox)
            If textBox IsNot Nothing Then
                If CType(e.NewValue, Boolean) Then
                    AddHandler textBox.PreviewKeyDown, AddressOf TextBox_PreviewKeyDown
                Else
                    RemoveHandler textBox.PreviewKeyDown, AddressOf TextBox_PreviewKeyDown
                End If
            End If
        End Sub

        ' Gestore per PreviewKeyDown
        Private Shared Sub TextBox_PreviewKeyDown(sender As Object, e As KeyEventArgs)
            Dim tb As TextBox = CType(sender, TextBox)
            Dim cubi As BindingExpression = tb.GetBindingExpression(TextBox.TextProperty)

            If cubi IsNot Nothing Then
                Dim bp As Type = cubi.ResolvedSource.GetType().GetProperty(cubi.ResolvedSourcePropertyName).PropertyType
                If IsNumericType(bp) Then
                    If e.Key >= Key.A AndAlso e.Key <= Key.Z Then
                        e.Handled = True
                    ElseIf e.Key = Key.OemPeriod OrElse e.Key = Key.Decimal Then
                        Dim caretIndex As Integer = tb.CaretIndex
                        tb.Text = tb.Text.Insert(caretIndex, ",")
                        tb.CaretIndex = caretIndex + 1
                        e.Handled = True
                    End If
                End If
            End If
        End Sub

        ' Funzione per controllare se il tipo è numerico
        Private Shared Function IsNumericType(type As Type) As Boolean
            Return type Is GetType(Integer) OrElse type Is GetType(Double) OrElse type Is GetType(Decimal)
        End Function

    End Class

End Namespace
