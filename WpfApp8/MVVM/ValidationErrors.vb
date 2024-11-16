Imports System.Reflection

Namespace MVVM

    Public Class ValidationErrors
        <ObfuscationAttribute(Exclude:=True)>
        Public Property PropertyName As String
        <ObfuscationAttribute(Exclude:=True)>
        Public Property ErrorMessage As String
        <ObfuscationAttribute(Exclude:=True)>
        Public Property ControlName As String
    End Class

End Namespace