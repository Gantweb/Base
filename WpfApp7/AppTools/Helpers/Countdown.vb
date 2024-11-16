Imports System.Timers

Namespace AppTools.Helpers
Public Class Countdown

    Private _timer As Timer
    Private _callback As Action

    Public Sub StartCountdown(seconds As Integer, callback As Action)
        _callback = callback
        _timer = New Timer(seconds * 1000)
        AddHandler _timer.Elapsed, AddressOf OnTimedEvent
        _timer.AutoReset = False
        _timer.Enabled = True
    End Sub

    Private Sub OnTimedEvent(source As Object, e As ElapsedEventArgs)
        _timer.Stop()
        _callback.Invoke()
    End Sub

End Class
End Namespace