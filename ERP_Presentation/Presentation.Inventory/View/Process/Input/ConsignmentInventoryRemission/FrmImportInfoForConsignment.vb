'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Miguel Angel Fonseca
' Created          : 2017-12-12
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Base.Extentions
Imports Presentation.Inventory.MVP
Imports System.Text
Imports Presentation.Controls
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports DevExpress.Xpo
Imports Presentation.Controls.MVP

#End Region

Public Class FrmImportInfoForConsignment

#Region "GLOBALS"
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Inventory"

    ''' <summary>
    ''' id de la linea de distribucion del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim _supplierDistributionLineId As Integer

    ''' <summary>
    ''' id del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim _supplierId As Integer

    ''' <summary>
    ''' id del almacen
    ''' </summary>
    ''' <remarks></remarks>
    Private _warehouseId As Int32

    ''' <summary>
    ''' tipo de movimiento
    ''' </summary>
    ''' <remarks></remarks>
    Private _movementType As Byte

    ''' <summary>
    ''' código del usuario 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _codeUser As String

    ''' <summary>
    ''' listado del detalla de la remision para validar que los productos no se pepitan con la misma fuente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listConsignmentInventoryRemissionDetailValidation As List(Of ConsignmentInventoryRemissionDetail)

    ''' <summary>
    ''' listado de la orden de compra
    ''' </summary>
    ''' <remarks></remarks>
    Dim listPurchaseOrderDetail As New List(Of PurchaseOrderDetail)
    ''' <summary>
    ''' listado de los contratos
    ''' </summary>
    ''' <remarks></remarks>
    Dim listInventoryContractDetail As New List(Of InventoryContractDetail)
    ''' <summary>
    ''' listado de inventario ya usado y sin reposicion
    ''' </summary>
    ''' <remarks></remarks>
    Dim listInventoryConsignmentInventoryRemissionDetail As New List(Of ConsignmentInventoryRemissionDetail)

    ''' <summary>
    ''' Id de la moneda desde el formulario que llama a modal de importacion
    ''' </summary>
    Private _currencyId As Integer?
#End Region

#Region "PROPERTIES"
    ''' <summary>
    ''' Propiedad para asignar el Id de la linea de distribucion del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property SupplierDistributionLineId As Integer
        Set(value As Integer)
            _supplierDistributionLineId = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para asignar el id del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property SupplierId As Integer
        Set(value As Integer)
            _supplierId = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para asignar el id del almacen
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property WarehouseId As Integer
        Set(value As Integer)
            _warehouseId = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para asignar el tipo de movimiento de inventario
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property MovementType As Integer
        Set(value As Integer)
            _movementType = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para asignar el código del usuario
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property codeUser As String
        Set(value As String)
            _codeUser = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para para pasar el listado del detalla de la remision
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ListConsignmentInventoryRemissionDetailValidation As List(Of ConsignmentInventoryRemissionDetail)
        Set(value As List(Of ConsignmentInventoryRemissionDetail))
            If value IsNot Nothing Then
                _listConsignmentInventoryRemissionDetailValidation = New List(Of ConsignmentInventoryRemissionDetail)(value.ToArray())
            End If
        End Set
    End Property

    ''' <summary>
    ''' propiedad publica de la moneda desde donde se llama el modal de importacion
    ''' </summary>
    Public WriteOnly Property CurrencyId As Integer?
        Set(value As Integer?)
            _currencyId = value
        End Set
    End Property

#Region "TUPLE"
    Dim _listRemissionSource As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListRemissionSource As List(Of Tuple(Of Byte, String))
        Get
            If _listRemissionSource Is Nothing Then
                _listRemissionSource = New List(Of Tuple(Of Byte, String))
                _listRemissionSource.Add(New Tuple(Of Byte, String)(2, ResourceManager.GetString("PurchaseOrder", MODULE_NAME)))
                _listRemissionSource.Add(New Tuple(Of Byte, String)(3, ResourceManager.GetString("Contract", MODULE_NAME)))
            End If
            Return _listRemissionSource
        End Get
    End Property
#End Region
#End Region

