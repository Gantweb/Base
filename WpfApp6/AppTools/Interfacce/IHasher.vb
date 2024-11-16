Imports System.Security.Cryptography

Namespace AppTools.Interfacce

    Public Interface IHasher

        Function ComputeHash(data As Byte()) As Byte()

        Function ComputeHash(data As String, algorithm As HashAlgorithmName) As String
        Function ComputeHashBytes(input As String) As Byte()
    End Interface

End Namespace