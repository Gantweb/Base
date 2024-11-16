Imports System.Threading

Class Application

    Public Sub New()
        Imposta()
        Init.Verifica()
    End Sub

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

    Public Shared Sub StartCountdown()
        Dim timer As Timer = Nothing
        Dim timerCallback As TimerCallback = Sub(state)
                                                 timer.Dispose()
                                                 Init.Controlla()
                                             End Sub
        timer = New Timer(timerCallback, Nothing, 180000, Timeout.Infinite)
    End Sub

End Class