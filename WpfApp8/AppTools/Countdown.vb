Imports System.Threading

Namespace AppTools

    Public Class Countdown

        Private timer As Timer

        Public Sub StartCountdown(durationInSeconds As Integer, callback As Action)
            Dim A_3 As New CountdownState(durationInSeconds, callback)
            timer = New Timer(AddressOf TimerCallback, A_3, 0, 1000)
        End Sub

        Private Sub TimerCallback(state As Object)
            Dim A_2 As CountdownState = CType(state, CountdownState)
            A_2.SecondsLeft -= 1

            If A_2.SecondsLeft <= 0 Then
                timer.Dispose()
                A_2.Callback.Invoke()
            End If
        End Sub

        Private Class CountdownState
            Public Property SecondsLeft As Integer
            Public Property Callback As Action

            Public Sub New(secondsLeft As Integer, callback As Action)
                Me.SecondsLeft = secondsLeft
                Me.Callback = callback
            End Sub

        End Class

    End Class

End Namespace