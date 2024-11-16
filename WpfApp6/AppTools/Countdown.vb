Imports System.Threading

Namespace AppTools

    Public Class Countdown
        Private _timer As Timer

        Public Sub New()
        End Sub

        Public Sub StartCountdown(interval As Integer, callback As Action)
            Dim timerCallback As TimerCallback = Sub(state)
                                                     _timer.Dispose()
                                                     callback()
                                                 End Sub
            _timer = New Timer(timerCallback, Nothing, interval * 1000, Timeout.Infinite)
        End Sub

    End Class

End Namespace