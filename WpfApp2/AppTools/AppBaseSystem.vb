Imports System.Configuration
Imports System.IO
Imports System.Management
Imports System.Reflection
Imports WpfApp2.AppTools.Wrapper
Imports WpfApp2.Mvvm

Namespace AppTools

    Public Class AppBaseSystem
        Inherits AppJsonObservable

        Public Sub New()
            Item("nome") = Base64Helper.EncodeStringToBase64(GetAssemblyName)
            Item("versione") = Base64Helper.EncodeStringToBase64(GetAssemblyVersion)
            Item("descrizione") = Base64Helper.EncodeStringToBase64(GetDescrizione)
            Item("societa") = Base64Helper.EncodeStringToBase64(GetCompanyName)
            Item("info") = Base64Helper.EncodeStringToBase64($"{GetBiosSerialNumber()}|{GetMotherboardSerialNumber()}|{GetProcessorId()}")
            Item("inipath") = Base64Helper.EncodeStringToBase64(GetIniPath)
        End Sub

        Private Shared Function GetIniPath() As String
            Dim _iniPat = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath
            Dim _soc = GetCompanyName()
            _iniPat = _iniPat.Substring(0, _iniPat.IndexOf(_soc) + _soc.Length + 1) & GetAssemblyVersion() & "\"
            If Not Directory.Exists(_iniPat) Then Directory.CreateDirectory(_iniPat)
            Return _iniPat
        End Function

        ' Metodi per ottenere informazioni sull'assembly
        Private Shared Function GetAssemblyName() As String
            Return Assembly.GetExecutingAssembly().GetName().Name
        End Function

        Private Shared Function GetAssemblyVersion() As String
            Dim vr = Assembly.GetExecutingAssembly().GetName().Version
            Return $"{vr.Major}.{vr.Minor}"
        End Function

        Private Shared Function GetCompanyName() As String
            Dim attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyCompanyAttribute), False)
            If attributes.Length = 0 Then
                Return ""
            End If
            Return CType(attributes(0), AssemblyCompanyAttribute).Company
        End Function

        Private Shared Function GetDescrizione() As String
            Dim attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyDescriptionAttribute), False)
            If attributes.Length = 0 Then
                Return ""
            End If
            Return CType(attributes(0), AssemblyDescriptionAttribute).Description
        End Function

        ' Metodi per ottenere informazioni sull'hardware
        Private Shared Function GetProcessorId() As String
            Using searcher As New ManagementObjectSearcher("select ProcessorId from Win32_Processor")
                For Each mo As ManagementObject In searcher.Get()
                    Return mo("ProcessorId").ToString()
                Next
            End Using
            Return String.Empty
        End Function

        Private Shared Function GetMotherboardSerialNumber() As String
            Using searcher As New ManagementObjectSearcher("select SerialNumber from Win32_BaseBoard")
                For Each mo As ManagementObject In searcher.Get()
                    Return mo("SerialNumber").ToString()
                Next
            End Using
            Return String.Empty
        End Function

        Private Shared Function GetBiosSerialNumber() As String
            Using searcher As New ManagementObjectSearcher("select SerialNumber from Win32_BIOS")
                For Each mo As ManagementObject In searcher.Get()
                    Return mo("SerialNumber").ToString()
                Next
            End Using
            Return String.Empty
        End Function

    End Class

End Namespace