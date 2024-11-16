Imports System.IO

Namespace AppTools.Wrapper
    Friend Class MemoryStreamWrapper
        Inherits MemoryStream

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(buffer As Byte())
            MyBase.New(buffer)
        End Sub

        Public Function ToArrays() As Byte()
            Return MyBase.ToArray()
        End Function
    End Class
End Namespace
