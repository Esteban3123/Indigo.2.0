'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Henry Alejandro Vargas Polania 
' Created          : 13/01/2015
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
Imports DevExpress.XtraEditors
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Inventory.MVP
Imports Presentation.Maintenance.MVP
Imports Presentation.Payments.MVP

#End Region

Public Class FrmEntranceVoucher
    Implements IEntranceVoucher, ICustomizableForm

#Region "Builder"
    ''' <summary>
    ''' Se inicializa una isntancia de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        ctrTmp = New CtrTotalInvoiceEntranceVoucher()
        InitializeComponent()
        ctrTmp.SetInfoFunction(AddressOf getInfoServiceOrder)
        ctrTmp.PrintInfo()
        ctrTmp.PopupContainerControlTotalValue = PopUpSummarySettlement
        ctrTmp.Dock = System.Windows.Forms.DockStyle.Fill
        AddHandler ctrTmp.ConsecutiveCopied, AddressOf CtrTmp_ConsecutiveCopied
        AdditionalControlPanel.Controls.Add(ctrTmp)
        IndigoGridControl1.SetHideNoRecords(INDGcProducts, True)
        IndigoGridControl1.SetHideNoRecords(INDGcOtherDeduction, True)
        IndigoGridControl1.SetHideNoRecords(INDGcOtherWithholding, True)
    End Sub
#End Region

#Region "Globals"

    ''' <summary>
    ''' Propiedad que verifica si hay actividad económica
    ''' </summary>
    ''' <remarks></remarks>
    Private Property IsEconomicActivity As Boolean

    ''' <summary>
    ''' Listado de compromisos
    ''' </summary>
    Private ListEntranceVoucherCommitment As List(Of EntranceVoucherCommitment)

    ''' <summary>
    ''' Listado de eliminados de compromisos
    ''' </summary>
    Private ListDeleteEntranceVoucherCommitment As List(Of EntranceVoucherCommitment)

    ''' <summary>
    ''' Control para establecer informacion del ingreso
    ''' </summary>
    Private ctrTmp As CtrTotalInvoiceEntranceVoucher

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordInventory

    ''' <summary>
    ''' Representa el presentador 
    ''' </summary>
    ''' <remarks></remarks>
    Private Presenter As PEntranceVoucher

    ''' <summary>
    ''' Representa el modelo 
    ''' </summary>
    ''' <remarks></remarks>
    Private Model As MInventoryContract

    ''' <summary>
    ''' Representa la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private entranceVoucher As EntranceVoucher

    ''' <summary>
    ''' define la unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Private _idOperativeUnit As Integer

    ''' <summary>
    ''' Prefijo seleccionado
    ''' </summary>
    Private _prefixSelected As String

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' flag para solo lectura
    ''' </summary>
    ''' <remarks></remarks>
    Private OnlyRead As Boolean = False

    ''' <summary>
    ''' flag para el proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private flagLoad As Boolean = False

    ''' <summary>
    ''' Variable que contiene un item del detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private ItemEntranceVoucherDetail As EntranceVoucherDetail

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Private indexEditRecord As Integer

    ''' <summary>
    ''' Tercero de la empresa actualmente seleccionada
    ''' </summary>
    Private _currentCompany As ThirdParty

    ''' <summary>
    ''' Variable que representa la entidad de parametros
    ''' </summary>
    ''' <remarks></remarks>
    Private _settingInventory As SettingInventory

    ''' <summary>
    ''' Representa a la entidad de parámetros de cxp
    ''' </summary>
    Private _settingPayments As PaymentsSettingPaymentsXpo

    ''' <summary>
    ''' Linea de distribucion seleccionada del proveedor
    ''' </summary>
    Private _suppliersDistibutionLine As Infrastructure.Data.Xpo.CommonRepository.CommonSuppliersDistibutionLineXpo

    ''' <summary>
    ''' entidad de proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private _supplier As New Domain.Entities.Supplier

    ''' <summary>
    ''' Concepto de retención de ICA
    ''' </summary>
    Private _icaRetentionConcept As RetentionConcepts


    ''' <summary>
    ''' listado del detalle de la remision para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Private ListEntranceVoucherDetailDelete As List(Of EntranceVoucherDetail)

    ''' <summary>
    ''' Listado que contiene las retenciones que se le pueden aplican al comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListRetention As New List(Of OtherWithholdingDeduction) Implements IEntranceVoucher.ListRetention

    ''' <summary>
    ''' Listado que contiene las deducciones que se le pueden aplican al comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListDeduction As New List(Of OtherWithholdingDeduction) Implements IEntranceVoucher.ListDeduction


    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private varImp As Integer

    ''' <summary>
    ''' Controlar si confirmar procede de guardar o actualizar
    ''' </summary>
    Private _action As Integer

    Private _FillingHandleDocumentSupport As List(Of Tuple(Of Boolean, String))
    Private ReadOnly Property FillingHandleDocumentSupport As List(Of Tuple(Of Boolean, String))
        Get
            If _FillingHandleDocumentSupport Is Nothing Then
                _FillingHandleDocumentSupport = New List(Of Tuple(Of Boolean, String))
                _FillingHandleDocumentSupport.Add(New Tuple(Of Boolean, String)(True, "Si"))
                _FillingHandleDocumentSupport.Add(New Tuple(Of Boolean, String)(False, "No"))
            End If
            Return _FillingHandleDocumentSupport
        End Get
    End Property

    ''' <summary>
    ''' tasa de cambio
    ''' </summary>
    Private _tRM As TRM

    ''' <summary>
    ''' Entidad GeneralLedgerSettings (Configuración de contabilidad y mensajería electrónica)
    ''' </summary>
    Private _generalLedgerSettings As GeneralLedgerSettings

    ''' <summary>
    ''' Valores totales de los productos
    ''' </summary>
    Private _valueProducts As Decimal
    Private _valueTaxProducts As Decimal
#End Region

