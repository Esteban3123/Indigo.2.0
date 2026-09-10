'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Henry Alejandro Vargas Polania 
' Created          : 20/02/2015
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
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.InventoryRepository.View
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common.MVP
Imports Presentation.Controls
Imports Presentation.Inventory.MVP
Imports Presentation.Maintenance.MVP

#End Region

Public Class FrmRefundPurchase
    Implements IRefundPurchase, ICustomizableForm

#Region "Builder"

    ''' <summary>
    ''' Se inicializa una isntancia de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        _ctrTmp = New CtrRefoundPurchase()
        InitializeComponent()
        _ctrTmp.SetInfoFunction(AddressOf getInfoServiceOrder)
        _ctrTmp.PrintInfo()
        _ctrTmp.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(_ctrTmp)
    End Sub

    ''' <summary>
    ''' Devuelve el listado para mostrar el Total del contrato y sus derivados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfoServiceOrder() As Tuple(Of Decimal, Boolean?)
        Return New Tuple(Of Decimal, Boolean?)(TotalValue, DevolutionType)
    End Function

#End Region

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Const NAME_MODULE As String = "Inventory"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Variable que representa el control agregado a la barra de usuario
    ''' </summary>
    ''' <remarks></remarks>
    Dim _ctrTmp As CtrRefoundPurchase

    ''' <summary>
    ''' Variable que representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim _presenter As PRefundPurchase

    ''' <summary>
    ''' define la unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idOperativeUnit As Integer

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Dim _idCurrentSequence As Int64

    ''' <summary>
    ''' Prefijo seleccionado
    ''' </summary>
    Dim _prefixSelected As String

    ''' <summary>
    ''' secuencia
    ''' </summary>
    Dim _sequence As InventorySequence

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _blockRecord As BlockRecordInventory

    ''' <summary>
    ''' Tercero de la empresa actualmente seleccionada
    ''' </summary>
    Dim _currentCompany As New ThirdParty

    ''' <summary>
    ''' Variable que representa la entidad de parametros
    ''' </summary>
    ''' <remarks></remarks>
    Dim _settingsInventory As SettingInventory

    ''' <summary>
    ''' entidad de proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim _supplier As New Supplier

    ''' <summary>
    ''' entidad de proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim _currency As New Currency

    ''' <summary>
    ''' Variable que representa la entdad principal de devolucion de comprobante de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Dim _entranceDevolution As EntranceVoucherDevolution

    ''' <summary>
    ''' Variable que representa la entidad del comprobante de entrada a devolver
    ''' </summary>
    Dim _entranceVoucher As EntranceVoucher

    ''' <summary>
    ''' Obtiene el listado de obligaciones asociadas a los compromisos que realizó el comprobante de entrada
    ''' </summary>
    Dim _listEntranceVoucherDevolutionObligationBudget As List(Of EntranceVoucherDevolutionObligationBudget)

    ''' <summary>
    ''' Obtiene el listado de eliminados de obligaciones asociadas a los compromisos que realizó el comprobante de entrada
    ''' </summary>
    Dim _listDeleteEntranceVoucherDevolutionObligationBudget As List(Of EntranceVoucherDevolutionObligationBudget)

    ''' <summary>
    ''' Obtiene el listado de devoluciones ya realizadas al comprobante de entrada
    ''' </summary>
    Dim _listEntranceVoucherDevolutionByEntranceVoucher As List(Of ViewEntranceVoucherDevolutionXpo)

    ''' <summary>
    ''' Bandera que identifica cuando el formulario esta cargando un registro
    ''' </summary>
    ''' <remarks></remarks>
    Dim _isLoading As Boolean = False

    ''' <summary>
    ''' Bandera que identifica cuando el registro esta cargando un registro
    ''' </summary>
    ''' <remarks></remarks>
    Dim _isReLoading As Boolean = False

#End Region

#Region "Properties"

    ''' <summary>
    ''' Otiene o establece el valor del tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IRefundPurchase.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el control de layout
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IRefundPurchase.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que modifica los estados de los controles para personalizar el formulario
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IRefundPurchase.ActionsOnControls
        Set(value As Boolean)
            LyRefundPurchase.BeginUpdate()

            'Datos Principales
            INDBtnCode.Enabled = Not value
            INDDteDocumentDate.Enabled = value
            INDSleWarehouse.Enabled = value
            INDTxtDescription.Enabled = value
            'Información Comprobante de entrada
            INDSleVoucherCode.Enabled = value
            INDDteDateEntranceVoucher.Enabled = value
            INDTxtSupplier.Enabled = value
            INDTxtWarehouse.Enabled = value
            INDTxtInvoiceNumber.Enabled = value
            INDTxtInvoiceDate.Enabled = value
            INDTxtDayPeriod.Enabled = value
            'Flete
            INDTxtFreightValue.Enabled = value
            INDTxtFreightInvoice.Enabled = value
            INDSpnFreightIvaPercentage.Enabled = value
            INDTxtFregithIVAValue.Enabled = value
            'Información Factura
            INDTxtSubTotal.Enabled = value
            INDTxtDiscountValue.Enabled = value
            INDTxtValueTax.Enabled = value
            INDTxtWhithholdigTaxValue.Enabled = value
            INDTxtWhithholdigICAValue.Enabled = value
            INDTxtWhithholdigSourceValue.Enabled = value
            INDTxtOtherRetention.Enabled = value
            INDTxtOtherDeduction.Enabled = value
            INDTxtTotalValue.Enabled = value
            INDGcProducts.Enabled = value
            BarraBotones.StatusRecordVisible = value

            INDGcProducts.Enabled = value
            INDgcObligation.Enabled = value
            INDGcOtherWithholding.Enabled = value
            INDGcOtherDeduction.Enabled = value

            LyRefundPurchase.EndUpdate()

            If value Then
                INDDteDocumentDate.Focus()
            Else
                INDBtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Public Property Sequense As InventorySequence Implements IRefundPurchase.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As InventorySequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As InventorySequenceDetail In Me._sequence.InventorySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IRefundPurchase.Code
        Get
            If INDBtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
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
    ''' Obtiene o establece la fecha del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As DateTime? Implements IRefundPurchase.DocumentDate
        Get
            Return INDDteDocumentDate.EditValue
        End Get
        Set(value As DateTime?)
            INDDteDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la descripcion del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description As String Implements IRefundPurchase.Description
        Get
            Return INDTxtDescription.Text
        End Get
        Set(value As String)
            INDTxtDescription.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el almacen del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WarehouseId As Integer? Implements IRefundPurchase.WarehouseId
        Get
            Return INDSleWarehouse.EditValue
        End Get
        Set(value As Integer?)
            INDSleWarehouse.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estable el comprobante de entrada de registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EntranceVoucherId As Integer? Implements IRefundPurchase.EntranceVoucherId
        Get
            Return INDSleVoucherCode.EditValue
        End Get
        Set(value As Integer?)
            INDSleVoucherCode.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Nivel de redondeo usado en el comprobante de entrada
    ''' </summary>
    Private _RoundingValue As Integer
	Public Property RoundingValue As Integer Implements IRefundPurchase.RoundingValue
		Get
			Return _RoundingValue
		End Get
		Set(value As Integer)
			_RoundingValue = value
			If _entranceDevolution IsNot Nothing Then
				_entranceDevolution.RoundingValue = _RoundingValue
			End If
		End Set
	End Property

	''' <summary>
	''' Digitos de la mascara numerica
	''' </summary>
	''' <returns></returns>
	Private ReadOnly Property MaskDigitRounding As Integer
		Get
			Return If(_RoundingValue = 0, 2, 0)
		End Get
	End Property

	''' <summary>
	''' Obtiene o establece el tipo de devolucion
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks></remarks>
	Private Property _DevolutionType As Boolean?
    Public Property DevolutionType As Boolean? Implements IRefundPurchase.DevolutionType
        Get
            Return _DevolutionType
        End Get
        Set(value As Boolean?)
            _DevolutionType = If(value Is Nothing, False, value)

            LyGroupOtherDeduction.HideControl(Not _DevolutionType)
            LyGroupFreight.HideControl(Not _DevolutionType)
            INDLyTxtOtherDeduction.HideControl(Not _DevolutionType)

            If _entranceDevolution IsNot Nothing AndAlso (_isLoading = False OrElse _entranceDevolution.Status = 1) Then
                _entranceDevolution.DevolutionType = If(value Is Nothing, False, value)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Byte Implements IRefundPurchase.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            Me.BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

#Region "Devolution Values"

    ''' <summary>
    ''' Obtiene o establece el valor del flete del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FreightValue As Decimal Implements IRefundPurchase.FreightValue
        Get
            Return INDTxtFreightValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtFreightValue.EditValue = value
            If _entranceDevolution IsNot Nothing AndAlso _isLoading = False Then
                _entranceDevolution.FreightValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece e porcentaje de iva del flete
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FreightIVAPercentage As Decimal Implements IRefundPurchase.FreightIVAPercentage
        Get
            Return INDSpnFreightIvaPercentage.EditValue
        End Get
        Set(value As Decimal)
            INDSpnFreightIvaPercentage.EditValue = value
            If _entranceDevolution IsNot Nothing AndAlso _isLoading = False Then
                _entranceDevolution.FreightIVAPercentage = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del iva del flete
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FreightIVAValue As Decimal Implements IRefundPurchase.FreightIVAValue
        Get
            Return INDTxtFregithIVAValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtFregithIVAValue.EditValue = value
            If _entranceDevolution IsNot Nothing AndAlso _isLoading = False Then
                _entranceDevolution.FreightIVAValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el subtotal del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Value As Decimal Implements IRefundPurchase.Value
        Get
            Return INDTxtSubTotal.EditValue
        End Get
        Set(value As Decimal)
            INDTxtSubTotal.EditValue = value
            If _entranceDevolution IsNot Nothing AndAlso _isLoading = False Then
                _entranceDevolution.Value = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del descuento del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValueDiscount As Decimal Implements IRefundPurchase.ValueDiscount
        Get
            Return INDTxtDiscountValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtDiscountValue.EditValue = value
            If _entranceDevolution IsNot Nothing AndAlso _isLoading = False Then
                _entranceDevolution.ValueDiscount = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establce el valor del iva del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValueTax As Decimal Implements IRefundPurchase.ValueTax
        Get
            Return INDTxtValueTax.EditValue
        End Get
        Set(value As Decimal)
            INDTxtValueTax.EditValue = value
            If _entranceDevolution IsNot Nothing AndAlso _isLoading = False Then
                _entranceDevolution.ValueTax = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Variable que almacena la base de la retencion de iva
    ''' </summary>
    ''' <remarks></remarks>
    Private WithholdingTaxBase As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor del porcentaje de retencion de iva del registro
    ''' </summary>
    ''' <remarks></remarks>
    Private _WithholdingTaxPercentaje As Decimal
    Public Property WithholdingTaxPercentaje As Decimal Implements IRefundPurchase.WithholdingTaxPercentaje
        Get
            Return _WithholdingTaxPercentaje
        End Get
        Set(value As Decimal)
            _WithholdingTaxPercentaje = value
            If _entranceDevolution IsNot Nothing AndAlso _isLoading = False Then
                _entranceDevolution.WithholdingTaxPercentaje = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de la retencion de iva del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WithholdingTax As Decimal Implements IRefundPurchase.WithholdingTax
        Get
            Return INDTxtWhithholdigTaxValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtWhithholdigTaxValue.EditValue = value
            If _entranceDevolution IsNot Nothing AndAlso _isLoading = False Then
                _entranceDevolution.WithholdingTax = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el porcentaje de iva del comprobante de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Private _WithholdingIcaPercentage As Decimal
    Public Property WithholdingIcaPercentage As Decimal? Implements IRefundPurchase.WithholdingIcaPercentage
        Get
            Return _WithholdingIcaPercentage
        End Get
        Set(value As Decimal?)
            _WithholdingIcaPercentage = value
            If _entranceDevolution IsNot Nothing AndAlso _isLoading = False Then
                _entranceDevolution.WithholdingIcaPercentage = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de la retencion de ica del producto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WithholdingICA As Decimal Implements IRefundPurchase.WithholdingICA
        Get
            Return INDTxtWhithholdigICAValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtWhithholdigICAValue.EditValue = value
            If _entranceDevolution IsNot Nothing AndAlso _isLoading = False Then
                _entranceDevolution.WithholdingICA = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de la retencion en la fuente del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RetentionSource As Decimal Implements IRefundPurchase.RetentionSource
        Get
            Return INDTxtWhithholdigSourceValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtWhithholdigSourceValue.EditValue = value
            If _entranceDevolution IsNot Nothing AndAlso _isLoading = False Then
                _entranceDevolution.RetentionSource = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de las otras retenciones del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RetentionOther As Decimal Implements IRefundPurchase.RetentionOther
        Get
            Return INDTxtOtherRetention.EditValue
        End Get
        Set(value As Decimal)
            INDTxtOtherRetention.EditValue = value
            If _entranceDevolution IsNot Nothing AndAlso _isLoading = False Then
                _entranceDevolution.RetentionOther = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de las otras deducciones del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DeductionOther As Decimal Implements IRefundPurchase.DeductionOther
        Get
            Return INDTxtOtherDeduction.EditValue
        End Get
        Set(value As Decimal)
            INDTxtOtherDeduction.EditValue = value
            If _entranceDevolution IsNot Nothing AndAlso _isLoading = False Then
                _entranceDevolution.DeductionOther = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de los descuentos distritales del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DistrictTax As Decimal Implements IRefundPurchase.DistrictTax
        Get
            Dim districtTaxTmp As Decimal = 0
            If _entranceDevolution IsNot Nothing Then
                districtTaxTmp = _entranceDevolution.DistrictTax
            End If
            Return districtTaxTmp
        End Get
        Set(value As Decimal)
            If _entranceDevolution IsNot Nothing AndAlso _isLoading = False Then
                _entranceDevolution.DistrictTax = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor total del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TotalValue As Decimal Implements IRefundPurchase.TotalValue
        Get
            Return INDTxtTotalValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtTotalValue.EditValue = value
            If _entranceDevolution IsNot Nothing AndAlso _isLoading = False Then
                _entranceDevolution.TotalValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del descuetno de la orden de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NetoValue As Decimal Implements IRefundPurchase.NetoValue
        Get
            Return INDTxtSubTotal.EditValue
        End Get
        Set(value As Decimal)
            INDTxtSubTotal.EditValue = value
            If ListDeductionDevolution.Count > 0 Then
                For Each i In ListDeductionDevolution
                    i.ValueBase = value
                Next
            End If
            If ListRetentionDevolution.Count > 0 Then
                For Each i In ListRetentionDevolution
                    i.ValueBase = value
                    Dim dDevolutionValue As Decimal = value * i.RetentionPercentage / 100
                    i.DevolutionValue = Utils.RoundValue(dDevolutionValue, RoundingValue)
                Next
            End If
        End Set
    End Property

#End Region

#End Region

#Region "Datasources"

    ''' <summary>
    ''' Obtiene o establece los almacenes
    ''' </summary>
    ''' <remarks></remarks>
    Public Property ListWareHouse As XPInstantFeedbackSource Implements IRefundPurchase.ListWareHouse
        Get
            Return INDSleWarehouse.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleWarehouse.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los comprobantes de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Property ListEntranceVoucher As XPInstantFeedbackSource Implements IRefundPurchase.ListEntranceVoucher
        Get
            Return INDSleVoucherCode.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleVoucherCode.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de causas de devolución para el repositorio
    ''' </summary>
    Public Property DevolutionCauseXpo As XPInstantFeedbackSource Implements IRefundPurchase.DevolutionCauseXpo
        Get
            Return CType(INDRptSleDevolutionCause.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDRptSleDevolutionCause.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' listado del detalle de la entrance voucher
    ''' </summary>
    ''' <remarks></remarks>
    Property ListEntranceVoucherDetail As List(Of EntranceVoucherDetailBatchSerial) Implements IRefundPurchase.ListEntranceVoucherDetail
        Get
            If INDGcProducts Is Nothing Then
                Return New List(Of EntranceVoucherDetailBatchSerial)
            End If
            Return INDGcProducts.DataSource
        End Get
        Set(value As List(Of EntranceVoucherDetailBatchSerial))
            INDGcProducts.DataSource = value
            INDGcProducts.RefreshDataSource()
        End Set
    End Property

    ''' <summary>
    ''' Listado que contiene las retenciones que se le pueden aplicar al comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListRetention As New List(Of OtherWithholdingDeduction) Implements IRefundPurchase.ListRetention

    ''' <summary>
    ''' Listado que contiene las deducciones que se le pueden aplican al comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListDeduction As New List(Of OtherWithholdingDeduction) Implements IRefundPurchase.ListDeduction

    ''' <summary>
    ''' Listado que contiene las deducciones que se le pueden aplican al comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListDeductionDevolution As New List(Of EntranceVoucherDevolutionOtherDeduction) Implements IRefundPurchase.ListDeductionDevolution

    ''' <summary>
    ''' Listado que contiene las retenciones que se le pueden aplican al comprobante devolucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListRetentionDevolution As New List(Of EntranceVoucherDevolutionOtherDeduction) Implements IRefundPurchase.ListRetentionDevolution

#End Region

#Region "ICrud Base"

    ''' <summary>
    ''' Inicia una busqueda con el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

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
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 100},
                              New ColumnInfo With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = 180},
                              New ColumnInfo With {.Caption = "Almacen", .FieldName = "WarehouseId.CodeName", .ColumnWidth = 180},
                              New ColumnInfo With {.Caption = "Comprobante de Entrada", .FieldName = "EntranceVoucherId.Code", .ColumnWidth = 180},
                              New ColumnInfo With {.Caption = "Descripción", .FieldName = "Description", .ColumnWidth = 180},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = 80}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListEntranceVoucherDevolution
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Retorna el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ReturnValue As String, ReturnObject As Object)
        Await DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Deshace los cambios realziado e inicializa el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Inicializa el formulario para realizar una nueva entrada de registro
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewRefundPurchase()
        End If
    End Sub

    ''' <summary>
    ''' Método utilizado para guardar, anular o actualziar la entidad de entrancevoucherdevolution
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If _entranceDevolution IsNot Nothing AndAlso _entranceDevolution.Status < 3 Then
            Dim errors = ValidateControls()
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
        End If
        Try
            AssigningValues()
            Using Model As New MRefundPurchase(MyTag)
                AsyncLoader(True)
                Dim result = Await Model.SaveEntranceVoucherDevolution(_entranceDevolution, _idCurrentSequence, Me._sequence)
                AsyncLoader(False)
                If result.StateResult = True Then
                    _entranceDevolution = result.ObjectEmbbeded
                    If _entranceDevolution.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                        Me.BarraBotones.PrintReport(PrintReportAction.Create, _entranceDevolution.Id, 0, _entranceDevolution.Id)
                    Else
                        If _entranceDevolution.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _entranceDevolution.Id, 0, _entranceDevolution.Id)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, _entranceDevolution.Id, 0, _entranceDevolution.Id)
                        End If
                    End If

                    'Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.Deshacer()
                Else
                    If result.StateResult = False And result.StateResultAux = False Then
                        If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count() > 0 Then
                            Mensaje(EeventViewerImages.MensajeError) = result.MessageResult(0).ToString()
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                        End If
                    Else
                        If result.MessageResult.Count() > 0 Then
                            Mensaje(EeventViewerImages.MensajeError) = result.MessageResult(0).ToString()
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                        End If
                    End If

                    If _entranceDevolution.Id > 0 Then
                        Dim ev = Await Model.GetRefundPurchase(Code)
                        _entranceDevolution = ev.ObjectEmbbeded
                    Else
                        _entranceDevolution = New EntranceVoucherDevolution With {.Status = 1}
                    End If

                    Await Me.ReloadDetails()

                    INDBtnCode.Enabled = False
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Método encargado de guardar y confirmar el documento de devolución
    ''' </summary>
    ''' <param name="actions"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function SaveOrUpdateAndConfirm(actions As Integer) As Task
        Dim errors = ValidateControls()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Function
        End If
        'Se valida que las cantidades a devolver no esten en cero
        If ListEntranceVoucherDetail IsNot Nothing AndAlso ListEntranceVoucherDetail.Count > 0 Then
            If (From x In ListEntranceVoucherDetail Where x.DevolutionQuantity > 0 Select x).Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede confirmar porque no hay cantidades a devolver"
                Exit Function
            End If
        End If
        Try
            AssigningValues()
            Using model As New MRefundPurchase(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveAndConfirmEntranceVoucherDevolution(_entranceDevolution, _idCurrentSequence, actions, Me._sequence)
                AsyncLoader(False)
                If result.StateResult = True And result.StateResultAux = True Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    _entranceDevolution = result.ObjectEmbbeded
                    If result.MessageResultAux IsNot Nothing AndAlso result.MessageResultAux.Count > 0 Then
                        ViewMessageValidationStock(result.MessageResultAux)
                    End If
                    Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _entranceDevolution.Id, 0, _entranceDevolution.Id)

                    Me.Deshacer()
                ElseIf result.StateResult = True And result.StateResultAux = False Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    _entranceDevolution = result.ObjectEmbbeded
                    Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _entranceDevolution.Id, 0, _entranceDevolution.Id)

                    Me.Deshacer()
                ElseIf result.StateResult = False And result.StateResultAux = False Then
                    If result.MessageResult IsNot Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    If _entranceDevolution.Id > 0 Then
                        Dim res = Await model.GetRefundPurchase(Code)
                        _entranceDevolution = res.ObjectEmbbeded
                    Else
                        _entranceDevolution = New EntranceVoucherDevolution With {.Status = 1}
                    End If

                    Await Me.ReloadDetails()

                    INDBtnCode.Enabled = False
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnCode.Enabled = False
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Recarga los detalles si ocurre un error
    ''' </summary>
    ''' <returns></returns>
    Private Async Function ReloadDetails() As Task
        _isLoading = True
        _isReLoading = True
        Await LoadEntranceVoucher()
        _isLoading = False
        _isReLoading = False
    End Function

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Método que anula el documento
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Eliminar() Implements ICrudBase.Eliminar
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _entranceDevolution.Status = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Propiedad que define el mensaje para mostrar al usuario
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

