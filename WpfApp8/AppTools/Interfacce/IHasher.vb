Namespace AppTools.Interfacce

    Public Interface IHasher

        Function ComputeHash(data As Byte()) As Byte()

        Function ComputeHash(data As String) As String

    End Interface

End Namespace