#Region "Entity Fields"
    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Byte Implements IEntranceVoucher.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            Me.BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces el codigo del comprobante de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IEntranceVoucher.Code
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
    ''' Obtiene o estableces la fecha del comprobante de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As DateTime? Implements IEntranceVoucher.DocumentDate
        Get
            Return INDDteDocumentDate.EditValue
        End Get
        Set(value As DateTime?)
            INDDteDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establecesel proveedor del comprobante de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property _supplierId As Integer
    Public Property SupplierId As Integer Implements IEntranceVoucher.SupplierId
        Get
            Return _supplierId
        End Get
        Set(value As Integer)
            _supplierId = value
            If entranceVoucher IsNot Nothing AndAlso flagLoad = False Then
                entranceVoucher.SupplierId = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces las lineas de distribucion del proveedor del comprobante de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierDistributionLineId As Integer? Implements IEntranceVoucher.SupplierDistributionLineId
        Get
            Return INDSleSupplier.EditValue
        End Get
        Set(value As Integer?)
            INDSleSupplier.EditValue = value
            If entranceVoucher IsNot Nothing AndAlso flagLoad = False Then
                entranceVoucher.SupplierDistributionLineId = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces el almacen del comprobante de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WarehouseId As Integer? Implements IEntranceVoucher.WarehouseId
        Get
            Return INDSleWarehouse.EditValue
        End Get
        Set(value As Integer?)
            INDSleWarehouse.EditValue = value
            If entranceVoucher IsNot Nothing AndAlso flagLoad = False Then
                entranceVoucher.WarehouseId = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la cuenta por pagar asignada al comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property _accountPayableId As Integer
    Public Property AccountPayableId As Integer? Implements IEntranceVoucher.AccountPayableId
        Get
            Return _accountPayableId
        End Get
        Set(value As Integer?)
            If value Is Nothing Then
                _accountPayableId = 0
            Else
                _accountPayableId = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la descripción asignada al comprobante de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Descripcion As String Implements IEntranceVoucher.Descripcion
        Get
            Return INDTxtDescription.Text
        End Get
        Set(value As String)
            INDTxtDescription.Text = value
            If entranceVoucher IsNot Nothing AndAlso flagLoad = False Then
                entranceVoucher.Description = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de recurso del comprobante de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ResourceType As Byte? Implements IEntranceVoucher.ResourceType
        Get
            Return CByte(INDGleResourceType.EditValue)
        End Get
        Set(value As Byte?)
            INDGleResourceType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece redondeo del comprobante de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RoundService As Integer? Implements IEntranceVoucher.RoundService
        Get
            Return CInt(INDGleRoundService.EditValue)
        End Get
        Set(value As Integer?)
            INDGleRoundService.EditValue = value
            If entranceVoucher IsNot Nothing AndAlso flagLoad = False Then
                entranceVoucher.RoundService = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el porcentaje de iva del comprobante de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IcaPercentage As Decimal? Implements IEntranceVoucher.IcaPercentage
        Get
            Return INDSpnIcaPercentage.EditValue
        End Get
        Set(value As Decimal?)
            INDSpnIcaPercentage.EditValue = value
            If entranceVoucher IsNot Nothing AndAlso flagLoad = False Then
                entranceVoucher.IcaPercentage = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece numero de factura asociado del comprobante de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InvoiceNumber As String Implements IEntranceVoucher.InvoiceNumber
        Get
            Return INDTxtInvoiceNumber.Text
        End Get
        Set(value As String)
            INDTxtInvoiceNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece fecha de la factura asociada del comprobante de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InvoiceDate As DateTime? Implements IEntranceVoucher.InvoiceDate
        Get
            Return INDDteInvoiceDate.EditValue
        End Get
        Set(value As DateTime?)
            INDDteInvoiceDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece dias de plazo de la factura del comprobante de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DayPeriod As Integer? Implements IEntranceVoucher.DayPeriod
        Get
            Return CInt(INDSpnDayPeriod.EditValue)
        End Get
        Set(value As Integer?)
            INDSpnDayPeriod.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tipo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierTypeId As Integer? Implements IEntranceVoucher.SupplierTypeId
        Get
            Return INDsleSupplierType.EditValue
        End Get
        Set(value As Integer?)
            INDsleSupplierType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece porcentaje iva del flete del comprobante de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property _freightIVAPercentage As Decimal
    Public Property FreightIVAPercentage As Decimal Implements IEntranceVoucher.FreightIVAPercentage
        Get
            Return _freightIVAPercentage
        End Get
        Set(value As Decimal)
            _freightIVAPercentage = value
            If entranceVoucher IsNot Nothing AndAlso flagLoad = False Then
                entranceVoucher.FreightIVAPercentage = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece valor neto del comprobante de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Value As Decimal Implements IEntranceVoucher.Value
        Get
            Return _valueProducts
        End Get
        Set(value As Decimal)
            _valueProducts = value
            INDPopTxtValue.EditValue = value
            If entranceVoucher IsNot Nothing And flagLoad = False Then
                entranceVoucher.Value = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del descuetno de la orden de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValueDiscount As Decimal Implements IEntranceVoucher.ValueDiscount
        Get
            Return INDTxtDiscountValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtDiscountValue.EditValue = value
            INDPopTxtDiscountValue.EditValue = value
            If entranceVoucher IsNot Nothing And flagLoad = False Then
                entranceVoucher.ValueDiscount = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del descuetno de la orden de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property _netoValue As Decimal
    Public Property NetoValue As Decimal Implements IEntranceVoucher.NetoValue
        Get
            Return _netoValue
        End Get
        Set(value As Decimal)
            _netoValue = value
            If ListDeduction.Count > 0 Then
                For Each i In ListDeduction
                    i.BaseValue = value
                Next
            End If
            If ListRetention.Count > 0 Then
                For Each i In ListRetention
                    i.BaseValue = value
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece Representa el valor del IVA de la orden de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValueTax As Decimal Implements IEntranceVoucher.ValueTax
        Get
            Return _valueTaxProducts
        End Get
        Set(value As Decimal)
            _valueTaxProducts = value
            INDPopTxtValueTax.EditValue = value
            If entranceVoucher IsNot Nothing And flagLoad = False Then
                entranceVoucher.ValueTax = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece Retencion de iva del comprobante de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WithholdingTax As Decimal Implements IEntranceVoucher.WithholdingTax
        Get
            Return INDTxtWithholdingTax.EditValue
        End Get
        Set(value As Decimal)
            INDTxtWithholdingTax.EditValue = value
            INDPopTxtWithholdingTax.EditValue = value
            If entranceVoucher IsNot Nothing And flagLoad = False Then
                entranceVoucher.WithholdingTax = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece Retencion de ica del comprobante de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WithholdingICA As Decimal Implements IEntranceVoucher.WithholdingICA
        Get
            Return INDTxtWithholdingICA.EditValue
        End Get
        Set(value As Decimal)
            INDTxtWithholdingICA.EditValue = value
            INDPopTxtWithholdingICA.EditValue = value
            If entranceVoucher IsNot Nothing And flagLoad = False Then
                entranceVoucher.WithholdingICA = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece valor del flete del comprobante de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FreightValue As Decimal? Implements IEntranceVoucher.FreightValue
        Get
            Return CDec(INDTxtFreightValue.EditValue)
        End Get
        Set(value As Decimal?)
            INDTxtFreightValue.EditValue = value
            INDPopTxtFreightValue.EditValue = value
            If entranceVoucher IsNot Nothing And flagLoad = False Then
                entranceVoucher.FreightValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece valor iva del flete del comprobante de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FreightIVAValue As Decimal Implements IEntranceVoucher.FreightIVAValue
        Get
            Return INDSpnFreightIVA.EditValue
        End Get
        Set(value As Decimal)
            INDSpnFreightIVA.EditValue = value
            INDPopSpnFreightIVA.EditValue = value
            If entranceVoucher IsNot Nothing And flagLoad = False Then
                entranceVoucher.FreightIVAValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Valor neto incluyendo el valor flete
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property NetoWithFreight As Decimal
        Get
            Return (Me.NetoValue + Me.FreightValue)
        End Get
    End Property

    ''' <summary>
    ''' Valor total de los IVAs incluyendo el IVA del flete
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property TaxWithFreight As Decimal
        Get
            Return (Me.ValueTax + Me.FreightIVAValue)
        End Get
    End Property

    ''' <summary>
    ''' Valor total de la factura incluyendo el valor del flete
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property InvoiceWithFreight As Decimal
        Get
            Return (Me.NetoValue + Me.ValueTax + Me.FreightValue + Me.FreightIVAValue)
        End Get
    End Property

    ''' <summary>
    ''' Propiedad para actualizar visualmente el Subtotal incluyendo el valor del flete
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property SubtotalUI As Decimal
        Get
            Return _valueProducts + FreightValue
        End Get
    End Property

    ''' <summary>
    ''' Propiedad para acualizar visualmente el IVA incluyendo el IVA del flete
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ValueTaxUI As Decimal
        Get
            Return _valueTaxProducts + FreightIVAValue
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece Retencion en la fuente del comprobante de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RetentionSource As Decimal Implements IEntranceVoucher.RetentionSource
        Get
            Return INDTxtRetentionSource.EditValue
        End Get
        Set(value As Decimal)
            INDTxtRetentionSource.EditValue = value
            INDPopTxtRetentionSource.EditValue = value
            If entranceVoucher IsNot Nothing And flagLoad = False Then
                entranceVoucher.RetentionSource = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece otras Retencion del comprobante de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RetentionOther As Decimal Implements IEntranceVoucher.RetentionOther
        Get
            Return INDTxtRetentionOther.EditValue
        End Get
        Set(value As Decimal)
            INDTxtRetentionOther.EditValue = value
            INDPopTxtRetentionOther.EditValue = value
            If entranceVoucher IsNot Nothing And flagLoad = False Then
                entranceVoucher.RetentionOther = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece otras deducciones del comprobante de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DeductionOther As Decimal Implements IEntranceVoucher.DeductionOther
        Get
            Return INDTxtDeductionOther.EditValue
        End Get
        Set(value As Decimal)
            INDTxtDeductionOther.EditValue = value
            INDPopTxtDeductionOther.EditValue = value
            If entranceVoucher IsNot Nothing And flagLoad = False Then
                entranceVoucher.DeductionOther = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece impuesto distrital del comprobante de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DistrictTax As Decimal Implements IEntranceVoucher.DistrictTax
        Get
            Return INDPopTxtDistricTaxes.EditValue
        End Get
        Set(value As Decimal)
            INDPopTxtDistricTaxes.EditValue = value
            If entranceVoucher IsNot Nothing And flagLoad = False Then
                entranceVoucher.DistrictTax = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece Representa el valor total de la orden de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TotalValue As Decimal Implements IEntranceVoucher.TotalValue
        Get
            Return INDTxtTotal.EditValue
        End Get
        Set(value As Decimal)
            INDPopTxtTotalCxp.EditValue = value
            INDTxtTotal.EditValue = value
            If entranceVoucher IsNot Nothing And flagLoad = False Then
                entranceVoucher.TotalValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece la actividad económica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EconomicActivityId As Integer?
        Get
            Return INDsleIncomeGeneratingEconomicActivity.EditValue
        End Get
        Set(value As Integer?)
            INDsleIncomeGeneratingEconomicActivity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de las actividades económicas que sí generan ingresos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EconomicActivityXpo As XPInstantFeedbackSource Implements IEntranceVoucher.EconomicActivityXpo
        Get
            Return CType(INDsleIncomeGeneratingEconomicActivity.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleIncomeGeneratingEconomicActivity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InvoiceValue As Decimal Implements IEntranceVoucher.InvoiceValue
#End Region

#Region "Properties"

    ''' <summary>
    ''' Datasource compromiso
    ''' </summary>
    ''' <returns></returns>
    Public Property CommitmentDetailXpo As XPCollection Implements IEntranceVoucher.CommitmentDetailXpo
        Get
            Return INDsleCommitmentDetail.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleCommitmentDetail.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datsource entidad presupuestal
    ''' </summary>
    ''' <returns></returns>
    Public Property BudgetaryEntityXpo As XPCollection Implements IEntranceVoucher.BudgetaryEntityXpo
        Get
            Return INDsleBudgetaryEntity.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleBudgetaryEntity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource vigencia
    ''' </summary>
    ''' <returns></returns>
    Public Property BudgetaryValidityXpo As XPCollection Implements IEntranceVoucher.BudgetaryValidityXpo
        Get
            Return INDsleBudgetaryValidity.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleBudgetaryValidity.Properties.DataSource = value
        End Set
    End Property


    ''' <summary>
    ''' Obtiene o establece el proveedor con sus lineas de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SuppliersDistributionLinesXpo As Xpo.XPInstantFeedbackSource Implements IEntranceVoucher.SuppliersDistributionLinesXpo
        Get
            Return CType(INDSleSupplier.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As Xpo.XPInstantFeedbackSource)
            INDSleSupplier.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource el tipo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierTypeXpo As List(Of SupplierType) Implements IEntranceVoucher.SupplierTypeXpo
        Get
            Return INDsleSupplierType.Properties.DataSource
        End Get
        Set(value As List(Of SupplierType))
            INDsleSupplierType.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource del control de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListSupplier As XPInstantFeedbackSource Implements IEntranceVoucher.ListSupplier
        Get
            Return INDSleSupplier.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleSupplier.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica como se va redondear
    ''' </summary>
    ''' <remarks></remarks>
    Private _entranceSource As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListEntranceSource As List(Of Tuple(Of Integer, String))
        Get
            If _entranceSource Is Nothing Then
                _entranceSource = New List(Of Tuple(Of Integer, String))
                _entranceSource.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("EntranceVoucher_EntranceSource_NoOne", NAME_MODULE)))
                _entranceSource.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("EntranceVoucher_EntranceSource_PurshaseOrder", NAME_MODULE)))
                _entranceSource.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("EntranceVoucher_EntranceSource_Contract", NAME_MODULE)))
                _entranceSource.Add(New Tuple(Of Integer, String)(4, ResourceManager.GetString("EntranceVoucher_EntranceSource_RemissionEntrance", NAME_MODULE)))
                _entranceSource.Add(New Tuple(Of Integer, String)(5, ResourceManager.GetString("EntranceVoucher_EntranceSource_ConsignmentInventoryRemission", NAME_MODULE)))
            End If
            Return _entranceSource
        End Get
    End Property

    ''' <summary>
    ''' Especifica como se va redondear
    ''' </summary>
    ''' <remarks></remarks>
    Private _roundService As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListRoundService As List(Of Tuple(Of Integer, String))
        Get
            If _roundService Is Nothing Then
                _roundService = New List(Of Tuple(Of Integer, String))
                _roundService.Add(New Tuple(Of Integer, String)(0, ResourceManager.GetString("EntranceVoucher_Without_Rounding", NAME_MODULE)))
                _roundService.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("EntranceVoucher_Peso", NAME_MODULE)))
                _roundService.Add(New Tuple(Of Integer, String)(10, ResourceManager.GetString("EntranceVoucher_Decima", NAME_MODULE)))
                _roundService.Add(New Tuple(Of Integer, String)(100, ResourceManager.GetString("EntranceVoucher_Hundredth", NAME_MODULE)))
                _roundService.Add(New Tuple(Of Integer, String)(1000, ResourceManager.GetString("EntranceVoucher_Thousandth", NAME_MODULE)))
            End If
            Return _roundService
        End Get
    End Property

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Public Property _sequence As Domain.Entities.InventorySequence
    Public Property Sequense As InventorySequence Implements IEntranceVoucher.Sequense
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
    ''' Especifica el Tipo de Recurso (Ninguna = 0,Funcionamiento = 1,Inversion = 2)
    ''' </summary>
    ''' <remarks></remarks>
    Private _resource As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListResource As List(Of Tuple(Of Integer, String))
        Get
            If _resource Is Nothing Then
                _resource = New List(Of Tuple(Of Integer, String))
                _resource.Add(New Tuple(Of Integer, String)(0, ResourceManager.GetString("EntranceVoucher_None", NAME_MODULE)))
                _resource.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("EntranceVoucher_Performance", NAME_MODULE)))
                _resource.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("EntranceVoucher_Investment", NAME_MODULE)))
            End If
            Return _resource
        End Get
    End Property

    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements IEntranceVoucher.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene y establece el tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IEntranceVoucher.MyTag
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
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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
    ''' Establece la dispocicion de los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IEntranceVoucher.ActionsOnControls
        Set(value As Boolean)
            ' Datos Principales
            INDlyEntranceVoucher.BeginUpdate()
            INDBtnCode.Enabled = Not value
            INDDteDocumentDate.Enabled = value
            INDSleSupplier.Enabled = value
            INDsleSupplierType.Enabled = value
            INDSleWarehouse.Enabled = value
            INDTxtDescription.Enabled = value
            INDGleResourceType.Enabled = value
            INDGleRoundService.Enabled = value
            INDSpnIcaPercentage.Enabled = value
            INDsleCurrency.Enabled = value
            ' Factura y Flete
            INDsleCommitmentDetail.Enabled = value
            INDTxtInvoiceNumber.Enabled = value
            INDDteInvoiceDate.Enabled = value
            INDSleHandleDocumentSupport.Enabled = value
            INDSleDocumentSupportId.Enabled = value
            INDSpnDayPeriod.Enabled = value
            INDTxtFreightValue.Enabled = value
            INDSpnFreightIVA.Enabled = value
            INDsleIncomeGeneratingEconomicActivity.Enabled = value
            ' Productos
            If flagLoad Then
                INDBtnAddProducts.Enabled = True
            Else
                INDBtnAddProducts.Enabled = value
            End If
            INDGcProducts.Enabled = value

            INDpceCommitment.Enabled = value
            INDgcCommitment.Enabled = value

            BarraBotones.StatusRecordVisible = value
            INDlyEntranceVoucher.EndUpdate()
            If value Then
                INDDteDocumentDate.Focus()
            Else
                INDBtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los almacenes
    ''' </summary>
    ''' <remarks></remarks>
    Public Property ListWareHouse As XPInstantFeedbackSource Implements IEntranceVoucher.ListWareHouse
        Get
            Return INDSleWarehouse.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleWarehouse.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Porpiedad que contiene el listado de centros de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentSupportXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IEntranceVoucher.DocumentSupportXpo
        Get
            Return CType(INDSleDocumentSupportId.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleDocumentSupportId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece el tipo de registro de IVA, 1 alcosto, 2 descontable
    ''' </summary>
    ''' <returns></returns>
    Public Property TaxRegistration As Byte? Implements IEntranceVoucher.TaxRegistration
        Get
            Return If(INDSleTaxRegistration.EditValue Is Nothing, 1, CByte(INDSleTaxRegistration.EditValue))
        End Get
        Set(value As Byte?)
            If value Is Nothing OrElse value = 3 Then
                INDLciTaxRegistration.Visibility = XtraLayout.Utils.LayoutVisibility.Always
                INDSleTaxRegistration.EditValue = 1
            Else
                INDSleTaxRegistration.EditValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de la moneda, recibe como parametro opcional la abreviacion para cuando se postula manualmente el Id
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    ''' <returns></returns>
    Public Property CurrencyId(Optional _currencyAbbreviation As String = Nothing) As Integer Implements IEntranceVoucher.CurrencyId
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
    Public Property CurrencyDatasource As XPInstantFeedbackSource Implements IEntranceVoucher.CurrencyDatasource
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
    ''' Digitos de la mascara numerica
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property MaskDigitRounding As Integer
        Get
            Return If(Me.RoundService = 0, 2, 0)
        End Get
    End Property


    Private _listControls As List(Of TextEdit)
    ''' <summary>
    ''' Lista de TextEdit para la culturizacion
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property ListControls As List(Of TextEdit)
        Get
            If _listControls Is Nothing Then
                _listControls = New List(Of TextEdit)
                _listControls.Add(Me.INDTxtFreightValue)
                _listControls.Add(Me.INDSpnFreightIVA)
                _listControls.Add(Me.INDTxtValue)
                _listControls.Add(Me.INDTxtDiscountValue)
                _listControls.Add(Me.INDTxtValueTax)
                _listControls.Add(Me.INDTxtWithholdingTax)
                _listControls.Add(Me.INDTxtWithholdingICA)
                _listControls.Add(Me.INDTxtRetentionSource)
                _listControls.Add(Me.INDTxtRetentionOther)
                _listControls.Add(Me.INDTxtDeductionOther)
                _listControls.Add(Me.INDTxtTotal)
                _listControls.Add(Me.INDPopTxtDistricTaxes)
                _listControls.Add(Me.INDPopTxtDeductionOther)
                _listControls.Add(Me.INDPopTxtRetentionOther)
                _listControls.Add(Me.INDPopTxtRetentionSource)
                _listControls.Add(Me.INDPopSpnFreightIVA)
                _listControls.Add(Me.INDPopTxtWithholdingICA)
                _listControls.Add(Me.INDPopTxtWithholdingTax)
                _listControls.Add(Me.INDPopTxtFreightValue)
                _listControls.Add(Me.INDPopTxtValueTax)
                _listControls.Add(Me.INDPopTxtTotalCxp)
                _listControls.Add(Me.INDPopTxtDiscountValue)
                _listControls.Add(Me.INDPopTxtValue)
            End If
            Return _listControls
        End Get
    End Property

    Private _listGridColumns As List(Of DevExpress.XtraGrid.Columns.GridColumn)
    ''' <summary>
    ''' Lista de las columnas de la rejilla que maneja valor
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property ListGridColumns As List(Of DevExpress.XtraGrid.Columns.GridColumn)
        Get
            If _listGridColumns Is Nothing Then
                _listGridColumns = New List(Of DevExpress.XtraGrid.Columns.GridColumn)
                _listGridColumns.Add(Me.ColUnitValue)
                _listGridColumns.Add(Me.ColProductCost)
                _listGridColumns.Add(Me.ColTotal)
                _listGridColumns.Add(Me.INDColBalanceBudget)
                _listGridColumns.Add(Me.INDColValueBudget)
                _listGridColumns.Add(Me.ColBaseValue)
                _listGridColumns.Add(Me.ColRetentionValue)
                _listGridColumns.Add(Me.ColValueBaseDeduction)
                _listGridColumns.Add(Me.ColDeductionValue)
                _listGridColumns.Add(Me.ColIvaValue)
                _listGridColumns.Add(Me.ColDiscountValue)
            End If
            Return _listGridColumns
        End Get
    End Property
#End Region

#Region "DataSource"
    ''' <summary>
    ''' datasource para registro de IVA
    ''' </summary>
    Private _listTaxRegistration As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListTaxRegistration As List(Of Tuple(Of Byte, String))
        Get
            If _listTaxRegistration Is Nothing Then
                _listTaxRegistration = New List(Of Tuple(Of Byte, String))
                _listTaxRegistration.Add(New Tuple(Of Byte, String)(1, ResourceManager.GetString("TaxCost", NAME_MODULE)))
                _listTaxRegistration.Add(New Tuple(Of Byte, String)(2, ResourceManager.GetString("DiscountableTax", NAME_MODULE)))
            End If
            Return _listTaxRegistration
        End Get
    End Property
