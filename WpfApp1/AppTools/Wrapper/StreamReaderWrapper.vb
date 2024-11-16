Imports System.IO

Namespace AppTools.Wrapper

    Friend Class StreamReaderWrapper
        Inherits StreamReader

        Friend Sub New(stream As Stream)
            MyBase.New(stream)
        End Sub

        Friend Sub New(path As String)
            MyBase.New(path)
        End Sub

        Friend Sub New(stream As Stream, detectEncodingFromByteOrderMarks As Boolean)
            MyBase.New(stream, detectEncodingFromByteOrderMarks)
        End Sub

        Friend Sub New(stream As Stream, encoding As Text.Encoding)
            MyBase.New(stream, encoding)
        End Sub

        Friend Sub New(path As String, detectEncodingFromByteOrderMarks As Boolean)
            MyBase.New(path, detectEncodingFromByteOrderMarks)
        End Sub

        Friend Sub New(path As String, options As FileStreamOptions)
            MyBase.New(path, options)
        End Sub

        Friend Sub New(path As String, encoding As Text.Encoding)
            MyBase.New(path, encoding)
        End Sub

        Friend Sub New(stream As Stream, encoding As Text.Encoding, detectEncodingFromByteOrderMarks As Boolean)
            MyBase.New(stream, encoding, detectEncodingFromByteOrderMarks)
        End Sub

        Friend Sub New(path As String, encoding As Text.Encoding, detectEncodingFromByteOrderMarks As Boolean)
            MyBase.New(path, encoding, detectEncodingFromByteOrderMarks)
        End Sub

        Friend Sub New(stream As Stream, encoding As Text.Encoding, detectEncodingFromByteOrderMarks As Boolean, bufferSize As Integer)
            MyBase.New(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize)
        End Sub

        Friend Sub New(path As String, encoding As Text.Encoding, detectEncodingFromByteOrderMarks As Boolean, bufferSize As Integer)
            MyBase.New(path, encoding, detectEncodingFromByteOrderMarks, bufferSize)
        End Sub

        Friend Sub New(path As String, encoding As Text.Encoding, detectEncodingFromByteOrderMarks As Boolean, options As FileStreamOptions)
            MyBase.New(path, encoding, detectEncodingFromByteOrderMarks, options)
        End Sub

        Friend Sub New(stream As Stream, Optional encoding As Text.Encoding = Nothing, Optional detectEncodingFromByteOrderMarks As Boolean = True, Optional bufferSize As Integer = -1, Optional leaveOpen As Boolean = False)
            MyBase.New(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen)
        End Sub

        Friend Function Leggi() As String
            Return ReadToEnd()
        End Function

    End Class

End Namespace