Imports System.ComponentModel
Imports System.Reflection
Imports System.Windows.Media.Animation
Imports WpfApp6.Model
Imports WpfApp6.MVVM

Public Class ElencoErrori

    Public Sub New()
        InitializeComponent()
    End Sub

    Private _dataContext As AppObservable
    Private _controlMappings As Dictionary(Of String, String)

    Public Property Errors As List(Of ValidationErrors)
        Get
            Return CType(ErrorsDataGrid.ItemsSource, List(Of ValidationErrors))
        End Get
        Set(value As List(Of ValidationErrors))
            ErrorsDataGrid.ItemsSource = value
            ErrorsDataGrid.Items.Refresh() ' Aggiorna il DataGrid
            UpdateExpanderHeader(value) ' Aggiorna lo stato dell'Expander
        End Set
    End Property

    ' Metodo per inizializzare il UserControl e la validazione
    Public Sub Initialize(dataContext As AppObservable)
        _dataContext = dataContext

        ' Aggiungi un gestore di eventi per PropertyChanged
        AddHandler _dataContext.PropertyChanged, AddressOf OnViewModelPropertyChanged

        ' Creare automaticamente la mappa dei controlli associati alle proprietà
        _controlMappings = CreateControlMappings(Application.Current.MainWindow)

        ' Forza la validazione iniziale
        ValidateViewModel()
    End Sub

    Private Sub OnViewModelPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        ValidateViewModel()
    End Sub

    Private Sub ValidateViewModel()
        Dim service As New ValidationService()
        Dim myObservableObjects As New List(Of AppObservable) From {_dataContext}

        ' Raccogliere e impostare gli errori
        Dim collectedErrors = service.CollectErrors(myObservableObjects, _controlMappings)
        Errors = collectedErrors ' Assegna gli errori alla proprietà Errors

        ' Aggiorna l'interfaccia del DataGrid
        ErrorsDataGrid.Items.Refresh()
    End Sub

    Private Sub RemoveSatisfiedErrors(collectedErrors As List(Of ValidationErrors))
        ' Filtra e rimuove gli errori non più validi
        Errors = Errors.Where(Function(errorItem) collectedErrors.Any(Function(e) e.PropertyName = errorItem.PropertyName AndAlso e.ErrorMessage = errorItem.ErrorMessage)).ToList()
        ErrorsDataGrid.Items.Refresh() ' Aggiorna il DataGrid
    End Sub

    Private Sub UpdateExpanderHeader(errors As List(Of ValidationErrors))
        Dim blinkingAnimation As Storyboard = CType(Me.FindResource("BlinkingAnimation"), Storyboard)

        If errors IsNot Nothing AndAlso errors.Count > 0 Then
            blinkingAnimation.Begin(ErrorExpander, True) ' Avvia l'animazione per l'header dell'Expander
        Else
            blinkingAnimation.Stop(ErrorExpander) ' Ferma l'animazione se non ci sono errori
            ErrorExpander.Foreground = New SolidColorBrush(Colors.Black) ' Reimposta il colore dell'header
            ErrorExpander.FontWeight = FontWeights.Normal ' Reimposta lo stile del font
        End If
    End Sub

    Private Function GetAllControls(root As DependencyObject) As IEnumerable(Of Control)
        Dim controls As New List(Of Control)
        For i As Integer = 0 To VisualTreeHelper.GetChildrenCount(root) - 1
            Dim child = VisualTreeHelper.GetChild(root, i)
            If TypeOf child Is Control Then
                controls.Add(DirectCast(child, Control))
            End If
            controls.AddRange(GetAllControls(child))
        Next
        Return controls
    End Function

    Private Function CreateControlMappings(root As DependencyObject) As Dictionary(Of String, String)
        Dim controlMappings As New Dictionary(Of String, String)

        Dim controls = GetAllControls(root)
        For Each control In controls
            ' Ottenere tutte le DependencyProperty del controllo
            Dim dependencyProperties = control.GetType().GetFields(BindingFlags.[Public] Or BindingFlags.[Static] Or BindingFlags.FlattenHierarchy).
                                                Where(Function(f) GetType(DependencyProperty).IsAssignableFrom(f.FieldType)).
                                                Select(Function(f) DirectCast(f.GetValue(Nothing), DependencyProperty))

            For Each dp In dependencyProperties
                Dim binding = BindingOperations.GetBindingExpression(control, dp)
                If Not IsNothing(binding) Then
                    Dim propertyName = binding.ParentBinding.Path.Path
                    If Not controlMappings.ContainsKey(propertyName) Then
                        controlMappings(propertyName) = control.Name
                    End If
                End If
            Next
        Next

        Return controlMappings
    End Function

    Private Sub ErrorsDataGrid_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles ErrorsDataGrid.MouseDoubleClick
        Dim errorItem = CType(ErrorsDataGrid.SelectedItem, ValidationErrors)
        If errorItem IsNot Nothing AndAlso errorItem.ControlName IsNot Nothing Then
            ' Trova il controllo a partire dalla finestra principale o da un altro UserControl
            Dim mainWindow As Window = Application.Current.MainWindow
            Dim control As Control = FindControlByName(mainWindow, errorItem.ControlName)
            If control IsNot Nothing Then
                control.Focus()
            End If
        End If
    End Sub

    Private Function FindControlByName(root As DependencyObject, name As String) As Control
        For i As Integer = 0 To VisualTreeHelper.GetChildrenCount(root) - 1
            Dim child = VisualTreeHelper.GetChild(root, i)
            If TypeOf child Is Control AndAlso DirectCast(child, Control).Name = name Then
                Return DirectCast(child, Control)
            End If

            Dim foundChild = FindControlByName(child, name)
            If foundChild IsNot Nothing Then
                Return foundChild
            End If
        Next
        Return Nothing
    End Function

End Class