Imports WpfApp7.ViewModel

Class MainWindow

    Private mv As New Customer

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        DataContext = mv

        ' Inizializzare il controllo ElencoErrori
        'ErrorsControl.Initialize(mv)
    End Sub

End Class
