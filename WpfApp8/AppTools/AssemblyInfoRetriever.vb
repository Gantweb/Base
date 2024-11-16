Imports System.Reflection
Imports WpfApp8.AppTools.Interfacce

Namespace AppTools

    Public Class AssemblyInfoRetriever
        Implements IAssemblyInfoRetriever

        Public Function GetAssemblyName() As AssemblyName Implements IAssemblyInfoRetriever.GetAssemblyName
            Return Assembly.GetExecutingAssembly().GetName()
        End Function

        Public Function GetCompanyAttribute() As AssemblyCompanyAttribute Implements IAssemblyInfoRetriever.GetCompanyAttribute
            Dim attributes As Object() = Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyCompanyAttribute), False)
            If attributes.Length > 0 Then
                Return DirectCast(attributes(0), AssemblyCompanyAttribute)
            Else
                Return Nothing
            End If
        End Function

        Public Function GetDescriptionAttribute() As AssemblyDescriptionAttribute Implements IAssemblyInfoRetriever.GetDescriptionAttribute
            Dim attributes As Object() = Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyDescriptionAttribute), False)
            If attributes.Length > 0 Then
                Return DirectCast(attributes(0), AssemblyDescriptionAttribute)
            Else
                Return Nothing
            End If
        End Function

    End Class

End Namespace