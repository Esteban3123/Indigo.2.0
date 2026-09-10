'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Johan Sebastian Cuellar Esquivel
' Created          : 25-03-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Controls.FormBase
Imports Presentation.Controls.MVP
Imports Presentation.Inventory.MVP
#End Region

Public Class PopupCUMTransferOrder


#Region "EVENTS"

    Public Event GetCUM(senser As Object, e As GetCUMEventArgs)

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

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
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Protected indigo As SessionValues

    ''' <summary>
    ''' entidad de producto
    ''' </summary>
    ''' <remarks></remarks>
    Dim product As InventoryProduct

    ''' <summary>
    ''' codigo del producto de crystal
    ''' </summary>
    ''' <remarks></remarks>
    Dim _productCode As String
    ''' <summary>
    ''' cantidad que se va a entregar
    ''' </summary>
    ''' <remarks></remarks>
    Dim _quantityDeliver As Integer
    ''' <summary>
    ''' listado del inventario fisico 
    ''' </summary>
    ''' <remarks></remarks>
    Dim listPhysicalInventoryCrystalProduct As List(Of PhysicalInventory)

    ''' <summary>
    ''' listado del inventario fisico por el codigo atc
    ''' </summary>
    ''' <remarks>HRR PBI3410</remarks>
    Dim listPhysicalInventoryCustodyProduct As List(Of PhysicalInventoryCustody)

    ''' <summary>
    ''' 0 = No hay integración, 1 = Hay integración con Heon, 2 = Hay integración entre Medilaser y FarmaQx
    ''' </summary>
    Private DispensingIntegration As Integer

    ''' <summary>
    ''' Cantidad que viene desde el request de HEON
    ''' </summary>
    Public QuantityAsignedByHEON As Integer = 0

    ''' <summary>
    ''' Detalle de la solicitud de HEON para asignar los valores del tableLayout
    ''' </summary>
    Public solicitudDetalle As SolicitudDetalleObject

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>HRR PBI3410</remarks>
    Public Custody As Boolean

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>HRR PBI3410</remarks>
    Public AdmissionNumber As String

    ''' <summary>
    ''' Fecha actual 
    ''' </summary>
    Dim _currentDate As Date

    ''' <summary>
    ''' parámetros de inventario
    ''' </summary>
    Dim _settingInventory As SettingInventory

    ''' <summary>
    ''' bandera para saber si se esta importando informacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _importDataMode As Boolean

    ''' <summary>
    ''' id de la unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Dim _operatingUnitId As Integer

    ''' <summary>
    ''' id del almacen 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _wareHouseId As Integer?

    ''' <summary>
    ''' listado del detalle de la orden de traslado cuando se importa informacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listTransferOrderDetailImportInfo As List(Of TransferOrderDetail)

    ''' <summary>
    ''' listado del detalla de la orden de traslado para validar que los productos no se pepitan con la misma fuente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listTransferOrderDetailValidation As List(Of TransferOrderDetail)

    ''' <summary>
    ''' detalle de la orden de traslado
    ''' </summary>
    ''' <remarks></remarks>
    Public transferOrderDetail As TransferOrderDetail

    ''' <summary>
    ''' bandera para saber que se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Dim _editMode As Boolean

    ''' <summary>
    ''' listado del inventario fisico
    ''' </summary>
    ''' <remarks></remarks>
    Dim listPhysicalInventory As List(Of PhysicalInventory)

    ''' <summary>
    ''' listado de la orden de compra
    ''' </summary>
    ''' <remarks></remarks>
    Dim listInventoryRequestDetailOther As New List(Of ViewListRequestDetailImport)

    ''' <summary>
    ''' Listado donde se agrega physical
    ''' </summary>
    Dim listPhysicalInventoryDetail As New List(Of PhysicalInventory)

    ''' <summary>
    ''' Lista temp
    ''' </summary>
    Dim ListTransferOrderDetailImportarInfo = New List(Of TransferOrderDetail)

#End Region

#Region "PROPERTIES"

    Public Property operatingUnitId As Integer
        Get
            Return _operatingUnitId
        End Get
        Set(value As Integer)
            _operatingUnitId = value
        End Set
    End Property
    Public WriteOnly Property WareHouseId As Integer
        Set(value As Integer)
            _wareHouseId = value
        End Set
    End Property

    Public Property transferOrderDetailProperty As TransferOrderDetail
        Get
            Return transferOrderDetail
        End Get
        Set(value As TransferOrderDetail)
            transferOrderDetail = value
        End Set
    End Property

    Public Property ListTransferOrderDetailImportInfo As List(Of TransferOrderDetail)
        Get
            Return _listTransferOrderDetailImportInfo
        End Get
        Set(value As List(Of TransferOrderDetail))
            _listTransferOrderDetailImportInfo = value
        End Set
    End Property

    Public Property ListTransferOrderDetailValidation As List(Of TransferOrderDetail)
        Get
            Return _listTransferOrderDetailValidation
        End Get
        Set(value As List(Of TransferOrderDetail))
            If value IsNot Nothing Then
                _listTransferOrderDetailValidation = New List(Of TransferOrderDetail)(value.ToArray())
            End If
        End Set
    End Property

    Public WriteOnly Property ImportDataMode As Boolean
        Set(value As Boolean)
            _importDataMode = value
        End Set
    End Property

    Dim _careGroupId As Integer
    Public WriteOnly Property CareGroupId As Integer
        Set(value As Integer)
            _careGroupId = value
        End Set
    End Property

    Dim _productType As Integer
    Public WriteOnly Property ProductType As Integer
        Set(value As Integer)
            _productType = value
        End Set
    End Property

    Public WriteOnly Property QuantityDeliver As Integer
        Set(value As Integer)
            _quantityDeliver = value
            INDLblQuantity.Text = "Cantidad Solicitada: " + value.ToString()
        End Set
    End Property


    ''' <summary>
    ''' listado de el inventario fisico que se pasa desde el formulario principal
    ''' </summary>
    ''' <remarks>HRR PBI3410</remarks>
    Dim _listPhysicalInventoryCustody As List(Of PhysicalInventoryCustody)
    Public WriteOnly Property ListPhysicalInventoryCustody As List(Of PhysicalInventoryCustody)
        Set(value As List(Of PhysicalInventoryCustody))
            _listPhysicalInventoryCustody = value
        End Set
    End Property

    ''' <summary>
    ''' Validar productos próximos a vencer
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ValidateBatchSerialExpiredDate As Boolean
        Get
            Return If(Me._settingInventory Is Nothing, False, Me._settingInventory.ValidateBatchSerialExpiredDate)
        End Get
    End Property

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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

#Region "HANDLES"

#Region "Load"
    Private Async Sub PopupCUMTransferOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDGvCUM.OptionsView.ShowAutoFilterRow = False
        Await LoadParameters()
    End Sub

    Private Async Function LoadParameters() As Task
        Using Model As New MSettingInventory(Me.Tag)
            'parámetros de inventario
            Dim result = Await Model.GetInventorySettingsRegister(operatingUnitId)
            If result.StateResult = False Then
                Mensaje(EeventViewerImages.Advertencia) = Infrastructure.CrossCutting.Resources.ResourceManager.GetString("SettingParameter", NAME_MODULE)
                Exit Function
            End If
            _settingInventory = result.ObjectEmbbeded
            If _settingInventory Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = Infrastructure.CrossCutting.Resources.ResourceManager.GetString("SettingParameter", NAME_MODULE)
                Exit Function
            End If
        End Using
    End Function

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        product = Nothing
        _wareHouseId = Nothing
        _operatingUnitId = Nothing
        listPhysicalInventory = Nothing
        ListTransferOrderDetailImportarInfo = Nothing
        listPhysicalInventoryDetail = Nothing
        transferOrderDetail = Nothing
        _editMode = Nothing
        _importDataMode = Nothing
        _listTransferOrderDetailImportInfo = Nothing
        _listPhysicalInventoryCustody = Nothing
        ListTransferOrderDetailValidation = Nothing
        ListTransferOrderDetailImportInfo = Nothing
        _listTransferOrderDetailValidation = Nothing
    End Sub

    Private Sub LoadControls(Optional transferOrderDetailTmp As TransferOrderDetail = Nothing)
        Me.indigo = SessionValues.Instance

        Dim listInventoryRequestDetailOtherXpo As XPCollection
        Dim Row As Integer
        'se trae a la cabecera
        With transferOrderDetail
            Using model As New MInventoryProduct(Me.Tag)
                product = model.GetInventoryProductByIdSimple(.ProductId)
                Row = If(.InventoryRequestDetailId IsNot Nothing, .InventoryRequestDetailId, .InventoryRequestDetailOtherId)
                Dim filter() As Object = {Row}
                listInventoryRequestDetailOtherXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListViewRequestDetailEntityId(CInt(filter.ElementAt(0)))
                For Each itemXpo As InventoryRepository.View.ViewListRequestDetailImportXpo In listInventoryRequestDetailOtherXpo
                    INDLcgMain.Text = itemXpo.SourceCodeName
                Next
                .InventoryQuantity = CalculatedQuantityInventory() 'cantidad disponible
                If _importDataMode = True Then
                    QuantityDeliver = .QuantityImport
                    .DescriptionProduct = product.Code + " - " + product.Name
                Else
                    QuantityDeliver = .Quantity
                End If
            End Using

            If ListTransferOrderDetailImportInfo IsNot Nothing Then
                Using modelPhysical As New MCtrPhysicalInventory(Me.Tag)
                    For Each item In ListTransferOrderDetailImportInfo

                        listPhysicalInventoryCrystalProduct = modelPhysical.GetListPhysicalInventory(item.ProductId, _wareHouseId)
                        If listPhysicalInventoryCrystalProduct.Count > 0 Then

                            listPhysicalInventoryCrystalProduct = listPhysicalInventoryCrystalProduct.
                                OrderBy(Function(d) If(_wareHouseId IsNot Nothing AndAlso _wareHouseId = d.WarehouseId, 0, 1)).
                                ThenBy(Function(d) If(d.BatchSerialExpiredDate Is Nothing, DateTime.MaxValue, d.BatchSerialExpiredDate)).ToList()

                            For Each PhysicalInventory In listPhysicalInventoryCrystalProduct
                                Dim transferOrderDetailBatchSerial As New TransferOrderDetailBatchSerial
                                transferOrderDetailBatchSerial.PhysicalInventoryId = PhysicalInventory.Id
                                transferOrderDetailBatchSerial.Quantity = PhysicalInventory.QuantityDeliver
                                transferOrderDetailBatchSerial.OutstandingQuantity = PhysicalInventory.QuantityDeliver
                                transferOrderDetailBatchSerial.CodeNameProduct = .DescriptionProduct
                                transferOrderDetailBatchSerial.CodeBatchSerial = PhysicalInventory.CodeNameBatchSerial
                                transferOrderDetailBatchSerial.CodeNameWarehouse = PhysicalInventory.CodeNameWarehouse
                                transferOrderDetail.TransferOrderDetailBatchSerial.Add(transferOrderDetailBatchSerial)
                            Next
                        End If
                    Next
                End Using
            End If
        End With
        INDGcCUM.DataSource = Nothing
        INDGcCUM.DataSource = listPhysicalInventoryCrystalProduct
        INDGcCUM.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Consultar la cantidad disponible de productos en el almacen de origen
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CalculatedQuantityInventory() As Integer
        Using modelPhysical As New MCtrPhysicalInventory(Me.Tag)
            listPhysicalInventory = modelPhysical.GetListPhysicalInventory(product.Id, _wareHouseId)
        End Using
        Dim QuantityAvailable = listPhysicalInventory.Sum(Function(x)
                                                              Return x.Quantity
                                                          End Function)
        Return QuantityAvailable
    End Function

#End Region

#Region "Shown"


    Private Sub PopupCUMTransferOrder_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        If _editMode = True Then
            LoadControls()
        ElseIf _importDataMode = True Then
            'si se esta importando informacion
            If _listTransferOrderDetailImportInfo.Count > 0 Then
                LoadControls()
            End If
        Else
            CleanControls()
        End If
    End Sub
#End Region

#Region "Click"
    Private Async Sub INDBtnOk_Click(sender As Object, e As EventArgs) Handles INDBtnOk.Click
        Try
            Dim errors = ValidateControlsPopup()
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
            'si no se esta importando informacion asigno valores
            If _listTransferOrderDetailValidation Is Nothing Then
                _listTransferOrderDetailValidation = New List(Of TransferOrderDetail)
            End If

            Dim args As New AddProductTransferOrderDetailEventArgs

            If _editMode = True Then
                args.ItemTransferOrderDetail = transferOrderDetail
                args.EditMode = True
            Else
                args.ItemTransferOrderDetail = transferOrderDetail
                _listTransferOrderDetailValidation.Add(transferOrderDetail)
            End If

            If _importDataMode = True Then
                args.ImportDataMode = True
                args.ListTransferOrderDetail = SetValuesData()
                _listTransferOrderDetailValidation.Add(transferOrderDetail)
            End If

            RaiseEvent AddTransferOrderDetail(Nothing, args)

            If _importDataMode = True OrElse _editMode = True Then
                product = Nothing
                Me.Close()
            Else
                CleanControls()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    ''' <summary>
    ''' valida los controles del formualario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim errors As New StringBuilder
        Dim listTmpSum
        'Listado que se arma con los items que no arrojaron errores
        Dim listTempTransferOrderDetail As New List(Of TransferOrderDetail)
        'recorro todos los items que se importaron para hacer las validaciones
        For Each item In _listTransferOrderDetailImportInfo
            'Variable para saber si se agrega el item al listado que se retorna con los items que no arrojaron errores
            Dim aggregatedItem As Boolean = True
            Using model As New MInventoryProduct(Me.Tag)
                product = model.GetInventoryProductByIdSimple(item.ProductId)
            End Using

            Using model As New MSubGroup(Me.Tag)
                If product.ProductSubGroup Is Nothing Then
                    aggregatedItem = False
                    errors.AppendLine(String.Format("El producto del item {0} no tiene un SubGrupo asociado.", (_listTransferOrderDetailImportInfo.IndexOf(item) + 1).ToString()))
                Else
                    If listPhysicalInventoryDetail Is Nothing OrElse listPhysicalInventoryDetail.Count = 0 Then
                        aggregatedItem = False
                        errors.AppendLine("El producto " + product.Name + " no se encontro dentro del inventario fisico")
                    End If
                End If
            End Using

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


        Dim listTmp = New List(Of PhysicalInventory)(listPhysicalInventoryDetail.ToArray())
        If listTmp IsNot Nothing Then
            listTmpSum = listTmp.Sum(Function(x) x.QuantityDeliver)
            If listTmpSum <= 0 Then
                errors.AppendLine(String.Format("No puede tener cantidades en 0 "))
            End If
        End If


        Return errors.ToString()
    End Function


    Public Function SetValuesData() As List(Of TransferOrderDetail)
        If listPhysicalInventoryDetail IsNot Nothing And ListTransferOrderDetailImportInfo IsNot Nothing Then

            For Each item In listPhysicalInventoryDetail
                Using model As New MInventoryProduct(Me.Tag)

                    product = New InventoryProduct

                    With transferOrderDetail
                        product = model.GetInventoryProductByIdSimple(item.ProductId)
                        .ProductId = product.Id
                        .DescriptionProduct = product.Code + " - " + product.Name
                        .Value = product.ProductCost
                        .ConsumptionUnit = product.PackingUnitDescription
                        .CostProduct = product.ProductCost
                        .Quantity = item.QuantityDeliver
                        .InventoryQuantity = item.Quantity

                        If product.ProductSubGroup IsNot Nothing Then

                            If product.ProductSubGroup.HandlesBatch = True Then
                                'si se esta editando el registro elimino los detalles de TransferOrderDetailBatchSerial 

                                If _editMode = True Then
                                    If listPhysicalInventory IsNot Nothing AndAlso listPhysicalInventory.Count > 0 Then
                                        While .TransferOrderDetailBatchSerial.Count > 0
                                            If .TransferOrderDetailBatchSerial(0).Id < 0 Then
                                                .TransferOrderDetailBatchSerial.Remove(.TransferOrderDetailBatchSerial(0))
                                            End If
                                        End While
                                    End If
                                End If

                                If listPhysicalInventoryDetail IsNot Nothing Then
                                    For Each itemTwo In listPhysicalInventoryDetail
                                        Dim transferOrderDetailBatchSerial As New TransferOrderDetailBatchSerial
                                        transferOrderDetailBatchSerial.PhysicalInventoryId = itemTwo.Id
                                        transferOrderDetailBatchSerial.Quantity = itemTwo.QuantityDeliver
                                        transferOrderDetailBatchSerial.OutstandingQuantity = itemTwo.QuantityDeliver
                                        transferOrderDetailBatchSerial.CodeNameProduct = .DescriptionProduct
                                        transferOrderDetailBatchSerial.CodeBatchSerial = itemTwo.CodeNameBatchSerial
                                        transferOrderDetailBatchSerial.CodeNameWarehouse = itemTwo.CodeNameWarehouse
                                        .TransferOrderDetailBatchSerial.Add(transferOrderDetailBatchSerial)
                                    Next
                                End If

                            Else
                                ' elimino todos los registros de transferOrderDetailBatchSerial
                                While .TransferOrderDetailBatchSerial.Count > 0
                                    If .TransferOrderDetailBatchSerial(0).Id < 0 Then
                                        .TransferOrderDetailBatchSerial.Remove(.TransferOrderDetailBatchSerial(0))
                                    End If
                                End While

                                Dim transferOrderDetailBatchSerial As New TransferOrderDetailBatchSerial
                                transferOrderDetailBatchSerial.Quantity = item.QuantityDeliver
                                transferOrderDetailBatchSerial.OutstandingQuantity = item.QuantityDeliver
                                transferOrderDetailBatchSerial.CodeNameProduct = .DescriptionProduct
                                transferOrderDetailBatchSerial.PhysicalInventoryId = listPhysicalInventory(0).Id
                                .TransferOrderDetailBatchSerial.Add(transferOrderDetailBatchSerial)
                            End If
                        End If
                    End With
                    If transferOrderDetail.Quantity > 0 Then
                        ListTransferOrderDetailImportarInfo.Add(transferOrderDetail)
                    End If
                End Using
            Next
        End If

        'Validación        
        Dim validateExpired As Boolean = False
        Dim oustandingQuantityDeliver As Integer
        For Each item In ListTransferOrderDetailImportarInfo
            oustandingQuantityDeliver += item.Quantity
        Next
        For Each physical In listPhysicalInventoryDetail
            Dim itemDeliver = listPhysicalInventory.FirstOrDefault(Function(d) d.Id = physical.Id AndAlso d.WarehouseId = physical.WarehouseId AndAlso d.ProductId = physical.ProductId AndAlso d.BatchSerialId.Equals(physical.BatchSerialId))
            If itemDeliver Is Nothing Then
                'Si no se encuentra el item dentro de los productos a entregar
                If physical.BatchSerialId IsNot Nothing Then
                    'Y este maneja lote, no se esta entregando el próximo a vencer
                    validateExpired = True
                End If
            Else
                'Si se encuentra el item dentro de los productos a entregar
                oustandingQuantityDeliver = oustandingQuantityDeliver - itemDeliver.QuantityDeliver
                If physical.BatchSerialId IsNot Nothing Then
                    'Y este maneja lote
                    If oustandingQuantityDeliver > 0 AndAlso physical.Quantity > itemDeliver.QuantityDeliver Then
                        'Y aún queda cantidades por entregar en el item y del total entregado, no se esta entregando el próximo a vencer
                        validateExpired = True
                    End If
                End If
            End If
            If validateExpired OrElse Not oustandingQuantityDeliver > 0 Then
                Exit For
            End If
        Next


        For Each item As TransferOrderDetail In ListTransferOrderDetailImportarInfo
            If item.Quantity = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "La cantidad en el inventario del producto " + item.DescriptionProduct + " es menor a la cantidad a entregar "
            End If
        Next

        Return ListTransferOrderDetailImportarInfo

    End Function

#End Region

#Region "KeyDown"
    Private Sub PopupCUMTransferOrder_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub
#End Region





#Region "EditValueChanging"
    Private Sub INDRptSeQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptSeQuantity.EditValueChanging
        If e.NewValue Is String.Empty Then
            Exit Sub
        End If
        Dim quantity

        Dim physicalInventoryTmp
        If Custody Then
            Dim listTmp = New List(Of PhysicalInventoryCustody)(listPhysicalInventoryCustodyProduct.ToArray())
            physicalInventoryTmp = DirectCast(INDGvCUM.GetFocusedRow, PhysicalInventoryCustody)
            If e.NewValue > physicalInventoryTmp.Quantity Then
                Mensaje(EeventViewerImages.Advertencia) = "La cantidad en el inventario del producto " + physicalInventoryTmp.CodeNameProduct + " es menor a la cantidad a entregar "
                e.Cancel = True
                Exit Sub
            End If
            listTmp.Remove(physicalInventoryTmp)
            quantity = listTmp.Sum(Function(x) x.QuantityDeliver)
        Else
            Dim listTmp = New List(Of PhysicalInventory)(listPhysicalInventoryDetail.ToArray())
            physicalInventoryTmp = DirectCast(INDGvCUM.GetFocusedRow, PhysicalInventory)
            If e.NewValue > physicalInventoryTmp.Quantity Then
                Mensaje(EeventViewerImages.Advertencia) = "La cantidad en el inventario del producto " + physicalInventoryTmp.CodeNameProduct + " es menor a la cantidad a entregar "
                e.Cancel = True
                Exit Sub
            End If
            listTmp.Remove(physicalInventoryTmp)

            quantity = listTmp.Sum(Function(x) x.QuantityDeliver)
        End If
        quantity += e.NewValue
        If quantity > _quantityDeliver Then
            Mensaje(EeventViewerImages.Advertencia) = "La cantidad a entregar por todos los productos supera la cantidad solicitada"
            e.Cancel = True
            Exit Sub
        End If
        physicalInventoryTmp.QuantityDeliver = e.NewValue
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de cantidad solicitada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDseRequestQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDseRequestQuantity.EditValueChanging
        If e IsNot Nothing Then

            'Se valida que si la cantidad digitada es menor a la cantidad entregada en la rejilla
            If Custody Then
                If listPhysicalInventoryCustodyProduct IsNot Nothing AndAlso listPhysicalInventoryCustodyProduct.Count > 0 Then
                    If e.NewValue < listPhysicalInventoryCustodyProduct.Sum(Function(x) x.QuantityDeliver) Then
                        Mensaje(EeventViewerImages.Advertencia) = "La cantidad solicitada no puede ser menor a la sumatoria de la cantidad entregada en la rejilla"
                        e.Cancel = True
                        Exit Sub
                    End If
                End If
            Else
                If listPhysicalInventoryCrystalProduct IsNot Nothing AndAlso listPhysicalInventoryCrystalProduct.Count > 0 Then
                    If e.NewValue < listPhysicalInventoryCrystalProduct.Sum(Function(x) x.QuantityDeliver) Then
                        Mensaje(EeventViewerImages.Advertencia) = "La cantidad solicitada no puede ser menor a la sumatoria de la cantidad entregada en la rejilla"
                        e.Cancel = True
                        Exit Sub
                    End If
                End If
            End If

            _quantityDeliver = e.NewValue
        End If
    End Sub

#End Region

#Region "RowStyle"

    Private Sub INDGvCUM_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles INDGvCUM.RowStyle
        Dim view As GridView = TryCast(sender, GridView)
        Dim _expirationDate As DateTime? = view.GetRowCellValue(e.RowHandle, "BatchSerialExpiredDate")
        If _expirationDate IsNot Nothing Then
            If Me._settingInventory IsNot Nothing Then
                If Me._settingInventory.BatchSerialRange IsNot Nothing AndAlso Me._settingInventory.BatchSerialRange.Any Then
                    Dim diff = DateDiff(DateInterval.Day, Me._currentDate, _expirationDate.Value)
                    Dim batchSerialRange As BatchSerialRange = Nothing
                    If diff <= 0 Then
                        batchSerialRange = Me._settingInventory.BatchSerialRange.FirstOrDefault()
                    Else
                        batchSerialRange = Me._settingInventory.BatchSerialRange.FirstOrDefault(Function(r) r.InitialRange <= diff AndAlso r.EndRange >= diff)
                    End If
                    If batchSerialRange IsNot Nothing Then
                        'e.Appearance.ForeColor = ForeColor
                        e.Appearance.BackColor = System.Drawing.Color.FromArgb(batchSerialRange.Color)
                        e.HighPriority = True
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        product = Nothing
        listPhysicalInventory = Nothing
        _listTransferOrderDetailImportInfo = Nothing
        _importDataMode = False
        _editMode = False
    End Sub
    Private Sub PopupCUMTransferOrder_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        CleanControls()
    End Sub
#End Region

#End Region

End Class

