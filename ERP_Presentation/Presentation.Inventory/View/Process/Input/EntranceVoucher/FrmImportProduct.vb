'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Ernesto Córdoba
' Created          : 08-01-2015
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
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo.InventoryRepository

#End Region

Public Class FrmImportProduct

#Region "EVENTS"
    ''' <summary>
    ''' evento para obtener el listado del detalle de la remision de entrada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event GetListEntranceVoucherDetail(sender As Object, e As GetListEntranceVoucherDetailEventArgs)
#End Region

#Region "TUPLE"
    Dim _listEntranceSource As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListEntranceSource As List(Of Tuple(Of Byte, String))
        Get
            If _listEntranceSource Is Nothing Then
                _listEntranceSource = New List(Of Tuple(Of Byte, String))
                _listEntranceSource.Add(New Tuple(Of Byte, String)(2, ResourceManager.GetString("PurchaseOrder", MODULE_NAME)))
                _listEntranceSource.Add(New Tuple(Of Byte, String)(3, ResourceManager.GetString("Contract", MODULE_NAME)))
                _listEntranceSource.Add(New Tuple(Of Byte, String)(4, ResourceManager.GetString("EntranceRemission", MODULE_NAME)))
                _listEntranceSource.Add(New Tuple(Of Byte, String)(5, ResourceManager.GetString("ConsignmentInventoryRemission", MODULE_NAME)))
            End If
            Return _listEntranceSource
        End Get
    End Property
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
    Dim listPurchaseOrderDetail As New List(Of PurchaseOrderDetail)
    ''' <summary>
    ''' listado de los contratos
    ''' </summary>
    ''' <remarks></remarks>
    Dim listInventoryContractDetail As New List(Of InventoryContractDetail)
    ''' <summary>
    ''' listado de las remisiones
    ''' </summary>
    ''' <remarks></remarks>
    Dim listRemissionEntranceDetail As New List(Of RemissionEntranceDetailBatchSerial)
    ''' <summary>
    ''' listado de las remisiones
    ''' </summary>
    ''' <remarks></remarks>
    Dim listConsignmentInventoryRemissionDetail As New List(Of ConsignmentInventoryRemissionDetailBatchSerial)
    ''' <summary>
    ''' listado del detalla de la remision para validar que los productos no se pepitan con la misma fuente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listEntranceVoucherDetailValidation As List(Of EntranceVoucherDetail)
    ''' <summary>
    ''' id del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim _supplierId As Integer
    ''' <summary>
    ''' id de la linea de distribucion del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim _supplierDistributionLineId As Integer
    ''' <summary>
    ''' código del usuario 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _codeUser As String

    Private _warehouseId As Int32

    ''' <summary>
    ''' Id de la moneda desde el formulario que llama a modal de importacion
    ''' </summary>
    Private _currencyId As Integer?
#End Region

#Region "PROPERTIES"
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
    Public WriteOnly Property ListEntranceVoucherDetailValidation As List(Of EntranceVoucherDetail)
        Set(value As List(Of EntranceVoucherDetail))
            If value IsNot Nothing Then
                _listEntranceVoucherDetailValidation = New List(Of EntranceVoucherDetail)(value.ToArray())
            End If
        End Set
    End Property

    Public WriteOnly Property WarehouseId As Integer
        Set(value As Integer)
            _warehouseId = value
        End Set
    End Property
#End Region

#Region "HANDLES"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        listPurchaseOrderDetail = Nothing
        listInventoryContractDetail = Nothing
        listRemissionEntranceDetail = Nothing
        listConsignmentInventoryRemissionDetail = Nothing
        _listEntranceVoucherDetailValidation = Nothing
        _supplierId = Nothing
        _supplierDistributionLineId = Nothing
        _codeUser = Nothing
    End Sub

    ''' <summary>
    ''' load del del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmImportProducts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDGleEntranceSource.Properties.DataSource = ListEntranceSource
        INDGleEntranceSource.Focus()
        IndigoGridControl1.RefreshGrid(INDGcImportInfo)
    End Sub

    ''' <summary>
    ''' propiedad publica de la moneda desde donde se llama el modal de importacion
    ''' </summary>
    Public WriteOnly Property CurrencyId As Integer?
        Set(value As Integer?)
            _currencyId = value
        End Set
    End Property

#End Region

