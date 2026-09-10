'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Miguel Angel Fonseca
' Created          : 2017-12-12
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Drawing
Imports System.Text
Imports DevExpress.Data.Async.Helpers
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Inventory.MVP
Imports Presentation.Maintenance

#End Region

Public Class FrmConsignmentInventoryRemission
    Implements IConsignmentInventoryRemission, ICustomizableForm

#Region "TUPLES"
    Private _listMovementType As List(Of Tuple(Of Integer, String))
#End Region

#Region "GLOBALS"

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Inventory"

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private _indigoSession As SessionValues

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.InventorySequence

    ''' <summary>
    ''' Prefijo seleccionado
    ''' </summary>
    Private _prefixSelected As String

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Variable que representa la entidad de parametros
    ''' </summary>
    ''' <remarks></remarks>
    Dim _settingsInventory As SettingInventory

    ''' <summary>
    ''' presenter de remision de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PConsignmentInventoryRemission

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordInventory

    ''' <summary>
    ''' entidad de remision de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Dim consignmentInventoryRemission As ConsignmentInventoryRemission

    ''' <summary>
    ''' entidad del detalle de la remision
    ''' </summary>
    ''' <remarks></remarks>
    Dim consignmentInventoryRemissionDetail As ConsignmentInventoryRemissionDetail

    ''' <summary>
    ''' listado del detalle de la remision
    ''' </summary>
    ''' <remarks></remarks>
    Dim listConsignmentInventoryRemissionDetail As List(Of ConsignmentInventoryRemissionDetail)

    ''' <summary>
    ''' listado del detalle de la remision para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listConsignmentInventoryRemissionDetailDelete As List(Of ConsignmentInventoryRemissionDetail)

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim indexEditRecord As Integer

    ''' <summary>
    ''' valor total del iva
    ''' </summary>
    ''' <remarks></remarks>
    Dim _ivaValue As Decimal = 0

    ''' <summary>
    ''' valor total neto
    ''' </summary>
    ''' <remarks></remarks>
    Dim _value As Decimal = 0

    ''' <summary>
    ''' valor total
    ''' </summary>
    ''' <remarks></remarks>
    Dim _totalValue As Decimal = 0

    ''' <summary>
    ''' Control para establecer informacion del ingreso
    ''' </summary>
    Private ctrTmp As CtrContractTotalInfo

    ''' <summary>
    ''' Controlar si confirmar procede de guardar o actualizar
    ''' </summary>
    Private _action As Integer

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

    ''' <summary>
    ''' tasa de cambio
    ''' </summary>
    Private _tRM As TRM

    ''' <summary>
    ''' flag para el proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim flagLoad As Boolean = False

#End Region

#Region "PROPERTIES"