#End Region

#Region "Methods & Functions"

    ''' <summary>
    ''' Carga los estados de contrato
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "0", .StatusName = String.Empty, .StatusColor = System.Drawing.Color.White})
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
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
    ''' Carga el tercero de la empresa actualmente seleccionada
    ''' </summary>
    ''' <returns>Valor que indica si la empresa se cargo</returns>
    Private Async Function LoadCurrentCompany() As Task(Of Boolean)
        Try
            AsyncLoader(True)
            Using model As New MEntranceVoucher(MyTag)
                Me._currentCompany = Await model.GetThirdPartyAsync(Me.indigo.IndigoCompanyNit)
                If Me._currentCompany Is Nothing OrElse Me._currentCompany.Id = 0 Then
                    Return False
                End If
            End Using
            Return True
        Catch ex As Exception
            Me.Mensaje(EeventViewerImages.Advertencia) = ex.Message
            Return False
        Finally
            AsyncLoader(False)
        End Try
    End Function

    ''' <summary>
    ''' Método utilizado para cargar los parametros
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadParameters() As Task
        Using Model As New MEntranceVoucher(Me.MyTag)
            _settingsInventory = Await Model.GetSettingInventory(_idOperativeUnit)
            If _settingsInventory Is Nothing OrElse _settingsInventory.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", NAME_MODULE)
                Deshacer()
                Exit Function
            End If
            FreightIVAPercentage = _settingsInventory.IvaFreigthPercentage
            WithholdingTaxBase = _settingsInventory.WithholdingIvaBase
            WithholdingTaxPercentaje = _settingsInventory.WithholdingIvaPercentage
            Me.ValidateDate()
        End Using

        Using model As New Presentation.Payments.MVP.MParameters(MyTag)
            Dim budgetInterface As Boolean = False
            Dim _parameter = Await model.GetSettingPaymentsByIdOperatingUnit(_idOperativeUnit)
            If _parameter IsNot Nothing AndAlso _parameter.ObjectEmbbeded IsNot Nothing AndAlso _parameter.StateResult Then
                budgetInterface = _parameter.ObjectEmbbeded.BudgetInterface
            End If

            INDlygBudget.Visibility = If(budgetInterface, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
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
        INDDteDocumentDate.Properties.MinValue = dateMin
        INDDteDocumentDate.Properties.MaxValue = GetDateServer()
    End Sub

    ''' <summary>
    ''' Método que consulta las retenciones y las deducciones del modulo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub GetOtherRentetionsAndDeductions()
        Using Model As New MEntranceVoucher(Me.Tag)
            Dim resultOperation = Await Model.ListOtherRetentionAndDeduction()
            If resultOperation.ObjectEmbbeded.Count > 0 Then
                For Each item In resultOperation.ObjectEmbbeded
                    If item.Type = 1 Then
                        ListRetention.Add(item)
                    Else
                        ListDeduction.Add(item)
                    End If
                Next
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Ajusta los controles del comprobante de entrada para que no puedan ser modificados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadOnlyEntranceVoucherInfo()
        INDDteDateEntranceVoucher.Properties.ReadOnly = True
        INDTxtSupplier.Properties.ReadOnly = True
        INDTxtWarehouse.Properties.ReadOnly = True
        INDTxtInvoiceNumber.Properties.ReadOnly = True
        INDTxtInvoiceDate.Properties.ReadOnly = True
        INDTxtDayPeriod.Properties.ReadOnly = True
        INDTxtFreightInvoice.Properties.ReadOnly = True
        INDSpnFreightIvaPercentage.Properties.ReadOnly = True
        INDTxtFregithIVAValue.Properties.ReadOnly = True
    End Sub

    ''' <summary>
    ''' Ajusta los controles de informacion monetaria para que no puedan ser modificados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadOnlyMonetaryInfo()
        INDTxtSubTotal.Properties.ReadOnly = True
        INDTxtDiscountValue.Properties.ReadOnly = True
        INDTxtValueTax.Properties.ReadOnly = True
        INDTxtWhithholdigTaxValue.Properties.ReadOnly = True
        INDTxtWhithholdigICAValue.Properties.ReadOnly = True
        INDTxtWhithholdigSourceValue.Properties.ReadOnly = True
        INDTxtOtherRetention.Properties.ReadOnly = True
        INDTxtOtherDeduction.Properties.ReadOnly = True
        INDTxtTotalValue.Properties.ReadOnly = True
    End Sub

    ''' <summary>
    ''' Método que limpia los items seleecionados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearOtherRetentionDeductions()
        LyGroupOtherRetention.Visibility = XtraLayout.Utils.LayoutVisibility.Never
        LyGroupOtherDeduction.Visibility = XtraLayout.Utils.LayoutVisibility.Never
        ListRetentionDevolution = New List(Of EntranceVoucherDevolutionOtherDeduction)
        ListDeductionDevolution = New List(Of EntranceVoucherDevolutionOtherDeduction)

        INDGcOtherWithholding.RefreshDataSource()
        INDGcOtherDeduction.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Método que limpia los controles al seleccionar un comprobante de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlsEntranceVoucher()
        If _isReLoading = False Then
            If _isLoading = False Then
                _entranceDevolution = New EntranceVoucherDevolution With {.Status = 1}
            End If

            DevolutionType = True
            INDDteDateEntranceVoucher.EditValue = Nothing
            INDTxtSupplier.Text = String.Empty
            INDTxtWarehouse.Text = String.Empty
            INDTxtInvoiceNumber.Text = String.Empty
            INDTxtInvoiceDate.Text = String.Empty
            INDTxtDayPeriod.Text = String.Empty
            INDTxtFreightInvoice.EditValue = 0
            INDTxtFreightValue.EditValue = 0
            INDTxtFregithIVAValue.EditValue = 0
            INDSpnFreightIvaPercentage.EditValue = 0

            ListEntranceVoucherDetail = New List(Of EntranceVoucherDetailBatchSerial)
            ClearOtherRetentionDeductions()
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles y las variables
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub CleanControls()
        LyRefundPurchase.BeginUpdate()
        Await DeleteBlockedRecord()

        'Datos Principales
        Code = String.Empty
        DocumentDate = Me.GetDateServer()
        WarehouseId = Nothing
        INDSleWarehouse.Properties.NullText = String.Empty
        Description = Nothing
        Status = 0
        'Información Comprobante de entrada
        EntranceVoucherId = Nothing
        INDSleVoucherCode.Properties.NullText = String.Empty
        RoundingValue = 1
        DevolutionType = Nothing
        'Flete
        FreightIVAPercentage = 0
        FreightIVAValue = 0
        FreightValue = 0
        'Información Factura
        Value = 0
        ValueDiscount = 0
        ValueTax = 0
        WithholdingIcaPercentage = 0
        WithholdingTax = 0
        WithholdingICA = 0
        RetentionSource = 0
        RetentionOther = 0
        DeductionOther = 0
        DistrictTax = 0
        TotalValue = 0
        NetoValue = 0

        _ctrTmp.PrintInfo()
        INDGcProducts.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDGcProducts)
        INDgcObligation.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDgcObligation)

        _doc = Nothing
        _isLoading = False
        _isReLoading = False
        _entranceVoucher = Nothing
        _entranceDevolution = Nothing
        _listEntranceVoucherDevolutionObligationBudget = Nothing
        _listDeleteEntranceVoucherDevolutionObligationBudget = Nothing

        Me.ValidateDate()
        ShowWithholdingICA()
        ReadOnlyControls(False)
        ReadOnlyMonetaryInfo()
        ReadOnlyEntranceVoucherInfo()
        RepoSpeCantDevolutionProduct.ReadOnly = False
        ActionsOnControls = False
        INDBtnCode.Focus()


        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True

        LyRefundPurchase.EndUpdate()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._entranceDevolution.Code, Me._entranceDevolution.DocumentDate),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me._entranceDevolution.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._entranceDevolution.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._entranceDevolution.Code, Me._entranceDevolution.DocumentDate)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._entranceDevolution.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Método que elimina el registro guardado para concurrencia
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function DeleteBlockedRecord() As Task
        If _blockRecord IsNot Nothing AndAlso _blockRecord.Id > 0 AndAlso _blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(_blockRecord)
                _blockRecord = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Genera una nueva entidad de comprobante de entrada
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function NewRefundPurchase() As Task
        If Me._currentCompany Is Nothing OrElse Me._currentCompany.Id = 0 Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "No se ha podido cargar el tercero de la empresa (" & Me.indigo.IndigoCompanyNit & ") actualmente seleccionada. Verifique que haya sido creada e intente de nuevo."
            Exit Function
        End If
        If Me._settingsInventory Is Nothing OrElse Me._settingsInventory.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", NAME_MODULE)
            Exit Function
        End If

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

        BarraBotones.StatusRecord = "1"
        BarraBotones.StatusRecordVisible = True
        _entranceDevolution = New EntranceVoucherDevolution With {.Status = 1}
    End Function

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
                Using Model As New MRefundPurchase(CStr(Me.Tag))
                    AsyncLoader(True)
                    LyRefundPurchase.BeginUpdate()
                    Dim result = Await Model.GetRefundPurchase(INDBtnCode.Text.Trim)
                    If result.StateResult Then
                        _entranceDevolution = result.ObjectEmbbeded
                        If _entranceDevolution IsNot Nothing AndAlso _entranceDevolution.Id > 0 Then
                            Me.BarraBotones.StatusRecordVisible = True
                            Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                                _blockRecord = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_entranceDevolution.Id))
                                With _entranceDevolution
                                    LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                                    If .Status <> 1 Then
                                        INDDteDocumentDate.Properties.MinValue = .DocumentDate
                                    End If

                                    _isLoading = True

                                    BarraBotones.OperatingUnitValue = .OperatingUnitId
                                    Code = .Code
                                    DocumentDate = .DocumentDate
                                    WarehouseId = .WarehouseId
                                    INDSleWarehouse.Properties.NullText = .DescriptionWarehouse
                                    Description = .Description
                                    EntranceVoucherId = .EntranceVoucherId
                                    INDSleVoucherCode.Properties.NullText = .CodeEntranceVoucher
                                    INDSleVoucherCode.Properties.ReadOnly = True
                                    Me.Status = .Status

                                    Await LoadEntranceVoucher()

                                    _listEntranceVoucherDevolutionObligationBudget = .EntranceVoucherDevolutionObligationBudget.ToList()
                                    INDgcObligation.DataSource = Nothing
                                    INDgcObligation.DataSource = _listEntranceVoucherDevolutionObligationBudget

                                    _isLoading = False
                                End With
                                Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._entranceDevolution.Code)
                                If _blockRecord.Id = 0 Then
                                    _blockRecord = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = _entranceDevolution.Id})
                                    ).ObjectEmbbeded
                                Else
                                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _blockRecord.CodUser, _blockRecord.NameUser, _blockRecord.BlockDate)
                                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _blockRecord.CodUser)
                                End If

                                If _entranceDevolution.Status = 1 Then
                                    INDSleVoucherCode.Properties.ReadOnly = True
                                    INDDteDateEntranceVoucher.Properties.ReadOnly = True
                                    INDTxtSupplier.Properties.ReadOnly = True
                                    INDTxtWarehouse.Properties.ReadOnly = True
                                    INDTxtInvoiceNumber.Properties.ReadOnly = True
                                    INDTxtInvoiceDate.Properties.ReadOnly = True
                                    INDTxtDayPeriod.Properties.ReadOnly = True
                                    INDTxtFreightInvoice.Properties.ReadOnly = True
                                    INDSpnFreightIvaPercentage.Properties.ReadOnly = True
                                    INDTxtFregithIVAValue.Properties.ReadOnly = True

                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                                Else
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                    Me.ReadOnlyControls(True)
                                    RepoSpeCantDevolutionProduct.ReadOnly = True
                                End If

                                INDDteDocumentDate.Focus()
                                Me.BarraBotones.SetDocuments(_entranceDevolution.Id, Me.Tag.ToString(), Nothing, GetType(EntranceVoucherDevolution).Name)
                                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                                Me.BarraBotones.PrintReport(PrintReportAction.None, _entranceDevolution.Id, 0, _entranceDevolution.Id)


                                ActionsOnControls = True
                                AsyncLoader(False)
                            End Using
                        Else
                            AsyncLoader(False)
                            If Me._sequence.IsManual Then
                                Await Me.NewRefundPurchase()
                            Else
                                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                                Code = String.Empty
                                Deshacer()
                                INDBtnCode.Focus()
                            End If
                        End If
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = result.Message
                        Code = String.Empty
                        INDBtnCode.Focus()
                    End If

                    LyRefundPurchase.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
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
		_culture.NumberFormat.CurrencyDecimalDigits = MaskDigitRounding
		Me.changeNumericFormatByCurrency(_culture.NumberFormat)
		Me._ctrTmp.CurrencyNumbertFormat = _culture.NumberFormat
		Me._ctrTmp.CodeISO4217 = _currencyAbbreviation
        _ctrTmp.PrintInfo()
    End Sub

    ''' <summary>
    ''' Método con el que se realiza la carga del comprobante de entrada que se va a devolver
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function LoadEntranceVoucher() As Task
        ClearControlsEntranceVoucher()

        If Not _isReLoading Then
            Using Model As New MEntranceVoucher(MyTag)
                _entranceVoucher = Await Model.GetEntranceVoucherById(EntranceVoucherId)
                Using modelSupplier As New MSupplier(MyTag)
                    _supplier = modelSupplier.GetSupplierById(_entranceVoucher.SupplierId)
                End Using

                ''Se obtiene la moneda del comprobante de entrada
                ''Revisar codigo
                Using modelCurrency As New MCurrency(MyTag)
                    _currency = Await modelCurrency.GetCurrencyById(_entranceVoucher.CurrencyId)
                End Using

                SetCurrencyUI(_currency.Abbreviation)

                'Detalle del comprobante de entrada
                ListEntranceVoucherDetail = Model.GetDetailEntranceVoucherWithBatchSerialByIdEntranceVoucher(_entranceVoucher.Id)

                'Devoluciones previamente realizadas
                Me._listEntranceVoucherDevolutionByEntranceVoucher = _presenter.ListEntranceVoucherDevolutionByEntranceVoucherId(EntranceVoucherId)
            End Using
        End If

        With _entranceVoucher
            'Cabecera del comprobante
            INDDteDateEntranceVoucher.EditValue = .DocumentDate
            INDTxtSupplier.Text = .DescriptionSupplier
            INDTxtWarehouse.Text = .DescriptionWarehouse
            INDTxtInvoiceNumber.Text = .InvoiceNumber
            INDTxtInvoiceDate.EditValue = .InvoiceDate
            INDTxtDayPeriod.Text = .DayPeriod
            INDTxtFreightInvoice.EditValue = .FreightValue
            RoundingValue = .RoundService

            WithholdingIcaPercentage = If((_isLoading AndAlso _entranceDevolution.Status <> 1), _entranceDevolution.WithholdingIcaPercentage, .IcaPercentage)
            FreightValue = If((_isLoading AndAlso _entranceDevolution.Status <> 1), _entranceDevolution.FreightValue, .FreightValueOutstanding)
            INDSpnFreightIvaPercentage.EditValue = If((_isLoading AndAlso _entranceDevolution.Status <> 1), _entranceDevolution.FreightIVAPercentage, .FreightIVAPercentage)
            FreightIVAValue = If((_isLoading AndAlso _entranceDevolution.Status <> 1), _entranceDevolution.FreightIVAValue, .FreightIVAValueOutstanding)

            If ListEntranceVoucherDetail IsNot Nothing Then
                For Each evdbs In ListEntranceVoucherDetail
                    Dim detail As New EntranceVoucherDevolutionDetail
                    If _isLoading Then
                        If _entranceDevolution.EntranceVoucherDevolutionDetail Is Nothing Then
                            _entranceDevolution.EntranceVoucherDevolutionDetail.Add(detail)

                            detail.EntranceVoucherDetailBatchSerialId = evdbs.Id
                            detail.Quantity = If(_isReLoading, evdbs.DevolutionQuantity, 0)
                            detail.DevolutionCauseId = evdbs.DevolutionCauseId
                        Else
                            detail = _entranceDevolution.EntranceVoucherDevolutionDetail.Where(Function(d) d.EntranceVoucherDetailBatchSerialId = evdbs.Id).FirstOrDefault()
                            If detail Is Nothing Then
                                detail = New EntranceVoucherDevolutionDetail
                                _entranceDevolution.EntranceVoucherDevolutionDetail.Add(detail)

                                detail.EntranceVoucherDetailBatchSerialId = evdbs.Id
                                detail.Quantity = If(_isReLoading, evdbs.DevolutionQuantity, 0)
                                detail.DevolutionCauseId = evdbs.DevolutionCauseId
                            Else
                                detail.Quantity = If(_isReLoading, evdbs.DevolutionQuantity, detail.Quantity)
                                detail.DevolutionCauseId = If(_isReLoading, evdbs.DevolutionCauseId, detail.DevolutionCauseId)
                                evdbs.DevolutionCauseId = detail.DevolutionCauseId
                            End If
                        End If
                    Else
                        _entranceDevolution.EntranceVoucherDevolutionDetail.Add(detail)

                        detail.EntranceVoucherDetailBatchSerialId = evdbs.Id
                        detail.Quantity = If(_isReLoading, evdbs.DevolutionQuantity, evdbs.OutstandingQuantity)
                    End If

                    evdbs.DevolutionQuantity = detail.Quantity
                    detail.BatchSerialId = evdbs.BatchSerialId
                    detail.EntranceVoucherDetailId = evdbs.EntranceVoucherDetailId
                    detail.InitialQuantity = evdbs.Quantity
                    detail.OutstandingQuantity = evdbs.OutstandingQuantity
                    detail.UnitValueProduct = evdbs.UnitValueProduct
                    detail.SubTotalValueProduct = evdbs.SubTotalValueProduct
                    detail.DiscountPercentageProduct = evdbs.DiscountPercentageProduct
                    detail.DiscountValueProduct = evdbs.DiscountValueProduct
                    detail.IvaPercentageProduct = evdbs.IvaPercentageProduct
                    detail.IvaValueProduct = evdbs.IvaValueProduct
                    detail.RtfPercentageProduct = evdbs.RtfPercentageProduct
                    detail.RtfValueProduct = evdbs.RtfValueProduct

                    If detail.OutstandingQuantity > 0 AndAlso detail.OutstandingQuantity <> detail.Quantity Then
                        DevolutionType = False
                    End If
                Next
            End If

            If _isLoading Then
                For Each item In _entranceDevolution.EntranceVoucherDevolutionOtherDeduction
                    AddListEntranceVoucherOtherDeduction(item)
                Next
            Else
                For Each item In .EntranceVoucherOtherDeduction
                    Dim dod = New EntranceVoucherDevolutionOtherDeduction
                    dod.EntranceVoucherOtherDeductionId = item.Id
                    dod.OtherWithholdingDeductionId = item.OtherWithholdingDeductionId
                    dod.Type = item.Type
                    dod.ValueBase = NetoValue
                    dod.Value = item.ValueOutstanding
                    dod.DevolutionValue = item.ValueOutstanding
                    AddListEntranceVoucherOtherDeduction(dod)
                    _entranceDevolution.EntranceVoucherDevolutionOtherDeduction.Add(dod)
                Next
            End If

            INDGcOtherWithholding.DataSource = ListRetentionDevolution
            INDGcOtherWithholding.RefreshDataSource()
            INDGcOtherDeduction.DataSource = ListDeductionDevolution
            INDGcOtherDeduction.RefreshDataSource()

            LyGroupFreight.HideControl(DevolutionType = False OrElse ListDeductionDevolution.Count = 0)
            LyGroupProducts.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            LyGroupOtherDeduction.HideControl(DevolutionType = False OrElse ListDeductionDevolution.Count = 0)
            LyGroupOtherRetention.HideControl(DevolutionType = False OrElse ListRetentionDevolution.Count = 0)

            RefreshTotals()
            If Not _isLoading Then
                INDviewObligation.ShowLoadingPanel()
                Await Task.Factory.StartNew(Sub() LoadObligationsOfCommitmentsEntranceVoucher())
            End If
        End With
    End Function

    Private Async Sub AddListEntranceVoucherOtherDeduction(item As EntranceVoucherDevolutionOtherDeduction)
        If item IsNot Nothing Then
            If item.Type = 1 Then
                If ListRetention IsNot Nothing Then
                    item.ConceptName = ListRetention.Where(Function(x) x.Id = item.OtherWithholdingDeductionId).FirstOrDefault.CodeNameConcepts
                    item.RetentionPercentage = ListRetention.Where(Function(x) x.Id = item.OtherWithholdingDeductionId).FirstOrDefault.RetentionPercentage
                    ListRetentionDevolution.Add(item)
                End If
            Else
                If ListDeduction IsNot Nothing Then
                    item.ConceptName = ListDeduction.Where(Function(x) x.Id = item.OtherWithholdingDeductionId).FirstOrDefault.CodeNameConcepts
                    ListDeductionDevolution.Add(item)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método utilziado para cargar los valores de los totales
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RefreshTotals()
        If _entranceDevolution IsNot Nothing Then
            _entranceDevolution.WithholdingTaxPercentaje = WithholdingTaxPercentaje

            If _supplier.ThirdParty IsNot Nothing Then
                If _entranceDevolution.Status = 1 Then
                    Me.CalculateTotals(_entranceDevolution, _currentCompany.ContributionType, _supplier.ThirdParty.ContributionType)
                End If
            End If

            With _entranceDevolution
                Value = .Value
                ValueDiscount = .ValueDiscount
                NetoValue = .Value
                ValueTax = .ValueTax
                RetentionSource = .RetentionSource
                WithholdingTax = .WithholdingTax
                WithholdingICA = .WithholdingICA
                DistrictTax = .DistrictTax
                RetentionOther = .RetentionOther
                DeductionOther = .DeductionOther
                TotalValue = .TotalValue
            End With

            _ctrTmp.PrintInfo()
        End If
        ShowWithholdingICA()
    End Sub

    Public Sub CalculateTotals(entity As EntranceVoucherDevolution, CurrentCompanyContributionType As Byte, SupplierContributionType As Byte)
        entity.Value = 0
        entity.ValueDiscount = 0
        entity.ValueTax = 0
        entity.RetentionSource = 0
        entity.WithholdingTax = 0
        entity.WithholdingICA = 0
        entity.RetentionOther = 0
        entity.DeductionOther = 0
        entity.TotalValue = 0

        If entity.EntranceVoucherDevolutionDetail.Count > 0 Then
            'Variables de los valores previamente devueltos
            Dim SubTotalPreviousValue = 0D
            Dim DiscountPreviousValue = 0D
            Dim IvaPreviousValue = 0D
            Dim RTFPreviousValue = 0D
            Dim WithholdingTaxPreviousValue = 0D
            Dim WithholdingICAPreviousValue = 0D

            'Se indica si es una devolución total o parcial
            DevolutionType = If(entity.EntranceVoucherDevolutionDetail.Any(Function(d) d.Quantity < d.OutstandingQuantity), False, If(entity.EntranceVoucherDevolutionDetail.Any(Function(d) d.Quantity > 0), True, False))

            'Actualizamos los valores de los detalles
            For Each detail In entity.EntranceVoucherDevolutionDetail
                detail.SubTotalValue = Utils.RoundValue(detail.Quantity * detail.UnitValueProduct, 6)
                detail.DiscountValue = Utils.RoundValue(detail.Quantity * detail.UnitValueProduct * detail.DiscountPercentageProduct / 100, 6)
                detail.IvaValue = Utils.RoundValue(detail.Quantity * detail.UnitValueProduct * (100 - detail.DiscountPercentageProduct) / 100 * detail.IvaPercentageProduct / 100, 6)
                detail.RtfValue = Utils.RoundValue(detail.Quantity * detail.UnitValueProduct * (100 - detail.DiscountPercentageProduct) / 100 * detail.RtfPercentageProduct / 100, 6)
            Next

            'Si se estan devolviendo todas las cantidades de un detalle del comprobante de entrada
            For Each groupDetail In entity.EntranceVoucherDevolutionDetail.
                                      GroupBy(Function(d) d.EntranceVoucherDetailId).
                                      Select(Function(g) New With
                                      {
                                          Key .EntranceVoucherDetailId = g.Key,
                                          Key .OutstandingQuantity = g.Sum(Function(d) d.OutstandingQuantity),
                                          Key .Quantity = g.Sum(Function(d) d.Quantity),
                                          Key .SubTotalValue = g.Sum(Function(d) d.SubTotalValue),
                                          Key .DiscountValue = g.Sum(Function(d) d.DiscountValue),
                                          Key .IvaValue = g.Sum(Function(d) d.IvaValue),
                                          Key .RtfValue = g.Sum(Function(d) d.RtfValue)
                                      })

                If groupDetail.Quantity > 0 Then
                    'Ajustamos el primero de los detalles
                    Dim detail = entity.EntranceVoucherDevolutionDetail.Where(Function(d) d.EntranceVoucherDetailId = groupDetail.EntranceVoucherDetailId AndAlso d.Quantity > 0).FirstOrDefault()

                    'Valores de devoluciones anteriores asociadas al detalle
                    If Me._listEntranceVoucherDevolutionByEntranceVoucher IsNot Nothing AndAlso Me._listEntranceVoucherDevolutionByEntranceVoucher.Any() Then
                        For Each previousDevolution In Me._listEntranceVoucherDevolutionByEntranceVoucher.Where(Function(p) p.EntranceVoucherDetailId = groupDetail.EntranceVoucherDetailId)
                            SubTotalPreviousValue += previousDevolution.SubTotalValue
                            DiscountPreviousValue += previousDevolution.DiscountValue
                            IvaPreviousValue += previousDevolution.IvaValue
                            RTFPreviousValue += previousDevolution.RTFValue
                        Next

                        'Ajuste a fin de que no se supere los valores iniciales del comprobante
                        If SubTotalPreviousValue > detail.SubTotalValueProduct Then
                            SubTotalPreviousValue = detail.SubTotalValueProduct
                        End If
                        If DiscountPreviousValue > detail.DiscountValueProduct Then
                            DiscountPreviousValue = detail.DiscountValueProduct
                        End If
                        If IvaPreviousValue > detail.IvaValueProduct Then
                            IvaPreviousValue = detail.IvaValueProduct
                        End If
                        If RTFPreviousValue > detail.RtfValueProduct Then
                            RTFPreviousValue = detail.RtfValueProduct
                        End If
                    End If

                    'Ajuste a fin de que no se supere o pierdan los valores iniciales del comprobante
                    If (groupDetail.Quantity = groupDetail.OutstandingQuantity) OrElse ((SubTotalPreviousValue + groupDetail.SubTotalValue) > detail.SubTotalValueProduct) Then
                        detail.SubTotalValue += detail.SubTotalValueProduct - (SubTotalPreviousValue + groupDetail.SubTotalValue)
                    End If
                    If (groupDetail.Quantity = groupDetail.OutstandingQuantity) OrElse ((DiscountPreviousValue + groupDetail.DiscountValue) > detail.DiscountValueProduct) Then
                        detail.DiscountValue += detail.DiscountValueProduct - (DiscountPreviousValue + groupDetail.DiscountValue)
                    End If
                    If (groupDetail.Quantity = groupDetail.OutstandingQuantity) OrElse ((IvaPreviousValue + groupDetail.IvaValue) > detail.IvaValueProduct) Then
                        detail.IvaValue += detail.IvaValueProduct - (IvaPreviousValue + groupDetail.IvaValue)
                    End If
                    If (groupDetail.Quantity = groupDetail.OutstandingQuantity) OrElse ((RTFPreviousValue + groupDetail.RtfValue) > detail.RtfValueProduct) Then
                        detail.RtfValue += detail.RtfValueProduct - (RTFPreviousValue + groupDetail.RtfValue)
                    End If
                End If
            Next

            entity.Value = entity.EntranceVoucherDevolutionDetail.Sum(Function(x) x.SubTotalValue)
            entity.ValueDiscount = entity.EntranceVoucherDevolutionDetail.Sum(Function(x) x.DiscountValue)
            entity.ValueTax = entity.EntranceVoucherDevolutionDetail.Sum(Function(x) x.IvaValue)

            'Retenciones en devoluciones anteriores
            If Me._listEntranceVoucherDevolutionByEntranceVoucher IsNot Nothing AndAlso Me._listEntranceVoucherDevolutionByEntranceVoucher.Any() Then
                For Each previousDevolution In Me._listEntranceVoucherDevolutionByEntranceVoucher
                    WithholdingTaxPreviousValue += Utils.RoundValue(previousDevolution.IvaValue * entity.WithholdingTaxPercentaje / 100, 6)
                    WithholdingICAPreviousValue += Utils.RoundValue((previousDevolution.SubTotalValue - previousDevolution.DiscountValue) * entity.WithholdingIcaPercentage / 100, 6)
                Next

                'Ajuste a fin de que no se supere los valores iniciales del comprobante
                If WithholdingTaxPreviousValue > _entranceVoucher.WithholdingTax Then
                    WithholdingTaxPreviousValue = _entranceVoucher.WithholdingTax
                End If
                If WithholdingICAPreviousValue > _entranceVoucher.WithholdingICA Then
                    WithholdingICAPreviousValue = _entranceVoucher.WithholdingICA
                End If
            End If

            'Si el comprobante de entrada tiene retenciones le calculo las retenciones y se las ajusto
            If _entranceVoucher.RetentionSource > 0 Then
                entity.RetentionSource = entity.EntranceVoucherDevolutionDetail.Sum(Function(x) x.RtfValue)
            End If
            If _entranceVoucher.WithholdingTax > 0 Then
                Dim WithholdingTax = Utils.RoundValue(entity.ValueTax * entity.WithholdingTaxPercentaje / 100, 6)
                'Ajuste a fin de que no se supere o pierdan los valores iniciales del comprobante
                If DevolutionType OrElse ((WithholdingTaxPreviousValue + WithholdingTax) > _entranceVoucher.WithholdingTax) Then
                    WithholdingTax = (_entranceVoucher.WithholdingTax - WithholdingTaxPreviousValue)
                End If

                entity.WithholdingTax = WithholdingTax
            End If
            If _entranceVoucher.WithholdingICA > 0 Then
                Dim WithholdingICA = Utils.RoundValue((entity.Value - entity.ValueDiscount) * entity.WithholdingIcaPercentage / 100, 6)
                'Ajuste a fin de que no se supere o pierdan los valores iniciales del comprobante
                If DevolutionType OrElse ((WithholdingICAPreviousValue + WithholdingICA) > _entranceVoucher.WithholdingICA) Then
                    WithholdingICA = (_entranceVoucher.WithholdingICA - WithholdingICAPreviousValue)
                End If
                entity.WithholdingICA = WithholdingICA
            End If
        End If

        entity.TotalValue = entity.Value - entity.ValueDiscount + entity.ValueTax - entity.WithholdingTax - entity.WithholdingICA - entity.RetentionSource - entity.DistrictTax

        If DevolutionType Then
            If entity.EntranceVoucherDevolutionOtherDeduction.Count > 0 Then
                For Each i In entity.EntranceVoucherDevolutionOtherDeduction
                    If i.Type = 1 Then
                        i.DevolutionValue = i.Value
                    End If
                Next
                entity.RetentionOther = entity.EntranceVoucherDevolutionOtherDeduction.Sum(Function(x) IIf(x.Type = 1, x.DevolutionValue, 0))
                entity.DeductionOther = entity.EntranceVoucherDevolutionOtherDeduction.Sum(Function(x) IIf(x.Type = 2, x.DevolutionValue, 0))
            End If

            entity.TotalValue = entity.TotalValue - entity.RetentionOther - entity.DeductionOther + entity.FreightIVAValue + entity.FreightValue
        End If
    End Sub

    ''' <summary>
    ''' Método que carga las obligaciones asociadas al compromiso generado por el comprobante de entrada
    ''' </summary>
    Private Sub LoadObligationsOfCommitmentsEntranceVoucher()
        CheckForIllegalCrossThreadCalls = False

        If _listEntranceVoucherDevolutionObligationBudget IsNot Nothing AndAlso _listEntranceVoucherDevolutionObligationBudget.Count > 0 Then
            While _listEntranceVoucherDevolutionObligationBudget.Count > 0
                If _listEntranceVoucherDevolutionObligationBudget(0).Id > 0 Then
                    If _listDeleteEntranceVoucherDevolutionObligationBudget Is Nothing Then
                        _listDeleteEntranceVoucherDevolutionObligationBudget = New List(Of EntranceVoucherDevolutionObligationBudget)
                    End If
                    _listEntranceVoucherDevolutionObligationBudget(0).MarkAsDeleted()
                    _listDeleteEntranceVoucherDevolutionObligationBudget.Add(_listEntranceVoucherDevolutionObligationBudget(0))
                End If

                _listEntranceVoucherDevolutionObligationBudget.Remove(_listEntranceVoucherDevolutionObligationBudget(0))
            End While
        Else
            _listEntranceVoucherDevolutionObligationBudget = New List(Of EntranceVoucherDevolutionObligationBudget)
        End If

        Dim listXpo = _presenter.ListObligationsOfEntranceVoucher(EntranceVoucherId)
        If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
            For Each itemXpo In listXpo
                Dim entity As New EntranceVoucherDevolutionObligationBudget
                With entity
                    .ObligationDetailId = itemXpo.ObligationDetailId
                    .Value = 0
                    .ObligationCode = itemXpo.ObligationCode
                    .ObligationDocument = itemXpo.ObligationDocument
                    .CategoryName = itemXpo.CategoryDescription
                    .FinancialSourceDescription = itemXpo.FinancialSourceDescription
                    .RevenueTypeDescription = itemXpo.RevenueTypeDescription
                    .ObligationBalance = itemXpo.Balance
                    .CommitmentDetailId = itemXpo.CommitmentDetailId
                End With
                _listEntranceVoucherDevolutionObligationBudget.Add(entity)
            Next

            INDgcObligation.DataSource = Nothing
            INDgcObligation.DataSource = _listEntranceVoucherDevolutionObligationBudget
            INDgcObligation.RefreshDataSource()
        End If

        INDviewObligation.HideLoadingPanel()
    End Sub

    ''' <summary>
    ''' valida los controles del formulario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControls() As String
        Dim errors As New StringBuilder

        If Code Is Nothing Then
            errors.AppendLine(INDBtnCode.Text + ResourceManager.GetString("Empty"))
        End If

        If DocumentDate Is Nothing Then
            errors.AppendLine(INDLyDteDocumentDate.Text + ResourceManager.GetString("Empty"))
        End If

        If WarehouseId Is Nothing Then
            errors.AppendLine(INDLySleWarehouse.Text + ResourceManager.GetString("Empty"))
        End If

        If Description Is Nothing OrElse Description = String.Empty Then
            errors.AppendLine(INDLyTxtDescription.Text + ResourceManager.GetString("Empty"))
        End If

        If EntranceVoucherId Is Nothing Then
            errors.AppendLine(INDLySleVoucherCode.Text + ResourceManager.GetString("Empty"))
        End If

        If WarehouseId IsNot Nothing AndAlso _entranceVoucher IsNot Nothing AndAlso WarehouseId <> _entranceVoucher.WarehouseId Then
            errors.AppendLine("El almacen de la devolución no corresponde con el almacén de la compra.")
        End If

        If FreightValue > INDTxtFreightInvoice.EditValue Then
            errors.AppendLine(ResourceManager.GetString("ValueGreaterFreight_RefundPurchase", NAME_MODULE))
        End If

        If INDGvProducts.RowCount = 0 Then
            errors.AppendLine(ResourceManager.GetString("AddEntranceVoucherDetail", NAME_MODULE))
        End If

        If TotalValue = 0 Then
            errors.AppendLine(ResourceManager.GetString("TotalValueZero_RefundPurchase", NAME_MODULE))
        End If

        If _entranceVoucher IsNot Nothing Then
            If TotalValue > _entranceVoucher.TotalValue Then
                errors.AppendLine("El valor de la devolución no puede ser mayor que el valor del comprobante")
            End If
        End If

        Return errors.ToString()
    End Function

    '''' <summary>
    '''' Metodo que muestra/oculta el control de %Retencion.ICA
    '''' </summary>
    Private Sub ShowWithholdingICA()
        If Me.indigo.Culture.Name <> "es-CO" Then
            INDLyTxtWhithholdigICAValue.HideLayout()
            WithholdingICA = 0
        Else
            INDLyTxtWhithholdigICAValue.ShowLayout()
        End If
    End Sub

    ''' <summary>
    ''' Método que asigna los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With _entranceDevolution
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .OperatingUnitId = Me.BarraBotones.OperatingUnitValue
            .Prefix = Me._prefixSelected
            .DocumentDate = DocumentDate
            .Description = Description
            .WarehouseId = WarehouseId
            .EntranceVoucherId = EntranceVoucherId

            .FreightValue = FreightValue
            .FreightIVAPercentage = FreightIVAPercentage
            .FreightIVAValue = FreightIVAValue

            For Each detailDR In .EntranceVoucherDevolutionOtherDeduction
                If detailDR.Type = 1 Then
                    Dim retention = ListRetentionDevolution.Where(Function(x) x.OtherWithholdingDeductionId = detailDR.OtherWithholdingDeductionId).FirstOrDefault
                    If retention IsNot Nothing Then
                        detailDR.Value = retention.DevolutionValue
                        detailDR.RetentionPercentage = retention.RetentionPercentage
                        detailDR.ConceptName = retention.ConceptName
                    Else
                        Exit For
                    End If
                Else
                    Dim retention = ListDeductionDevolution.Where(Function(x) x.OtherWithholdingDeductionId = detailDR.OtherWithholdingDeductionId).FirstOrDefault
                    If retention IsNot Nothing Then
                        detailDR.ConceptName = retention.ConceptName
                        detailDR.DevolutionValue = retention.Value
                        detailDR.Value = retention.Value
                        detailDR.ValueBase = NetoValue
                    Else
                        Exit For
                    End If
                End If
            Next

            If _listEntranceVoucherDevolutionObligationBudget IsNot Nothing AndAlso _listEntranceVoucherDevolutionObligationBudget.Count > 0 Then
                _listEntranceVoucherDevolutionObligationBudget.ForEach(Sub(item) .EntranceVoucherDevolutionObligationBudget.Add(item))
            End If

            If _listDeleteEntranceVoucherDevolutionObligationBudget IsNot Nothing AndAlso _listDeleteEntranceVoucherDevolutionObligationBudget.Count > 0 Then
                _listDeleteEntranceVoucherDevolutionObligationBudget.ForEach(Sub(item) .EntranceVoucherDevolutionObligationBudget.Add(item))
            End If
        End With
    End Sub

    ''' <summary>
    ''' Metodo para mostar un pop up con los mensajes de validacion por stock
    ''' </summary>
    ''' <param name="messagesValidationStock"></param>
    ''' <remarks></remarks>
    Private Sub ViewMessageValidationStock(messagesValidationStock As List(Of String))
        Using formulario As New FrmPopUpValidateStock
            Me.Cursor = ChangeCursorIndigo()
            formulario.Size = New Size(800, 730)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Datasource = messagesValidationStock
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

