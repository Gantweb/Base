Imports System.Windows.Input

Namespace MVVM

    Public Class CompositeCommand
        Implements ICommand

        Private ReadOnly _commands As New List(Of ICommand)

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public Sub AddCommand(command As ICommand)
            _commands.Add(command)
            AddHandler command.CanExecuteChanged, AddressOf OnCanExecuteChanged
        End Sub

        Public Sub RemoveCommand(command As ICommand)
            If _commands.Remove(command) Then
                RemoveHandler command.CanExecuteChanged, AddressOf OnCanExecuteChanged
            End If
        End Sub

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            Return _commands.Any(Function(c) c.CanExecute(parameter))
        End Function

        Public Sub Execute(parameter As Object) Implements ICommand.Execute
            For Each Commands In _commands
                If Commands.CanExecute(parameter) Then
                    Commands.Execute(parameter)
                End If
            Next
        End Sub

        Private Sub OnCanExecuteChanged()
            RaiseEvent CanExecuteChanged(Me, EventArgs.Empty)
        End Sub

    End Class

End Namespace

