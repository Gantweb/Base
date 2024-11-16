Namespace AppTools.Interfacce

    Public Interface IApiClient

        Function GetAsync(Of T)(url As String) As Task(Of T)

        Function PostAsync(Of T)(url As String, jsonData As String) As Task(Of String)

    End Interface

End Namespace