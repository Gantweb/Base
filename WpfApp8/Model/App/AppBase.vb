Imports System.Configuration
Imports System.IO
Imports System.Management
Imports System.Reflection
Imports WpfApp8.AppTools
Imports WpfApp8.AppTools.Interfacce

Namespace Model.App

    Public Class AppBase
        Inherits MVVM.AppJsonObservable

        Public ReadOnly Property Percent As Double
            Get
                Return If(Base("veri"), 1, GetRandomNumber())
            End Get
        End Property

        Public Sub New()
            Item("nome") = GetAssemblyName()
            Item("versione") = GetAssemblyVersion()
            Item("descrizione") = GetDescrizione()
            Item("societa") = GetCompanyName()
            Item("info") = $"{Trova("SerialNumber", "Win32_BIOS")}|{Trova("SerialNumber", "Win32_BaseBoard")}|{Trova("ProcessorId", "Win32_Processor")}"
            Item("inipath") = GetIniPath()
            Item("code") = GetCode()
        End Sub

        Private Shared Function Trova(tp As String, tz As String) As String
            Dim A_1 As String
            Dim A_2 As ISystemInfoRetriever = New SystemInfoRetriever()
            Dim A_3 As ManagementObjectCollection = A_2.GetSystemInfo($"select {tp} from {tz}")
            For Each A_4 As ManagementObject In A_3
                A_1 = A_4(tp).ToString()
            Next
            Return A_1
        End Function

        Dim retriever As IAssemblyInfoRetriever = New AssemblyInfoRetriever()
        Dim asse As AssemblyName = retriever.GetAssemblyName()

        Private Function GetAssemblyName() As String
            Return asse.Name
        End Function

        Private Function GetAssemblyVersion() As String
            Dim vr = asse.Version
            Return $"{vr.Major}.{vr.Minor}"
        End Function

        Dim comp As AssemblyCompanyAttribute = retriever.GetCompanyAttribute()

        Private Function GetCompanyName() As String
            Return comp.Company
        End Function

        Dim descr As AssemblyDescriptionAttribute = retriever.GetDescriptionAttribute()

        Private Function GetDescrizione() As String
            Return descr.Description
        End Function

        Private Function GetRandomNumber() As Double
            Dim random As New Random()
            Dim number As Double
            Do
                number = 0.8 + (random.NextDouble() * (1.2 - 0.8))
            Loop While Math.Abs(number - 1.0) < Double.Epsilon
            Return number
        End Function

        Private Function GetCode() As Object
            Dim hashBytes As Byte() = _hasher.ComputeHash(_b64.StringToBytes($"{Item("info")}|{Item("nome")}"))
            Dim asciiSum As Integer = hashBytes.Sum(Function(b) b * 10001)
            Dim hexSum As String = asciiSum.ToString("X8")
            Return $"{hexSum.Substring(0, 4)}-{hexSum.Substring(4, 4)}"
        End Function

        Private Function GetIniPath() As String
            Dim _iniPat = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath
            Dim _soc = GetCompanyName()
            _iniPat = _iniPat.Substring(0, _iniPat.IndexOf(_soc) + _soc.Length + 1) & GetAssemblyVersion() & "\"
            If Not Directory.Exists(_iniPat) Then Directory.CreateDirectory(_iniPat)
            Return _iniPat
        End Function

    End Class

End Namespace