#Region "Model.Properties"

    ''' <summary>
    ''' codigo de la remision
    ''' </summary>
    Public Property Code As String Implements IConsignmentInventoryRemission.Code
        Get
            If INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDBteCode.Text
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' fecha de la remision
    ''' </summary>
    Public Property RemissionDate As DateTime? Implements IConsignmentInventoryRemission.RemissionDate
        Get
            Return INDDteDate.EditValue
        End Get
        Set(value As DateTime?)
            INDDteDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' id de la linea de distribucion
    ''' </summary>
    Public Property SupplierDistributionLineId As Integer? Implements IConsignmentInventoryRemission.SupplierDistributionLineId
        Get
            Return INDSleSupplierDistributionLine.EditValue
        End Get
        Set(value As Integer?)
            INDSleSupplierDistributionLine.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' id del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim SupplierId As Integer

    ''' <summary>
    ''' id del almacen
    ''' </summary>
    Public Property WarehouseId As Integer? Implements IConsignmentInventoryRemission.WarehouseId
        Get
            Return INDSleWareHouse.EditValue
        End Get
        Set(value As Integer?)
            INDSleWareHouse.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' tipo de la movimiento
    ''' </summary>
    Public Property MovementType As Integer? Implements IConsignmentInventoryRemission.MovementType
        Get
            Return INDGleMovementType.EditValue
        End Get
        Set(value As Integer?)
            INDGleMovementType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' numero de la remision
    ''' </summary>
    Public Property RemissionNumber As String Implements IConsignmentInventoryRemission.RemissionNumber
        Get
            Return INDTxtReferalNumber.Text
        End Get
        Set(value As String)
            INDTxtReferalNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' detalle de la remision
    ''' </summary>
    Public Property Description As String Implements IConsignmentInventoryRemission.Description
        Get
            Return INDMeDetail.Text
        End Get
        Set(value As String)
            INDMeDetail.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IConsignmentInventoryRemission.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements IConsignmentInventoryRemission.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IConsignmentInventoryRemission.ActionsOnControls
        Set(value As Boolean)
            INDlcConsignmentInventoryRemission.BeginUpdate()

            INDBteCode.Enabled = Not value
            INDDteDate.Enabled = value
            INDSleSupplierDistributionLine.Enabled = value
            INDTxtReferalNumber.Enabled = value
            INDSleWareHouse.Enabled = value
            INDGleMovementType.Enabled = value
            INDMeDetail.Enabled = value
            INDBtnAdd.Enabled = False
            Me.INDSleCurrency.Enabled = value
            INDGcConsignmentInventoryRemission.Enabled = value

            INDlcConsignmentInventoryRemission.EndUpdate()

            If value = False Then
                INDBteCode.Focus()
            Else
                INDDteDate.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    Public Property Sequense As Domain.Entities.InventorySequence Implements IConsignmentInventoryRemission.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As Domain.Entities.InventorySequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.InventorySequenceDetail In Me._sequence.InventorySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de la moneda, recibe como parametro opcional la abreviacion para cuando se postula manualmente el Id
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    ''' <returns></returns>
    Public Property CurrencyId(Optional _currencyAbbreviation As String = Nothing) As Integer Implements IConsignmentInventoryRemission.CurrencyId
        Get
            Return INDSleCurrency.EditValue
        End Get
        Set(value As Integer)
            INDSleCurrency.EditValue = value
            INDSleCurrency.Properties.NullText = _currencyAbbreviation
            SetCurrencyUI(_currencyAbbreviation)
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establce el datasource del combo de moneda
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyDatasource As XPInstantFeedbackSource Implements IConsignmentInventoryRemission.CurrencyDatasource
        Get
            Return TryCast(INDSleCurrency.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCurrency.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el trm
    ''' </summary>
    ''' <returns></returns>
    Private Property TRM As TRM
        Get
            Return _tRM
        End Get
        Set(value As TRM)
            _tRM = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Byte
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            Me.BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Moneda seleccionada carga registro cuando se ha desplegado el combo
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property CurrencySelected As Infrastructure.Data.Xpo.CommonRepository.CommonCurrencyXpo
        Get
            Return TryCast(TryCast(INDGvCurrency.GetFocusedRow, ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonCurrencyXpo)
        End Get
    End Property

    ''' <summary>
    ''' propiedad para tomar la abreviacion de la moneda y guardarla temp para cuando se necesite editar
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property CurrencyAbbreviation As String
        Get
            Return INDSleCurrency.Text
        End Get
    End Property

#End Region

#Region "Datasources"

    ''' <summary>
    ''' datasource de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleSupplierDistributionLine.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleSupplierDistributionLine.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de almacenes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WarehouseXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleWareHouse.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleWareHouse.Properties.DataSource = value
        End Set
    End Property

#End Region

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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

#Region "BUILDER"

    Public Sub New()
        InitializeComponent()
        ctrTmp = New CtrContractTotalInfo()
        ctrTmp.SetInfoFunction(AddressOf getInfoConsignmentInventoryRemission)
        ctrTmp.PrintInfo()
        ctrTmp.TextDiscountValue = "NETO:"
        ctrTmp.TextIvaValue = "IVA:"
        ctrTmp.MaskTotalValue = "c2"
        ctrTmp.INDPceNetValue.Visible = False
        ctrTmp.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
    End Sub

#End Region

#Region "BAR BUTTONS"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso operatingUnit.Id <> Me._idOperativeUnit Then
            Me._idOperativeUnit = operatingUnit.Id
            Await Me.LoadParameters()
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing Then
                If Not Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBteCode.ButtonClick
        Buscar()
    End Sub

    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        Using formulario As New FrmImportInfoForConsignment
            AddHandler formulario.GetListConsignmentInventoryRemissionDetail, AddressOf ReturnGetListConsignmentInventoryRemissionDetail
            Me.Cursor = ChangeCursorIndigo()
            formulario.Size = New Drawing.Size(800, 700)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.SupplierDistributionLineId = SupplierDistributionLineId
            formulario.SupplierId = SupplierId
            formulario.WarehouseId = WarehouseId
            formulario.MovementType = MovementType
            formulario.codeUser = indigo.UserIndigo
            formulario.CurrencyId = Me.CurrencyId
            formulario.ListConsignmentInventoryRemissionDetailValidation = listConsignmentInventoryRemissionDetail
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        varImp = 2
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        SaveOrUpdateAndConfirm(1)
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        SaveOrUpdateAndConfirm(2)
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            consignmentInventoryRemission.Status = 3
            varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, consignmentInventoryRemission.Id, 0, consignmentInventoryRemission.Id)
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

    ''' <summary>
    ''' metodo para obtener lo que se retorna del formulario modal de producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddConsignmentInventoryRemissionDetail(sender As Object, e As AddConsignmentInventoryRemissionDetailEventArgs)
        If listConsignmentInventoryRemissionDetail Is Nothing Then
            INDSleSupplierDistributionLine.Properties.ReadOnly = True
            INDSleWareHouse.Properties.ReadOnly = True
            INDGleMovementType.Properties.ReadOnly = True
            listConsignmentInventoryRemissionDetail = New List(Of ConsignmentInventoryRemissionDetail)
        End If
        If e.EditMode = True Then
            listConsignmentInventoryRemissionDetail.Remove(consignmentInventoryRemissionDetail)
            listConsignmentInventoryRemissionDetail.Insert(indexEditRecord, e.ConsignmentInventoryRemissionDetail)
        ElseIf e.ImportDataMode = True Then
            listConsignmentInventoryRemissionDetail.AddRange(e.ListConsignmentInventoryRemissionDetail)
        Else
            listConsignmentInventoryRemissionDetail.Add(e.ConsignmentInventoryRemissionDetail)
        End If
        INDGcConsignmentInventoryRemission.DataSource = Nothing
        INDGcConsignmentInventoryRemission.DataSource = listConsignmentInventoryRemissionDetail
        ctrTmp.PrintInfo()
        Me.EnableDisableCurrency()
    End Sub

    ''' <summary>
    ''' metodo que obtiene las ordenes de servicio y los contratos que se seleccionaron en el formulario para importar informacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnGetListConsignmentInventoryRemissionDetail(sender As Object, e As GetListConsignmentInventoryRemissionDetailEventArgs)
        Dim listConsignmentInventoryRemissionDetailHandlesBatch As New List(Of ConsignmentInventoryRemissionDetail)
        Dim listConsignmentInventoryRemissionDetailNotHandlesBatch As New List(Of ConsignmentInventoryRemissionDetail)
        Dim dictionaryProduct As Dictionary(Of Integer, Domain.Entities.InventoryProduct) = New Dictionary(Of Integer, InventoryProduct)()
        For Each Item In e.ListConsignmentInventoryRemissionDetail
            Dim product As New InventoryProduct
            If Not dictionaryProduct.ContainsKey(Item.ProductId) Then
                Using model As New MInventoryProduct(Me.Tag)
                    Dim productTmp = model.GetInventoryProductByIdSimpleToGroup(Item.ProductId)
                    dictionaryProduct.Add(Item.ProductId, productTmp)
                End Using
            End If
            product = dictionaryProduct(Item.ProductId)
            If product.ProductSubGroup.HandlesBatch Then
                listConsignmentInventoryRemissionDetailHandlesBatch.Add(Item)
            Else
                Item.CodeNameProduct = product.Code + " - " + product.Name
                Item.ManufacturerName = product.ManufacturerDescription
                Item.HealthRegistration = product.HealthRegistration
                Item.Presentation = product.Presentation
                Item.LastValue = product.FinalProductCost
                If product.IVAId IsNot Nothing Then
                    Using model As New MGeneralLedgerIVA(Me.Tag)
                        Dim iva = model.GetGeneralLedgerIVAById(product.IVAId)
                        Item.IvaPercentage = iva.ObjectEmbbeded.Percentage
                        Item.IvaValue = Math.Round(Item.Quantity * (Item.UnitValue * iva.ObjectEmbbeded.Percentage / 100), 2, MidpointRounding.AwayFromZero)
                    End Using
                End If
                Item.SubTotalValue = Item.UnitValue * Item.Quantity
                Item.TotalValue = Item.SubTotalValue + Item.IvaValue

                'Agregamos el lote así no tenga
                Dim consignmentInventoryRemissionDetailBatchSerial As New ConsignmentInventoryRemissionDetailBatchSerial
                consignmentInventoryRemissionDetailBatchSerial.Quantity = Item.Quantity
                consignmentInventoryRemissionDetailBatchSerial.OutstandingQuantity = Item.Quantity
                'Si es una reposicición
                If Item.RemissionSource = 4 AndAlso Item.ConsignmentInventoryRemissionDetailBatchSerialId IsNot Nothing Then
                    consignmentInventoryRemissionDetailBatchSerial.ConsignmentInventoryRemissionDetailBatchSerialId = Item.ConsignmentInventoryRemissionDetailBatchSerialId
                End If
                Item.ConsignmentInventoryRemissionDetailBatchSerial.Add(consignmentInventoryRemissionDetailBatchSerial)

                listConsignmentInventoryRemissionDetailNotHandlesBatch.Add(Item)
            End If
        Next

        'agregamos los productos que no manejen lote a la rejilla directamente
        If listConsignmentInventoryRemissionDetailNotHandlesBatch IsNot Nothing AndAlso listConsignmentInventoryRemissionDetailNotHandlesBatch.Count > 0 Then
            Dim args As New AddConsignmentInventoryRemissionDetailEventArgs
            args.ImportDataMode = True
            args.ListConsignmentInventoryRemissionDetail = listConsignmentInventoryRemissionDetailNotHandlesBatch
            ReturnAddConsignmentInventoryRemissionDetail(Nothing, args)
        End If

        'abrimos el popup de agregar productos si el producto maneja lote
        If listConsignmentInventoryRemissionDetailHandlesBatch IsNot Nothing AndAlso listConsignmentInventoryRemissionDetailHandlesBatch.Count > 0 Then
            Using formulario As New FrmPopupProductInConsignment(_currency:=New Currency With {.Id = Me.CurrencyId, .Abbreviation = Me.CurrencyAbbreviation},
                                                             _tRMValue:=If(Me.TRM Is Nothing, 1, Me.TRM.Value))
                Me.Cursor = ChangeCursorIndigo()
                AddHandler formulario.AddConsignmentInventoryRemissionDetail, AddressOf ReturnAddConsignmentInventoryRemissionDetail
                formulario.Size = New Drawing.Size(800, 730)
                formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                formulario.ListConsignmentInventoryRemissionDetailImportInfo = listConsignmentInventoryRemissionDetailHandlesBatch
                formulario.ListConsignmentInventoryRemissionDetailValidation = listConsignmentInventoryRemissionDetail
                formulario.WareHouseId = WarehouseId
                formulario.MovementType = MovementType
                formulario.OperatingUnitId = _idOperativeUnit
                formulario.ImportDataMode = True
                Dim transparent = New Base.FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If

    End Sub

#End Region

#Region "HANDLES"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _sequence = Nothing
        _prefixSelected = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        _settingsInventory = Nothing
        presenter = Nothing
        record = Nothing
        consignmentInventoryRemission = Nothing
        consignmentInventoryRemissionDetail = Nothing
        listConsignmentInventoryRemissionDetail = Nothing
        listConsignmentInventoryRemissionDetailDelete = Nothing
        indexEditRecord = Nothing
        _ivaValue = Nothing
        _value = Nothing
        _totalValue = Nothing
        ctrTmp = Nothing
        _action = Nothing
        varImp = Nothing
        flagLoad = Nothing
    End Sub

    Private Async Sub FrmConsignmentInventoryRemission_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LayoutControls.SetIsCustomizable(Me.INDlcConsignmentInventoryRemission, True)

        _doc = Nothing
        _indigoSession = SessionValues.Instance
        presenter = New PConsignmentInventoryRemission(Me)
        presenter.LoadDefinitionLayout()
        presenter.GetSequense()
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Await Me.LoadParameters()

        AddActionsColumns()
        Deshacer()
        LoadStatus()
        RemissionDate = GetDateServer()
    End Sub

#End Region

#Region "Activated"

    Private Sub FrmConsignmentInventoryRemission_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDBteCode.Enabled = True Then
            INDBteCode.Focus()
        End If
    End Sub

#End Region

#Region "FormClosing"

    Private Sub FrmConsignmentInventoryRemission_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    Private Async Sub INDBteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewConsignmentInventoryRemission()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    Private Sub INDMeDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDMeDetail.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDBtnAdd.Focus()
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        If SupplierDistributionLineId Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un proveedor antes de agregar los productos."
            Exit Sub
        End If
        If WarehouseId Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un almacen antes de agregar los productos."
            Exit Sub
        End If
        If MovementType Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione el tipo de movimiento de la remisión antes de agregar los productos."
            Exit Sub
        End If

        Using formulario As New FrmPopupProductInConsignment(_currency:=New Currency With {.Id = Me.CurrencyId, .Abbreviation = Me.CurrencyAbbreviation},
                                                             _tRMValue:=If(Me.TRM Is Nothing, 1, Me.TRM.Value))
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddConsignmentInventoryRemissionDetail, AddressOf ReturnAddConsignmentInventoryRemissionDetail
            formulario.Size = New Drawing.Size(800, 730)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.ListConsignmentInventoryRemissionDetailImportInfo = listConsignmentInventoryRemissionDetail
            formulario.WareHouseId = WarehouseId
            formulario.MovementType = MovementType
            formulario.OperatingUnitId = _idOperativeUnit
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Evento al aceptar dialog
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmNotificationItemDetailConfirm_Accept(sender As Object, e As EventArgs)
        SaveOrUpdateAndConfirm(_action, False)
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDSleSupplier_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleSupplierDistributionLine.QueryPopUp
        If INDSleSupplierDistributionLine.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If SupplierXPO Is Nothing Then
            Using model As New MConsignmentInventoryRemission(MyTag)
                SupplierXPO = model.ListSuppliersDistributionLines()
            End Using
        End If
    End Sub

    Private Sub INDSleWareHouse_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleWareHouse.QueryPopUp
        If INDSleWareHouse.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If WarehouseXPO Is Nothing Then
            Using model As New MConsignmentInventoryRemission(MyTag)
                WarehouseXPO = model.ListWarehouseBySupplier(SupplierId)
            End Using
        End If
    End Sub

    Private Sub INDRptPceBatchSerial_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDRptPceBatchSerial.QueryPopUp
        Dim record = DirectCast(INDGvConsignmentInventoryRemission.GetFocusedRow, ConsignmentInventoryRemissionDetail)
        INDGcBatchSerial.DataSource = Nothing
        INDGcBatchSerial.DataSource = record.ConsignmentInventoryRemissionDetailBatchSerial
    End Sub

    ''' <summary>
    ''' Evento de cargue del datasource de la moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCurrency_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCurrency.QueryPopUp
        If CurrencyDatasource Is Nothing Then
            presenter.InitializeCurrency()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDSleSupplier_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleSupplierDistributionLine.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmSupplier
                OpenFormDialog(form)
                SupplierXPO = Nothing
                INDSleSupplierDistributionLine.Focus()
            End Using
        End If
    End Sub

    Private Sub INDSleWareHouse_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleWareHouse.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmStores
                OpenFormDialog(form)
                WarehouseXPO = Nothing
                INDSleWareHouse.Focus()
            End Using
        End If
    End Sub

#End Region

#Region "Click_ButtonAction"

    ''' <summary>
    ''' Evento que da la opcion de eliminar o modificar los regitros de productos de la orden de traslado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim button As DevExpress.XtraEditors.SimpleButton = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        'Valida que no sea una remision confirmada(2) o anulada(3)
        If Me.Status = 2 OrElse Me.Status = 3 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se puede editar ni eliminar una Remisión Confirmada o Anulada"
            Exit Sub
        End If
        Select Case button.Tag.ToString
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

    ''' <summary>
    ''' Crea el menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        'Valida que no sea una remision confirmada(2) o anulada(3)
        If Me.Status = 2 OrElse Me.Status = 3 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se puede editar ni eliminar una Remisión Confirmada o Anulada"
            Exit Sub
        End If

        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDSleSupplierDistributionLine_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleSupplierDistributionLine.EditValueChanged
        WarehouseXPO = Nothing
        WarehouseId = Nothing
        INDSleWareHouse.Properties.NullText = String.Empty

        If SupplierDistributionLineId IsNot Nothing Then
            ValidateEditValue()
            If SupplierXPO IsNot Nothing Then
                If Not (consignmentInventoryRemission IsNot Nothing AndAlso consignmentInventoryRemission.Id > 0) Then
                    Dim supplierDistributionLine = presenter.GetSupplierBySupplierDistributionLineId(SupplierDistributionLineId)
                    SupplierId = supplierDistributionLine.IdSupplier.Id
                Else
                    SupplierId = consignmentInventoryRemission.SupplierId
                End If
            End If
        Else
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de almacen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleWareHouse_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleWareHouse.EditValueChanged
        Me.MovementType = Nothing
        If WarehouseId Is Nothing Then
            Me.INDGleMovementType.Enabled = False
        Else
            ValidateEditValue()
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("O") Then
                Dim store = If(INDGdvWareHouse.DataSource IsNot Nothing, DirectCast(DirectCast(INDGdvWareHouse.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.InventoryRepository.WarehouseXpo), Nothing)
                Me._idCurrentSequence = Me.GetIdSequenceByPrefix(If(store IsNot Nothing, store.Prefix, Me.consignmentInventoryRemission.Prefix))
                Me._prefixSelected = If(store IsNot Nothing, store.Prefix, Me.consignmentInventoryRemission.Prefix)
            End If

            If consignmentInventoryRemission IsNot Nothing AndAlso consignmentInventoryRemission.Id > 0 AndAlso consignmentInventoryRemission.Status <> 1 Then
                Me.INDGleMovementType.Enabled = False
                Me._listMovementType = New List(Of Tuple(Of Integer, String)) From {
                    New Tuple(Of Integer, String)(1, "Apertura"),
                    New Tuple(Of Integer, String)(2, "Incremento"),
                    New Tuple(Of Integer, String)(3, "Reposición")
                }
            Else
                Me.INDGleMovementType.Enabled = True
                'Verifico que si el almacen ya ha tenido movimientos
                Using model As New MConsignmentInventoryRemission(MyTag)
                    Dim count = model.ValidateChangeWarehouse(WarehouseId)
                    If count > 0 Then
                        Me._listMovementType = New List(Of Tuple(Of Integer, String)) From {
                            New Tuple(Of Integer, String)(2, "Incremento"),
                            New Tuple(Of Integer, String)(3, "Reposición")
                        }
                    Else
                        Me._listMovementType = New List(Of Tuple(Of Integer, String)) From {
                            New Tuple(Of Integer, String)(1, "Apertura")
                        }
                    End If
                End Using
            End If
        End If

        INDGleMovementType.Properties.DataSource = Me._listMovementType
    End Sub

    Private Sub INDGleMovementType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleMovementType.EditValueChanged
        If MovementType IsNot Nothing Then
            ValidateEditValue()
            INDBtnAdd.Enabled = (MovementType <> 3)
        Else
            INDBtnAdd.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' evento cuando la moneda cambia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSleCurrency_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCurrency.EditValueChanged
        If INDSleCurrency.EditValue Is Nothing OrElse Me.CurrencyId = 0 Then
            Exit Sub
        End If

        Await Me.ValidateCurrencyEditValue(Me.flagLoad, Me.CurrencyId)
    End Sub
#End Region

#Region "IdEntityLoaded"

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.consignmentInventoryRemission IsNot Nothing AndAlso Me.consignmentInventoryRemission.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBteCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pitar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmConsignmentInventoryRemission_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDBteCode.Focus()
        INDDteDate.Properties.MaxValue = GetDateServer()
    End Sub

#End Region

#End Region

#Region "METHODS"

#Region "CRUD"

    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If

        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewConsignmentInventoryRemission()
        End If
    End Sub

    ''' <summary>
    ''' metodo para generar secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function NewConsignmentInventoryRemission() As Task

        If Me._settingsInventory Is Nothing OrElse Me._settingsInventory.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", MODULE_NAME)
            Exit Function
        End If

        consignmentInventoryRemission = New ConsignmentInventoryRemission()
        If Not Me._sequence.IsManual Then
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.InventorySequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.InventorySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                            Exit Function
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                End If
            End If
        End If

        Me.ActionsOnControls = True
        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Me.SaveAndConfirmObligatory()

        BarraBotones.StatusRecordVisible = True
        BarraBotones.StatusRecord = "1"
    End Function

    Private Sub SaveAndConfirmObligatory()
        If Not Me.BarraBotones.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.GuardaryConfirmar)) Then
            Exit Sub
        End If
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = Not Me.BarraBotones.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.GuardaryConfirmar))
    End Sub

    ''' <summary>
    ''' carga los controles con la informacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If

            Try
                Using Model As New MConsignmentInventoryRemission(CStr(Me.Tag))
                    AsyncLoader(True)
                    INDlcConsignmentInventoryRemission.BeginUpdate()

                    consignmentInventoryRemission = Await Model.GetConsignmentInventoryRemissionByCode(INDBteCode.Text.Trim)
                    If consignmentInventoryRemission IsNot Nothing AndAlso consignmentInventoryRemission.Id > 0 Then
                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(consignmentInventoryRemission.Id))
                            listConsignmentInventoryRemissionDetail = Model.GetConsignmentInventoryRemissionDetailByConsignmentInventoryRemissionId(consignmentInventoryRemission.Id)

                            If consignmentInventoryRemission.MovementType = 3 Then
                                For Each detail As Domain.Entities.ConsignmentInventoryRemissionDetail In listConsignmentInventoryRemissionDetail
                                    If detail.ConsignmentInventoryRemissionDetailBatchSerialId Is Nothing Then
                                        detail.ConsignmentInventoryRemissionDetailBatchSerialId = detail.ConsignmentInventoryRemissionDetailBatchSerial.FirstOrDefault().ConsignmentInventoryRemissionDetailBatchSerialId
                                    End If
                                Next
                            End If
                            flagLoad = True
                            With consignmentInventoryRemission
                                Me.BarraBotones.StatusRecordVisible = True
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)
                                Me.BarraBotones.SetDocuments(consignmentInventoryRemission.Id, Me.Tag.ToString(), Nothing, GetType(ConsignmentInventoryRemission).Name)

                                Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                _idOperativeUnit = .OperatingUnitId

                                If .Status <> 1 Then
                                    INDDteDate.Properties.MinValue = .RemissionDate
                                End If

                                Code = .Code
                                RemissionDate = .RemissionDate
                                SupplierDistributionLineId = .SupplierDistributionLineId
                                INDSleSupplierDistributionLine.Properties.NullText = .CodeNameSupplier + " - " + .CodeNameDistributionLine
                                INDSleSupplierDistributionLine.Properties.ReadOnly = True
                                SupplierId = .SupplierId
                                RemissionNumber = .RemissionNumber
                                WarehouseId = .WarehouseId
                                INDSleWareHouse.Properties.NullText = .CodeNameWareHouse
                                INDSleWareHouse.Properties.ReadOnly = True
                                MovementType = .MovementType
                                INDGleMovementType.Properties.ReadOnly = True
                                Description = .Description
                                Me.CurrencyId(.Currency?.Abbreviation) = .CurrencyId
                                Me.Status = .Status
                                If .Status = 1 AndAlso .RemissionDate.ToString("dd/MM/yyyy") <> GetServerDate().ToString("dd/MM/yyyy") AndAlso .CurrencyId <> indigo.OfficialCurrencyId Then
                                    If MessageIndigo.Show("La fecha de la Remisión es inferior a la actual. ¿Desea actualizar y recalcular los valores?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                                        Recalculate()
                                    End If
                                End If
                                Await Me.ValidateCurrencyEditValue(False, Me.CurrencyId)
                                BarraBotones.StatusRecord = .Status.ToString()
                            End With

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.consignmentInventoryRemission.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = consignmentInventoryRemission.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If

                            ActionsOnControls = True
                            Select Case consignmentInventoryRemission.Status
                                Case 1
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                                    ValidateEditValue()
                                Case Else
                                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                    INDBtnAdd.Enabled = False
                                    ReadOnlyControls(True)
                            End Select

                            INDGcConsignmentInventoryRemission.DataSource = Nothing
                            INDGcConsignmentInventoryRemission.DataSource = listConsignmentInventoryRemissionDetail
                            ctrTmp.PrintInfo()
                            INDDteDate.Focus()
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, consignmentInventoryRemission.Id, 0, consignmentInventoryRemission.Id)
                        End Using
                        flagLoad = False
                        Me.EnableDisableCurrency()
                    Else
                        If Me._sequence.IsManual Then
                            Await Me.NewConsignmentInventoryRemission()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                            Code = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If

                    AsyncLoader(False)
                    INDlcConsignmentInventoryRemission.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If consignmentInventoryRemission IsNot Nothing AndAlso consignmentInventoryRemission.Status < 3 Then
            If ValidateControls() = True Then
                If INDGvConsignmentInventoryRemission.RowCount = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AddRemissionDetail", MODULE_NAME)
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
            AssigningValues()
        End If

        Try
            Using model As New MConsignmentInventoryRemission(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveConsignmentInventoryRemission(consignmentInventoryRemission, _idCurrentSequence, Me._sequence)
                If result.StateResult = True Then
                    If consignmentInventoryRemission.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            If Me.DicSequense(Me._sequence.InventorySequenceDetail(0).Id).Count > 0 Then
                                Me.DicSequense(Me._sequence.InventorySequenceDetail(0).Id).RemoveAt(0)
                            End If
                        End If
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                    Else
                        If consignmentInventoryRemission.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If

                    Me.consignmentInventoryRemission = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, consignmentInventoryRemission.Id, 0, consignmentInventoryRemission.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, consignmentInventoryRemission.Id, 0, consignmentInventoryRemission.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, consignmentInventoryRemission.Id, 0, consignmentInventoryRemission.Id)
                    End Select

                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    If result.StateResult = False And result.StateResultAux = False Then
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    If consignmentInventoryRemission.Id > 0 Then
                        consignmentInventoryRemission = Await model.GetConsignmentInventoryRemissionByCode(Code)
                    Else
                        consignmentInventoryRemission = New ConsignmentInventoryRemission
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' metodo para guardar y confirmar o para actualizar y confirmar
    ''' </summary>
    ''' <param name="action"></param>
    ''' <remarks></remarks>
    Private Async Sub SaveOrUpdateAndConfirm(action As Integer, Optional controlCost As Boolean = True)
        If ValidateControls() = True Then
            If INDGvConsignmentInventoryRemission.RowCount = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AddRemissionDetail", MODULE_NAME)
                Exit Sub
            End If
        Else
            Exit Sub
        End If

        AssigningValues()

        Try
            Using model As New MConsignmentInventoryRemission(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveAndConfirmConsignmentInventoryRemission(consignmentInventoryRemission, _idCurrentSequence, action, Me._sequence, controlCost)
                If result.StateResult = True And result.StateResultAux = True Then
                    'Se descarta la secuencia numerica usada
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        If Me.DicSequense(Me._sequence.InventorySequenceDetail(0).Id).Count > 0 Then
                            Me.DicSequense(Me._sequence.InventorySequenceDetail(0).Id).RemoveAt(0)
                        End If
                    End If
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    consignmentInventoryRemission = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    If result.MessageResultAux IsNot Nothing AndAlso result.MessageResultAux.Count > 0 Then
                        ViewMessageValidationStock(result.MessageResultAux)
                    End If
                    Me.BarraBotones.PrintReport(PrintReportAction.Confirm, consignmentInventoryRemission.Id, 0, consignmentInventoryRemission.Id)
                    AsyncLoader(False)
                    Me.Deshacer()
                ElseIf result.StateResult = True And result.StateResultAux = False Then
                    If (result.ObjectEmbbeded IsNot Nothing And result.ObjectEmbbeded.Id > 0) Then
                        Code = result.ObjectEmbbeded.Code
                        consignmentInventoryRemission = Await model.GetConsignmentInventoryRemissionByCode(result.ObjectEmbbeded.Code)
                        listConsignmentInventoryRemissionDetail = model.GetConsignmentInventoryRemissionDetailByConsignmentInventoryRemissionId(consignmentInventoryRemission.Id)
                        listConsignmentInventoryRemissionDetailDelete = New List(Of ConsignmentInventoryRemissionDetail)
                    Else
                        consignmentInventoryRemission = New ConsignmentInventoryRemission
                    End If

                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 AndAlso result.MessageResult.First().ToString() <> "" Then
                        If result.MessageResult.First().Contains("porcentaje de variación") Then
                            _action = action
                            Using formulario As New FrmNotificationItemDetailConfirm
                                AddHandler formulario.AcceptMessage, AddressOf FrmNotificationItemDetailConfirm_Accept
                                formulario.TxtMessage.Text = String.Join(vbCrLf, result.MessageResult) & vbCrLf & "¿Desea continuar confirmando este registro?"
                                formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                                Dim transparent = New Base.FrmTransparent(formulario, False)
                                transparent.ShowDialog(Me)
                            End Using
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = String.Join(vbCrLf, result.MessageResult)
                            Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, consignmentInventoryRemission.Id, 0, consignmentInventoryRemission.Id)
                            Me.Deshacer()
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        Me.BarraBotones.PrintReport(PrintReportAction.Confirm, consignmentInventoryRemission.Id, 0, consignmentInventoryRemission.Id)
                        Me.Deshacer()
                    End If
                    AsyncLoader(False)
                ElseIf result.StateResult = False And result.StateResultAux = False Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub Eliminar() Implements Base.ICrudBase.Eliminar

    End Sub

#End Region

#Region "Details"

    ''' <summary>
    ''' Editar el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDetail()
        consignmentInventoryRemissionDetail = DirectCast(INDGvConsignmentInventoryRemission.GetFocusedRow(), ConsignmentInventoryRemissionDetail)
        indexEditRecord = listConsignmentInventoryRemissionDetail.IndexOf(consignmentInventoryRemissionDetail)
        Using formulario As New FrmPopupProductInConsignment(_currency:=New Currency With {.Id = Me.CurrencyId, .Abbreviation = Me.CurrencyAbbreviation},
                                                             _tRMValue:=If(Me.TRM Is Nothing, 1, Me.TRM.Value))
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddConsignmentInventoryRemissionDetail, AddressOf ReturnAddConsignmentInventoryRemissionDetail
            formulario.Size = New Drawing.Size(800, 730)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.ConsignmentInventoryRemissionDetailEdit = consignmentInventoryRemissionDetail
            formulario.EditMode = True
            formulario.WareHouseId = WarehouseId
            formulario.MovementType = MovementType
            formulario.OperatingUnitId = _idOperativeUnit
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Eliminar Detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        consignmentInventoryRemissionDetail = DirectCast(INDGvConsignmentInventoryRemission.GetFocusedRow(), ConsignmentInventoryRemissionDetail)
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If consignmentInventoryRemissionDetail.Id > 0 Then
                If listConsignmentInventoryRemissionDetailDelete Is Nothing Then
                    listConsignmentInventoryRemissionDetailDelete = New List(Of ConsignmentInventoryRemissionDetail)
                End If
                While consignmentInventoryRemissionDetail.ConsignmentInventoryRemissionDetailBatchSerial.Count > 0
                    If consignmentInventoryRemissionDetail.ConsignmentInventoryRemissionDetailBatchSerial(0).Id > 0 Then
                        consignmentInventoryRemissionDetail.ConsignmentInventoryRemissionDetailBatchSerial(0).MarkAsDeleted()
                    Else
                        consignmentInventoryRemissionDetail.ConsignmentInventoryRemissionDetailBatchSerial.Remove(consignmentInventoryRemissionDetail.ConsignmentInventoryRemissionDetailBatchSerial(0))
                    End If
                End While
                consignmentInventoryRemissionDetail.MarkAsDeleted()
                listConsignmentInventoryRemissionDetailDelete.Add(consignmentInventoryRemissionDetail)
            End If
            listConsignmentInventoryRemissionDetail.Remove(consignmentInventoryRemissionDetail)
            If listConsignmentInventoryRemissionDetail.Count = 0 AndAlso consignmentInventoryRemission.Id = 0 Then
                INDSleSupplierDistributionLine.Properties.ReadOnly = False
                INDSleWareHouse.Properties.ReadOnly = False
                INDGleMovementType.Properties.ReadOnly = False
            End If
            INDGcConsignmentInventoryRemission.DataSource = Nothing
            INDGcConsignmentInventoryRemission.DataSource = listConsignmentInventoryRemissionDetail
            ctrTmp.PrintInfo()
        End If
        Me.EnableDisableCurrency()
    End Sub

#End Region
#Region "RecalculateValues"
    Private Async Sub Recalculate()
        ''se actualiza la fecha de la remision
        RemissionDate = GetServerDate()
        Await Me.ValidateCurrencyEditValue(False, Me.CurrencyId)
        '' se actualizan los valores segun trm de cada detalle
        For Each item In listConsignmentInventoryRemissionDetail
            Using model As New MInventoryProduct(Me.Tag)
                Dim product = model.GetInventoryProductByIdSimple(item.ProductId)
                Using modelIva As New MGeneralLedgerIVA(Me.Tag)
                    Dim iva
                    If product.IVAId IsNot Nothing Then
                        iva = modelIva.GetGeneralLedgerIVAById(product.IVAId)
                    End If
                    Using msearch As New MBusqueda
                        Dim _finalProductCost As Decimal = 0
                        _finalProductCost = If(product.FinalProductCost, 0)
                        Dim filter() As Object = {BarraBotones.OperatingUnitValue}
                        Dim settings As XPCollection(Of SettingInventoryXpo) = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetSettingInventoryByOperatingUnit, filter)
                        Dim _tRMValue = If(Me.TRM Is Nothing, 1, Me.TRM.Value)

                        If settings(0).IVACost = True AndAlso iva IsNot Nothing Then
                            item.LastValue = Math.Round((_finalProductCost / _tRMValue) / (1 + (iva.ObjectEmbbeded.Percentage / 100)), 2, MidpointRounding.AwayFromZero)
                        Else
                            item.LastValue = Math.Round((_finalProductCost / _tRMValue), 2, MidpointRounding.AwayFromZero)
                        End If
                        item.UnitValue = item.LastValue
                        If product.IVAId IsNot Nothing Then
                            Using model2 As New MGeneralLedgerIVA(Me.Tag)
                                item.IvaPercentage = iva.ObjectEmbbeded.Percentage
                                item.IvaValue = Math.Round((item.UnitValue * iva.ObjectEmbbeded.Percentage / 100), 2, MidpointRounding.AwayFromZero)
                            End Using
                        End If
                        Dim subtotalTemp = item.UnitValue * item.Quantity
                        Dim ivaTemp = Math.Round((item.IvaValue * item.Quantity), 2, MidpointRounding.AwayFromZero)
                        item.SubTotalValue = subtotalTemp
                        item.TotalValue = item.SubTotalValue + ivaTemp
                    End Using
                End Using
            End Using
        Next
        'For Each item in
    End Sub
#End Region

#Region "Block"

    ''' <summary>
    ''' metodo para generar el registro de bloqueo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub GenerateBlockRecord()
        Using model As New MBlockRecordAndSequense(MyTag)
            Dim result = Await model.GetBlockRecord(Me.Tag, Me.consignmentInventoryRemission.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                record = New BlockRecordInventory With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .FormId = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .RecordId = Me.consignmentInventoryRemission.Id}
                Dim operation = Await model.SaveBlockRecord(record)
                record = operation.ObjectEmbbeded
            Else
                Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                record = result
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(Me.Tag.ToString())
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub

#End Region

#Region "Document"

    Private Function LoadReport() As Task
        Return Task.Factory.StartNew(Sub()
                                         Me.BarraBotones.SafeInvoke(Sub(x) x.PrintReport(PrintReportAction.None, consignmentInventoryRemission.Id, 0, consignmentInventoryRemission.Id))
                                     End Sub)
    End Function

    ''' <summary>
    ''' metodo para generar kla indexacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateDoc() As IndexedDocument2
        Dim content = String.Format(ResourceManager.GetString("FrmConsignmentInventoryRemission_IndexContent", MODULE_NAME), consignmentInventoryRemission.Code, If(INDSleSupplierDistributionLine.Text = String.Empty, INDSleSupplierDistributionLine.Properties.NullText, INDSleSupplierDistributionLine.Text), RemissionDate, If(INDSleWareHouse.Text = String.Empty, INDSleWareHouse.Properties.NullText, INDSleWareHouse.Text))
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = content,
                .CreationDate = dateServer,
                .CreationUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName,
                .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.consignmentInventoryRemission.Code & "#$",
                .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.consignmentInventoryRemission.Code),
                .Update = dateServer,
                .UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName
            Me._doc.Content = content
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.consignmentInventoryRemission.Code)
            Return Me._doc
        End If
    End Function

#End Region

    ''' <summary>
    ''' Método utilizado para cargar los parametros
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadParameters() As Task
        Using Model As New MEntranceVoucher(Me.MyTag)
            _settingsInventory = Await Model.GetSettingInventory(_idOperativeUnit)
            If _settingsInventory Is Nothing OrElse _settingsInventory.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", MODULE_NAME)
                Deshacer()
                Exit Function
            End If
            Me.ValidateDate()
        End Using
    End Function

    ''' <summary>
    ''' Consulta la fecha de los parametros y establece la fecha minima y maxima de la fecha del documento
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateDate()
        If _settingsInventory Is Nothing OrElse _settingsInventory.Id = 0 Then
            Exit Sub
        End If

        Dim dateMin As DateTime = Convert.ToDateTime(_settingsInventory.Year.ToString + "/" + _settingsInventory.Month.ToString + "/01")
        INDDteDate.Properties.MinValue = GetDateServer()
        INDDteDate.Properties.MaxValue = GetDateServer()
    End Sub

    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDGvConsignmentInventoryRemission, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvConsignmentInventoryRemission.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' Obtiene el id del detalle de secuencia por el prefijo seleccionado
    ''' </summary>
    ''' <param name="prefix">Prefijo a buscar</param>
    ''' <returns>Id del detalle de secuencia</returns>
    Private Function GetIdSequenceByPrefix(ByVal prefix As String) As Int64
        If Me._sequence IsNot Nothing AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing AndAlso Me._sequence.InventorySequenceDetail.Any(Function(d) d.Prefix IsNot Nothing AndAlso d.Prefix.Trim().Equals(prefix)) Then
            Return Me._sequence.InventorySequenceDetail.Where(Function(d) d.Prefix IsNot Nothing AndAlso d.Prefix.Trim().Equals(prefix)).FirstOrDefault().Id
        Else
            Return 0
        End If
    End Function

    ''' <summary>
    ''' retorna la informacion que se establece en el control de la barra
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfoConsignmentInventoryRemission() As Tuple(Of String, String, String, String)
        If listConsignmentInventoryRemissionDetail IsNot Nothing AndAlso listConsignmentInventoryRemissionDetail.Count > 0 Then
            _ivaValue = listConsignmentInventoryRemissionDetail.Sum(Function(x) x.IvaValue)
            _value = listConsignmentInventoryRemissionDetail.Sum(Function(x) x.SubTotalValue)
            _totalValue = _ivaValue + _value
        Else
            _ivaValue = 0
            _value = 0
            _totalValue = 0
        End If
        Return New Tuple(Of String, String, String, String)(_ivaValue.ToString("c2"), _value.ToString("c2"), _totalValue.ToString("c2"), "")
    End Function

    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 100},
                              New ColumnInfo With {.Caption = "Tipo de Movimiento", .FieldName = "MovementTypeName", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Proveedor - Linea de Distribución", .FieldName = "DisplaySupplier", .ColumnWidth = 300},
                              New ColumnInfo With {.Caption = "Fecha", .FieldName = "RemissionDate", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Almacen", .FieldName = "WarehouseId.CodeName", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Moneda", .FieldName = "CurrencyAbbreviation", .ColumnWidth = 85},
                              New ColumnInfo With {.Caption = "Total", .FieldName = "TotalValue", .ColumnFormat = "n2", .ColumnFormatType = 3, .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Estado Producto", .FieldName = "ProductStatusName", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = 200}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllConsignmentInventoryRemission
            .FiltroBusqueda = _idOperativeUnit.ToString()
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlcConsignmentInventoryRemission.BeginUpdate()

        _doc = Nothing
        DeleteBlockedRecord()
        ReadOnlyControls(False)
        BarraBotones.EnableBarItems()
        BarraBotones.DisableBarDocument()
        BarraBotones.StatusRecordVisible = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.ValidateDate()

        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Code = String.Empty
        Me.Status = 0
        RemissionDate = GetDateServer()
        SupplierDistributionLineId = Nothing
        INDSleSupplierDistributionLine.Properties.NullText = String.Empty
        INDSleSupplierDistributionLine.Properties.ReadOnly = False
        WarehouseId = Nothing
        INDSleWareHouse.Properties.NullText = String.Empty
        INDSleWareHouse.Properties.ReadOnly = False
        MovementType = Nothing
        INDGleMovementType.Properties.ReadOnly = False
        RemissionNumber = Nothing
        Description = String.Empty
        INDGcConsignmentInventoryRemission.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDGcConsignmentInventoryRemission)
        listConsignmentInventoryRemissionDetail = Nothing
        listConsignmentInventoryRemissionDetailDelete = Nothing
        consignmentInventoryRemission = Nothing
        ActionsOnControls = False
        flagLoad = False
        Me.CurrencyDatasource = Nothing
        Me.CurrencyId(Me.indigo.CurrencyISO4217) = Me.indigo.OfficialCurrencyId
        ctrTmp.PrintInfo()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlcConsignmentInventoryRemission.EndUpdate()
    End Sub

    ''' <summary>
    ''' asigna los valroes a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With consignmentInventoryRemission
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .RemissionDate = RemissionDate
            .OperatingUnitId = _idOperativeUnit
            .SupplierId = SupplierId
            .SupplierDistributionLineId = SupplierDistributionLineId
            .WarehouseId = WarehouseId
            .MovementType = MovementType
            .RemissionNumber = RemissionNumber
            .Description = Description
            .Value = _value
            .IvaValue = _ivaValue
            .TotalValue = _totalValue
            .Prefix = Me._prefixSelected
            .Status = 1
            .ProductStatus = 1
            .CurrencyId = Me.CurrencyId
            For Each item In listConsignmentInventoryRemissionDetail
                .ConsignmentInventoryRemissionDetail.Add(item)
            Next
            If listConsignmentInventoryRemissionDetailDelete IsNot Nothing Then
                For Each item In listConsignmentInventoryRemissionDetailDelete
                    .ConsignmentInventoryRemissionDetail.Add(item)
                Next
            End If

        End With
    End Sub

    ''' <summary>
    ''' Controla el boton de agregar producto
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateEditValue()
        If SupplierDistributionLineId IsNot Nothing AndAlso WarehouseId IsNot Nothing AndAlso MovementType IsNot Nothing Then
            If MovementType <> 3 Then
                INDBtnAdd.Enabled = True
            End If
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
        Else
            INDBtnAdd.Enabled = False
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        End If
    End Sub

    ''' <summary>
    ''' metodo para mostrar los formulario en el evento buttonclik
    ''' </summary>
    ''' <param name="form"></param>
    ''' <remarks></remarks>
    Private Sub OpenFormDialog(form As FormBase)
        form.ViewModeEditHold = True
        form.Size = New Size(800, 700)
        form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        form.MaximizeBox = False
        form.MinimizeBox = False
        Dim transparent = New FrmTransparent(form, False)
        transparent.ShowDialog(Me)
    End Sub

    ''' <summary>
    ''' Metodo para mostar un pop up con los mensajes de validacion por stock
    ''' </summary>
    ''' <param name="messagesValidationStock"></param>
    ''' <remarks></remarks>
    Private Sub ViewMessageValidationStock(messagesValidationStock As List(Of String))
        Using formulario As New FrmPopUpValidateStock
            Me.Cursor = ChangeCursorIndigo()
            formulario.Size = New Drawing.Size(800, 730)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Datasource = messagesValidationStock
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

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
        _culture.NumberFormat = New Globalization.CultureInfo(_currencyAbbreviation.GetCultureId()).NumberFormat
        Me.changeNumericFormatByCurrency(_culture.NumberFormat)
        Me.ctrTmp.CodeISO4217 = _currencyAbbreviation
        ctrTmp.PrintInfo()
    End Sub


    ''' <summary>
    ''' metodo que inactiva o desactiva el control de moneda dependiendo si hay o no registro del detalle cargados
    ''' </summary>
    Private Sub EnableDisableCurrency()
        INDSleCurrency.Enabled = If(listConsignmentInventoryRemissionDetail?.Any(), False, True)
    End Sub


    ''' <summary>
    ''' funcion Valida y establece el evento cuando la moneda cambia 
    ''' </summary>
    ''' <param name="IsLoadControl"></param>
    ''' <returns></returns>
    Private Async Function ValidateCurrencyEditValue(IsLoadControl As Boolean, _currencyId As Integer?) As Task(Of ActionResult)
        If _currencyId Is Nothing OrElse _currencyId = 0 OrElse IsLoadControl OrElse {2, 3}.Contains(Me.Status) Then
            Return New ActionResult With {.StateResult = True}
        End If

        If Me.CurrencySelected IsNot Nothing Then
            Me.SetCurrencyUI(Me.CurrencyAbbreviation)
        End If

        If _currencyId = Me.indigo.OfficialCurrencyId Then
            Me.TRM = New TRM With {.CurrencyId = _currencyId, .OfficialCurrencyId = Me.indigo.OfficialCurrencyId, .Value = 1}
            Return New ActionResult With {.StateResult = True}
        End If

        Dim _stateResult = Await GetTRM(Me.indigo.OfficialCurrencyId, _currencyId)

        If _stateResult Then
            Return New ActionResult With {.StateResult = True}
        End If

        If Not flagLoad Then
            Me.CurrencyId(Me.indigo.CurrencyISO4217) = Me.indigo.OfficialCurrencyId
        End If
        Return New ActionResult With {.StateResult = False}
    End Function

    ''' <summary>
    ''' Funcion que se encarga de consultar el TRM
    ''' </summary>
    ''' <param name="_currencyId"></param>
    ''' <param name="ToCurrencyId"></param>
    ''' <returns></returns>
    Private Async Function GetTRM(_currencyId As Integer, ToCurrencyId As Integer) As Task(Of Boolean)
        Using Model As New MInventoryContract("")
            Dim Result = Await Model.GetTRMbyCurrencyIdAsync(ToCurrencyId, _currencyId, RemissionDate)
            If Result Is Nothing OrElse Not Result?.StateResult Then
                Me.Mensaje(EeventViewerImages.Advertencia) = Result?.Message
                Return False
            End If
            Me.TRM = Result.ObjectEmbbeded
            Me.Mensaje(EeventViewerImages.Informacion) = Result?.Message
            Return True
        End Using
    End Function

#End Region

End Class