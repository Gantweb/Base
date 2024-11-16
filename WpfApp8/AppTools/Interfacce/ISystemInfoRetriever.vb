Imports System.Management

Namespace AppTools.Interfacce

    Public Interface ISystemInfoRetriever

        Function GetSystemInfo(query As String) As ManagementObjectCollection

    End Interface

End Namespace