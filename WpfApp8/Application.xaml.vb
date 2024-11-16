Imports System.Globalization
Imports System.Threading
Imports System.Windows.Markup
Imports System.Windows.Threading
Imports WpfApp8.AppTools

Class Application

    Public Sub New()
        Dim culture As New CultureInfo("it-IT")
        culture.NumberFormat.NumberDecimalSeparator = ","
        culture.NumberFormat.CurrencyDecimalSeparator = ","
        culture.NumberFormat.PercentDecimalSeparator = ","
        Thread.CurrentThread.CurrentUICulture = culture
        Thread.CurrentThread.CurrentCulture = culture
        CultureInfo.DefaultThreadCurrentCulture = culture
        CultureInfo.DefaultThreadCurrentUICulture = culture
        FrameworkElement.LanguageProperty.OverrideMetadata(GetType(FrameworkElement),
                                                       New FrameworkPropertyMetadata(XmlLanguage.GetLanguage(culture.Name)))

        ' Registrare il gestore dell'evento GotFocus per tutti i TextBox
        EventManager.RegisterClassHandler(GetType(TextBox), TextBox.GotFocusEvent, New RoutedEventHandler(AddressOf OnTextBoxGotFocus))
        ' Registrare il gestore degli eventi per la selezione con mouse
        EventManager.RegisterClassHandler(GetType(TextBox), TextBox.PreviewMouseDownEvent, New MouseButtonEventHandler(AddressOf OnTextBoxPreviewMouseDown))
        EventManager.RegisterClassHandler(GetType(TextBox), TextBox.PreviewMouseUpEvent, New MouseButtonEventHandler(AddressOf OnTextBoxPreviewMouseUp))
        ' Registrare il gestore dell'evento per la selezione con Tab
        EventManager.RegisterClassHandler(GetType(TextBox), TextBox.PreviewGotKeyboardFocusEvent, New RoutedEventHandler(AddressOf OnTextBoxGotFocus))

        Imposta()
        Init.Controlla()
    End Sub

    Private Sub OnTextBoxGotFocus(sender As Object, e As RoutedEventArgs)
        SelezionaTutto(sender)
    End Sub

    Private Sub OnTextBoxPreviewMouseDown(sender As Object, e As MouseButtonEventArgs)
        Dim textBox As TextBox = TryCast(sender, TextBox)
        textBox.Focus()
    End Sub

    Private Sub OnTextBoxPreviewMouseUp(sender As Object, e As MouseButtonEventArgs)
        SelezionaTutto(sender)
    End Sub

    Private Sub SelezionaTutto(sender As Object)
        Dim textBox As TextBox = TryCast(sender, TextBox)
        textBox?.SelectAll()
    End Sub

    Private Sub Application_DispatcherUnhandledException(sender As Object, e As DispatcherUnhandledExceptionEventArgs) Handles Me.DispatcherUnhandledException
        Try
            Debug.WriteLine($"Errore archiviato: {e.Exception.Message}")
            Logs.Log(e.Exception)
        Catch ex As Exception
            Debug.WriteLine($"Errore durante la registrazione dell'eccezione: {ex.Message}")
        End Try
        e.Handled = True
    End Sub

    Public Shared Sub StartCountdown()
        Dim countdown As New Countdown()
        Dim callback As Action = Sub()
                                     Init.Controlla()
                                 End Sub
        countdown.StartCountdown(120, callback)
    End Sub

End Class