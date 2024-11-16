Imports System.ComponentModel
Imports System.Reflection
Imports WpfApp8.MVVM

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
            ErrorsDataGrid.Items.Refresh()
        End Set
    End Property

    Public Sub Initialize(dataContext As AppObservable)
        _dataContext = dataContext

        AddHandler _dataContext.PropertyChanged, AddressOf OnViewModelPropertyChanged

        _controlMappings = CreateControlMappings(Application.Current.MainWindow)

        ValidateViewModel()
    End Sub

    Private Sub OnViewModelPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        ValidateViewModel()
    End Sub

    Private Sub ValidateViewModel()
        Dim service As New ValidationService()
        Dim myObservableObjects As New List(Of AppObservable) From {_dataContext}

        Dim collectedErrors = service.CollectErrors(myObservableObjects, _controlMappings)
        Errors = collectedErrors

        ErrorsDataGrid.Items.Refresh()
    End Sub

    Private Sub RemoveSatisfiedErrors(collectedErrors As List(Of ValidationErrors))
        Errors = Errors.Where(Function(errorItem) collectedErrors.Any(Function(e) e.PropertyName = errorItem.PropertyName AndAlso e.ErrorMessage = errorItem.ErrorMessage)).ToList()
        ErrorsDataGrid.Items.Refresh()
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
            Dim dependencyProperties = control.GetType().GetFields(BindingFlags.[Public] Or BindingFlags.[Static] Or BindingFlags.FlattenHierarchy).
                                                Where(Function(f) GetType(DependencyProperty).IsAssignableFrom(f.FieldType)).
                                                Select(Function(f) DirectCast(f.GetValue(Nothing), DependencyProperty))

            For Each dp In dependencyProperties
                Dim binding = BindingOperations.GetBindingExpression(control, dp)
                If Not IsNothing(binding) Then
                    Dim propertyPath = binding.ParentBinding.Path.Path
                    Dim propertyParts = propertyPath.Split("."c)

                    ' Utilizza l'ultima parte del percorso come nome della proprietà
                    Dim propertyName = propertyParts.Last()
                    If Not controlMappings.ContainsKey(propertyName) Then
                        controlMappings(propertyName) = control.Name
                    End If

                    ' Aggiungi tutte le parti del percorso nel dizionario
                    For Each part In propertyParts
                        If Not controlMappings.ContainsKey(part) Then
                            controlMappings(part) = control.Name
                        End If
                    Next
                End If
            Next
        Next

        Return controlMappings
    End Function

    Private Sub ErrorsDataGrid_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles ErrorsDataGrid.MouseDoubleClick
        Dim errorItem = CType(ErrorsDataGrid.SelectedItem, ValidationErrors)
        If errorItem IsNot Nothing AndAlso errorItem.ControlName IsNot Nothing Then
            Dim mainWindow As Window = Application.Current.MainWindow
            Dim control As Control = FindControlByName(mainWindow, errorItem.ControlName)
            If Not IsNothing(control) Then
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
            If Not IsNothing(foundChild) Then
                Return foundChild
            End If
        Next
        Return Nothing
    End Function

End Class
