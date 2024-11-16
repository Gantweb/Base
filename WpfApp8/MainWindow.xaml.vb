Imports System.ComponentModel
Imports WpfApp8.Model
Imports WpfApp8.MVVM
Imports WpfApp8.MVVM.ViewModel

Class MainWindow

    Private mv As New MainView(Me)

    Public Sub New()
        ' La chiamata è richiesta dalla finestra di progettazione.
        InitializeComponent()

        DataContext = mv
    End Sub

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ErrorsControl.Initialize(mv.Lavoro)
    End Sub

    Friend Sub MainWindow_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Dim trc As ChangeTracker = AppObservable.GetChangeTracker()
        e.Cancel = mv.VerificaLavoro(trc)
    End Sub

End Class