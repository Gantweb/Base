Namespace AppTools.Interfacce
Public Interface IFileHandler
    Function ReadWriteFile(Of T)(filePath As String, content As Object, Optional compress As Boolean = False, Optional encrypt As Boolean = False) As T
End Interface
End Namespace