'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Ernesto Córdoba
' Created          : 08-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.FixedAsset.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports System.Drawing
Imports Presentation.Maintenance
Imports Presentation.Base.BaseClass
Imports System.Text
Imports Presentation.Payments.MVP
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Inventory
Imports System.ComponentModel
Imports Infrastructure.Data.Xpo.FixedAssetRepository

#End Region

Public Class FrmImportInfo

#Region "Enums"

    Enum EEntranceVoucherSource As Integer
        Purcharse = 2
        Contract = 3
        RemissionEntrance = 4
    End Enum

#End Region

#Region "EVENTS"

    Public Event AddImportsInfoEventArgs(sender As Object, e As AddImportsInfo)

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
    Dim listPurchaseOrderDetail As New List(Of FixedAssetPurchaseOrderItem)

    ''' <summary>
    ''' listado del detalla de la remision para validar que los productos no se pepitan con la misma fuente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listEntranceVoucherDetailValidation As List(Of FixedAssetRemissionEntranceItem)

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
    ''' id de la linea de la localizacion
    ''' </summary>
    ''' <remarks></remarks>
    Private _getLocationResponsible As Integer

    ''' <summary>
    ''' tipo de adquisicion
    ''' </summary>
    ''' <remarks></remarks>
    Private _getAdquisitionType As Integer

    ''' <summary>
    ''' Presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PImports

    ''' <summary>
    ''' Listado de articulos de orden de compra
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListFixedAssetPurchaseOrderItemXpo As List(Of FixedAssetPurchaseOrderItemXpo)

    ''' <summary>
    ''' Listado que se devuelve
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListFixedAssetRemissionEntranceItem As List(Of FixedAssetRemissionEntranceItem)

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
    Public Property SupplierDistributionLineId As Integer
        Get
            Return _supplierDistributionLineId
        End Get
        Set(value As Integer)
            _supplierDistributionLineId = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para asignar el Id de la localización
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property GetLocationResponsible As Integer
        Get
            Return _getLocationResponsible
        End Get
        Set(value As Integer)
            _getLocationResponsible = value
        End Set
    End Property

    ''' <summary>
    ''' Variable que toma el Tipo de Adquisición para las Validaciones Pertinentes
    ''' </summary>
    ''' <remarks></remarks>
    Public Property AdquisitionType As Integer
        Get
            Return _getAdquisitionType
        End Get
        Set(value As Integer)
            _getAdquisitionType = value
        End Set
    End Property

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Private _listCompare As List(Of FixedAssetRemissionEntranceItem)
    Public Property ListCompare As List(Of FixedAssetRemissionEntranceItem)
        Get
            Return _listCompare
        End Get
        Set(value As List(Of FixedAssetRemissionEntranceItem))
            _listCompare = value
        End Set
    End Property

#End Region

#Region "HANDLES"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        listPurchaseOrderDetail = Nothing
        _listEntranceVoucherDetailValidation = Nothing
        _supplierId = Nothing
        _supplierDistributionLineId = Nothing
        _getLocationResponsible = Nothing
        _getAdquisitionType = Nothing
        Presenter = Nothing
        ListFixedAssetPurchaseOrderItemXpo = Nothing
        ListFixedAssetRemissionEntranceItem = Nothing
    End Sub

    ''' <summary>
    ''' load del del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmImportProducts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IndigoGridControl1.RefreshGrid(INDGcImportInfo)
        Presenter = New PImports()
        INDGcImportInfo.DataSource = Nothing
        ListFixedAssetPurchaseOrderItemXpo = Presenter.ListFixedAssetPurchaseOrderBySupplierDistributionLineId(SupplierDistributionLineId)
        If ListFixedAssetPurchaseOrderItemXpo IsNot Nothing AndAlso ListFixedAssetPurchaseOrderItemXpo.Count > 0 Then
            INDGcImportInfo.DataSource = ListFixedAssetPurchaseOrderItemXpo
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub INDRptCheActivate_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptCheActivate.EditValueChanging
        If e IsNot Nothing Then
            Dim itemXpo As FixedAssetPurchaseOrderItemXpo = INDGvImportInfo.GetFocusedRow()
            If itemXpo IsNot Nothing Then
                itemXpo.SelectOption = e.NewValue
                INDGcImportInfo.RefreshDataSource()
                If ListFixedAssetPurchaseOrderItemXpo.Where(Function(item) item.SelectOption = True).Count = ListFixedAssetPurchaseOrderItemXpo.Count Then
                    Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.check
                Else
                    Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.undcheck
                End If
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
        ImportInfoPurchaseOrder()
    End Sub

#End Region

#Region "MouseDoubleClick"

    ''' <summary>
    ''' activa o desactiva todos los items del listado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGcImportInfo_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDGcImportInfo.MouseDoubleClick
        If ListFixedAssetPurchaseOrderItemXpo IsNot Nothing AndAlso ListFixedAssetPurchaseOrderItemXpo.Count > 0 Then
            Dim hitPoint = Me.INDGvImportInfo.CalcHitInfo(e.Location)
            If hitPoint.Column IsNot Nothing Then
                If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDcolSelect") Then

                    Dim listFilterXpCollection = INDGvImportInfo.DataController.GetAllFilteredAndSortedRows()
                    Dim cont As Integer = (From l In listFilterXpCollection Where l.SelectOption = True).Count

                    If cont = listFilterXpCollection.Count Then
                        For Each item In listFilterXpCollection
                            item.SelectOption = False
                        Next
                        Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.undcheck
                    Else
                        For Each item In listFilterXpCollection
                            item.SelectOption = True
                        Next
                        Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.check
                    End If
                    Me.INDGcImportInfo.RefreshDataSource()
                    Me.INDGcImportInfo.Invalidate()
                End If
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
    Private Sub SetImageActivateColumn(remissionSource As EEntranceVoucherSource)
        If remissionSource = EEntranceVoucherSource.Purcharse Then
            Dim listActivated = listPurchaseOrderDetail.FindAll(Function(x) x.Activated = True)
            If listActivated.Count = listPurchaseOrderDetail.Count Then
                Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.check
            Else
                Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.undcheck
            End If
        End If
    End Sub

    ''' <summary>
    ''' metodo para generar los detalles de la remision
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateEntranceVoucherDetail() As List(Of FixedAssetRemissionEntranceItem)
        Dim listEntranceVoucherDetailTmp As New List(Of FixedAssetRemissionEntranceItem)
        Dim entranceVoucherDetailTmp As FixedAssetRemissionEntranceItem
        Dim entranceVoucherDetailEquipmentDetailTmp As FixedAssetRemissionEntranceItemDetail
        Dim entranceVoucherPartsTmp As FixedAssetRemissionEntranceItemDetailPart
        'genero los detalles por orden de compra
        For Each item In listPurchaseOrderDetail.FindAll(Function(x) x.Activated = True And x.ItemInvalid = False)
            entranceVoucherDetailTmp = New FixedAssetRemissionEntranceItem
            With entranceVoucherDetailTmp
                .Quantity = item.Quantity
                .RemissionSource = 2
                '.PurchaseOrderDetailId = item.Id
                .SourceCode = item.Code
                '.inven = item.InventoryTypeId
                .ItemId = item.ItemId
                '.NameEquipment = item.NameEquipment
                '.NameInventoryType = item.NameInventoryType
                '.NameEquipmentType = item.NameEquipmentType
                .IVAId = item.IVAId
                '.NameIva = item.NameIva
                '.IdEquipmentType = item.ItemTypeId
                '.ProductValue = item.ProductValue
                .TotalValue = item.TotalValue
                '.TaxesValue = item.TaxesValue
                .TrademarkId = item.TrademarkId
                '.NameTrademark = item.NameTrademark
                .Model = item.Model
                '.IdPolize = item.poli
                '.NamePoliza = item.NamePoliza
            End With

            'If item.FixedAssetPurchaseOrderEquipmentDetail IsNot Nothing And item.FixedAssetPurchaseOrderEquipmentDetail.Count > 0 Then
            '    For Each item1 In item.FixedAssetPurchaseOrderEquipmentDetail
            '        entranceVoucherDetailEquipmentDetailTmp = New InputRemissionEquipmentDetail

            '        With entranceVoucherDetailEquipmentDetailTmp
            '            .IdInputRemissionEquipment = item.Id
            '            .IdEquipment = item.IdEquipment
            '            .LicensePlate = item1.LicensePlate
            '            .Serie = item1.Serie
            '            .IdResponsible = item1.IdResponsible
            '            .NameResponsible = item1.NameResponsible
            '            .IdFunctionalUnit = item1.IdFunctionalUnit
            '            .NameFunctionalUnit = item1.NameFunctionalUnit
            '            .IdLocation = item1.IdLocation
            '            .NameLocation = item1.NameLocation
            '            .AdquisitionDate = item1.AdquisitionDate
            '            .Depreciate = item1.Depreciate
            '            .ComponentDepreciate = item1.ComponentDepreciate
            '        End With

            '        If item1.FixedAssetPurchaseOrderPartAccesoriesConsumibles IsNot Nothing And item1.FixedAssetPurchaseOrderPartAccesoriesConsumibles.Count > 0 Then
            '            For Each item2 In item1.FixedAssetPurchaseOrderPartAccesoriesConsumibles
            '                entranceVoucherPartsTmp = New FixedAssetPartAccesoriesConsumiblesInputRemission

            '                With entranceVoucherPartsTmp
            '                    .IdInputRemissionEquipmentDetail = item1.Id
            '                    .IdEquipment = item.IdEquipment
            '                    .DepreciatePart = item2.DepreciatePart
            '                    .IdPartAccesoriesConsumibles = item2.IdPartAccesoriesConsumibles
            '                    .NamePartsAccesoriesConsumibles = item2.NamePartsAccesoriesConsumibles
            '                    .Value = item2.Value
            '                End With

            '                entranceVoucherDetailEquipmentDetailTmp.FixedAssetPartAccesoriesConsumiblesInputRemission.Add(entranceVoucherPartsTmp)

            '            Next
            '        End If


            '        entranceVoucherDetailTmp.InputRemissionEquipmentDetail.Add(entranceVoucherDetailEquipmentDetailTmp)
            '    Next
            'End If


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
        'Dim errors As New List(Of String)
        'Dim listPurchaseOrderDetailActivated = listPurchaseOrderDetail.FindAll(Function(x) x.Activated = True)

        'If listPurchaseOrderDetailActivated.Count = 0 Then
        '    errors.Add(ResourceManager.GetString("NoSelectedItem", MODULE_NAME))
        'End If
        'If errors.Count = 0 Then
        '    If _listEntranceVoucherDetailValidation IsNot Nothing AndAlso _listEntranceVoucherDetailValidation.Count > 0 Then
        '        'valido que el producto no este con la misma orden de compra solo en los items que estan seleccionados
        '        Dim _listEntranceVoucherDetailValidationTmp = _listEntranceVoucherDetailValidation.Where(Function(x) x.PurchaseOrderDetailId IsNot Nothing).ToList()
        '        For Each item In listPurchaseOrderDetailActivated
        '            'Dim entranceVoucherDetailTmp = _listEntranceVoucherDetailValidation.Find(Function(x) x.ProductId = item.ProductId And x.EntranceSource = 2)
        '            Dim entranceVoucherDetailTmp = _listEntranceVoucherDetailValidationTmp.Find(Function(x) x.PurchaseOrderDetailId = item.Id)
        '            If entranceVoucherDetailTmp IsNot Nothing Then
        '                errors.Add(String.Format(ResourceManager.GetString("ProductAdded", MODULE_NAME), entranceVoucherDetailTmp.ProductCodeName, ResourceManager.GetString("PurchaseOrder", MODULE_NAME)))
        '                item.ItemInvalid = True
        '            End If
        '        Next
        '    End If
        'End If
        'Return errors
    End Function

    ''' <summary>
    ''' Metodo que carga el listado de detalles de contrato
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadListPurcharseOrder(listXpo As XPCollection)
        listPurchaseOrderDetail = New List(Of Domain.Entities.FixedAssetPurchaseOrderItem)
        If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
            For Each itemXpo As FixedAssetPurchaseOrderEquipmentXpo In listXpo
                If itemXpo.OutstandingQuantity > 0 Then
                    Dim purchaseOrderDetail As New Domain.Entities.FixedAssetPurchaseOrderItem
                    With purchaseOrderDetail
                        .Id = itemXpo.Id
                        .PurchaseOrderId = itemXpo.IdFixedAssetPurchaseOrder.Id
                        .Code = itemXpo.IdFixedAssetPurchaseOrder.Code
                        '.invent = itemXpo.IdInventoryType.Id
                        .NameInventoryType = itemXpo.IdInventoryType.Name
                        '.ItemTypeId = itemXpo.IdEquipmentType.Id
                        .NameEquipmentType = itemXpo.IdEquipmentType.Name
                        .NameEquipment = itemXpo.IdEquipment.Description
                        .ItemId = itemXpo.IdEquipment.Id
                        .IVAId = itemXpo.IdIvaCode.Id
                        .NameIva = itemXpo.IdIvaCode.Name
                        .Quantity = itemXpo.Quantity
                        .NameEquipment = itemXpo.IdEquipment.Description
                        '.ProductValue = itemXpo.ProductValue
                        .TotalValue = itemXpo.TotalValue
                        '.TaxesValue = itemXpo.TaxesValue
                        .TrademarkId = itemXpo.IdTrademark.Id
                        .NameTrademark = itemXpo.IdTrademark.Descripcion
                        .Model = itemXpo.Model
                        '.IdPolize = itemXpo.IdPolize.Id
                        .NamePoliza = itemXpo.IdPolize.Name
                        .OutstandingQuantity = itemXpo.OutstandingQuantity

                    End With

                    'If itemXpo.FixedAssetPurchaseOrderEquipmentDetailXpo IsNot Nothing And itemXpo.FixedAssetPurchaseOrderEquipmentDetailXpo.Count > 0 Then
                    '    For Each itemXpoDetail As FixedAssetPurchaseOrderEquipmentDetailXpo In itemXpo.FixedAssetPurchaseOrderEquipmentDetailXpo
                    '        Dim purchaseOrderEquipmentDetail As New Domain.Entities.FixedAssetPurchaseOrderEquipmentDetail
                    '        With purchaseOrderEquipmentDetail
                    '            .IdFixedAssetPurchaseOrderEquipment = itemXpo.Id
                    '            .IdEquipment = itemXpo.IdEquipment.Id
                    '            .LicensePlate = itemXpoDetail.LicensePlate
                    '            .Serie = itemXpoDetail.Serie
                    '            .IdResponsible = itemXpoDetail.IdResponsible.Id
                    '            .NameResponsible = itemXpoDetail.IdResponsible.IdThirdParty.Name
                    '            .IdFunctionalUnit = itemXpoDetail.IdFunctionalUnit.Id
                    '            .NameFunctionalUnit = itemXpoDetail.IdFunctionalUnit.Name
                    '            .IdLocation = itemXpoDetail.IdLocation.Id
                    '            .NameLocation = itemXpoDetail.IdLocation.Name
                    '            .AdquisitionDate = itemXpoDetail.AdquisitionDate
                    '            .Depreciate = itemXpoDetail.Depreciate
                    '            .ComponentDepreciate = itemXpoDetail.ComponentDepreciate

                    '        End With

                    '        'If itemXpoDetail.FixedAssetPurchaseOrderPartAccesoriesConsumiblesXpo IsNot Nothing And itemXpoDetail.FixedAssetPurchaseOrderPartAccesoriesConsumiblesXpo.Count > 0 Then
                    '        '    'For Each itemXpoParts As FixedAssetPurchaseOrderPartAccesoriesConsumiblesXpo In itemXpoDetail.FixedAssetPurchaseOrderPartAccesoriesConsumiblesXpo
                    '        '    '    Dim purchaseOrderParts As New Domain.Entities.FixedAssetPurchaseOrderPartAccesoriesConsumibles
                    '        '    '    With purchaseOrderParts
                    '        '    '        .IdFixedAssetPurchaseOrderEquipmentDetail = itemXpo.Id
                    '        '    '        .IdEquipment = itemXpo.IdEquipment.Id
                    '        '    '        .IdPartAccesoriesConsumibles = itemXpoParts.IdPartAccesoriesConsumibles.Id
                    '        '    '        .NamePartsAccesoriesConsumibles = itemXpoParts.IdPartAccesoriesConsumibles.Name
                    '        '    '        .DepreciatePart = itemXpoParts.DepreciatePart
                    '        '    '        .Value = itemXpoParts.Value
                    '        '    '    End With

                    '        '    '    purchaseOrderEquipmentDetail.FixedAssetPurchaseOrderPartAccesoriesConsumibles.Add(purchaseOrderParts)

                    '        '    'Next
                    '        'End If

                    '        'purchaseOrderDetail.FixedAssetPurchaseOrderEquipmentDetail.Add(purchaseOrderEquipmentDetail)

                    '    Next
                    'End If

                    'If itemXpo.FixedAssetPurchaseOrderPartAccesoriesConsumiblesXpo IsNot Nothing And itemXpo.FixedAssetPurchaseOrderPartAccesoriesConsumiblesXpo.Count > 0 Then
                    '    For Each itemXpoParts As FixedAssetPurchaseOrderPartAccesoriesConsumiblesXpo In itemXpo.FixedAssetPurchaseOrderPartAccesoriesConsumiblesXpo
                    '        Dim purchaseOrderParts As New Domain.Entities.FixedAssetPurchaseOrderPartAccesoriesConsumibles
                    '        With purchaseOrderParts
                    '            .IdFixedAssetPurchaseOrderEquipment = itemXpo.Id
                    '            .IdEquipment = itemXpo.IdEquipment.Id
                    '            .IdPartAccesoriesConsumibles = itemXpoParts.IdPartAccesoriesConsumibles.Id
                    '            .NamePartsAccesoriesConsumibles = itemXpoParts.IdPartAccesoriesConsumibles.Name
                    '            .DepreciatePart = itemXpoParts.DepreciatePart
                    '            .Value = itemXpoParts.Value
                    '        End With

                    '        purchaseOrderDetail.FixedAssetPurchaseOrderPartAccesoriesConsumibles.Add(purchaseOrderParts)

                    '    Next
                    'End If

                    listPurchaseOrderDetail.Add(purchaseOrderDetail)
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' Metodo que importa la información al form principal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ImportInfoPurchaseOrder()
        Try
            'AsyncLoader(True)
            'Se valida que al menos haya un item seleccionado en la rejilla
            If ListFixedAssetPurchaseOrderItemXpo Is Nothing OrElse ListFixedAssetPurchaseOrderItemXpo.Count = 0 OrElse ListFixedAssetPurchaseOrderItemXpo.Where(Function(item) item.SelectOption = True).Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un item para poder importar."
                'AsyncLoader(False)
                Exit Sub
            End If
            'Se valida que la orden que se selecciono no este en la rejilla principal
            If ListCompare IsNot Nothing AndAlso ListCompare.Count > 0 Then
                Dim ListErrors As New StringBuilder
                ListFixedAssetPurchaseOrderItemXpo.ForEach(Sub(item)
                                                               If item.SelectOption Then
                                                                   If ListCompare.Where(Function(itemCompare) itemCompare.ItemId = item.ItemId.Id).Count > 0 Then
                                                                       ListErrors.AppendLine("El articulo " + item.ItemId.CodeDescription + " ya existe en la lista.")
                                                                   End If
                                                               End If
                                                           End Sub)
                If ListErrors.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ListErrors.ToString
                    'AsyncLoader(False)
                    Exit Sub
                End If
            End If
            'Se crean los objetos correspondenties para pegar en la rejilla
            CreateEntitiesWithPurchaseOrder()

            'Se realiza el llamado para ingresar toda la info necesaria
            Using formulario As New FrmFixedAssetRemissionEntranceItem
                Me.Cursor = ChangeCursorIndigo()
                AddHandler formulario.AddEquipmentRemissionEventArgs, AddressOf ReturnAddEventArgs
                formulario.ListFixedAssetRemissionEntranceItem = ListFixedAssetRemissionEntranceItem
                formulario.GetLocationResponsible = GetLocationResponsible
                formulario.ImportData = True
                formulario.AdquisitionType = AdquisitionType
                formulario.Size = New Drawing.Size(1090, 750)
                formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent = New Base.FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using

            'AsyncLoader(False)
        Catch ex As Exception
            'AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo que retorna el listado al form principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddEventArgs(sender As Object, e As AddEquipmentRemissionEventArgs)
        ListFixedAssetRemissionEntranceItem = e.ListFixedAssetRemissionEntranceItem
        'Se crea el objeto que se va a devolver
        Dim args As New AddImportsInfo
        args.ListFixedAssetRemissionEntranceItem = ListFixedAssetRemissionEntranceItem
        RaiseEvent AddImportsInfoEventArgs(Nothing, args)
        Me.Close()
    End Sub

    ''' <summary>
    ''' Crea la entidad de articulo de rmeision de entrada con la orden de compra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateEntitiesWithPurchaseOrder()
        'Se instancia el listado que se envia al form principal
        ListFixedAssetRemissionEntranceItem = New List(Of FixedAssetRemissionEntranceItem)
        'Listado de errores
        Dim ListErorrs As New StringBuilder

        For Each purchaseOrderItem In (From l In ListFixedAssetPurchaseOrderItemXpo Where l.SelectOption = True).ToList 'Se recorre los items que se seleccionaron
            'Se valida que el item no se haya agregado con anterioridad al listado que se envia al form principal
            If ListFixedAssetRemissionEntranceItem.Where(Function(item) item.ItemId = purchaseOrderItem.ItemId.Id).Count > 0 Then
                ListErorrs.AppendLine("El articulo " + purchaseOrderItem.ItemId.CodeDescription + " de la orden " + purchaseOrderItem.PurchaseOrderId.Code + " ya ha sido agregado.")
                Continue For
            End If

            Dim fixedAssetRemissionEntranceItem As New FixedAssetRemissionEntranceItem
            With fixedAssetRemissionEntranceItem
                .RemissionSource = 2 'Orden de compra
                .SourceCode = purchaseOrderItem.PurchaseOrderId.Code
                .PurchaseOrderItemId = purchaseOrderItem.Id
                .ItemId = purchaseOrderItem.ItemId.Id
                .NameEquipment = purchaseOrderItem.ItemId.CodeDescription
                .IVAId = purchaseOrderItem.IVAId.Id
                .NameIva = purchaseOrderItem.IVAId.Code + " - " + purchaseOrderItem.IVAId.Name
                .TrademarkId = purchaseOrderItem.TrademarkId.Id
                .NameTrademark = purchaseOrderItem.TrademarkId.CodeDescription
                .Model = purchaseOrderItem.Model
                .Quantity = purchaseOrderItem.Quantity
                .OutstandingQuantity = purchaseOrderItem.OutstandingQuantity
                .UnitValue = purchaseOrderItem.UnitValue
                .SubTotalValue = purchaseOrderItem.SubTotalValue
                .IvaPercentage = purchaseOrderItem.IvaPercentage
                .IvaValue = purchaseOrderItem.IvaValue
                .DiscountPercentage = purchaseOrderItem.DiscountPercentage
                .DiscountValue = purchaseOrderItem.DiscountValue
                .TotalValue = purchaseOrderItem.TotalValue
            End With
            ListFixedAssetRemissionEntranceItem.Add(fixedAssetRemissionEntranceItem)
        Next

        If ListErorrs.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ListErorrs.ToString
        End If
    End Sub

#End Region

End Class