Namespace AppTools.Interfacce

    Public Interface IBase64

        Function StringToBase64(input As String) As String

        Function Base64ToString(base64 As String) As String

        Function BytesToBase64(bytes As Byte()) As String

        Function Base64ToBytes(base64 As String) As Byte()

        Function StringToBytes(input As String) As Byte()

        Function BytesToString(bytes As Byte()) As String

    End Interface

End Namespace