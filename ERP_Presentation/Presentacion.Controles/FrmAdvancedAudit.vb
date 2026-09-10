'***********************************************************************
' Assembly         : Presentacion.Controls
' Author           : Juan Diego Diaz
' Created          : 25-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Presentation.Controls.MVP
Imports DevExpress.Data.Async.Helpers
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports System.Runtime.CompilerServices
Imports System.Reflection
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid

''' <summary>
''' Fronta de Auditoria Avanzada
''' </summary>
Public Class FrmAdvancedAudit

#Region "Fields"

    ''' <summary>
    ''' Variable con Id del formulario
    ''' </summary>
    Private _idForm As String
    ''' <summary>
    ''' Variable con Id del registro seleccionado
    ''' </summary>
    Private _idEntity As String
    ''' <summary>
    ''' Variable el modelo de auditoria avanzada
    ''' </summary>
    Private _model As MAdvancedAudit

    ''' <summary>
    ''' Variable para el nombre de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private _entityName As String

    Private _controlAction As String

#End Region

#Region "Builders, Methods and Functions"

    ''' <summary>
    ''' Constructor del formulario
    ''' </summary>
    Sub New(entityName As String, IdForm As String, IdEntity As String)
        Me._idForm = IdForm
        Me._idEntity = IdEntity
        Me._entityName = entityName
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Metodo para la carga inicial del formulario
    ''' </summary>
    Private Sub FrmAdvancedAudit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._model = New MAdvancedAudit
        INDAuditCGc.DataSource = Me._model.GetAuditC(Me._entityName, Me._idForm, Me._idEntity)
    End Sub

    ''' <summary>
    ''' Evento para seleccionar un detalle de auditoria avanzada
    ''' </summary>
    Private Sub RepositoryItemButtonEdit1_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDRepositoryDetailBte.ButtonClick
        Dim objetoSeleccion = CType(Me.INDAuditCGc.DefaultView.GetRow(CType(Me.INDAuditCGc.DefaultView, GridView).FocusedRowHandle()), ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        Dim data As AuditXpo = CType(objetoSeleccion.OriginalRow, AuditXpo)
        'LayoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.INDAuditDetalGc.DataSource = Me._model.GetAuditList(data.Id, Me._entityName)
        Me.INDAuditDetalGc.RefreshDataSource()
        Me._controlAction = data.Accion
        Dim View As GridView = INDAuditDetalGc.FocusedView
        If (View.IsMasterRow(View.FocusedRowHandle)) Then
            Dim detailIndex As Integer = 0
            View.SetMasterRowExpandedEx(View.FocusedRowHandle, detailIndex, True)
            Dim childView As ColumnView = View.GetDetailView(View.FocusedRowHandle, detailIndex)
            If Not childView Is Nothing Then
                childView.ZoomView()
                ' Perform some actions. 
                ' ... 
                'childView.NormalView()
            End If
        End If
        INDlycGroupAuditAdvanced.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlycGroupAuditDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
    End Sub

    ''' <summary>
    ''' Evento click sobre el boton de registros de creación y actualización
    ''' </summary>
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles INDRecordCreateUpdateSmb.Click
        Me.LayoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.LayoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.LayoutControlGroup3.Text = "Registros de Creación, Modificación, Anulación y Confirmación"
        'Me.LayoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.ActionCCol.GroupIndex = 0
        INDAuditCGc.DataSource = Me._model.GetAuditC(Me._entityName, Me._idForm, Me._idEntity)
    End Sub

    ''' <summary>
    ''' Evento click sobre el boton de registros de eliminación
    ''' </summary>
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles INDRecordDeleteSmb.Click
        Me.LayoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.LayoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.LayoutControlGroup3.Text = "Registros de Eliminación"
        Me.LayoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.ActionCCol.GroupIndex = -1
        INDAuditCGc.DataSource = Me._model.GetAuditCDelete(Me._entityName, Me._idForm)
    End Sub

    ''' <summary>
    ''' Evento para cambiar el valor mostrado de la columna de acciones
    ''' </summary>
    Private Sub GridView_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDAuditCGv.CustomColumnDisplayText
        If e.Column.FieldName = "Accion" Then
            If e.Value IsNot Nothing Then
                Select Case e.Value.ToString.Trim()
                    Case "1"
                        e.DisplayText = "Crear"
                    Case "2"
                        e.DisplayText = "Actualizar"
                    Case "3"
                        e.DisplayText = "Eliminar"
                    Case "4"
                        e.DisplayText = "Consultar"
                    Case "5"
                        e.DisplayText = "Confirmar"
                    Case "6"
                        e.DisplayText = "Anular"
                    Case "7"
                        e.DisplayText = "Desconfirmar"
                End Select
            End If
        End If
    End Sub

