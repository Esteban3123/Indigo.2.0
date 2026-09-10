'***********************************************************************
' Assembly         : Presentacion.Controls
' Author           : Juan F. Tamayo Puertas
' Created          : 2015-01-07
'
' Last Modified By :
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports DevExpress.XtraGrid.Views.Grid

#End Region

''' <summary>
''' Proporciona ayuda para ocultar o mostrar filas específicas
''' en una vista GridView
''' </summary>
Public Class RowVisibilityHelper

#Region "Fields"

    ''' <summary>
    ''' Vista a observar
    ''' </summary>
    Private _view As GridView

    ''' <summary>
    ''' Lista de filas ocultas
    ''' </summary>
    Private _invisibleRows As List(Of Int32)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna la vista a observar
    ''' </summary>
    ''' <value>Vista a observar</value>
    ''' <returns>La vista a observar</returns>
    Public Property View As GridView
        Get
            Return Me._view
        End Get
        Set(value As GridView)
            Me.UnsubscribeEvents()
            Me._view = value
            Me.SubscribeEvents()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la lista de filas ocultas
    ''' </summary>
    ''' <value>Lista de filas ocultas</value>
    ''' <returns>La lista de filas ocultas</returns>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    Public Property InvisibleRows As List(Of Int32)
        Get
            If Me._invisibleRows Is Nothing Then
                Me._invisibleRows = New List(Of Integer)()
            End If
            Return Me._invisibleRows
        End Get
        Set(value As List(Of Int32))
            Me._invisibleRows = value
        End Set
    End Property

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Aqui se aplica la lógica para ocultar las filas almacenadas en la lista
    ''' </summary>
    Private Sub GridView_CustomRowFilter(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowFilterEventArgs)
        If Me.IsRowInvisible(e.ListSourceRow) Then
            e.Visible = False
            e.Handled = True
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Limpia las filas invisibles y las muestra de nuevo
    ''' </summary>
    Public Sub CleanInvisibleRows()
        Me.InvisibleRows.Clear()
        Me.View.RefreshData()
    End Sub

    ''' <summary>
    ''' Oculta una fila
    ''' </summary>
    ''' <param name="dataSourceRowIndex">Indice de la fila</param>
    Public Sub HideRow(ByVal dataSourceRowIndex As Int32)
        If Not Me.IsRowInvisible(dataSourceRowIndex) Then
            Me.InvisibleRows.Add(dataSourceRowIndex)
        End If
        Me.View.RefreshData()
    End Sub

    ''' <summary>
    ''' Muestra una fila
    ''' </summary>
    ''' <param name="dataSourceRowIndex">Indice de la fila</param>
    Public Sub ShowRow(ByVal dataSourceRowIndex As Int32)
        If Me.IsRowInvisible(dataSourceRowIndex) Then
            Me.InvisibleRows.Remove(dataSourceRowIndex)
        End If
        Me.View.RefreshData()
    End Sub

    ''' <summary>
    ''' Cambia la visibilidad de una fila
    ''' </summary>
    ''' <param name="dataSourceRowIndex">Indice de la fila</param>
    Public Sub ToggleRowVisibility(ByVal dataSourceRowIndex As Int32)
        If Me.IsRowInvisible(dataSourceRowIndex) Then
            Me.ShowRow(dataSourceRowIndex)
        Else
            Me.HideRow(dataSourceRowIndex)
        End If
    End Sub

    ''' <summary>
    ''' Subscribe a los eventos de la vista a observar
    ''' </summary>
    Private Sub SubscribeEvents()
        If Me.View IsNot Nothing Then
            AddHandler Me.View.CustomRowFilter, AddressOf GridView_CustomRowFilter
        End If
    End Sub

    ''' <summary>
    ''' Desubscribe a los eventos de la vista a observar
    ''' </summary>
    Private Sub UnsubscribeEvents()
        If Me.View IsNot Nothing Then
            RemoveHandler Me.View.CustomRowFilter, AddressOf GridView_CustomRowFilter
        End If
    End Sub

    ''' <summary>
    ''' Obtiene un valor que indica si la fila esta visible
    ''' </summary>
    ''' <param name="dataSourceRowIndex">Indice de la fila</param>
    ''' <returns>Valor que indica si la fila esta visible</returns>
    Public Function IsRowInvisible(ByVal dataSourceRowIndex As Int32) As Boolean
        Return Me.InvisibleRows.Contains(dataSourceRowIndex)
    End Function

#End Region

End Class