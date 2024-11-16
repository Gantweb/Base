Namespace MVVM
    Public Class ChangeTracker
        Private ReadOnly observables As New List(Of AppObservable)

        ' Metodo per aggiungere un'istanza di AppObservable al gestore
        Public Sub AddObservable(observable As AppObservable)
            If Not observables.Contains(observable) Then
                observables.Add(observable)
            End If
        End Sub

        ' Metodo per rimuovere un'istanza di AppObservable dal gestore
        Public Sub RemoveObservable(observable As AppObservable)
            If observables.Contains(observable) Then
                observables.Remove(observable)
            End If
        End Sub

        ' Metodo per verificare se qualche istanza è cambiata
        Public Function AnyChanged() As Boolean
            Return observables.Any(Function(o) o.IsChanged)
        End Function

        ' Metodo per resettare lo stato di cambiamento di tutte le istanze
        Public Sub ResetChanges()
            For Each observable In observables
                observable.IsChanged = False
            Next
        End Sub
    End Class
End Namespace