Imports System.Management
Imports WpfApp8.AppTools.Interfacce

Namespace AppTools

    Public Class SystemInfoRetriever
        Implements ISystemInfoRetriever

        Public Function GetSystemInfo(query As String) As ManagementObjectCollection Implements ISystemInfoRetriever.GetSystemInfo
            Dim searcher As New ManagementObjectSearcher(query)
            Return searcher.Get()
        End Function

    End Class

End Namespace