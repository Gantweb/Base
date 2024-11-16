Imports System.Configuration
Imports System.IO
Imports System.Management
Imports System.Reflection
Imports WpfApp3.AppTools.Helper
Imports WpfApp3.Mvvm

Namespace AppTools

    Public Class AppBaseSystem
        Inherits AppJsonObservable

        Private bs64 As New Base64Helper

        Public Sub New()
            Item("nome") = bs64.EncodeStringToBase64(GetAssemblyName())
            Item("versione") = bs64.EncodeStringToBase64(GetAssemblyVersion())
            Item("descrizione") = bs64.EncodeStringToBase64(GetDescrizione())
            Item("societa") = bs64.EncodeStringToBase64(GetCompanyName())
            Item("info") = bs64.EncodeStringToBase64($"{GetBiosSerialNumber()}|{GetMotherboardSerialNumber()}|{GetProcessorId()}")
            Item("inipath") = bs64.EncodeStringToBase64(GetIniPath())
        End Sub

        Private Function GetIniPath() As String
            Dim _iniPat = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath
            Dim _soc = GetCompanyName()
            _iniPat = _iniPat.Substring(0, _iniPat.IndexOf(_soc) + _soc.Length + 1) & GetAssemblyVersion() & "\"
            If Not Directory.Exists(_iniPat) Then Directory.CreateDirectory(_iniPat)
            Return _iniPat
        End Function

        Private Function GetAssemblyName() As String
            Return Assembly.GetExecutingAssembly().GetName().Name
        End Function

        Private Function GetAssemblyVersion() As String
            Dim vr = Assembly.GetExecutingAssembly().GetName().Version
            Return $"{vr.Major}.{vr.Minor}"
        End Function

        Private Function GetCompanyName() As String
            Dim attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyCompanyAttribute), False)
            If attributes.Length = 0 Then Return ""
            Return CType(attributes(0), AssemblyCompanyAttribute).Company
        End Function

        Private Function GetDescrizione() As String
            Dim attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyDescriptionAttribute), False)
            If attributes.Length = 0 Then Return ""
            Return CType(attributes(0), AssemblyDescriptionAttribute).Description
        End Function

        Private Function GetProcessorId() As String
            Using searcher As New ManagementObjectSearcher("select ProcessorId from Win32_Processor")
                For Each mo As ManagementObject In searcher.Get()
                    Return mo("ProcessorId").ToString()
                Next
            End Using
            Return String.Empty
        End Function

        Private Function GetMotherboardSerialNumber() As String
            Using searcher As New ManagementObjectSearcher("select SerialNumber from Win32_BaseBoard")
                For Each mo As ManagementObject In searcher.Get()
                    Return mo("SerialNumber").ToString()
                Next
            End Using
            Return String.Empty
        End Function

        Private Function GetBiosSerialNumber() As String
            Using searcher As New ManagementObjectSearcher("select SerialNumber from Win32_BIOS")
                For Each mo As ManagementObject In searcher.Get()
                    Return mo("SerialNumber").ToString()
                Next
            End Using
            Return String.Empty
        End Function

    End Class

End Namespace