Imports System.Windows.Input

Namespace MVVM

    Public Class CommandManagerHelper

        Public Shared Sub AddHandlers(command As ICommand, canExecuteChangedHandler As EventHandler)
            AddHandler command.CanExecuteChanged, canExecuteChangedHandler
        End Sub

        Public Shared Sub RemoveHandlers(command As ICommand, canExecuteChangedHandler As EventHandler)
            RemoveHandler command.CanExecuteChanged, canExecuteChangedHandler
        End Sub

    End Class

End Namespace
