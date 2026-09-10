Imports System.ComponentModel

Public Module Extensions

#Region "DevExpress Obsolete"

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

#End Region

#Region "SearchLookUp"

    ''' <summary>
    ''' Obtiene el objeto seleccionado en la fuente de datos según el EditValue
    ''' </summary>
    ''' <param name="obj">Objeto que extiende el metodo</param>
    ''' <returns>Objeto seleccionado</returns>
    <System.Runtime.CompilerServices.Extension> _
    Public Function GetSelectedObject(ByVal obj As DevExpress.XtraEditors.SearchLookUpEdit) As Object
        If obj.Properties.DataSource IsNot Nothing AndAlso obj.Properties.ValueMember IsNot Nothing AndAlso Not obj.Properties.ValueMember.Trim().Equals(String.Empty) AndAlso obj.EditValue IsNot Nothing Then
            If obj.Properties.View.DataController.GetAllFilteredAndSortedRows().Count > 0 Then 'Se ha aplicado un filtro o un ordenamiento
                Dim isServerModeFlag = obj.Properties.DataSource.GetType().Name.EndsWith("InstantFeedbackSource")
                For Each o In obj.Properties.View.DataController.GetAllFilteredAndSortedRows()
                    If isServerModeFlag Then 'Si la fuente de datos es de tipo servidor (e.g XPInstantFeedbackSource)
                        Dim entity = CType(o, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
                        Dim propInf = entity.GetType().GetProperty(obj.Properties.ValueMember)
                        If propInf IsNot Nothing Then
                            Dim val = propInf.GetValue(entity)
                            If val.Equals(obj.EditValue) Then
                                Return entity
                            End If
                        End If
                    Else
                        Dim propInf = o.GetType().GetProperty(obj.Properties.ValueMember)
                        If propInf IsNot Nothing Then
                            Dim val = propInf.GetValue(o)
                            If val.Equals(obj.EditValue) Then
                                Return o
                            End If
                        End If
                    End If
                Next
            Else 'Se obtiene de forma normal
                Return obj.Properties.View.GetFocusedRow()
            End If
        End If
        Return Nothing
    End Function

#End Region

End Module