Imports System.Text

Namespace AppTools.Wrapper
    Friend Module UtfWrapper
        Friend Function FromUtf(txt As Byte()) As String
            Return Encoding.UTF8.GetString(txt)
        End Function

        Friend Function ToUtf(txt As String) As Byte()
            Return Encoding.UTF8.GetBytes(txt)
        End Function
    End Module
End Namespace
