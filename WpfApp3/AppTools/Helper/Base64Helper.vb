Imports System.Text

Namespace AppTools.Helper

    Public Class Base64Helper
        Private log As String

        Public Sub New(Optional logString As String = Nothing)
            log = logString
        End Sub

        Public Delegate Function Base64Delegate(Of TInput, TOutput)(data As TInput) As TOutput

        Public Function EncodeToBase64(data As Byte()) As String
            Return Convert.ToBase64String(data)
        End Function

        Public Function DecodeFromBase64(data As String) As Byte()
            Return Convert.FromBase64String(data)
        End Function

        Public Function UseBase64Delegate(data As Byte(), operation As Base64Delegate(Of Byte(), String)) As String
            Return operation(data)
        End Function

        Public Function UseBase64Delegate(data As String, operation As Base64Delegate(Of String, Byte())) As Byte()
            Return operation(data)
        End Function

        Public Function EncodeStringToBase64(data As String) As String
            Dim u As Byte() = Encoding.UTF8.GetBytes(data)
            Return UseBase64Delegate(u, AddressOf EncodeToBase64)
        End Function

        Public Function DecodeStringFromBase64(data As String) As String
            Dim d As Byte() = UseBase64Delegate(data, AddressOf DecodeFromBase64)
            Return Encoding.UTF8.GetString(d)
        End Function

    End Class

End Namespace