'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Diego A. Roldán L.
' Created          : 2023-03-24
'
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Inventory.MVP

Public Class FrmPopupProductConsignmentTransfer

#Region "Properties and variables"
    ''' <summary>
    ''' Ubicación del mouse al dar click
    ''' </summary>
    Private _mouseLocation As Drawing.Point
    ''' <summary>
    ''' Nombre del módulo
    ''' </summary>
    Private Const MODULE_NAME = "Inventory"
    ''' <summary>
    ''' Almacén de origen
    ''' </summary>
    ''' <returns></returns>
    Public Property SourceWarehouseId As Integer
    ''' <summary>
    ''' Proveedor del almacén de origen
    ''' </summary>
    ''' <returns></returns>
    Public Property SourceSupplierWarehouseId As Integer
    ''' <summary>
    ''' Producto seleccionado
    ''' </summary>
    Private _product As InventoryProductXpo = Nothing
    ''' <summary>
    ''' Listado del producto en el inventario
    ''' </summary>
    Private _listPhysicalInventory As List(Of PhysicalInventory)
    ''' <summary>
    ''' Indica si se estan cargando los controles o no
    ''' </summary>
    Private _isLoading As Boolean = False
    ''' <summary>
    ''' Interfaz utilizada para validar si un producto ya existe en el listado
    ''' </summary>
    Private _validateDetails As FrmConsignmentTransfer.IValidateDetails
    ''' <summary>
    ''' detalle a editar
    ''' </summary>
    Private _consignmentTransferDetailEdit As ConsignmentTransferDetail = Nothing
    ''' <summary>
    ''' Evento ejecutado para guardar los cambios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="consignmentDetail"></param>
    Public Event OnAddConsignmentProduct(sender As Object, consignmentDetail As ConsignmentTransferDetail)
    ''' <summary>
    ''' Mensaje
    ''' </summary>
    ''' <param name="Icono"></param>
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
#End Region

#Region "Functions and Methods"
    ''' <summary>
    ''' Carga los controles para editar un detalle
    ''' </summary>
    ''' <param name="consignmentDetail"></param>
    ''' <returns></returns>
    Public Async Function EditDetail(consignmentDetail As ConsignmentTransferDetail) As Task
        Try
            AsyncLoader(True)
            _consignmentTransferDetailEdit = consignmentDetail
            _isLoading = True
            _product = Await Task.Factory.StartNew(Function() XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of InventoryProductXpo)($"Id = {consignmentDetail.ProductId}"))
            INDPceProducts.Text = $"{_product.Code} - {_product.Name}"
            INDPceProducts.Properties.ReadOnly = True

            With consignmentDetail
                INDSleTargetWarehouse.EditValue = .WarehouseId
                INDSleTargetWarehouse.Properties.NullText = .WarehouseCodeName
                INDSpnDeliveryQuantity.EditValue = .Quantity
                INDSpnProductCost.EditValue = .ProductCost
                INDMeDescription.EditValue = .Description

                INDLciBatchCode.ShowLayout()
                
                CtrPhysicalInventory1.MovementType = InventoryStaticServices.MovementType.OutPut
                CtrPhysicalInventory1.Product = New InventoryProduct With {
                        .Id = _product.Id,
                        .Code = _product.Code,
                        .Name = _product.Name,
                        .ManufacturerDescription = $"{_product.ManufacturerId.Code} - {_product.ManufacturerId.Name}"
                }

                CtrPhysicalInventory1.WareHouseId = SourceWarehouseId
                CtrPhysicalInventory1.FormOwner = Me
                CtrPhysicalInventory1.SetListPhysicalInventory()

                If .ConsignmentTransferDetailBatchSerial IsNot Nothing AndAlso .ConsignmentTransferDetailBatchSerial.Count > 0 Then
                    ' Producto con lotes ya asignados
                    CtrPhysicalInventory1.SetQuantityPhysicalInventory(.ConsignmentTransferDetailBatchSerial.ToList())
                    INDPceBatchSerial.EditValue = String.Format(ResourceManager.GetString("SelectedTotalQuantity", MODULE_NAME), .ConsignmentTransferDetailBatchSerial.Count.ToString(), .ConsignmentTransferDetailBatchSerial.Sum(Function(x) x.Quantity).ToString())
                Else
                    ' Producto importado sin lotes asignados
                    INDPceBatchSerial.EditValue = String.Empty
                End If
                
                _listPhysicalInventory = CtrPhysicalInventory1.GetListPhysicalInventory()
                INDSpnDeliveryQuantity.Properties.ReadOnly = True
                INDSpnDeliveryQuantity.EditValue = .Quantity
            End With

            INDSbAdd.Text = ResourceManager.GetString("Edit")
        Finally
            _isLoading = False
            AsyncLoader(False)
        End Try
    End Function

    ''' <summary>
    ''' Valida los controles para guardar
    ''' </summary>
    ''' <returns></returns>
    Public Overloads Function ValidateControls() As Boolean
        If INDSleTargetWarehouse.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un almacén"
            Return False
        End If

        If _product Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un producto"
            Return False
        End If

        If Not _listPhysicalInventory?.Any() Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione cantidades"
            Return False
        End If

        If Not _validateDetails.ValidateProductOnGrid(_consignmentTransferDetailEdit, INDSleTargetWarehouse.EditValue, _product.Id) Then
            Mensaje(EeventViewerImages.Advertencia) = "El producto seleccionado ya se encuentra agregado"
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Establece quién implementa la interfaz para validar
    ''' </summary>
    ''' <param name="validate"></param>
    Public Sub SetOnValidateProducts(validate As FrmConsignmentTransfer.IValidateDetails)
        _validateDetails = validate
    End Sub

    ''' <summary>
    ''' Asigna los valores a la entidad
    ''' </summary>
    Private Sub AssigningValues()
        If _consignmentTransferDetailEdit Is Nothing Then _consignmentTransferDetailEdit = New ConsignmentTransferDetail()

        With _consignmentTransferDetailEdit
            .WarehouseId = INDSleTargetWarehouse.EditValue
            .WarehouseCodeName = INDSleTargetWarehouse.Text
            .ProductId = _product.Id
            .ProductCodeName = _product.CodeName
            .Quantity = INDSpnDeliveryQuantity.EditValue
            .PhysicalQuantity = CtrPhysicalInventory1.listPhysicalInventory.Sum(Function(m) m.Quantity)
            .ProductCost = INDSpnProductCost.EditValue
            .Description = INDMeDescription.EditValue
            .HandlesBatch = _product.ProductSubGroupId.HandlesBatch
        End With

        For Each py In _listPhysicalInventory
            Dim batch = _consignmentTransferDetailEdit.ConsignmentTransferDetailBatchSerial.FirstOrDefault(Function(m) m.PhysicalInventoryId = py.Id)
            If batch Is Nothing Then
                _consignmentTransferDetailEdit.ConsignmentTransferDetailBatchSerial.Add(New ConsignmentTransferDetailBatchSerial With {
                    .PhysicalInventoryId = py.Id,
                    .Quantity = py.QuantityDeliver
                })
            Else
                batch.Quantity = py.QuantityDeliver
            End If
        Next

        Dim onlyPhysicalId = _listPhysicalInventory.Select(Function(m) m.Id).ToList()
        Dim toRemove = _consignmentTransferDetailEdit.ConsignmentTransferDetailBatchSerial.Where(Function(m) Not onlyPhysicalId.Contains(m.PhysicalInventoryId))

        While toRemove.Any()
            toRemove(0).MarkAsDeleted()
        End While
    End Sub

    ''' <summary>
    ''' Clean Controls
    ''' </summary>
    Public Sub CleanControls()
        _isLoading = False

        CleanControlsProduct()
        INDMeDescription.EditValue = Nothing
        INDPceProducts.Focus()
        _consignmentTransferDetailEdit = Nothing
        INDSbAdd.Text = ResourceManager.GetString("Add")
    End Sub

    ''' <summary>
    ''' Clean controls when warehouse change
    ''' </summary>
    Public Sub CleanControlsProduct()
        CtrPhysicalInventory1.CleanControls()
        INDPceProducts.Text = String.Empty
        INDPceProducts.Properties.ReadOnly = False
        INDPceBatchSerial.EditValue = Nothing
        _product = Nothing
        INDSpnDeliveryQuantity.EditValue = 0
        INDSpnProductCost.EditValue = 0
        _listPhysicalInventory = Nothing
        INDSpnDeliveryQuantity.Properties.ReadOnly = False
    End Sub

    ''' <summary>
    ''' Metodo para establecer los valores a los campos 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetValues()
        If _isLoading Then Exit Sub

        INDSpnDeliveryQuantity.EditValue = _listPhysicalInventory.Sum(Function(m) m.QuantityDeliver)
    End Sub
#End Region

#Region "Handlers"
    ''' <summary>
    ''' Agrega los cambios a la entidad para guardar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAdd_Click(sender As Object, e As EventArgs) Handles INDSbAdd.Click
        If Not ValidateControls() Then Return

        AssigningValues()
        RaiseEvent OnAddConsignmentProduct(Me, _consignmentTransferDetailEdit)
        CleanControls()
    End Sub

    ''' <summary>
    ''' Query popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPceProducts_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceProducts.QueryPopUp
        If CtrProducts1.DataSource Is Nothing Then
            CtrProducts1.SetDataSourceProduct(IdAlmacen:=SourceWarehouseId)
        End If
    End Sub

    ''' <summary>
    ''' Query popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPceBatchSerial_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceBatchSerial.QueryPopUp
        CtrPhysicalInventory1.MovementType = InventoryStaticServices.MovementType.OutPut
        CtrPhysicalInventory1.Product = New InventoryProduct With {
            .Id = _product.Id,
            .Code = _product.Code,
            .Name = _product.Name,
            .ManufacturerDescription = $"{_product.ManufacturerId.Code} - {_product.ManufacturerId.Name}"
        }
        CtrPhysicalInventory1.WareHouseId = SourceWarehouseId
        CtrPhysicalInventory1.FormOwner = Me
        CtrPhysicalInventory1.SetListPhysicalInventory()
    End Sub

#Region "Closed"
    ''' <summary>
    ''' Closed del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPceBatchSerial_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDPceBatchSerial.Closed
        _listPhysicalInventory = CtrPhysicalInventory1.GetListPhysicalInventory()
        If _listPhysicalInventory.Count > 0 Then

            INDPceBatchSerial.EditValue = String.Format(ResourceManager.GetString("SelectedTotalQuantity", MODULE_NAME), _listPhysicalInventory.Count.ToString(), _listPhysicalInventory.Sum(Function(x) x.QuantityDeliver).ToString())
        Else
            INDPceBatchSerial.EditValue = Nothing
        End If

        SetValues()
    End Sub
#End Region

    ''' <summary>
    ''' Seleciona el producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub CtrProducts1_SelectProduct(sender As Object, e As SelectProductEventArgs) Handles CtrProducts1.SelectProduct
        INDPceProducts.Text = e.CodeNameProduct
        INDPceProducts.Focus()
        INDPceProducts.ClosePopup()
        CtrPhysicalInventory1.CleanControls()
        INDPceBatchSerial.EditValue = Nothing
        AsyncLoader(True)
        _product = Await Task.Factory.StartNew(Function() XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of InventoryProductXpo)($"Id = {e.ProductId}"))
        AsyncLoader(False)

        If _product.ProductSubGroupId IsNot Nothing Then
            INDSpnDeliveryQuantity.Properties.ReadOnly = True
            INDPceBatchSerial.Focus()
            INDPceBatchSerial.ShowPopup()
        Else
            Mensaje(EeventViewerImages.Advertencia) = $"El producto {_product.CodeName} no tiene un subgrupo asociado"
            Exit Sub
        End If

        INDSpnProductCost.EditValue = _product.ProductCost
    End Sub

    ''' <summary>
    ''' Query popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleTargetWarehouse_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleTargetWarehouse.QueryPopUp
        If INDSleTargetWarehouse.Properties.DataSource Is Nothing Then
            Dim filter = $"Id <> {SourceWarehouseId} And SupplierId = {SourceSupplierWarehouseId} And Status = True And VirtualStore = 0 And WarehouseConsignment = 1 And CustodyStore = 0 And TransitStore = 0 And Inventory_WarehouseUsers[UserCode = '{indigo.UserIndigo}']"
            INDSleTargetWarehouse.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListXPInstantFeedbackSource(Of WarehouseXpo)(filter)
        End If
    End Sub

    ''' <summary>
    ''' Keydown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopupProductConsignmentTransfer_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Close()
        End If
    End Sub

    ''' <summary>
    ''' Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopupProductConsignmentTransfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = False
        CleanControls()
    End Sub

    ''' <summary>
    ''' Form closing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopupProductConsignmentTransfer_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If _product IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Button click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSpnDeliveryQuantity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSpnDeliveryQuantity.ButtonClick
        Dim control As Windows.Forms.Control = sender
        INDPccMoreInfo.ShowPopup(control.PointToScreen(_mouseLocation))
    End Sub

    ''' <summary>
    ''' Mouse click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSpnDeliveryQuantity_MouseClick(sender As Object, e As Windows.Forms.MouseEventArgs) Handles INDSpnDeliveryQuantity.MouseClick
        _mouseLocation = e.Location
    End Sub

    ''' <summary>
    ''' Validacio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDPccMoreInfo_BeforePopup(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPccMoreInfo.BeforePopup
        If _product Is Nothing OrElse INDSleTargetWarehouse.EditValue Is Nothing Then
            e.Cancel = True
            Return
        End If

        INDPpLoading.BringToFront()
        Using model As New MConsignmentTransfer("")
            Dim res = Await model.GetConsignmentInventoryQuantitiesAsync(INDSleTargetWarehouse.EditValue, _product.Id)
            ShowAndHideMoreInfoControls(res)
            INDPpLoading.SendToBack()
        End Using
    End Sub

    ''' <summary>
    ''' Contiene la lógica de mostrar u ocultar los controles del mas info
    ''' </summary>
    ''' <param name="c"></param>
    Private Sub ShowAndHideMoreInfoControls(c As ConsignmentMovementInventory)
        INDLciOpenQuantity.HideLayout()
        INDLciMaxLimit.HideLayout()
        INDLciIncrementQuantity.HideLayout()
        INDLciAvailableQuantity.HideLayout()

        If c Is Nothing Then
            INDLciOpenQuantity.ShowLayout()
            INDTeOpenQuantity.EditValue = INDSpnDeliveryQuantity.EditValue
        Else
            Dim consumedQuantity = c.MaxLimitQuantity - c.KardexQuantity

            If c.KardexQuantity = c.MaxLimitQuantity Then
                INDLciMaxLimit.ShowLayout()
                INDLciIncrementQuantity.ShowLayout()
                INDLciAvailableQuantity.ShowLayout()

                INDTeMaxLimit.EditValue = c.MaxLimitQuantity
                INDTeIncrementQuantity.EditValue = INDSpnDeliveryQuantity.EditValue
                INDTeAvailableQuantity.EditValue = c.KardexQuantity
            ElseIf c.KardexQuantity < c.MaxLimitQuantity Then
                INDLciMaxLimit.ShowLayout()
                INDLciConsumedQuantity.ShowLayout()
                INDLciRepositionQuantity.ShowLayout()
                INDLciAvailableQuantity.ShowLayout()

                INDTeMaxLimit.EditValue = c.MaxLimitQuantity
                INDTeConsumedQuantity.EditValue = consumedQuantity
                INDTeRepositionQuantity.EditValue = INDSpnDeliveryQuantity.EditValue
                INDTeAvailableQuantity.EditValue = c.KardexQuantity
            ElseIf CInt(INDSpnDeliveryQuantity.EditValue) > c.MaxLimitQuantity Then
                If consumedQuantity > 1 AndAlso CInt(INDSpnDeliveryQuantity.EditValue) > consumedQuantity Then
                    INDLciMaxLimit.ShowLayout()
                    INDLciConsumedQuantity.ShowLayout()
                    INDLciRepositionQuantity.ShowLayout()
                    INDLciAvailableQuantity.ShowLayout()
                    INDLciIncrementQuantity.ShowLayout()

                    INDTeMaxLimit.EditValue = c.MaxLimitQuantity
                    INDTeConsumedQuantity.EditValue = consumedQuantity
                    INDTeRepositionQuantity.EditValue = consumedQuantity
                    INDTeIncrementQuantity.EditValue = CInt(INDSpnDeliveryQuantity.EditValue) - consumedQuantity
                    INDTeAvailableQuantity.EditValue = c.KardexQuantity
                End If
            End If
        End If
    End Sub
#End Region

End Class