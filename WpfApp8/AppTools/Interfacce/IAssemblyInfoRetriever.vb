Imports System.Reflection

Namespace AppTools.Interfacce

    Public Interface IAssemblyInfoRetriever

        Function GetAssemblyName() As AssemblyName

        Function GetCompanyAttribute() As AssemblyCompanyAttribute

        Function GetDescriptionAttribute() As AssemblyDescriptionAttribute

    End Interface

End Namespace