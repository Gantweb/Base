Imports WpfApp3.AppTools.Helper

Module Program

    Private bs64 As New Base64Helper
    Public Base As New AppTools.AppBaseSystem
    Public Logs As New AppTools.AppLog
    Public Init As New AppTools.AppConfig

    Public Sub Imposta()
        Dim aa As String = Base("nome")
        Dim bb = bs64.DecodeStringFromBase64(aa)



        Init("ext") = bs64.EncodeStringToBase64($"File {bs64.DecodeStringFromBase64(Base(bs64.EncodeStringToBase64("nome")))} (*.a01)|*.a01")
        Init("ragsoc") = bs64.EncodeStringToBase64($"Ragione Sociale Azienda")
        Init("indiri") = bs64.EncodeStringToBase64($"Indirizzo Azienda")
        Init("iban") = bs64.EncodeStringToBase64($"iban Azienda")
        Init("urlsito") = bs64.EncodeStringToBase64($"https://localhost:44369")
        Init.Save()
    End Sub

End Module