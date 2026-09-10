'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Henry Alejandro Vargas Polania 
' Created          : 27/12/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress
Imports DevExpress.Data.Async.Helpers
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Xpo.Base
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Inventory.MVP

#End Region

Public Class FrmInventoryContract
    Implements IInventoryContract, ICustomizableForm

#Region "Builder"
    Public Sub New()
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
    End Sub
#End Region

#Region "Globals"

    ''' <summary>
    ''' Control para establecer informacion del ingreso
    ''' </summary>
    Private ctrTmp As CtrContractTotalInfo

    ''' <summary>
    ''' Representa el presentador 
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PInventoryContract

    ''' <summary>
    ''' Representa el modelo 
    ''' </summary>
    ''' <remarks></remarks>
    Dim Model As MInventoryContract

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

    ''' <summary>
    ''' Representa la entidad de contrato
    ''' </summary>
    ''' <remarks></remarks>
    Dim contract As InventoryContract

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
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

    ''' <summary>
    ''' variable que contiene el drug interaction actual
    ''' </summary>
    ''' <remarks></remarks>
    Dim CurrentContractType As InventoryContractType

    ''' <summary>
    ''' Variable que contiene un item del detalle
    ''' </summary>
    ''' <remarks></remarks>
    Dim ItemContractDetail As InventoryContractDetail

    ''' <summary>
    ''' Listado de eliminados
    ''' </summary>
    Private ListDeleteProducts As Domain.Entities.TrackableCollection(Of InventoryContractDetail)

    Dim _isLoading As Boolean

    Dim OnlyRead As Boolean = False

    Dim nameResourse As String = String.Empty

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

    ''' <summary>
    ''' tasa de cambio
    ''' </summary>
    Private _tRM As TRM
#End Region

#Region "Fields Entity Properties"

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Byte Implements IInventoryContract.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            Me.BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces el codigo del tipo de contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IInventoryContract.Code
        Get
            If (INDBtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDBtnCode.Text
            End If
        End Get
        Set(value As String)
            INDBtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces el id del tipo de contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractTypeId As Integer Implements IInventoryContract.ContractTypeId
        Get
            Return INDGleContractType.EditValue
        End Get
        Set(value As Integer)
            INDGleContractType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces el numero de contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractNumber As String Implements IInventoryContract.ContractNumber
        Get
            If (INDTxtContractNumber.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDTxtContractNumber.Text
            End If
        End Get
        Set(value As String)
            INDTxtContractNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces la fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As Date? Implements IInventoryContract.DocumentDate
        Get
            Return INDdeDocumentDate.EditValue
        End Get
        Set(value As Date?)
            INDdeDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces la fecha incial de contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InitialDate As Date? Implements IInventoryContract.InitialDate
        Get
            Return INDDteInitialDate.EditValue
        End Get
        Set(value As Date?)
            INDDteInitialDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces la fecha final de contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EndDate As Date? Implements IInventoryContract.EndDate
        Get
            Return INDDteEndDate.EditValue
        End Get
        Set(value As Date?)
            INDDteEndDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces la descripcion de contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description As String Implements IInventoryContract.Description
        Get
            If (INDMmoDescription.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDMmoDescription.Text
            End If
        End Get
        Set(value As String)
            INDMmoDescription.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces las lineas de distribucion del proveedor del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property _supplierId As Integer
    Public Property SupplierId As Integer Implements IInventoryContract.SupplierId
        Get
            Return _supplierId
        End Get
        Set(value As Integer)
            _supplierId = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces el proveedor del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierDistributionLineId As Integer Implements IInventoryContract.SupplierDistributionLineId
        Get
            Return INDSleSupplier.EditValue
        End Get
        Set(value As Integer)
            INDSleSupplier.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces el metodo de pago del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PaymentMethod As String Implements IInventoryContract.PaymentMethod
        Get
            If (INDTxtPaymentMethod.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDTxtPaymentMethod.Text
            End If
        End Get
        Set(value As String)
            INDTxtPaymentMethod.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces el metodo de entrega del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DeliveryMethod As String Implements IInventoryContract.DeliveryMethod
        Get
            If (INDTxtDeliveryMethod.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDTxtDeliveryMethod.Text
            End If
        End Get
        Set(value As String)
            INDTxtDeliveryMethod.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el lugar de entrega del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DeliveryPlace As String Implements IInventoryContract.DeliveryPlace
        Get
            If (INDTxtDeliveryPlacer.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDTxtDeliveryPlacer.Text
            End If
        End Get
        Set(value As String)
            INDTxtDeliveryPlacer.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el origen del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SourceOrder As Integer Implements IInventoryContract.SourceOrder
        Get
            Return INDGleSourcerOrder.EditValue
        End Get
        Set(value As Integer)
            INDGleSourcerOrder.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el proceso de compra del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PurchaseProcess As Integer Implements IInventoryContract.PurchaseProcess
        Get
            Return INDGlePurchaseProcess.EditValue
        End Get
        Set(value As Integer)
            INDGlePurchaseProcess.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la exclusividad del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Exclusivity As Boolean? Implements IInventoryContract.Exclusivity
        Get
            Return INDGleExclusivity.EditValue
        End Get
        Set(value As Boolean?)
            INDGleExclusivity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la propiedad maneja productos para identificar si el contrato es por valor o por items
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ManageProducts As Boolean? Implements IInventoryContract.ManageProducts
        Get
            Return INDgleManageProducts.EditValue
        End Get
        Set(value As Boolean?)
            INDgleManageProducts.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si es garantia unica del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OnlyGuarantee As Boolean? Implements IInventoryContract.OnlyGuarantee
        Get
            Return INDGleOnlyGuarantee.EditValue
        End Get
        Set(value As Boolean?)
            INDGleOnlyGuarantee.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la supervicion tecnica del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TechnicalSupervicion As String Implements IInventoryContract.TechnicalSupervicion
        Get
            If (INDTxtTechnicalSupervicion.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDTxtTechnicalSupervicion.Text
            End If
        End Get
        Set(value As String)
            INDTxtTechnicalSupervicion.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la supervicion tecnica del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupervisionExecution As String Implements IInventoryContract.SupervisionExecution
        Get
            If (INDTxtSupervicionExecution.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDTxtSupervicionExecution.Text
            End If
        End Get
        Set(value As String)
            INDTxtSupervicionExecution.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la disponibilidad del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Availability As String Implements IInventoryContract.Availability
        Get
            If (INDTxtAvailability.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDTxtAvailability.Text
            End If
        End Get
        Set(value As String)
            INDTxtAvailability.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la resolucion del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Resolution As String Implements IInventoryContract.Resolution
        Get
            If (INDTxtResolution.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDTxtResolution.Text
            End If
        End Get
        Set(value As String)
            INDTxtResolution.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha de resolucion del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ResolutionDate As Date? Implements IInventoryContract.ResolutionDate
        Get
            Return INDTxtResolutionDate.EditValue
        End Get
        Set(value As Date?)
            INDTxtResolutionDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece las clausulas del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Clauses As String Implements IInventoryContract.Clauses
        Get
            If (INDMmoClauses.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDMmoClauses.Text
            End If
        End Get
        Set(value As String)
            INDMmoClauses.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los anexos del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Attachments As String Implements IInventoryContract.Attachments
        Get
            If (INDMmoAttachments.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDMmoAttachments.Text
            End If
        End Get
        Set(value As String)
            INDMmoAttachments.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el numero de cotizacion del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property QuoteNumber As String Implements IInventoryContract.QuoteNumber
        Get
            If (INDTxtQuoteNumber.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDTxtQuoteNumber.Text
            End If
        End Get
        Set(value As String)
            INDTxtQuoteNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha de la cotizacion del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property QuoteDate As Date? Implements IInventoryContract.QuoteDate
        Get
            Return INDDteQuoteDate.EditValue
        End Get
        Set(value As Date?)
            INDDteQuoteDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el numero del acta del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RecordNumber As String Implements IInventoryContract.RecordNumber
        Get
            If (INDTxtRecordNumber.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDTxtRecordNumber.Text
            End If
        End Get
        Set(value As String)
            INDTxtRecordNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha del acta del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RecordDate As Date? Implements IInventoryContract.RecordDate
        Get
            Return INDDteRecordDate.EditValue
        End Get
        Set(value As Date?)
            INDDteRecordDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los tipos de negosiacion de la cotizacion del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NegotiationType As String Implements IInventoryContract.NegotiationType
        Get
            If (INDTxtNegotiationType.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDTxtNegotiationType.Text
            End If
        End Get
        Set(value As String)
            INDTxtNegotiationType.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la aprovacion de la cotizacion del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Approved As String Implements IInventoryContract.Approved
        Get
            If (INDTxtApproved.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDTxtApproved.Text
            End If
        End Get
        Set(value As String)
            INDTxtApproved.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha limiete de la cotizacion del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Deadline As Date? Implements IInventoryContract.Deadline
        Get
            Return INDDteDeadLine.EditValue
        End Get
        Set(value As Date?)
            INDDteDeadLine.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha de vigencia de la cotizacion del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidityDate As Date? Implements IInventoryContract.ValidityDate
        Get
            Return INDDteValidityDate.EditValue
        End Get
        Set(value As Date?)
            INDDteValidityDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el subtotal del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Value As Decimal Implements IInventoryContract.Value

    ''' <summary>
    ''' Obtiene o establece el valor IVA del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IvaValue As Decimal Implements IInventoryContract.IvaValue

    ''' <summary>
    ''' Obtiene o establece el valor descuento del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DiscountValue As Decimal Implements IInventoryContract.DiscountValue

    ''' <summary>
    ''' Obtiene o establece el valor Total del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TotalValue As Decimal Implements IInventoryContract.TotalValue
        Get
            Return INDtxtValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de la moneda, recibe como parametro opcional la abreviacion para cuando se postula manualmente el Id
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    ''' <returns></returns>
    Public Property CurrencyId(Optional _currencyAbbreviation As String = Nothing) As Integer Implements IInventoryContract.CurrencyId
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
    Public Property CurrencyDatasource As XPInstantFeedbackSource Implements IInventoryContract.CurrencyDatasource
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
#End Region

