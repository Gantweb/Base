Imports System.Reflection

Namespace AppTools

    <ObfuscationAttribute(Exclude:=True)>
    Public Class NumericInputBehavior

        ' Definizione della Attached Property IsNumeric
        Public Shared ReadOnly IsNumericProperty As DependencyProperty = DependencyProperty.RegisterAttached(
            "IsNumeric", GetType(Boolean), GetType(NumericInputBehavior), New PropertyMetadata(False, AddressOf OnIsNumericChanged))

        Public Shared Sub SetIsNumeric(element As UIElement, value As Boolean)
            element.SetValue(IsNumericProperty, value)
        End Sub

        Public Shared Function GetIsNumeric(element As UIElement) As Boolean
            Return CBool(element.GetValue(IsNumericProperty))
        End Function

        Private Shared Sub OnIsNumericChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
            Dim textBox As TextBox = TryCast(d, TextBox)
            If textBox IsNot Nothing Then
                If CBool(e.NewValue) Then
                    AddHandler textBox.PreviewKeyDown, AddressOf TextBox_PreviewKeyDown
                Else
                    RemoveHandler textBox.PreviewKeyDown, AddressOf TextBox_PreviewKeyDown
                End If
            End If
        End Sub

        Private Shared Sub TextBox_PreviewKeyDown(sender As Object, e As KeyEventArgs)
            Dim textBox As TextBox = DirectCast(sender, TextBox)
            Dim bindingExpression As BindingExpression = textBox.GetBindingExpression(TextBox.TextProperty)
            Dim propertyType As Type = bindingExpression.ResolvedSource.GetType().GetProperty(bindingExpression.ResolvedSourcePropertyName).PropertyType

            If IsNumericType(propertyType) Then
                If e.Key >= Key.A AndAlso e.Key <= Key.Z Then
                    e.Handled = True
                ElseIf e.Key = Key.OemPeriod OrElse e.Key = Key.Decimal Then
                    Dim caretIndex As Integer = textBox.CaretIndex
                    textBox.Text = textBox.Text.Insert(caretIndex, ",")
                    textBox.CaretIndex = caretIndex + 1
                    e.Handled = True
                End If
            End If
        End Sub

        Private Shared Function IsNumericType(type As Type) As Boolean
            Return type Is GetType(Integer) OrElse
                   type Is GetType(Double) OrElse
                   type Is GetType(Decimal) OrElse
                   type Is GetType(Single) OrElse
                   type Is GetType(Long) OrElse
                   type Is GetType(Short)
        End Function

    End Class

End Namespace