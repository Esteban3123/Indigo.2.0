'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Daniel Eduardo Arévalo
' Created          : 10/01/2014
'
' Last Modified By : Diego Andrés Roldán
' Last Modified On : 02-07-2015
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Data.Async.Helpers
Imports DevExpress.Spreadsheet
Imports DevExpress.Xpo
Imports DevExpress.XtraSpreadsheet
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Xpo.Base
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common.MVP
Imports Presentation.Controls
Imports Presentation.Inventory.MVP
Imports Presentation.Maintenance
Imports Presentation.Maintenance.MVP
Imports DevExpress.Images
#End Region

Public Class FrmPurchaseOrder
    Implements IPurchaseOrder, ICustomizableForm

#Region "Builder"
    Public Sub New()
        InitializeComponent()
        INDEsbDetails.AddRangeColumns("Código Producto", "Cantidad", "V. Unitario", "% Descuento")
        OsteosynthesisEquipment = False
        ctrTmp = New CtrContractTotalInfo()
        ctrTmp.SetInfoFunction(AddressOf getInfoServiceOrder)
        ctrTmp.PrintInfo()
        ctrTmp.TextIvaValue = "IVA: "
        ctrTmp.TextDiscountValue = "DTO: "
        ctrTmp.INDPceTotalValue.Size = New Size(292, 52)
        ctrTmp.Dock = System.Windows.Forms.DockStyle.Fill
        ctrTmp.INDPceNetValue.Visible = False
        AdditionalControlPanel.Controls.Add(ctrTmp)
        IndigoGridControl1.SetHideNoRecords(INDGcProducts, True)
    End Sub

    Public Sub New(idDocument As Integer, idDistributionLine As Integer, _warehouseCode As String, listProductsCrystal As List(Of Tuple(Of String, Integer)), MesseageDescription As String)
        InitializeComponent()
        ctrTmp = New CtrContractTotalInfo()
        ctrTmp.SetInfoFunction(AddressOf getInfoServiceOrder)
        ctrTmp.PrintInfo()
        ctrTmp.TextIvaValue = "IVA: "
        ctrTmp.TextDiscountValue = "DTO: "
        ctrTmp.INDPceNetValue.Visible = False
        ctrTmp.INDPceTotalValue.Size = New Size(292, 52)
        ctrTmp.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
        OsteosynthesisEquipment = True
        OsteosynthesisEquipmentId = idDocument
        'HRR PBI3498
        Me.SupplierDistributionLineId = idDistributionLine
        Me._WarehouseCode = _warehouseCode
        Me.Description = MesseageDescription
        ChargeProducts(listProductsCrystal)
    End Sub

    ''' <summary>
    ''' Carga los controls cuando se ejecuta desde el EHR - Material de osteosíntesis
    ''' </summary>
    ''' <remarks>EHR PBI - 3498</remarks>
    Public Sub CargarControles()
        Dim continuar As Boolean = True
        Me.IsBasedContract = 0
        INDSeLookEdContract.EditValue = Nothing
        Me.OrderType = 1
        Me.DocumentDate = GetServerDate()
        Me.DeliveredDate = GetServerDate()

        Me.DeliveryMethod = Nothing
        Me.DeliveryPlace = Nothing
        Dim store As WarehouseXpo = _presenter.GetWarehouseByCode(Me._WarehouseCode)
        If store IsNot Nothing Then
            If store.Inventory_WarehouseUsers.FirstOrDefault(Function(wu) wu.UserCode = indigo.UserIndigo) Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "El usuario no tiene permisos al almacen seleccionado"
                continuar = False
            Else
                INDsleStock.EditValue = store.Id
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = "No se encontro el almacen seleccionado"
            continuar = False
        End If

        If Me.ListProducts IsNot Nothing AndAlso Me.ListProducts.Count > 0 Then 'continuar AndAlso
            RefreshTotals()
            'GenerateAutomaticPurchaseOrder()
        End If
    End Sub

#End Region

