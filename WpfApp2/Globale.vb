Imports WpfApp2.AppTools.Wrapper

Module Globale

    Public Base As New AppTools.AppBaseSystem
    Public Logs As New AppTools.AppLog
    Public Init As New AppTools.AppConfig

    Public Sub Imposta()
        Init("ext") = Base64Helper.EncodeStringToBase64($"File {Base("nome")} (*.a01)|*.a01")
        Init("ragsoc") = Base64Helper.EncodeStringToBase64($"Ragione Sociale Azienda")
        Init("indiri") = Base64Helper.EncodeStringToBase64($"Indirizzo Azienda")
        Init("iban") = Base64Helper.EncodeStringToBase64($"iban Azienda")
        Init("urlsito") = Base64Helper.EncodeStringToBase64($"https://localhost:44369")
    End Sub

End Module