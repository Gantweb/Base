Imports System.IO

Namespace AppTools.Wrapper

    Public Class StreamReaderWrapper
        Implements IDisposable

        Private sr As StreamReader

        Public Sub New(cs As Stream)
            sr = New StreamReader(cs)
        End Sub

        Public Function ReadToEnd() As String
            Return sr.ReadToEnd()
        End Function

        Public Sub Dispose() Implements IDisposable.Dispose
            sr.Dispose()
        End Sub

    End Class

End Namespace