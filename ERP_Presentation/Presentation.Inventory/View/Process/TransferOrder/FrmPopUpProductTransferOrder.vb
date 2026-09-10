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
Imports Presentation.Controls
Imports System.Drawing
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Inventory.MVP
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports System.Text
Imports Presentation.Accounting.MVP
Imports Presentation.Controls.MVP

#End Region

Public Class FrmPopUpProductTransferOrder

#Region "EVENTS"
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
    ''' entidad de producto
    ''' </summary>
    ''' <remarks></remarks>
    Dim product As InventoryProduct
    ''' <summary>
    ''' id del almacen 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _wareHouseId As Integer
    ''' <summary>
    ''' id del atc 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _atcId As Integer?
    ''' <summary>
    ''' id del supply
    ''' </summary>
    ''' <remarks></remarks>
    Dim _supplyId As Integer?
    ''' <summary>
    ''' id de la unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Dim _operatingUnitId As Integer
    ''' <summary>
    ''' listado del inventario fisico
    ''' </summary>
    ''' <remarks></remarks>
    Dim listPhysicalInventory As List(Of PhysicalInventory)
    ''' <summary>
    ''' detalle de la orden de traslado
    ''' </summary>
    ''' <remarks></remarks>
    Dim transferOrderDetail As TransferOrderDetail
    ''' <summary>
    ''' bandera para saber que se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Dim _editMode As Boolean
    ''' <summary>
    ''' bandera para saber si se esta importando informacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _importDataMode As Boolean
    ''' <summary>
    ''' listado del detalle de la orden de traslado cuando se importa imformacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listTransferOrderDetailImportInfo As List(Of TransferOrderDetail)
    ''' <summary>
    ''' listado del detalla de la orden de traslado para validar que los productos no se pepitan con la misma fuente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listTransferOrderDetailValidation As List(Of TransferOrderDetail)
    ''' <summary>
    ''' Diccionario para optimizar la consulta de los productos
    ''' </summary>
    ''' <remarks></remarks>
    Dim _productsDictionary As Dictionary(Of Integer, InventoryProduct) = New Dictionary(Of Integer, InventoryProduct)()

#End Region

#Region "PROPERTIES"

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
    ''' <summary>
    ''' propiedad para para pasar el listado del detalle de la remision cuando se importa imformacion
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ListTransferOrderDetailImportInfo As List(Of TransferOrderDetail)
        Set(value As List(Of TransferOrderDetail))
            _listTransferOrderDetailImportInfo = value
        End Set
    End Property
    ''' <summary>
    ''' propiedad para establecer si se va a editar un registro 
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property EditMode As Boolean
        Set(value As Boolean)
            _editMode = value
        End Set
    End Property
    ''' <summary>
    ''' propiedad para establecer si se esta importando informacion
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ImportDataMode As Boolean
        Set(value As Boolean)
            _importDataMode = value
        End Set
    End Property
    ''' <summary>
    ''' propiedad publica para pasar el registro que se va a editar
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property TransferOrderDetailEdit As TransferOrderDetail
        Set(value As TransferOrderDetail)
            transferOrderDetail = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para activar o desactivar controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Private WriteOnly Property ActionsControls As Boolean
        Set(value As Boolean)
            INDPceBatchSerial.Enabled = value
            INDMeDescription.Enabled = value
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
    ''' propiedad para establecer el id del almacen
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property WareHouseId As Integer
        Set(value As Integer)
            _wareHouseId = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad para establecer el id de la unidad operativa
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property operatingUnitId As Integer
        Set(value As Integer)
            _operatingUnitId = value
        End Set
    End Property

    Private _permissionValidateQuantity As Integer
    ''' <summary>
    ''' Permite saber si el usuario tiene permiso para realizar validación de cantidades cuando se importa
    ''' </summary>
    ''' <returns></returns>
    Public Property PermissionValidateQuantity As Integer
        Get
            Return _permissionValidateQuantity
        End Get
        Set(value As Integer)
            _permissionValidateQuantity = value
        End Set
    End Property

#End Region