#End Region

#Region "Events"

    ''' <summary>
    ''' Evento que se dispara cuando se copia el consecutivo de cuenta por pagar
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <remarks></remarks>
    Private Sub CtrTmp_ConsecutiveCopied(consecutive As String)
        Mensaje(EeventViewerImages.Informacion) = "Consecutivo CxP copiado al portapapeles: " + consecutive
    End Sub

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing
        Presenter = Nothing
        Model = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequence = Nothing
        _prefixSelected = Nothing
        _currentCompany = Nothing
        _settingInventory = Nothing
        _suppliersDistibutionLine = Nothing
        _supplier = Nothing
        _icaRetentionConcept = Nothing
        entranceVoucher = Nothing
        record = Nothing
        indexEditRecord = Nothing
        ItemEntranceVoucherDetail = Nothing
        ListEntranceVoucherDetailDelete = Nothing
        ListRetention = Nothing
        ListDeduction = Nothing
        OnlyRead = Nothing
        flagLoad = Nothing
        varImp = Nothing
        _action = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando incia el form, carga los controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmEntranceVoucher_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Personalizacion de la rejilla, muestra las columnas ocultas como mas infomacion
        IndigoGridView1.MoreInfoColunmns(INDGvProducts)

        ' Agrega a la rejilla la columna de Acciones
        IndigoGridView1.SetListAcction(INDGvProducts, {eAcciones.Edit, eAcciones.Remove}.ToList())
        IndigoGridControl1.RefreshGrid(INDGcProducts)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvProducts.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        ' Agrega a la rejilla de presupuesto la columna de Acciones
        IndigoGridView2.SetListAcction(INDviewGridCommitment, {eAcciones.Remove}.ToList())
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewGridCommitment.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        'GroupSummaries
        INDGvProducts.OptionsView.GroupFooterShowMode = XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
        Dim item1 As DevExpress.XtraGrid.GridGroupSummaryItem = New DevExpress.XtraGrid.GridGroupSummaryItem()
        item1.FieldName = "TotalValue"
        item1.SummaryType = DevExpress.Data.SummaryItemType.Sum
        item1.DisplayFormat = Convert.ToChar(Keys.Tab) + "{0:c0}"
        item1.ShowInGroupColumnFooter = INDGvProducts.Columns("TotalValue")
        INDGvProducts.GroupSummary.Add(item1)

        Me.LayoutControls.SetIsCustomizable(Me.INDlyEntranceVoucher, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        'Cargar GridLookUpEdit        
        INDSleHandleDocumentSupport.Properties.DataSource = FillingHandleDocumentSupport
        'Dar un valor por defecto a los GridLookEdit        
        INDSleHandleDocumentSupport.EditValue = False
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PEntranceVoucher(Me)

        Try
            AsyncLoader(True)
            Await LoadParameters()
            Await Presenter.GetSequense()
            Await GetOtherRentetionsAndDeductions()
            Await LoadCurrentCompany()
        Catch ex As Exception
            Me.Mensaje(EeventViewerImages.Advertencia) = ex.Message
        Finally
            AsyncLoader(False)
        End Try

        INDGleResourceType.Properties.DataSource = ListResource
        INDGleRoundService.Properties.DataSource = ListRoundService
        RepositoryItemGridLookUpEdit1.DataSource = ListEntranceSource
        INDSleTaxRegistration.Properties.DataSource = ListTaxRegistration
        IndigoGridControl1.SetControlNextFocus(INDGcProducts, INDTxtFreightValue)
        ShowEconomicActivity()
        LoadStatus()
        Deshacer()
        INDDteInvoiceDate.Properties.MaxValue = GetDateServer()
    End Sub
#End Region

#Region "Activated"
    ''' <summary>
    ''' Evetno que se dispara cuando el formulario se activa, direge el foco al control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmEntranceVoucher_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDBtnCode.Text Is String.Empty Then
            INDBtnCode.Focus()
        End If
    End Sub
#End Region

#Region "FormClosing"
    ''' <summary>
    ''' Evento que se dispara cuando el formulario se va a cerrar, elimina el registro bloqueado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmEntranceVoucher_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Evento que se dispara cuando es seleccionado un valor en el combo y establece los valores del proveedor y las lineas de distribucion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSleSupplier_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleSupplier.EditValueChanged
        If SupplierDistributionLineId > 0 Then
            If flagLoad = False Then
                If viewSupplier.DataSource Is Nothing Then
                    _suppliersDistibutionLine = Presenter.GetSupplierDistributionLineById(SupplierDistributionLineId)
                Else
                    _suppliersDistibutionLine = DirectCast(DirectCast(viewSupplier.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonSuppliersDistibutionLineXpo)
                End If

                SupplierId = _suppliersDistibutionLine.IdSupplier.Id
                Using modelSupplier As New MSupplier(MyTag)
                    _supplier = modelSupplier.GetSupplierById(SupplierId)
                    INDSpnDayPeriod.EditValue = _supplier.TimeLimitDays
                End Using

                If _generalLedgerSettings?.HandlesSupportDocument Then
                    ''Se oculta elemento  cuando el tercero es facturador electrónico (ElectronicBiller = true)
                    INDLyItemHandleDocumentSupport.HideControl(_supplier.ThirdParty.ElectronicBiller = True)
                    INDSleHandleDocumentSupport.EditValue = (_supplier.ThirdParty.ElectronicBiller = False)
                    INDLyItemDocumentSupportId.HideControl(_supplier.ThirdParty.ElectronicBiller = True)
                End If

                If WarehouseId <> 0 Then
                    INDBtnAddProducts.Enabled = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                End If

                INDsleSupplierType.SupplierId = SupplierId
                INDsleCommitmentDetail.Properties.NullText = String.Empty

                Await LoadIcaPercentage(_supplier, SupplierDistributionLineId, Me.BarraBotones.OperatingUnit.Id)
            End If
        Else
            _suppliersDistibutionLine = Nothing
            _supplier = Nothing
            SupplierId = 0
            IcaPercentage = 0
            INDsleCommitmentDetail.Properties.NullText = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando es seleccionado un almacen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleWarehouse_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleWarehouse.EditValueChanged
        If WarehouseId <> 0 And SupplierId <> 0 Then
            INDBtnAddProducts.Enabled = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
        End If
        If Me.entranceVoucher IsNot Nothing AndAlso Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("O") Then
            If Not flagLoad Then
                Dim store = If(INDSleViewWarehouse.DataSource IsNot Nothing, DirectCast(DirectCast(INDSleViewWarehouse.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.InventoryRepository.WarehouseXpo), Nothing)
                Me._idCurrentSequence = Me.GetIdSequenceByPrefix(If(store IsNot Nothing, store.Prefix, Me.entranceVoucher.Prefix))
                Me._prefixSelected = If(store IsNot Nothing, store.Prefix, Me.entranceVoucher.Prefix)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Manejador del evento que calcula el valor del iva del flete de acuerdo al valor digitado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDTxtFreightValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtFreightValue.EditValueChanged
        If Not _updatingUI AndAlso Not flagLoad AndAlso INDTxtFreightValue.EditValue IsNot Nothing Then
            FreightValue = CDec(INDTxtFreightValue.EditValue)
            FreightIVAValue = Utils.RoundValue(CDec(INDTxtFreightValue.EditValue * FreightIVAPercentage / 100), CInt(RoundService))
            CalculateTotalValue()
            IndigoGridControl1.ControlNextFocus = True
        End If
    End Sub

    ''' <summary>
    ''' Carga los tipos de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplierType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSupplierType.EditValueChanged
        If SupplierTypeId IsNot Nothing AndAlso Not flagLoad Then
            INDsleSupplierType.ValidateSupplierType()
        End If
    End Sub

    ''' <summary>
    ''' Asigna los valores para conversion de redondeo de los valores del comprobante
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleRoundService_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleRoundService.EditValueChanged
        If INDGleRoundService.EditValue IsNot Nothing AndAlso RoundService IsNot Nothing Then
            If Me.Visible Then
                SetCurrencyUI(Me.CurrencyAbbreviation)
            End If
            RoundService = CInt(INDGleRoundService.EditValue)
            CalculateTotalValue()
        End If
    End Sub

    Private Sub INDSleHandleDocumentSupport_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleHandleDocumentSupport.EditValueChanged
        INDSleDocumentSupportId.EditValue = Nothing
        INDSleDocumentSupportId.Properties.NullText = String.Empty

        INDLyItemDocumentSupportId.HideControl(If(INDSleHandleDocumentSupport.EditValue Is Nothing Or INDSleHandleDocumentSupport.EditValue = 0, True, Not INDSleHandleDocumentSupport.EditValue))
    End Sub

    ''' <summary>
    ''' Evento para cuando se cambia la moneda 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDsleCurrency_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCurrency.EditValueChanged
        If INDsleCurrency.EditValue Is Nothing OrElse Me.CurrencyId = 0 Then
            Exit Sub
        End If

        Await Me.ValidateCurrencyEditValue(Me.flagLoad, Me.CurrencyId)
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de valor de la rejilla de presupuesto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepTxtValue_EditValueChanging(sender As Object, e As XtraEditors.Controls.ChangingEventArgs) Handles INDrepTxtValue.EditValueChanging
        If e IsNot Nothing AndAlso e.NewValue IsNot Nothing Then
            Dim entity As EntranceVoucherCommitment = INDviewGridCommitment.GetFocusedRow()
            If CDec(e.NewValue) > CDec(entity.Balance) Then
                Mensaje(EeventViewerImages.Advertencia) = "El valor a ejecutar no puede ser mayor al saldo del compromiso"
                e.Cancel = True
                Exit Sub
            End If
            entity.Value = e.NewValue
        End If
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDSleWarehouse_ButtonClick(sender As Object, e As XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleWarehouse.ButtonClick
        If e.Button.Kind = XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("302", Nothing, True)
        End If
    End Sub

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
    ''' Evento que se dispara al dar click en el boton del control de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSupplier_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleSupplier.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("558", Nothing, True)
            Presenter.InitializeSupplier()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplierType_ButtonClick(sender As Object, e As XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSupplierType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("726", "", True)
            INDsleSupplierType.Properties.DataSource = Nothing
            INDsleSupplierType.Me_QueryPopUp(Nothing, Nothing)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en el boton del control de la actividad económica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIncomeGeneratingEconomicActivity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleIncomeGeneratingEconomicActivity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1845, Nothing, True)
            Presenter.GetEconomicActivity()
        End If
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
            Me.BarraBotones.ControlHideStatus = False
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewEntranceVoucher()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Mueve el foco desde el control de porcentaje de ica para el boton de agregar productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSpnIcaPercentage_KeyDown(sender As Object, e As KeyEventArgs) Handles INDSpnIcaPercentage.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDBtnAddProducts.Focus()
        End If
    End Sub
#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Carga el datasource de la entidad presupuestal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBudgetaryEntity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBudgetaryEntity.QueryPopUp
        If BudgetaryEntityXpo Is Nothing Then
            Presenter.InitializeBudgetaryEntity()
        End If
    End Sub

    ''' <summary>
    ''' Carga el datasource de la vigencia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBudgetaryValidity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBudgetaryValidity.QueryPopUp
        If BudgetaryValidityXpo Is Nothing AndAlso INDsleBudgetaryEntity.EditValue IsNot Nothing Then
            Presenter.InitializeBudgetaryValidity(INDsleBudgetaryEntity.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' Carga el datasource del compromiso
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCommitmentDetail_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCommitmentDetail.QueryPopUp
        If CommitmentDetailXpo Is Nothing AndAlso _supplier.ThirdParty IsNot Nothing AndAlso _supplier.ThirdParty.Id > 0 AndAlso INDsleBudgetaryValidity.EditValue IsNot Nothing Then
            Presenter.InitializeCommitmentDetail(_supplier.ThirdParty.Id, INDsleBudgetaryValidity.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' Realiza la consulta del combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSupplier_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleSupplier.QueryPopUp
        If INDSleSupplier.Properties.ReadOnly = True Then
            Exit Sub
        End If
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
    Private Sub INDSleWarehouse_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleWarehouse.QueryPopUp
        If INDSleWarehouse.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If INDSleWarehouse.Properties.DataSource Is Nothing Then
            Presenter.LoadWarehouse()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar documento soporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleResolutionDocumentSupport_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleDocumentSupportId.QueryPopUp
        Presenter.InitializeDocumentSupport()
    End Sub

    ''' <summary>
    ''' Evento para consultar las monedas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCurrency_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCurrency.QueryPopUp
        If Me.CurrencyDatasource Is Nothing Then
            Presenter.InitializeCurrency()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se despliega el control de las actividades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIncomeGeneratingEconomicActivity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleIncomeGeneratingEconomicActivity.QueryPopUp
        If EconomicActivityXpo Is Nothing Then
            Presenter.GetEconomicActivity()
        End If
    End Sub
#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de agregar presupuesto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddCommitment_Click(sender As Object, e As EventArgs) Handles INDbtnAddCommitment.Click
        If ValidateControlsPopup() = False Then
            Exit Sub
        End If

        If ListEntranceVoucherCommitment Is Nothing Then
            ListEntranceVoucherCommitment = New List(Of EntranceVoucherCommitment)
        End If

        If (From x In ListEntranceVoucherCommitment Where x.CommitmentDetailId = INDsleCommitmentDetail.EditValue Select x).Count > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El compromiso seleccionado ya existe en la lista"
            Exit Sub
        End If

        Dim entityXpo As ViewListCommitmentDetailXpo = INDviewSearchCommitment.GetFocusedRow()
        Dim entranceVoucherCommitment As New EntranceVoucherCommitment
        With entranceVoucherCommitment
            .CommitmentDetailId = entityXpo.CommitmentDetailId
            .Value = 0
            .CommitmentCode = entityXpo.CommitmentCode
            .CommitmentDocument = entityXpo.Document
            .CategoryCodeName = entityXpo.CategoryCodeName
            .FinancialSourceCodeName = entityXpo.FinancialSourceCodeName
            .RevenueTypeCodeName = entityXpo.RevenueTypeCodeName
            .Balance = entityXpo.Balance
        End With

        ListEntranceVoucherCommitment.Add(entranceVoucherCommitment)
        INDgcCommitment.DataSource = Nothing
        INDgcCommitment.DataSource = ListEntranceVoucherCommitment
        INDsleCommitmentDetail.EditValue = Nothing
        INDsleCommitmentDetail.Focus()
    End Sub

    ''' <summary>
    ''' Abre el popup para agregar productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAddProducts_Click(sender As Object, e As EventArgs) Handles INDBtnAddProducts.Click
        If OnlyRead = False Then
            If SupplierDistributionLineId Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Seleccione un proveedor antes de agregar los productos."
                Exit Sub
            End If
            If SupplierTypeId Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Seleccione un tipo de proveedor antes de agregar los productos."
                Exit Sub
            End If
            If WarehouseId Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Seleccione un almacen antes de agregar los productos."
                Exit Sub
            End If
            InstantiatePopup()
        End If
    End Sub

    ''' <summary>
    ''' Evento al aceptar dialog
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmNotificationItemDetailConfirm_Accept(sender As Object, e As EventArgs)
        Await SaveOrUpdateAndConfirm(_action, False)
    End Sub

#End Region

#Region "CheckStateChanged"
    ''' <summary>
    ''' Evento que que dispara la sumatoria de las otras retenciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepoCheckRetentionSelected_CheckStateChanged(sender As Object, e As EventArgs) Handles RepoCheckRetentionSelected.CheckStateChanged
        Dim check = CType(sender, DevExpress.XtraEditors.CheckEdit)
        Dim retention As OtherWithholdingDeduction = INDGvOtherWithholdingDeduction.GetFocusedRow()
        If retention IsNot Nothing Then
            If entranceVoucher.EntranceVoucherOtherDeduction Is Nothing Then
                entranceVoucher.EntranceVoucherOtherDeduction = New Domain.Entities.TrackableCollection(Of EntranceVoucherOtherDeduction)
            End If
            If check.EditValue Then
                Dim od As New EntranceVoucherOtherDeduction()
                od.OtherWithholdingDeductionId = retention.Id
                od.Value = Utils.RoundValue(CDec((retention.BaseValue * (retention.RetentionPercentage / 100))), CInt(RoundService))
                od.ValueOutstanding = od.Value
                od.WithholdingPercentage = retention.RetentionPercentage
                od.Type = 1
                entranceVoucher.EntranceVoucherOtherDeduction.Add(od)
            Else
                If entranceVoucher.EntranceVoucherOtherDeduction.Count > 0 Then
                    If entranceVoucher.EntranceVoucherOtherDeduction.ToList().Find(Function(x) x.Id <> 0) IsNot Nothing Then
                        entranceVoucher.EntranceVoucherOtherDeduction.ToList().Find(Function(x) x.OtherWithholdingDeductionId = retention.Id).MarkAsDeleted()
                    Else
                        entranceVoucher.EntranceVoucherOtherDeduction.Remove(entranceVoucher.EntranceVoucherOtherDeduction.ToList().Find(Function(x) x.OtherWithholdingDeductionId = retention.Id))
                    End If
                End If
            End If
            RetentionOther = entranceVoucher.EntranceVoucherOtherDeduction.Sum(Function(x) IIf(x.Type = 1, x.Value, 0))
            If RetentionOther < 0 Then
                RetentionOther = 0
            End If
            CalculateTotalValue()
        End If
    End Sub

    ''' <summary>
    ''' Evento que que dispara la sumatoria de las otras deducciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepoCheckDeductionSelected_CheckStateChanged(sender As Object, e As EventArgs) Handles RepoCheckDeductionSelected.CheckStateChanged
        Dim check = CType(sender, DevExpress.XtraEditors.CheckEdit)
        Dim deduction As OtherWithholdingDeduction = INDGvOtherDeduction.GetFocusedRow()
        If deduction IsNot Nothing Then
            If entranceVoucher.EntranceVoucherOtherDeduction Is Nothing Then
                entranceVoucher.EntranceVoucherOtherDeduction = New Domain.Entities.TrackableCollection(Of EntranceVoucherOtherDeduction)
            End If
            If check.EditValue Then
                Dim od As New EntranceVoucherOtherDeduction()
                od.OtherWithholdingDeductionId = deduction.Id
                od.Value = Utils.RoundValue(CDec(deduction.DeductionValue), CInt(RoundService))
                od.ValueOutstanding = od.Value
                od.Type = 2
                entranceVoucher.EntranceVoucherOtherDeduction.Add(od)
            Else
                If entranceVoucher.EntranceVoucherOtherDeduction.Count > 0 Then
                    If entranceVoucher.EntranceVoucherOtherDeduction.ToList().Find(Function(x) x.Id <> 0) IsNot Nothing Then
                        entranceVoucher.EntranceVoucherOtherDeduction.ToList().Find(Function(x) x.OtherWithholdingDeductionId = deduction.Id).MarkAsDeleted()
                    Else
                        entranceVoucher.EntranceVoucherOtherDeduction.Remove(entranceVoucher.EntranceVoucherOtherDeduction.ToList().Find(Function(x) x.OtherWithholdingDeductionId = deduction.Id))
                    End If
                End If
            End If
            DeductionOther = entranceVoucher.EntranceVoucherOtherDeduction.Sum(Function(x) IIf(x.Type = 2, x.Value, 0))
            If DeductionOther < 0 Then
                DeductionOther = 0
            End If
            CalculateTotalValue()
        End If
    End Sub
#End Region

#Region "ImportarInformacion"
    ''' <summary>
    ''' Evento que dispara el formulario de importar productos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        Using formulario As New FrmImportProduct
            AddHandler formulario.GetListEntranceVoucherDetail, AddressOf ReturnGetListEntranceVoucherDetail
            Me.Cursor = ChangeCursorIndigo()
            formulario.Size = New System.Drawing.Size(800, 700)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.SupplierDistributionLineId = SupplierDistributionLineId
            formulario.SupplierId = SupplierId
            formulario.codeUser = indigo.UserIndigo
            formulario.WarehouseId = INDSleWarehouse.EditValue
            formulario.CurrencyId = Me.CurrencyId
            formulario.ListEntranceVoucherDetailValidation = entranceVoucher.EntranceVoucherDetail.ToList()
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub
#End Region

#Region "Click_ButtonAction"

    ''' <summary>
    ''' Evento que da la opcion de eliminar o modificar los regitros de productos de la orden de traslado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        DeleteCommitment()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmEntranceVoucher_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDBtnCode.Focus()

        'Lógica del control de compromiso
        ShowHideControlCommitment()
    End Sub

    ''' <summary>
    ''' Consulta la fecha de los parametros y establece la fecha minima y maxima de la fecha del documento
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateDate()
        If _settingInventory Is Nothing OrElse _settingInventory.Id = 0 Then
            Exit Sub
        End If

        Dim dateMin As DateTime = Convert.ToDateTime(_settingInventory.Year.ToString + "/" + _settingInventory.Month.ToString + "/01")
        INDDteDocumentDate.Properties.MinValue = dateMin
        INDDteDocumentDate.Properties.MaxValue = GetDateServer()
    End Sub

#End Region

#Region "Popup"
    Private Sub INDRpPceBatch_Popup(sender As Object, e As EventArgs) Handles RptPceLote.Popup
        Dim detail = DirectCast(INDGvProducts.GetFocusedRow, EntranceVoucherDetail)
        INDGcBatch.DataSource = Nothing
        INDGcBatch.DataSource = detail.EntranceVoucherDetailBatchSerial
    End Sub
#End Region

#Region "ShowingEditor"
    Private Sub INDGvProducts_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDGvProducts.ShowingEditor
        Dim detail = DirectCast(INDGvProducts.GetFocusedRow, EntranceVoucherDetail)
        RptPceLote.ReadOnly = Not detail.HandlesBatch
    End Sub
#End Region

#End Region

#Region "ICrud"
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Método utilziado para anular el comprobante
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Anular() Implements Base.ICrudBase.Eliminar
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            entranceVoucher.Status = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Limpia los campos e inicia una nueva instancia del comprobante
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewEntranceVoucher()
        End If
    End Sub

    ''' <summary>
    ''' Método asignado de la barra de botones que despliega el formulario de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Método asignado a la barra de botones que deshace los cambios y limpia los campos del frontal
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Método utilziado para guardar el comprobante
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If entranceVoucher IsNot Nothing AndAlso entranceVoucher.Status < 3 Then
            Dim errors = ValidateControls()
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
            AssigningValues()
        End If
        Try
            Using model As New MEntranceVoucher(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveEntranceVoucher(entranceVoucher, _idCurrentSequence, Me._sequence)
                If result.StateResult = True Then
                    entranceVoucher = result.ObjectEmbbeded
                    If entranceVoucher.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                        Me.BarraBotones.PrintReport(PrintReportAction.Create, entranceVoucher.Id, 0, entranceVoucher.Id)
                    Else
                        If entranceVoucher.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, entranceVoucher.Id, 0, entranceVoucher.Id)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, entranceVoucher.Id, 0, entranceVoucher.Id)
                        End If
                    End If
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDBtnCode.Enabled = False
                    If result.StateResult = False And result.StateResultAux = False Then
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    If entranceVoucher.Id > 0 Then
                        Dim ev = Await model.GetEntranceVoucher(Code)
                        entranceVoucher = ev.ObjectEmbbeded
                    Else
                        entranceVoucher = New EntranceVoucher
                    End If
                    AsyncLoader(False)
                End If
            End Using
        Catch ex As Exception
            Me.Deshacer()
            AsyncLoader(False)
            INDBtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Método utilizado para Guardar/Actualziar y Confirmar el comprobante de entrada
    ''' </summary>
    ''' <param name="actions"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function SaveOrUpdateAndConfirm(actions As Integer, Optional controlCost As Boolean = True) As Task
        Dim errors = ValidateControls()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Function
        End If

        AssigningValues()
        AsyncLoader(True)
        Try
            Using model As New MEntranceVoucher(MyTag)
                Dim result = Await model.SaveAndConfirmEntranceVoucher(entranceVoucher, _idCurrentSequence, actions, Me._sequence, controlCost)
                If result.StateResult = True And result.StateResultAux = True Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    entranceVoucher = result.ObjectEmbbeded
                    'Actualizar el consecutivo de la cuenta por pagar en el control
                    If entranceVoucher.AccountPayableId IsNot Nothing AndAlso entranceVoucher.AccountPayableId > 0 Then
                        Using modelPayments As New MAccountPayable(Me.Tag)
                            Dim accountPayable = modelPayments.GetAccountPayableById(entranceVoucher.AccountPayableId)
                            If accountPayable IsNot Nothing Then
                                ctrTmp.AccountPayableConsecutive = "CxP: " + accountPayable.Code
                            Else
                                ctrTmp.AccountPayableConsecutive = "CxP: " + entranceVoucher.AccountPayableId.ToString()
                            End If
                        End Using
                    End If
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    If result.MessageResultAux IsNot Nothing AndAlso result.MessageResultAux.Count > 0 Then
                        ViewMessageValidationStock(result.MessageResultAux)
                    End If
                    Me.BarraBotones.PrintReport(PrintReportAction.Confirm, entranceVoucher.Id, 0, entranceVoucher.Id)
                    AsyncLoader(False)
                    Me.Deshacer()
                ElseIf result.StateResult = True And result.StateResultAux = False Then
                    If entranceVoucher.Id > 0 Or (result.ObjectEmbbeded IsNot Nothing And result.ObjectEmbbeded.Id > 0) Then
                        Dim res = Await model.GetEntranceVoucher(result.ObjectEmbbeded.Code)
                        Code = result.ObjectEmbbeded.Code
                        entranceVoucher = res.ObjectEmbbeded
                        RefrescarRejilla()
                        ListEntranceVoucherDetailDelete = New List(Of EntranceVoucherDetail)
                    Else
                        entranceVoucher.MarkAsAdded
                    End If

                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 AndAlso result.MessageResult.First().ToString() <> "" Then
                        If result.MessageResult.First().Contains("porcentaje de variación") Then
                            _action = actions
                            Using formulario As New FrmNotificationItemDetailConfirm
                                AddHandler formulario.AcceptMessage, AddressOf FrmNotificationItemDetailConfirm_Accept
                                formulario.TxtMessage.Text = String.Join(vbCrLf, result.MessageResult) & vbCrLf & "¿Desea continuar confirmando este registro?"
                                formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                                Dim transparent = New Base.FrmTransparent(formulario, False)
                                transparent.ShowDialog(Me)
                                If formulario.IsClosedWithAcept = False Then
                                    AsyncLoader(False)
                                End If
                            End Using
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = String.Join(vbCrLf, result.MessageResult)
                            Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, entranceVoucher.Id, 0, entranceVoucher.Id)
                            AsyncLoader(False)
                            Me.Deshacer()
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        Me.BarraBotones.PrintReport(PrintReportAction.Confirm, entranceVoucher.Id, 0, entranceVoucher.Id)
                        AsyncLoader(False)
                        Me.Deshacer()
                    End If
                ElseIf result.StateResult = False And result.StateResultAux = False Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    AsyncLoader(False)
                    INDBtnCode.Enabled = False
                End If
            End Using
        Catch ex As Exception
            Me.Deshacer()
            AsyncLoader(False)
            INDBtnCode.Enabled = False
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Método asignado de la barra de botones que despliega el formulario de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 100},
                              New ColumnInfo With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = 180},
                              New ColumnInfo With {.Caption = "Proveedor", .FieldName = "SupplierId.CodeName", .ColumnWidth = 180},
                              New ColumnInfo With {.Caption = "Almacen", .FieldName = "WarehouseId.CodeName", .ColumnWidth = 180},
                              New ColumnInfo With {.Caption = "Descripción", .FieldName = "Description", .ColumnWidth = 180},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = 80}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListEntranceVoucher
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub
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
    ''' Evento de la barra de botones que deshacer los cambios en el form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Evento de la barra de botnes que abre el formulario de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Evento de la barra de botones que inicia un nuevo registro
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Await NewEntranceVoucher()
    End Sub

    ''' <summary>
    ''' Evento de la barra de botones que Guarda
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar, BarraBotones.ClickActualizar
        BarraBotones.Focus()
        Guardar()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, entranceVoucher.Id, 0, entranceVoucher.Id)
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
    ''' Evento de la barra de botones que Guarda y Confirma el Documento
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        BarraBotones.Focus()
        Await SaveOrUpdateAndConfirm(1)
    End Sub

    ''' <summary>
    ''' Evento de la barra de botones que actualiza y Confirma el Documento
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        BarraBotones.Focus()
        Await SaveOrUpdateAndConfirm(2)
    End Sub

    ''' <summary>
    ''' Evento de la barra de btones que Anula el documento
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        BarraBotones.Focus()
        Anular()
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Método que elimina un compromiso de la rejilla
    ''' </summary>
    Private Sub DeleteCommitment()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim entity As EntranceVoucherCommitment = INDviewGridCommitment.GetFocusedRow()
        ListEntranceVoucherCommitment.Remove(entity)

        If entity.Id > 0 Then
            If ListDeleteEntranceVoucherCommitment Is Nothing Then
                ListDeleteEntranceVoucherCommitment = New List(Of EntranceVoucherCommitment)
            End If
            entity.MarkAsDeleted()
            ListDeleteEntranceVoucherCommitment.Add(entity)
        End If

        INDgcCommitment.DataSource = Nothing
        INDgcCommitment.DataSource = ListEntranceVoucherCommitment
    End Sub

    ''' <summary>
    ''' Método que muestra u oculta el campo de Actividad Económica
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub ShowEconomicActivity()
        Using model As New MCompanySettings(Tag)
            Dim companySettings As CompanySettings = Await model.GetCompanySettings
            IsEconomicActivity = If(companySettings?.TransactionEconomicActivity, False)
            If IsEconomicActivity Then
                ShowLayout(INDLyIncomeGeneratingEconomicActivity)
            Else
                HideLayout(INDLyIncomeGeneratingEconomicActivity)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Valida los controles del popup de presupuesto
    ''' </summary>
    Private Function ValidateControlsPopup() As Boolean
        Dim errors As New StringBuilder

        If INDsleBudgetaryEntity.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar una entidad presupuestal")
        End If

        If INDsleBudgetaryValidity.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar una vigencia")
        End If

        If INDsleCommitmentDetail.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar un compromiso")
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        Return True
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
    ''' Carga el tercero de la empresa actualmente seleccionada
    ''' </summary>
    ''' <returns>Valor que indica si la empresa se cargo</returns>
    Private Async Function LoadCurrentCompany() As Task
        Using model As New MEntranceVoucher(MyTag)
            Me._currentCompany = Await model.GetThirdPartyAsync(Me.indigo.IndigoCompanyNit)
        End Using
    End Function

    Private Async Function LoadIcaPercentage(ByVal supplier As Supplier, ByVal supplierDistributionLineId As Integer, ByVal operatingUnitId As Integer) As Task
        If Me.entranceVoucher IsNot Nothing AndAlso Me.entranceVoucher.Status = 1 Then
            IcaPercentage = 0
            If supplier.ThirdParty.RetentionType = 2 AndAlso supplier.ThirdParty.Ica AndAlso Not supplier.SelfWithholdingICA Then
                Using Model As New MEntranceVoucher(Me.MyTag)
                    _icaRetentionConcept = Await Model.GetICARetentionConceptBySupplierDistributionLine(supplierDistributionLineId, operatingUnitId)
                    If _icaRetentionConcept IsNot Nothing Then
                        IcaPercentage = _icaRetentionConcept.Rate
                    Else
                        IcaPercentage = supplier.ThirdParty.IcaPercentage
                    End If
                End Using
            End If
        End If
    End Function

    ''' <summary>
    ''' Método que elimina el registro guardado para concurrencia
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
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.entranceVoucher.Code, Me.entranceVoucher.InvoiceNumber),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.entranceVoucher.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.entranceVoucher.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.entranceVoucher.Code, Me.entranceVoucher.InvoiceNumber)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.entranceVoucher.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Devuelve el listado para mostrar el Total del contrato y sus derivados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfoServiceOrder() As Tuple(Of String, String, String, String)
        Return New Tuple(Of String, String, String, String)(NetoWithFreight, ValueDiscount, TaxWithFreight, InvoiceWithFreight)
    End Function

    ''' <summary>
    ''' Limpia los controles y las variables
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyEntranceVoucher.BeginUpdate()
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        BarraBotones.ReassignOperatingUnit()
        Me.ValidateDate()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True

        entranceVoucher = Nothing
        Status = 0
        Code = String.Empty
        DocumentDate = Nothing
        SupplierId = Nothing
        SupplierDistributionLineId = Nothing
        SupplierTypeId = Nothing
        WarehouseId = Nothing
        AccountPayableId = Nothing
        ctrTmp.AccountPayableConsecutive = ""
        Descripcion = Nothing
        ResourceType = Nothing
        IcaPercentage = 0D
        INDsleCommitmentDetail.Properties.NullText = String.Empty
        InvoiceNumber = String.Empty
        InvoiceDate = Nothing
        INDLyItemHandleDocumentSupport.HideControl(True)
        INDSleHandleDocumentSupport.EditValue = False
        INDSleDocumentSupportId.EditValue = Nothing
        INDSleDocumentSupportId.Properties.NullText = String.Empty
        DayPeriod = 1
        FreightValue = 0
        FreightIVAValue = 0
        FreightIVAPercentage = If(_settingInventory Is Nothing, 0, _settingInventory.IvaFreigthPercentage)
        Value = Nothing
        WithholdingTax = Nothing
        WithholdingICA = Nothing
        RetentionSource = Nothing
        RetentionOther = Nothing
        DeductionOther = Nothing
        DistrictTax = Nothing
        ValueDiscount = 0
        ValueTax = 0
        TotalValue = 0
        indexEditRecord = 0
        SupplierTypeId = Nothing
        InvoiceValue = 0
        NetoValue = 0
        Me.CurrencyDatasource = Nothing
        Me.CurrencyId(Me.indigo.CurrencyISO4217) = Me.indigo.OfficialCurrencyId
        RoundService = 1

        INDSleSupplier.Properties.NullText = String.Empty
        INDSleWarehouse.Properties.NullText = String.Empty
        INDsleSupplierType.Properties.NullText = String.Empty

        flagLoad = False
        OnlyRead = False
        ReadOnlyControls(False)
        ReadOnlyMonetaryInfo()
        INDSpnDayPeriod.Properties.ReadOnly = True
        INDSpnFreightIVA.Properties.ReadOnly = True
        INDSpnIcaPercentage.Properties.ReadOnly = True
        ItemEntranceVoucherDetail = Nothing

        ListEntranceVoucherDetailDelete = Nothing
        ClearOtherRetentionDeductions()

        EconomicActivityId = Nothing
        ListEntranceVoucherCommitment = Nothing
        ListDeleteEntranceVoucherCommitment = Nothing
        INDgcCommitment.DataSource = Nothing
        INDsleCommitmentDetail.EditValue = Nothing
        INDsleCommitmentDetail.Properties.DataSource = Nothing

        INDGcProducts.DataSource = Nothing
        ActionsOnControls = False
        SupplierTypeXpo = Nothing
        ctrTmp.PrintInfo()
        INDBtnCode.Focus()

        INDGleRoundService.Enabled = True

        INDlyEntranceVoucher.EndUpdate()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        ShowIcaPercentage()
        ShowRoundService()
    End Sub

    ''' <summary>
    ''' Genera una nueva entidad de comprobante de entrada
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function NewEntranceVoucher() As Task
        If Me._currentCompany Is Nothing Then 'Si la empresa actual no existe como tercero, se debe crear
            Me.Mensaje(EeventViewerImages.Advertencia) = "No se ha podido cargar el tercero de la empresa (" & Me.indigo.IndigoCompanyNit & ") actualmente seleccionada. Verifique que haya sido creada e intente de nuevo."
            Exit Function
        End If
        If Me._settingInventory Is Nothing OrElse Me._settingInventory.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", NAME_MODULE)
            Exit Function
        End If
        entranceVoucher = New Domain.Entities.EntranceVoucher With {.Status = 1}
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
        Me.BarraBotones.ControlHideStatus = False
        If ListDeduction.Count > 0 Then
            LyGroupOtherDeduction.Visibility = XtraLayout.Utils.LayoutVisibility.Always
        End If
        If ListRetention.Count > 0 Then
            LyGroupOtherRetention.Visibility = XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Function

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
    ''' Obtiene o establece el proovedor 
    ''' </summary>
    Private Async Function GetSupplier() As Task(Of Integer)
        Using modelSupplier As New MSupplier(MyTag)
            _supplier = modelSupplier.GetSupplierById(entranceVoucher.SupplierId)
        End Using
        SupplierId = entranceVoucher.SupplierId
        Return Await Task.FromResult(Of Integer)(0)
    End Function

    ''' <summary>
    ''' Método que carga los controles de la consulta del contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Task
        If Me._currentCompany Is Nothing Then 'Si la empresa actual no existe como tercero, se debe crear
            Me.Mensaje(EeventViewerImages.Advertencia) = "No se ha podido cargar el tercero de la empresa (" & Me.indigo.IndigoCompanyNit & ") actualmente seleccionada. Verifique que haya sido creada e intente de nuevo."
            Exit Function
        End If
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MEntranceVoucher(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim tmpEntranceVoucher = (Await Model.GetEntranceVoucher(INDBtnCode.Text.Trim)).ObjectEmbbeded

                    INDlyEntranceVoucher.BeginUpdate()
                    If tmpEntranceVoucher IsNot Nothing AndAlso tmpEntranceVoucher.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        'Cargar unidad operativa
                        BarraBotones_ChangueOperatingUnit(New OperatingUnit With {.Id = tmpEntranceVoucher.OperatingUnitId})
                        entranceVoucher = tmpEntranceVoucher
                        BarraBotones_ChangueOperatingUnit(New OperatingUnit With {.Id = Me.BarraBotones.OperatingUnit.Id})

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(entranceVoucher.Id))
                            flagLoad = True

                            With entranceVoucher
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Await GetSupplier()

                                Code = .Code
                                DocumentDate = .DocumentDate
                                SupplierId = .SupplierId
                                INDSleSupplier.Properties.NullText = .DescriptionSupplier
                                SupplierDistributionLineId = .SupplierDistributionLineId
                                INDsleSupplierType.SupplierId = SupplierId
                                INDsleSupplierType.SetDefault = New SupplierType With {.Id = entranceVoucher.SupplierTypeId, .CodeName = entranceVoucher.DescriptionSupplierType}
                                SupplierTypeId = .SupplierTypeId
                                WarehouseId = .WarehouseId
                                INDSleWarehouse.Properties.NullText = .DescriptionWarehouse
                                EconomicActivityId = .EconomicActivityId
                                AccountPayableId = .AccountPayableId
                                'Actualizar el consecutivo de cuenta por pagar en el control
                                If .AccountPayableId IsNot Nothing AndAlso .AccountPayableId > 0 Then
                                    Using modelPayments As New MAccountPayable(Me.Tag)
                                        Dim accountPayable = modelPayments.GetAccountPayableById(.AccountPayableId)
                                        If accountPayable IsNot Nothing Then
                                            ctrTmp.AccountPayableConsecutive = "CxP: " + accountPayable.Code
                                        Else
                                            ctrTmp.AccountPayableConsecutive = "CxP: " + .AccountPayableId.ToString()
                                        End If
                                    End Using
                                Else
                                    ctrTmp.AccountPayableConsecutive = ""
                                End If
                                Descripcion = .Description
                                ResourceType = .ResourceType
                                IcaPercentage = .IcaPercentage
                                InvoiceNumber = .InvoiceNumber
                                InvoiceDate = .InvoiceDate
                                DayPeriod = .DayPeriod
                                FreightValue = .FreightValue
                                FreightIVAPercentage = .FreightIVAPercentage
                                FreightIVAValue = .FreightIVAValue
                                Value = .Value
                                ValueDiscount = .ValueDiscount
                                ValueTax = .ValueTax
                                WithholdingTax = .WithholdingTax
                                WithholdingICA = .WithholdingICA
                                RetentionSource = .RetentionSource
                                RetentionOther = .RetentionOther
                                DeductionOther = .DeductionOther
                                DistrictTax = .DistrictTax
                                TotalValue = .TotalValue
                                NetoValue = entranceVoucher.EntranceVoucherDetail.Sum(Function(x) x.NetoValue)
                                RoundService = .RoundService
                                Me.Status = .Status
                                Me.TaxRegistration = .TaxRegistration
                                Me.CurrencyId(.Currency?.Abbreviation) = .CurrencyId
                                Await Me.ValidateCurrencyEditValue(False, Me.CurrencyId)
                                ShowRoundService()
                                ShowIcaPercentage()
                                If entranceVoucher.Status = 1 Then
                                    INDSleHandleDocumentSupport.EditValue = (_supplier.ThirdParty.ElectronicBiller = False AndAlso entranceVoucher.DocumentSupportId IsNot Nothing)
                                    INDSleDocumentSupportId.EditValue = If(_supplier.ThirdParty.ElectronicBiller = False, entranceVoucher.DocumentSupportId, Nothing)
                                    INDSleDocumentSupportId.Properties.NullText = If(_supplier.ThirdParty.ElectronicBiller = False, entranceVoucher.DescriptionDocumentSupport, Nothing)
                                Else
                                    INDSleHandleDocumentSupport.EditValue = If(entranceVoucher.DocumentSupportId Is Nothing, False, True)
                                    INDSleDocumentSupportId.EditValue = entranceVoucher.DocumentSupportId
                                    INDSleDocumentSupportId.Properties.NullText = entranceVoucher.DescriptionDocumentSupport
                                End If

                                If _generalLedgerSettings?.HandlesSupportDocument Then
                                    INDLyItemHandleDocumentSupport.HideControl(If(_supplier.ThirdParty.ElectronicBiller = False And entranceVoucher.DocumentSupportId IsNot Nothing, False, True))
                                    INDLyItemDocumentSupportId.HideControl(If(_supplier.ThirdParty.ElectronicBiller = False And entranceVoucher.DocumentSupportId IsNot Nothing, False, True))
                                End If

                                RefrescarRejilla()
                                Await LoadIcaPercentage(_supplier, .SupplierDistributionLineId, .OperatingUnitId)

                                If .EntranceVoucherOtherDeduction.Count > 0 Then
                                    For Each item In .EntranceVoucherOtherDeduction
                                        If ListRetention.Count > 0 Then
                                            LyGroupOtherRetention.Visibility = XtraLayout.Utils.LayoutVisibility.Always
                                            For Each ret In ListRetention
                                                If ret.Id = item.OtherWithholdingDeductionId Then
                                                    ret.Selected = True
                                                    item.WithholdingPercentage = ret.RetentionPercentage
                                                End If
                                            Next
                                        End If
                                        If ListDeduction.Count > 0 Then
                                            LyGroupOtherDeduction.Visibility = XtraLayout.Utils.LayoutVisibility.Always
                                            For Each ded In ListDeduction
                                                If ded.Id = item.OtherWithholdingDeductionId Then
                                                    ded.Selected = True
                                                End If
                                            Next
                                        End If
                                    Next
                                ElseIf .Status = 1 Then
                                    If ListDeduction.Count > 0 Then
                                        LyGroupOtherDeduction.Visibility = XtraLayout.Utils.LayoutVisibility.Always
                                    End If
                                    If ListRetention.Count > 0 Then
                                        LyGroupOtherRetention.Visibility = XtraLayout.Utils.LayoutVisibility.Always
                                    End If
                                End If

                                ListEntranceVoucherCommitment = .EntranceVoucherCommitment.ToList()
                                INDgcCommitment.DataSource = Nothing
                                INDgcCommitment.DataSource = ListEntranceVoucherCommitment

                                Await CalculateRetentions()
                            End With

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.entranceVoucher.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = entranceVoucher.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            If Status = 1 Then
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                                Me.ReadOnlyControls(False)
                                INDSpnDayPeriod.Properties.ReadOnly = True
                                INDSpnFreightIVA.Properties.ReadOnly = True
                                INDSpnIcaPercentage.Properties.ReadOnly = True
                                OnlyRead = False
                                INDBtnAddProducts.Enabled = True
                            Else
                                INDBtnAddProducts.Enabled = False
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                Me.ReadOnlyControls(True)
                                OnlyRead = True
                            End If
                            Me.BarraBotones.SetDocuments(entranceVoucher.Id, Me.Tag.ToString(), Nothing, GetType(EntranceVoucher).Name)
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, entranceVoucher.Id, 0, entranceVoucher.Id)

                            AsyncLoader(False)
                            ActionsOnControls = True
                            INDDteDocumentDate.Focus()
                            flagLoad = False
                            Me.EnableDisableCurrency()
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewEntranceVoucher()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBtnCode.Focus()
                        End If
                    End If
                    INDlyEntranceVoucher.EndUpdate()
                End Using
                ReadOnlyMonetaryInfo()
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
    Private Sub InstantiatePopup()
        Me.Cursor = ChangeCursorIndigo()
        Using formulario As New PopUpProductsEntranceVoucher(RoundService,
                                                             _currency:=New Currency With {.Id = Me.CurrencyId, .Abbreviation = Me.CurrencyAbbreviation},
                                                             _tRMValue:=If(Me.TRM Is Nothing, 1, Me.TRM.Value))
            AddHandler formulario.AddEntranceVoucherDetail, AddressOf ReturnPopupAddProduct

            formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            formulario.ViewModeEditHold = True
            formulario.StartPosition = FormStartPosition.CenterParent
            formulario.SupplierId = SupplierId
            formulario.WareHouseId = WarehouseId
            formulario.TaxRegistration = TaxRegistration
            formulario.operatingUnitId = BarraBotones.OperatingUnitValue
            formulario.Declarant = _supplier.Declarant
            formulario.ListEntranceVoucherDetailValidation = entranceVoucher.EntranceVoucherDetail.ToList()
            formulario.Size = New Size(1100, 780)
            Dim transparent = New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método utilizado para adquirir los productos retornados por el frontal de agregar productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnPopupAddProduct(sender As Object, e As AddProductEntranceVoucherDetailEventArgs)

        If e.EditMode Then
            entranceVoucher.EntranceVoucherDetail.Remove(ItemEntranceVoucherDetail)
            entranceVoucher.EntranceVoucherDetail.Insert(indexEditRecord, e.ItemEntranceVoucherDetail)

        ElseIf e.ImportDataMode Then
            For Each evd In e.ListEntranceVoucherDetail
                entranceVoucher.EntranceVoucherDetail.Add(evd)
            Next
        Else
            entranceVoucher.EntranceVoucherDetail.Add(e.ItemEntranceVoucherDetail)
        End If

        If entranceVoucher.EntranceVoucherDetail.Any() Then
            INDGleRoundService.Enabled = False
        Else
            INDGleRoundService.Enabled = True
        End If

        INDSleSupplier.Properties.ReadOnly = True
        INDSleWarehouse.Properties.ReadOnly = True
        RefrescarRejilla()

        Me.EnableDisableCurrency()
        CalculateRetentions()
        INDGvProducts.ExpandAllGroups()
    End Sub

    ''' <summary>
    ''' Carga la rejilla de presupuesto con la información que viene desde el importar
    ''' </summary>
    Public Sub LoadDatasourceCommitments(entranceSource As Integer, list As List(Of EntranceVoucherDetail))
        CheckForIllegalCrossThreadCalls = False

        If list IsNot Nothing AndAlso list.Count > 0 Then
            Dim listXpo = Presenter.GetListCommitmentBySourceCodeAndEntityName(list, entranceSource)

            If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
                If ListEntranceVoucherCommitment Is Nothing Then
                    ListEntranceVoucherCommitment = New List(Of EntranceVoucherCommitment)
                End If

                For Each itemXpo In listXpo
                    If (From x In ListEntranceVoucherCommitment Where x.CommitmentDetailId = itemXpo.CommitmentDetailId Select x).Count > 0 Then
                        Continue For
                    End If

                    Dim entity As New EntranceVoucherCommitment
                    entity.CommitmentDetailId = itemXpo.CommitmentDetailId
                    entity.CommitmentCode = itemXpo.Code
                    entity.CommitmentDocument = itemXpo.Document
                    entity.CategoryCodeName = itemXpo.CategoryCodeName
                    entity.FinancialSourceCodeName = itemXpo.FinancialSourceCodeName
                    entity.RevenueTypeCodeName = itemXpo.RevenueTypeCodeName
                    entity.Value = 0
                    entity.Balance = itemXpo.Balance
                    ListEntranceVoucherCommitment.Add(entity)
                Next
                INDgcCommitment.DataSource = Nothing
                INDgcCommitment.DataSource = ListEntranceVoucherCommitment
                INDgcCommitment.RefreshDataSource()
            End If

            INDviewGridCommitment.HideLoadingPanel()
        End If
    End Sub

    ''' <summary>
    ''' Método utilizado para cargar los parametros
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadParameters() As Task
        Using Model As New MEntranceVoucher(Me.MyTag)
            _settingInventory = Await Model.GetSettingInventory(_idOperativeUnit)

            If _settingInventory Is Nothing Then
                Me.TaxRegistration = Nothing
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", NAME_MODULE)
                Deshacer()
                Exit Function
            End If

            FreightIVAPercentage = _settingInventory.IvaFreigthPercentage
            Me.TaxRegistration = _settingInventory?.TaxRegistration

            If _settingInventory?.TaxRegistration <> 3 Then
                INDLciTaxRegistration.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            End If

            Me.ValidateDate()
        End Using

        Using modelGeneralLedgerSettings As New MSettingsAccount(MyTag)
            _generalLedgerSettings = Await modelGeneralLedgerSettings.GetSettingAccount(_idOperativeUnit)

            If _generalLedgerSettings Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "No se logró cargar los parametros de contabilidad"
                Deshacer()
                Exit Function
            End If
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
    ''' asigna los valroes a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With entranceVoucher
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .OperatingUnitId = Me.BarraBotones.OperatingUnitValue
            .DocumentDate = DocumentDate
            .SupplierId = SupplierId
            .SupplierDistributionLineId = SupplierDistributionLineId
            .SupplierTypeId = SupplierTypeId
            .WarehouseId = WarehouseId
            .Description = Descripcion
            .ResourceType = ResourceType
            .RoundService = RoundService
            .IcaPercentage = IcaPercentage
            .InvoiceNumber = InvoiceNumber
            .Prefix = Me._prefixSelected
            .InvoiceDate = InvoiceDate
            .DocumentSupportId = INDSleDocumentSupportId.EditValue
            .DayPeriod = DayPeriod
            .FreightValue = FreightValue
            .FreightValueOutstanding = FreightValue
            .FreightIVAPercentage = FreightIVAPercentage
            .FreightIVAValue = FreightIVAValue
            .FreightIVAValueOutstanding = FreightIVAValue
            .Value = Value
            .ValueDiscount = ValueDiscount
            .ValueTax = ValueTax
            .WithholdingTax = WithholdingTax
            .WithholdingICA = WithholdingICA
            .RetentionSource = RetentionSource
            .RetentionOther = RetentionOther
            .DeductionOther = DeductionOther
            .DistrictTax = DistrictTax
            .TotalValue = TotalValue
            .Status = 1
            .EconomicActivityId = EconomicActivityId
            If INDLciTaxRegistration.Visibility = XtraLayout.Utils.LayoutVisibility.Never AndAlso _settingInventory IsNot Nothing And _settingInventory?.TaxRegistration <> 3 Then
                .TaxRegistration = _settingInventory?.TaxRegistration
            Else
                .TaxRegistration = Me.TaxRegistration
            End If
            .CurrencyId = Me.CurrencyId
            If ListEntranceVoucherCommitment IsNot Nothing AndAlso ListEntranceVoucherCommitment.Count > 0 Then
                ListEntranceVoucherCommitment.ForEach(Sub(item) .EntranceVoucherCommitment.Add(item))
            End If

            If ListDeleteEntranceVoucherCommitment IsNot Nothing AndAlso ListDeleteEntranceVoucherCommitment.Count > 0 Then
                ListDeleteEntranceVoucherCommitment.ForEach(Sub(item) .EntranceVoucherCommitment.Add(item))
            End If
        End With
    End Sub

    ''' <summary>
    ''' Método que retorna los registros importados al formulario de agregar productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnGetListEntranceVoucherDetail(sender As Object, e As GetListEntranceVoucherDetailEventArgs)
        Dim errors As New StringBuilder

        Dim listEntranceVoucherDetailHandlesBatch As New List(Of EntranceVoucherDetail)
        Dim listEntranceVoucherDetailNotHandlesBatch As New List(Of EntranceVoucherDetail)
        Dim dictionaryProduct As Dictionary(Of Integer, Domain.Entities.InventoryProduct) = New Dictionary(Of Integer, InventoryProduct)()
        Dim dictionaryAccountPayableConcept As Dictionary(Of Integer, PaymentsAccountPayableConceptsXpoP) = New Dictionary(Of Integer, PaymentsAccountPayableConceptsXpoP)()
        Dim dictionaryRetentionConcept As Dictionary(Of Integer, GeneralLedgerRetentionConceptsReportXpo) = New Dictionary(Of Integer, GeneralLedgerRetentionConceptsReportXpo)()

        Dim product As InventoryProduct = Nothing
        Dim accountPayableConcept As PaymentsAccountPayableConceptsXpoP = Nothing
        Dim retentionConcept As GeneralLedgerRetentionConceptsReportXpo = Nothing

        If e.EntranceSource = 2 OrElse e.EntranceSource = 3 Then
            INDviewGridCommitment.ShowLoadingPanel()
            Task.Factory.StartNew(Sub() LoadDatasourceCommitments(e.EntranceSource, e.ListEntranceVoucherDetail))
        End If

        For Each Item In e.ListEntranceVoucherDetail
            If Not dictionaryProduct.ContainsKey(Item.ProductId) Then
                Using model As New MInventoryProduct(Me.Tag)
                    Dim productTmp = model.GetInventoryProductByIdSimpleToGroup(Item.ProductId)
                    dictionaryProduct.Add(Item.ProductId, productTmp)
                End Using
            End If

            product = dictionaryProduct(Item.ProductId)
            If product.ProductGroup Is Nothing Then
                errors.AppendLine(String.Format(ResourceManager.GetString("ProductWithoutGroup", NAME_MODULE), product.Code + " - " + product.Name))
                Continue For
            End If

            If product.FinalProductCost Is Nothing OrElse product.FinalProductCost = 0 Then
                errors.AppendLine(String.Format(ResourceManager.GetString("FinalProductCostZero", NAME_MODULE), product.Code + " - " + product.Name))
                Continue For
            End If

            If product.ProductGroup IsNot Nothing Then
                Dim accountPayableConceptId = If(_supplier.Declarant, product.ProductGroup.DeclarantRetentionAccountPayableConceptId, product.ProductGroup.NotDeclarantRetentionAccountPayableConceptId)
                If Not dictionaryAccountPayableConcept.ContainsKey(accountPayableConceptId) Then
                    accountPayableConcept = Presenter.GetAccountPayableConceptById(accountPayableConceptId)
                    dictionaryAccountPayableConcept.Add(accountPayableConceptId, accountPayableConcept)
                End If

                accountPayableConcept = dictionaryAccountPayableConcept(accountPayableConceptId)
                If accountPayableConcept IsNot Nothing Then
                    If Not dictionaryRetentionConcept.ContainsKey(accountPayableConcept.RetentionConceptId) Then
                        retentionConcept = Presenter.GetRetentionConceptById(accountPayableConcept.RetentionConceptId)
                        dictionaryRetentionConcept.Add(accountPayableConcept.RetentionConceptId, retentionConcept)
                    End If

                    retentionConcept = dictionaryRetentionConcept(accountPayableConcept.RetentionConceptId)
                    If retentionConcept IsNot Nothing Then
                        Item.MinBase = retentionConcept.MinBase
                        Item.Rate = retentionConcept.Rate
                        Item.TypeRounding = retentionConcept.TypeRounding
                    End If
                End If
            End If

            Item.ProductCode = product.Code
            Item.ProductName = product.Name
            Item.ProductCodeName = product.Code + " - " + product.Name
            Item.ManufacturerName = product.ManufacturerDescription
            Item.HealthRegistration = product.HealthRegistration
            Item.Presentation = product.Presentation

            If Item.UnitValue = 0 Then
                Item.UnitValue = product.FinalProductCost
            Else
                Item.UnitValue = Item.UnitValue
            End If

            If product.IVAId IsNot Nothing Then
                Using modelIva As New MGeneralLedgerIVA(Me.Tag)
                    Dim iva = modelIva.GetGeneralLedgerIVAById(product.IVAId)
                    Item.IvaPercentage = iva.ObjectEmbbeded.Percentage
                End Using
            End If

            Item.SubTotalValue = Utils.RoundValue(Item.UnitValue * Item.Quantity, RoundService.Value) 'valor subtotal
            Item.DiscountValue = Utils.RoundValue(Item.SubTotalValue * Item.DiscountPercentage / 100, RoundService.Value)  'valor descuento
            Item.NetoValue = Item.SubTotalValue - Item.DiscountValue 'valor neto
            Item.IvaValue = Utils.RoundValue(CDec(Item.NetoValue * (Item.IvaPercentage / 100)), RoundService.Value) 'valor iva
            Item.TotalValue = Item.NetoValue + Item.IvaValue 'valot total
            Item.RTFPercentage = 0
            Item.RTFValue = 0

            If product.ProductSubGroup.HandlesBatch Then
                listEntranceVoucherDetailHandlesBatch.Add(Item)
            Else
                If Not Item.EntranceVoucherDetailBatchSerial.Any() Then
                    'Agregamos el lote así no tenga, para la modificacion de las cantidades en la devolucion
                    Dim entranceVoucherDetailBatchSerial As New EntranceVoucherDetailBatchSerial
                    entranceVoucherDetailBatchSerial.Quantity = Item.Quantity
                    entranceVoucherDetailBatchSerial.OutstandingQuantity = Item.Quantity
                    Item.EntranceVoucherDetailBatchSerial.Add(entranceVoucherDetailBatchSerial)
                End If
                listEntranceVoucherDetailNotHandlesBatch.Add(Item)
            End If
        Next

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString
        End If

        'agregamos los productos que no manejen lote a la rejilla directamente
        If listEntranceVoucherDetailNotHandlesBatch IsNot Nothing AndAlso listEntranceVoucherDetailNotHandlesBatch.Count > 0 Then
            Dim args As New AddProductEntranceVoucherDetailEventArgs
            args.ImportDataMode = True
            args.ListEntranceVoucherDetail = listEntranceVoucherDetailNotHandlesBatch
            ReturnPopupAddProduct(Nothing, args)
        End If

        'abrimos el popup de agregar productos si el producto maneja lote
        If listEntranceVoucherDetailHandlesBatch IsNot Nothing AndAlso listEntranceVoucherDetailHandlesBatch.Count > 0 Then
            Using formulario As New PopUpProductsEntranceVoucher(RoundService,
                                                                 _currency:=New Currency With {.Id = Me.CurrencyId, .Abbreviation = Me.CurrencyAbbreviation},
                                                                _tRMValue:=If(Me.TRM Is Nothing, 1, Me.TRM.Value))
                Me.Cursor = ChangeCursorIndigo()
                AddHandler formulario.AddEntranceVoucherDetail, AddressOf ReturnPopupAddProduct
                formulario.Size = New System.Drawing.Size(800, 750)
                formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                formulario.ListEntranceVoucherDetailImportInfo = listEntranceVoucherDetailHandlesBatch
                formulario.ListEntranceVoucherDetailValidation = entranceVoucher.EntranceVoucherDetail.ToList()
                formulario.WareHouseId = WarehouseId
                formulario.TaxRegistration = TaxRegistration
                formulario.operatingUnitId = BarraBotones.OperatingUnitValue
                formulario.ImportDataMode = True
                formulario.Declarant = _supplier.Declarant
                Dim transparent = New Base.FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' valida los controles del formulario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControls() As String
        Dim errors As New StringBuilder

        If SupplierDistributionLineId Is Nothing Then
            errors.AppendLine(INDLySleSupplier.Text + ResourceManager.GetString("Empty"))
        End If

        If DocumentDate Is Nothing Then
            errors.AppendLine(INDLyDteDocumentDate.Text + ResourceManager.GetString("Empty"))
        Else
            If entranceVoucher.EntranceVoucherDetail IsNot Nothing AndAlso entranceVoucher.EntranceVoucherDetail.Any Then
                If entranceVoucher.EntranceVoucherDetail.Any(Function(d) d.EntranceSource = 2 AndAlso d.DeliveredDate IsNot Nothing) Then
                    Dim detail = entranceVoucher.EntranceVoucherDetail.Where(Function(d) d.EntranceSource = 2 AndAlso d.DeliveredDate IsNot Nothing).OrderBy(Function(d) d.DeliveredDate).FirstOrDefault()
                    If DocumentDate > detail.DeliveredDate Then
                        errors.AppendLine(String.Format("La fecha del documento no puede superar la fecha de entrega ({0}) de la orden de compra {1}", CDate(detail.DeliveredDate).ToString("yyyy-MM-dd"), detail.SourceCode))
                    End If
                End If
            End If
        End If

        If WarehouseId Is Nothing Then
            errors.AppendLine(INDLySleWarehouse.Text + ResourceManager.GetString("Empty"))
        End If

        If Descripcion Is Nothing OrElse Descripcion = String.Empty Then
            errors.AppendLine(INDLyTxtDescription.Text + ResourceManager.GetString("Empty"))
        End If

        If INDGleResourceType.EditValue Is Nothing Then
            errors.AppendLine(INDLyGleResourceType.Text + ResourceManager.GetString("Empty"))
        End If

        If RoundService Is Nothing Then
            errors.AppendLine(INDLyTxtRoundService.Text + ResourceManager.GetString("Empty"))
        End If

        If IcaPercentage Is Nothing Then
            errors.AppendLine(INDLySpnIcaPercentage.Text + ResourceManager.GetString("Empty"))
        End If

        If InvoiceNumber Is String.Empty Then
            errors.AppendLine(INDLyTxtInvoiceNumber.Text + ResourceManager.GetString("Empty"))
        End If

        If InvoiceDate Is Nothing Then
            errors.AppendLine(INDLyTxtInvoiceDate.Text + ResourceManager.GetString("Empty"))
        End If

        If INDSleHandleDocumentSupport.EditValue IsNot Nothing AndAlso INDSleHandleDocumentSupport.EditValue AndAlso INDSleDocumentSupportId.EditValue Is Nothing Then
            errors.AppendLine(INDLyItemDocumentSupportId.Text + ResourceManager.GetString("Empty"))
        End If

        If DayPeriod Is Nothing Then
            errors.AppendLine(INDLySpnDayPeriod.Text + ResourceManager.GetString("Empty"))
        End If

        If FreightValue Is Nothing Then
            errors.AppendLine(INDLyTxtFreightValue.Text + ResourceManager.GetString("Empty"))
        End If

        If IsEconomicActivity AndAlso EconomicActivityId Is Nothing Then
            errors.AppendLine("Se debe diligenciar el campo Actividad Económica Generadora de Ingreso")
        End If

        If INDGvProducts.RowCount = 0 Then
            errors.AppendLine(ResourceManager.GetString("AddEntranceVoucherDetail", NAME_MODULE))
        End If
        Return errors.ToString()
    End Function

    ''' <summary>
    ''' Método que consulta las retenciones y las deducciones del modulo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function GetOtherRentetionsAndDeductions() As Task
        Using Model As New MEntranceVoucher(Me.Tag)
            Dim resultOperation = Await Model.ListOtherRetentionAndDeduction()
            If resultOperation IsNot Nothing AndAlso resultOperation.ObjectEmbbeded IsNot Nothing AndAlso resultOperation.ObjectEmbbeded.Count > 0 Then
                For Each item In resultOperation.ObjectEmbbeded
                    If item.Type = 1 Then
                        ListRetention.Add(item)
                    Else
                        ListDeduction.Add(item)
                    End If
                Next

                INDGcOtherWithholding.DataSource = ListRetention
                INDGcOtherDeduction.DataSource = ListDeduction
            End If
        End Using
    End Function

    ''' <summary>
    ''' Método que limpia los items seleecionados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearOtherRetentionDeductions()
        LyGroupOtherRetention.Visibility = XtraLayout.Utils.LayoutVisibility.Never
        LyGroupOtherDeduction.Visibility = XtraLayout.Utils.LayoutVisibility.Never
        If ListRetention IsNot Nothing AndAlso ListDeduction IsNot Nothing Then
            If ListRetention.Count > 0 Then
                For Each item In ListRetention
                    item.Selected = False
                Next
            End If
            If ListDeduction.Count > 0 Then
                For Each item In ListDeduction
                    item.Selected = False
                Next
            End If

            INDGcOtherWithholding.RefreshDataSource()
            INDGcOtherDeduction.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Ajusta los controles de informacion monetaria para que no puedan ser modificados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadOnlyMonetaryInfo()
        INDTxtValue.Enabled = False
        INDTxtDiscountValue.Enabled = False
        INDTxtValueTax.Enabled = False
        INDTxtWithholdingTax.Enabled = False
        INDTxtWithholdingICA.Enabled = False
        INDTxtRetentionSource.Enabled = False
        INDTxtRetentionOther.Enabled = False
        INDTxtDeductionOther.Enabled = False
        INDTxtTotal.Properties.Enabled = False
    End Sub

    ''' <summary>
    ''' Se traslada la logica del dominio
    ''' </summary>
    Public Function CalculateRetentions() As Task(Of Integer)
        If Me.entranceVoucher Is Nothing OrElse {2, 3}.Contains(Me.entranceVoucher.Status) Then
            Me.CalculateTotalValue()
            Return Task.FromResult(Of Integer)(0)
        End If

        'Totales
        Me.Value = 0
        Me.ValueDiscount = 0
        Me.NetoValue = 0
        Me.ValueTax = 0
        'Retenciones
        Me.RetentionSource = 0
        Me.WithholdingICA = 0
        Me.WithholdingTax = 0
        'Otras Retenciones
        Me.DistrictTax = 0
        Me.RetentionOther = 0
        Me.DeductionOther = 0

        If Me.entranceVoucher.EntranceVoucherDetail.Count > 0 Then
            'Se verifica si el proveedor maneja IVA
            If _supplier?.NotIva Then
                For Each item In Me.entranceVoucher.EntranceVoucherDetail
                    item.IvaValue = 0
                    item.TotalValue = item.NetoValue
                Next
            End If

            Me.Value = Me.entranceVoucher.EntranceVoucherDetail.Sum(Function(x) x.SubTotalValue)
            Me.ValueDiscount = Me.entranceVoucher.EntranceVoucherDetail.Sum(Function(x) x.DiscountValue)
            Me.NetoValue = Me.entranceVoucher.EntranceVoucherDetail.Sum(Function(x) x.NetoValue)
            Me.ValueTax = Me.entranceVoucher.EntranceVoucherDetail.Sum(Function(x) x.IvaValue)

            If _supplier IsNot Nothing AndAlso _supplier.ThirdParty IsNot Nothing Then
                'Si el tercero del proveedor maneja retenciones
                If _supplier.ThirdParty.RetentionType = 2 Then
                    Dim errors As New StringBuilder()

                    'Dictionaries
                    Dim dictionaryAccountPayableConcepts As New Dictionary(Of Integer, Integer)()
                    Dim dictionaryRetentionConcepts As New Dictionary(Of Integer, GeneralLedgerRetentionConceptsReportXpo)()
                    'Items Individuals
                    Dim retentionConceptId As Integer = Nothing
                    Dim retentionConceptXpo As GeneralLedgerRetentionConceptsReportXpo = Nothing
                    'Valor acumulado retefuente base
                    Dim baseRtfItems As Decimal = 0

                    'Retefuente
                    If Not _supplier.SelfWithholding Then
                        For Each item In Me.entranceVoucher.EntranceVoucherDetail
                            item.RTFPercentage = 0
                            item.RTFValue = 0
                            If _supplier.PermanentRetention OrElse (NetoWithFreight >= item.MinBase) Then
                                Dim baseItem = (item.SubTotalValue - item.DiscountValue)
                                item.RTFPercentage = item.Rate
                                item.RTFValue = Utils.RoundValue(((item.SubTotalValue - item.DiscountValue) * item.Rate / 100), item.TypeRounding)
                                Me.RetentionSource += item.RTFValue
                                baseRtfItems += baseItem
                            End If
                        Next

                        'Retención del flete en proporción al valor de los items
                        If Me.FreightValue > 0 AndAlso baseRtfItems > 0 Then
                            Dim tasaEfectiva As Decimal = Me.RetentionSource / baseRtfItems
                            Dim rtfFleteRaw As Decimal = Me.FreightValue * tasaEfectiva
                            Dim rtfFlete As Decimal = Utils.RoundValue(rtfFleteRaw, 1)
                            Me.RetentionSource += rtfFlete
                        End If
                    End If

                    'ReteIca
                    If _supplier.ThirdParty.Ica AndAlso Not _supplier.SelfWithholdingICA Then
                        If IcaPercentage > 0 Then
                            Dim minBase = If(_supplier.ThirdParty.IcaTop, _supplier.ThirdParty.IcaTopValue, If(_icaRetentionConcept IsNot Nothing, _icaRetentionConcept.MinBase, 0))
                            Dim typeRounding = If(_icaRetentionConcept IsNot Nothing, _icaRetentionConcept.TypeRounding, 1)
                            If (NetoWithFreight >= minBase) Then
                                Me.WithholdingICA = Utils.RoundValue(CDec(NetoWithFreight * IcaPercentage / 100), typeRounding)
                            End If
                        End If
                    End If

                    'ReteIva
                    If Me.TaxWithFreight > 0 Then
                        If _supplier.ThirdParty.ContributionType > 0 AndAlso _currentCompany.ContributionType > _supplier.ThirdParty.ContributionType Then
                            If _settingInventory.IVARetention = 1 Then
                                'La retención se saca del tercero
                                If _supplier.ThirdParty.IVARetentionConceptId Is Nothing Then
                                    errors.AppendLine("El tercero no tiene parametrizado un concepto de retención de IVA")
                                Else
                                    retentionConceptId = _supplier.ThirdParty.IVARetentionConceptId
                                End If
                            Else
                                'La retención se saca del concepto de los parámetros
                                If Not dictionaryAccountPayableConcepts.ContainsKey(_settingInventory.IVARetentionAccountPayableConceptId) Then
                                    Dim accountPayableConceptXpo = Presenter.GetAccountPayableConceptById(_settingInventory.IVARetentionAccountPayableConceptId)
                                    retentionConceptId = accountPayableConceptXpo.RetentionConceptId
                                    dictionaryAccountPayableConcepts.Add(_settingInventory.IVARetentionAccountPayableConceptId, retentionConceptId)
                                Else
                                    retentionConceptId = dictionaryAccountPayableConcepts(_settingInventory.IVARetentionAccountPayableConceptId)
                                End If
                            End If

                            'Obtener el concepto de retención
                            If Not dictionaryRetentionConcepts.ContainsKey(retentionConceptId) Then
                                retentionConceptXpo = Presenter.GetRetentionConceptById(retentionConceptId)
                                dictionaryRetentionConcepts.Add(retentionConceptId, retentionConceptXpo)
                            Else
                                retentionConceptXpo = dictionaryRetentionConcepts(retentionConceptId)
                            End If

                            If retentionConceptXpo Is Nothing Then
                                errors.AppendLine("No se encuentra parametrizado un concepto de retención de IVA")
                            Else
                                If (Me.TaxWithFreight >= retentionConceptXpo.MinBase) Then
                                    Me.WithholdingTax = Utils.RoundValue((Me.TaxWithFreight * retentionConceptXpo.Rate / 100), retentionConceptXpo.TypeRounding)
                                End If
                            End If
                        End If
                    End If

                    If errors.Length > 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
                    End If
                End If
            End If
        End If

        'Otras retenciones
        If Me.entranceVoucher.EntranceVoucherOtherDeduction.Count > 0 Then
            For Each i In Me.entranceVoucher.EntranceVoucherOtherDeduction
                If i.Type = 1 Then
                    Dim baseOtras As Decimal = Me.entranceVoucher.Value + Me.FreightValue - Me.entranceVoucher.ValueDiscount
                    i.Value = Utils.RoundValue(baseOtras * (i.WithholdingPercentage / 100), CInt(RoundService))
                End If
            Next
            Me.RetentionOther = Me.entranceVoucher.EntranceVoucherOtherDeduction.Sum(Function(x) IIf(x.Type = 1, x.Value, 0))
            Me.DeductionOther = Me.entranceVoucher.EntranceVoucherOtherDeduction.Sum(Function(x) IIf(x.Type = 2, x.Value, 0))
        End If

        Me.CalculateTotalValue()
        Return Task.FromResult(Of Integer)(0)
    End Function

    ''' <summary>
    ''' Refresca los datos de la rejilla
    ''' </summary>
    Private Sub RefrescarRejilla()
        INDGcProducts.DataSource = entranceVoucher.EntranceVoucherDetail.ToList()
        INDGvProducts.RefreshData()
        INDGcProducts.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Método utilziado para calcular los valores de los totales
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateTotalValue()
        Me.InvoiceValue = Me.InvoiceWithFreight
        Me.TotalValue = Me.InvoiceValue - Me.RetentionSource - Me.WithholdingICA - Me.WithholdingTax - Me.DistrictTax - Me.RetentionOther - Me.DeductionOther
        ctrTmp.PrintInfo()
        UpdateUI_Liquidation()
    End Sub

    ''' <summary>
    ''' Metodo para mostar un pop up con los mensajes de validacion por stock
    ''' </summary>
    ''' <param name="messagesValidationStock"></param>
    ''' <remarks></remarks>
    Private Sub ViewMessageValidationStock(messagesValidationStock As List(Of String))
        Using formulario As New FrmPopUpValidateStock
            Me.Cursor = ChangeCursorIndigo()
            formulario.Size = New System.Drawing.Size(800, 730)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Datasource = messagesValidationStock
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Editar el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDetail()
        ItemEntranceVoucherDetail = DirectCast(INDGvProducts.GetFocusedRow(), EntranceVoucherDetail)
        indexEditRecord = entranceVoucher.EntranceVoucherDetail.IndexOf(ItemEntranceVoucherDetail)
        Using formulario As New PopUpProductsEntranceVoucher(RoundService,
                                                             _currency:=New Currency With {.Id = Me.CurrencyId, .Abbreviation = Me.CurrencyAbbreviation},
                                                             _tRMValue:=If(Me.TRM Is Nothing, 1, Me.TRM.Value))
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddEntranceVoucherDetail, AddressOf ReturnPopupAddProduct
            formulario.Size = New System.Drawing.Size(800, 750)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.EntranceVoucherDetailEdit = ItemEntranceVoucherDetail
            formulario.WareHouseId = WarehouseId
            formulario.TaxRegistration = TaxRegistration
            formulario.operatingUnitId = BarraBotones.OperatingUnitValue
            formulario.EditMode = True
            formulario.SupplierId = SupplierId
            formulario.Declarant = _supplier.Declarant
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
        ItemEntranceVoucherDetail = DirectCast(INDGvProducts.GetFocusedRow(), EntranceVoucherDetail)
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then

            If ItemEntranceVoucherDetail.Id > 0 Then
                If ListEntranceVoucherDetailDelete Is Nothing Then
                    ListEntranceVoucherDetailDelete = New List(Of EntranceVoucherDetail)
                End If
                While ItemEntranceVoucherDetail.EntranceVoucherDetailBatchSerial.Count > 0
                    If ItemEntranceVoucherDetail.EntranceVoucherDetailBatchSerial(0).Id > 0 Then
                        ItemEntranceVoucherDetail.EntranceVoucherDetailBatchSerial(0).MarkAsDeleted()
                    Else
                        ItemEntranceVoucherDetail.EntranceVoucherDetailBatchSerial.Remove(ItemEntranceVoucherDetail.EntranceVoucherDetailBatchSerial(0))
                    End If
                End While
                ItemEntranceVoucherDetail.MarkAsDeleted()
                ListEntranceVoucherDetailDelete.Add(ItemEntranceVoucherDetail)
            End If
            entranceVoucher.EntranceVoucherDetail.Remove(ItemEntranceVoucherDetail)

            If entranceVoucher.EntranceVoucherDetail.Any() Then
                INDGleRoundService.Enabled = False
            Else
                INDGleRoundService.Enabled = True
            End If
            Me.EnableDisableCurrency()
            CalculateRetentions()
            Me.RefrescarRejilla()
        End If
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.entranceVoucher IsNot Nothing AndAlso Me.entranceVoucher.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBtnCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Método que muestra/oculta el control de compromiso
    ''' </summary>
    Private Sub ShowHideControlCommitment()
        'Se obtiene los parámetros de pagos por unidad operativa
        If BarraBotones.OperatingUnitValue <> Nothing AndAlso BarraBotones.OperatingUnitValue > 0 Then
            _settingPayments = Presenter.GetSettingsPaymentsByOperatingUnitId(BarraBotones.OperatingUnitValue)

            If _settingPayments IsNot Nothing Then
                'Si se encuentra habilitada la interfaz de presupuesto y el control debe aparecer en la cabecera 
                If _settingPayments.BudgetInterface Then
                    INDlyItemCommitmentDetail.Visibility = XtraLayout.Utils.LayoutVisibility.Always

                    'Si es obligatorio llenar el control de compromiso
                    If _settingPayments.ObligationBudgetInterface Then
                        INDlyItemCommitmentDetail.AllowHide = False
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que muestra/oculta el control de Redondeo
    ''' </summary>
    Private Sub ShowRoundService()
        INDLyTxtRoundService.ShowLayout()
        If Me.indigo.Culture.Name <> "es-CO" Then
            INDLyTxtRoundService.HideLayout()           'Oculta el campo
            RoundService = 0                            'Por defecto es 0 -> Sin Redondeo
        Else
            INDLyTxtRoundService.ShowLayout()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que muestra/oculta el control de %Retencion.ICA
    ''' </summary>
    Private Sub ShowIcaPercentage()
        INDLySpnIcaPercentage.ShowLayout()
        INDLyTxtWithholdingICA.ShowLayout()
        If Me.indigo.Culture.Name <> "es-CO" Then
            INDLySpnIcaPercentage.HideLayout()          'Oculta layout de seccion Factura y Flete
            INDLyTxtWithholdingICA.HideLayout()         'Oculta layout de seccion Liquidacion Factura
            IcaPercentage = Nothing                     'Por defecto sin % rete ica
            WithholdingICA = 0                          'Por defecto en 0 Retención ICA
        Else
            INDLySpnIcaPercentage.ShowLayout()
            INDLyTxtWithholdingICA.ShowLayout()
        End If
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
        _culture.NumberFormat = _currencyAbbreviation.GetNumberFormat
        _culture.NumberFormat.CurrencyDecimalDigits = Me.MaskDigitRounding

        For Each control In ListControls
            control.Properties.Mask.Culture = _culture
            control.Properties.Mask.EditMask = $"C{ _culture.NumberFormat.CurrencyDecimalDigits}"
        Next

        For Each column In ListGridColumns
            column = Window.Utils.FormatGrid(column, _currencyAbbreviation, _culture.NumberFormat.CurrencyDecimalDigits)
        Next

        Me.ctrTmp.CurrencyNumbertFormat = _culture.NumberFormat
        Me.ctrTmp.CodeISO4217 = _currencyAbbreviation
        ctrTmp.PrintInfo()
    End Sub

    ''' <summary>
    ''' metodo que inactiva o desactiva el control de moneda dependiendo si hay o no registro del detalle cargados
    ''' </summary>
    Private Sub EnableDisableCurrency()
        INDsleCurrency.Enabled = If(entranceVoucher.EntranceVoucherDetail?.Any(), False, True)
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
    ''' Nos aseguramos que se actualicen los campos solicitados del semento "Liquidación Factura"
    ''' </summary>
    Private _updatingUI As Boolean

    Private Sub UpdateUI_Liquidation()
        _updatingUI = True
        Try
            INDTxtValue.EditValue = SubtotalUI
            INDTxtValueTax.EditValue = ValueTaxUI
            Me.TotalValue = Me.TotalValue
        Finally
            _updatingUI = False
        End Try
    End Sub

#End Region

End Class