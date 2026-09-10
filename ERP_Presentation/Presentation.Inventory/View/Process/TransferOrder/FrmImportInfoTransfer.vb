'***********************************************************************
' Assembly         : Presentation.Inventory
' Author           : Johan Sebastian Cuellar Esquivel
' Created          : 24-06-2021
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Inventory.MVP
#End Region

Public Class FrmImportInfoTransfer


#Region "EVENTS"
    ''' <summary>
    ''' evento para obtener el listado del detalle de la remision de entrada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event GetListTransferOrderDetail(sender As Object, e As GetListTransferOrderDetailEventArgs)


    ''' <summary>
    ''' evento para agregar un producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddTransferOrderDetail(sender As Object, e As AddProductTransferOrderDetailEventArgs)
#End Region

#Region "GLOBALS"

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Inventory"

    ''' <summary>
    ''' listado de la orden de compra
    ''' </summary>
    ''' <remarks></remarks>
    Dim listInventoryRequestDetailOther As New List(Of ViewListRequestDetailImport)
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

    ''' <summary>
    ''' Lista que se llena desde la vista
    ''' </summary>
    Dim listInventoryRequestDetailOtherXpo As XPCollection

    ''' <summary>
    ''' listado del detalle de la orden de traslado cuando se importa informacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listTransferOrderDetailImportInfo As List(Of TransferOrderDetail)


    Dim _settingInventory As SettingInventory


    Dim _currentDate As Date

    ''' <summary>
    ''' detalle de la orden de traslado
    ''' </summary>
    ''' <remarks></remarks>
    Public transferOrderDetail As TransferOrderDetail

    Dim listTransferOrderDetailTmp As New List(Of TransferOrderDetail)

    ''' <summary>
    ''' entidad de producto
    ''' </summary>
    ''' <remarks></remarks>
    Dim product As InventoryProduct

    ''' <summary>
    ''' listado del inventario fisico 
    ''' </summary>
    ''' <remarks></remarks>
    Dim listPhysicalInventoryCrystalProduct As List(Of PhysicalInventory)

    Dim BteCum As Integer = 0

    Public Property ListTransferOrderDetailImportInfo As List(Of TransferOrderDetail)
        Get
            Return _listTransferOrderDetailImportInfo
        End Get
        Set(value As List(Of TransferOrderDetail))
            _listTransferOrderDetailImportInfo = value
        End Set
    End Property

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
        listInventoryRequestDetailOther = Nothing
        _listTransferOrderDetailValidation = Nothing
        ListTransferOrderDetailValidation = Nothing
        _orderType = Nothing
        _dispatchTo = Nothing
        _filterFunctionalUnitWarehouse = Nothing
        _warehouseId = Nothing
        _listTransferOrderDetailImportInfo = Nothing
    End Sub

    Private Sub LoadDataSource()
        IndigoGridControl1.RefreshGrid(INDGcImportInfoTransfer)
        Using model As New MBusqueda
            Dim filter() As Object = {_filterFunctionalUnitWarehouse, _orderType, _dispatchTo}
            listInventoryRequestDetailOtherXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListViewRequestDetail(CInt(filter.ElementAt(0)), CByte(filter.ElementAt(1)), CByte(filter.ElementAt(2)))
            LoadListInventoryRequest(listInventoryRequestDetailOtherXpo)
            INDGcImportInfoTransfer.DataSource = Nothing
            INDGcImportInfoTransfer.DataSource = listInventoryRequestDetailOther
            INDGcImportInfoTransfer.RefreshDataSource()
        End Using
    End Sub



    ''' <summary>
    ''' se dispara al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmImportInfoTransferOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeButton()
        LoadDataSource()
    End Sub

    Private Sub InitializeButton()
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.EntregaManual) = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
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
    ''' Guardar
    ''' </summary>
    Public Async Sub Guardar()
        BteCum = 0
        Dim errors = ValidateRecords()
        If errors.Count > 0 Then
            Using formulario As New FrmListErrors(errors)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If
        If listInventoryRequestDetailOther.Count > 0 And BteCum = 0 Then
            Dim args As New GetListTransferOrderDetailEventArgs
            args.ListTransferOrdeDetail = GenerateTransferOrderDetail()
            args.ListDetailMedicineAndSupplie = GenerateListDetailMedicineAndSupplie()
            If args.ListTransferOrdeDetail.Count > 0 Then
                Me.Close()
                RaiseEvent GetListTransferOrderDetail(Nothing, args)
            End If
        End If

    End Sub


    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_EntregaManual() Handles BarraBotones.Click_EntregaManual
        Dim list As New List(Of TransferOrderDetail)
        Dim listDetail As New List(Of TransferOrderDetail)

        Dim errors = ValidateRecords()
        If errors.Count > 0 Then
            Using formulario As New FrmListErrors(errors)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If
        AsyncLoader(True)

        list = GenerateTransferOrderDetail()
        listDetail = GenerateListDetailMedicineAndSupplie()

        Dim errorString As New StringBuilder

        If list.Count > 0 Then
            ManualDelivery(list, listDetail, errorString)
        End If

        AsyncLoader(False)
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        Dim errors = ValidateRecords()
        If errors.Count > 0 Then
            Using formulario As New FrmListErrors(errors)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If
        Dim list As New List(Of TransferOrderDetail)
        list = GenerateTransferOrderDetail()
        If listInventoryRequestDetailOther.Count > 0 And list.Count > 0 Then
            Using model As New MTransferOrder(Me.Tag.ToString())
                AsyncLoader(True)
                Dim listActivated = listInventoryRequestDetailOther.FindAll(Function(x) list.Any(Function(y) x.Row = If(y.InventoryRequestDetailId IsNot Nothing, y.InventoryRequestDetailId, y.InventoryRequestDetailOtherId)))
                Dim result = model.ChangeStateInventoryRequestDetail(listActivated)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = "Se anulo correctamente, los items seleccionados"
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "No se pudo anular, item de la solicitud"
                End If
                LoadDataSource()
                AsyncLoader(False)
            End Using
        End If
    End Sub

    Private Sub INDRptBteCUM_Click(sender As Object, e As EventArgs) Handles INDRptBteCUM.Click

        BteCum = 1
        Dim request = DirectCast(INDGvImportInfoTransfer.GetFocusedRow(), ViewListRequestDetailImport)
        Dim args As New GetListTransferOrderDetailEventArgs

        If request.Activated = False Then
            Mensaje(EeventViewerImages.Advertencia) = "Debes seleccionar una solicitud"
        Else
            args.ListTransferOrdeDetail = GenerateTransferOrderDetail()
            args.ListDetailMedicineAndSupplie = GenerateListDetailMedicineAndSupplie()
            RaiseEvent GetListTransferOrderDetail(Nothing, args)
        End If

        'Me.Close()

    End Sub


#End Region

#Region "EditValueChanged"

    Private Sub INDRicSelectOption_EditValueChanged(sender As Object, e As EventArgs) Handles INDRicSelectOption.EditValueChanged
        Dim checkControl = DirectCast(sender, DevExpress.XtraEditors.CheckEdit)
        Dim request = DirectCast(INDGvImportInfoTransfer.GetFocusedRow(), ViewListRequestDetailImport)
        request.Activated = checkControl.EditValue
        SetImageActivateColumn()
        Me.INDGcImportInfoTransfer.RefreshDataSource()
        Me.INDGvImportInfoTransfer.Invalidate()
    End Sub


#End Region

#Region "MouseDoubleClick"

    ''' <summary>
    ''' activa o desactiva todos los items del listado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGcImportInfo_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDGcImportInfoTransfer.MouseDoubleClick
        Dim hitPoint = Me.INDGvImportInfoTransfer.CalcHitInfo(e.Location)
        If hitPoint.Column IsNot Nothing Then
            If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDgclState") Then
                If INDGvImportInfoTransfer.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of ViewListRequestDetailImport).Where(Function(s) s.Activated).ToList().Count = INDGvImportInfoTransfer.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of ViewListRequestDetailImport).Count Then
                    INDGvImportInfoTransfer.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of ViewListRequestDetailImport).ForEach(Sub(x) x.Activated = False)
                Else
                    INDGvImportInfoTransfer.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of ViewListRequestDetailImport).ForEach(Sub(x) x.Activated = True)
                End If
                Me.INDGcImportInfoTransfer.RefreshDataSource()
            End If
            Me.INDGcImportInfoTransfer.Invalidate()
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
        Dim listActivated = listInventoryRequestDetailOther.FindAll(Function(x) x.Activated = True)
        If listActivated.Count = listInventoryRequestDetailOther.Count Then
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

        Dim TransferOrderDetailTmp As TransferOrderDetail
        Dim product As New InventoryProductXpo
        listTransferOrderDetailTmp = New List(Of TransferOrderDetail)
        Dim request = DirectCast(INDGvImportInfoTransfer.GetFocusedRow(), ViewListRequestDetailImport)
        If BteCum = 1 Then
            For Each item As ViewListRequestDetailImport In listInventoryRequestDetailOther.FindAll(Function(x) x.Activated = True And x.ItemInvalid = False And x.Row = request.Row)
                TransferOrderDetailTmp = New TransferOrderDetail
                With TransferOrderDetailTmp
                    If item.InventoryRequestDetailType = 1 Then 'Detalle 1
                        .InventoryRequestDetailId = item.Row
                    Else 'Detalle 2
                        .InventoryRequestDetailOtherId = item.Row
                    End If
                    .ProductId = item.EntityId
                    .QuantityImport = item.OutstandingQuantity
                    .DescriptionProduct = product.Code + " - " + product.Name
                End With
                listTransferOrderDetailTmp.Add(TransferOrderDetailTmp)
            Next
        Else
            For Each item As ViewListRequestDetailImport In listInventoryRequestDetailOther.FindAll(Function(x) x.Activated = True And x.ItemInvalid = False)
                TransferOrderDetailTmp = New TransferOrderDetail
                With TransferOrderDetailTmp
                    If item.InventoryRequestDetailType = 1 Then 'Detalle 1
                        .InventoryRequestDetailId = item.Row
                    Else 'Detalle 2
                        .InventoryRequestDetailOtherId = item.Row
                    End If
                    .ProductId = item.EntityId
                    .QuantityImport = item.OutstandingQuantity
                    .DescriptionProduct = product.Code + " - " + product.Name
                End With
                listTransferOrderDetailTmp.Add(TransferOrderDetailTmp)
            Next
        End If


        Using modelPhysical As New MCtrPhysicalInventory(Me.Tag)
            For Each item In listTransferOrderDetailTmp
                listPhysicalInventoryCrystalProduct = modelPhysical.GetListPhysicalInventory(item.ProductId, _warehouseId)
                If listPhysicalInventoryCrystalProduct.Count > 0 Then

                    listPhysicalInventoryCrystalProduct = listPhysicalInventoryCrystalProduct.
                                OrderBy(Function(d) If(_warehouseId > 0 AndAlso _warehouseId = d.WarehouseId, 0, 1)).
                                ThenBy(Function(d) If(d.BatchSerialExpiredDate Is Nothing, DateTime.MaxValue, d.BatchSerialExpiredDate)).ToList()

                    For Each PhysicalInventory In listPhysicalInventoryCrystalProduct
                        Dim transferOrderDetailBatchSerial As New TransferOrderDetailBatchSerial
                        transferOrderDetailBatchSerial.PhysicalInventoryId = PhysicalInventory.Id
                        transferOrderDetailBatchSerial.Quantity = PhysicalInventory.QuantityDeliver
                        transferOrderDetailBatchSerial.OutstandingQuantity = PhysicalInventory.QuantityDeliver
                        transferOrderDetailBatchSerial.CodeNameProduct = TransferOrderDetailTmp.DescriptionProduct
                        transferOrderDetailBatchSerial.CodeBatchSerial = PhysicalInventory.CodeNameBatchSerial
                        transferOrderDetailBatchSerial.CodeNameWarehouse = PhysicalInventory.CodeNameWarehouse
                        If transferOrderDetail IsNot Nothing And transferOrderDetailBatchSerial IsNot Nothing Then
                            transferOrderDetail.TransferOrderDetailBatchSerial.Add(transferOrderDetailBatchSerial)
                        End If
                    Next
                End If
            Next
        End Using

        Return listTransferOrderDetailTmp
    End Function

    ''' <summary>
    ''' Agregar hijos al producto
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateListDetailMedicineAndSupplie() As List(Of TransferOrderDetail)
        Dim listTransferOrderDetailOtherTmp As New List(Of TransferOrderDetail)
        Dim TransferOrderDetailTmp As TransferOrderDetail
        Dim Listproducts As New List(Of InventoryProductXpo)
        Dim ListATC As New ATC
        Dim ListSupplie As New InventorySupplieXpo
        Dim Codeproduct As New ViewListRequestDetailImport


        Dim request = DirectCast(INDGvImportInfoTransfer.GetFocusedRow(), ViewListRequestDetailImport)
        'genero los detalles por orden de compra, solo obtiene la cabecera segun el caso de la sección InventoryRequestDetailOtherId 
        For Each item In listInventoryRequestDetailOther.FindAll(Function(x) x.Activated = True And x.ItemInvalid = False And x.Row = request.Row)

            If item.ComponentType = 1 Then 'Medicamento
                Using model As New MATC(Tag)
                    Listproducts = model.GetProductByATCXpo(item.EntityId)
                End Using
            ElseIf item.ComponentType = 2 Then 'Insumo
                Using model As New MInventorySupplie(Tag)
                    Listproducts = model.GetProductBySupplieXpo(item.EntityId)
                End Using
            End If

            If Listproducts IsNot Nothing And Listproducts.Count > 0 Then
                For Each ItemTwo In Listproducts
                    TransferOrderDetailTmp = New TransferOrderDetail
                    With TransferOrderDetailTmp
                        If item.InventoryRequestDetailType = 1 Then 'Detalle 1                            
                            .InventoryRequestDetailId = item.Row
                        Else 'Detalle 2
                            .InventoryRequestDetailOtherId = item.Row
                        End If
                        .ProductId = ItemTwo.Id
                        .QuantityImport = item.OutstandingQuantity
                    End With
                    listTransferOrderDetailOtherTmp.Add(TransferOrderDetailTmp)
                Next
            Else

                TransferOrderDetailTmp = New TransferOrderDetail
                With TransferOrderDetailTmp
                    If item.InventoryRequestDetailType = 1 Then 'Detalle 1
                        .InventoryRequestDetailId = item.Row
                    Else 'Detalle 2
                        .InventoryRequestDetailOtherId = item.Row
                    End If
                    .ProductId = item.EntityId
                    .QuantityImport = item.OutstandingQuantity
                End With

                listTransferOrderDetailOtherTmp.Add(TransferOrderDetailTmp)
            End If
        Next
        Return listTransferOrderDetailOtherTmp

    End Function

    ''' <summary>
    ''' metodo para cargar manualmente las cantidades que se van a entregar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ManualDelivery(listTransferOrder As List(Of TransferOrderDetail), listTransferOrderDetail As List(Of TransferOrderDetail), errors As StringBuilder)
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Exit Sub
        End If
        Dim listTransferOrderDetailtemp As List(Of TransferOrderDetail)
        listTransferOrderDetailtemp = New List(Of TransferOrderDetail)
        If listTransferOrderDetail IsNot Nothing Then
            For Each item In listTransferOrderDetail ''listTransferOrder

                Using model As New MInventoryProduct(Me.Tag)
                        product = model.GetInventoryProductByIdSimple(item.ProductId)
                    End Using
                    item.DescriptionProduct = product.Code + " - " + product.Name

                Using model As New MCtrPhysicalInventory(Me.Tag)
                    Dim listPhysicalInventoryCrystalProduct = model.GetListPhysicalInventory(item.ProductId, _warehouseId)
                    'Se realiza el ordenamiento de acuerdo al almacen y los productos con la fecha de vencimiento mas próxima
                    Dim warehouseId As Integer? = _warehouseId
                    If listPhysicalInventoryCrystalProduct.Count > 0 Then

                        listPhysicalInventoryCrystalProduct = listPhysicalInventoryCrystalProduct.
                            OrderBy(Function(d) If(warehouseId IsNot Nothing AndAlso warehouseId = d.WarehouseId, 0, 1)).
                            ThenBy(Function(d) If(d.BatchSerialExpiredDate Is Nothing, DateTime.MaxValue, d.BatchSerialExpiredDate)).ToList()

                        Dim listPhysicalInventory As New List(Of PhysicalInventory)
                        Dim outStandingQuantity = item.QuantityImport


                        For Each itemPhysical In listPhysicalInventoryCrystalProduct

                            Dim transferOrderDetailBatchSerial As New TransferOrderDetailBatchSerial
                            transferOrderDetailBatchSerial.PhysicalInventoryId = itemPhysical.Id
                            transferOrderDetailBatchSerial.Quantity = itemPhysical.QuantityDeliver

                            transferOrderDetailBatchSerial.CodeNameProduct = item.DescriptionProduct
                            transferOrderDetailBatchSerial.CodeBatchSerial = itemPhysical.CodeNameBatchSerial
                            transferOrderDetailBatchSerial.CodeNameWarehouse = itemPhysical.CodeNameWarehouse


                            If itemPhysical.Quantity >= outStandingQuantity Then
                                item.Quantity += outStandingQuantity
                                itemPhysical.QuantityDeliver = outStandingQuantity
                                transferOrderDetailBatchSerial.OutstandingQuantity = itemPhysical.QuantityDeliver
                                listPhysicalInventory.Add(itemPhysical)
                                item.TransferOrderDetailBatchSerial.Add(transferOrderDetailBatchSerial)
                                Exit For
                            Else
                                item.Quantity += itemPhysical.Quantity
                                itemPhysical.QuantityDeliver = itemPhysical.Quantity
                                outStandingQuantity -= itemPhysical.Quantity
                                transferOrderDetailBatchSerial.OutstandingQuantity = itemPhysical.QuantityDeliver
                                listPhysicalInventory.Add(itemPhysical)
                                item.TransferOrderDetailBatchSerial.Add(transferOrderDetailBatchSerial)
                            End If

                        Next

                    End If
                End Using
                If item.TransferOrderDetailBatchSerial.Count > 0 Then
                    listTransferOrderDetailtemp.Add(item)
                End If

            Next

        End If

        Dim args As New AddProductTransferOrderDetailEventArgs
        args.ImportDataMode = True
        args.ListTransferOrderDetail = listTransferOrderDetailtemp
        RaiseEvent AddTransferOrderDetail(Nothing, args)
        Me.Close()
    End Sub


    ''' <summary>
    ''' valida los datos para poder importarlos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateRecords() As List(Of String)
        Dim errors As New List(Of String)
        Dim listInventoryRequestDetailOtherActivated = listInventoryRequestDetailOther.FindAll(Function(x) x.Activated = True)
        If (listInventoryRequestDetailOtherActivated.Count = 0) Then
            errors.Add("Debes seleccionar una solicitud")
        End If
        Return errors
    End Function


    ''' <summary>
    ''' Metodo que carga el listado de detalles de contrato
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadListInventoryRequest(listXpo As XPCollection)
        listInventoryRequestDetailOther = New List(Of Domain.Entities.ViewListRequestDetailImport)
        If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
            For Each itemXpo As InventoryRepository.View.ViewListRequestDetailImportXpo In listXpo
                Dim InventoryRequestDetailOther As New Domain.Entities.ViewListRequestDetailImport
                With InventoryRequestDetailOther
                    .InventoryRequestDetailType = itemXpo.InventoryRequestDetailType
                    .Code = itemXpo.Code
                    .DocumentDate = itemXpo.DocumentDate
                    .Row = itemXpo.Row
                    .ComponentType = itemXpo.ComponentType
                    .EntityId = itemXpo.EntityId
                    .SourceCode = itemXpo.SourceCode
                    .SourceCodeName = itemXpo.SourceCodeName
                    .QuantityRequested = itemXpo.QuantityRequested
                    .QuantityDelivered = itemXpo.QuantityDelivered
                    .OutstandingQuantity = itemXpo.OutstandingQuantity
                    .DescriptionProduct = itemXpo.DescriptionProduct
                    .TargetWarehouseId = itemXpo.TargetWarehouseId
                    .TargetFunctionalUnitId = itemXpo.TargetFunctionalUnitId
                    .SourceWarehouseId = itemXpo.SourceWarehouseId
                    .CUMSourceCodeName = itemXpo.CUMSourceCodeName
                    .Status = itemXpo.Status
                    .Activated = False
                    .ItemInvalid = False
                    .ComponentTypeName = itemXpo.ComponentTypeName
                End With
                listInventoryRequestDetailOther.Add(InventoryRequestDetailOther)
            Next
        End If
    End Sub

#End Region

End Class