#Region "EditValueChanged"
    Private Sub INDGleRemissionSource_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleEntranceSource.EditValueChanged
        If INDGleEntranceSource.EditValue IsNot Nothing Then
            If INDGleEntranceSource.EditValue = 2 Then
                Using model As New MReferralEntry(Me.Tag)
                    '************Cargo los datos de orden de compra*************'
                    Dim filter() As Object = {_supplierId, _supplierDistributionLineId, _codeUser}
                    Dim listPurchaseOrderDetailXpo As XPCollection = model.ListPurchaseOrderDetailByWarehouseConsignment(_supplierId, _supplierDistributionLineId, _warehouseId, False)
                    LoadListPurcharseOrder(listPurchaseOrderDetailXpo)
                    INDGcImportInfo.DataSource = Nothing
                    INDGcImportInfo.DataSource = listPurchaseOrderDetail
                    INDGcImportInfo.RefreshDataSource()
                End Using
            ElseIf INDGleEntranceSource.EditValue = 3 Then
                Using model As New MBusqueda
                    '************Cargo los datos de contrato*************'
                    Dim filter() As Object = {_supplierId, _supplierDistributionLineId}
                    Dim listInventoryContractDetailXpo As XPCollection = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListInventoryContractDetailBySupplierIdAndSupplierDistributionLineId, filter)
                    LoadListContract(listInventoryContractDetailXpo)
                    INDGcImportInfo.DataSource = Nothing
                    INDGcImportInfo.DataSource = listInventoryContractDetail
                    INDGcImportInfo.RefreshDataSource()
                End Using
            ElseIf INDGleEntranceSource.EditValue = 4 Then
                Using model As New MEntranceVoucher(Me.Tag)
                    '************Cargo los datos de remision de entrada*************'
                    Dim filter() As Object = {_supplierId, _supplierDistributionLineId, _codeUser}
                    Dim listRemissionEntranceDetailXpo As XPCollection = model.ListPurchaseRemissionEntrance(_supplierId, _supplierDistributionLineId, _warehouseId)
                    LoadListRemisionEntrance(listRemissionEntranceDetailXpo)
                    INDGcImportInfo.DataSource = Nothing
                    INDGcImportInfo.DataSource = listRemissionEntranceDetail
                    INDGcImportInfo.RefreshDataSource()
                End Using
            ElseIf INDGleEntranceSource.EditValue = 5 Then
                Using model As New MEntranceVoucher(Me.Tag)
                    '************Cargo los datos de remision de inventario en consignación*************'
                    Dim listConsignmentInventoryRemissionDetailXpo As XPCollection = model.ListConsignmentInventoryRemissionWithoutLegalize(_supplierId, _supplierDistributionLineId, _warehouseId)
                    LoadListConsignmentInventoryRemission(listConsignmentInventoryRemissionDetailXpo)
                    INDGcImportInfo.DataSource = Nothing
                    INDGcImportInfo.DataSource = listConsignmentInventoryRemissionDetail
                    INDGcImportInfo.RefreshDataSource()
                End Using
            End If
        End If
    End Sub
#End Region

