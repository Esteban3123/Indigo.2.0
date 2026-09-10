'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Miguel Angel Fonseca
' Created          : 2017-12-12
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
Imports Infrastructure.Data.Xpo.InventoryRepository

#End Region

Public Class FrmPopupProductInConsignment

#Region "GLOBALS"
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Inventory"

    ''' <summary>
    ''' bandera para saber que se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Private _editMode As Boolean

    ''' <summary>
    ''' bandera para saber si se esta importando informacion
    ''' </summary>
    ''' <remarks></remarks>
    Private _importDataMode As Boolean

    ''' <summary>
    ''' id de la unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Private _operatingUnitId As Integer

    ''' <summary>
    ''' id del almacen 
    ''' </summary>
    ''' <remarks></remarks>
    Private _wareHouseId As Integer

    ''' <summary>
    ''' tipo de movimiento
    ''' </summary>
    ''' <remarks></remarks>
    Private _movementType As Integer?

    ''' <summary>
    ''' control que se muestra en la barra botones
    ''' </summary>
    ''' <remarks></remarks>
    Private ctrTmp As CtrRemissionSource

    ''' <summary>
    ''' entidad de producto
    ''' </summary>
    ''' <remarks></remarks>
    Private product As InventoryProduct

    ''' <summary>
    ''' detalle de la remision
    ''' </summary>
    ''' <remarks></remarks>
    Private consignmentInventoryRemissionDetail As ConsignmentInventoryRemissionDetail

    ''' <summary>
    ''' listado del detalle de la remision cuando se importa imformacion
    ''' </summary>
    ''' <remarks></remarks>
    Private _listConsignmentInventoryRemissionDetailImportInfo As List(Of ConsignmentInventoryRemissionDetail)

    ''' <summary>
    ''' listado del detalla de la remision para validar que los productos no se repitan con la misma fuente
    ''' </summary>
    ''' <remarks></remarks>
    Private _listConsignmentInventoryRemissionDetailValidation As List(Of ConsignmentInventoryRemissionDetail)

    ''' <summary>
    ''' listado de los lotes
    ''' </summary>
    ''' <remarks></remarks>
    Private listBatchSerial As List(Of BatchSerial)

    ''' <summary>
    ''' Moneda de la cabecera del documento
    ''' </summary>
    Private _headCurrency As Currency

    ''' <summary>
    ''' variable de la tasa de cambio, por defecto es 1 cuando la moneda es igual a la oficial
    ''' </summary>
    Private _tRMValue As Decimal = 1

    ''' <summary>
    ''' variable de session
    ''' </summary>
    Private _indigo As SessionValues

#End Region

#Region "PROPERTIES"

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
    ''' Propiedad para establecer el id de la unidad operativa
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property OperatingUnitId As Integer
        Set(value As Integer)
            _operatingUnitId = value
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
    ''' Propiedad para asignar el tipo de movimiento de inventario
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property MovementType As Integer?
        Set(value As Integer?)
            _movementType = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad publica para pasar el registro que se va a editar
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ConsignmentInventoryRemissionDetailEdit As ConsignmentInventoryRemissionDetail
        Set(value As ConsignmentInventoryRemissionDetail)
            consignmentInventoryRemissionDetail = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para para pasar el listado del detalle de la remision cuando se importa imformacion
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ListConsignmentInventoryRemissionDetailImportInfo As List(Of ConsignmentInventoryRemissionDetail)
        Set(value As List(Of ConsignmentInventoryRemissionDetail))
            _listConsignmentInventoryRemissionDetailImportInfo = value
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
    ''' propiedad para activar o desactivar controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Private WriteOnly Property ActionsControls As Boolean
        Set(value As Boolean)
            INDSeQuantity.Enabled = value
            INDPceBatchSerial.Enabled = value
            INDTxtUnitValue.Enabled = value
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
    ''' propieda que obtiene o establece el valor de la ultima compra
    ''' </summary>
    ''' <returns></returns>
    Property LastPurcharse As Decimal
        Get
            Return INDTxtLastPurcharse.EditValue
        End Get
        Set(value As Decimal)
            INDTxtLastPurcharse.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece la cantidad
    ''' </summary>
    ''' <returns></returns>
    Property Quantity As Integer
        Get
            Return INDSeQuantity.EditValue
        End Get
        Set(value As Integer)
            INDSeQuantity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el valor del IVA
    ''' </summary>
    ''' <returns></returns>
    Property IvaValue As Decimal
        Get
            Return INDTxtIvaValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtIvaValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el valor Unitario
    ''' </summary>
    ''' <returns></returns>
    Property UnitValue As Decimal
        Get
            Return INDTxtUnitValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtUnitValue.EditValue = value
        End Set
    End Property

#End Region

#Region "BUILDER"
    Sub New(Optional _currency As Currency = Nothing, Optional _tRMValue As Decimal = 1)
        InitializeComponent()
        ctrTmp = New CtrRemissionSource()
        ctrTmp.SetInfoFunction(AddressOf getInfo)
        ctrTmp.PrintInfo()
        ctrTmp.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
        Me._tRMValue = _tRMValue
        Me._indigo = SessionValues.Instance
        Me._headCurrency = If(_currency Is Nothing, New Currency With {.Id = Me._indigo.OfficialCurrencyId,
                            .Abbreviation = Me._indigo.CurrencyISO4217}, _currency)
        Me.SetCurrencyUI(Me._headCurrency?.Abbreviation)
    End Sub
#End Region

#Region "BAR BUTTONS"
    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub
#End Region

#Region "EVENTS"
    ''' <summary>
    ''' evento para agregar un producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddConsignmentInventoryRemissionDetail(sender As Object, e As AddConsignmentInventoryRemissionDetailEventArgs)
#End Region

#Region "HANDLES"
#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _editMode = Nothing
        _importDataMode = Nothing
        _operatingUnitId = Nothing
        _wareHouseId = Nothing
        _movementType = Nothing
        ctrTmp = Nothing
        product = Nothing
        consignmentInventoryRemissionDetail = Nothing
        _listConsignmentInventoryRemissionDetailImportInfo = Nothing
        _listConsignmentInventoryRemissionDetailValidation = Nothing
        listBatchSerial = Nothing
    End Sub

    Private Sub FrmPopupProductInConsignment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = True
        CtrBatchSerial1.RemissionType = ERemissionType.Input

        If Me._headCurrency?.Id <> Me._indigo?.OfficialCurrencyId AndAlso Me._tRMValue = 1 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("TRMEmpty")
            Me.Close()
            Exit Sub
        End If

        If _editMode Then
            'si esta editando un registro
            LoadControls()
        ElseIf _importDataMode Then
            'si se esta importando informacion
            If _listConsignmentInventoryRemissionDetailImportInfo.Count > 1 Then
                Me.BarraBotones.ColumnInfo = {New ColumnInfo With {.Caption = "Producto", .FieldName = "CodeNameProduct"}}.ToList()
                BarraBotones.FilterDataSource = _listConsignmentInventoryRemissionDetailImportInfo
            Else
                LoadControls(_listConsignmentInventoryRemissionDetailImportInfo(0))
            End If
        Else
            'si es un nuevo registro
            CleanControls()
        End If
    End Sub
#End Region

#Region "FormClosing"
    Private Sub FrmPopupProductInConsignment_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If product IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub
#End Region

#Region "Activated"
    Private Sub FrmPopupProductInConsignment_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If product Is Nothing Then
            INDPceProduct.Focus()
        End If
        If _importDataMode Then
            If INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If INDPceBatchSerial.EditValue Is String.Empty Then
                    INDPceBatchSerial.Focus()
                End If
            Else
                If Quantity = 0 Then
                    INDSeQuantity.Focus()
                End If
            End If
        End If
        If _editMode Then
            INDPceProduct.Focus()
        End If
    End Sub
#End Region

#Region "SelectProduct"
    ''' <summary>
    ''' Handles the SelectProduct event of the CtrProducts1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="SelectProductEventArgs"/> instance containing the event data.</param>
    Private Sub CtrProducts1_SelectProduct(sender As Object, e As SelectProductEventArgs) Handles CtrProducts1.SelectProduct
        INDPceProduct.Text = e.CodeNameProduct
        INDPceProduct.Focus()
        INDPceProduct.ClosePopup()
        CtrBatchSerial1.CleanControls()
        Me.Quantity = 0
        INDPceBatchSerial.EditValue = Nothing
        ActionsControls = True
        Using model As New MInventoryProduct(Me.Tag)
            product = model.GetInventoryProductByIdSimple(e.ProductId)
            If product IsNot Nothing Then
                INDTxtUnitPurcharse.Text = product.PackingUnitDescription

                Using modelIva As New MGeneralLedgerIVA(Me.Tag)

                    Dim iva As ActionResult(Of GeneralLedgerIVA) = New ActionResult(Of GeneralLedgerIVA)
                    If product?.IVAId IsNot Nothing Then
                        iva = modelIva.GetGeneralLedgerIVAById(product.IVAId)
                    End If

                    Dim result = GetLastPurcharse(product, Me._tRMValue, iva?.ObjectEmbbeded?.Percentage)

                    If result Is Nothing OrElse Not result.StateResult Then
                        Mensaje(EeventViewerImages.Advertencia) = result?.Message
                        Me.CleanControls()
                        Exit Sub
                    End If

                    Me.LastPurcharse = result.ObjectEmbbeded
                    Me.UnitValue = Me.LastPurcharse
                End Using

                If product.ProductSubGroup IsNot Nothing Then
                    If product.ProductSubGroup.HandlesBatch Then
                        INDSeQuantity.Properties.ReadOnly = True
                        INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDPceBatchSerial.Focus()
                        If _editMode Then
                            'instancio el listado para poder hacer la validacion de los lotes cuando se esta editando y saber si esta sin registros
                            listBatchSerial = New List(Of BatchSerial)
                        End If
                        INDPceBatchSerial.Focus()
                        INDPceBatchSerial.ShowPopup()
                    Else
                        INDSeQuantity.Properties.ReadOnly = False
                        INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDSeQuantity.Focus()
                    End If
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "El producto " + String.Concat(product.Code, " - ", product.Name) + " no tiene un subgrupo asociado"
                    CleanControls()
                End If
            End If
        End Using
    End Sub
#End Region

#Region "QueryPopUp"
    Private Sub INDPceProduct_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceProduct.QueryPopUp
        CtrProducts1.SetDataSourceProduct()
    End Sub

    Private Sub INDPceBatchSerial_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceBatchSerial.QueryPopUp
        CtrBatchSerial1.Product = product
        CtrBatchSerial1.FormOwner = Me
        CtrBatchSerial1.SetListBatchSerial()
    End Sub
#End Region

#Region "ButtonClick"
    Private Sub INDPceProduct_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDPceProduct.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmProducts
                OpenFormDialog(form)
            End Using
        End If
    End Sub
#End Region

#Region "ChangeQuantity"
    Private Sub CtrBatchSerial1_ChangeQuantity(sender As Object, e As ChangeQuantityEventArgs) Handles CtrBatchSerial1.ChangeQuantity
        Quantity = e.Quantity
    End Sub
#End Region

#Region "Closed"
    Private Sub INDPceBatchSerial_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDPceBatchSerial.Closed
        listBatchSerial = CtrBatchSerial1.GetListBatchSerial()
        If listBatchSerial.Count = 0 Then
            INDPceBatchSerial.EditValue = String.Empty
        Else
            INDPceBatchSerial.EditValue = String.Format(ResourceManager.GetString("SelectedBatch", MODULE_NAME), listBatchSerial.Count.ToString())
        End If
        If consignmentInventoryRemissionDetail IsNot Nothing AndAlso _importDataMode Then
            If listBatchSerial IsNot Nothing Then
                consignmentInventoryRemissionDetail.ConsignmentInventoryRemissionDetailBatchSerial.Clear()
                For Each item In listBatchSerial
                    Dim consignmentInventoryRemissionDetailBatchSerial As New ConsignmentInventoryRemissionDetailBatchSerial
                    consignmentInventoryRemissionDetailBatchSerial.BatchSerialId = item.Id
                    consignmentInventoryRemissionDetailBatchSerial.Quantity = item.Quantity
                    consignmentInventoryRemissionDetailBatchSerial.OutstandingQuantity = item.Quantity
                    consignmentInventoryRemissionDetailBatchSerial.CodeBatchSerial = item.BatchCode
                    consignmentInventoryRemissionDetailBatchSerial.ConsignmentInventoryRemissionDetailBatchSerialId = consignmentInventoryRemissionDetail.ConsignmentInventoryRemissionDetailBatchSerialId
                    consignmentInventoryRemissionDetail.ConsignmentInventoryRemissionDetailBatchSerial.Add(consignmentInventoryRemissionDetailBatchSerial)
                Next
            Else
                Dim consignmentInventoryRemissionDetailBatchSerial As New ConsignmentInventoryRemissionDetailBatchSerial
                consignmentInventoryRemissionDetailBatchSerial.Quantity = Quantity
                consignmentInventoryRemissionDetailBatchSerial.OutstandingQuantity = Quantity
                consignmentInventoryRemissionDetailBatchSerial.ConsignmentInventoryRemissionDetailBatchSerialId = consignmentInventoryRemissionDetail.ConsignmentInventoryRemissionDetailBatchSerialId
                consignmentInventoryRemissionDetail.ConsignmentInventoryRemissionDetailBatchSerial.Add(consignmentInventoryRemissionDetailBatchSerial)
            End If
        End If
    End Sub
#End Region

#Region "Click"
    Private Async Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        Try
            AsyncLoader(True)
            INDBtnAdd.Enabled = False
            Dim errors = ValidateControlsPopup()

            If errors.Length > 0 Then
                AsyncLoader(False)
                INDBtnAdd.Enabled = True
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
            'si no se esta importando informacion asigno valores
            If Not _importDataMode Then
                SetValues()
                Using modelSettings As New MSettingInventory(Me.Tag)
                    Dim resulValidateStock = Await modelSettings.ValidateStock(consignmentInventoryRemissionDetail.ProductId, _operatingUnitId, _wareHouseId, consignmentInventoryRemissionDetail.Quantity, InventoryStaticServices.MovementType.Input)
                    If resulValidateStock.StateResult = False Then
                        Mensaje(EeventViewerImages.Advertencia) = resulValidateStock.Message
                    End If
                End Using
            End If

            If _listConsignmentInventoryRemissionDetailValidation Is Nothing Then
                _listConsignmentInventoryRemissionDetailValidation = New List(Of ConsignmentInventoryRemissionDetail)
            End If

            Dim args As New AddConsignmentInventoryRemissionDetailEventArgs
            If _editMode Then
                args.ConsignmentInventoryRemissionDetail = consignmentInventoryRemissionDetail
                args.EditMode = True
            Else
                args.ConsignmentInventoryRemissionDetail = consignmentInventoryRemissionDetail
                _listConsignmentInventoryRemissionDetailValidation.Add(consignmentInventoryRemissionDetail)
            End If

            If _importDataMode Then
                args.ImportDataMode = True
                'asignamos las propiedades a la lista de detalles importada
                For Each item In _listConsignmentInventoryRemissionDetailImportInfo
                    Dim productItem As New InventoryProduct
                    Using model As New MInventoryProduct(Me.Tag)
                        productItem = model.GetInventoryProductByIdSimple(item.ProductId)
                    End Using
                    item.CodeNameProduct = productItem.Code + " - " + productItem.Name
                    item.ManufacturerName = productItem.ManufacturerDescription
                    item.HealthRegistration = productItem.HealthRegistration
                    item.Presentation = productItem.Presentation

                    Dim result = GetLastPurcharse(productItem, Me._tRMValue)

                    If result Is Nothing OrElse Not result.StateResult Then
                        Mensaje(EeventViewerImages.Advertencia) = result?.Message
                        Exit Sub
                    End If

                    item.LastValue = result.ObjectEmbbeded
					If productItem.IVAId IsNot Nothing Then
						Using model As New MGeneralLedgerIVA(Me.Tag)
							Dim iva = model.GetGeneralLedgerIVAById(productItem.IVAId)
								item.IvaPercentage = iva.ObjectEmbbeded.Percentage
							item.IvaValue = Math.Round((item.UnitValue * item.Quantity) * iva.ObjectEmbbeded.Percentage / 100, 2, MidpointRounding.AwayFromZero)
						End Using
					End If

					Dim subtotalTemp = item.UnitValue * item.Quantity
                    Dim ivaTemp = Math.Round((item.IvaValue * item.Quantity), 2, MidpointRounding.AwayFromZero)
                    item.SubTotalValue = subtotalTemp
                    item.TotalValue = item.SubTotalValue + ivaTemp
                Next

                args.ListConsignmentInventoryRemissionDetail = _listConsignmentInventoryRemissionDetailImportInfo
                _listConsignmentInventoryRemissionDetailValidation.AddRange(_listConsignmentInventoryRemissionDetailImportInfo)
            End If

            RaiseEvent AddConsignmentInventoryRemissionDetail(Nothing, args)
            AsyncLoader(False)
            INDBtnAdd.Enabled = True
            CleanControls()

        Catch ex As Exception
            AsyncLoader(False)
            INDBtnAdd.Enabled = True
            Throw ex
        End Try
    End Sub
#End Region

#Region "KeyDown"
    Private Sub FrmPopupProductInConsignment_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub INDTxtUnitValue_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDTxtUnitValue.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDBtnAdd.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDPceProduct_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDPceProduct.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Await AssignProductWithCode()
        End If
    End Sub

#End Region

#Region "RecordNavigationChangeEvent"
    Private Sub BarraBotones_RecordNavigationChangeEvent(Record As Object) Handles BarraBotones.RecordNavigationChangeEvent
        LoadControls(Record)
    End Sub
#End Region

#Region "EditValueChanged"
    Private Sub INDSeQuantity_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeQuantity.EditValueChanged
        If consignmentInventoryRemissionDetail IsNot Nothing AndAlso _importDataMode Then
            consignmentInventoryRemissionDetail.Quantity = Quantity
            If product.IVAId IsNot Nothing Then
                CalculateIvaValue()
            End If
        End If
    End Sub

    Private Sub INDTxtUnitValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtUnitValue.EditValueChanged
        If consignmentInventoryRemissionDetail IsNot Nothing AndAlso _importDataMode = True Then
            consignmentInventoryRemissionDetail.UnitValue = math.round(Me.UnitValue,2,MidpointRounding.AwayFromZero)
            consignmentInventoryRemissionDetail.LastValue = Me.LastPurcharse
            If product.IVAId IsNot Nothing Then
                CalculateIvaValue()
            End If
            consignmentInventoryRemissionDetail.SubTotalValue = Math.Round(consignmentInventoryRemissionDetail.UnitValue * consignmentInventoryRemissionDetail.Quantity, 2, MidpointRounding.AwayFromZero)
            consignmentInventoryRemissionDetail.TotalValue = consignmentInventoryRemissionDetail.SubTotalValue + consignmentInventoryRemissionDetail.IvaValue
        Else
            If product IsNot Nothing Then
                If product.IVAId IsNot Nothing Then
                    Using model As New MGeneralLedgerIVA(Me.Tag)
                        Dim iva = model.GetGeneralLedgerIVAById(product.IVAId)
                        ''Math.Round((consignmentInventoryRemissionDetail.UnitValue * Quantity) * iva.ObjectEmbbeded.Percentage / 100, 2, MidpointRounding.AwayFromZero)
                        IvaValue = Math.Round(Me.UnitValue * iva.ObjectEmbbeded.Percentage / 100, 2, MidpointRounding.AwayFromZero)
                        'consignmentInventoryRemissionDetail.IvaValue = IvaValue
                    End Using
                End If
            End If
        End If
    End Sub
#End Region

#Region "Popup"
    Private Sub INDPceBatchSerial_Popup(sender As Object, e As EventArgs) Handles INDPceBatchSerial.Popup
        CtrBatchSerial1.SetFocusGrid()
    End Sub
#End Region
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
        CtrBatchSerial1.CleanControls()
        consignmentInventoryRemissionDetail = Nothing
        ActionsControls = False
        INDPceProduct.Text = String.Empty
        INDPceProduct.Properties.ReadOnly = False
        INDPceBatchSerial.EditValue = Nothing
        product = Nothing
        INDTxtUnitPurcharse.Text = String.Empty
        Me.LastPurcharse = 0
        Quantity = 0
        Me.UnitValue = 0
        Me.IvaValue = 0
        INDPceProduct.Focus()
        listBatchSerial = Nothing
        _listConsignmentInventoryRemissionDetailImportInfo = Nothing
        BarraBotones.FilterDataSource = Nothing
        _importDataMode = False
        _editMode = False
        ctrTmp.PrintInfo()
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
                errors.AppendLine(INDLciProduct.Text + ResourceManager.GetString("Empty"))
            End If

            If Not _editMode AndAlso product IsNot Nothing Then
                'valido que no se agregue el mismo producto con tipo fuente ninguno

                If _listConsignmentInventoryRemissionDetailValidation IsNot Nothing AndAlso _listConsignmentInventoryRemissionDetailValidation.Count > 0 Then
                    Dim consignmentInventoryRemissionDetailTmp = _listConsignmentInventoryRemissionDetailValidation.Find(Function(x) x.ProductId = product.Id And x.RemissionSource = 1)
                    If consignmentInventoryRemissionDetailTmp IsNot Nothing Then
                        errors.AppendLine(String.Format(ResourceManager.GetString("SourceProductNone", MODULE_NAME), consignmentInventoryRemissionDetailTmp.CodeNameProduct))
                    End If
                End If

                If _movementType Is Nothing OrElse _movementType = 3 Then
                    errors.AppendLine(String.Format("La remisión no acepta nuevos productos."))
                End If
            End If

            If Quantity = 0 Then
                errors.AppendLine(INDLciQuantity.Text + ResourceManager.GetString("Empty"))
            End If

            If INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If _editMode = False Then
                    If listBatchSerial Is Nothing Then
                        errors.AppendLine(INDLciBatchSerial.Text + ResourceManager.GetString("Empty"))
                    End If
                    If listBatchSerial IsNot Nothing AndAlso listBatchSerial.Count = 0 Then
                        errors.AppendLine(INDLciBatchSerial.Text + ResourceManager.GetString("Empty"))
                    End If
                Else
                    If listBatchSerial IsNot Nothing AndAlso listBatchSerial.Count = 0 Then
                        errors.AppendLine(INDLciBatchSerial.Text + ResourceManager.GetString("Empty"))
                    End If
                End If
            End If

            If Me.UnitValue = 0 Then
                errors.AppendLine(INDLciUnitValue.Text + ResourceManager.GetString("Empty"))
            End If

            If consignmentInventoryRemissionDetail IsNot Nothing AndAlso consignmentInventoryRemissionDetail.RemissionSource = 4 Then
                If Quantity > consignmentInventoryRemissionDetail.ConsignmentInventoryRemissionDetailBatchSerial.Sum(Function(x) x.OutstandingQuantity) Then
                    errors.AppendLine(String.Format(ResourceManager.GetString("QuantityOverFlow", MODULE_NAME), consignmentInventoryRemissionDetail.CodeNameProduct, "Remision de Inventario en consignación"))
                End If
            End If
        Else
            'recorro todos los items que se importaron para hacer las validaciones
            For Each item In _listConsignmentInventoryRemissionDetailImportInfo
                Using model As New MInventoryProduct(Me.Tag)
                    product = model.GetInventoryProductByIdSimple(item.ProductId)
                End Using
                Using model As New MSubGroup(Me.Tag)
                    If product.ProductSubGroupId = 0 Then
                        errors.AppendLine(String.Format("El producto del item {0} no tiene un SubGrupo asociado.", (_listConsignmentInventoryRemissionDetailImportInfo.IndexOf(item) + 1).ToString()))
                    Else
                        Dim subGroup = model.GetProductSubGroupByIdSimple(product.ProductSubGroupId)
                        If subGroup.ObjectEmbbeded.HandlesBatch = True And item.ConsignmentInventoryRemissionDetailBatchSerial.Count = 0 Then
                            'valido que si el producto maneja lote se encuentre agregado minimo un lote en ConsignmentInventoryRemissionDetailBatchSerial 
                            errors.AppendLine(String.Format(ResourceManager.GetString("HandlesBatch", MODULE_NAME), (_listConsignmentInventoryRemissionDetailImportInfo.IndexOf(item) + 1).ToString()))
                        Else
                            If item.Quantity = 0 Then
                                errors.AppendLine(String.Format(ResourceManager.GetString("QuantityZero", MODULE_NAME), (_listConsignmentInventoryRemissionDetailImportInfo.IndexOf(item) + 1).ToString()))
                            End If
                        End If
                    End If
                End Using

                If item.Quantity > item.QuantityImport Then
                    If item.RemissionSource = 2 Then
                        errors.AppendLine("La cantidad pendiente del producto " + item.CodeNameProduct + " en la orden de compra del item " + (_listConsignmentInventoryRemissionDetailImportInfo.IndexOf(item) + 1).ToString() + " es menor a la cantidad que se va agregar")
                    Else
                        errors.AppendLine("La cantidad pendiente del producto " + item.CodeNameProduct + " en el contrato del item " + (_listConsignmentInventoryRemissionDetailImportInfo.IndexOf(item) + 1).ToString() + " es menor a la cantidad que se va agregar")
                    End If

                End If

                If item.UnitValue = 0 Then
                    errors.AppendLine(String.Format(ResourceManager.GetString("ValueZero", MODULE_NAME), (_listConsignmentInventoryRemissionDetailImportInfo.IndexOf(item) + 1).ToString()))
                End If
            Next
        End If

        Return errors.ToString()
    End Function

    Private Sub CalculateIvaValue()
        Using model As New MGeneralLedgerIVA(Me.Tag)
            Dim iva = model.GetGeneralLedgerIVAById(product.IVAId)
            consignmentInventoryRemissionDetail.IvaPercentage = iva.ObjectEmbbeded.Percentage
            consignmentInventoryRemissionDetail.IvaValue = Math.Round((consignmentInventoryRemissionDetail.UnitValue * Quantity) * iva.ObjectEmbbeded.Percentage / 100, 2, MidpointRounding.AwayFromZero)
            IvaValue = consignmentInventoryRemissionDetail.IvaValue
        End Using
    End Sub

    ''' <summary>
    ''' establece los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetValues()
        If Not _editMode Then
            consignmentInventoryRemissionDetail = New ConsignmentInventoryRemissionDetail
        End If

        With consignmentInventoryRemissionDetail
            If Not _editMode Then
                .RemissionSource = 1
            End If

            .ProductId = product.Id
            .CodeNameProduct = product.Code + " - " + product.Name
            .ManufacturerName = product.ManufacturerDescription
            .HealthRegistration = product.HealthRegistration
            .Presentation = product.Presentation
            .Quantity = Quantity
            .UnitValue = Me.UnitValue
            .LastValue = Me.LastPurcharse

            If product.IVAId IsNot Nothing Then
                Using model As New MGeneralLedgerIVA(Me.Tag)
                    Dim iva = model.GetGeneralLedgerIVAById(product.IVAId)
                    .IvaPercentage = iva.ObjectEmbbeded.Percentage
                    'valor iva
                    .IvaValue = Math.Round((.Quantity * .UnitValue) * (iva.ObjectEmbbeded.Percentage / 100), 2, MidpointRounding.AwayFromZero)
                End Using
            End If

            .SubTotalValue = .UnitValue * .Quantity
            .TotalValue = .SubTotalValue + .IvaValue
            If INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                'si se esta editando el registro elimino los detalles de ConsignmentInventoryRemissionDetailBatchSerial 
                If _editMode = True Then
                    If listBatchSerial IsNot Nothing AndAlso listBatchSerial.Count > 0 Then
                        While .ConsignmentInventoryRemissionDetailBatchSerial.Count > 0
                            If .ConsignmentInventoryRemissionDetailBatchSerial(0).Id > 0 Then
                                .ConsignmentInventoryRemissionDetailBatchSerial(0).MarkAsDeleted()
                            Else
                                .ConsignmentInventoryRemissionDetailBatchSerial.Remove(.ConsignmentInventoryRemissionDetailBatchSerial(0))
                            End If
                        End While
                    End If
                End If

                If listBatchSerial IsNot Nothing Then
                    For Each item In listBatchSerial
                        Dim consignmentInventoryRemissionDetailBatchSerial As New ConsignmentInventoryRemissionDetailBatchSerial
                        consignmentInventoryRemissionDetailBatchSerial.BatchSerialId = item.Id
                        consignmentInventoryRemissionDetailBatchSerial.Quantity = item.Quantity
                        consignmentInventoryRemissionDetailBatchSerial.OutstandingQuantity = item.Quantity
                        consignmentInventoryRemissionDetailBatchSerial.CodeBatchSerial = item.BatchCode
                        'Si es una reposicición
                        If .RemissionSource = 4 AndAlso .ConsignmentInventoryRemissionDetailBatchSerialId IsNot Nothing Then
                            consignmentInventoryRemissionDetailBatchSerial.ConsignmentInventoryRemissionDetailBatchSerialId = .ConsignmentInventoryRemissionDetailBatchSerialId
                        End If
                        .ConsignmentInventoryRemissionDetailBatchSerial.Add(consignmentInventoryRemissionDetailBatchSerial)
                    Next
                End If
            Else
                ' elimino todos los registros de ConsignmentInventoryRemissionDetailBatchSerial
                While .ConsignmentInventoryRemissionDetailBatchSerial.Count > 0
                    If .ConsignmentInventoryRemissionDetailBatchSerial(0).Id > 0 Then
                        .ConsignmentInventoryRemissionDetailBatchSerial(0).MarkAsDeleted()
                    Else
                        .ConsignmentInventoryRemissionDetailBatchSerial.Remove(.ConsignmentInventoryRemissionDetailBatchSerial(0))
                    End If
                End While

                Dim consignmentInventoryRemissionDetailBatchSerial As New ConsignmentInventoryRemissionDetailBatchSerial
                consignmentInventoryRemissionDetailBatchSerial.Quantity = Quantity
                consignmentInventoryRemissionDetailBatchSerial.OutstandingQuantity = Quantity
                'Si es una reposicición
                If .RemissionSource = 4 AndAlso .ConsignmentInventoryRemissionDetailBatchSerialId IsNot Nothing Then
                    consignmentInventoryRemissionDetailBatchSerial.ConsignmentInventoryRemissionDetailBatchSerialId = .ConsignmentInventoryRemissionDetailBatchSerialId
                End If

                .ConsignmentInventoryRemissionDetailBatchSerial.Add(consignmentInventoryRemissionDetailBatchSerial)
            End If
        End With
    End Sub

    ''' <summary>
    ''' metodo para cargar los controles con la informacion requerida
    ''' </summary>
    ''' <param name="consignmentInventoryRemissionDetailTmp"></param>
    ''' <remarks></remarks>
    Private Sub LoadControls(Optional consignmentInventoryRemissionDetailTmp As ConsignmentInventoryRemissionDetail = Nothing)
        If _editMode Then
            INDBtnAdd.Text = ResourceManager.GetString("Edit")
        End If

        If _importDataMode Then
            consignmentInventoryRemissionDetail = consignmentInventoryRemissionDetailTmp
        End If

        With consignmentInventoryRemissionDetail
            Using model As New MInventoryProduct(Me.Tag)
                product = model.GetInventoryProductByIdSimple(.ProductId)
                INDPceProduct.Text = product.Code + " - " + product.Name
                If _importDataMode = True Then
                    consignmentInventoryRemissionDetail.CodeNameProduct = INDPceProduct.Text
                End If
                INDPceProduct.Properties.ReadOnly = True
                INDTxtUnitPurcharse.Text = product.PackingUnitDescription

                Dim result = Me.GetLastPurcharse(product, Me._tRMValue)

                If result Is Nothing AndAlso Not result.StateResult Then
                    Mensaje(EeventViewerImages.Advertencia) = result?.Message
                    Me.CleanControls()
                    Exit Sub
                End If

                Me.LastPurcharse = result.ObjectEmbbeded
            End Using

            Using model As New MSubGroup(Me.Tag)
                If product.ProductSubGroupId > 0 Then
                    Dim subGroup = model.GetProductSubGroupByIdSimple(product.ProductSubGroupId)
                    If subGroup.ObjectEmbbeded.HandlesBatch = True Then
                        INDSeQuantity.Properties.ReadOnly = True
                        INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Else
                        INDSeQuantity.Properties.ReadOnly = False
                        INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    End If
                End If
            End Using

            Quantity = .Quantity

            If INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If .ConsignmentInventoryRemissionDetailBatchSerial.Count = 0 Then
                    INDPceBatchSerial.EditValue = String.Empty
                Else
                    INDPceBatchSerial.EditValue = String.Format(ResourceManager.GetString("SelectedBatch", MODULE_NAME), .ConsignmentInventoryRemissionDetailBatchSerial.Count.ToString())
                End If

                CtrBatchSerial1.Product = product
                CtrBatchSerial1.FormOwner = Me

                If _importDataMode Then
                    CtrBatchSerial1.CleanControls()
                End If

                CtrBatchSerial1.SetListBatchSerial()
                CtrBatchSerial1.SetQuantityBatchSerial(.ConsignmentInventoryRemissionDetailBatchSerial.ToList())
            End If
            'si se esta editando coloco el valor que asignaron sino el ultimo valor del producto

            If .UnitValue = 0 OrElse (Not _editMode AndAlso Not _importDataMode) OrElse (_importDataMode AndAlso consignmentInventoryRemissionDetailTmp?.RemissionSource = 4) Then
                Me.UnitValue = Me.LastPurcharse
                Me.IvaValue = Math.Round((Me.UnitValue * Quantity) * (product.PercentageIVA / 100), 2, MidpointRounding.AwayFromZero)
            Else
                Me.UnitValue = .UnitValue
                Me.IvaValue = IvaValue
            End If
        End With

        ctrTmp.PrintInfo()
    End Sub

    ''' <summary>
    ''' metodo para obtener la informacion y colocarla en el control de usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfo() As Tuple(Of String, String)
        If consignmentInventoryRemissionDetail IsNot Nothing Then
            Return New Tuple(Of String, String)(consignmentInventoryRemissionDetail.RemissionSource.ToString(), consignmentInventoryRemissionDetail.SourceCode)
        Else
            Return New Tuple(Of String, String)(1, String.Empty)
        End If
    End Function

    ''' <summary>
    ''' Lanza la consulta del producto para poderlo seleccionar
    ''' sin necesidad de desplegar el control de producto
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function AssignProductWithCode() As Task
        If INDPceProduct.Text.Trim <> String.Empty AndAlso _importDataMode = False Then
            'Separo el string escrito en el control de producto
            Dim arrayCodeProduct As String() = INDPceProduct.Text.Split(" - ")
            'Capturo el codigo del producto
            Dim codeProduct As String = arrayCodeProduct(0).Trim

            If codeProduct Is String.Empty Then
                If product IsNot Nothing Then
                    INDPceProduct.Text = product.Code + " - " + product.Name
                End If
                Exit Function
            End If

            Using model As New MInventoryProduct(Me.Tag)
                'Consulto el producto para armar el objeto SelectProductEventArgs
                Dim resultProduct = Await model.GetInventoryProduct(INDPceProduct.Text.Trim)
                If resultProduct.StateResult = False Then
                    Mensaje(EeventViewerImages.Advertencia) = resultProduct.MessageResult(0)
                    CleanControls()
                    Exit Function
                End If
                'Valido que exista el producto
                If resultProduct.ObjectEmbbeded IsNot Nothing AndAlso resultProduct.ObjectEmbbeded.Id = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format("El producto no existe")
                    CleanControls()
                    Exit Function
                End If
                'Valido que el producto no este inactivo
                If resultProduct.ObjectEmbbeded.Status = False Then
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

        ElseIf INDPceProduct.Text.Trim = String.Empty Then
            If product IsNot Nothing Then
                INDPceProduct.Text = product.Code + " - " + product.Name
            End If
        End If
    End Function

    ''' <summary>
    ''' Establece el formato moneda en los controles del formulario
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyUI(_currencyAbbreviation As String)

        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "Está llegando vacia la abreviación de la moneda"
            Exit Sub
        End If

        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = _currencyAbbreviation.GetNumberFormat

        Me.INDTxtLastPurcharse.Properties.Mask.Culture = _culture
        Me.INDTxtUnitValue.Properties.Mask.Culture = _culture
        Me.INDTxtIvaValue.Properties.Mask.Culture = _culture
    End Sub

    ''' <summary>
    ''' funcion para obtener calculado segun el parametro de iva de inventario el valor del ultimo costo
    ''' </summary>
    ''' <param name="product"></param>
    ''' <returns></returns>
    Private Function GetLastPurcharse(product As InventoryProduct, tRM As Decimal, Optional ivaPercent As Decimal? = Nothing) As ActionResult(Of Decimal)

        If product Is Nothing Then
            Return New ActionResult(Of Decimal) With {.StateResult = False, .Message = "Parametro producto vacio"}
        End If

        Using msearch As New MBusqueda
            Dim filter() As Object = {BarraBotones.OperatingUnitValue}
            Dim settings As XPCollection(Of SettingInventoryXpo) = msearch.ConsultarEntidades(eDataSource.GetSettingInventoryByOperatingUnit, filter)

            Dim lastPurcharse As Decimal = 0
            Dim Message As String = String.Empty
            ivaPercent = If(ivaPercent, product?.PercentageIVA)

            If Not product.TaxedProduct Then
                ivaPercent = 0
            End If

            Select Case settings?.FirstOrDefault?.TaxRegistration
                Case 1, 3
                    lastPurcharse = Math.Round((If(product?.FinalProductCost, 0) / tRM) / (1 + (If(ivaPercent, 0) / 100)), 2, MidpointRounding.AwayFromZero)
                Case 2
                    lastPurcharse = Math.Round((If(product.FinalProductCost, 0) / tRM), 2, MidpointRounding.AwayFromZero)
                Case Else
                    Message = "Debe parámetrizar un IVA"
            End Select

            Return New ActionResult(Of Decimal) With {.StateResult = String.IsNullOrEmpty(Message), .ObjectEmbbeded = lastPurcharse, .Message = Message}
        End Using
    End Function
#End Region
End Class