#Region "Properties"

#Region "Budget Interface"

    Public ReadOnly Property BudgetInterface As Boolean
        Get
            Return If(PaymentsSettingPaymentsXpo Is Nothing, False, PaymentsSettingPaymentsXpo.BudgetInterface)
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

    Public Property BudgetaryEntityXpo As DevExpress.Xpo.XPCollection Implements IInventoryContract.BudgetaryEntityXpo
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

    Public Property BudgetaryValidityXpo As XPCollection Implements IInventoryContract.BudgetaryValidityXpo
        Get
            Return INDSleBudgetaryValidityId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDSleBudgetaryValidityId.Properties.DataSource = value
        End Set
    End Property

    Public Property AvailabilityXpo As XPInstantFeedbackSource Implements IInventoryContract.AvailabilityXpo
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
    Public Property ListContractAvailability As List(Of InventoryContractAvailability) Implements IInventoryContract.ListContractAvailability
        Get
            Return INDgcAvailability.DataSource
        End Get
        Set(value As List(Of InventoryContractAvailability))
            INDgcAvailability.DataSource = value
            INDgcAvailability.RefreshDataSource()
        End Set
    End Property

    ''' <summary>
    ''' Representa el listado de detalles de disponibilidades
    ''' </summary>
    Private ListDeleteContractAvailability As List(Of InventoryContractAvailability)

#End Region

    ''' <summary>
    ''' Especifica el origen segun de la Cuantia
    ''' </summary>
    ''' <remarks></remarks>
    Private _source As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListSource As List(Of Tuple(Of Integer, String))
        Get
            If _source Is Nothing Then
                _source = New List(Of Tuple(Of Integer, String))
                _source.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("InventoryContract_SimpleSource", NAME_MODULE)))
                _source.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("InventoryContract_CallOffer", NAME_MODULE)))
                _source.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("InventoryContract_TenderPublished", NAME_MODULE)))
            End If
            Return _source
        End Get
    End Property

    ''' <summary>
    ''' Especifica el proceso de compras
    ''' </summary>
    ''' <remarks></remarks>
    Private _purchaseProc As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListPurchaseProcess As List(Of Tuple(Of Integer, String))
        Get
            If _purchaseProc Is Nothing Then
                _purchaseProc = New List(Of Tuple(Of Integer, String))
                _purchaseProc.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("InventoryContract_Direct", NAME_MODULE)))
                _purchaseProc.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("InventoryContract_JointBargaining", NAME_MODULE)))
            End If
            Return _purchaseProc
        End Get
    End Property

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Public Property _sequence As Domain.Entities.InventorySequence
    Public Property Sequense As InventorySequence Implements IInventoryContract.Sequense
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IInventoryContract.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IInventoryContract.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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
    ''' Obtiene o establece el datasource del control de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListSupplier As XPInstantFeedbackSource Implements IInventoryContract.ListSupplier
        Get
            Return INDSleSupplier.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleSupplier.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource del control de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListContractType As XPInstantFeedbackSource Implements IInventoryContract.ListContractType
        Get
            Return INDGleContractType.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDGleContractType.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los productos
    ''' </summary>
    ''' <remarks></remarks>
    Property ListProducts As Domain.Entities.TrackableCollection(Of InventoryContractDetail)
        Get
            Return INDGcProducts.DataSource
        End Get
        Set(value As Domain.Entities.TrackableCollection(Of InventoryContractDetail))
            INDGcProducts.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece las acciones de abilitacion de los controles del frontal
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IInventoryContract.ActionsOnControls
        Set(value As Boolean)
            ' Datos Principales
            INDlyContract.BeginUpdate()
            INDBtnCode.Enabled = Not value
            INDGleContractType.Enabled = value
            INDdeDocumentDate.Enabled = value
            INDTxtContractNumber.Enabled = value
            INDDteInitialDate.Enabled = value
            INDTxtContractNumber.Enabled = value
            INDDteEndDate.Enabled = value
            INDMmoDescription.Enabled = value
            INDsleCurrency.Enabled = value
            INDtxtValue.Enabled = value
            ' Detalles del Contrato
            INDSleSupplier.Enabled = value
            INDTxtPaymentMethod.Enabled = value
            INDTxtDeliveryMethod.Enabled = value
            INDTxtDeliveryPlacer.Enabled = value
            INDGleSourcerOrder.Enabled = value
            INDGlePurchaseProcess.Enabled = value
            INDGleExclusivity.Enabled = value
            INDgleManageProducts.Enabled = value
            ' Información Adicional
            INDGleOnlyGuarantee.Enabled = value
            INDTxtTechnicalSupervicion.Enabled = value
            INDTxtSupervicionExecution.Enabled = value
            INDTxtAvailability.Enabled = value
            INDTxtResolution.Enabled = value
            INDTxtResolutionDate.Enabled = value
            ' Clausulas y Anexos
            INDMmoClauses.Enabled = value
            INDMmoAttachments.Enabled = value
            ' Cotización
            INDTxtQuoteNumber.Enabled = value
            INDDteQuoteDate.Enabled = value
            INDTxtRecordNumber.Enabled = value
            INDDteRecordDate.Enabled = value
            INDTxtNegotiationType.Enabled = value
            INDTxtApproved.Enabled = value
            INDDteDeadLine.Enabled = value
            INDDteValidityDate.Enabled = value
            'Productos
            INDBtnAddProduct.Enabled = value
            INDGcProducts.Enabled = value

            INDSleBudgetaryEntityId.Enabled = value
            INDSleBudgetaryValidityId.Enabled = value
            INDsleAvailability.Enabled = value
            INDbtnAddAvailability.Enabled = value
            INDgcAvailability.Enabled = value

            BarraBotones.StatusRecordVisible = value
            INDlyContract.EndUpdate()
            If value Then
                INDGleContractType.Focus()
            Else
                INDBtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el proveedor con sus lineas de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SuppliersDistributionLinesXpo As Xpo.XPInstantFeedbackSource Implements IInventoryContract.SuppliersDistributionLinesXpo
        Get
            Return CType(INDSleSupplier.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As Xpo.XPInstantFeedbackSource)
            INDSleSupplier.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "ICrud"

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewContract()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click Eliminar.
    ''' </summary>
    Public Sub Anular() Implements ICrudBase.Eliminar
        If Status <> 1 Then
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("InventoryContract_ContractConfirm", NAME_MODULE)
            Exit Sub
        End If
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.Status = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click Guardar.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If ValidateControls() Then
            If ManageProducts Then
                If ListProducts Is Nothing OrElse ListProducts.Count = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("InventoryContract_EmpyList"), NAME_MODULE)
                    Exit Sub
                End If
            Else
                If TotalValue <= 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Debe establecer un Valor del Contrato"
                    Exit Sub
                End If
            End If

            If Not ValidateInterfaceBudget() Then
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        Try
            AssigningValues()
            Using model As New MInventoryContract(Me.Tag.ToString())
                AsyncLoader(True)

                Dim Result = Await model.SaveInventoryContract(contract, _idCurrentSequence)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    If contract.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Mensaje(EeventViewerImages.Informacion) = Result.Message
                    Me.contract = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDBtnCode.Enabled = False
                    If Result.MessageResult IsNot Nothing AndAlso Result.MessageResult.Count > 0 AndAlso Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

#End Region

#Region "Events"

#Region "Activated"

    ''' <summary>
    ''' Se dispara cuando el formulario se activa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmInventoryContract_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBtnCode.Enabled Then
            INDBtnCode.Focus()
        End If
    End Sub

#End Region

#Region "FromClosing"

    ''' <summary>
    ''' Se dispara cuando el formulario se va a cerrar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmInventoryContract_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing
        Presenter = Nothing
        Model = Nothing
        contract = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        record = Nothing
        CurrentContractType = Nothing
        ItemContractDetail = Nothing
        OnlyRead = Nothing
        _isLoading = Nothing
        nameResourse = Nothing
        varImp = Nothing
    End Sub

    ''' <summary>
    ''' Se dispara cuando incial la carga del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmInventoryContract_LoadAsync(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Personalizacion de la rejilla, muestra las columnas ocultas como mas infomacion
        IndigoGridView1.MoreInfoColunmns(INDGvProducts)
        ' Agrega a la rejilla la columna de Acciones de productos
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Edit)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvProducts, ListActions)
        IndigoGridControl1.RefreshGrid(INDGcProducts)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvProducts.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        Dim ListActions2 As New List(Of eAcciones)
        ListActions2.Add(eAcciones.Remove)
        IndigoGridView2.SetListAcction(INDviewAvailability, ListActions2)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewAvailability.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        Me.LayoutControls.SetIsCustomizable(Me.INDlyContract, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PInventoryContract(Me)
        Me.LoadSettings()
        Presenter.GetSequense()

        INDGleSourcerOrder.Properties.DataSource = ListSource
        INDGlePurchaseProcess.Properties.DataSource = ListPurchaseProcess

        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control para cargar los controles del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDBtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBtnCode.KeyDown
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
                    Await Me.NewContract()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDsleBudgetaryEntityId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleBudgetaryEntityId.QueryPopUp
        Presenter.InitializeBudgetaryEntity()
    End Sub

    Private Sub INDsleBudgetaryValidityId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleBudgetaryValidityId.QueryPopUp
        Presenter.InitializeBudgetaryValidity(BudgetaryEntityId)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de disponibilidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleAvailability_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAvailability.QueryPopUp
        If BudgetaryEntityId IsNot Nothing Then
            Presenter.InitializeAvailabilityDetails(BudgetaryEntityId)
        End If
    End Sub

    ''' <summary>
    ''' Realiza la consulta del combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSupplier_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleSupplier.QueryPopUp
        If INDSleSupplier.Properties.DataSource Is Nothing Then
            Presenter.InitializeSupplier()
        End If
    End Sub

    ''' <summary>
    ''' Realiza la consulta del combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleContractType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDGleContractType.QueryPopUp
        If INDGleContractType.Properties.DataSource Is Nothing Then
            Presenter.LoadContractTypesByStatus()
        End If
    End Sub

    ''' <summary>
    ''' evento para consultar las monedas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCurrency_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCurrency.QueryPopUp
        If Me.CurrencyDatasource Is Nothing Then
            Presenter.InitializeCurrency()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDSleSupplier_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleSupplier.EditValueChanged
        If SupplierDistributionLineId > 0 Then
            If _isLoading = False Then
                Dim suppplierMainAccount = DirectCast(DirectCast(viewSupplier.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonSuppliersDistibutionLineXpo)
                SupplierId = suppplierMainAccount.IdSupplier.Id
            End If
        Else
            SupplierId = 0
        End If
    End Sub

    Private Sub INDDteInitialDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDDteInitialDate.EditValueChanged
        If INDDteInitialDate.EditValue IsNot Nothing Then
            INDDteEndDate.Properties.MinValue = INDDteInitialDate.EditValue
        End If
    End Sub

    Private Sub INDgleManageProducts_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleManageProducts.EditValueChanged
        If ManageProducts Then
            INDLciValue.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            INDlyGpProducts.Visibility = XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciValue.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            INDlyGpProducts.Visibility = XtraLayout.Utils.LayoutVisibility.Never

            If ListProducts IsNot Nothing Then
                If ListDeleteProducts Is Nothing Then
                    ListDeleteProducts = New Domain.Entities.TrackableCollection(Of InventoryContractDetail)
                End If

                For Each detail In ListProducts
                    ListDeleteProducts.Add(detail)
                Next

                ListProducts.Clear()
            End If
        End If
        Me.EnableOrDisableCurrecy()
    End Sub

    Private Sub INDsleBudgetaryEntityId_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleBudgetaryEntityId.EditValueChanged
        CleanBudgetInterface(1)

        If BudgetaryEntityId IsNot Nothing Then
            Presenter.InitializeBudgetaryValidity(BudgetaryEntityId)
            SetFirstOrDefaultValidity()
        End If
    End Sub

    Private Sub INDsleBudgetaryValidityId_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleBudgetaryValidityId.EditValueChanged
        CleanBudgetInterface(2)

        If BudgetaryValidityId IsNot Nothing Then
            Presenter.InitializeAvailabilityDetails(BudgetaryValidityId)
        End If
    End Sub

    ''' <summary>
    ''' evento cuando se cambia la moneda de la cabecera
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCurrency_Properties_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCurrency.Properties.EditValueChanged

        If Me.CurrencyId = 0 OrElse {2, 3, 4}.Contains(Me.Status) Then
            Exit Sub
        End If

        If Me.CurrencySelected IsNot Nothing Then
            Me.SetCurrencyUI(Me.CurrencySelected?.Abbreviation)
        End If

        If Me.CurrencyId = Me.indigo.OfficialCurrencyId Then
            Me.TRM = New TRM With {.CurrencyId = Me.CurrencyId, .OfficialCurrencyId = Me.indigo.OfficialCurrencyId, .Value = 1}
            Exit Sub
        End If

        Dim _stateResult = GetTRM(Me.indigo.OfficialCurrencyId, Me.CurrencyId)

        If _stateResult Then
            Exit Sub
        End If

        If Not _isLoading Then
            Me.CurrencyId(Me.indigo.CurrencyISO4217) = Me.indigo.OfficialCurrencyId
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se ejecuta al cambiar el valor del control de repositorio de la rejilla para el valor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepTxtValue_EditValueChanging(sender As Object, e As XtraEditors.Controls.ChangingEventArgs) Handles INDrepTxtValue.EditValueChanging
        If e IsNot Nothing AndAlso e.NewValue IsNot Nothing Then
            Dim entity As InventoryContractAvailability = INDviewAvailability.GetFocusedRow()
            If CDec(e.NewValue) > CDec(entity.Balance) Then
                Mensaje(EeventViewerImages.Advertencia) = "El valor a ejecutar no puede ser mayor al saldo de la disponibilidad"
                e.Cancel = True
                Exit Sub
            End If
            entity.Value = e.NewValue
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Manejador del evento que se dispara al darle click sobre el boton del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Carga el formulario de tipos de contrato
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleContractType_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDGleContractType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmInventoryContractType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de agregar disponibilidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddAvailability_Click(sender As Object, e As EventArgs) Handles INDbtnAddAvailability.Click
        If INDsleAvailability.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una disponibilidad"
            INDsleAvailability.Focus()
            Exit Sub
        End If

        If ListContractAvailability Is Nothing Then
            ListContractAvailability = New List(Of InventoryContractAvailability)
        End If

        If (From x In ListContractAvailability Where x.AvailabilityDetailId = INDsleAvailability.EditValue Select x).Count() > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "La disponibilidad " + INDsleAvailability.Text + " ya existe en la lista"
            INDsleAvailability.Focus()
            Exit Sub
        End If

        Dim viewXpo As ViewListAvailabilityDetailXpo = DirectCast(INDsleAvailability.GetSelectedObject(), ViewListAvailabilityDetailXpo)
        If viewXpo Is Nothing Then
            viewXpo = Presenter.GetAvailabilityDetailById(INDsleAvailability.EditValue)
        End If

        Dim inventoryContractAvailability = New InventoryContractAvailability()
        With inventoryContractAvailability
            .AvailabilityDetailId = viewXpo.Id
            .AvailabilityCode = viewXpo.AvailabilityCode
            .CategoryCodeName = viewXpo.CategoryCodeName
            .FinancialSourceCodeName = viewXpo.FinancialSourceCodeName
            .RevenueTypeCodeName = viewXpo.RevenueTypeCodeName
            .Balance = viewXpo.Balance
            .Value = 0
        End With

        ListContractAvailability.Add(inventoryContractAvailability)
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
    Private Sub INDBtnAddProduct_Click(sender As Object, e As EventArgs) Handles INDBtnAddProduct.Click
        If OnlyRead = False Then
            InstantiatePopup(False, Nothing, ListProducts)
        End If
    End Sub

#End Region

#Region "MenuActions"

    ''' <summary>
    ''' Acciones de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        ItemContractDetail = INDGvProducts.GetRow(INDGvProducts.FocusedRowHandle)
        If ItemContractDetail IsNot Nothing Then
            Select Case sender.Tag
                Case "Remove"
                    DeleteProduct()
                Case "Edit"
                    InstantiatePopup(OnlyRead, ItemContractDetail, Nothing)
            End Select
        End If
    End Sub

    ''' <summary>
    ''' Acciones de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions, IndigoGridView2.Click_ButtonAction
        DeleteAvailability()
    End Sub

#End Region

#End Region

#Region "Methods"

    Private Sub LoadSettings()
        Try
            AsyncLoader(True)
            'Se obtiene los parámetros de pagos por unidad operativa
            If BarraBotones.OperatingUnitValue <> Nothing AndAlso BarraBotones.OperatingUnitValue > 0 Then
                PaymentsSettingPaymentsXpo = Presenter.GetSettingsPaymentsByOperatingUnitId(BarraBotones.OperatingUnitValue)
                InventorySettingsXpo = Presenter.GetSettingsInventoryByOperatingUnitId(BarraBotones.OperatingUnitValue)
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
                If ListContractAvailability IsNot Nothing AndAlso ListContractAvailability.Any() Then
                    If ListDeleteContractAvailability Is Nothing Then
                        ListDeleteContractAvailability = New List(Of InventoryContractAvailability)
                    End If

                    For Each AccountPayableCommitment In ListContractAvailability
                        If AccountPayableCommitment.Id > 0 Then
                            ListDeleteContractAvailability.Add(AccountPayableCommitment)
                        End If
                    Next
                End If
                ListContractAvailability = Nothing
                contract.InventoryContractAvailability.Clear()
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
        If contract IsNot Nothing AndAlso contract.Status <> 1 Then
            Exit Sub
        End If

        Dim status As Boolean = False
        If ListContractAvailability IsNot Nothing AndAlso ListContractAvailability.Any() Then
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
                If ListContractAvailability Is Nothing OrElse Not ListContractAvailability.Any(Function(d) d.Value > 0) Then
                    listErrors.AppendLine("Debe agregar al menos una disponibilidad con el cual realizar el compromiso presupuestal")
                End If
            End If

            If ListContractAvailability IsNot Nothing AndAlso ListContractAvailability.Any Then
                Dim contractValue As Decimal = Math.Round(TotalValue, 0, MidpointRounding.AwayFromZero)
                Dim commitmentValue As Decimal = ListContractAvailability.Where(Function(d) d.Value > 0).Sum(Function(d) d.Value)
                If commitmentValue > contractValue Then
                    listErrors.AppendLine("La suma de los valores de los detalles presupuestales superan el valor del contrato")
                ElseIf commitmentValue < contractValue Then
                    listErrors.AppendLine("La suma de los valores de los detalles presupuestales son menores al valor del contrato")
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

        Dim entity As InventoryContractAvailability = INDviewAvailability.GetFocusedRow()
        ListContractAvailability.Remove(entity)

        If entity.Id > 0 Then
            If ListDeleteContractAvailability Is Nothing Then
                ListDeleteContractAvailability = New List(Of InventoryContractAvailability)
            End If
            entity.MarkAsDeleted()
            ListDeleteContractAvailability.Add(entity)
        End If

        EnableBudgetInterface()
        INDgcAvailability.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Elimina un producto
    ''' </summary>
    Private Sub DeleteProduct()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim entity As InventoryContractDetail = INDGvProducts.GetFocusedRow()
        ListProducts.Remove(entity)

        If entity.Id > 0 Then
            If ListDeleteProducts Is Nothing Then
                ListDeleteProducts = New Domain.Entities.TrackableCollection(Of InventoryContractDetail)
            End If
            entity.MarkAsDeleted()
            ListDeleteProducts.Add(entity)
        End If
        Me.EnableOrDisableCurrecy()
        RefreshTotals()
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.contract.Code, Me.contract.ContractNumber),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.contract.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.contract.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.contract.Code, Me.contract.ContractNumber)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.contract.Code)
            Return Me._doc
        End If
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
        listStates.Add(New StatusRecord With {.StatusValue = "0", .StatusName = String.Empty, .StatusColor = System.Drawing.Color.White})
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = ResourceManager.GetString("StatusLegalized"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Genera una nueva entidad de Contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function NewContract() As Task
        contract = New InventoryContract()
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
        End If

        If BudgetInterface Then
            Presenter.InitializeBudgetaryEntity()
            Me.SetFirstOrDefaultEntity()
        End If
    End Function

    ''' <summary>
    ''' Despliega el formulario de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 150},
                              New ColumnInfo With {.Caption = "Tipo Contrato", .FieldName = "ContractTypeId.CodeName", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "No. Contrato", .FieldName = "ContractNumber", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Fecha Inicial", .FieldName = "InitialDate", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Fecha Final", .FieldName = "EndDate", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Proveedor", .FieldName = "SupplierId.CodeName", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = 200}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllInventoryContracts
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Devuelve el listado para mostrar el Total del contrato y sus derivados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfoServiceOrder() As Tuple(Of String, String, String, String)
        Return New Tuple(Of String, String, String, String)(String.Format(_IvaValue, "Numeric", "c2"), String.Format(DiscountValue, "Numeric", "c2"), String.Format(TotalValue, "Numeric", "c2"), "")
        ' Return New Tuple(Of String, String, String)(String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", _IvaValue), String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", DiscountValue), String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", TotalValue))
    End Function

    ''' <summary>
    ''' Limpia los controles y las variables
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyContract.BeginUpdate()
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        ListDeleteProducts = Nothing
        ListProducts = New Domain.Entities.TrackableCollection(Of InventoryContractDetail)
        INDGcProducts.DataSource = Nothing

        Code = String.Empty
        ContractTypeId = Nothing
        INDGleContractType.Properties.NullText = String.Empty
        ContractNumber = String.Empty
        DocumentDate = Nothing
        InitialDate = Nothing
        EndDate = Nothing
        Description = String.Empty
        SupplierId = Nothing
        INDSleSupplier.Properties.NullText = String.Empty
        SupplierDistributionLineId = Nothing
        PaymentMethod = String.Empty
        DeliveryMethod = String.Empty
        DeliveryPlace = String.Empty
        SourceOrder = Nothing
        PurchaseProcess = Nothing
        Exclusivity = Nothing
        ManageProducts = Nothing
        OnlyGuarantee = Nothing
        TechnicalSupervicion = String.Empty
        SupervisionExecution = String.Empty
        Availability = String.Empty
        Resolution = String.Empty
        ResolutionDate = Nothing
        Clauses = String.Empty
        Attachments = String.Empty
        QuoteNumber = String.Empty
        QuoteDate = Nothing
        RecordNumber = String.Empty
        RecordDate = Nothing
        NegotiationType = String.Empty
        Approved = String.Empty
        Deadline = Nothing
        ValidityDate = Nothing
        Value = 0
        IvaValue = 0
        DiscountValue = 0
        TotalValue = 0
        Status = 0
        Me.CurrencyDatasource = Nothing
        Me.CurrencyId(Me.indigo?.CurrencyISO4217) = If(Me.indigo Is Nothing, 0, Me.indigo.OfficialCurrencyId)

        _isLoading = False
        OnlyRead = False
        ReadOnlyControls(False)
        ActionsOnControls = False
        nameResourse = String.Empty

        ListContractAvailability = Nothing
        ListDeleteContractAvailability = Nothing
        INDgcAvailability.DataSource = Nothing
        INDsleAvailability.EditValue = Nothing

        ctrTmp.PrintInfo()
        INDBtnCode.Focus()
        INDlyContract.EndUpdate()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Retorna el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ReturnValue As String, ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método que carga los controles de la consulta del contrato
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
                Using Model As New MInventoryContract(CStr(Me.Tag))
                    AsyncLoader(True)
                    contract = (Await Model.GetInventoryContract(INDBtnCode.Text.Trim)).ObjectEmbbeded
                    INDlyContract.BeginUpdate()
                    If contract IsNot Nothing AndAlso contract.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(contract.Id))

                            _isLoading = True
                            With contract
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                ContractTypeId = .ContractTypeId
                                DocumentDate = If(.DocumentDate Is Nothing, .InitialDate, .DocumentDate)
                                INDGleContractType.Properties.NullText = .DescriptionContractType
                                ContractNumber = .ContractNumber
                                InitialDate = .InitialDate
                                EndDate = .EndDate
                                Description = .Description
                                SupplierDistributionLineId = .SupplierDistributionLineId
                                INDSleSupplier.Properties.NullText = .DescriptionSupplier
                                SupplierId = .SupplierId
                                PaymentMethod = .PaymentMethod
                                DeliveryMethod = .DeliveryMethod
                                DeliveryPlace = .DeliveryPlace
                                SourceOrder = .SourceOrder
                                PurchaseProcess = .PurchaseProcess
                                Exclusivity = .Exclusivity
                                ManageProducts = .ManageProducts
                                OnlyGuarantee = .OnlyGuarantee
                                TechnicalSupervicion = .TechnicalSupervicion
                                SupervisionExecution = .SupervisionExecution
                                Availability = .Availability
                                Resolution = .Resolution
                                ResolutionDate = .ResolutionDate
                                Clauses = .Clauses
                                Attachments = .Attachments
                                QuoteNumber = .QuoteNumber
                                QuoteDate = .QuoteDate
                                RecordNumber = .RecordNumber
                                RecordDate = .RecordDate
                                NegotiationType = .NegotiationType
                                Approved = .Approved
                                Deadline = .Deadline
                                ValidityDate = .ValidityDate
                                Value = .Value
                                IvaValue = .IvaValue
                                DiscountValue = .DiscountValue
                                TotalValue = .TotalValue
                                BarraBotones.StatusRecord = .Status.ToString()
                                ListProducts = .InventoryContractDetail
                                Me.CurrencyId(.Currency?.Abbreviation) = .CurrencyId
                                If BudgetInterface Then
                                    BudgetaryEntityId = .BudgetaryEntityId
                                    INDSleBudgetaryEntityId.Properties.NullText = .BudgetaryEntityDescription
                                    BudgetaryValidityId = .BudgetaryValidityId
                                    INDSleBudgetaryValidityId.Properties.NullText = .BudgetaryValidityDescription
                                    ListContractAvailability = .InventoryContractAvailability.ToList()
                                End If
                            End With

                            ctrTmp.PrintInfo()
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.contract.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = contract.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If

                            ActionsOnControls = True
                            If Status = 1 Then
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                Me.ReadOnlyControls(False)
                                OnlyRead = False
                                Me.EnableOrDisableCurrecy()
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndPrint)
                                Me.ReadOnlyControls(True)
                                OnlyRead = True

                                If Status = 2 Then
                                    Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Confirmar, "Legalizar")
                                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                                    INDDteInitialDate.Properties.ReadOnly = False
                                    INDDteEndDate.Properties.ReadOnly = False
                                    INDTxtTechnicalSupervicion.Properties.ReadOnly = False
                                    INDTxtSupervicionExecution.Properties.ReadOnly = False
                                End If
                            End If

                            EnableBudgetInterface()
                            Me.BarraBotones.PrintReport(PrintReportAction.None, contract.Id, 0, contract.Id)
                            Me.BarraBotones.SetDocuments(contract.Id, Me.Tag.ToString(), Nothing, GetType(InventoryContract).Name)

                            _isLoading = False
                            AsyncLoader(False)

                            INDGleContractType.Focus()
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewContract()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBtnCode.Focus()
                        End If
                    End If
                    INDlyContract.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' metodo para instanciar el formulario de concepto de recibo de caja
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InstantiatePopup(OnlyRead As Boolean, _Detaill As InventoryContractDetail,
                                 _listProducts As Domain.Entities.TrackableCollection(Of InventoryContractDetail),
                                 Optional EditMode As Boolean = False)
        Me.Cursor = ChangeCursorIndigo()
        Using formulario As New PopupProductsContract(OnlyRead, _Detaill, _listProducts,
                                                      _currency:=New Currency With {.Id = Me.CurrencyId, .Abbreviation = Me.CurrencyAbbreviation},
                                                        _tRMValue:=Me.TRM.Value)
            AddHandler formulario.AddProductContract, AddressOf ReturnPopupAddProductContract

            formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            formulario.ViewModeEditHold = True
            formulario.StartPosition = FormStartPosition.CenterParent
            formulario.Size = New Size(800, 700)
            Dim transparent = New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' metodo para agregar un concepto de recibo de caja a la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnPopupAddProductContract(sender As Object, e As AddProductContractEventArgs)
        If e.ListInventoryContractDetail IsNot Nothing Then
            ListProducts = e.ListInventoryContractDetail
        End If
        If e.ItemInventorycontractDetail IsNot Nothing Then
            ListProducts.Remove(ItemContractDetail)
            ListProducts.Add(e.ItemInventorycontractDetail)
        End If
        Me.EnableOrDisableCurrecy()
        RefreshTotals()
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With contract
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .OperatingUnitId = BarraBotones.OperatingUnit.Id
            .ContractTypeId = ContractTypeId
            .DocumentDate = DocumentDate
            .ContractNumber = ContractNumber
            .InitialDate = InitialDate
            .EndDate = EndDate
            .Description = Description
            .SupplierId = SupplierId
            .SupplierDistributionLineId = SupplierDistributionLineId
            .PaymentMethod = PaymentMethod
            .DeliveryMethod = DeliveryMethod
            .DeliveryPlace = DeliveryPlace
            .SourceOrder = SourceOrder
            .PurchaseProcess = PurchaseProcess
            .Exclusivity = Exclusivity
            .ManageProducts = ManageProducts
            .OnlyGuarantee = OnlyGuarantee
            .TechnicalSupervicion = TechnicalSupervicion
            .SupervisionExecution = SupervisionExecution
            .Availability = Availability
            .Resolution = Resolution
            .ResolutionDate = ResolutionDate
            .Clauses = Clauses
            .Attachments = Attachments
            .QuoteNumber = QuoteNumber
            .QuoteDate = QuoteDate
            .RecordNumber = RecordNumber
            .RecordDate = RecordDate
            .NegotiationType = NegotiationType
            .Approved = Approved
            .Deadline = Deadline
            .ValidityDate = ValidityDate
            .Value = Value
            .IvaValue = IvaValue
            .DiscountValue = DiscountValue
            .TotalValue = TotalValue
            .Status = Me.Status
            .CurrencyId = Me.CurrencyId

            If ManageProducts Then
                If ListProducts IsNot Nothing AndAlso ListProducts.Count > 0 Then
                    For Each item In ListProducts
                        .InventoryContractDetail.Add(item)
                    Next
                End If
            End If

            If ListDeleteProducts IsNot Nothing AndAlso ListDeleteProducts.Count > 0 Then
                For Each item In ListDeleteProducts
                    item.MarkAsDeleted()
                    .InventoryContractDetail.Add(item)
                Next
            End If

            If BudgetInterface Then
                If ListContractAvailability IsNot Nothing Then
                    contract.BudgetaryEntityId = BudgetaryEntityId
                    contract.BudgetaryEntityDescription = INDSleBudgetaryEntityId.Text
                    contract.BudgetaryValidityId = BudgetaryValidityId
                    contract.BudgetaryValidityDescription = INDSleBudgetaryValidityId.Text

                    For Each contractAvailability In ListContractAvailability
                        If contractAvailability.Value > 0 Then
                            If contractAvailability.ChangeTracker.State = ObjectState.Added Then
                                contract.InventoryContractAvailability.Add(contractAvailability)
                            ElseIf contractAvailability.ChangeTracker.State = ObjectState.Modified Then
                                contractAvailability.MarkAsModified()
                            End If
                        Else
                            contractAvailability.MarkAsDeleted()
                        End If
                    Next
                End If
            End If

            If ListDeleteContractAvailability IsNot Nothing Then
                For Each contractAvailability In ListDeleteContractAvailability
                    contractAvailability.MarkAsDeleted()
                    contract.InventoryContractAvailability.Add(contractAvailability)
                Next
            End If
        End With
    End Sub

    ''' <summary>
    ''' Método utilziado para refrescar los valores de los totales
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RefreshTotals()
        If ListProducts IsNot Nothing OrElse ListProducts.Count > 0 Then
            Value = ListProducts.Sum(Function(x) x.Value)
            IvaValue = ListProducts.Sum(Function(x) x.IvaValue)
            DiscountValue = ListProducts.Sum(Function(x) x.DiscountValue)
            TotalValue = ListProducts.Sum(Function(x) x.TotalValue)
        Else
            Value = 0
            IvaValue = 0
            DiscountValue = 0
            TotalValue = 0
        End If

        ctrTmp.PrintInfo()
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.contract IsNot Nothing AndAlso Me.contract.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                INDBtnCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            INDBtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
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

        Me.INDtxtValue.Properties.Mask.Culture = _culture
        Me.INDColSubTotalValue = Window.Utils.FormatGrid(Me.INDColSubTotalValue, _currencyAbbreviation)
        Me.INDColTotalValue = Window.Utils.FormatGrid(Me.INDColTotalValue, _currencyAbbreviation)
        Me.ctrTmp.CodeISO4217 = _currencyAbbreviation
        ctrTmp.PrintInfo()
    End Sub

    ''' <summary>
    ''' Funcion que se encarga de consultar el TRM
    ''' </summary>
    ''' <param name="_currencyId"></param>
    ''' <param name="ToCurrencyId"></param>
    ''' <returns></returns>
    Private Function GetTRM(_currencyId As Integer, ToCurrencyId As Integer) As Boolean
        Using Model As New MInventoryContract("")
            Dim Result = Model.GetTRMbyCurrencyId(ToCurrencyId, _currencyId)
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
        Me.INDsleCurrency.ReadOnly = If(ListProducts?.Any(), True, False)
    End Sub
#End Region

#Region "BarButtons"

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Status = 1
        varImp = 1
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        Status = 2
        varImp = 3
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        varImp = 2
        Status = 1
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        Status = 2
        varImp = 3
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        Anular()
    End Sub

    Private Sub BarraBotones_ClickLegalize() Handles BarraBotones.ClickConfirmar
        Status = 4
        varImp = 3
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnitAsync(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            Me.LoadSettings()
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing Then
                If Not Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

End Class