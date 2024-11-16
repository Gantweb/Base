Imports System.Text
Imports WpfApp6.AppTools.Interfacce

Namespace AppTools

    Public Class Base64Helper
        Implements IBase64

        ' Converte una stringa in Base64
        Public Function StringToBase64(input As String) As String Implements IBase64.StringToBase64
            Return Convert.ToBase64String(StringToBytes(input))
        End Function

        ' Converte una stringa Base64 in una stringa normale
        Public Function Base64ToString(base64 As String) As String Implements IBase64.Base64ToString
            Return BytesToString(Base64ToBytes(base64))
        End Function

        ' Converte una sequenza di byte in Base64
        Public Function BytesToBase64(bytes As Byte()) As String Implements IBase64.BytesToBase64
            Return Convert.ToBase64String(bytes)
        End Function

        ' Converte una stringa Base64 in una sequenza di byte
        Public Function Base64ToBytes(base64 As String) As Byte() Implements IBase64.Base64ToBytes
            Return Convert.FromBase64String(base64)
        End Function

        ' Converte una stringa in una sequenza di byte con UTF8 Encoding
        Public Function StringToBytes(input As String) As Byte() Implements IBase64.StringToBytes
            Return Encoding.UTF8.GetBytes(input)
        End Function

        ' Converte una sequenza di byte in una stringa con UTF8 Encoding
        Public Function BytesToString(bytes As Byte()) As String Implements IBase64.BytesToString
            Return Encoding.UTF8.GetString(bytes)
        End Function

    End Class

End Namespace