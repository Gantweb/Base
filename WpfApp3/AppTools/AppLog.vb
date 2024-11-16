Imports System.Text.Json
Imports WpfApp3.AppTools.Helper

Namespace AppTools

    Public Class AppLog
        Private bs64 As New Base64Helper
        Private aLog As List(Of Exception)
        Private pth As String = bs64.DecodeStringFromBase64(Base("inipath")) & "Error.log"
        Private fileHelper As New FileHelper

        Public Sub New()
        End Sub

        Public Sub Log(exp As Exception, Optional prompt As String = Nothing)
            DeserializeLog(fileHelper.LeggiFileCompresso(pth))
            aLog.Add(exp)
            fileHelper.ScriviFileCompresso(pth, SerializeLog())
            aLog = Nothing
        End Sub

        Public Function SerializeLog() As String
            Dim serializableLog As List(Of SerializableException) = aLog.Select(Function(ex) ExceptionHelper.ConvertToSerializable(ex)).ToList()
            Return JsonSerializer.Serialize(serializableLog)
        End Function

        Private Sub DeserializeLog(json As String)
            Try
                Dim serializableLog As List(Of SerializableException) = JsonSerializer.Deserialize(Of List(Of SerializableException))(json)
                aLog = serializableLog.Select(Function(se) ExceptionHelper.ConvertToException(se)).ToList()
            Catch ex As Exception
                aLog = New List(Of Exception)()
            End Try
        End Sub

        Private Class ExceptionHelper

            Public Shared Function ConvertToSerializable(ex As Exception) As SerializableException
                Return New SerializableException(ex)
            End Function

            Public Shared Function ConvertToException(se As SerializableException) As Exception
                Dim ex As New Exception(se.Message)
                Return ex
            End Function

        End Class

        Private Class SerializableException
            Public Property Message As String
            Public Property StackTrace As String

            Public Sub New(ex As Exception)
                Message = ex.Message
                StackTrace = ex.StackTrace
            End Sub

            Public Sub New()
            End Sub

        End Class

    End Class

End Namespace