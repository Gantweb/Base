Imports System.Text

Namespace AppTools.Wrapper

    Public Class Base64Helper

        ' Delegate per le operazioni di codifica e decodifica
        Public Delegate Function Base64Delegate(Of TInput, TOutput)(data As TInput) As TOutput

        ' Wrapper per Convert.ToBase64String
        Public Shared Function EncodeToBase64(data As Byte()) As String
            Return Convert.ToBase64String(data)
        End Function

        ' Wrapper per Convert.FromBase64String
        Public Shared Function DecodeFromBase64(data As String) As Byte()
            Return Convert.FromBase64String(data)
        End Function

        ' Metodo per usare il delegate per codifica byte[] a stringa
        Public Shared Function UseBase64Delegate(data As Byte(), operation As Base64Delegate(Of Byte(), String)) As String
            Return operation(data)
        End Function

        ' Metodo per usare il delegate per decodifica stringa a byte[]
        Public Shared Function UseBase64Delegate(data As String, operation As Base64Delegate(Of String, Byte())) As Byte()
            Return operation(data)
        End Function

        ' Codifica una stringa in Base64
        Public Shared Function EncodeStringToBase64(data As String) As String
            Dim utf8Bytes As Byte() = Encoding.UTF8.GetBytes(data)
            Return UseBase64Delegate(utf8Bytes, AddressOf EncodeToBase64)
        End Function

        ' Decodifica una stringa da Base64
        Public Shared Function DecodeStringFromBase64(data As String) As String
            Dim decodedBytes As Byte() = UseBase64Delegate(data, AddressOf DecodeFromBase64)
            Return Encoding.UTF8.GetString(decodedBytes)
        End Function

    End Class

End Namespace