#Region "Properties"

    Public Property _WarehouseCode As String

    ''' <summary>
    ''' Obtiene la secuencia numérica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As InventorySequence Implements IPurchaseOrder.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As InventorySequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.InventorySequenceDetail In Me._sequence.InventorySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ReadOnly Property SequenseType As Byte
        Get
            Dim _sequenseType As Byte = 0

            If IsBasedContract Then
                If OrderType = 1 Then
                    _sequenseType = 1
                ElseIf OrderType = 2 Then
                    _sequenseType = 3
                End If
            Else
                If OrderType = 1 Then
                    _sequenseType = 2
                ElseIf OrderType = 2 Then
                    _sequenseType = 4
                End If
            End If

            Return _sequenseType
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IPurchaseOrder.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IPurchaseOrder.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IPurchaseOrder.ActionsOnControls
        Set(value As Boolean)
            INDlyPurchaseOrder.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDbtnCode.Enabled = Not value
            INDdeDateDocument.Enabled = value
            INDdeDateDelivery.Enabled = value
            INDCmbContractBased.Enabled = value
            INDCmbOrderType.Enabled = value
            INDtxtValue.Enabled = value
            INDtxtIvaValue.Enabled = value
            INDtxtTotalValue.Enabled = value
            INDdeDateDocument.Enabled = value
            INDdeDateDelivery.Enabled = value
            INDsleSupplier.Enabled = value
            INDsleStock.Enabled = value
            INDteDeliveryForm.Enabled = value
            INDteDeliveryPlace.Enabled = value
            INDmeDetail.Enabled = value
            INDBtnAddProducts.Enabled = value
            INDSeLookEdContract.Enabled = value
            INDGcProducts.Enabled = value
            INDEsbDetails.Enabled = value
            INDBtnImportFile.Enabled = value
            INDsleCurrency.Enabled = value

            INDsleFunctionalUnitRequest.Enabled = value
            INDsleTypePaymentMethod.Enabled = value

            INDSleBudgetaryEntityId.Enabled = value
            INDSleBudgetaryValidityId.Enabled = value
            INDsleAvailability.Enabled = value
            INDbtnAddAvailability.Enabled = value
            INDgcAvailability.Enabled = value

            BarraBotones.StatusRecordVisible = value
            INDlyPurchaseOrder.EndUpdate()
            If value Then
                INDdeDateDocument.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el código del grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IPurchaseOrder.Code
        Get
            If (INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnCode.Text
            End If
        End Get
        Set(value As String)
            INDbtnCode.Text = value
        End Set
    End Property

    Public Property IsBasedContract As Boolean Implements IPurchaseOrder.IsBasedContract
        Get
            Return INDCmbContractBased.EditValue
        End Get
        Set(value As Boolean)
            INDCmbContractBased.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece el Id de Contrato de Inventarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractId As Integer Implements IPurchaseOrder.ContractId
        Get
            Return INDSeLookEdContract.EditValue
        End Get
        Set(value As Integer)
            INDSeLookEdContract.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece la Fecha de Entrega
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DeliveredDate As DateTime? Implements IPurchaseOrder.DeliveredDate
        Get
            Return INDdeDateDelivery.EditValue
        End Get
        Set(value As DateTime?)
            INDdeDateDelivery.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece la Fecha del Documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As DateTime? Implements IPurchaseOrder.DocumentDate
        Get
            Return INDdeDateDocument.EditValue
        End Get
        Set(value As DateTime?)
            INDdeDateDocument.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece la Forma de Entrega
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DeliveryMethod As String Implements IPurchaseOrder.DeliveryMethod
        Get
            Return INDteDeliveryForm.EditValue
        End Get
        Set(value As String)
            INDteDeliveryForm.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece el lugar de Entrega
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DeliveryPlace As String Implements IPurchaseOrder.DeliveryPlace
        Get
            Return INDteDeliveryPlace.EditValue
        End Get
        Set(value As String)
            INDteDeliveryPlace.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece la Descripción de la Orden de la Compra
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description As String Implements IPurchaseOrder.Description
        Get
            Return INDmeDetail.EditValue
        End Get
        Set(value As String)
            INDmeDetail.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece el Valor de Descuento de la Orden de Compra
    ''' </summary>
    ''' <returns></returns>
    Public Property DiscountValue As Decimal Implements IPurchaseOrder.DiscountValue

    Public Property OrderType As Integer Implements IPurchaseOrder.OrderType
        Get
            Return INDCmbOrderType.EditValue
        End Get
        Set(value As Integer)
            INDCmbOrderType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece los Estados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As String Implements IPurchaseOrder.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As String)
            Me.BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece el Id de la Línea de Distribución del Proveedor
    ''' </summary>
    ''' <returns></returns>
    Public Property SupplierDistributionLineId As Integer? Implements IPurchaseOrder.SupplierDistributionLineId
        Get
            Return INDsleSupplier.EditValue
        End Get
        Set(value As Integer?)
            INDsleSupplier.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece el Id del Proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property _supplierId As Integer
    Public Property SupplierId As Integer Implements IPurchaseOrder.SupplierId
        Get
            Return _supplierId
        End Get
        Set(value As Integer)
            _supplierId = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece el Valor de la Orden de Compra
    ''' </summary>
    ''' <returns></returns>
    Public Property Value As Decimal Implements IPurchaseOrder.Value
        Get
            Return INDtxtValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece el Valor de IVA de la Orden de Compra
    ''' </summary>
    ''' <returns></returns>
    Public Property IvaValue As Decimal Implements IPurchaseOrder.IvaValue
        Get
            Return INDtxtIvaValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtIvaValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece el Valor Total de la Orden de Compra
    ''' </summary>
    ''' <returns></returns>
    Public Property TotalValue As Decimal Implements IPurchaseOrder.TotalValue
        Get
            Return INDtxtTotalValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtTotalValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' bandera que Especifica que fue creado desde el formulario de material osteosíntesis de cristal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OsteosynthesisEquipment As Boolean Implements IPurchaseOrder.OsteosynthesisEquipment

    ''' <summary>
    ''' Obtiene o establece el id de la cabecera de la orden de procedimientos QX(HCORDPROQ)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OsteosynthesisEquipmentId As Integer? Implements IPurchaseOrder.OsteosynthesisEquipmentId

    ''' <summary>
    ''' Obtiene o Establece el Id del Almacén
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WarehouseId As Integer? Implements IPurchaseOrder.WarehouseId
        Get
            Return INDsleStock.EditValue
        End Get
        Set(value As Integer?)
            INDsleStock.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource del control de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListSupplier As XPInstantFeedbackSource Implements IPurchaseOrder.ListSupplier
        Get
            Return INDsleSupplier.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleSupplier.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la Lista de Líneas de Distribución
    ''' </summary>
    ''' <returns></returns>
    Public Property SuppliersDistributionLinesXpo As XPInstantFeedbackSource Implements IPurchaseOrder.SuppliersDistributionLinesXpo
        Get
            Return CType(INDsleSupplier.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleSupplier.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los productos
    ''' </summary>
    ''' <remarks></remarks>
    Property ListProducts As Domain.Entities.TrackableCollection(Of PurchaseOrderDetail)
        Get
            Return INDGcProducts.DataSource
        End Get
        Set(value As Domain.Entities.TrackableCollection(Of PurchaseOrderDetail))
            INDGcProducts.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece los Almacenes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListWarehouse As XPInstantFeedbackSource Implements IPurchaseOrder.ListWarehouse
        Get
            Return INDsleStock.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleStock.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la Lista de Contratos de Inventarios
    ''' </summary>
    ''' <returns></returns>
    Public Property ListContractInventory As XPInstantFeedbackSource Implements IPurchaseOrder.ListContractInventory
        Get
            Return INDSeLookEdContract.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSeLookEdContract.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de la moneda, recibe como parametro opcional la abreviacion para cuando se postula manualmente el Id
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    ''' <returns></returns>
    Public Property CurrencyId(Optional _currencyAbbreviation As String = Nothing) As Integer Implements IPurchaseOrder.CurrencyId
        Get
            Return INDsleCurrency.EditValue
        End Get
        Set(value As Integer)
            INDsleCurrency.EditValue = value
            INDsleCurrency.Properties.NullText = _currencyAbbreviation
            SetCurrencyUI(_currencyAbbreviation)
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establce el datasource del combo de moneda
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyDatasource As XPInstantFeedbackSource Implements IPurchaseOrder.CurrencyDatasource
        Get
            Return TryCast(INDsleCurrency.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCurrency.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Moneda seleccionada carga registro cuando se ha desplegado el combo
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property CurrencySelected As Infrastructure.Data.Xpo.CommonRepository.CommonCurrencyXpo
        Get
            Return TryCast(TryCast(INDGvCurrencyAdvance.GetFocusedRow, ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonCurrencyXpo)
        End Get
    End Property

    ''' <summary>
    ''' propiedad para tomar la abreviacion de la moneda y guardarla temp para cuando se necesite editar
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property CurrencyAbbreviation As String
        Get
            Return INDsleCurrency.Text
        End Get
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
    ''' Obtiene o Establece la forma de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TypePaymentMethod As Integer? Implements IPurchaseOrder.TypePaymentMethod
        Get
            Return INDsleTypePaymentMethod.EditValue
        End Get
        Set(value As Integer?)
            INDsleTypePaymentMethod.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece la la unidad funcioanl requerida
    ''' </summary>C:\Project\ERP_Master\ERP_Presentation\Infrastructure.Data.Xpo.InventoryRepository\Relation\DistributionLinesXpo.cs
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FunctionalUnitRequestId As Integer? Implements IPurchaseOrder.FunctionalUnitRequestId
        Get
            Return INDsleFunctionalUnitRequest.EditValue
        End Get
        Set(value As Integer?)
            INDsleFunctionalUnitRequest.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece los dias de plazo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DaysTerm As Integer? Implements IPurchaseOrder.DaysTerm
        Get
            Return INDteDaysTerm.EditValue
        End Get
        Set(value As Integer?)
            INDteDaysTerm.EditValue = value
        End Set
    End Property

#Region "Budget Interface"

    Public ReadOnly Property BudgetInterface As Boolean
        Get
            Dim _budgetInterface As Boolean

            If PaymentsSettingPaymentsXpo IsNot Nothing AndAlso PaymentsSettingPaymentsXpo.BudgetInterface Then
                If Not INDCmbContractBased.EditValue Then
                    _budgetInterface = True
                End If
            End If

            Return _budgetInterface
        End Get
    End Property

    Public ReadOnly Property CommitmentBudgetInterface As Boolean
        Get
            Return If(InventorySettingsXpo Is Nothing, False, InventorySettingsXpo.CommitmentBudgetInterface)
        End Get
    End Property

    Public Property BudgetaryEntityId As Integer?
        Get
            Return If(String.IsNullOrEmpty(INDSleBudgetaryEntityId.EditValue), Nothing, INDSleBudgetaryEntityId.EditValue)
        End Get
        Set(value As Integer?)
            INDSleBudgetaryEntityId.EditValue = value
        End Set
    End Property

    Public Property BudgetaryEntityXpo As DevExpress.Xpo.XPCollection Implements IPurchaseOrder.BudgetaryEntityXpo
        Get
            Return INDSleBudgetaryEntityId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDSleBudgetaryEntityId.Properties.DataSource = value
        End Set
    End Property

    Public Property BudgetaryValidityId As Integer?
        Get
            Return If(String.IsNullOrEmpty(INDSleBudgetaryValidityId.EditValue), Nothing, INDSleBudgetaryValidityId.EditValue)
        End Get
        Set(value As Integer?)
            INDSleBudgetaryValidityId.EditValue = value
        End Set
    End Property

    Public Property BudgetaryValidityXpo As XPCollection Implements IPurchaseOrder.BudgetaryValidityXpo
        Get
            Return INDSleBudgetaryValidityId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDSleBudgetaryValidityId.Properties.DataSource = value
        End Set
    End Property

    Public Property AvailabilityXpo As XPInstantFeedbackSource Implements IPurchaseOrder.AvailabilityXpo
        Get
            Return INDsleAvailability.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAvailability.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Listado para disponibilidades asociadas al contrato
    ''' </summary>
    ''' <remarks></remarks>
    Public Property ListPurchaseOrderAvailability As List(Of PurchaseOrderAvailability) Implements IPurchaseOrder.ListPurchaseOrderAvailability
        Get
            Return INDgcAvailability.DataSource
        End Get
        Set(value As List(Of PurchaseOrderAvailability))
            INDgcAvailability.DataSource = value
            INDgcAvailability.RefreshDataSource()
        End Set
    End Property

    ''' <summary>
    ''' Representa el listado de detalles de disponibilidades
    ''' </summary>
    Private ListDeletePurchaseOrderAvailability As List(Of PurchaseOrderAvailability)

#End Region

#End Region

#Region "Variables"

    ''' <summary>
    ''' Control para establecer información del ingreso
    ''' </summary>
    Private ctrTmp As CtrContractTotalInfo

    ''' <summary>
    ''' Representa el presentador 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _presenter As PPurchaseOrder

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const MODULE_NAME As String = "Inventory"

    ''' <summary>
    ''' Secuencia numérica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.InventorySequence

    ''' <summary>
    ''' Prefijo seleccionado
    ''' </summary>
    Private _prefixSelected As String

    ''' <summary>
    ''' Representa la entidad de ordenes de compra
    ''' </summary>
    ''' <remarks></remarks>
    Private _purchaseOrder As PurchaseOrder

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuración de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordInventory

    ''' <summary>
    ''' Representa a la entidad de parámetros de cxp
    ''' </summary>
    Public PaymentsSettingPaymentsXpo As PaymentsSettingPaymentsXpo

    ''' <summary>
    ''' Representa a la entidad de parámetros de inventario
    ''' </summary>
    Public InventorySettingsXpo As SettingInventoryXpo

    Dim OnlyRead As Boolean = False

    ''' <summary>
    ''' Variable que contiene un ítem del detalle
    ''' </summary>
    ''' <remarks></remarks>
    Dim ItemPurchaseOrderDetail As PurchaseOrderDetail

    ''' <summary>
    ''' Permite controlar cuando se carga desde el loadControls
    ''' </summary>
    Dim _isLoading As Boolean

    Private _advanceValue As Decimal

    Dim ctrAdvance As CtrAdvanceTreasury

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer
    ''' <summary>
    ''' bandera para saber si se esta importando desde una interfaz
    ''' </summary>
    ''' <remarks></remarks>
    Dim _flagInterface As Boolean = True
    ''' <summary>
    ''' código del proveedor de la orden de interfazada
    ''' </summary>
    ''' <remarks></remarks>
    Dim _supplierIdentification As String

    ''' <summary>
    ''' Listado para validar si el detalle esta cruzada
    ''' </summary>
    Private ListViewValidateQuantityPurchaseOrderDetailXpo As List(Of ViewValidateQuantityPurchaseOrderDetailXpo)

    ''' <summary>
    ''' Listado del CopyPaste
    ''' </summary>
    Dim ListCopyPaste As List(Of List(Of String))

    ''' <summary>
    ''' tasa de cambio
    ''' </summary>
    Private _tRM As TRM

    ''' <summary>
    ''' tasa de cambio
    ''' </summary>
    Private _roundingType As Decimal = 1

    ''' <summary>
    ''' Variable que contiene la lista de tipos de libro contable
    ''' </summary>
    Dim ListPaymentForm As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Obtiene o establece el proveedor seleccionado
    ''' </summary>
    Private SupplierData As Supplier
#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing
        _presenter = Nothing
        _sequence = Nothing
        _prefixSelected = Nothing
        _purchaseOrder = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        record = Nothing
        OnlyRead = Nothing
        ItemPurchaseOrderDetail = Nothing
        _isLoading = Nothing
        _advanceValue = Nothing
        ctrAdvance = Nothing
        varImp = Nothing
        _flagInterface = Nothing
        _supplierIdentification = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmPurchaseOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyPurchaseOrder, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        ChangeIconButtonPurchaseOrder(_idOperativeUnit)

        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PPurchaseOrder(Me)
        Me.LoadParameters()
        Await _presenter.GetSequense()
        Me.CurrencyId(Me.indigo?.CurrencyISO4217) = Me.indigo?.OfficialCurrencyId
        ' Personalización de la rejilla, muestra las columnas ocultas como mas información
        IndigoGridView1.MoreInfoColunmns(INDGvProducts)

        ' Agrega a la rejilla la columna de Acciones

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Edit)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvProducts, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvProducts.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
        IndigoGridControl1.RefreshGrid(INDGcProducts)
        InitializeTuples()
        FunctionalUnitRequest_GetData()
        IndigoGridView2.SetListAcction(INDviewAvailability, {eAcciones.Remove}.ToList)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewAvailability.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        'se muestran los campos de unidad funcional y dias de plazo solo para CR
        If indigo.LanguageCulture = "es-CR" Then
            INDlyItemFunctionalUnitRequest.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemDaysTerm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If

        If OsteosynthesisEquipment Then
            Await NewPurchaseOrder()
            Dim _supplier As String = String.Empty
            _supplier = _presenter.GetSupplierDistributionLine(SupplierDistributionLineId).FirstOrDefault?.IdSupplier?.CodeName
            INDsleSupplier.Properties.NullText = _supplier
            INDBtnAddProducts.Enabled = False

            'EHR PBI-3498
            INDsleSupplier.Enabled = False
            INDsleStock.Enabled = False
            INDsleStock_QueryPopUp(Nothing, Nothing)
            CargarControles()
        Else
            LoadStatus()
            Deshacer()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al seleccionar un registro desde el Vituel
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'Me.ViewModeEditHold = True
        If Me._purchaseOrder IsNot Nothing AndAlso Me._purchaseOrder.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Método que retorna los registros importados al formulario de agregar productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnGetListPurchaseOrderDetail(sender As Object, e As AddProductPurchaseOrder)
        Dim errors As New StringBuilder
        Dim dictionaryProduct As Dictionary(Of Integer, InventoryProduct) = New Dictionary(Of Integer, InventoryProduct)()
        Dim dictionaryIva As Dictionary(Of Integer, ActionResult(Of GeneralLedgerIVA)) = New Dictionary(Of Integer, ActionResult(Of GeneralLedgerIVA))()

        Dim product
        Dim productTmp
        Dim iva
        Dim ivaTmp
        Using model As New MInventoryProduct(Me.Tag)
            Using modelIva As New MGeneralLedgerIVA(Me.Tag)
                For Each Item In e.ListPurchaseOrderDetail
                    product = New InventoryProduct
                    If Not dictionaryProduct.ContainsKey(Item.ProductId) Then
                        productTmp = model.GetInventoryProductByIdSimpleToGroup(Item.ProductId)
                        dictionaryProduct.Add(Item.ProductId, productTmp)
                    End If
                    product = dictionaryProduct(Item.ProductId)
                    If product.FinalProductCost Is Nothing OrElse product.FinalProductCost = 0 Then
                        errors.AppendLine(String.Format(ResourceManager.GetString("FinalProductCostZero", MODULE_NAME), product.Code + " - " + product.Name))
                        Continue For
                    End If


                    Item.InventoryProduct = product
                    Item.OutstandingQuantity = Item.Quantity
                    Item.CancelledQuantity = 0
                    Item.Value = Math.Round(product.FinalProductCost / Me._tRM.Value, 2, MidpointRounding.AwayFromZero)
                    Item.IvaPercentage = 0
                    If product.IVAId IsNot Nothing Then
                        iva = New ActionResult(Of GeneralLedgerIVA)
                        If Not dictionaryIva.ContainsKey(product.IVAId) Then
                            ivaTmp = modelIva.GetGeneralLedgerIVAById(product.IVAId)
                            dictionaryIva.Add(product.IVAId, ivaTmp)
                        End If
                        iva = dictionaryIva(product.IVAId)
                        Item.IvaPercentage = iva.ObjectEmbbeded.Percentage
                    End If

                    Item.SubTotalValue = Utils.RoundValue(Item.Value * Item.Quantity, _roundingType)
                    Item.DiscountValue = Utils.RoundValue(Item.SubTotalValue * Item.DiscountPercentage / 100, 2)
                    Item.IvaValue = Utils.RoundValue(CDec((Item.SubTotalValue - Item.DiscountValue) * (Item.IvaPercentage / 100)), 2)
                    Item.TotalIva = Utils.RoundValue(CDec(Item.SubTotalValue * (Item.IvaPercentage / 100)), 2)
                    Item.TotalValue = Item.SubTotalValue + Item.IvaValue - Item.DiscountValue
                    ListProducts.Add(Item)
                Next
            End Using
        End Using
        RefreshTotals()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString
        End If

    End Sub
#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de código
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbtnCode.KeyDown
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
                    Await Me.NewPurchaseOrder()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub
#End Region

#Region "ButtonClick"
    Private Sub INDsleSupplier_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSupplier.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmSupplier()
                OpenFormDialog(form)
            End Using
        End If
    End Sub

    Private Sub INDsleStock_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleStock.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmStores()
                OpenFormDialog(form)
            End Using
        End If
    End Sub
#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor que indica si es basado en un contrato
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDCmbContractBased_EditValueChanged(sender As Object, e As EventArgs) Handles INDCmbContractBased.EditValueChanged
        If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("TO") Then
            Me._idCurrentSequence = Me.GetIdSequenceByType()
        End If
        Me.ContractId = Nothing
        Me.EnableOrDisableCurrecy()
    End Sub


    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de contratos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSeLookEdContract_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeLookEdContract.EditValueChanged
        If INDSeLookEdContract.EditValue IsNot Nothing AndAlso INDSeLookEdContract.EditValue <> 0 AndAlso Not _isLoading Then
            Dim contract = TryCast(INDSeLookEdContract.GetSelectedObject(), Infrastructure.Data.Xpo.InventoryRepository.InventoryContractXpo)
            If contract Is Nothing Then
                contract = _presenter.GetContractById(ContractId)
            End If

            _supplierId = contract.SupplierId.Id
            INDsleSupplier.Properties.DataSource = Nothing
            Me.CurrencyId(contract?.CurrencyAbbreviation) = contract?.CurrencyId

            Dim _stateResult = Await Me.ValidateCurrencyEditValue(False, contract?.CurrencyId)
            If Not _stateResult?.StateResult Then
                Me.CurrencyId(Me.indigo.CurrencyISO4217) = Me.indigo.OfficialCurrencyId
                Me.ContractId = Nothing
                Exit Sub
            End If

            If contract.ManageProducts Then
                INDGvProducts.ShowLoadingPanel()
                Await Task.Factory.StartNew(AddressOf LoadProductsOfContract)
            End If
            EnableOrDisableCurrecy()
        End If
    End Sub

    ''' <summary>
    ''' Método que carga los productos traidos desde el contrato
    ''' </summary>
    Private Sub LoadProductsOfContract()
        CheckForIllegalCrossThreadCalls = False

        If ListProducts IsNot Nothing AndAlso ListProducts.Count > 0 Then
            While ListProducts.Count > 0 AndAlso (From x In ListProducts Where x.Id > 0).Count > 0
                Dim entity = ListProducts(0)
                ListProducts.Remove(entity)
                entity.MarkAsDeleted()
            End While
        End If
        ListProducts = Nothing

        Dim ListInventoryContractDetailXpo = _presenter.GetListProductsOfInventoryContract(INDSeLookEdContract.EditValue)

        If ListInventoryContractDetailXpo IsNot Nothing AndAlso ListInventoryContractDetailXpo.Count > 0 Then
            ListProducts = New Domain.Entities.TrackableCollection(Of PurchaseOrderDetail)

            For Each itemXpo In ListInventoryContractDetailXpo
                Dim entity = New PurchaseOrderDetail()
                With entity
                    .ProductId = itemXpo.ProductId.Id
                    .ProductCode = itemXpo.ProductId.Code
                    .ProductName = itemXpo.ProductId.Name
                    .Quantity = itemXpo.OutstandingQuantity
                    .OutstandingQuantity = itemXpo.OutstandingQuantity
                    .CancelledQuantity = itemXpo.CancelledQuantity
                    .Value = itemXpo.Value
                    .SubTotalValue = Utils.RoundValue(itemXpo.OutstandingQuantity * itemXpo.Value, _roundingType)
                    .IvaPercentage = itemXpo.IvaPercentage
                    .IvaValue = itemXpo.IvaValue
                    .DiscountPercentage = itemXpo.DiscountPercentage
                    .DiscountValue = itemXpo.DiscountValue
                    .TotalValue = .SubTotalValue + .IvaValue - .DiscountValue
                    .OrderSource = 2
                    .SourceCode = itemXpo.InventoryContractId.Code
                End With
                ListProducts.Add(entity)
            Next
        End If

        INDBtnAddProducts.Enabled = False
        INDEsbDetails.Enabled = False
        INDBtnImportFile.Enabled = False
        INDGvProducts.HideLoadingPanel()
        RefreshTotals()
    End Sub

    Private Sub ImageComboBoxEdit1_SelectedValueChanged(sender As Object, e As EventArgs) Handles INDCmbContractBased.SelectedValueChanged
        INDlyItemContract.Visibility = If(INDCmbContractBased.EditValue, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDsleSupplier.Properties.DataSource = Nothing
        ShowHideBudgetInterface()
    End Sub

    Private Sub INDsleSupplier_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSupplier.EditValueChanged
        If SupplierDistributionLineId > 0 Then
            If _isLoading = False Then
                Using model As New MPurchaseOrder(Me.Tag)
                    SupplierId = model.GetSupplierIdByIdDistributionLines(SupplierDistributionLineId)
                End Using
            End If
        Else
            SupplierId = 0
        End If

        ''Obtengo los dias de plazo que tiene el proveedor asignados
        If SupplierId > 0 Then
            DaysTermGetData(SupplierId)
        End If
        HideButtonImportInformation()
    End Sub

    Private Sub DaysTermGetData(SupplierId As Integer)
        ''valida si es de contado o no
        If TypePaymentMethod = 1 Then
            DaysTerm = 0
        Else
            Using modelSupplier As New MSupplier(Me.Tag)
                SupplierData = modelSupplier.GetSupplierById(SupplierId)
                DaysTerm = SupplierData.TimeLimitDays
            End Using
        End If
    End Sub


    Private Sub INDsleTypePaymentMethod_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleTypePaymentMethod.EditValueChanged
        If SupplierId > 0 Then
            DaysTermGetData(SupplierId)
        End If
    End Sub

    Private Sub INDCmbOrderType_EditValueChanged(sender As Object, e As EventArgs) Handles INDCmbOrderType.EditValueChanged
        If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("TO") Then
            Me._idCurrentSequence = Me.GetIdSequenceByType()
        End If

        If OrderType = 2 Then
            INDLciValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciIvaValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciTotalValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemStock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyGpProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            WarehouseId = Nothing

            If ListProducts IsNot Nothing Then
                For Each detail In ListProducts.ToList()
                    If detail.Id > 0 Then
                        detail.MarkAsDeleted()
                    End If
                Next
            End If
        Else
            INDLciValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciIvaValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciTotalValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemStock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyGpProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
        Me.EnableOrDisableCurrecy()
        HideButtonImportInformation()
    End Sub

    Private Sub HideButtonImportInformation()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = Not (Not IsBasedContract AndAlso OrderType = 1 AndAlso SupplierId > 0 AndAlso WarehouseId.GetValueOrDefault > 0)
    End Sub

    Private Sub INDsleStock_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleStock.EditValueChanged
        If Me.INDsleStock.EditValue IsNot Nothing AndAlso Me.INDsleStock.EditValue > 0 Then
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("O") Then
                Dim store = _presenter.GetWarehouseById(WarehouseId)
                Me._idCurrentSequence = Me.GetIdSequenceByPrefix(If(store IsNot Nothing, store.Prefix, Me._purchaseOrder.Prefix))
                Me._prefixSelected = If(store IsNot Nothing, store.Prefix, Me._purchaseOrder.Prefix)
            End If
        End If
        HideButtonImportInformation()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la fecha del documento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDdeDateDocument_EditValueChanged(sender As Object, e As EventArgs) Handles INDdeDateDocument.EditValueChanged
        If INDdeDateDocument.EditValue IsNot Nothing Then
            ValidateDate()
        End If
    End Sub

    Private Sub INDsleBudgetaryEntityId_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleBudgetaryEntityId.EditValueChanged
        CleanBudgetInterface(1)

        If BudgetaryEntityId IsNot Nothing Then
            _presenter.InitializeBudgetaryValidity(BudgetaryEntityId)
            SetFirstOrDefaultValidity()
        End If
    End Sub

    Private Sub INDsleBudgetaryValidityId_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleBudgetaryValidityId.EditValueChanged
        CleanBudgetInterface(2)

        If BudgetaryValidityId IsNot Nothing Then
            _presenter.InitializeAvailabilityDetails(BudgetaryValidityId)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de una orden de servicio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtxtValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtValue.EditValueChanged, INDtxtIvaValue.EditValueChanged
        If OrderType = 2 Then
            TotalValue = Value + IvaValue
        End If
        ctrTmp.PrintInfo()
    End Sub

    ''' <summary>
    ''' evento cuando se cambia la moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDsleCurrency_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCurrency.EditValueChanged
        Await Me.ValidateCurrencyEditValue((Me._isLoading OrElse Me.IsBasedContract), Me.CurrencyId)

    End Sub
#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor a ejecutar en la rejilla de disponibilidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepTxtValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepTxtValue.EditValueChanging
        If e IsNot Nothing AndAlso e.NewValue IsNot Nothing Then
            Dim entity As PurchaseOrderAvailability = INDviewAvailability.GetFocusedRow()
            If CDec(e.NewValue) > CDec(entity.Balance) Then
                Mensaje(EeventViewerImages.Advertencia) = "El valor a ejecutar no puede ser mayor al saldo de la disponibilidad"
                e.Cancel = True
                Exit Sub
            End If
            entity.Value = e.NewValue
        End If
    End Sub

#End Region

#Region "ImportarInformacion"
    ''' <summary>
    ''' Evento que dispara el formulario de importar productos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        Using formulario As New FrmPurchaseOrderImport
            AddHandler formulario.GetListPurchaseOrderDetail, AddressOf ReturnGetListPurchaseOrderDetail
            Me.Cursor = ChangeCursorIndigo()
            formulario.Size = New System.Drawing.Size(800, 700)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent

            If ListProducts Is Nothing Then
                ListProducts = New Domain.Entities.TrackableCollection(Of PurchaseOrderDetail)
            End If
            formulario.ListPurchaseOrderDetailValidation = ListProducts.ToList()
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub
#End Region

#Region "FromClosing"
    ''' <summary>
    ''' Se dispara cuando se cierra el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPurchaseOrder_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "Activated"
    Private Sub FrmPurchaseOrder_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDbtnCode.Text Is String.Empty Then
            INDbtnCode.Focus()
        End If
    End Sub
#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Realiza la consulta del combo de Proveedores
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSupplier_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSupplier.QueryPopUp
        If INDsleSupplier.Properties.DataSource Is Nothing Then
            Dim SupplierId As Integer? = Nothing
            If IsBasedContract AndAlso Me._supplierId > 0 Then
                SupplierId = _supplierId
            End If
            _presenter.InitializeSupplier(SupplierId)
        End If
    End Sub

    Private Sub INDsleSupplier_Popup(sender As Object, e As EventArgs) Handles INDsleSupplier.Popup
        If _flagInterface AndAlso Not String.IsNullOrEmpty(_supplierIdentification) Then
            viewSupplier.ShowLoadingPanel()
            Dim search = CType(sender, DevExpress.XtraEditors.SearchLookUpEdit)
            Dim view = search.Properties.View
            Using model As New MThirdParty(MyTag)
                Dim third = model.GetThirdParty(_supplierIdentification)
                If third IsNot Nothing AndAlso third.Id > 0 Then
                    view.ActiveFilterString = "IdSupplier.IdThirdParty.Id =" & third.Id
                    view.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
                End If
            End Using
            viewSupplier.HideLoadingPanel()
        End If
    End Sub

    ''' <summary>
    ''' Realiza la consulta del combo de Almacenes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleStock_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleStock.QueryPopUp
        If INDsleStock.Properties.DataSource Is Nothing Then
            _presenter.LoadWarehouse()
        End If
    End Sub

    ''' <summary>
    ''' Realiza la consulta del combo de Contratos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSeLookEdContract_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSeLookEdContract.QueryPopUp
        If INDSeLookEdContract.Properties.DataSource Is Nothing Then
            _presenter.LoadContractInventory()
        End If
    End Sub

    Private Sub INDsleBudgetaryEntityId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleBudgetaryEntityId.QueryPopUp
        _presenter.InitializeBudgetaryEntity()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de disponibilidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleAvailability_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAvailability.QueryPopUp
        If BudgetaryEntityId IsNot Nothing Then
            _presenter.InitializeAvailabilityDetails(BudgetaryEntityId)
        End If
    End Sub

    ''' <summary>
    ''' Evento para consultar las monedas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCurrency_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCurrency.QueryPopUp
        If Me.CurrencyDatasource Is Nothing Then
            _presenter.InitializeCurrency()
        End If
    End Sub

    ''' <summary>
    ''' evento que consulta las unidades funcionales asignadas al usuario
    ''' </summary>
    Private Sub FunctionalUnitRequest_GetData()
        INDsleFunctionalUnitRequest.Properties.DataSource = _presenter.InitializeFunctionalUnit(indigo.UserIndigoId, indigo.UserIndigo)
    End Sub

    ''' <summary>
    ''' Evento que consulta las unidades funcionales 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleFunctionalUnitRequest_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFunctionalUnitRequest.QueryPopUp
        If INDsleFunctionalUnitRequest.Properties.DataSource Is Nothing Then
            FunctionalUnitRequest_GetData()
        End If
    End Sub
#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de agregar la disponibilidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddAvailability_Click(sender As Object, e As EventArgs) Handles INDbtnAddAvailability.Click
        If INDsleAvailability.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una disponibilidad"
            INDsleAvailability.Focus()
            Exit Sub
        End If

        If ListPurchaseOrderAvailability Is Nothing Then
            ListPurchaseOrderAvailability = New List(Of PurchaseOrderAvailability)
        End If

        If (From x In ListPurchaseOrderAvailability Where x.AvailabilityDetailId = INDsleAvailability.EditValue Select x).Count() > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "La disponibilidad " + INDsleAvailability.Text + " ya existe en la lista"
            INDsleAvailability.Focus()
            Exit Sub
        End If

        Dim viewXpo As ViewListAvailabilityDetailXpo = DirectCast(INDsleAvailability.GetSelectedObject(), ViewListAvailabilityDetailXpo)
        If viewXpo Is Nothing Then
            viewXpo = _presenter.GetAvailabilityDetailById(INDsleAvailability.EditValue)
        End If

        Dim purchaseOrderAvailability = New PurchaseOrderAvailability()
        With purchaseOrderAvailability
            .AvailabilityDetailId = viewXpo.Id
            .AvailabilityCode = viewXpo.AvailabilityCode
            .CategoryCodeName = viewXpo.CategoryCodeName
            .FinancialSourceCodeName = viewXpo.FinancialSourceCodeName
            .RevenueTypeCodeName = viewXpo.RevenueTypeCodeName
            .Balance = viewXpo.Balance
            .Value = 0
        End With

        ListPurchaseOrderAvailability.Add(purchaseOrderAvailability)
        EnableBudgetInterface()
        INDgcAvailability.RefreshDataSource()

        Mensaje(EeventViewerImages.Informacion) = "Disponibilidad agregada correctamente"
        INDsleAvailability.EditValue = Nothing
        INDsleAvailability.Focus()
    End Sub

    ''' <summary>
    ''' Agrega productos al contrato
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAddProducts_Click(sender As Object, e As EventArgs) Handles INDBtnAddProducts.Click
        If OnlyRead = False Then
            InstantiatePopup(False, Nothing, ListProducts)
        End If
    End Sub

#End Region

#Region "MenuActions"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        ItemPurchaseOrderDetail = INDGvProducts.GetRow(INDGvProducts.FocusedRowHandle)
        If ItemPurchaseOrderDetail IsNot Nothing Then
            Select Case sender.Tag
                Case "Remove"
                    DeleteProduct(ItemPurchaseOrderDetail)
                Case "Edit"
                    InstantiatePopup(OnlyRead, ItemPurchaseOrderDetail, Nothing)
            End Select
        End If
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        DeleteAvailability()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPurchaseOrder_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbtnCode.Focus()
        INDdeDateDocument.Properties.MaxValue = GetDateServer()
    End Sub

#End Region

#Region "Paste & Imports"

    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Dim myStream As String = Nothing
    ''' <summary>
    ''' listado de errores que se presentaron validando el archivo
    ''' </summary>
    ''' <remarks></remarks>
    Dim listErrosImportFile As List(Of String)
    ''' <summary>
    ''' control donde se muestra el progreso de la operacion de importar archivos
    ''' </summary>
    ''' <remarks></remarks>
    Dim progress As CtrProgress
    ''' <summary>
    ''' total de los items a procesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim totalItems As Integer
    ''' <summary>
    ''' items procesados
    ''' </summary>
    ''' <remarks></remarks>
    Dim totalProcessedItems As Integer = 0
    ''' <summary>
    ''' coleccion de filas que se van a precesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim rows As RowCollection
    ''' <summary>
    ''' listado de las filas que se van a procesar y a validar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listRows As New Concurrent.ConcurrentBag(Of ImportFileRow)()
    Private Sub INDBtnImportFile_Click(sender As Object, e As EventArgs) Handles INDBtnImportFile.Click
        Try
            Dim openFileDialog1 As New OpenFileDialog()
            openFileDialog1.InitialDirectory = "c:\"
            openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
            openFileDialog1.FilterIndex = 2
            openFileDialog1.RestoreDirectory = True
            openFileDialog1.Title = "Importar Archivo"
            AsyncLoader(True)
            If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Try
                    'obtengo la rura del archivo
                    myStream = openFileDialog1.FileName
                    If (myStream IsNot Nothing AndAlso Not myStream.Trim().Equals(String.Empty)) Then
                        Dim sddf = New SpreadsheetControl()
                        sddf.AllowDrop = False
                        sddf.LoadDocument(myStream)
                        Dim workBook As IWorkbook = sddf.Document
                        rows = workBook.Worksheets(0).Rows
                        If rows.LastUsedIndex = 0 Then
                            Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
                            AsyncLoader(False)
                            Exit Sub
                        End If
                        ImportExcelFile(False)
                    End If
                Catch ex As Exception
                    Mensaje(EeventViewerImages.Advertencia) = "No se pudo leer el archivo por " + Environment.NewLine + ex.Message
                    AsyncLoader(False)
                End Try
            Else
                AsyncLoader(False)
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    Private Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If _purchaseOrder.Status = 2 Then
            Mensaje(EeventViewerImages.Advertencia) = "El documento esta confirmado"
            Exit Sub
        End If
        If Not CurrencyId > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe elegir primero la moneda antes de añadir productos"
        End If
        Try
            AsyncLoader(True)

            ListCopyPaste = e.Rows

            Dim errors As New List(Of String)()
            Dim sddf = New SpreadsheetControl()
            sddf.AllowDrop = False
            'sddf.LoadDocument(myStream)
            Dim workBook As IWorkbook = sddf.Document
            rows = workBook.Worksheets(0).Rows
            rows.Insert(0) 'Inserto una fila en blanco que emularía los encabezados
            Dim objLock As New Object()
            Parallel.For(0, e.Rows.Count, Sub(i)
                                              If e.Rows(i).Count <> 4 Then
                                                  errors.Add($"El registro {i + 1} no tiene la estructura requerida")
                                                  Exit Sub
                                              End If
                                              SyncLock objLock
                                                  i += 1
                                                  rows.Insert(i)
                                                  rows(i).Item(0).SetValue(e.Rows(i - 1)(0))
                                                  rows(i).Item(1).SetValue(e.Rows(i - 1)(1))
                                                  rows(i).Item(2).SetValue(e.Rows(i - 1)(2))
                                                  rows(i).Item(3).SetValue(e.Rows(i - 1)(3))
                                              End SyncLock
                                          End Sub)
            If errors.Count > 0 Then
                Using formulario As New FrmListErrors(errors)
                    formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
                AsyncLoader(False)
                Exit Sub
            End If
            ImportExcelFile(True)
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
            AsyncLoader(False)
        End Try
    End Sub

    Private Async Sub ImportExcelFile(ByVal CopyPaste As Boolean)
        listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()
        Using model As New MPurchaseOrder(Me.MyTag)
            Dim result As ActionResult(Of List(Of PurchaseOrderDetail)) = Nothing
            If CopyPaste Then 'Si viene desde CopyPaste
                SetRowCopyPaste(0, ListCopyPaste.Count() - 1)
            Else 'Si viene desde importar archivo
                SetRow(1, rows.LastUsedIndex + 1)
            End If
            result = Await model.SetPurchaseOrderImportFile(listRows.ToList(), _idOperativeUnit, _roundingType)
            'si ocurrio un error
            If result.StatusCode = eStatusResult.EXCEPTION Then
                Mensaje(EeventViewerImages.Advertencia) = result.Message
                AsyncLoader(False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                Exit Sub
            End If
            For Each item In result.ObjectEmbbeded
                If item.InventoryProduct.Status = False Then
                    result.MessageResult.Add($"El producto {item.InventoryProduct.Code} se encuentra inactivo")
                Else
                    Dim purchaseorder = ListProducts.Where(Function(x) x.ProductId = item.ProductId).FirstOrDefault()
                    If purchaseorder Is Nothing OrElse purchaseorder.ProductId = 0 Then
                        ListProducts.Add(item)
                    Else
                        result.MessageResult.Add($"El producto {purchaseorder.InventoryProduct.Code} ya esta agregado")
                    End If
                End If
            Next
            INDGcProducts.DataSource = ListProducts
            INDGcProducts.RefreshDataSource()
            If result.MessageResult.Count > 0 Then
                Using formulario As New FrmListErrors(result.MessageResult)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If
        End Using
        RefreshTotals()
        AsyncLoader(False)
    End Sub

    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        Dim objLock As New Object()
        Parallel.For(indexSend, indexEnd, Sub(x)
                                              SyncLock objLock
                                                  listRows.Add(New ImportFileRow With {.IndexRow = x + 1, .Row = rows.Item(x).SpreadsheetRowToList(4)})
                                              End SyncLock
                                          End Sub)
    End Sub

    ''' <summary>
    ''' Metodo que convierte el listado del CopyPaste al listado que se envia al sp para que valide la info
    ''' </summary>
    ''' <param name="indexSend"></param>
    ''' <param name="indexEnd"></param>
    Private Sub SetRowCopyPaste(indexSend As Integer, indexEnd As Integer)
        For i As Integer = indexSend To indexEnd
            listRows.Add(New ImportFileRow With {.IndexRow = i + 1, .Row = ConvertInfo(ListCopyPaste(i))})
        Next
    End Sub

    ''' <summary>
    ''' Convierte la información del CopyPaste a objeto de la entidad del listado que se envia al sp
    ''' </summary>
    ''' <returns></returns>
    Private Function ConvertInfo(data As List(Of String)) As List(Of Object)
        Dim res As New List(Of Object)
        For x As Integer = 0 To data.Count - 1
            res.Add(data(x))
        Next
        Return res
    End Function

#End Region

#End Region

#Region "Methods"

    Private Sub LoadParameters()
        Try
            AsyncLoader(True)
            'Se obtiene los parámetros de pagos por unidad operativa
            If BarraBotones.OperatingUnitValue <> Nothing AndAlso BarraBotones.OperatingUnitValue > 0 Then
                PaymentsSettingPaymentsXpo = _presenter.GetSettingsPaymentsByOperatingUnitId(BarraBotones.OperatingUnitValue)
                InventorySettingsXpo = _presenter.GetSettingsInventoryByOperatingUnitId(BarraBotones.OperatingUnitValue)
            End If

            ShowHideBudgetInterface()
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = "Se presentó un error al cargar los datos de la unidad operativa"
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Método que muestra/oculta el control de compromiso
    ''' </summary>
    Private Sub ShowHideBudgetInterface()
        INDLciBudgetaryEntityId.AllowHide = Not (BudgetInterface AndAlso CommitmentBudgetInterface)
        INDLciBudgetaryEntityId.ShowInCustomizationForm = Not (BudgetInterface AndAlso CommitmentBudgetInterface)
        INDLciBudgetaryValidityId.AllowHide = Not (BudgetInterface AndAlso CommitmentBudgetInterface)
        INDLciBudgetaryValidityId.ShowInCustomizationForm = Not (BudgetInterface AndAlso CommitmentBudgetInterface)
        INDlygBudget.Visibility = If(BudgetInterface, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
    End Sub

    Private Sub CleanBudgetInterface(level As Integer)
        If level < 1 Then
            BudgetaryEntityId = Nothing
            INDSleBudgetaryEntityId.Properties.NullText = String.Empty
        End If
        If level < 2 Then
            BudgetaryValidityId = Nothing
            INDSleBudgetaryValidityId.Properties.NullText = String.Empty
            BudgetaryValidityXpo = Nothing
        End If
        If level < 3 Then
            If Not _isLoading Then
                If ListPurchaseOrderAvailability IsNot Nothing AndAlso ListPurchaseOrderAvailability.Any() Then
                    If ListDeletePurchaseOrderAvailability Is Nothing Then
                        ListDeletePurchaseOrderAvailability = New List(Of PurchaseOrderAvailability)
                    End If

                    For Each PurchaseOrderAvailability In ListPurchaseOrderAvailability
                        If PurchaseOrderAvailability.Id > 0 Then
                            ListDeletePurchaseOrderAvailability.Add(PurchaseOrderAvailability)
                        End If
                    Next
                End If
                ListPurchaseOrderAvailability = Nothing
                _purchaseOrder.PurchaseOrderAvailability.Clear()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para seleccionar por defecto el primer registro si solo hay uno en entidades presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetFirstOrDefaultEntity()
        If BudgetaryEntityXpo IsNot Nothing AndAlso BudgetaryEntityXpo.Count > 0 Then
            Dim item = (From l In BudgetaryEntityXpo Where l.Status = 1 Select l).FirstOrDefault
            If item IsNot Nothing Then
                BudgetaryEntityId = item.Id
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para seleccionar por defecto el primer registro si solo hay uno en vigencias
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetFirstOrDefaultValidity()
        If BudgetaryValidityXpo IsNot Nothing AndAlso BudgetaryValidityXpo.Count > 0 Then
            Dim item = (From l In BudgetaryValidityXpo Where l.Status = 2 Select l).FirstOrDefault
            If item IsNot Nothing Then
                BudgetaryValidityId = item.Id
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para activar los controles de presupuesto
    ''' </summary>
    Private Sub EnableBudgetInterface()
        If _purchaseOrder IsNot Nothing AndAlso _purchaseOrder.Status <> 1 Then
            Exit Sub
        End If

        Dim status As Boolean = False
        If ListPurchaseOrderAvailability IsNot Nothing AndAlso ListPurchaseOrderAvailability.Any() Then
            status = True
        End If

        INDSleBudgetaryEntityId.Properties.ReadOnly = status
        INDSleBudgetaryValidityId.Properties.ReadOnly = status
    End Sub

    ''' <summary>
    ''' Metodo que valida la interfaz presupuestal
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateInterfaceBudget() As Boolean
        If BudgetInterface Then
            Dim listErrors As New StringBuilder

            If CommitmentBudgetInterface Then
                If ListPurchaseOrderAvailability Is Nothing OrElse Not ListPurchaseOrderAvailability.Any(Function(d) d.Value > 0) Then
                    listErrors.AppendLine("Debe agregar al menos una disponibilidad con el cual realizar el compromiso presupuestal")
                End If
            End If

            If ListPurchaseOrderAvailability IsNot Nothing AndAlso ListPurchaseOrderAvailability.Any Then
                Dim purchaseOrderValue As Decimal = Math.Round(TotalValue, 0, MidpointRounding.AwayFromZero)
                Dim commitmentValue As Decimal = ListPurchaseOrderAvailability.Where(Function(d) d.Value > 0).Sum(Function(d) d.Value)
                If commitmentValue > purchaseOrderValue Then
                    listErrors.AppendLine("La suma de los valores de los detalles presupuestales superan el valor de la orden de compra")
                ElseIf commitmentValue < purchaseOrderValue Then
                    listErrors.AppendLine("La suma de los valores de los detalles presupuestales son menores al valor de la orden de compra")
                End If
            End If

            If listErrors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
                Return False
            End If
        End If

        Return True
    End Function

    ''' <summary>
    ''' Elimina una disponibilidad
    ''' </summary>
    Private Sub DeleteAvailability()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim entity As PurchaseOrderAvailability = INDviewAvailability.GetFocusedRow()
        ListPurchaseOrderAvailability.Remove(entity)

        If entity.Id > 0 Then
            If ListDeletePurchaseOrderAvailability Is Nothing Then
                ListDeletePurchaseOrderAvailability = New List(Of PurchaseOrderAvailability)
            End If
            entity.MarkAsDeleted()
            ListDeletePurchaseOrderAvailability.Add(entity)
        End If

        EnableBudgetInterface()
        INDgcAvailability.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Elimina un producto de la rejilla
    ''' </summary>
    Private Sub DeleteProduct(entity As PurchaseOrderDetail)
        'Se validan las cantidades del listado de validación
        If ListViewValidateQuantityPurchaseOrderDetailXpo IsNot Nothing AndAlso ListViewValidateQuantityPurchaseOrderDetailXpo.Count > 0 Then
            If (From x In ListViewValidateQuantityPurchaseOrderDetailXpo Where x.PurchaseOrderDetailId = entity.Id Select x.QuantityRegister).FirstOrDefault() > 0 OrElse
                (From x In ListViewValidateQuantityPurchaseOrderDetailXpo Where x.PurchaseOrderDetailId = entity.Id Select x.QuantityConfirmed).FirstOrDefault() > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "El registro no se puede eliminar porque ya ha sido cruzado"
                Exit Sub
            End If
        End If

        entity.MarkAsDeleted()
        ListProducts.Remove(entity)
        EnableOrDisableCurrecy()
        RefreshTotals()
    End Sub

    ''' <summary>
    ''' Consulta la fecha de los parámetros y establece la fecha mínima y máxima de
    ''' la fecha del documento
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateDate()
        If INDdeDateDocument.EditValue IsNot Nothing Then
            If _purchaseOrder Is Nothing Then
                Exit Sub
            End If
            If InventorySettingsXpo Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "No se han cargado los parametros de inventario."
                Exit Sub
            End If

            If _purchaseOrder.Status <= 1 Then
                Dim Errors As New StringBuilder
                'Se valida que la el mes y el año de la fecha seleccionada corresponda al mes y al año de los parámetros
                If Convert.ToDateTime(INDdeDateDocument.EditValue).Month <> InventorySettingsXpo.Month Then 'Si el mes es diferente
                    Errors.AppendLine("El mes de la fecha del documento(" + Convert.ToDateTime(INDdeDateDocument.EditValue).Month.ToString + ") no es el mismo al de los parámetros(" + InventorySettingsXpo.Month.ToString + ")")
                End If
                If Convert.ToDateTime(INDdeDateDocument.EditValue).Year <> InventorySettingsXpo.Year Then 'Si el año es diferente
                    Errors.AppendLine("El año de la fecha del documento(" + Convert.ToDateTime(INDdeDateDocument.EditValue).Year.ToString + ") no es el mismo al de los parámetros(" + InventorySettingsXpo.Year.ToString + ")")
                End If
                If Errors.ToString.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = Errors.ToString
                    INDdeDateDocument.EditValue = Nothing
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la lógica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewPurchaseOrder() As Task
        _purchaseOrder = New PurchaseOrder()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
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
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
            BarraBotones.StatusRecordVisible = True
            BarraBotones.StatusRecord = "1"
        End If

        If BudgetInterface Then
            _presenter.InitializeBudgetaryEntity()
            Me.SetFirstOrDefaultEntity()
        End If

        If Me.InventorySettingsXpo IsNot Nothing AndAlso Me.InventorySettingsXpo.PurchaseOrderInterface > 0 Then
            Me.BarraBotones.ChangeButtonLargeImageIndex(EbuttonsWithoutPermission.LoadPurcharseOrder, If(Me.InventorySettingsXpo.PurchaseOrderInterface = 1, 64, 62))
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.LoadPurcharseOrder) = False
        End If
    End Function

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
    ''' Obtiene el id del detalle de secuencia por el tipo
    ''' </summary>
    ''' <returns>Id del detalle de secuencia</returns>
    Private Function GetIdSequenceByType() As Int64
        If Me._sequence IsNot Nothing AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing AndAlso Me._sequence.InventorySequenceDetail.Any(Function(d) d.Type IsNot Nothing AndAlso d.Type.Equals(Me.SequenseType)) Then
            Return Me._sequence.InventorySequenceDetail.Where(Function(d) d.Type IsNot Nothing AndAlso d.Type.Equals(Me.SequenseType)).FirstOrDefault().Id
        Else
            Return 0
        End If
    End Function

    ''' <summary>
    ''' Método que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MPurchaseOrder(CStr(Me.Tag))
                    AsyncLoader(True)
                    _purchaseOrder = Await Model.GetPurchaseOrderByCode(INDbtnCode.Text.Trim)
                    INDlyPurchaseOrder.BeginUpdate()
                    If _purchaseOrder IsNot Nothing AndAlso _purchaseOrder.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_purchaseOrder.Id))

                            _isLoading = True
                            With _purchaseOrder
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                '''consulto las unidades funcionales 
                                FunctionalUnitRequest_GetData()

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                Me._idOperativeUnit = .OperatingUnitId
                                BarraBotones.OperatingUnitValue = .OperatingUnitId
                                OrderType = .OrderType
                                Me.IsBasedContract = .IsBasedContract
                                ContractId = IIf(.ContractId Is Nothing, Nothing, .ContractId)
                                INDSeLookEdContract.Properties.NullText = .ContractNumber
                                DocumentDate = .DocumentDate
                                DeliveredDate = .DeliveredDate
                                SupplierId = .SupplierId
                                SupplierDistributionLineId = .SupplierDistributionLineId
                                INDsleSupplier.Properties.NullText = .DescriptionSupplier
                                WarehouseId = .WarehouseId
                                INDsleStock.Properties.NullText = .DescriptionWarehouse
                                Description = .Description

                                If (.TypePaymentMethod Is Nothing) Then
                                    Select Case UCase(.PaymentMethod)
                                        Case "CONTADO"
                                            TypePaymentMethod = 1
                                        Case "CREDITO"
                                            TypePaymentMethod = 2
                                        Case Else
                                            TypePaymentMethod = 1
                                    End Select
                                Else
                                    TypePaymentMethod = .TypePaymentMethod
                                End If

                                ''Validacion para asignar la unidad funcional y los dias de plazo solo con la cultura costa rica
                                If indigo.LanguageCulture = "es-CR" Then
                                    FunctionalUnitRequestId = .FunctionalUnitRequestId

                                    If (.DaysTerm Is Nothing) Then
                                        DaysTermGetData(.SupplierId)
                                    Else
                                        DaysTerm = .DaysTerm
                                    End If

                                End If

                                DeliveryMethod = .DeliveryMethod
                                DeliveryPlace = .DeliveryPlace
                                Value = .Value
                                DiscountValue = .DiscountValue
                                IvaValue = .IvaValue
                                TotalValue = .TotalValue
                                Status = .Status.ToString()
                                ListProducts = .PurchaseOrderDetail
                                If _purchaseOrder.partly Then
                                    OutstandingQuantity.ShowColumn
                                    Quantity.HideColumn
                                Else
                                    OutstandingQuantity.HideColumn
                                    Quantity.ShowColumn
                                End If
                                Me.CurrencyId(.Currency?.Abbreviation) = .CurrencyId
                                Await Me.ValidateCurrencyEditValue(False, .CurrencyId)
                                If BudgetInterface Then
                                    BudgetaryEntityId = .BudgetaryEntityId
                                    INDSleBudgetaryEntityId.Properties.NullText = .BudgetaryEntityDescription
                                    BudgetaryValidityId = .BudgetaryValidityId
                                    INDSleBudgetaryValidityId.Properties.NullText = .BudgetaryValidityDescription
                                    ListPurchaseOrderAvailability = .PurchaseOrderAvailability.ToList()
                                End If
                                ctrTmp.PrintInfo()
                            End With

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._purchaseOrder.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = _purchaseOrder.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If

                            ActionsOnControls = True
                            If _purchaseOrder.Status = 2 Then 'estado confirmado
                                ReadOnlyControls(True)
                                OnlyRead = True
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyDisconfirm)
                            ElseIf _purchaseOrder.Status = 3 Then
                                ReadOnlyControls(True)
                                OnlyRead = True
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                            Else
                                If _purchaseOrder.partly Then
                                    ReadOnlyControls(True)
                                End If
                                OnlyRead = False
                                EnableOrDisableCurrecy()
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
                                HideButtonImportInformation()

                                If _purchaseOrder.IsBasedContract AndAlso _purchaseOrder.ContractManageProducts Then
                                    INDBtnAddProducts.Enabled = False
                                    INDEsbDetails.Enabled = False
                                    INDBtnImportFile.Enabled = False
                                End If
                            End If

                            ListViewValidateQuantityPurchaseOrderDetailXpo = _presenter.ValidateQuantityPurchaseOrderDetail(_purchaseOrder.Id)

                            EnableBudgetInterface()
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, _purchaseOrder.Id, 0, _purchaseOrder.Id)
                            Me.BarraBotones.SetDocuments(_purchaseOrder.Id, Me.Tag.ToString(), Nothing, GetType(PurchaseOrder).Name)

                            _isLoading = False
                            AsyncLoader(False)

                            INDbtnCode.Enabled = False
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewPurchaseOrder()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    If _purchaseOrder.entranceVoucherCodes.Count > 0 OrElse _purchaseOrder.remissionCodes.Count > 0 Then
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = True
                        Dim entranceVoucherCodes As String = String.Join(", ", _purchaseOrder.entranceVoucherCodes)
                        Dim remissionCodes As String = String.Join(", ", _purchaseOrder.remissionCodes)
                        Mensaje(EeventViewerImages.Advertencia) = "La orden de compra " & _purchaseOrder.Code & " tiene todos los productos legalizados" & vbCrLf &
                                                              " en los comprobantes de entrada: " & entranceVoucherCodes & " y/o Remisión de entrada: " & remissionCodes & ""
                    End If
                    If _purchaseOrder.partly Then
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                    End If
                    INDlyPurchaseOrder.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Llena el control de comportamiento con el listado
    ''' </summary>
    Private Sub InitializeTuples()
        ListPaymentForm = New List(Of Tuple(Of Integer, String))
        ListPaymentForm.Add(New Tuple(Of Integer, String)(1, "Contado"))
        ListPaymentForm.Add(New Tuple(Of Integer, String)(2, "Crédito"))
        INDsleTypePaymentMethod.Properties.DataSource = ListPaymentForm.ToList()
    End Sub

    ''' <summary>
    ''' metodo para instanciar el formulario de concepto de recibo de caja
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InstantiatePopup(OnlyRead As Boolean, _Detaill As PurchaseOrderDetail, _listProducts As Domain.Entities.TrackableCollection(Of PurchaseOrderDetail), Optional EditMode As Boolean = False)
        Dim _quantityConfirmed As Integer = 0
        If ListViewValidateQuantityPurchaseOrderDetailXpo IsNot Nothing AndAlso ListViewValidateQuantityPurchaseOrderDetailXpo.Count > 0 AndAlso _Detaill IsNot Nothing Then
            _quantityConfirmed = (From x In ListViewValidateQuantityPurchaseOrderDetailXpo Where x.PurchaseOrderDetailId = _Detaill.Id Select x.QuantityConfirmed).FirstOrDefault()
        End If
        Me.Cursor = ChangeCursorIndigo()

        Using formulario As New PopupProductsPurchaseOrder(OnlyRead, _Detaill, _listProducts,
                                                            _currency:=New Currency With {.Id = Me.CurrencyId, .Abbreviation = Me.CurrencyAbbreviation},
                                                            _tRMValue:=If(Me.TRM Is Nothing, 1, Me.TRM.Value),
                                                            _roundType:=_roundingType)
            AddHandler formulario.AddProductContract, AddressOf ReturnPopupAddProductContract
            formulario.QuantityConfirmed = _quantityConfirmed
            formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            formulario.ViewModeEditHold = True
            formulario.StartPosition = FormStartPosition.CenterParent
            formulario.Size = New Size(800, 700)
            formulario.FormBorderStyle = FormBorderStyle.Sizable
            Dim transparent = New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' método para agregar un concepto de recibo de caja a la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnPopupAddProductContract(sender As Object, e As AddProductPurchaseOrder)
        If e.ListPurchaseOrderDetail IsNot Nothing Then
            ListProducts = e.ListPurchaseOrderDetail
        End If
        If e.ItemPurchaseOrderDetail IsNot Nothing Then
            ListProducts.Remove(ItemPurchaseOrderDetail)
            ListProducts.Add(e.ItemPurchaseOrderDetail)
        End If
        EnableOrDisableCurrecy()
        RefreshTotals()
    End Sub

    ''' <summary>
    ''' Método utilizado para refrescar los valores de los totales
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RefreshTotals()
        If ListProducts IsNot Nothing AndAlso ListProducts.Count > 0 Then

            Value = Math.Round(CDec(ListProducts.Sum(Function(x) x.SubTotalValue)), 2, MidpointRounding.AwayFromZero)
            IvaValue = Math.Round(CDec(ListProducts.Sum(Function(x) x.IvaValue)), 2, MidpointRounding.AwayFromZero)
            DiscountValue = Math.Round(CDec(ListProducts.Sum(Function(x) x.DiscountValue)), 2, MidpointRounding.AwayFromZero)
            TotalValue = Math.Round(CDec(ListProducts.Sum(Function(x) x.TotalValue)), 2, MidpointRounding.AwayFromZero)

        Else
            Value = 0
            IvaValue = 0
            DiscountValue = 0
            TotalValue = 0
        End If
        ctrTmp.PrintInfo()
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With _purchaseOrder
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .OperatingUnitId = Me.BarraBotones.OperatingUnitValue
            .OrderType = OrderType
            .IsBasedContract = INDCmbContractBased.EditValue
            .ContractId = IIf(INDCmbContractBased.EditValue = False, Nothing, ContractId)
            .DocumentDate = DocumentDate
            .DeliveredDate = DeliveredDate
            .SupplierId = SupplierId
            .SupplierDistributionLineId = SupplierDistributionLineId
            .WarehouseId = WarehouseId
            .Description = Description

            'se guarda el id de la forma de pago ademas se seguir guardando en el antiguo campo el tipo de pago
            .TypePaymentMethod = TypePaymentMethod
            Select Case TypePaymentMethod
                Case 1
                    .PaymentMethod = "CONTADO"
                Case 2
                    .PaymentMethod = "CREDITO"
            End Select

            'asigno los valores solo para CR
            If indigo.LanguageCulture = "es-CR" Then
                .FunctionalUnitRequestId = FunctionalUnitRequestId
            Else
                .FunctionalUnitRequestId = Nothing
            End If

            .DaysTerm = DaysTerm
            .DeliveryMethod = DeliveryMethod
            .DeliveryPlace = DeliveryPlace
            .Value = Value
            .DiscountValue = DiscountValue
            .IvaValue = IvaValue
            .TotalValue = TotalValue
            .OsteosynthesisEquipment = OsteosynthesisEquipment
            .Prefix = Me._prefixSelected
            .CurrencyId = Me.CurrencyId
            If ListProducts IsNot Nothing AndAlso ListProducts.Count > 0 Then
                For Each itemDetail As PurchaseOrderDetail In ListProducts
                    .PurchaseOrderDetail.Add(itemDetail)
                Next
            End If
            If OsteosynthesisEquipment Then
                .OsteosynthesisEquipmentId = OsteosynthesisEquipmentId
                Exit Sub
            End If

            If BudgetInterface Then
                If ListPurchaseOrderAvailability IsNot Nothing Then
                    _purchaseOrder.BudgetaryEntityId = BudgetaryEntityId
                    _purchaseOrder.BudgetaryEntityDescription = INDSleBudgetaryEntityId.Text
                    _purchaseOrder.BudgetaryValidityId = BudgetaryValidityId
                    _purchaseOrder.BudgetaryValidityDescription = INDSleBudgetaryValidityId.Text

                    For Each purchaseOrderAvailability In ListPurchaseOrderAvailability
                        If purchaseOrderAvailability.Value > 0 Then
                            If purchaseOrderAvailability.ChangeTracker.State = ObjectState.Added Then
                                _purchaseOrder.PurchaseOrderAvailability.Add(purchaseOrderAvailability)
                            ElseIf purchaseOrderAvailability.ChangeTracker.State = ObjectState.Modified Then
                                purchaseOrderAvailability.MarkAsModified()
                            End If
                        Else
                            purchaseOrderAvailability.MarkAsDeleted()
                        End If
                    Next
                End If
            End If

            If ListDeletePurchaseOrderAvailability IsNot Nothing Then
                For Each purchaseOrderAvailability In ListDeletePurchaseOrderAvailability
                    purchaseOrderAvailability.MarkAsDeleted()
                    _purchaseOrder.PurchaseOrderAvailability.Add(purchaseOrderAvailability)
                Next
            End If
        End With
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>HRR PBI3498</remarks>
    Private Sub AssigningAutomaticValues()
        With _purchaseOrder
            '.CustomProperties = LayoutControls.GetCustomFieldsValue()
            '.Code = Code
            .Code = String.Empty
            .OperatingUnitId = SessionValues.Instance.IndigoOperatingUnitId
            .OrderType = 1
            .IsBasedContract = False
            .ContractId = Nothing
            .DocumentDate = Me.DocumentDate
            .DeliveredDate = Me.DeliveredDate
            .SupplierId = SupplierId
            .SupplierDistributionLineId = SupplierDistributionLineId
            .WarehouseId = WarehouseId
            .Description = Me.Description
            .PaymentMethod = String.Empty
            .DeliveryMethod = Nothing
            .DeliveryPlace = Nothing
            .Value = Value
            .DiscountValue = DiscountValue
            .IvaValue = IvaValue
            .TotalValue = TotalValue
            .OsteosynthesisEquipment = OsteosynthesisEquipment
            .CurrencyId = Me.CurrencyId
            If OsteosynthesisEquipment Then
                .OsteosynthesisEquipmentId = OsteosynthesisEquipmentId
            End If
            .Prefix = Me._prefixSelected
            If ListProducts IsNot Nothing AndAlso ListProducts.Count > 0 Then
                For Each itemDetail As PurchaseOrderDetail In ListProducts
                    .PurchaseOrderDetail.Add(itemDetail)
                Next
            End If
        End With
    End Sub

    ''' <summary>
    ''' Limpia los controles y las variables
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.LoadPurcharseOrder) = True
        ReadOnlyControls(False)
        INDlyPurchaseOrder.BeginUpdate()
        ActionsOnControls = False
        Code = String.Empty
        OrderType = 0

        IsBasedContract = Nothing
        ContractId = Nothing
        INDSeLookEdContract.Properties.NullText = String.Empty
        DocumentDate = Nothing
        DeliveredDate = Nothing
        SupplierId = Nothing
        SupplierDistributionLineId = Nothing
        INDsleSupplier.Properties.NullText = String.Empty
        WarehouseId = Nothing
        INDsleStock.Properties.NullText = String.Empty
        Description = Nothing
        DeliveryMethod = Nothing
        DeliveryPlace = Nothing
        Value = 0
        IvaValue = 0
        DiscountValue = 0
        TotalValue = 0
        INDtxtTotalValue.Properties.ReadOnly = True
        Me.CurrencyDatasource = Nothing
        Me.CurrencyId(Me.indigo?.CurrencyISO4217) = Me.indigo?.OfficialCurrencyId
        Dim search = CType(INDsleSupplier, DevExpress.XtraEditors.SearchLookUpEdit)
        Dim view = search.Properties.View
        view.ActiveFilterString = Nothing

        INDdeDateDocument.EditValue = Nothing
        INDdeDateDelivery.Text = String.Empty

        ListPurchaseOrderAvailability = Nothing
        ListDeletePurchaseOrderAvailability = Nothing
        INDgcAvailability.DataSource = Nothing
        INDsleAvailability.EditValue = Nothing

        FunctionalUnitRequestId = Nothing
        DaysTerm = Nothing
        ListPaymentForm = Nothing
        SupplierData = Nothing
        INDsleFunctionalUnitRequest.Properties.DataSource = Nothing

        'Status = "-1"
        _isLoading = False
        OnlyRead = False
        INDmeDetail.Text = String.Empty
        INDGcProducts.DataSource = Nothing
        HideButtonImportInformation()
        INDlyPurchaseOrder.EndUpdate()
        ctrTmp.PrintInfo()
        ListProducts = New Domain.Entities.TrackableCollection(Of PurchaseOrderDetail)
        _purchaseOrder = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        INDlyItemContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        _flagInterface = False
        ListViewValidateQuantityPurchaseOrderDetailXpo = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Retorna el valor de la búsqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ReturnValue As String, ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        'If Me._doc Is Nothing Then
        '    Me._doc = New IndexedDocument2 With { _
        '        .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.purchaseOrder.Code, Me.purchaseOrder.DescriptionSupplier), _
        '        .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
        '        .IdEntity =  "$#" & CStr(Me.Tag) & "_" & Me.purchaseOrder.Code & "#$", .IdForm = CStr(Me.Tag), _
        '        .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.purchaseOrder.Code), _
        '        .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        '    Return Me._doc
        'Else
        '    Me._doc.Update = dateServer
        '    Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
        '    Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.purchaseOrder.Code, Me.purchaseOrder.DescriptionSupplier)
        '    Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.purchaseOrder.Code)
        '    Return Me._doc
        'End If
    End Function

    ''' <summary>
    ''' Método que borra el registro de auditoria
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Carga los estados de contrato
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = String.Empty, .StatusColor = System.Drawing.Color.White})
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Obtiene el valor del anticipo
    ''' </summary>
    ''' <returns></returns>
    Private Function getAdvance() As Decimal
        Dim _advance As Decimal = 0
        _advance = _advanceValue
        Return _advance
    End Function

    Private Sub OpenFormDialog(form As FormBase)
        form.ViewModeEditHold = True
        form.Size = New Size(800, 700)
        form.StartPosition = FormStartPosition.CenterParent
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        form.MaximizeBox = False
        form.MinimizeBox = False
        Dim transparent = New FrmTransparent(form, False)
        transparent.ShowDialog()
    End Sub

    ''' <summary>
    ''' Devuelve el listado para mostrar el Total del contrato y sus derivados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfoServiceOrder() As Tuple(Of String, String, String, String)
        Return New Tuple(Of String, String, String, String)(IvaValue.ToString("c2"), DiscountValue.ToString("c2"), TotalValue.ToString("c2"), "")
        ' Return New Tuple(Of String, String, String)(String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", _IvaValue), String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", DiscountValue), String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", TotalValue))
    End Function

    ''' <summary>
    ''' Armar el listado del detalle cuando generamos la orden de compra desde crystal
    ''' </summary>
    ''' <param name="listProductsCrystal"></param>
    ''' <remarks></remarks>
    Public Function ChargeProducts(listProductsCrystal As List(Of Tuple(Of String, Integer))) As Boolean
        Dim errors As New StringBuilder
        Dim conError As Boolean
        If listProductsCrystal.Count > 0 Then
            Dim purchaseOrderDetail As Domain.Entities.PurchaseOrderDetail
            Dim listProductos As New Domain.Entities.TrackableCollection(Of Domain.Entities.PurchaseOrderDetail)()
            For Each item In listProductsCrystal
                Using model As New MInventoryProduct(Me.Tag)
                    Dim product = model.GetInventoryProductByCodeSimpleToGroup(item.Item1)
                    If product Is Nothing Or product.Id = 0 Then
                        'Mensaje(EeventViewerImages.Advertencia) = "El producto " & item.Item1 & " no existe en Indigo Vie"
                        'Return
                        errors.AppendLine(String.Format("El producto {0} no existe en Indigo Vie", item.Item1))
                        Continue For
                    End If
                    If product.FinalProductCost Is Nothing OrElse product.FinalProductCost = 0 Then
                        errors.AppendLine(String.Format(ResourceManager.GetString("FinalProductCostZero", MODULE_NAME), product.Code + " - " + product.Name))
                        Continue For
                    End If
                    purchaseOrderDetail = New Domain.Entities.PurchaseOrderDetail

                    purchaseOrderDetail.InventoryProduct = product
                    purchaseOrderDetail.ProductCode = product.Code
                    purchaseOrderDetail.ProductName = product.Name
                    purchaseOrderDetail.Quantity = item.Item2
                    purchaseOrderDetail.OutstandingQuantity = item.Item2
                    purchaseOrderDetail.CancelledQuantity = 0
                    purchaseOrderDetail.Value = product.FinalProductCost
                    purchaseOrderDetail.SubTotalValue = (product.FinalProductCost * item.Item2)
                    If Not String.IsNullOrEmpty(product.IvaPercent) Then
                        purchaseOrderDetail.IvaPercentage = CDec(product.IvaPercent)
                    Else
                        purchaseOrderDetail.IvaPercentage = 0
                    End If
                    purchaseOrderDetail.IvaValue = Math.Round(purchaseOrderDetail.SubTotalValue * (purchaseOrderDetail.IvaPercentage / 100), 2, MidpointRounding.AwayFromZero)
                    purchaseOrderDetail.DiscountPercentage = 0
                    purchaseOrderDetail.DiscountValue = 0
                    purchaseOrderDetail.TotalValue = purchaseOrderDetail.SubTotalValue + purchaseOrderDetail.IvaValue - purchaseOrderDetail.DiscountValue
                End Using
                listProductos.Add(purchaseOrderDetail)
            Next
            ListProducts = listProductos
        End If
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            conError = True
        End If
        Return conError
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>HRR PBI3498</remarks>
    Public Async Sub GenerateAutomaticPurchaseOrder()
        If (ListProducts Is Nothing OrElse ListProducts.Count = 0) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe agregar al menos un producto"
            Exit Sub
        End If
        Try
            AssigningAutomaticValues()
            _purchaseOrder.Status = 1 'guardar
            _purchaseOrder.ChangeTracker.State = ObjectState.Added
            Using model As New MPurchaseOrder(Me.Tag.ToString())

                If _sequence IsNot Nothing Then
                    If _sequence.IsManual Then
                        Mensaje(EeventViewerImages.MensajeError) = "No se genero la orden de compra, la secuencia esta configurada como manual"
                    Else
                        Dim store = _presenter.GetWarehouseById(WarehouseId)
                        If Me._idCurrentSequence = 0 Then
                            Me._idCurrentSequence = Me.GetIdSequenceByPrefix(If(store IsNot Nothing, store.Prefix, Me._purchaseOrder.Prefix))
                        End If

                        If _idCurrentSequence = 0 AndAlso _sequence.IdSequence Is Nothing Then
                            Mensaje(EeventViewerImages.MensajeError) = "No se encontró una secuencia válida para generar la orden de compra"
                        Else
                            Me._prefixSelected = If(store IsNot Nothing, store.Prefix, Me._purchaseOrder.Prefix)

                            Dim Result = Await model.SavePurchaseOrder(_purchaseOrder, _idCurrentSequence, Me._sequence)
                            If Result.StateResult = True Then

                                'Se descarta la secuencia numerica usada
                                If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                                    Me.DicSequense(Me._sequence.InventorySequenceDetail(0).Id).RemoveAt(0)
                                End If
                                If Me._sequence.Sequential Then
                                    Mensaje(EeventViewerImages.Informacion) = String.Format("Se guardo la Orden de Compra con el código {0} ", Result.ObjectEmbbeded.Code)
                                Else
                                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                                End If

                                Me._purchaseOrder = Result.ObjectEmbbeded
                                If OsteosynthesisEquipment Then
                                    Me.Close()
                                End If
                            Else

                                If Result.MessageResult IsNot Nothing AndAlso Result.MessageResult(0) = ErrorConcurrencia Then
                                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                                ElseIf Not String.IsNullOrEmpty(Result.Message) Then
                                    Mensaje(EeventViewerImages.Advertencia) = Result.Message
                                Else
                                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                                End If
                            End If
                        End If
                    End If
                End If
            End Using
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Establece el formato moneda en los controles del formulario
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Async Sub SetCurrencyUI(_currencyAbbreviation As String)

        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "Está llegando vacia la abreviación de la moneda"
            Exit Sub
        End If

        If Me.CurrencyId > 0 Then
            Using modelCurrency As New MCurrency(MyTag)
                Dim currency As Currency = Await modelCurrency.GetCurrencyById(Me.CurrencyId)
                Dim round = currency.RoundingType
                Select Case round
                    Case 1
                        _roundingType = 0.01
                    Case 2
                        _roundingType = 0.1
                    Case 3
                        _roundingType = 1
                    Case 4
                        _roundingType = 10
                    Case 5
                        _roundingType = 100
                    Case 6
                        _roundingType = 1000
                End Select
            End Using
            ''se actualiza el formato de los campos numericos en el formuario
            Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
            _culture.NumberFormat = _currencyAbbreviation.GetNumberFormat
            _culture.NumberFormat.CurrencyDecimalDigits = Utils.MaskByCurrencyRounding(_roundingType, _culture.NumberFormat)
            Me.changeNumericFormatByCurrency(_culture.NumberFormat)
            Me.ctrTmp.CodeISO4217 = _currencyAbbreviation
            Me.ctrTmp.CurrencyNumbertFormat = _culture.NumberFormat
            ctrTmp.PrintInfo()
        End If
    End Sub

    ''' <summary>
    ''' Funcion que se encarga de consultar el TRM
    ''' </summary>
    ''' <param name="_currencyId"></param>
    ''' <param name="ToCurrencyId"></param>
    ''' <returns></returns>
    Private Async Function GetTRM(_currencyId As Integer, ToCurrencyId As Integer) As Task(Of Boolean)
        Using Model As New MInventoryContract("")
            Dim Result = Await Model.GetTRMbyCurrencyIdAsync(ToCurrencyId, _currencyId)
            If Result Is Nothing OrElse Not Result?.StateResult Then
                Me.Mensaje(EeventViewerImages.Advertencia) = Result?.Message
                Return False
            End If
            Me.TRM = Result.ObjectEmbbeded
            Me.Mensaje(EeventViewerImages.Informacion) = Result?.Message
            Return True
        End Using
    End Function

    ''' <summary>
    ''' habilita o deshabilita el control de currency dependiendo si estan cargados o no detalles
    ''' </summary>
    Private Sub EnableOrDisableCurrecy()
        Me.INDsleCurrency.ReadOnly = If(ListProducts?.Any() OrElse Me.IsBasedContract, True, False)
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

        If Not _isLoading Then
            Me.CurrencyId(Me.indigo.CurrencyISO4217) = Me.indigo.OfficialCurrencyId
        End If
        Return New ActionResult With {.StateResult = False}
    End Function

    Private Async Sub ChangeIconButtonPurchaseOrder(ByVal idOperativeUnit As Integer)
        Dim PurchaseOrderInterface As Integer '= GetSettingInvetoryByUnitOperative(idOperativeUnit)
        Using Model As New MSettingInventory(CStr(Me.Tag))
            Dim res = Await Model.GetInventorySettingsRegister(idOperativeUnit)
            If res IsNot Nothing AndAlso res.StateResult Then
                PurchaseOrderInterface = res.ObjectEmbbeded.PurchaseOrderInterface
                If PurchaseOrderInterface = 2 Then
                    BarraBotones.ChangeButtonLargeImageWithImage(EbuttonsWithoutPermission.LoadPurcharseOrder, My.Resources.PurchaseOrderIcon)
                End If
            End If
        End Using
    End Sub

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click Eliminar.
    ''' </summary>
    Public Sub Anular() Implements ICrudBase.Eliminar
        If Status = 2 Then
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("InventoryContract_ContractConfirm", MODULE_NAME)
            Exit Sub
        End If
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _purchaseOrder.Status = 3
            Guardar(False)
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Sub Guardar() Implements ICrudBase.Guardar

    End Sub

    ''' <summary>
    ''' Guardar the specified with confirm.
    ''' </summary>
    ''' <param name="withConfirm">if set to <c>true</c> [with confirm].</param>
    Public Async Sub Guardar(withConfirm As Boolean)
        If _purchaseOrder.Status <> 3 Then
            If Not ValidateControls() Then
                Exit Sub
            End If
            If OrderType = 1 Then
                If (ListProducts Is Nothing OrElse ListProducts.Count = 0) Then
                    Mensaje(EeventViewerImages.Advertencia) = "Debe agregar al menos un producto"
                    Exit Sub
                End If
            ElseIf OrderType = 2 Then
                If TotalValue <= 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Debe establecer un Valor del Servicio"
                    Exit Sub
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = "El tipo de orden no es válido"
                Exit Sub
            End If

            If IsBasedContract AndAlso ContractId = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "El tipo de orden es basada en un contrato, seleccione uno por favor."
                Exit Sub
            End If

            If Not ValidateInterfaceBudget() Then
                Exit Sub
            End If
        End If

        Try
            AssigningValues()
            Using model As New MPurchaseOrder(Me.Tag.ToString())
                AsyncLoader(True)
                'Establecer estado 2 en aplicacion
                Dim Result = Await model.SavePurchaseOrder(_purchaseOrder, _idCurrentSequence, Me._sequence)
                If Result.StateResult = True Then
                    If _purchaseOrder.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._sequence.InventorySequenceDetail(0).Id).RemoveAt(0)
                        End If
                        If Me._sequence.Sequential Then
                            If _purchaseOrder.Status = 2 Then
                                Mensaje(EeventViewerImages.Informacion) = Result.Message
                            Else
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                            End If
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    ElseIf _purchaseOrder.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        If _purchaseOrder.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        ElseIf _purchaseOrder.Status = 2 Then
                            Mensaje(EeventViewerImages.Informacion) = Result.Message
                        ElseIf _purchaseOrder.Status = 1 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If
                    Me._purchaseOrder = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, _purchaseOrder.Id, 0, _purchaseOrder.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, _purchaseOrder.Id, 0, _purchaseOrder.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _purchaseOrder.Id, 0, _purchaseOrder.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _purchaseOrder.Id, 0, _purchaseOrder.Id)
                    End Select

                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    If Result.MessageResult IsNot Nothing AndAlso Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    ElseIf Result.MessageResult IsNot Nothing AndAlso Not String.IsNullOrEmpty(Result.MessageResult(0)) Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.MessageResult(0)
                    ElseIf Not String.IsNullOrEmpty(Result.Message) Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewPurchaseOrder()
        End If
    End Sub

    ''' <summary>
    ''' este método abre el frontal de búsqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 150},
                              New ColumnInfo With {.Caption = "Descripción", .FieldName = "Description", .ColumnWidth = 230},
                              New ColumnInfo With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = 150},
                              New ColumnInfo With {.Caption = "Fecha Entrega", .FieldName = "DeliveredDate", .ColumnWidth = 150},
                              New ColumnInfo With {.Caption = "Proveedor", .FieldName = "SupplierId.CodeName", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = 150},
                              New ColumnInfo With {.Caption = "Moneda", .FieldName = "CurrencyAbbreviation", .ColumnWidth = 75},
                              New ColumnInfo With {.Caption = "Valor", .ColumnFormat = "n2", .ColumnFormatType = DevExpress.Utils.FormatType.Custom, .FieldName = "TotalValue", .ColumnWidth = 150}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllInventoryPurchaserOrder
            '.FiltroBusqueda = BarraBotones.OperatingUnitValue.ToString()
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        _purchaseOrder.Status = 1
        varImp = 2
        Guardar(False)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Anular()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        _purchaseOrder.Status = 1
        varImp = 1
        Guardar(False)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ guardar confirmar.
    ''' </summary>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        _purchaseOrder.Status = 2
        varImp = 3
        Guardar(True)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ actualizar confirmar.
    ''' </summary>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        _purchaseOrder.Status = 2
        varImp = 3
        Guardar(True)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click anular.
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        varImp = 4
        Anular()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el botón imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        'Dim reportDef As New Reporter.rptPurchaseOrder
        'Me.BarraBotones.PrintReport(reportDef, purchaseOrder.Id, True, Me.Tag, Nothing, "FrmPurchaseOrder")
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _purchaseOrder.Id, 0, _purchaseOrder.Id)
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnitAsync(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            Me.LoadParameters()
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing Then
                If Not Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
        If operatingUnit IsNot Nothing Then
            ValidateDate()
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el botón desconfirmar de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_Desconfirmar() Handles BarraBotones.Click_Desconfirmar
        Using model As New MPurchaseOrder(Me.Tag.ToString())
            AsyncLoader(True)
            Dim Result = Await model.DisconfirmPurchaseOrder(_purchaseOrder)
            If Result.StateResult = True Then
                Mensaje(EeventViewerImages.Informacion) = Result.Message
                'CleanControls()
                ReadOnlyControls(False)
                Await LoadControls()
            Else
                If Result.MessageResult IsNot Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = Result.MessageResult(0)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = Result.Message
                End If
            End If
            AsyncLoader(False)
        End Using
    End Sub

    Private Async Sub BarraBotones_Click_LoadPurcharseOrder() Handles BarraBotones.Click_LoadPurcharseOrder
        If ListProducts IsNot Nothing AndAlso ListProducts.Count > 0 Then
            If MessageIndigo.Show("Se perderán los datos cargados, Desea continuar", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If
        _flagInterface = True
        AsyncLoader(True)
        Using model As New MPurchaseOrder(MyTag)
            Using frm As New PopupOrderCode
                frm.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(frm, False)
                transparent.ShowDialog(Me)
                If transparent.DialogResult = System.Windows.Forms.DialogResult.OK Then
                    Dim errors = String.Empty
                    If InventorySettingsXpo.PurchaseOrderInterface = 1 Then
                        Dim result = Await model.LoadPurcharseOrderSBS(InventorySettingsXpo.PurchaseOrderURL, InventorySettingsXpo.PurchaseOrderIdentifier, InventorySettingsXpo.PurchaseOrderUser, InventorySettingsXpo.PurchaseOrderPass, frm.OrderCode)
                        If result.StateResult Then
                            INDCmbOrderType.EditValue = 1

                            errors = result.Message
                            _supplierIdentification = result.MessageResult(0)
                            ListProducts = result.ObjectEmbbeded
                            INDGcProducts.DataSource = ListProducts
                            RefreshTotals()
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                        End If
                    ElseIf InventorySettingsXpo.PurchaseOrderInterface = 2 Then
                        Dim result = Await model.LoadPurcharseOrderBionexo(InventorySettingsXpo.PurchaseOrderURL, InventorySettingsXpo.PurchaseOrderIdentifier, InventorySettingsXpo.PurchaseOrderUser, InventorySettingsXpo.PurchaseOrderPass, frm.OrderCode)
                        If result.StateResult Then
                            INDCmbOrderType.EditValue = 1

                            errors = result.Message
                            _supplierIdentification = result.MessageResult(0)
                            ListProducts = result.ObjectEmbbeded
                            INDGcProducts.DataSource = ListProducts
                            RefreshTotals()
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                        End If
                    End If

                    If Not String.IsNullOrEmpty(errors) Then
                        Mensaje(EeventViewerImages.Advertencia) = errors
                    End If
                End If
            End Using
        End Using
        AsyncLoader(False)
        INDdeDateDocument.Focus()
    End Sub

#End Region

End Class