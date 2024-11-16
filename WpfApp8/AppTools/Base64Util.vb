Imports System.Text
Imports WpfApp8.AppTools.Interfacce

Namespace AppTools

    Public Class Base64Util
        Implements IBase64Util

        ' Converte una stringa Base64 in una sequenza di byte
        Public Function Base64ToBytes(base64 As String) As Byte() Implements IBase64Util.Base64ToBytes
            Return Convert.FromBase64String(base64)
        End Function

        ' Converte una stringa Base64 in una stringa normale
        Public Function Base64ToString(base64 As String) As String Implements IBase64Util.Base64ToString
            Return BytesToString(Base64ToBytes(base64))
        End Function

        ' Converte una sequenza di byte in Base64
        Public Function BytesToBase64(bytes As Byte()) As String Implements IBase64Util.BytesToBase64
            Return Convert.ToBase64String(bytes)
        End Function

        ' Converte una sequenza di byte in una stringa con UTF8 Encoding
        Public Function BytesToString(bytes As Byte()) As String Implements IBase64Util.BytesToString
            Return Encoding.UTF8.GetString(bytes)
        End Function

        ' Converte una stringa in Base64
        Public Function StringToBase64(input As String) As String Implements IBase64Util.StringToBase64
            Return BytesToBase64(StringToBytes(input))
        End Function

        ' Converte una stringa in una sequenza di byte con UTF8 Encoding
        Public Function StringToBytes(input As String) As Byte() Implements IBase64Util.StringToBytes
            Return Encoding.UTF8.GetBytes(input)
        End Function

    End Class

End Namespace