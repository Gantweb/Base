Imports System.Reflection
Imports WpfApp8.AppTools

Namespace MVVM.ViewModel

    Public Class MainView
        Inherits AppObservable

        Private _mainWindow As MainWindow
        Private _fileHandler As New FileHandler()
        Private _Lavoro As New Model.Lavoro

        ' Gestione del menu
        <ObfuscationAttribute(Exclude:=True)>
        Public Property NewCommand As New AppCommand(AddressOf NewFile)
        <ObfuscationAttribute(Exclude:=True)>
        Public Property OpenCommand As New AppCommand(AddressOf OpenFile)
        <ObfuscationAttribute(Exclude:=True)>
        Public Property SaveCommand As New AppCommand(AddressOf SaveFile)
        <ObfuscationAttribute(Exclude:=True)>
        Public Property ExitCommand As New AppCommand(AddressOf ExitApplication)
        <ObfuscationAttribute(Exclude:=True)>
        Public Property UndoCommand As New AppCommand(AddressOf Undo)
        <ObfuscationAttribute(Exclude:=True)>
        Public Property RedoCommand As New AppCommand(AddressOf Redo)
        <ObfuscationAttribute(Exclude:=True)>
        Public Property CutCommand As New AppCommand(AddressOf Cut)
        <ObfuscationAttribute(Exclude:=True)>
        Public Property CopyCommand As New AppCommand(AddressOf Copy)
        <ObfuscationAttribute(Exclude:=True)>
        Public Property PasteCommand As New AppCommand(AddressOf Paste)
        <ObfuscationAttribute(Exclude:=True)>
        Public Property AboutCommand As New AppCommand(AddressOf ShowAbout)


        <ObfuscationAttribute(Exclude:=True)>
        Public Property Lavoro As Model.Lavoro
            Get
                Return _Lavoro
            End Get
            Set
                SetVal(_Lavoro, Value)
            End Set
        End Property


        Public Sub New(mv As MainWindow)
            _mainWindow = mv
        End Sub

        ' Implementazione dei metodi dei comandi
        Private Sub NewFile(parameter As Object)
            Dim trc As ChangeTracker = AppObservable.GetChangeTracker()
            If VerificaLavoro(trc) Then Return
            Lavoro = New Model.Lavoro
            trc.ResetChanges()
        End Sub

        Private Sub OpenFile(parameter As Object)
            Dim trc As ChangeTracker = AppObservable.GetChangeTracker()
            If VerificaLavoro(trc) Then Return
            Dim pth = AppTools.ApriFile($"Apri un lavoro {Base("nome")}", Init("ext"))
            If Not String.IsNullOrEmpty(pth) Then
                Dim job As Model.Lavoro = _fileHandler.ReadWriteFile(Of Model.Lavoro)(pth, "", True, True)
                If IsNothing(job) Then Return
                Lavoro = job
                trc.ResetChanges()
            End If
        End Sub

        Private Sub SaveFile(saveAs As Object)
            LavSalva(saveAs)
        End Sub

        Private Sub ExitApplication(parameter As Object)
            _mainWindow.Close()
        End Sub

        Public Function VerificaLavoro(trc As ChangeTracker) As Boolean
            If trc.AnyChanged Then
                Dim tit As String = "Attenzione!"
                Dim msg As String = "Il lavoro è cambiato, ma non è stato salvato!" & vbCrLf & "VUOI SALVARE IL LAVORO CORRENTE?"
                Dim mb = MessageBox.Show(msg, tit, 3, 48)
                If mb = MessageBoxResult.Yes Then
                    Return Not LavSalva(Me)
                ElseIf mb = MessageBoxResult.No Then
                    Return False
                ElseIf mb = MessageBoxResult.Cancel Then
                    Return True
                End If
            End If
            Return False
        End Function

        Private Function LavSalva(saveAs As Object) As Boolean
            Dim sw As Boolean = Not IsNothing(saveAs)
            Dim pth As String
            If Lavoro.Nome.Equals("nuovo lavoro", StringComparison.OrdinalIgnoreCase) OrElse sw Then
                pth = AppTools.ApriFile($"Salva il lavoro {Base("nome")}", Init("ext"))
            End If
            If pth = "" Then Return False
            Lavoro.Pat = pth
            Try
                _fileHandler.ReadWriteFile(Of Model.Lavoro)(pth, Lavoro, True, True)
                Return True
            Catch ex As Exception
                ' a
            End Try
            Return False
        End Function





















        Private Sub Undo(parameter As Object)
            ' Logica per annullare un'azione
        End Sub

        Private Sub Redo(parameter As Object)
            ' Logica per ripetere un'azione
        End Sub

        Private Sub Cut(parameter As Object)
            ' Logica per tagliare
        End Sub

        Private Sub Copy(parameter As Object)
            ' Logica per copiare
        End Sub

        Private Sub Paste(parameter As Object)
            ' Logica per incollare
        End Sub

        Private Sub ShowAbout(parameter As Object)
            ' Logica per mostrare informazioni sull'applicazione
        End Sub

    End Class

End Namespace