Imports System.Configuration
Imports System.IO
Imports System.Management
Imports System.Reflection
Imports WpfApp5.AppTools.Interfacce

Namespace Model

    Public Class AppBase
        Inherits MVVM.AppJsonObservable

        Private ReadOnly _hasher As IHasher

        Public ReadOnly Property Percent As Double
            Get
                Return If(Base("veri"), 1, GenerateRandomNumber())
            End Get
        End Property

        Public Sub New(base64Helper As IBase64, hasher As IHasher)
            MyBase.New(base64Helper)
            _hasher = hasher
            Item("nome") = GetAssemblyName()
            Item("versione") = GetAssemblyVersion()
            Item("descrizione") = GetDescrizione()
            Item("societa") = GetCompanyName()
            Item("info") = $"{GetBiosSerialNumber()}|{GetMotherboardSerialNumber()}|{GetProcessorId()}"
            Item("inipath") = GetIniPath()
            Item("code") = GetCode()
        End Sub

        Private Shared Function GetAssemblyName() As String
            Return Assembly.GetExecutingAssembly().GetName().Name
        End Function

        Private Shared Function GetAssemblyVersion() As String
            Dim vr = Assembly.GetExecutingAssembly().GetName().Version
            Return $"{vr.Major}.{vr.Minor}"
        End Function

        Private Shared Function GetBiosSerialNumber() As String
            Using searcher As New ManagementObjectSearcher("select SerialNumber from Win32_BIOS")
                For Each mo As ManagementObject In searcher.Get()
                    Return mo("SerialNumber").ToString()
                Next
            End Using
            Return String.Empty
        End Function

        Private Shared Function GetCompanyName() As String
            Dim attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyCompanyAttribute), False)
            If attributes.Length = 0 Then Return ""
            Return CType(attributes(0), AssemblyCompanyAttribute).Company
        End Function

        Private Shared Function GetDescrizione() As String
            Dim attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyDescriptionAttribute), False)
            If attributes.Length = 0 Then Return ""
            Return CType(attributes(0), AssemblyDescriptionAttribute).Description
        End Function

        Private Shared Function GetMotherboardSerialNumber() As String
            Using searcher As New ManagementObjectSearcher("select SerialNumber from Win32_BaseBoard")
                For Each mo As ManagementObject In searcher.Get()
                    Return mo("SerialNumber").ToString()
                Next
            End Using
            Return String.Empty
        End Function

        Private Shared Function GetProcessorId() As String
            Using searcher As New ManagementObjectSearcher("select ProcessorId from Win32_Processor")
                For Each mo As ManagementObject In searcher.Get()
                    Return mo("ProcessorId").ToString()
                Next
            End Using
            Return String.Empty
        End Function

        Private Function GenerateRandomNumber() As Double
            Dim random As New Random()
            Dim number As Double
            Do
                number = 0.8 + (random.NextDouble() * (1.2 - 0.8))
            Loop While Math.Abs(number - 1.0) < Double.Epsilon
            Return number
        End Function

        Private Function GetCode() As Object
            Dim hashBytes As Byte() = _hasher.ComputeHashBytes(Item("info") & Item("nome"))
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