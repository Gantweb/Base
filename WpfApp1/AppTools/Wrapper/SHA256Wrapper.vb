
Imports System.Security.Cryptography
Imports System.Text

Namespace AppTools.Wrapper
    Friend Module SHA256Wrapper
        Friend Function ComputeHash(data As String) As String
            Using sha256 As SHA256 = SHA256.Create()
                Dim bytes As Byte() = Encoding.UTF8.GetBytes(data)
                Dim hash As Byte() = sha256.ComputeHash(bytes)
                Return BitConverter.ToString(hash).Replace("-", String.Empty)
            End Using
        End Function
    End Module
End Namespace
