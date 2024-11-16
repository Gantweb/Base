Namespace AppTools.Wrapper
    Friend Module Base64Wrapper

        Friend Function FromBase64ToByte(txt As String) As Byte()
            Return Convert.FromBase64String(txt)
        End Function

        Friend Function FromBase64ToString(txt As String) As Object
            Dim res As Object
            Select Case True
                Case IsNumeric(txt)
                    res = Convert.ToDouble(UtfWrapper.FromUtf(Convert.FromBase64String(txt)))
                Case FromUtf(Convert.FromBase64String(txt)).Equals("true", StringComparison.CurrentCultureIgnoreCase) OrElse FromUtf(Convert.FromBase64String(txt)).Equals("false", StringComparison.CurrentCultureIgnoreCase)
                    res = Convert.ToBoolean(UtfWrapper.FromUtf(Convert.FromBase64String(txt)))
                Case Else
                    res = UtfWrapper.FromUtf(Convert.FromBase64String(txt))
            End Select
            Return res
        End Function

        Friend Function ToBase64String(txt As Object) As String
            Return Convert.ToBase64String(UtfWrapper.ToUtf(txt.ToString))
        End Function

        Friend Function ToBase64ToByte(txt As Byte()) As String
            Return Convert.ToBase64String(txt)
        End Function

    End Module
End Namespace