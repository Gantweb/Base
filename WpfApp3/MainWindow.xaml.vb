Class MainWindow
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        MsgBox(Init.ToJson)
    End Sub
End Class