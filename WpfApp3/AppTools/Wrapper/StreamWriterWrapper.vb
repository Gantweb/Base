Imports System.IO

Namespace AppTools.Wrapper

    Public Class StreamWriterWrapper
        Implements IDisposable

        Private sw As StreamWriter

        Public Sub New(cs As Stream)
            sw = New StreamWriter(cs)
        End Sub

        Public Sub Write(data As String)
            sw.Write(data)
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            sw.Dispose()
        End Sub

    End Class

End Namespace