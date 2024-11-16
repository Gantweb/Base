Imports System.IO
Imports Microsoft.Win32.SafeHandles

Namespace AppTools.Wrapper

    Friend Class FileStreamWrapper
        Inherits FileStream

        Friend Sub New(handle As SafeFileHandle, access As FileAccess)
            MyBase.New(handle, access)
        End Sub

        Friend Sub New(path As String, mode As FileMode)
            MyBase.New(path, mode)
        End Sub

        Friend Sub New(path As String, options As FileStreamOptions)
            MyBase.New(path, options)
        End Sub

        Friend Sub New(handle As SafeFileHandle, access As FileAccess, bufferSize As Integer)
            MyBase.New(handle, access, bufferSize)
        End Sub

        Friend Sub New(path As String, mode As FileMode, access As FileAccess)
            MyBase.New(path, mode, access)
        End Sub

        Friend Sub New(path As String, tipo As Boolean)
            MyClass.New(path, If(tipo, 2, 3), If(tipo, 2, 1))
        End Sub

        Friend Sub New(handle As SafeFileHandle, access As FileAccess, bufferSize As Integer, isAsync As Boolean)
            MyBase.New(handle, access, bufferSize, isAsync)
        End Sub

        Friend Sub New(path As String, mode As FileMode, access As FileAccess, share As FileShare)
            MyBase.New(path, mode, access, share)
        End Sub

        Friend Sub New(path As String, mode As FileMode, access As FileAccess, share As FileShare, bufferSize As Integer)
            MyBase.New(path, mode, access, share, bufferSize)
        End Sub

        Friend Sub New(path As String, mode As FileMode, access As FileAccess, share As FileShare, bufferSize As Integer, useAsync As Boolean)
            MyBase.New(path, mode, access, share, bufferSize, useAsync)
        End Sub

        Friend Sub New(path As String, mode As FileMode, access As FileAccess, share As FileShare, bufferSize As Integer, options As FileOptions)
            MyBase.New(path, mode, access, share, bufferSize, options)
        End Sub

        Friend Sub Scrivi(data() As Byte)
            Write(data, 0, data.Length)
        End Sub

    End Class

End Namespace