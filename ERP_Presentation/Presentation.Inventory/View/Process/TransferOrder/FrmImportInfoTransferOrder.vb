'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Juan Carlos Bermudez Gutierrez 
' Created          : 20/05/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Inventory.MVP
Imports System.Text
Imports Presentation.Controls
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository.View
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo

#End Region

Public Class FrmImportInfoTransferOrder

#Region "EVENTS"
    ''' <summary>
    ''' evento para obtener el listado del detalle de la remision de entrada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event GetListTransferOrderDetail(sender As Object, e As GetListTransferOrderDetailEventArgs)
#End Region

#Region "GLOBALS"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Inventory"
    ''' <summary>
    ''' listado de la orden de compra
    ''' </summary>
    ''' <remarks></remarks>
    Dim listInventoryRequestDetailXpo As List(Of ViewListRequestDetailXpo)

    ''' <summary>
    ''' listado del detalla de la orden de traslado para validar que los productos no se repitan con la misma fuente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listTransferOrderDetailValidation As List(Of TransferOrderDetail)

    ''' <summary>
    ''' tipo de orden 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _orderType As Byte

    ''' <summary>
    ''' despachar a 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _dispatchTo As Byte?

    ''' <summary>
    ''' id de la unidad funcional o el almacen para filtrar las solicitudes
    ''' </summary>
    ''' <remarks></remarks>
    Dim _filterFunctionalUnitWarehouse As Integer

    ''' <summary>
    ''' id del almacen de origen
    ''' </summary>
    ''' <remarks></remarks>
    Dim _warehouseId As Integer


#End Region

#Region "Properties"

    ''' <summary>
    ''' propiedad para asignar el tipo de orden de traslado
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property OrderType As Byte
        Set(value As Byte)
            _orderType = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para asignar despachar a de orden de traslado
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property DispatchTo As Byte?
        Set(value As Byte?)
            _dispatchTo = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para asignar el id de la unidad funcional o del almacen para filtrar las solicitudes
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property FilterFunctionalUnitWarehouse As Integer
        Set(value As Integer)
            _filterFunctionalUnitWarehouse = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para asignar el id del almacen de origen
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property WarehouseId As Integer
        Set(value As Integer)
            _warehouseId = value
        End Set
    End Property

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' propiedad para para pasar el listado del detalla de la remision
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ListTransferOrderDetailValidation As List(Of TransferOrderDetail)
        Set(value As List(Of TransferOrderDetail))
            If value IsNot Nothing Then
                _listTransferOrderDetailValidation = New List(Of TransferOrderDetail)(value.ToArray())
            End If
        End Set
    End Property
#End Region