#Region "KeyDown"
    Private Sub FrmImportInfo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
        End If
        Dim args As New GetListEntranceVoucherDetailEventArgs
        args.ListEntranceVoucherDetail = GenerateEntranceVoucherDetail()
        args.EntranceSource = INDGleEntranceSource.EditValue
        If args.ListEntranceVoucherDetail.Count > 0 Then
            Me.Close()
            RaiseEvent GetListEntranceVoucherDetail(Nothing, args)
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
                If INDGleEntranceSource.EditValue = 2 Then

                    If INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of PurchaseOrderDetail).Where(Function(s) s.Activated).ToList().Count = INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of PurchaseOrderDetail).Count Then
                        INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of PurchaseOrderDetail).ForEach(Sub(x) x.Activated = False)
                    Else
                        INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of PurchaseOrderDetail).ForEach(Sub(x) x.Activated = True)
                    End If

                    Me.INDGcImportInfo.RefreshDataSource()

                ElseIf INDGleEntranceSource.EditValue = 3 Then

                    If INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of InventoryContractDetail).Where(Function(s) s.Activated).ToList().Count = INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of InventoryContractDetail).Count Then
                        INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of InventoryContractDetail).ForEach(Sub(x) x.Activated = False)
                    Else
                        INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of InventoryContractDetail).ForEach(Sub(x) x.Activated = True)
                    End If

                    Me.INDGcImportInfo.RefreshDataSource()

                ElseIf INDGleEntranceSource.EditValue = 4 Then

                    If INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of RemissionEntranceDetailBatchSerial).Where(Function(s) s.Activated).ToList().Count = INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of RemissionEntranceDetailBatchSerial).Count Then
                        INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of RemissionEntranceDetailBatchSerial).ForEach(Sub(x) x.Activated = False)
                    Else
                        INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of RemissionEntranceDetailBatchSerial).ForEach(Sub(x) x.Activated = True)
                    End If

                    Me.INDGcImportInfo.RefreshDataSource()

                Else

                    If INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of ConsignmentInventoryRemissionDetailBatchSerial).Where(Function(s) s.Activated).ToList().Count = INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of ConsignmentInventoryRemissionDetailBatchSerial).Count Then
                        INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of ConsignmentInventoryRemissionDetailBatchSerial).ForEach(Sub(x) x.Activated = False)
                    Else
                        INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of ConsignmentInventoryRemissionDetailBatchSerial).ForEach(Sub(x) x.Activated = True)
                    End If

                    Me.INDGcImportInfo.RefreshDataSource()

                End If

            End If
            Me.INDGcImportInfo.Invalidate()
        End If
    End Sub
#End Region
#End Region