#Region "METHODS"

    ''' <summary>
    ''' metodo para mostrar los formulario en el evento buttonclik
    ''' </summary>
    ''' <param name="form"></param>
    ''' <remarks></remarks>
    Private Sub OpenFormDialog(form As FormBase)
        form.ViewModeEditHold = True
        form.Size = New Size(800, 730)
        form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        form.MaximizeBox = False
        form.MinimizeBox = False
        Dim transparent = New FrmTransparent(form, False)
        transparent.ShowDialog()
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        CleanControlsLigth()
        listPhysicalInventory = Nothing
        ActionsControls = False
        INDPceProducts.Focus()
        _listTransferOrderDetailImportInfo = Nothing
        BarraBotones.FilterDataSource = Nothing
        _importDataMode = False
        _editMode = False
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsLigth()
        TransferOrderDetailEdit = Nothing
        CtrPhysicalInventory1.CleanControls()
        INDPceProducts.Text = String.Empty
        INDPceProducts.Properties.ReadOnly = False
        INDPceBatchSerial.EditValue = Nothing
        INDspnQuantity.EditValue = 0
        INDspnOutstandingQuantity.EditValue = 0
        INDspnStocksProducts.EditValue = 0
        INDTxtMeasureUnit.EditValue = Nothing
        INDTxtCostProduct.EditValue = Nothing
        INDMeDescription.EditValue = Nothing
        INDLciMedicine.HideControl(True)
        INDLciSupply.HideControl(True)
        product = Nothing
    End Sub

    ''' <summary>
    ''' metodo para cargar los controles con la informacion requerida
    ''' </summary>
    ''' <param name="transferOrderDetailTmp"></param>
    ''' <remarks></remarks>
    Private Sub LoadControls(Optional transferOrderDetailTmp As TransferOrderDetail = Nothing)
        If _editMode Then
            INDBtnAddProduct.Text = ResourceManager.GetString("Edit")
        End If

        If _importDataMode Then
            CleanControlsLigth()
            transferOrderDetail = transferOrderDetailTmp
        End If
        If transferOrderDetail.InventoryRequestDetailId IsNot Nothing Or transferOrderDetail.InventoryRequestDetailOtherId IsNot Nothing Then
            INDLciOutstandingQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDspnOutstandingQuantity.EditValue = transferOrderDetail.QuantityImport
        End If

        With transferOrderDetail
            _atcId = Nothing
            _supplyId = Nothing

            INDspnQuantity.EditValue = .Quantity

            If .ProductId > 0 Then
                Using model As New MInventoryProduct(Me.Tag)
                    If _productsDictionary.ContainsKey(.ProductId) Then
                        product = _productsDictionary(.ProductId)
                    Else
                        product = model.GetInventoryProductByIdSimple(.ProductId)
                        _productsDictionary.Add(.ProductId, product)
                    End If
                    INDPceProducts.Text = product.Code + " - " + product.Name
                    INDPceProducts.Properties.ReadOnly = True
                    INDMeDescription.Text = .Description
                    INDTxtMeasureUnit.EditValue = product.MeasureUnitDescription
                    INDTxtCostProduct.EditValue = product.ProductCost
                    If _importDataMode Then
                        .DescriptionProduct = product.Code + " - " + product.Name
                        .InventoryQuantity = CalculatedQuantityInventory()
                    End If
                    INDspnStocksProducts.EditValue = .InventoryQuantity
                End Using
                If product.ProductSubGroup IsNot Nothing Then
                    If product.ProductSubGroup.HandlesBatch Then
                        INDspnQuantity.Properties.ReadOnly = True
                        INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDPceBatchSerial.Focus()
                    Else
                        Using modelPhysical As New MCtrPhysicalInventory(Me.Tag)
                            listPhysicalInventory = modelPhysical.GetListPhysicalInventory(product.Id, _wareHouseId)
                        End Using
                        INDspnQuantity.Properties.ReadOnly = False
                        INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDspnQuantity.Focus()
                    End If
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "El producto " + String.Concat(product.Code, " - ", product.Name) + " no tiene un subgrupo asociado"
                    CleanControls()
                End If
                If INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    If .TransferOrderDetailBatchSerial.Count = 0 Then
                        INDPceBatchSerial.EditValue = String.Empty
                    Else
                        INDPceBatchSerial.EditValue = String.Format(ResourceManager.GetString("SelectedTotalQuantity", MODULE_NAME), .TransferOrderDetailBatchSerial.Count.ToString(), .TransferOrderDetailBatchSerial.Sum(Function(x) x.Quantity).ToString())
                    End If
                    CtrPhysicalInventory1.MovementType = InventoryStaticServices.MovementType.OutPut
                    CtrPhysicalInventory1.Product = product
                    CtrPhysicalInventory1.WareHouseId = _wareHouseId
                    CtrPhysicalInventory1.FormOwner = Me
                    If _importDataMode Then
                        CtrPhysicalInventory1.CleanControls()
                    End If
                    CtrPhysicalInventory1.SetListPhysicalInventory()
                    CtrPhysicalInventory1.SetQuantityPhysicalInventory(.TransferOrderDetailBatchSerial.ToList())
                End If
            End If

            CtrProducts1.DataSource = Nothing
            If .ComponentType = 1 Then 'ComponentType = Medicamento
                Using model As New MATC(Me.Tag)
                    Dim atc = model.GetATCByIdSimple(.ItemId)
                    _atcId = atc.Id
                    INDSleMedicine.EditValue = atc.Id
                    INDSleMedicine.Properties.NullText = atc.Code + " - " + atc.Name
                    If transferOrderDetail.ProductId = Nothing Then
                        INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    End If
                    INDLciMedicine.HideControl(False)
                    INDPceProducts.Properties.ReadOnly = False
                    CtrProducts1.SetDataSourceProduct(ATCId:= .ItemId) 'Enviar id atc
                End Using
            ElseIf .ComponentType = 2 Then 'ComponentType = Insumo
                Using model As New MInventorySupplie(Me.Tag)
                    Dim supplie = model.GetSupplieByIdSimple(.ItemId)
                    _supplyId = supplie.Id
                    INDSleSupply.EditValue = supplie.Id
                    INDSleSupply.Properties.NullText = supplie.Code + " - " + supplie.SupplieName
                    If transferOrderDetail.ProductId = Nothing Then
                        INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    End If
                    INDLciSupply.HideControl(False)
                    INDPceProducts.Properties.ReadOnly = False
                    CtrProducts1.SetDataSourceProduct(SupplyId:= .ItemId) 'Enviar id del supply
                End Using
            ElseIf .ComponentType = 3 Then 'ComponentType = Producto
                CtrProducts1.SetDataSourceProduct()
            End If

            INDMeDescription.Text = transferOrderDetail.Description
            If transferOrderDetail.ProductId <> Nothing Then
                Using model As New MInventoryProduct(Me.Tag)
                    product = model.GetInventoryProductByIdSimple(.ProductId)
                    INDPceProducts.Text = product.Code + " - " + product.Name
                End Using
            End If

            'INDPceProducts.Text = transferOrderDetail.ProductId
            ActionsControls = True
            INDPceProducts.Focus()
        End With
    End Sub

    ''' <summary>
    ''' valida los controles del formualario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim errors As New StringBuilder
        If Not _importDataMode Then
            If product Is Nothing Then
                errors.AppendLine(INDLyPceProducts.Text + ResourceManager.GetString("Empty"))
            End If
            If Not _editMode AndAlso product IsNot Nothing Then
                If _listTransferOrderDetailValidation IsNot Nothing AndAlso _listTransferOrderDetailValidation.Count > 0 Then
                    Dim TransferOrderDetailTmp = _listTransferOrderDetailValidation.Find(Function(x) x.ProductId = product.Id And x.InventoryRequestDetailId Is Nothing)
                    If TransferOrderDetailTmp IsNot Nothing Then
                        errors.AppendLine(String.Format(ResourceManager.GetString("SourceProductNone", MODULE_NAME), TransferOrderDetailTmp.DescriptionProduct))
                    End If
                End If
            End If
            If INDspnQuantity.EditValue = 0 Then
                errors.AppendLine(INDLciQuantity.Text + ResourceManager.GetString("Empty"))
            End If
            If transferOrderDetail IsNot Nothing AndAlso transferOrderDetail.QuantityImport > 0 AndAlso _permissionValidateQuantity = 0 Then
                If listPhysicalInventory.Sum(Function(x) x.QuantityDeliver) > transferOrderDetail.QuantityImport Then
                    errors.AppendLine(String.Format(ResourceManager.GetString("QuantityOverFlow", MODULE_NAME), transferOrderDetail.DescriptionProduct, "Solicitada"))
                End If
            End If
            If INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If Not _editMode Then
                    If listPhysicalInventory Is Nothing Then
                        errors.AppendLine(INDLciBatchSerial.Text + ResourceManager.GetString("Empty"))
                    End If
                    If listPhysicalInventory IsNot Nothing AndAlso listPhysicalInventory.Count = 0 Then
                        errors.AppendLine(INDLciBatchSerial.Text + ResourceManager.GetString("Empty"))
                    End If
                Else
                    If listPhysicalInventory IsNot Nothing AndAlso listPhysicalInventory.Count = 0 Then
                        errors.AppendLine(INDLciBatchSerial.Text + ResourceManager.GetString("Empty"))
                    End If
                End If
            End If
            If INDspnStocksProducts.EditValue < INDspnQuantity.EditValue Then
                errors.AppendLine(String.Format(ResourceManager.GetString("QuantityRequest", MODULE_NAME)))
            End If
        Else
            If INDspnQuantity.EditValue = 0 Then
                errors.AppendLine(INDLciQuantity.Text + ResourceManager.GetString("Empty"))
            End If

            'Listado que se arma con los items que no arrojaron errores
            Dim listTempTransferOrderDetail As New List(Of TransferOrderDetail)
            'recorro todos los items que se importaron para hacer las validaciones
            For Each item In _listTransferOrderDetailImportInfo
                'Variable para saber si se agrega el item al listado que se retorna con los items que no arrojaron errores
                Dim aggregatedItem As Boolean = True

                Using model As New MInventoryProduct(Me.Tag)
                    If item.ProductId = 0 Then
                        errors.AppendLine($"No se ha seleccionado un producto para el item {_listTransferOrderDetailImportInfo.IndexOf(item) + 1}")
                        Continue For
                    End If
                    product = model.GetInventoryProductByIdSimple(item.ProductId)
                End Using
                Using model As New MSubGroup(Me.Tag)
                    If product.ProductSubGroup Is Nothing Then
                        aggregatedItem = False
                        errors.AppendLine(String.Format("El producto del item {0} no tiene un SubGrupo asociado.", (_listTransferOrderDetailImportInfo.IndexOf(item) + 1).ToString()))
                    Else
                        If product.ProductSubGroup.HandlesBatch And item.TransferOrderDetailBatchSerial.Count = 0 Then
                            aggregatedItem = False
                            'valido que si el producto maneja lote se encuentre agregado minimo un lote en TransferOrderBatchSerial
                            errors.AppendLine(String.Format(ResourceManager.GetString("HandlesBatch", MODULE_NAME), (_listTransferOrderDetailImportInfo.IndexOf(item) + 1).ToString()))
                        Else
                            If listPhysicalInventory Is Nothing OrElse listPhysicalInventory.Count = 0 Then
                                aggregatedItem = False
                                errors.AppendLine("El producto " + product.Name + " no se encontro dentro del inventario fisico")
                            End If
                            If item.Quantity = 0 Then
                                aggregatedItem = False
                                errors.AppendLine(String.Format(ResourceManager.GetString("QuantityZero", MODULE_NAME), (_listTransferOrderDetailImportInfo.IndexOf(item) + 1).ToString()))
                            End If
                        End If
                        If INDspnStocksProducts.EditValue < INDspnQuantity.EditValue Then
                            aggregatedItem = False
                            errors.AppendLine(String.Format(ResourceManager.GetString("QuantityRequest", MODULE_NAME)))
                        End If
                    End If
                End Using

                'Si el permiso 'Quitar Validación Cantidades Importadas' esta en 'Si' no se realiza la validación
                If item.Quantity > 0 AndAlso _permissionValidateQuantity = 0 Then
                    If item.Quantity > item.QuantityImport Then
                        errors.AppendLine(String.Format("La cantidad pendiente del producto {0} en la solicitud del item {1} es menor a la cantidad que se va agregar", item.DescriptionProduct, (_listTransferOrderDetailImportInfo.IndexOf(item) + 1)))
                        aggregatedItem = False
                    End If
                End If

                If aggregatedItem Then
                    listTempTransferOrderDetail.Add(item)
                End If
            Next
            If listTempTransferOrderDetail IsNot Nothing AndAlso listTempTransferOrderDetail.Count > 0 Then
                _listTransferOrderDetailImportInfo = listTempTransferOrderDetail

                If errors.ToString().Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
                    errors = New StringBuilder
                End If
            End If
        End If

        Return errors.ToString()
    End Function

    Private Sub SetValues()
        If Not _editMode Then
            transferOrderDetail = New TransferOrderDetail
        End If

        With transferOrderDetail
            .ProductId = product.Id
            .Description = INDMeDescription.Text
            .DescriptionProduct = product.Code + " - " + product.Name
            .Value = product.ProductCost
            .ConsumptionUnit = product.PackingUnitDescription
            .CostProduct = product.ProductCost
            .Quantity = INDspnQuantity.EditValue
            .InventoryQuantity = INDspnStocksProducts.EditValue
            If INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                'si se esta editando el registro elimino los detalles de TransferOrderDetailBatchSerial 
                If _editMode Then
                    If listPhysicalInventory IsNot Nothing AndAlso listPhysicalInventory.Count > 0 Then
                        While .TransferOrderDetailBatchSerial.Count > 0
                            If .TransferOrderDetailBatchSerial(0).Id > 0 Then
                                .TransferOrderDetailBatchSerial(0).MarkAsDeleted()
                            Else
                                .TransferOrderDetailBatchSerial.Remove(.TransferOrderDetailBatchSerial(0))
                            End If
                        End While
                    End If
                End If
                If listPhysicalInventory IsNot Nothing Then
                    For Each item In listPhysicalInventory
                        Dim transferOrderDetailBatchSerial As New TransferOrderDetailBatchSerial
                        transferOrderDetailBatchSerial.PhysicalInventoryId = item.Id
                        transferOrderDetailBatchSerial.Quantity = item.QuantityDeliver
                        transferOrderDetailBatchSerial.OutstandingQuantity = item.QuantityDeliver
                        transferOrderDetailBatchSerial.CodeNameProduct = .DescriptionProduct
                        transferOrderDetailBatchSerial.CodeBatchSerial = item.CodeNameBatchSerial
                        transferOrderDetailBatchSerial.CodeNameWarehouse = item.CodeNameWarehouse
                        .TransferOrderDetailBatchSerial.Add(transferOrderDetailBatchSerial)
                    Next
                End If
            Else
                ' elimino todos los registros de transferOrderDetailBatchSerial
                While .TransferOrderDetailBatchSerial.Count > 0
                    If .TransferOrderDetailBatchSerial(0).Id > 0 Then
                        .TransferOrderDetailBatchSerial(0).MarkAsDeleted()
                    Else
                        .TransferOrderDetailBatchSerial.Remove(.TransferOrderDetailBatchSerial(0))
                    End If
                End While
                Dim transferOrderDetailBatchSerial As New TransferOrderDetailBatchSerial
                transferOrderDetailBatchSerial.Quantity = INDspnQuantity.EditValue
                transferOrderDetailBatchSerial.OutstandingQuantity = INDspnQuantity.EditValue
                transferOrderDetailBatchSerial.CodeNameProduct = .DescriptionProduct
                transferOrderDetailBatchSerial.PhysicalInventoryId = listPhysicalInventory(0).Id
                .TransferOrderDetailBatchSerial.Add(transferOrderDetailBatchSerial)
            End If
        End With
    End Sub

    ''' <summary>
    ''' Consultar la cantidad disponible de productos en el almacen de origen
    ''' </summary>
    ''' <remarks></remarks>
    Public Function CalculatedQuantityInventory() As Integer

        Using modelPhysical As New MCtrPhysicalInventory(Me.Tag)
            listPhysicalInventory = modelPhysical.GetListPhysicalInventory(product.Id, _wareHouseId)
        End Using
        Dim QuantityAvailable = listPhysicalInventory.Sum(Function(x)
                                                              Return x.Quantity
                                                          End Function)
        Return QuantityAvailable

    End Function

    ''' <summary>
    ''' Lanza la consulta del producto para poderlo seleccionar
    ''' sin necesidad de desplegar el control de producto
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function AssignProductWithCode() As Task
        If INDPceProducts.Text.Trim <> String.Empty AndAlso Not _importDataMode Then
            'Separo el string escrito en el control de producto
            Dim arrayCodeProduct As String() = INDPceProducts.Text.Split(" - ")
            'Capturo el codigo del producto
            Dim codeProduct As String = arrayCodeProduct(0).Trim


            Using model As New MInventoryProduct(Me.Tag)
                'Consulto el producto para armar el objeto SelectProductEventArgs
                Dim resultProduct = Await model.GetInventoryProduct(codeProduct)
                If Not resultProduct.StateResult Then
                    Mensaje(EeventViewerImages.Advertencia) = resultProduct.MessageResult(0)
                    CleanControls()
                    Exit Function
                End If
                'Valido que exista el producto
                If resultProduct.ObjectEmbbeded IsNot Nothing AndAlso resultProduct.ObjectEmbbeded.Id = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format("El producto con código {0} no existe.", codeProduct)
                    CleanControls()
                    Exit Function
                End If
                'Valido que el producto no este inactivo
                If Not resultProduct.ObjectEmbbeded.Status Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format("El producto " + resultProduct.ObjectEmbbeded.Code + " - " + resultProduct.ObjectEmbbeded.Name + " está inactivo")
                    CleanControls()
                    Exit Function
                End If
                'Instancio el objeto SelectProductEventArgs
                Dim obj As New SelectProductEventArgs
                obj.ProductId = resultProduct.ObjectEmbbeded.Id
                obj.CodeNameProduct = resultProduct.ObjectEmbbeded.Code + " - " + resultProduct.ObjectEmbbeded.Name
                'Invoco la funcion que realiza el resto de la logica
                CtrProducts1_SelectProduct(Nothing, obj)
            End Using
        ElseIf INDPceProducts.Text.Trim = String.Empty Then
            If product IsNot Nothing Then
                INDPceProducts.Text = product.Code + " - " + product.Description
            End If
        End If
    End Function

#End Region

#Region "Handles"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        product = Nothing
        _wareHouseId = Nothing
        _operatingUnitId = Nothing
        listPhysicalInventory = Nothing
        transferOrderDetail = Nothing
        _editMode = Nothing
        _importDataMode = Nothing
        _listTransferOrderDetailImportInfo = Nothing
        _listTransferOrderDetailValidation = Nothing
    End Sub

    ''' <summary>
    ''' se dispara al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopUpProductTransferOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = False
        If _editMode Then
            LoadControls()
        ElseIf _importDataMode Then
            'si se esta importando informacion
            If _listTransferOrderDetailImportInfo.Count > 1 Then
                BarraBotones.FilterDataSource = _listTransferOrderDetailImportInfo
            Else
                LoadControls(_listTransferOrderDetailImportInfo(0))
            End If
        Else
            CleanControls()
        End If
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = New Globalization.CultureInfo(indigo.CurrencyISO4217.GetCultureId()).NumberFormat
        changeNumericFormatByCurrency(_culture.NumberFormat)

    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopUpProductTransferOrder_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If product Is Nothing Then
            INDPceProducts.Focus()
        End If
        If _importDataMode Then
            If INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If INDPceBatchSerial.EditValue Is String.Empty Then
                    INDPceBatchSerial.Focus()
                End If
            Else
                If INDspnQuantity.EditValue = 0 Then
                    INDspnQuantity.Focus()
                End If
            End If
        End If
        If _editMode Then
            INDPceProducts.Focus()
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' se dispara al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopUpProductTransferOrder_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If product IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "SelectProduct"

    Private Sub CtrProducts1_SelectProduct(sender As Object, e As SelectProductEventArgs) Handles CtrProducts1.SelectProduct
        INDPceProducts.Text = e.CodeNameProduct
        If transferOrderDetail IsNot Nothing Then
            transferOrderDetail.ProductId = e.ProductId
        End If
        INDPceProducts.Focus()
        INDPceProducts.ClosePopup()
        CtrPhysicalInventory1.CleanControls()
        INDPceBatchSerial.EditValue = Nothing
        Using model As New MInventoryProduct(Me.Tag)
            product = model.GetInventoryProductByIdSimple(e.ProductId)
        End Using
        If _importDataMode Then
            transferOrderDetail.DescriptionProduct = product.Code + " - " + product.Name
            transferOrderDetail.InventoryQuantity = CalculatedQuantityInventory()
        End If
        If product.ProductSubGroup IsNot Nothing Then
            If product.ProductSubGroup.HandlesBatch Then
                INDspnQuantity.Properties.ReadOnly = True
                INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDPceBatchSerial.Focus()
                If _editMode Then
                    'instancio el listado para poder hacer la validacion de los lotes cuando se esta editando y saber si esta sin registros
                    listPhysicalInventory = New List(Of PhysicalInventory)
                End If
                INDPceBatchSerial.Focus()
                INDPceBatchSerial.ShowPopup()
            Else
                INDspnQuantity.Properties.ReadOnly = False
                INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDspnQuantity.Focus()
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = "El producto " + String.Concat(product.Code, " - ", product.Name) + " no tiene un subgrupo asociado"
            CleanControls()
        End If
        ActionsControls = True
        INDspnStocksProducts.EditValue = CalculatedQuantityInventory()
        INDTxtMeasureUnit.EditValue = product.MeasureUnitDescription
        INDTxtCostProduct.EditValue = product.ProductCost
    End Sub

#End Region

#Region "ButtonClick"
    Private Sub INDPceProducts_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDPceProducts.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmProducts
                OpenFormDialog(form)
            End Using
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDPceProducts_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceProducts.QueryPopUp
        If transferOrderDetail Is Nothing Then
            CtrProducts1.SetDataSourceProduct()
        End If
    End Sub

    Private Sub INDPceBatchSerial_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceBatchSerial.QueryPopUp
        If _importDataMode Then
            CtrPhysicalInventory1.MaxQuantity = INDspnOutstandingQuantity.EditValue
        End If
        CtrPhysicalInventory1.MovementType = InventoryStaticServices.MovementType.OutPut
        CtrPhysicalInventory1.Product = product
        CtrPhysicalInventory1.WareHouseId = _wareHouseId
        CtrPhysicalInventory1.FormOwner = Me
        CtrPhysicalInventory1.SetListPhysicalInventory()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' se dispara al darle enter en el control de descripcion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDMeDescription_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDMeDescription.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDBtnAddProduct.Focus()
        End If
    End Sub

    ''' <summary>
    ''' se dispara al darle escape en el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopUpProductTransferOrder_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' se dispara al darle enter en el control de productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDPceProducts_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDPceProducts.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Await AssignProductWithCode()
        End If
    End Sub

#End Region

#Region "RecordNavigationChangeEvent"
    ''' <summary>
    ''' Se dispara cuando navega entre items importados
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_RecordNavigationChangeEvent(Record As Object) Handles BarraBotones.RecordNavigationChangeEvent
        LoadControls(Record)
        INDPceBatchSerial.Focus()
    End Sub

#End Region

#Region "Closed"
    Private Sub INDPceBatchSerial_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDPceBatchSerial.Closed
        listPhysicalInventory = CtrPhysicalInventory1.GetListPhysicalInventory()
        If transferOrderDetail IsNot Nothing AndAlso _importDataMode Then
            If listPhysicalInventory IsNot Nothing Then
                transferOrderDetail.TransferOrderDetailBatchSerial.Clear()
                For Each item In listPhysicalInventory
                    Dim TransferOrderDetailBatchSerial As New TransferOrderDetailBatchSerial
                    TransferOrderDetailBatchSerial.PhysicalInventoryId = item.Id
                    TransferOrderDetailBatchSerial.Quantity = item.QuantityDeliver
                    TransferOrderDetailBatchSerial.OutstandingQuantity = item.QuantityDeliver
                    TransferOrderDetailBatchSerial.CodeNameProduct = transferOrderDetail.DescriptionProduct
                    TransferOrderDetailBatchSerial.CodeBatchSerial = item.CodeNameBatchSerial
                    TransferOrderDetailBatchSerial.CodeNameWarehouse = item.CodeNameWarehouse
                    transferOrderDetail.TransferOrderDetailBatchSerial.Add(TransferOrderDetailBatchSerial)
                Next
            Else
                Dim TransferOrderDetailBatchSerial As New TransferOrderDetailBatchSerial
                TransferOrderDetailBatchSerial.Quantity = INDspnQuantity.EditValue
                TransferOrderDetailBatchSerial.OutstandingQuantity = INDspnQuantity.EditValue
                transferOrderDetail.TransferOrderDetailBatchSerial.Add(TransferOrderDetailBatchSerial)
            End If
        End If
        INDspnQuantity.EditValue = listPhysicalInventory.Sum(Function(x) x.QuantityDeliver)
        If listPhysicalInventory.Count > 0 Then
            INDPceBatchSerial.EditValue = String.Format(ResourceManager.GetString("SelectedTotalQuantity", MODULE_NAME), listPhysicalInventory.Count.ToString(), listPhysicalInventory.Sum(Function(x) x.QuantityDeliver).ToString())
        Else
            INDPceBatchSerial.EditValue = Nothing
        End If
        indspnquantity_editvaluechanged(Nothing, Nothing)
    End Sub
#End Region

#Region "Click"

    Private Async Sub INDBtnAddProduct_Click(sender As Object, e As EventArgs) Handles INDBtnAddProduct.Click
        Try
            INDBtnAddProduct.Enabled = False
            Dim errors = ValidateControlsPopup()
            If errors.Length > 0 Then
                INDBtnAddProduct.Enabled = True
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
            'si no se esta importando informacion asigno valores
            If Not _importDataMode Then
                SetValues()
                Using modelSettings As New MSettingInventory(Me.Tag)
                    Dim resulValidateStock = Await modelSettings.ValidateStock(transferOrderDetail.ProductId, _operatingUnitId, _wareHouseId, transferOrderDetail.Quantity, InventoryStaticServices.MovementType.OutPut)
                    If Not resulValidateStock.StateResult Then
                        Mensaje(EeventViewerImages.Advertencia) = resulValidateStock.Message
                    End If
                End Using
            End If
            If _listTransferOrderDetailValidation Is Nothing Then
                _listTransferOrderDetailValidation = New List(Of TransferOrderDetail)
            End If
            Dim args As New AddProductTransferOrderDetailEventArgs
            If _editMode Then
                args.ItemTransferOrderDetail = transferOrderDetail
                args.EditMode = True
            Else
                args.ItemTransferOrderDetail = transferOrderDetail
                _listTransferOrderDetailValidation.Add(transferOrderDetail)
            End If
            If _importDataMode Then
                args.ImportDataMode = True
                args.ListTransferOrderDetail = _listTransferOrderDetailImportInfo
                _listTransferOrderDetailValidation.Add(transferOrderDetail)
            End If
            RaiseEvent AddTransferOrderDetail(Nothing, args)
            INDBtnAddProduct.Enabled = True
            If _importDataMode OrElse _editMode Then
                product = Nothing
                Me.Close()
            Else
                CleanControls()
            End If
        Catch ex As Exception
            INDBtnAddProduct.Enabled = True
            Throw ex
        End Try

    End Sub

#End Region

#Region "EditValueChanged"
    Private Sub indspnquantity_editvaluechanged(sender As Object, e As EventArgs) Handles INDspnQuantity.EditValueChanged
        If transferOrderDetail IsNot Nothing Then
            If transferOrderDetail.InventoryRequestDetailId IsNot Nothing OrElse transferOrderDetail.InventoryRequestDetailOtherId IsNot Nothing Then
                transferOrderDetail.Quantity = INDspnQuantity.EditValue
            End If
        Else
            If INDspnQuantity.EditValue IsNot Nothing Then
                If INDspnQuantity.EditValue > INDspnStocksProducts.EditValue Then
                    INDspnQuantity.EditValue = INDspnStocksProducts.EditValue
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("QuantityRequest", "Inventory"))
                End If
            End If
        End If

    End Sub

#End Region

#End Region

#Region "BAR BUTTONS"
    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub

    Private Sub INDMeDescription_EditValueChanged(sender As Object, e As EventArgs) Handles INDMeDescription.EditValueChanged
        If transferOrderDetail IsNot Nothing Then
            transferOrderDetail.Description = INDMeDescription.Text
        End If
    End Sub
#End Region

End Class