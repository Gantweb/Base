Imports System.Security.Cryptography
Imports WpfApp8.AppTools.Interfacce

Namespace AppTools

    Public Class Hasher
        Implements IHasher

        Public Function ComputeHash(data As Byte()) As Byte() Implements IHasher.ComputeHash
            Dim A_2 As Byte()
            Using A_3 = SHA256.Create()
                A_2 = A_3.ComputeHash(data)
            End Using
            Return A_2
        End Function

        Public Function ComputeHash(data As String) As String Implements IHasher.ComputeHash
            Dim A_2 As Byte() = _b64.StringToBytes(data)
            Dim A_3 As Byte() = ComputeHash(A_2)
            Return _b64.BytesToString(A_3).Replace("-", "").ToLowerInvariant()
        End Function

    End Class

End Namespace