#End Region

#Region "Handless"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara cuando incia el form, carga los controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmRefundPurchase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.LyRefundPurchase, True)

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me._funct = AddressOf GenerateDoc
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue

        _presenter = New PRefundPurchase(Me)
        _presenter.GetSequense()
        _presenter.LoadDevolutionCauses()
        Await Me.LoadCurrentCompany()
        Await Me.LoadParameters()

        ListRetention = New List(Of OtherWithholdingDeduction)
        ListDeduction = New List(Of OtherWithholdingDeduction)
        LoadStatus()
        Deshacer()
        GetOtherRentetionsAndDeductions()

        ' Personalizacion de la rejilla, muestra las columnas ocultas como mas infomacion
        IndigoGridView1.MoreInfoColunmns(INDGvProducts)
        IndigoGridControl1.RefreshGrid(INDGcProducts)
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _ctrTmp = Nothing
        _presenter = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequence = Nothing
        _blockRecord = Nothing
        _prefixSelected = Nothing
        _currentCompany = Nothing
        _settingsInventory = Nothing
        _supplier = Nothing
        _entranceDevolution = Nothing
        _listEntranceVoucherDevolutionObligationBudget = Nothing
        _listDeleteEntranceVoucherDevolutionObligationBudget = Nothing
        _entranceVoucher = Nothing
        _isLoading = Nothing
        _isReLoading = Nothing
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evetno que se dispara cuando el formulario se activa, direge el foco al control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmRefundPurchase_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBtnCode.Enabled Then
            INDBtnCode.Focus()
        End If
    End Sub

