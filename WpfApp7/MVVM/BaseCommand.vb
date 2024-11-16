Imports System.Windows.Input

Namespace MVVM

    Public MustInherit Class BaseCommand
        Implements ICommand

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public MustOverride Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute

        Public MustOverride Sub Execute(parameter As Object) Implements ICommand.Execute

        Protected Sub OnCanExecuteChanged()
            RaiseEvent CanExecuteChanged(Me, EventArgs.Empty)
        End Sub

    End Class

End Namespace

