Imports System.Windows
Imports System.Windows.Input
Imports Microsoft.Xaml.Behaviors

Namespace MVVM

    Public Class EventToCommandBehavior
        Inherits Behavior(Of UIElement)

        Public Shared ReadOnly CommandProperty As DependencyProperty =
            DependencyProperty.Register("Command", GetType(ICommand), GetType(EventToCommandBehavior), New PropertyMetadata(Nothing))

        Public Property Command As ICommand
            Get
                Return CType(GetValue(CommandProperty), ICommand)
            End Get
            Set(value As ICommand)
                SetValue(CommandProperty, value)
            End Set
        End Property

        Public Shared ReadOnly CommandParameterProperty As DependencyProperty =
            DependencyProperty.Register("CommandParameter", GetType(Object), GetType(EventToCommandBehavior), New PropertyMetadata(Nothing))

        Public Property CommandParameter As Object
            Get
                Return GetValue(CommandParameterProperty)
            End Get
            Set(value As Object)
                SetValue(CommandParameterProperty, value)
            End Set
        End Property

        Public Shared ReadOnly EventNameProperty As DependencyProperty =
            DependencyProperty.Register("EventName", GetType(String), GetType(EventToCommandBehavior), New PropertyMetadata(Nothing, AddressOf OnEventNameChanged))

        Public Property EventName As String
            Get
                Return CType(GetValue(EventNameProperty), String)
            End Get
            Set(value As String)
                SetValue(EventNameProperty, value)
            End Set
        End Property

        Private Shared Sub OnEventNameChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
            Dim behavior = CType(d, EventToCommandBehavior)
            If behavior.AssociatedObject IsNot Nothing Then
                behavior.AttachHandler(If(e.OldValue?.ToString(), String.Empty), If(e.NewValue?.ToString(), String.Empty))
            End If
        End Sub

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AttachHandler(String.Empty, EventName)
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            AttachHandler(EventName, String.Empty)
        End Sub

        Private Sub AttachHandler(ByVal oldEventName As String, ByVal newEventName As String)
            If Not String.IsNullOrEmpty(oldEventName) Then
                RemoveEventHandler(oldEventName)
            End If

            If Not String.IsNullOrEmpty(newEventName) Then
                AddEventHandler(newEventName)
            End If
        End Sub

        Private Sub AddEventHandler(ByVal eventName As String)
            Dim eventInfo = AssociatedObject.GetType().GetEvent(eventName)
            If eventInfo IsNot Nothing Then
                Dim handler = GetType(EventToCommandBehavior).GetMethod("OnEventRaised", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
                Dim eventHandler = [Delegate].CreateDelegate(eventInfo.EventHandlerType, Me, handler)
                eventInfo.AddEventHandler(AssociatedObject, eventHandler)
            End If
        End Sub

        Private Sub RemoveEventHandler(ByVal eventName As String)
            Dim eventInfo = AssociatedObject.GetType().GetEvent(eventName)
            If eventInfo IsNot Nothing Then
                Dim handler = GetType(EventToCommandBehavior).GetMethod("OnEventRaised", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
                Dim eventHandler = [Delegate].CreateDelegate(eventInfo.EventHandlerType, Me, handler)
                eventInfo.RemoveEventHandler(AssociatedObject, eventHandler)
            End If
        End Sub

        Private Sub OnEventRaised(sender As Object, e As EventArgs)
            If Command IsNot Nothing AndAlso Command.CanExecute(CommandParameter) Then
                Command.Execute(CommandParameter)
            End If
        End Sub

    End Class

End Namespace
