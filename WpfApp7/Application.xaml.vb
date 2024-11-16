﻿Imports System.Windows.Threading
Imports WpfApp6.AppTools
Imports WpfApp7.AppTools.Helpers

Class Application

    Public Sub New()
        InitializeApplication()
        Imposta()
        Init.Verifica()
    End Sub

    Private Sub InitializeApplication()
        ' Esegui l'inizializzazione dell'applicazione qui.
    End Sub

    Public Shared Sub StartCountdown()
        Dim countdown As New Countdown()
        Dim callback As Action = Sub()
                                     Init.Controlla()
                                 End Sub
        countdown.StartCountdown(180, callback)
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

End Class