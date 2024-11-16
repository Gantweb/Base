Namespace AppTools.Interfacce

    Public Interface IBase64

        Function StringToBase64(input As String) As String

        Function Base64ToString(encoded As String) As String

        Function BytesToBase64(data As Byte()) As String

        Function Base64ToBytes(encoded As String) As Byte()

        Function StringToBytes(input As String) As Byte()

        Function BytesToString(data As Byte()) As String

    End Interface

End Namespace