#Region "EVENTS"
    ''' <summary>
    ''' evento para obtener el listado del detalle de la remision de entrada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event GetListConsignmentInventoryRemissionDetail(sender As Object, e As GetListConsignmentInventoryRemissionDetailEventArgs)
#End Region

#Region "HANDLES"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _supplierDistributionLineId = Nothing
        _supplierId = Nothing
        _warehouseId = Nothing
        _movementType = Nothing
        _codeUser = Nothing
        _listConsignmentInventoryRemissionDetailValidation = Nothing
        listPurchaseOrderDetail = Nothing
        listInventoryContractDetail = Nothing
        listInventoryConsignmentInventoryRemissionDetail = Nothing
    End Sub

    ''' <summary>
    ''' load del del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmImportInfoForConsignment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _movementType = 3 Then
            _listRemissionSource = New List(Of Tuple(Of Byte, String))
            _listRemissionSource.Add(New Tuple(Of Byte, String)(4, ResourceManager.GetString("ConsignmentInventoryRemission", MODULE_NAME)))
        End If

        INDGleRemissionSource.Properties.DataSource = ListRemissionSource
        INDGleRemissionSource.Focus()
        IndigoGridControl1.RefreshGrid(INDGcImportInfo)
    End Sub
#End Region

#Region "EditValueChanged"
    Private Sub INDGleRemissionSource_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleRemissionSource.EditValueChanged
        Me.INDColOutputDoc.Visible = False
        If INDGleRemissionSource.EditValue IsNot Nothing Then
            Dim warehouseConsignment = True
            If INDGleRemissionSource.EditValue = 2 Then
                Using model As New MConsignmentInventoryRemission(Me.Tag)
                    '************Cargo los datos de orden de compra, sólo almacenes en consignación*************'
                    Dim listPurchaseOrderDetailXpo As XPCollection = model.ListPurchaseOrderDetailByWarehouseConsignment(_supplierId, _supplierDistributionLineId, _warehouseId, warehouseConsignment)
                    LoadListPurcharseOrder(listPurchaseOrderDetailXpo)
                    INDGcImportInfo.DataSource = Nothing
                    INDGcImportInfo.DataSource = listPurchaseOrderDetail
                    INDGcImportInfo.RefreshDataSource()
                    SetImageActivateColumn(EConsignmentInventoryRemissionSource.Purcharse)
                End Using
            ElseIf INDGleRemissionSource.EditValue = 3 Then
                Using model As New MBusqueda
                    '************Cargo los datos de contrato*************'
                    Dim filter() As Object = {_supplierId, _supplierDistributionLineId}
                    Dim listInventoryContractDetailXpo As XPCollection = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListInventoryContractDetailBySupplierIdAndSupplierDistributionLineId, filter)
                    LoadListContract(listInventoryContractDetailXpo)
                    INDGcImportInfo.DataSource = Nothing
                    INDGcImportInfo.DataSource = listInventoryContractDetail
                    INDGcImportInfo.RefreshDataSource()
                    SetImageActivateColumn(EConsignmentInventoryRemissionSource.Contract)
                End Using
            ElseIf INDGleRemissionSource.EditValue = 4 Then
                Using model As New MConsignmentInventoryRemission(Me.Tag)
                    '************Cargo los datos de las remisiones de inventario en consignación usadas y que no han sido reemplazadas*************'
                    Dim filter() As Object = {_supplierId, _supplierDistributionLineId}
                    Dim listConsignmentInventoryRemissionDetailXpo As XPCollection = model.ListAllConsignmentInventoryRemissionWithPendingQuantityReplacement(_supplierId, _warehouseId)
                    LoadListConsignmentInventoryRemission(listConsignmentInventoryRemissionDetailXpo)
                    INDGcImportInfo.DataSource = Nothing
                    INDGcImportInfo.DataSource = listInventoryConsignmentInventoryRemissionDetail
                    INDGcImportInfo.RefreshDataSource()
                    SetImageActivateColumn(EConsignmentInventoryRemissionSource.ConsignmentInventoyRemission)
                End Using
            End If
        End If
    End Sub

    Private Sub INDRptCheActivate_EditValueChanged(sender As Object, e As EventArgs) Handles INDRptCheActivate.EditValueChanged
        Dim checkControl = DirectCast(sender, DevExpress.XtraEditors.CheckEdit)
        If INDGleRemissionSource.EditValue = 2 Then
            Dim purcharse = DirectCast(INDGvImportInfo.GetFocusedRow(), PurchaseOrderDetail)
            purcharse.Activated = checkControl.EditValue
            SetImageActivateColumn(EConsignmentInventoryRemissionSource.Purcharse)
        ElseIf INDGleRemissionSource.EditValue = 3 Then
            Dim contract = DirectCast(INDGvImportInfo.GetFocusedRow(), InventoryContractDetail)
            contract.Activated = checkControl.EditValue
            SetImageActivateColumn(EConsignmentInventoryRemissionSource.Contract)
        ElseIf INDGleRemissionSource.EditValue = 4 Then
            Dim consignmentInventoryRemissionDetailBatchSerial = DirectCast(INDGvImportInfo.GetFocusedRow(), ConsignmentInventoryRemissionDetail)
            consignmentInventoryRemissionDetailBatchSerial.Activated = checkControl.EditValue
            SetImageActivateColumn(EConsignmentInventoryRemissionSource.ConsignmentInventoyRemission)
        End If
        Me.INDGcImportInfo.RefreshDataSource()
        Me.INDGcImportInfo.Invalidate()
    End Sub
