Namespace AppTools.Interfacce

    Public Interface IStringCompressor

        Function Compress(input As String) As String

        Function Decompress(compressed As String) As String

    End Interface

End Namespace