Imports Microsoft.Win32

Namespace AppTools
    Module FileDialogModule

        Public Function ApriFile(titolo As String, filtro As String) As String
            Dim openFileDialog As New OpenFileDialog()
            openFileDialog.Filter = filtro
            openFileDialog.Title = titolo

            If openFileDialog.ShowDialog() = True Then
                Dim filePath As String = openFileDialog.FileName
                Return filePath
            End If
            Return Nothing
        End Function

        Public Function SalvaFile(titolo As String, filtro As String) As String
            Dim saveFileDialog As New SaveFileDialog()
            saveFileDialog.Filter = filtro
            saveFileDialog.Title = titolo

            If saveFileDialog.ShowDialog() = True Then
                Dim filePath As String = saveFileDialog.FileName
                Return filePath
            End If
            Return Nothing
        End Function

    End Module
End Namespace