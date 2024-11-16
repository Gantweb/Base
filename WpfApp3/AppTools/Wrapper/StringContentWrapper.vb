Imports System.Net.Http
Imports System.Text

Namespace AppTools.Wrapper

    Public Class StringContentWrapper
        Implements IDisposable

        Private content As StringContent
        Private disposed As Boolean = False

        Public Sub New(data As String, encoding As Encoding, mediaType As String)
            content = New StringContent(data, encoding, mediaType)
        End Sub

        Public Function GetContent() As StringContent
            Return content
        End Function

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposed Then
                If disposing Then
                    ' Dispose managed resources
                    If content IsNot Nothing Then
                        content.Dispose()
                        content = Nothing
                    End If
                End If

                ' Dispose unmanaged resources (if any)

                Me.disposed = True
            End If
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

    End Class

End Namespace
