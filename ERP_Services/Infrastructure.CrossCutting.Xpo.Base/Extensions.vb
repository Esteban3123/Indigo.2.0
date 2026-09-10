Imports System.ComponentModel

Public Module Extensions

    Private ReadOnly dicValues As New Dictionary(Of WeakReference, Dictionary(Of String, Object))()

    Private Function GetKey(target As Object, Optional doNotAdd As Boolean = False) As WeakReference
        Dim deadWeakReferences = New List(Of WeakReference)()

        Try
            For Each reference In dicValues.Keys
                If Not reference.IsAlive Then
                    deadWeakReferences.Add(reference)
                Else
                    If reference.Target.Equals(target) Then
                        Return reference
                    End If
                End If
            Next

            If doNotAdd Then
                Return Nothing
            End If

            Dim newWeakReference = New WeakReference(target)

            dicValues.Add(newWeakReference, New Dictionary(Of String, Object)())

            Return newWeakReference
        Finally
            For Each reference In deadWeakReferences
                dicValues.Remove(reference)
            Next
        End Try
    End Function

    <System.Runtime.CompilerServices.Extension> _
    Public Sub SetAttachedProperty(target As Object, name As String, value As Object)
        Dim reference = GetKey(target)
        Dim values = dicValues(reference)

        If values.ContainsKey(name) Then
            values(name) = value
        Else
            values.Add(name, value)
        End If
    End Sub

    <System.Runtime.CompilerServices.Extension> _
    Public Function GetAttachedProperty(Of T)(target As Object, name As String) As T
        Dim result = GetAttachedProperty(target, name)

        Return If(result Is Nothing, Nothing, DirectCast(result, T))
    End Function

    <System.Runtime.CompilerServices.Extension> _
    Public Function GetAttachedProperty(target As Object, name As String) As Object
        Dim reference = GetKey(target)

        Dim value As Object = Nothing

        Return If(dicValues(reference).TryGetValue(name, value), value, Nothing)
    End Function

    <System.Runtime.CompilerServices.Extension> _
    Public Sub RemoveAttachedProperties(target As Object)
        Dim reference = GetKey(target, True)

        If reference IsNot Nothing Then
            dicValues.Remove(reference)
        End If
    End Sub

    <System.Runtime.CompilerServices.Extension> _
    Public Sub RemoveAttachedProperty(target As Object, name As String)
        Dim reference = GetKey(target, True)
        If reference Is Nothing Then
            Return
        End If

        Dim values = dicValues(reference)

        values.Remove(name)
    End Sub

End Module