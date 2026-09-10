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

Public Class FrmImportInfo

#Region "EVENTS"
    ''' <summary>
    ''' evento para obtener el listado del detalle de la remision de entrada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event GetListRemissionEntranceDetail(sender As Object, e As GetListRemissionEntranceDetailEventArgs)
#End Region

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
    ''' listado del detalla de la remision para validar que los productos no se pepitan con la misma fuente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listRemissionEntranceDetailValidation As List(Of RemissionEntranceDetail)
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

    Public WriteOnly Property WarehouseId As Integer
        Set(value As Integer)
            _warehouseId = value
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
    Public WriteOnly Property ListRemissionEntranceDetailValidation As List(Of RemissionEntranceDetail)
        Set(value As List(Of RemissionEntranceDetail))
            If value IsNot Nothing Then
                _listRemissionEntranceDetailValidation = New List(Of RemissionEntranceDetail)(value.ToArray())
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
#End Region

#Region "HANDLES"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        listPurchaseOrderDetail = Nothing
        listInventoryContractDetail = Nothing
        _listRemissionEntranceDetailValidation = Nothing
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
    Private Sub FrmImportInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDGleRemissionSource.Properties.DataSource = ListRemissionSource
        INDGleRemissionSource.Focus()
        IndigoGridControl1.RefreshGrid(INDGcImportInfo)
    End Sub
#End Region

#Region "EditValueChanged"
    Private Sub INDGleRemissionSource_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleRemissionSource.EditValueChanged
        If INDGleRemissionSource.EditValue IsNot Nothing Then
            If INDGleRemissionSource.EditValue = 2 Then
                Using model As New MReferralEntry(Me.Tag)
                    '************Cargo los datos de orden de compra*************'
                    Dim warehouseConsignment = False
                    Dim listPurchaseOrderDetailXpo As XPCollection = model.ListPurchaseOrderDetailByWarehouseConsignment(_supplierId, _supplierDistributionLineId, _warehouseId, warehouseConsignment)
                    LoadListPurcharseOrder(listPurchaseOrderDetailXpo)
                    INDGcImportInfo.DataSource = Nothing
                    INDGcImportInfo.DataSource = listPurchaseOrderDetail
                    INDGcImportInfo.RefreshDataSource()
                    SetImageActivateColumn(ERemissionSource.Purcharse)
                End Using
            Else
                Using model As New MBusqueda
                    '************Cargo los datos de contrato*************'
                    Dim filter() As Object = {_supplierId, _supplierDistributionLineId}
                    Dim listInventoryContractDetailXpo As XPCollection = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListInventoryContractDetailBySupplierIdAndSupplierDistributionLineId, filter)
                    LoadListContract(listInventoryContractDetailXpo)
                    INDGcImportInfo.DataSource = Nothing
                    INDGcImportInfo.DataSource = listInventoryContractDetail
                    INDGcImportInfo.RefreshDataSource()
                    SetImageActivateColumn(ERemissionSource.Contract)
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
        Dim args As New GetListRemissionEntranceDetailEventArgs
        args.ListRemissionEntranceDetail = GenerateRemissionEntranceDetail()
        If args.ListRemissionEntranceDetail.Count > 0 Then
            Me.Close()
            RaiseEvent GetListRemissionEntranceDetail(Nothing, args)
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

                Else
                    If INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of InventoryContractDetail).Where(Function(s) s.Activated).ToList().Count = INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of InventoryContractDetail).Count Then
                        INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of InventoryContractDetail).ForEach(Sub(x) x.Activated = False)
                        Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
                    Else
                        INDGvImportInfo.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of InventoryContractDetail).ForEach(Sub(x) x.Activated = True)
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
    Private Sub SetImageActivateColumn(remissionSource As ERemissionSource)
        If remissionSource = ERemissionSource.Purcharse Then
            Dim listActivated = listPurchaseOrderDetail.FindAll(Function(x) x.Activated = True)
            If listActivated.Count = listPurchaseOrderDetail.Count Then
                Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.check
            Else
                Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
            End If
        Else
            Dim listActivated = listInventoryContractDetail.FindAll(Function(x) x.Activated = True)
            If listActivated.Count = listInventoryContractDetail.Count Then
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
    Private Function GenerateRemissionEntranceDetail() As List(Of RemissionEntranceDetail)
        Dim listRemissionEntranceDetailTmp As New List(Of RemissionEntranceDetail)
        Dim remissionEntranceDetailTmp As RemissionEntranceDetail
        'genero los detalles por orden de compra
        For Each item In listPurchaseOrderDetail.FindAll(Function(x) x.Activated = True And x.ItemInvalid = False)

            remissionEntranceDetailTmp = New RemissionEntranceDetail
            With remissionEntranceDetailTmp
                .SourceCode = item.Code
                .RemissionSource = 2
                .PurchaseOrderDetailId = item.Id
                .ProductId = item.ProductId
                .QuantityImport = item.OutstandingQuantity
                .Quantity = item.OutstandingQuantity
                .GrossUnitValue = item.Value
                .DiscountPercentage = item.DiscountPercentage
                .IvaPercentage = item.IvaPercentage
            End With
            listRemissionEntranceDetailTmp.Add(remissionEntranceDetailTmp)
        Next
        'genero los detalles por contratos
        For Each item In listInventoryContractDetail.FindAll(Function(x) x.Activated = True And x.ItemInvalid = False)
            remissionEntranceDetailTmp = New RemissionEntranceDetail
            With remissionEntranceDetailTmp
                .SourceCode = item.Code
                .RemissionSource = 3
                .ContractDetailId = item.Id
                .ProductId = item.ProductId
                .QuantityImport = item.OutstandingQuantity
                .Quantity = item.OutstandingQuantity
                .UnitValue = item.Value
            End With
            listRemissionEntranceDetailTmp.Add(remissionEntranceDetailTmp)
        Next
        Return listRemissionEntranceDetailTmp
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
        If (listInventoryContractDetailActivated.Count = 0 And listPurchaseOrderDetailActivated.Count = 0) Then
            errors.Add(ResourceManager.GetString("NoSelectedItem", MODULE_NAME))
        End If
        If Not errors?.Any() Then
            If Me._currencyId Is Nothing OrElse ((listPurchaseOrderDetailActivated?.Any() AndAlso Not listPurchaseOrderDetailActivated?.All(Function(x) x.CurrencyId = Me._currencyId)) _
                        OrElse (listInventoryContractDetailActivated?.Any() AndAlso Not listInventoryContractDetailActivated?.All(Function(x) x.CurrencyId = Me._currencyId))) Then
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

            If _listRemissionEntranceDetailValidation IsNot Nothing AndAlso _listRemissionEntranceDetailValidation.Count > 0 Then
                'valido que el producto no este con la misma orden de compra solo en los items que estan seleccionados
                For Each item In listPurchaseOrderDetailActivated
                    Dim remissionEntranceDetailTmp = _listRemissionEntranceDetailValidation.Find(Function(x) x.ProductId = item.ProductId And x.RemissionSource = 2 And x.SourceCode = item.Code)
                    If remissionEntranceDetailTmp IsNot Nothing Then
                        errors.Add(String.Format(ResourceManager.GetString("ProductAdded", MODULE_NAME), remissionEntranceDetailTmp.CodeNameProduct, ResourceManager.GetString("PurchaseOrder", MODULE_NAME)))
                        item.ItemInvalid = True
                    End If
                Next
                'valido que el producto no este con el mismo contrato
                For Each item In listInventoryContractDetailActivated
                    Dim remissionEntranceDetailTmp = _listRemissionEntranceDetailValidation.Find(Function(x) x.ProductId = item.ProductId And x.RemissionSource = 3 And x.SourceCode = item.Code)
                    If remissionEntranceDetailTmp IsNot Nothing Then
                        errors.Add(String.Format(ResourceManager.GetString("ProductAdded", MODULE_NAME), remissionEntranceDetailTmp.CodeNameProduct, ResourceManager.GetString("Contract", MODULE_NAME)))
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
                    .CurrencyAbbreviation = itemXpo?.CurrencyAbbreviation
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
                    .CurrencyAbbreviation = itemXpo?.CurrencyAbbreviation
                    .CurrencyId = itemXpo?.PurchaseOrderId?.CurrencyId
                End With
                listPurchaseOrderDetail.Add(purchaseOrderDetail)
            Next
        End If
    End Sub

#End Region

    Private Sub INDRptCheActivate_EditValueChanged(sender As Object, e As EventArgs) Handles INDRptCheActivate.EditValueChanged
        Dim checkControl = DirectCast(sender, DevExpress.XtraEditors.CheckEdit)
        If INDGleRemissionSource.EditValue = 2 Then
            Dim purcharse = DirectCast(INDGvImportInfo.GetFocusedRow(), PurchaseOrderDetail)
            purcharse.Activated = checkControl.EditValue
            SetImageActivateColumn(ERemissionSource.Purcharse)
        Else
            Dim contract = DirectCast(INDGvImportInfo.GetFocusedRow(), InventoryContractDetail)
            contract.Activated = checkControl.EditValue
            SetImageActivateColumn(ERemissionSource.Contract)
        End If
        Me.INDGcImportInfo.RefreshDataSource()
        Me.INDGcImportInfo.Invalidate()
    End Sub


End Class

Enum ERemissionSource As Integer
    Purcharse = 2
    Contract = 3
End Enum