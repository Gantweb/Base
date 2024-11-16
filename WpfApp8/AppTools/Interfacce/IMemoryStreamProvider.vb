Imports System.IO

Namespace AppTools.Interfacce

    Public Interface IMemoryStreamProvider

        Function CreateMemoryStream() As MemoryStream

        Function CreateMemoryStream(buffer As Byte()) As MemoryStream

    End Interface

End Namespace