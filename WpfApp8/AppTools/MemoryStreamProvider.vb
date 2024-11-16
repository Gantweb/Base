Imports System.IO

Namespace AppTools

    Public Class MemoryStreamProvider
        Implements AppTools.Interfacce.IMemoryStreamProvider

        Public Function CreateMemoryStream() As MemoryStream Implements AppTools.Interfacce.IMemoryStreamProvider.CreateMemoryStream
            Return New MemoryStream()
        End Function

        Public Function CreateMemoryStream(buffer As Byte()) As MemoryStream Implements AppTools.Interfacce.IMemoryStreamProvider.CreateMemoryStream
            Return New MemoryStream(buffer)
        End Function

    End Class

End Namespace