#Region "METHODS"
    ''' <summary>
    ''' metodo para generar los detalles de la remision
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateEntranceVoucherDetail() As List(Of EntranceVoucherDetail)
        Dim listEntranceVoucherDetailTmp As New List(Of EntranceVoucherDetail)
        Dim entranceVoucherDetailTmp As EntranceVoucherDetail
        'genero los detalles por orden de compra
        For Each item In listPurchaseOrderDetail.FindAll(Function(x) x.Activated = True And x.ItemInvalid = False)
            entranceVoucherDetailTmp = New EntranceVoucherDetail
            With entranceVoucherDetailTmp
                .Quantity = item.OutstandingQuantity
                .SourceCode = item.Code
                .EntranceSource = 2
                .PurchaseOrderDetailId = item.Id
                .ProductId = item.ProductId
                .GroupCodeName = item.GroupCodeName
                .UnitValue = item.Value
                .DiscountPercentage = item.DiscountPercentage
                .DeliveredDate = item.DeliveredDate
                .ProductCodeName = item.CodeNameProduct
                .CostWithDescount = item.Value - (item.Value * (item.DiscountPercentage / 100))
            End With
            listEntranceVoucherDetailTmp.Add(entranceVoucherDetailTmp)
        Next
        'genero los detalles por contratos
        For Each item In listInventoryContractDetail.FindAll(Function(x) x.Activated = True And x.ItemInvalid = False)
            entranceVoucherDetailTmp = New EntranceVoucherDetail
            With entranceVoucherDetailTmp
                .Quantity = item.OutstandingQuantity
                .SourceCode = item.Code
                .EntranceSource = 3
                .ContractDetailId = item.Id
                .ProductId = item.ProductId
                .GroupCodeName = item.GroupCodeName
                .ProductCodeName = item.CodeNameProduct
            End With
            listEntranceVoucherDetailTmp.Add(entranceVoucherDetailTmp)
        Next
        'genero los detalles por remision de entrada
        For Each item In listRemissionEntranceDetail.FindAll(Function(x) x.Activated = True And x.ItemInvalid = False)
            entranceVoucherDetailTmp = New EntranceVoucherDetail
            With entranceVoucherDetailTmp
                .RemissionEntranceDetailBatchSerialId = item.Id
                .EntranceSource = 4
                .SourceCode = item.Code
                .GroupCodeName = item.GroupCodeName
                .ProductId = item.ProductId
                .ProductCodeName = item.CodeNameProduct
                .DescriptionBatch = item.CodeBatchSerial
                .Quantity = item.OutstandingQuantity
                .DiscountPercentage = item.DiscountPercentage
                .UnitValue = item.RemissionEntranceDetail.GrossUnitValue
                .CostWithDescount = item.RemissionEntranceDetail.UnitValue
                .EntranceVoucherDetailBatchSerial.Add(New EntranceVoucherDetailBatchSerial With
                {
                    .BatchSerialId = item.BatchSerialId,
                    .CodeBatchSerial = item.CodeBatchSerial,
                    .Quantity = item.OutstandingQuantity,
                    .OutstandingQuantity = item.OutstandingQuantity
                })
            End With
            listEntranceVoucherDetailTmp.Add(entranceVoucherDetailTmp)
        Next
        'genero los detalles por remision de inventario en consignación
        For Each item In listConsignmentInventoryRemissionDetail.FindAll(Function(x) x.Activated = True And x.ItemInvalid = False)
            entranceVoucherDetailTmp = New EntranceVoucherDetail
            With entranceVoucherDetailTmp
                .ConsignmentInventoryRemissionDetailBatchSerialId = item.Id
                .EntranceSource = 5
                .SourceCode = item.Code
                .GroupCodeName = item.GroupCodeName
                .ProductId = item.ProductId
                .ProductCodeName = item.CodeNameProduct
                .DescriptionBatch = item.CodeBatchSerial
                .Quantity = item.OutstandingQuantity
                .UnitValue = item.ConsignmentInventoryRemissionDetail.UnitValue
                .EntranceVoucherDetailBatchSerial.Add(New EntranceVoucherDetailBatchSerial With
                {
                    .BatchSerialId = item.BatchSerialId,
                    .CodeBatchSerial = item.CodeBatchSerial,
                    .Quantity = item.OutstandingQuantity,
                    .OutstandingQuantity = item.OutstandingQuantity
                })
            End With
            listEntranceVoucherDetailTmp.Add(entranceVoucherDetailTmp)
        Next
        Return listEntranceVoucherDetailTmp
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
        Dim listRemissionEntraceDetailActivated = listRemissionEntranceDetail.FindAll(Function(x) x.Activated = True)
        Dim listConsignmentInventoryRemissionDetailActivated = listConsignmentInventoryRemissionDetail.FindAll(Function(x) x.Activated = True)
        Dim ProductForValidation As InventoryProduct = Nothing

        If Not (listInventoryContractDetailActivated?.Any() OrElse listPurchaseOrderDetailActivated?.Any() OrElse listRemissionEntraceDetailActivated?.Any() OrElse listConsignmentInventoryRemissionDetailActivated?.Any()) Then
            errors.Add(ResourceManager.GetString("NoSelectedItem", MODULE_NAME))
        End If

        If Not errors?.Any() Then
            'valido que todos los elementos seleccionado tenga la misma moneda de la cabecera de comprobante de entrada

            If Me._currencyId Is Nothing OrElse
             ((listPurchaseOrderDetailActivated?.Any() AndAlso Not listPurchaseOrderDetailActivated?.All(Function(x) x.CurrencyId = Me._currencyId)) OrElse
             (listInventoryContractDetailActivated?.Any() AndAlso Not listInventoryContractDetailActivated?.All(Function(x) x.CurrencyId = Me._currencyId)) OrElse
             (listConsignmentInventoryRemissionDetailActivated?.Any() AndAlso Not listConsignmentInventoryRemissionDetailActivated?.All(Function(x) x.CurrencyId = Me._currencyId)) OrElse
             (listRemissionEntraceDetailActivated?.Any() AndAlso Not listRemissionEntraceDetailActivated?.All(Function(x) x.CurrencyId = Me._currencyId))) Then

                errors.Add(ResourceManager.GetString("CurrencyDocumentDifferent"))

                listPurchaseOrderDetail.ForEach(Sub(x)
                                                    x.Activated = False
                                                End Sub)

                listInventoryContractDetail.ForEach(Sub(x)
                                                        x.Activated = False
                                                    End Sub)

                listConsignmentInventoryRemissionDetail.ForEach(Sub(x)
                                                                    x.Activated = False
                                                                End Sub)
                listRemissionEntranceDetail.ForEach(Sub(x)
                                                        x.Activated = False
                                                    End Sub)
                INDGcImportInfo.RefreshDataSource()

                Return errors

            End If

            'valido que el producto no este con la misma orden de compra solo en los items que estan seleccionados
            Dim _listEntranceVoucherDetailValidationTmp = _listEntranceVoucherDetailValidation.Where(Function(x) x.PurchaseOrderDetailId IsNot Nothing).ToList()
            For Each item In listPurchaseOrderDetailActivated

                Using model As New MInventoryProduct(Me.Tag)
                    ProductForValidation = model.GetInventoryProductByIdSimpleToGroup(item.ProductId)
                End Using
                If Not ProductForValidation.Status Then
                    errors.Add(String.Format("El siguiente producto se encuentra inactivo: {0}", ProductForValidation.Code))
                    item.ItemInvalid = True
                End If
                'Dim entranceVoucherDetailTmp = _listEntranceVoucherDetailValidation.Find(Function(x) x.ProductId = item.ProductId And x.EntranceSource = 2)
                Dim entranceVoucherDetailTmp = _listEntranceVoucherDetailValidationTmp.Find(Function(x) x.PurchaseOrderDetailId = item.Id)
                If entranceVoucherDetailTmp IsNot Nothing Then
                    errors.Add(String.Format(ResourceManager.GetString("ProductAdded", MODULE_NAME), entranceVoucherDetailTmp.ProductCodeName, ResourceManager.GetString("PurchaseOrder", MODULE_NAME)))
                    item.ItemInvalid = True
                End If
            Next
            'valido que el producto no este con el mismo contrato
            _listEntranceVoucherDetailValidationTmp = _listEntranceVoucherDetailValidation.Where(Function(x) x.ContractDetailId IsNot Nothing).ToList()
            For Each item In listInventoryContractDetailActivated
                'Dim entranceVoucherDetailTmp = _listEntranceVoucherDetailValidation.Find(Function(x) x.ProductId = item.ProductId And x.EntranceSource = 3)
                Dim entranceVoucherDetailTmp = _listEntranceVoucherDetailValidationTmp.Find(Function(x) x.ContractDetailId = item.Id)
                If entranceVoucherDetailTmp IsNot Nothing Then
                    errors.Add(String.Format(ResourceManager.GetString("ProductAdded", MODULE_NAME), entranceVoucherDetailTmp.ProductCodeName, ResourceManager.GetString("Contract", MODULE_NAME)))
                    item.ItemInvalid = True
                End If
            Next
            'valido que el producto no este con el mismo remision de entrada
            _listEntranceVoucherDetailValidationTmp = _listEntranceVoucherDetailValidation.Where(Function(x) x.RemissionEntranceDetailBatchSerialId IsNot Nothing).ToList()
            For Each item In listRemissionEntraceDetailActivated
                'Dim entranceVoucherDetailTmp = _listEntranceVoucherDetailValidation.Find(Function(x) x.ProductId = item.ProductId And x.EntranceSource = 3)
                Dim entranceVoucherDetailTmp = _listEntranceVoucherDetailValidationTmp.Find(Function(x) x.RemissionEntranceDetailBatchSerialId = item.Id)
                If entranceVoucherDetailTmp IsNot Nothing Then
                    errors.Add(String.Format(ResourceManager.GetString("ProductAdded", MODULE_NAME), entranceVoucherDetailTmp.ProductCodeName, ResourceManager.GetString("EntranceRemission", MODULE_NAME)))
                    item.ItemInvalid = True
                End If
            Next
            'valido que el producto no este con la misma remision de inventario en consignación
            _listEntranceVoucherDetailValidationTmp = _listEntranceVoucherDetailValidation.Where(Function(x) x.ConsignmentInventoryRemissionDetailBatchSerialId IsNot Nothing).ToList()
            For Each item In listConsignmentInventoryRemissionDetailActivated
                Dim entranceVoucherDetailTmp = _listEntranceVoucherDetailValidationTmp.Find(Function(x) x.ConsignmentInventoryRemissionDetailBatchSerialId = item.Id)
                If entranceVoucherDetailTmp IsNot Nothing Then
                    errors.Add(String.Format(ResourceManager.GetString("ProductAdded", MODULE_NAME), entranceVoucherDetailTmp.ProductCodeName, ResourceManager.GetString("ConsignmentInventoryRemission", MODULE_NAME)))
                    item.ItemInvalid = True
                End If
            Next
        End If
        Return errors
    End Function

    ''' <summary>
    ''' Metodo que carga el listado de detalles de remision de inventario en consignación
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadListConsignmentInventoryRemission(listXpo As XPCollection)
        listConsignmentInventoryRemissionDetail = New List(Of Domain.Entities.ConsignmentInventoryRemissionDetailBatchSerial)
        If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
            For Each itemXpo As ViewConsignmentInventoryRemissionWithoutLegalizeXpo In listXpo
                Dim consignmentInventoryRemissionDetailBatchSerial As New Domain.Entities.ConsignmentInventoryRemissionDetailBatchSerial
                With consignmentInventoryRemissionDetailBatchSerial
                    .Id = itemXpo.Id
                    .ProductId = itemXpo.ProductId
                    .Quantity = itemXpo.Quantity                    
                    .UsedQuantity = itemXpo.UsedQuantity
                    .LegalizedQuantity = itemXpo.LegalizedQuantity
                    .OutstandingQuantity = itemXpo.OutstandingQuantity
                    .GroupCodeName = itemXpo.GroupCodeName
                    .CodeNameProduct = itemXpo.CodeNameProduct
                    .Code = itemXpo.Code
                    .Activated = False
                    .ItemInvalid = False
                    If itemXpo.BatchSerialId IsNot Nothing Then
                        .BatchSerialId = itemXpo.BatchSerialId
                        .CodeBatchSerial = itemXpo.BatchCode
                    End If
                    .DocumentDate = itemXpo.RemissionDate
                    .CurrencyId = itemXpo.CurrencyId
                    .CurrencyAbbreviation = itemXpo.CurrencyAbbreviation
                    .ConsignmentInventoryRemissionDetail = New ConsignmentInventoryRemissionDetail() With {.UnitValue = itemXpo.UnitValue, .LastValue = itemXpo.LastValue, .IvaValue = itemXpo.IvaValue}
                End With
                listConsignmentInventoryRemissionDetail.Add(consignmentInventoryRemissionDetailBatchSerial)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Metodo que carga el listado de detalles de reimsion de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadListRemisionEntrance(listXpo As XPCollection)
        listRemissionEntranceDetail = New List(Of Domain.Entities.RemissionEntranceDetailBatchSerial)
        If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
            For Each itemXpo As InventoryRemissionEntranceDetailBatchSerialXpo In listXpo
                Dim remisionEntranceDetailBatchSerial As New Domain.Entities.RemissionEntranceDetailBatchSerial
                With remisionEntranceDetailBatchSerial
                    .Id = itemXpo.Id
                    .ProductId = itemXpo.RemissionEntranceDetailId.ProductId.Id
                    .Quantity = itemXpo.Quantity
                    .OutstandingQuantity = itemXpo.OutstandingQuantity
                    .GroupCodeName = itemXpo.RemissionEntranceDetailId.ProductId.ProductGroupId.Code + " - " + itemXpo.RemissionEntranceDetailId.ProductId.ProductGroupId.Name
                    .CodeNameProduct = itemXpo.RemissionEntranceDetailId.ProductId.Code + " - " + itemXpo.RemissionEntranceDetailId.ProductId.Name
                    .Code = itemXpo.RemissionEntranceDetailId.RemissionEntranceId.Code
                    .Activated = False
                    .ItemInvalid = False
                    .DiscountPercentage = itemXpo.RemissionEntranceDetailId.DiscountPercentage
                    If itemXpo.BatchSerialId IsNot Nothing Then
                        .BatchSerialId = itemXpo.BatchSerialId.Id
                        .CodeBatchSerial = itemXpo.BatchSerialId.BatchCode
                    End If
                    .DocumentDate = itemXpo.RemissionEntranceDetailId.RemissionEntranceId.RemissionDate
                    .RemissionEntranceDetail = New RemissionEntranceDetail() With {.UnitValue = itemXpo.RemissionEntranceDetailId.UnitValue, .GrossUnitValue = itemXpo.RemissionEntranceDetailId.GrossUnitValue, .LastValue = itemXpo.RemissionEntranceDetailId.LastValue, .IvaValue = itemXpo.RemissionEntranceDetailId.IvaValue}
                    .CurrencyId = itemXpo.CurrencyId
                    .CurrencyAbbreviation = itemXpo.CurrencyAbbreviation
                End With
                listRemissionEntranceDetail.Add(remisionEntranceDetailBatchSerial)
            Next
        End If
    End Sub

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
                    .GroupCodeName = itemXpo.ProductId.ProductGroupId.Code + " - " + itemXpo.ProductId.ProductGroupId.Name
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
                    .GroupCodeName = itemXpo.ProductId.ProductGroupId.Code + " - " + itemXpo.ProductId.ProductGroupId.Name
                    .CodeNameProduct = itemXpo.ProductId.Code + " - " + itemXpo.ProductId.Name
                    .Code = itemXpo.PurchaseOrderId.Code
                    .Activated = False
                    .ItemInvalid = False
                    .DocumentDate = itemXpo.PurchaseOrderId.DocumentDate
                    .DeliveredDate = itemXpo.PurchaseOrderId.DeliveredDate
                    .CurrencyAbbreviation = itemXpo.CurrencyAbbreviation
                    .CurrencyId = itemXpo?.PurchaseOrderId?.CurrencyId
                End With
                listPurchaseOrderDetail.Add(purchaseOrderDetail)
            Next
        End If
    End Sub

