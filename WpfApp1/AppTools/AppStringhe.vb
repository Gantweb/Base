Imports WpfApp1.AppTools.Wrapper

Namespace AppTools
    Friend Module AppStringhe

        Dim byteArray As Byte()
        Dim contenuto As String

        Friend Function StringZip(input As String) As String
            byteArray = ToUtf(input)
            Using A_2 As New MemoryStreamWrapper()
                Using A_3 As New GZipStreamWrapper(A_2, 1)
                    A_3.Scrivi(byteArray)
                End Using
                contenuto = ToBase64ToByte(A_2.ToArrays())
            End Using
            Return contenuto
        End Function

        Friend Function StringUnzip(compressedInput As String) As String
            byteArray = FromBase64ToByte(compressedInput)
            Using A_2 As New MemoryStreamWrapper(byteArray)
                Using A_3 As New GZipStreamWrapper(A_2, 0)
                    Using A_4 As New MemoryStreamWrapper()
                        A_3.CopyTos(A_4)
                        contenuto = FromUtf(A_4.ToArrays())
                    End Using
                End Using
            End Using
            Return contenuto
        End Function

        Friend Function ComputeHash(data As String) As String
            Return SHA256Wrapper.ComputeHash(data).ToUpperInvariant()
        End Function

    End Module
End Namespace