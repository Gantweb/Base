Imports System.Security.Cryptography
Imports System.Text

Namespace AppTools.Wrapper

    Public Class SHA256Wrapper

        Private sha256 As SHA256

        Public Sub New()
            sha256 = SHA256.Create()
        End Sub

        Public Function ComputeHash(data As String) As Byte()
            Return sha256.ComputeHash(Encoding.UTF8.GetBytes(data))
        End Function

    End Class

End Namespace