#End Region

#Region "KeyDown"
    Private Sub FrmImportInfoForConsignment_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub
#End Region

#Region "Click"
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        Dim errors = ValidateRecords()
        If errors.Count > 0 Then
            Using formulario As New FrmListErrors(errors)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
            Exit Sub
        End If
        Dim args As New GetListConsignmentInventoryRemissionDetailEventArgs
        args.ListConsignmentInventoryRemissionDetail = GenerateConsignmentInventoryRemissionDetail()
        If args.ListConsignmentInventoryRemissionDetail.Count > 0 Then
            Me.Close()
            RaiseEvent GetListConsignmentInventoryRemissionDetail(Nothing, args)
        End If
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
            If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDGclState") Then
                If INDGleRemissionSource.EditValue = 2 Then
                    If INDGvImportInfo.DataController.GetAllFilteredAndSortedRows().ToEntityList(Of PurchaseOrderDetail)().Where(Function(s) s.Activated).ToList().Count = INDGvImportInfo.DataController.GetAllFilteredAndSortedRows().ToEntityList(Of PurchaseOrderDetail).Count Then
                        INDGvImportInfo.DataController.GetAllFilteredAndSortedRows().ToEntityList(Of PurchaseOrderDetail).ForEach(Sub(x) x.Activated = False)
                        Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
                    Else
                        INDGvImportInfo.DataController.GetAllFilteredAndSortedRows().ToEntityList(Of PurchaseOrderDetail).ForEach(Sub(x) x.Activated = True)
                        Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.check
                    End If
                ElseIf INDGleRemissionSource.EditValue = 3 Then
                    If INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of InventoryContractDetail).Where(Function(s) s.Activated).ToList().Count = INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of InventoryContractDetail).Count Then
                        INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of InventoryContractDetail).ForEach(Sub(x) x.Activated = False)
                        Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
                    Else
                        INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of InventoryContractDetail).ForEach(Sub(x) x.Activated = True)
                        Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.check
                    End If
                ElseIf INDGleRemissionSource.EditValue = 4 Then
                    If INDGvImportInfo.DataController.GetAllFilteredAndSortedRows().ToEntityList(Of ConsignmentInventoryRemissionDetail)().Where(Function(s) s.Activated).ToList().Count = INDGvImportInfo.DataController.GetAllFilteredAndSortedRows().ToEntityList(Of ConsignmentInventoryRemissionDetail).Count Then
                        INDGvImportInfo.DataController.GetAllFilteredAndSortedRows().ToEntityList(Of ConsignmentInventoryRemissionDetail).ForEach(Sub(x) x.Activated = False)
                        Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
                    Else
                        INDGvImportInfo.DataController.GetAllFilteredAndSortedRows().ToEntityList(Of ConsignmentInventoryRemissionDetail).ForEach(Sub(x) x.Activated = True)
                        Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.check
                    End If
                End If
                Me.INDGcImportInfo.RefreshDataSource()
                Me.INDGcImportInfo.Invalidate()
            End If
        End If
    End Sub
