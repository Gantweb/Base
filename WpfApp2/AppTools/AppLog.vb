Imports System.Text.Json
Imports WpfApp2.AppTools.Helper
Imports WpfApp2.AppTools.Wrapper

Namespace AppTools

    Public Class AppLog

        Private aLog As List(Of Exception)
        Private pth As String = Base64Helper.DecodeStringFromBase64(Base("inipath")) & "Error.log"

        Public Sub New()
        End Sub

        Public Sub Log(exp As Exception, Optional prompt As String = Nothing)
            DeserializeLog(FileHelper.LeggiFileCompresso(pth))
            aLog.Add(exp)
            FileHelper.ScriviFileCompresso(pth, SerializeLog)
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
                aLog = New List(Of Exception)
            End Try
        End Sub

        Private Class ExceptionHelper

            Public Shared Function ConvertToSerializable(ex As Exception) As SerializableException
                Return New SerializableException(ex)
            End Function

            Public Shared Function ConvertToException(se As SerializableException) As Exception
                Dim ex As New Exception(se.Message)
                ' Nota: Non è possibile impostare StackTrace in modo programmatico
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

            ' Costruttore vuoto per la deserializzazione
            Public Sub New()
            End Sub

        End Class

    End Class

End Namespace