#End Region

    Private Sub INDAuditDetailGv_CustomRowCellEdit(sender As Object, e As CustomRowCellEditEventArgs)
        'If Not Me.INDAuditDetalGc.DefaultView.GetRow(e.RowHandle).GetType() = GetType(DevExpress.Data.NotLoadedObject) Then
        '    Dim objetoSeleccion = CType(Me.INDAuditDetalGc.DefaultView.GetRow(e.RowHandle), ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        '    Dim dataPrevious = objetoSeleccion.OriginalRow.PreviousValue
        '    Dim dataNew = objetoSeleccion.OriginalRow.NewValue
        '    If dataPrevious <> dataNew Then
        '        e.Column.AppearanceCell.BackColor = Color.RosyBrown
        '    End If
        'End If
    End Sub

    Private Shared Function IsAnonymousType(type As Type) As Boolean
        ' HACK: The only way to detect anonymous types right now.
        Return Attribute.IsDefined(type, GetType(CompilerGeneratedAttribute), False) AndAlso type.IsGenericType AndAlso _
            type.Name.Contains("AnonymousType") AndAlso (type.Name.StartsWith("<>", StringComparison.OrdinalIgnoreCase) OrElse _
                                                         type.Name.StartsWith("VB$", StringComparison.OrdinalIgnoreCase)) AndAlso _
                                                     (type.Attributes And TypeAttributes.NotPublic) = TypeAttributes.NotPublic
    End Function

    Private Sub INDAuditDetailGv_RowCellStyle(sender As Object, e As RowCellStyleEventArgs)
        'Dim objetoSeleccion = CType(Me.INDAuditDetalGc.DefaultView.GetRow(CType(Me.INDAuditDetalGc.DefaultView, GridView).FocusedRowHandle()), ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        'Dim data As Audit_AuditDetail = CType(objetoSeleccion.OriginalRow, Audit_AuditDetail)
        'If data IsNot Nothing Then
        '    If data.NewValue IsNot Nothing AndAlso data.PreviousValue IsNot Nothing AndAlso data.NewValue.ToString <> data.PreviousValue.ToString Then
        '        e.Appearance.BackColor = Color.RosyBrown
        '        e.Appearance.BackColor2 = Color.Azure
        '    End If
        'End If
        'If Me._controlAction = "2" Then
        '    If Not Me.INDAuditDetalGc.DefaultView.GetRow(e.RowHandle).GetType() = GetType(DevExpress.Data.NotLoadedObject) Then
        '        Dim objetoSeleccion = CType(Me.INDAuditDetalGc.DefaultView.GetRow(e.RowHandle), ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        '        Dim dataPrevious = objetoSeleccion.OriginalRow.PreviousValue
        '        Dim dataNew = objetoSeleccion.OriginalRow.NewValue
        '        If dataPrevious <> dataNew Then
        '            e.Appearance.BackColor = Color.RosyBrown
        '        End If
        '    End If
        'End If
    End Sub

    Private Sub INDAuditDetailGv_RowStyle(sender As Object, e As RowStyleEventArgs)
        Dim gridViewRow As GridView = CType(sender, GridView)
        If gridViewRow.GetRow(e.RowHandle) IsNot Nothing Then
            If Not gridViewRow.GetRow(e.RowHandle).GetType() = GetType(DevExpress.Data.NotLoadedObject) Then
                Dim objetoDetalle = CType(gridViewRow.GetRow(e.RowHandle), AuditDetailXpo)
                If objetoDetalle.GetType() = GetType(AuditDetailXpo) Then
                    Dim dataPrevious = objetoDetalle.ValorAnterior
                    Dim dataNew = objetoDetalle.NuevoValor
                    If dataPrevious <> dataNew Then
                        e.Appearance.BackColor = Color.RosyBrown
                        e.Appearance.BackColor2 = Color.WhiteSmoke
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub CtrNavigation1_ClickBack() Handles CtrNavigation1.ClickBack
        INDlycGroupAuditAdvanced.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlycGroupAuditDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDGridViewDetail.CollapseAllGroups()
        INDGridViewDetail.CollapseAllDetails()
        INDGridViewDetail.CollapseMasterRow(0)
        INDGridViewDetail.CollapseGroupRow(0)
        'While INDAuditDetalGc.ViewCollection.Count > 0
        '    INDAuditDetalGc.ViewCollection.RemoveAt(0)
        'End While
        INDAuditDetalGc.DataSource = Nothing
        INDAuditDetalGc.RefreshDataSource()
        INDAuditDetalGc.Refresh()
    End Sub

    Private Sub INDAuditDetalGc_ViewRegistered(sender As Object, e As DevExpress.XtraGrid.ViewOperationEventArgs) Handles INDAuditDetalGc.ViewRegistered
        Dim gridView As GridView = CType(e.View, GridView)
        AddHandler gridView.CustomColumnDisplayText, AddressOf GridView_CustomColumnDisplayText
        If gridView.LevelName = "Propiedades" Then
            AddHandler gridView.RowStyle, AddressOf INDAuditDetailGv_RowStyle
        End If
        If gridView.LevelName = "Propiedades" Or gridView.LevelName = "Listas" Or gridView.LevelName = "DetalleAgregado" Then
            For Each column As GridColumn In CType(e.View, GridView).Columns
                If Not (column.FieldName = "Accion" Or column.FieldName = "Entidad" Or column.FieldName = "Usuario" Or column.FieldName = "UsuarioWindows" Or column.FieldName = "Propiedad" Or column.FieldName = "ValorAnterior" Or column.FieldName = "NuevoValor") Then
                    column.Visible = False
                End If
            Next
        Else
            'For Each column As GridColumn In CType(e.View, GridView).Columns
            '    column.Visible = False
            'Next
            For I = 0 To gridView.SelectedRowsCount - 1
                gridView.DeleteRow(gridView.FocusedRowHandle)
            Next
        End If
    End Sub

    Private Sub INDAuditDetalGc_DefaultViewChanged(sender As Object, e As EventArgs) Handles INDAuditDetalGc.DefaultViewChanged
        Dim view As GridView = DirectCast(sender, GridControl).DefaultView
        Dim obj = view.GetRow(view.FocusedRowHandle)
        Console.WriteLine("")
        Console.WriteLine("------------")
        INDlbZoom.Text = imprimir(view.ParentView)
        Console.WriteLine("------------")
        Console.WriteLine("")
    End Sub

    Private Function imprimir(view As GridView) As String
        If (view IsNot Nothing) Then
            Dim obj = view.GetRow(view.FocusedRowHandle)
            Dim strResult As String = ""
            If (obj IsNot Nothing) Then
                If obj.GetType() = GetType(AuditXpo) Then
                    Dim objTmp As AuditXpo = CType(obj, AuditXpo)
                    strResult = objTmp.Entidad
                ElseIf obj.GetType() = GetType(AuditDetailXpo) Then
                    Dim objTmp As AuditDetailXpo = CType(obj, AuditDetailXpo)
                    strResult = objTmp.Propiedad
                End If
            End If
            If (view.ParentView IsNot Nothing) Then
                strResult = imprimir(view.ParentView) + "/" + strResult
            End If
            Return strResult
        Else
            Return ""
        End If
    End Function


End Class