#End Region

    Private Sub INDRptCheActivate_EditValueChanged(sender As Object, e As EventArgs) Handles INDRptCheActivate.EditValueChanged
        Dim checkControl = DirectCast(sender, DevExpress.XtraEditors.CheckEdit)
        If INDGleEntranceSource.EditValue = 2 Then
            Dim purcharse = DirectCast(INDGvImportInfo.GetFocusedRow(), PurchaseOrderDetail)
            purcharse.Activated = checkControl.EditValue
            SetImageActivateColumn(EEntranceVoucherSource.Purcharse)
        ElseIf INDGleEntranceSource.EditValue = 3 Then
            Dim contract = DirectCast(INDGvImportInfo.GetFocusedRow(), InventoryContractDetail)
            contract.Activated = checkControl.EditValue
            SetImageActivateColumn(EEntranceVoucherSource.Contract)
        ElseIf INDGleEntranceSource.EditValue = 4 Then
            Dim entrance = DirectCast(INDGvImportInfo.GetFocusedRow(), RemissionEntranceDetailBatchSerial)
            entrance.Activated = checkControl.EditValue
            SetImageActivateColumn(EEntranceVoucherSource.RemissionEntrance)
        ElseIf INDGleEntranceSource.EditValue = 5 Then
            Dim entrance = DirectCast(INDGvImportInfo.GetFocusedRow(), ConsignmentInventoryRemissionDetailBatchSerial)
            entrance.Activated = checkControl.EditValue
            SetImageActivateColumn(EEntranceVoucherSource.ConsignmentInventoryRemission)
        End If
        Me.INDGcImportInfo.RefreshDataSource()
        Me.INDGcImportInfo.Invalidate()
    End Sub

    ''' <summary>
    ''' metodo para establecer la imagen a la columna de activar o inactivar
    ''' </summary>
    ''' <param name="remissionSource"></param>
    ''' <remarks></remarks>
    Private Sub SetImageActivateColumn(remissionSource As EEntranceVoucherSource)
        If remissionSource = EEntranceVoucherSource.Purcharse Then
            Dim listActivated = listPurchaseOrderDetail.FindAll(Function(x) x.Activated = True)
            If listActivated.Count = listPurchaseOrderDetail.Count Then
                Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.check
            Else
                Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
            End If
        ElseIf remissionSource = EEntranceVoucherSource.Contract Then
            Dim listActivated = listInventoryContractDetail.FindAll(Function(x) x.Activated = True)
            If listActivated.Count = listInventoryContractDetail.Count Then
                Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.check
            Else
                Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
            End If
        ElseIf remissionSource = EEntranceVoucherSource.RemissionEntrance Then
            Dim listActivated = listRemissionEntranceDetail.FindAll(Function(x) x.Activated = True)
            If listActivated.Count = listRemissionEntranceDetail.Count Then
                Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.check
            Else
                Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
            End If
        ElseIf remissionSource = EEntranceVoucherSource.ConsignmentInventoryRemission Then
            Dim listActivated = listConsignmentInventoryRemissionDetail.FindAll(Function(x) x.Activated = True)
            If listActivated.Count = listConsignmentInventoryRemissionDetail.Count Then
                Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.check
            Else
                Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
            End If
        End If
    End Sub

    Enum EEntranceVoucherSource As Integer
        Purcharse = 2
        Contract = 3
        RemissionEntrance = 4
        ConsignmentInventoryRemission = 5
    End Enum
End Class