#Region "Handles"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        listInventoryRequestDetailXpo = Nothing
        _listTransferOrderDetailValidation = Nothing
        _orderType = Nothing
        _dispatchTo = Nothing
        _filterFunctionalUnitWarehouse = Nothing
        _warehouseId = Nothing
    End Sub

    ''' <summary>
    ''' se dispara al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmImportInfoTransferOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LoadInventoryRequest()
    End Sub

    ''' <summary>
    ''' funcion que obtiene la informacion de los items que se muestran en el popup
    ''' </summary>
    Private Sub LoadInventoryRequest()
        Using model As New MBusqueda
            '************Cargo los datos de orden de compra*************'
            listInventoryRequestDetailXpo = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).InventoryService.ListViewRequestDetailFiltered(_filterFunctionalUnitWarehouse, _orderType, _dispatchTo).ToEntityList(Of ViewListRequestDetailXpo)

            INDGcImportInfo.DataSource = Nothing
            INDGcImportInfo.DataSource = listInventoryRequestDetailXpo
            INDGcImportInfo.RefreshDataSource()
        End Using
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' cierra el popup al presionar la tecla esc
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmImportInfoTransferOrder_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        Dim errors = ValidateRecords()
        If errors.Count > 0 Then
            Using formulario As New FrmListErrors(errors)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If
        Dim args As New GetListTransferOrderDetailEventArgs
        args.ListTransferOrdeDetail = GenerateTransferOrderDetail()
        If args.ListTransferOrdeDetail.Count > 0 Then
            Me.Close()
            RaiseEvent GetListTransferOrderDetail(Nothing, args)
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' se dispara ala cambiar el valor de la cantidad a importar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSpeQuantityImport_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        Dim IteminventoryRequestDetailTem = DirectCast(INDGvImportInfo.GetFocusedRow(), InventoryRequestDetail)
        If CInt(e.NewValue) > IteminventoryRequestDetailTem.OutstandingQuantity Then
            e.NewValue = IteminventoryRequestDetailTem.OutstandingQuantity
            INDGcImportInfo.RefreshDataSource()
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("QuantityRequest", MODULE_NAME))
            e.Cancel = True
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDrptCheActivate_EditValueChanged(sender As Object, e As EventArgs) Handles INDrptCheActivate.EditValueChanged
        Dim checkControl = DirectCast(sender, DevExpress.XtraEditors.CheckEdit)
        Dim request = DirectCast(INDGvImportInfo.GetFocusedRow(), ViewListRequestDetailXpo)
        request.Activated = checkControl.EditValue
        SetImageActivateColumn()
        Me.INDGcImportInfo.RefreshDataSource()
        Me.INDGcImportInfo.Invalidate()
    End Sub

#End Region

#Region "MouseDoubleClick"

    'Public Shared Function ToList(Of TEntity)(arrayList As ArrayList) As List(Of TEntity)
    '    Dim list As New List(Of TEntity)(arrayList.Count)
    '    For Each instance As TEntity In arrayList
    '        list.Add(instance)
    '    Next
    '    Return list
    'End Function
    ''' <summary>
    ''' activa o desactiva todos los items del listado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGcImportInfo_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDGcImportInfo.MouseDoubleClick
        Dim hitPoint = Me.INDGvImportInfo.CalcHitInfo(e.Location)
        If hitPoint.Column IsNot Nothing Then
            If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDgclState") Then
                If INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of ViewListRequestDetailXpo).Where(Function(s) s.Activated).ToList().Count = INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of ViewListRequestDetailXpo).Count Then
                    INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of ViewListRequestDetailXpo).ForEach(Sub(x) x.Activated = False)
                Else
                    INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of ViewListRequestDetailXpo).ForEach(Sub(x) x.Activated = True)
                End If
                Me.INDGcImportInfo.RefreshDataSource()
            End If
            Me.INDGcImportInfo.Invalidate()
        End If
    End Sub
#End Region

#End Region

#Region "METHODS"

    ''' <summary>
    ''' metodo para establecer la imagen a la columna de activar o inactivar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetImageActivateColumn()
        Dim listActivated = listInventoryRequestDetailXpo.FindAll(Function(x) x.Activated = True)
        If listActivated.Count = listInventoryRequestDetailXpo.Count Then
            Me.INDgclState.Image = Global.Presentation.Inventory.My.Resources.Resources.check
        Else
            Me.INDgclState.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
        End If
    End Sub

    ''' <summary>
    ''' metodo para generar los detalles de la orden de traslado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateTransferOrderDetail() As List(Of TransferOrderDetail)
        Dim listTransferOrderDetailTmp As New List(Of TransferOrderDetail)
        Dim TransferOrderDetailTmp As TransferOrderDetail
        'genero los detalles por orden de compra

        For Each item In listInventoryRequestDetailXpo.FindAll(Function(x) x.Activated = True And x.ItemInvalid = False)
            TransferOrderDetailTmp = New TransferOrderDetail
            With TransferOrderDetailTmp
                .ComponentType = item.ComponentType
                .ItemId = item.ItemId
                If .ComponentType = 3 Then
                    .ProductId = item.ItemId
                End If
                If item.EntitySource = 1 Then
                    .InventoryRequestDetailId = item.Id
                Else
                    .InventoryRequestDetailOtherId = item.Id
                End If
                .QuantityImport = item.OutstandingQuantity
            End With
            listTransferOrderDetailTmp.Add(TransferOrderDetailTmp)
        Next
        Return listTransferOrderDetailTmp
    End Function

    ''' <summary>
    ''' valida los datos para poder importarlos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateRecords() As List(Of String)
        Dim errors As New List(Of String)
        Dim listInventoryRequestDetailActivated = listInventoryRequestDetailXpo.FindAll(Function(x) x.Activated = True)
        If (listInventoryRequestDetailActivated.Count = 0) Then
            errors.Add(ResourceManager.GetString("NoSelectedItem", MODULE_NAME))
        End If
        If errors.Count = 0 Then
            'valido que el producto no este con la misma solicitud solo en los items que estan seleccionados
            If _listTransferOrderDetailValidation IsNot Nothing AndAlso _listTransferOrderDetailValidation.Count > 0 Then
                For Each item In listInventoryRequestDetailActivated
                    If item.EntitySource = 1 Then
                        Dim transferOrderDetailTmp = _listTransferOrderDetailValidation.Find(Function(x) If(IsNothing(x.InventoryRequestDetailId) = True, 0, x.InventoryRequestDetailId) = item.Id)
                        If transferOrderDetailTmp IsNot Nothing Then
                            errors.Add(String.Format(ResourceManager.GetString("ProductAdded", MODULE_NAME), transferOrderDetailTmp.DescriptionProduct, ResourceManager.GetString("TransferOrder", MODULE_NAME)))
                            item.ItemInvalid = True
                            Continue For
                        End If
                    Else
                        Dim transferOrderDetailTmp = _listTransferOrderDetailValidation.Find(Function(x) If(IsNothing(x.InventoryRequestDetailOtherId) = True, 0, x.InventoryRequestDetailOtherId) = item.Id)
                        If transferOrderDetailTmp IsNot Nothing Then
                            errors.Add(String.Format(ResourceManager.GetString("ProductAdded", MODULE_NAME), transferOrderDetailTmp.DescriptionProduct, ResourceManager.GetString("TransferOrder", MODULE_NAME)))
                            item.ItemInvalid = True
                            Continue For
                        End If
                    End If
                Next
            End If
        End If
        Return errors
    End Function

#End Region


End Class