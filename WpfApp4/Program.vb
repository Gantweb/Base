Module Program

    Public Base As New Model.AppBase
    Public Logs As New Model.AppLog
    Public Init As New Model.AppConfig

    Public Sub Imposta()

        Init("ext") = $"File {Base("nome")} (*.a01)|*.a01"
        Init("ragsoc") = $"Ragione Sociale Azienda"
        Init("indiri") = $"Indirizzo Azienda"
        Init("iban") = $"iban Azienda"
        Init("urlsito") = $"https://localhost:44369"

        Application.StartCountdown()
    End Sub

End Module