#End Region
#End Region

#Region "METHODS"
    ''' <summary>
    ''' metodo para establecer la imagen a la columna de activar o inactivar
    ''' </summary>
    ''' <param name="remissionSource"></param>
    ''' <remarks></remarks>
    Private Sub SetImageActivateColumn(remissionSource As EConsignmentInventoryRemissionSource)
        If remissionSource = EConsignmentInventoryRemissionSource.Purcharse Then
            Dim listActivated = listPurchaseOrderDetail.FindAll(Function(x) x.Activated = True)
            If listActivated.Count = listPurchaseOrderDetail.Count Then
                Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.check
            Else
                Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
            End If
        ElseIf remissionSource = EConsignmentInventoryRemissionSource.Contract Then
            Dim listActivated = listInventoryContractDetail.FindAll(Function(x) x.Activated = True)
            If listActivated.Count = listInventoryContractDetail.Count Then
                Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.check
            Else
                Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
            End If
        ElseIf remissionSource = EConsignmentInventoryRemissionSource.ConsignmentInventoyRemission Then
            Dim listActivated = listInventoryConsignmentInventoryRemissionDetail.FindAll(Function(x) x.Activated = True)
            If listActivated.Count = listInventoryConsignmentInventoryRemissionDetail.Count Then
                Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.check
            Else
                Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
            End If
        End If
    End Sub

    ''' <summary>
    ''' metodo para generar los detalles de la remision
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateConsignmentInventoryRemissionDetail() As List(Of ConsignmentInventoryRemissionDetail)
        Dim listConsignmentInventoryRemissionDetailTmp As New List(Of ConsignmentInventoryRemissionDetail)
        Dim remissionEntranceDetailTmp As ConsignmentInventoryRemissionDetail
        'genero los detalles por orden de compra
        For Each item In listPurchaseOrderDetail.FindAll(Function(x) x.Activated = True And x.ItemInvalid = False)
            remissionEntranceDetailTmp = New ConsignmentInventoryRemissionDetail
            With remissionEntranceDetailTmp
                .SourceCode = item.Code
                .RemissionSource = 2
                .PurchaseOrderDetailId = item.Id
                .ProductId = item.ProductId
                .QuantityImport = item.OutstandingQuantity
                .Quantity = item.OutstandingQuantity
                .UnitValue = item.Value
            End With
            listConsignmentInventoryRemissionDetailTmp.Add(remissionEntranceDetailTmp)
        Next
        'genero los detalles por contratos
        For Each item In listInventoryContractDetail.FindAll(Function(x) x.Activated = True And x.ItemInvalid = False)
            remissionEntranceDetailTmp = New ConsignmentInventoryRemissionDetail
            With remissionEntranceDetailTmp
                .SourceCode = item.Code
                .RemissionSource = 3
                .ContractDetailId = item.Id
                .ProductId = item.ProductId
                .QuantityImport = item.OutstandingQuantity
                .Quantity = item.OutstandingQuantity
                .UnitValue = item.Value
            End With
            listConsignmentInventoryRemissionDetailTmp.Add(remissionEntranceDetailTmp)
        Next
        'genero los detalles por las remisiones de inventario en consignacion
        For Each item In listInventoryConsignmentInventoryRemissionDetail.FindAll(Function(x) x.Activated = True And x.ItemInvalid = False)
            remissionEntranceDetailTmp = New ConsignmentInventoryRemissionDetail
            With remissionEntranceDetailTmp
                .SourceCode = item.Code
                .RemissionSource = 4
                .ConsignmentInventoryRemissionDetailId = item.ConsignmentInventoryRemissionDetailId
                .ProductId = item.ProductId
                .QuantityImport = item.OutstandingQuantity
                .Quantity = item.OutstandingQuantity
                .UnitValue = item.UnitValue
                .ConsignmentInventoryRemissionDetailBatchSerialId = item.ConsignmentInventoryRemissionDetailBatchSerialId
            End With
            listConsignmentInventoryRemissionDetailTmp.Add(remissionEntranceDetailTmp)
        Next
        Return listConsignmentInventoryRemissionDetailTmp
    End Function

    ''' <summary>
    ''' valida los datos para poder importarlos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateRecords() As List(Of String)
        Dim errors As New List(Of String)
        Dim listInventoryContractDetailActivated = listInventoryContractDetail.FindAll(Function(x) x.Activated = True)
        Dim listPurchaseOrderDetailActivated = listPurchaseOrderDetail.FindAll(Function(x) x.Activated = True)
        Dim listInventoryConsignmentInventoryRemissionDetailActivated = listInventoryConsignmentInventoryRemissionDetail.FindAll(Function(x) x.Activated = True)
        If (listInventoryContractDetailActivated.Count = 0 AndAlso listPurchaseOrderDetailActivated.Count = 0 AndAlso listInventoryConsignmentInventoryRemissionDetailActivated.Count = 0) Then
            errors.Add(ResourceManager.GetString("NoSelectedItem", MODULE_NAME))
        End If
        If Not errors?.Any() Then

            'valido que todos los elementos seleccionado tenga la misma moneda de la cabecera de comprobante de entrada
            Select Case INDGleRemissionSource.EditValue
                Case 2, 3, 4
                    If Me._currencyId Is Nothing OrElse ((listPurchaseOrderDetailActivated?.Any() AndAlso Not listPurchaseOrderDetailActivated?.All(Function(x) x.CurrencyId = Me._currencyId)) _
                        OrElse (listInventoryContractDetailActivated?.Any() AndAlso Not listInventoryContractDetailActivated?.All(Function(x) x.CurrencyId = Me._currencyId)) _
                        OrElse (listInventoryConsignmentInventoryRemissionDetailActivated?.Any() AndAlso Not listInventoryConsignmentInventoryRemissionDetailActivated?.All(Function(x) x.CurrencyId = Me._currencyId))) Then
                        errors.Add(ResourceManager.GetString("CurrencyDocumentDifferent"))
                        listPurchaseOrderDetail.ForEach(Sub(x)
                                                            x.Activated = False
                                                        End Sub)
                        listInventoryContractDetail.ForEach(Sub(x)
                                                                x.Activated = False
                                                            End Sub)
                        INDGcImportInfo.RefreshDataSource()
                        Return errors
                    End If
            End Select

            If _listConsignmentInventoryRemissionDetailValidation IsNot Nothing AndAlso _listConsignmentInventoryRemissionDetailValidation.Count > 0 Then
                'valido que el producto no este con la misma orden de compra solo en los items que estan seleccionados
                For Each item In listPurchaseOrderDetailActivated
                    Dim remissionEntranceDetailTmp = _listConsignmentInventoryRemissionDetailValidation.Find(Function(x) x.ProductId = item.ProductId And x.RemissionSource = 2 And x.SourceCode = item.Code)
                    If remissionEntranceDetailTmp IsNot Nothing Then
                        errors.Add(String.Format(ResourceManager.GetString("ProductAdded", MODULE_NAME), remissionEntranceDetailTmp.CodeNameProduct, ResourceManager.GetString("PurchaseOrder", MODULE_NAME)))
                        item.ItemInvalid = True
                    End If
                Next
                'valido que el producto no este con el mismo contrato
                For Each item In listInventoryContractDetailActivated
                    Dim remissionEntranceDetailTmp = _listConsignmentInventoryRemissionDetailValidation.Find(Function(x) x.ProductId = item.ProductId And x.RemissionSource = 3 And x.SourceCode = item.Code)
                    If remissionEntranceDetailTmp IsNot Nothing Then
                        errors.Add(String.Format(ResourceManager.GetString("ProductAdded", MODULE_NAME), remissionEntranceDetailTmp.CodeNameProduct, ResourceManager.GetString("Contract", MODULE_NAME)))
                        item.ItemInvalid = True
                    End If
                Next
                'valido que el producto no este con la misma fuente de remision de inventario en consignación, solo en los items que estan seleccionados
                For Each item In listInventoryConsignmentInventoryRemissionDetailActivated
                    Dim remissionEntranceDetailTmp = _listConsignmentInventoryRemissionDetailValidation.Find(Function(x) x.ProductId = item.ProductId And x.RemissionSource = 4 And x.SourceCode = item.Code)
                    If remissionEntranceDetailTmp IsNot Nothing Then
                        errors.Add(String.Format(ResourceManager.GetString("ProductAdded", MODULE_NAME), remissionEntranceDetailTmp.CodeNameProduct, ResourceManager.GetString("ConsignmentInventoryRemission", MODULE_NAME)))
                        item.ItemInvalid = True
                    End If
                Next
            End If
        End If
        Return errors
    End Function

    ''' <summary>
    ''' Metodo que carga el listado de detalles de contrato
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadListContract(listXpo As XPCollection)
        listInventoryContractDetail = New List(Of Domain.Entities.InventoryContractDetail)
        If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
            For Each itemXpo As InventoryContractDetailXpo In listXpo
                Dim inventoryContractDetail As New Domain.Entities.InventoryContractDetail
                With inventoryContractDetail
                    .Id = itemXpo.Id
                    .ProductId = itemXpo.ProductId.Id
                    .InventoryContractId = itemXpo.InventoryContractId.Id
                    .Quantity = itemXpo.Quantity
                    .OutstandingQuantity = itemXpo.OutstandingQuantity
                    .CancelledQuantity = itemXpo.CancelledQuantity
                    .Value = itemXpo.Value
                    .SubTotalValue = itemXpo.SubTotalValue
                    .IvaPercentage = itemXpo.IvaPercentage
                    .IvaValue = itemXpo.IvaValue
                    .DiscountPercentage = itemXpo.DiscountPercentage
                    .DiscountValue = itemXpo.DiscountValue
                    .TotalValue = itemXpo.TotalValue
                    .CodeNameProduct = itemXpo.ProductId.Code + " - " + itemXpo.ProductId.Name
                    .Code = itemXpo.InventoryContractId.Code
                    .Activated = False
                    .ItemInvalid = False
                    .DocumentDate = itemXpo.InventoryContractId.InitialDate
                    .CurrencyAbbreviation = itemXpo.CurrencyAbbreviation
                    .CurrencyId = itemXpo?.InventoryContractId?.CurrencyId
                End With
                listInventoryContractDetail.Add(inventoryContractDetail)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Metodo que carga el listado de detalles de contrato
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadListPurcharseOrder(listXpo As XPCollection)
        listPurchaseOrderDetail = New List(Of Domain.Entities.PurchaseOrderDetail)
        If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
            For Each itemXpo As InventoryPurchaseOrderDetailReportXpo In listXpo
                Dim purchaseOrderDetail As New Domain.Entities.PurchaseOrderDetail
                With purchaseOrderDetail
                    .Id = itemXpo.Id
                    .PurchaseOrderId = itemXpo.PurchaseOrderId.Id
                    .ProductId = itemXpo.ProductId.Id
                    .Quantity = itemXpo.Quantity
                    .OutstandingQuantity = itemXpo.OutstandingQuantity
                    .CancelledQuantity = itemXpo.CancelledQuantity
                    .Value = itemXpo.Value
                    .SubTotalValue = itemXpo.SubTotalValue
                    .IvaPercentage = itemXpo.IvaPercentage
                    .IvaValue = itemXpo.IvaValue
                    .DiscountPercentage = itemXpo.DiscountPercentage
                    .DiscountValue = itemXpo.DiscountValue
                    .TotalValue = itemXpo.TotalValue
                    .CodeNameProduct = itemXpo.ProductId.Code + " - " + itemXpo.ProductId.Name
                    .Code = itemXpo.PurchaseOrderId.Code
                    .Activated = False
                    .ItemInvalid = False
                    .DocumentDate = itemXpo.PurchaseOrderId.DocumentDate
                    .CurrencyAbbreviation = itemXpo.CurrencyAbbreviation
                    .CurrencyId = itemXpo?.PurchaseOrderId?.CurrencyId
                End With
                listPurchaseOrderDetail.Add(purchaseOrderDetail)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Metodo que carga el listado de detalles de remisiones de inventario en consignación
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadListConsignmentInventoryRemission(listXpo As XPCollection)
        listInventoryConsignmentInventoryRemissionDetail = New List(Of Domain.Entities.ConsignmentInventoryRemissionDetail)
        If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
            For Each itemXpo As ConsignmentInventoryRemissionDetailBatchSerialXpo In listXpo
                Dim consignmentInventoryRemissionDetail As New Domain.Entities.ConsignmentInventoryRemissionDetail
                With consignmentInventoryRemissionDetail
                    .Id = itemXpo.ConsignmentInventoryRemissionDetailId.Id
                    .ConsignmentInventoryRemissionDetailId = itemXpo.ConsignmentInventoryRemissionDetailId.Id
                    .ConsignmentInventoryRemissionDetailBatchSerialId = itemXpo.Id
                    .ConsignmentInventoryRemissionId = itemXpo.ConsignmentInventoryRemissionDetailId.ConsignmentInventoryRemissionId.Id
                    .ProductId = itemXpo.ConsignmentInventoryRemissionDetailId.ProductId.Id
                    .Quantity = itemXpo.Quantity
                    .OutstandingQuantity = itemXpo.PendingQuantityReplacement
                    .UnitValue = itemXpo.ConsignmentInventoryRemissionDetailId.UnitValue
                    .SubTotalValue = itemXpo.ConsignmentInventoryRemissionDetailId.SubTotalValue
                    .IvaPercentage = itemXpo.ConsignmentInventoryRemissionDetailId.IvaPercentage
                    .IvaValue = itemXpo.ConsignmentInventoryRemissionDetailId.IvaValue
                    .TotalValue = itemXpo.ConsignmentInventoryRemissionDetailId.TotalValue
                    .CodeNameProduct = itemXpo.ConsignmentInventoryRemissionDetailId.ProductId.Code + " - " + itemXpo.ConsignmentInventoryRemissionDetailId.ProductId.Name
                    .Code = itemXpo.ConsignmentInventoryRemissionDetailId.ConsignmentInventoryRemissionId.Code
                    .DocumentDate = itemXpo.ConsignmentInventoryRemissionDetailId.ConsignmentInventoryRemissionId.RemissionDate
                    .Activated = False
                    .ItemInvalid = False
                    .OutstandingLegalizedQuantity = itemXpo.OutstandingLegalizedQuantity
                    .BatchSerialId = If(itemXpo?.BatchSerialId?.Id Is Nothing OrElse itemXpo?.BatchSerialId?.Id = 0, Nothing, itemXpo?.BatchSerialId?.Id)
                    .CurrencyAbbreviation = itemXpo.CurrencyAbbreviation
                    .CurrencyId = itemXpo.CurrencyId
                End With
                listInventoryConsignmentInventoryRemissionDetail.Add(consignmentInventoryRemissionDetail)
            Next
            Me.INDColOutputDoc.Visible = True
        End If
    End Sub

    ''' <summary>
    ''' evento para consultar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDRIPCEInfo_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDRIPCEInfo.QueryPopUp
        Try
            INDgcDetailControl.DataSource = Nothing
            Dim _row = TryCast(INDGvImportInfo.GetFocusedRow(), Domain.Entities.ConsignmentInventoryRemissionDetail)
            If _row Is Nothing Then
                Exit Sub
            End If
            Using model As New MConsignmentInventoryRemission(Me.Tag)
                INDviewDetailControl.LoadingPanelVisible = True
                Dim _obj = Await Task.Factory.StartNew(Function() As List(Of ConsignmentInventoryRemissionDetailControlXpo)
                                                           Return model.ListConsignmentInventoryRemissionDetailControlXpo(_row.Id, _row.BatchSerialId).ToList()
                                                       End Function)
                INDviewDetailControl.LoadingPanelVisible = False
                If _obj?.Select(Function(x) x.QuantityPendingLegalization)?.Sum() = _row.OutstandingLegalizedQuantity Then
                    INDgcDetailControl.DataSource = _obj
                End If
                INDgcDetailControl.RefreshDataSource()
            End Using
        Catch ex As Exception
        End Try
    End Sub
#End Region

End Class

Enum EConsignmentInventoryRemissionSource As Integer
    Purcharse = 2
    Contract = 3
    ConsignmentInventoyRemission = 4
End Enum