#End Region

#Region "FromClosing"

    ''' <summary>
    ''' Evento que se dispara cuando el formulario se va a cerrar, elimina el registro bloqueado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmRefundPurchase_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "IdEntityLoaded"


    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._entranceDevolution IsNot Nothing AndAlso Me._entranceDevolution.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedRecord()
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
                    Await Me.NewRefundPurchase()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Realiza la consulta del combo de almacen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleWarehouse_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleWarehouse.QueryPopUp
        If INDSleWarehouse.Properties.DataSource Is Nothing Then
            _presenter.LoadWarehouse()
        End If
    End Sub

    ''' <summary>
    ''' Realiza la consulta del combo de comprobante de entrada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleVoucherCode_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleVoucherCode.QueryPopUp
        If ListEntranceVoucher Is Nothing Then
            If WarehouseId Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un almacen"
                Exit Sub
            End If

            _presenter.LoadEntranceVoucherByStatusAndWarehouse(WarehouseId)
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDSleWarehouse_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleWarehouse.EditValueChanged
        If _isLoading Then
            Exit Sub
        End If

        EntranceVoucherId = Nothing
        ClearControlsEntranceVoucher()
        ListEntranceVoucher = Nothing

        If Me.INDSleWarehouse.EditValue IsNot Nothing Then
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("O") Then
                Dim store = If(INDGdvWarehouse.DataSource IsNot Nothing, DirectCast(DirectCast(INDGdvWarehouse.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.InventoryRepository.WarehouseXpo), Nothing)
                Me._idCurrentSequence = Me.GetIdSequenceByPrefix(If(store IsNot Nothing, store.Prefix, Me._entranceDevolution.Prefix))
                Me._prefixSelected = If(store IsNot Nothing, store.Prefix, Me._entranceDevolution.Prefix)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando selecciona un comprobante de entrada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSleVoucherCode_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleVoucherCode.EditValueChanged
        If INDSleVoucherCode.EditValue IsNot Nothing Then
            If _isLoading = False Then
                Await LoadEntranceVoucher()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Aplica el recalculo del flete y el total del producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDTxtFreightValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtFreightValue.EditValueChanged
        If _isLoading = True Then
            Exit Sub
        End If

        FreightValue = CDec(INDTxtFreightValue.EditValue)
        FreightIVAValue = Utils.RoundValue(INDTxtFreightValue.EditValue * FreightIVAPercentage / 100, RoundingValue)
        RefreshTotals()
    End Sub

    ''' <summary>
    ''' Aplica el recalculo del total deacuerdo a la cantidad de productos devueltos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepoSpeCantDevolutionProduct_EditValueChanged(sender As Object, e As EventArgs) Handles RepoSpeCantDevolutionProduct.EditValueChanged
        Dim spin = CType(sender, DevExpress.XtraEditors.SpinEdit)
        Dim evdbs As EntranceVoucherDetailBatchSerial = INDGvProducts.GetFocusedRow()
        If evdbs IsNot Nothing Then
            If evdbs.OutstandingQuantity < spin.EditValue Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DevolutionQuantityGreater_RefundPurchase", NAME_MODULE)
                spin.EditValue = evdbs.OutstandingQuantity
            End If

            Dim detail = _entranceDevolution.EntranceVoucherDevolutionDetail.Where(Function(d) d.EntranceVoucherDetailBatchSerialId = evdbs.Id).FirstOrDefault()
            If detail IsNot Nothing Then
                detail.Quantity = spin.Value
            End If

            RefreshTotals()
        End If
    End Sub

    ''' <summary>
    ''' Aplica el recaloculo del total de acuerdo a la cantidad de la deducción a devolver
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepoSpeDeductionValueDevolution_EditValueChanged(sender As Object, e As EventArgs) Handles RepoSpeDeductionValueDevolution.EditValueChanged
        Dim spin = CType(sender, DevExpress.XtraEditors.SpinEdit)
        Dim deduction As EntranceVoucherDevolutionOtherDeduction = INDGvOtherDeduction.GetFocusedRow()
        If deduction IsNot Nothing Then
            If deduction.Value < spin.EditValue Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DevolutionDeductionGreater_RefundPurchase", NAME_MODULE)
                spin.EditValue = deduction.Value
                Exit Sub
            End If
            deduction.DevolutionValue = spin.EditValue
            _entranceDevolution.EntranceVoucherDevolutionOtherDeduction.ToList.Find(Function(x) If(x.Id = deduction.Id, x.DevolutionValue = spin.EditValue, Nothing))

            RefreshTotals()
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' evento que evalua el cambio sobre el control antes de aplciarse para realziar las validaciones de cantidad mayor o menor de cero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDTxtFreightValue_EditValueChanging(sender As Object, e As XtraEditors.Controls.ChangingEventArgs) Handles INDTxtFreightValue.EditValueChanging
        If INDTxtFreightValue.EditValue IsNot Nothing AndAlso _isLoading = False Then
            If CDec(e.NewValue) > INDTxtFreightInvoice.EditValue Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ValueGreaterFreight_RefundPurchase", NAME_MODULE)
                FreightValue = INDTxtFreightInvoice.EditValue
                e.Cancel = True
            End If
            If CDec(e.NewValue) <> 0 AndAlso e.NewValue.ToString.StartsWith("-") Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ValueGreaterFreight_RefundPurchase", NAME_MODULE)
                FreightValue = INDTxtFreightInvoice.EditValue
                e.Cancel = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de valor de la rejilla de presupuesto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepTxtValue_EditValueChanging(sender As Object, e As XtraEditors.Controls.ChangingEventArgs) Handles INDrepTxtValue.EditValueChanging
        If e IsNot Nothing AndAlso e.NewValue IsNot Nothing Then
            Dim entity As EntranceVoucherDevolutionObligationBudget = INDviewObligation.GetFocusedRow()
            If CDec(e.NewValue) > CDec(entity.ObligationBalance) Then
                Mensaje(EeventViewerImages.Advertencia) = "El valor a ejecutar no puede ser mayor al saldo de la obligación"
                e.Cancel = True
                Exit Sub
            End If
            entity.Value = e.NewValue
        End If
    End Sub

#End Region

#Region "Spin"
    ''' <summary>
    ''' Evento utilizado para validar que las cantidades no sean negativas en el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepoSpeCantDevolutionProduct_Spin(sender As Object, e As XtraEditors.Controls.SpinEventArgs) Handles RepoSpeCantDevolutionProduct.Spin
        If e.IsSpinUp = False Then
            Dim spin As DevExpress.XtraEditors.SpinEdit = sender
            If spin.EditValue IsNot Nothing OrElse spin.EditValue = String.Empty Then
                If (spin.EditValue - 1) < 0 Then
                    spin.EditValue += 1
                End If
            End If
        End If
    End Sub
#End Region

#End Region

#Region "BarButtons"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

    ''' <summary>
    ''' Evento que convoca el Método OpenSearch para hacer la busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Evento que convoca el Método deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Evento que convoca el Método Nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Evento que convoca el Método Guardar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar, BarraBotones.ClickActualizar
        BarraBotones.Focus()
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento de la barra de botones que anula el documento sin confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _entranceDevolution.Status = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Evento que convoca el método de guardar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        BarraBotones.Focus()
        Await SaveOrUpdateAndConfirm(1)
    End Sub

    ''' <summary>
    ''' Evento que convoca el método de actualizar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        BarraBotones.Focus()
        Await SaveOrUpdateAndConfirm(2)
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _entranceDevolution.Id, 0, _entranceDevolution.Id)
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
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

    Private Sub INDSleWarehouse_ButtonClick(sender As Object, e As XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleWarehouse.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmStores
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
            End Using
        End If
    End Sub

#End Region

End Class