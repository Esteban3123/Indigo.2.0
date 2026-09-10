'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 20/05/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Payments.MVP
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Common
Imports System.Windows.Forms
Imports Domain.Entities.Service
Imports DevExpress.XtraEditors
Imports Presentation.Common.MVP
Imports System.Text
Imports System.Drawing
Imports Infrastructure.Data.Xpo.CostRepository
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports DevExpress.XtraGrid.Views.Grid
Imports Presentation.Accounting.MVP
Imports DevExpress.Xpo
Imports Presentation.Controls.MVP
Imports DevExpress.Data.Async.Helpers

#End Region

Public Class FrmPopupBills
    Implements IPopupBills

#Region "Builder"

    Public ctrTmp As CtrValue
    Dim debit As Decimal
    Dim credit As Decimal

    Public Sub New(ByVal list As List(Of Object))
        ' This call is required by the designer.
        InitializeComponent()
        CheckScreenResolution()
        listConceptCxpXpo = list
        ctrTmp = New CtrValue()
        ctrTmp.SetTotalValues(AddressOf getDebit)
        ctrTmp.RefreshTotalValues()
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
        AdditionalControlPanel.Parent.MinimumSize = New System.Drawing.Size(300, AdditionalControlPanel.Height)
        AdditionalControlPanel.Parent.MaximumSize = New System.Drawing.Size(300, AdditionalControlPanel.Height)
        AdditionalControlPanel.MinimumSize = New System.Drawing.Size(300, AdditionalControlPanel.Height)
        AdditionalControlPanel.MaximumSize = New System.Drawing.Size(300, AdditionalControlPanel.Height)
        AddHandler bwInfo.DoWork, AddressOf bwInfo_DoWork
        AddHandler bwInfo.RunWorkerCompleted, AddressOf bwInfo_RunWorkerCompleted
    End Sub


    Private Function getDebit() As Tuple(Of Decimal, Decimal, Decimal)
        If ListAddConcept Is Nothing Then
            Return New Tuple(Of Decimal, Decimal, Decimal)(0, ValueBill, Me.Taxes)
        End If
        debit = 0
        credit = 0
        For Each item As AccountPayableDetailConcept In ListAddConcept
            item.Value = Math.Round(item.Value, 2, MidpointRounding.AwayFromZero)
            If item.Nature = 1 Then
                debit = item.Value + debit
            Else
                credit = item.Value + credit
            End If
        Next
        Dim valTotal = (debit - credit).ToString("C2")
        Return New Tuple(Of Decimal, Decimal, Decimal)(valTotal, ValueBill, Me.Taxes)
    End Function

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el valor de la factura cuando no tiene permiso de causacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValueBill As Decimal Implements IPopupBills.ValueBill
        Get
            Return INDtxtValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdMainAccountSupplier As Integer
        Get
            Return _idMainAccountSupplier
        End Get
        Set(value As Integer)
            _idMainAccountSupplier = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la variable para poder saber si se desea recalcular la causacion diferida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HandlesPushTrue As Boolean
        Get
            Return _handlesPushTrue
        End Get
        Set(value As Boolean)
            _handlesPushTrue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de causacion diferida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListDeferredCausation As List(Of DeferredCausation)
        Get
            Return _listDeferredCausation
        End Get
        Set(value As List(Of DeferredCausation))
            _listDeferredCausation = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Supplier As Infrastructure.Data.Xpo.PaymentsRepository.Maintenance_Supplier Implements IPopupBills.Supplier
        Get
            Return _supplier
        End Get
        Set(value As Infrastructure.Data.Xpo.PaymentsRepository.Maintenance_Supplier)
            _supplier = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainAccount As Infrastructure.Data.Xpo.PaymentsRepository.GeneralLedgerMainAccountsXpo Implements IPopupBills.MainAccount
        Get
            Return _mainAccount
        End Get
        Set(value As Infrastructure.Data.Xpo.PaymentsRepository.GeneralLedgerMainAccountsXpo)
            _mainAccount = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece las cuotas de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Shares As Integer Implements IPopupBills.Shares
        Get
            Return CInt(INDseShares.EditValue)
        End Get
        Set(value As Integer)
            INDseShares.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece la actividad económica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdEconomicActivity As Integer? Implements IPopupBills.EconomicActivityId
        Get
            Return INDgleEconomicActivity.EditValue
        End Get
        Set(value As Integer?)
            INDgleEconomicActivity.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el listado de las actividades económicas que sí generan ingresos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EconomicActivityXpo As XPInstantFeedbackSource Implements IPopupBills.EconomicActivityXpo
        Get
            Return CType(INDgleEconomicActivity.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDgleEconomicActivity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de centros de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IPopupBills.CostCenterXpo
        Get
            Return CType(INDsleCostCenter.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdAccount As Integer Implements IPopupBills.IdAccount

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdCostCenter As Integer? Implements IPopupBills.IdCostCenter
        Get
            Return CInt(INDsleCostCenter.EditValue)
        End Get
        Set(value As Integer?)
            INDsleCostCenter.EditValue = value
        End Set
    End Property


    ''' <summary>
    ''' Obtiene o establece el numero de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BillNumber As String Implements IPopupBills.BillNumber
        Get
            Return INDtxtBillNumber.Text
        End Get
        Set(value As String)
            INDtxtBillNumber.Text = CStr(value)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los comentarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Comment As String Implements IPopupBills.Comment
        Get
            Return INDmemoComment.Text
        End Get
        Set(value As String)
            INDmemoComment.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el plazo de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Term As Integer Implements IPopupBills.Term
        Get
            Return CInt(INDseTerm.Text)
        End Get
        Set(value As Integer)
            INDseTerm.Text = CStr(value)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece las horas laboradas por el empleado independiente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Hours As Integer Implements IPopupBills.Hours
        Get
            Return CInt(INDseHours.Text)
        End Get
        Set(value As Integer)
            INDseHours.Text = CStr(value)
        End Set
    End Property

    ''' <summary>
    ''' Permite saber si es para modificar o agregar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BanClose As Boolean
        Get
            Return bandera
        End Get
        Set(value As Boolean)
            bandera = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene si maneja iva desc
    ''' </summary>
    Public Property DeductibleIva As Boolean? Implements IPopupBills.DeductibleIva
        Get
            Return INDRgDeductibleIva.EditValue
        End Get
        Set(value As Boolean?)
            INDRgDeductibleIva.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Representa a la entidad de cuenta por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Public Property accountPayable As AccountPayable
        Get
            Return Me._accountPayable
        End Get
        Set(value As AccountPayable)
            Me._accountPayable = value
            Me.CostDistributionDirectCostId = value.CostDistributionDirectCostId
        End Set
    End Property

    ''' <summary>
    ''' Fecha del documento que viene de la cabecera
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As DateTime
        Get
            Return _documentDate
        End Get
        Set(value As DateTime)
            _documentDate = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la moenda de la cuenta por pagar
    ''' </summary>
    ''' <returns></returns>
    Private Property CurrencyId(Optional Abbreviation As String = Nothing) As Integer? Implements IPopupBills.CurrencyId
        Get
            Return INDsleCurrency.EditValue
        End Get
        Set(value As Integer?)
            INDsleCurrency.EditValue = value
            INDsleCurrency.Properties.NullText = Abbreviation
            Me.SetCurrencyCultureUI(Abbreviation)
        End Set
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
    ''' Moneda seleccionada carga registro cuando se ha desplegado el combo
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property CurrencySelected As Infrastructure.Data.Xpo.CommonRepository.CommonCurrencyXpo
        Get
            Return TryCast(TryCast(INDGvCurrencyAdvance.GetFocusedRow, ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonCurrencyXpo)
        End Get
    End Property

    Public Property DocumentSupportId As Integer?
        Get
            Return INDSleDocumentSupportId.EditValue
        End Get
        Set(value As Integer?)
            INDSleDocumentSupportId.EditValue = value
        End Set
    End Property

    Public Property DocumentSupportXpo As XPInstantFeedbackSource Implements IPopupBills.DocumentSupportXpo
        Get
            Return CType(INDSleDocumentSupportId.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleDocumentSupportId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' maneja documento soporte
    ''' </summary>
    ''' <returns></returns>
    Public Property HandlesDocumentSupport As Boolean
        Get
            Return _handlesDocumentSupport
        End Get
        Set(value As Boolean)
            _handlesDocumentSupport = value
            If value Then
                INDLyItemHandleDocumentSupport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDLyItemHandleDocumentSupport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLyItemDocumentSupportId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                DocumentSupportId = Nothing
            End If
        End Set
    End Property

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
    ''' datasource del combo Moneda del segmento anticipo
    ''' </summary>
    ''' <returns></returns>
    Private Property CurrencyAdvanceDatasource As XPInstantFeedbackSource
        Get
            Return TryCast(INDsleCurrency.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCurrency.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' valor total facturable
    ''' </summary>
    ''' <returns></returns>
    Private Property TotalInvoice As Decimal
        Get
            Return INDseTotal.EditValue
        End Get
        Set(value As Decimal)
            INDseTotal.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' impuestos(IVA)
    ''' </summary>
    ''' <returns></returns>
    Private Property Taxes As Decimal
        Get
            Return INDseIva.EditValue
        End Get
        Set(value As Decimal)
            INDseIva.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el tipo de registro del IVA 
    ''' del parametro de empresa
    ''' </summary>
    ''' <returns>   1- IVA Costo (Control Fiscal)
    '''             2- IVA Descontable
    '''             3- IVA Mixto
    '''             4- IVA Costo
    '''             NULL - no parametrizado o Cxp en estado Diferente a registrado </returns>
    Public Property TaxRegistration As Byte?
        Get
            Return _taxRegistration
        End Get
        Set(value As Byte?)
            _taxRegistration = value
        End Set
    End Property
#Region "Budget Interface"

    Public ReadOnly Property BudgetInterface As Boolean
        Get
            Return If(PaymentsSettingPaymentsXpo Is Nothing, False, PaymentsSettingPaymentsXpo.BudgetInterface)
        End Get
    End Property

    Public ReadOnly Property ObligationBudgetInterface As Boolean
        Get
            Return If(PaymentsSettingPaymentsXpo Is Nothing, False, PaymentsSettingPaymentsXpo.ObligationBudgetInterface)
        End Get
    End Property

    Public ReadOnly Property ObligationDebitValue As Boolean
        Get
            Return If(PaymentsSettingPaymentsXpo Is Nothing, False, PaymentsSettingPaymentsXpo.ObligationDebitValue)
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

    Public Property BudgetaryEntityXpo As DevExpress.Xpo.XPCollection Implements IPopupBills.BudgetaryEntityXpo
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

    Public Property BudgetaryValidityXpo As DevExpress.Xpo.XPCollection Implements IPopupBills.BudgetaryValidityXpo
        Get
            Return INDSleBudgetaryValidityId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDSleBudgetaryValidityId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Listado para compromisos asociados a la factura
    ''' </summary>
    ''' <remarks></remarks>
    Public Property ListAccountPayableCommitment As List(Of AccountPayableCommitments) Implements IPopupBills.ListAccountPayableCommitment
        Get
            Return INDgcCommitmentDetail.DataSource
        End Get
        Set(value As List(Of AccountPayableCommitments))
            INDgcCommitmentDetail.DataSource = value
            INDgcCommitmentDetail.RefreshDataSource()
        End Set
    End Property

    ''' <summary>
    ''' Listado de compromisos eliminados
    ''' </summary>
    ''' <remarks></remarks>
    Public listDeleteAccountPayableCommitment As List(Of AccountPayableCommitments)

    ''' <summary>
    ''' obtiene o establece el detalle de una cuenta por pagar
    ''' </summary>
    ''' <returns></returns>
    Public Property AccountPayableDetail As AccountPayableDetailConcept
        Get
            Return _accountPayableConcept
        End Get
        Set(value As AccountPayableDetailConcept)
            _accountPayableConcept = value
        End Set
    End Property


#End Region

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador de la cabecera de la cxp
    ''' </summary>
    Dim PresenterAccountPayable As PAccountPayable

    ''' <summary>
    ''' Representa a la entidad de parámetros de cxp
    ''' </summary>
    Public PaymentsSettingPaymentsXpo As PaymentsSettingPaymentsXpo

    ''' <summary>
    ''' Obtiene o establece el id del tercero
    ''' </summary>
    Public ThirdPartyId As Integer
    ''' <summary>
    ''' Obtiene o establece el tipo de contribuyente segun tercero
    ''' </summary>
    Public ThirdPartyIdContributionType As Integer

    ''' <summary>
    ''' Asyncrono para realizar las consultas pesadas
    ''' </summary>
    ''' <remarks></remarks>
    Private bwInfo As BackgroundWorker = New BackgroundWorker

    ''' <summary>
    ''' Listado de conceptos de pago de tipo retencion que trae la linea de distribucion asociada al proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim listConceptCxpXpo As List(Of Object)

    ''' <summary>
    ''' Representa el id de la cuenta contable del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private _idMainAccountSupplier As Integer

    ''' <summary>
    ''' Representa la entidad de proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim _supplier As Infrastructure.Data.Xpo.PaymentsRepository.Maintenance_Supplier

    ''' <summary>
    ''' Representa la entidad de cuentas contables
    ''' </summary>
    ''' <remarks></remarks>
    Dim _mainAccount As Infrastructure.Data.Xpo.PaymentsRepository.GeneralLedgerMainAccountsXpo

    ''' <summary>
    ''' Variable que representa al presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PPopupBills

    ''' <summary>
    ''' Genera la fecha del servidor
    ''' </summary>
    ''' <remarks></remarks>
    Dim dateServerVariable As DateTime

    Dim bandera As Boolean

    ''' <summary>
    ''' Variable que contiene la lista con la naturaleza de la cuenta
    ''' </summary>
    Dim NatureType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Listado para agregar un concepto a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Public ListAddConcept As List(Of AccountPayableDetailConcept)

    ''' <summary>
    ''' Representa la entidad de detalle de cuentas por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Dim accountPayableDetailConcept As AccountPayableDetailConcept

    ''' <summary>
    ''' Contiene el listado de cuotas de la cuenta por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Public ListAccountPayableShares As List(Of AccountPayableShares)

    ''' <summary>
    ''' Contiene el valor total de la factura
    ''' </summary>
    ''' <remarks></remarks>
    Dim valBill As Decimal

    ''' <summary>
    ''' Listado de eliminados de cuotas
    ''' </summary>
    ''' <remarks></remarks>
    Dim listDelete As List(Of AccountPayableShares)

    ''' <summary>
    ''' Listado de eliminados de conceptos
    ''' </summary>
    ''' <remarks></remarks>
    Dim listDeleteDetailConcept As List(Of AccountPayableDetailConcept)

    ''' <summary>
    ''' Listado de causacion diferida
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeferredCausation As List(Of DeferredCausation)

    ''' <summary>
    ''' Variable para saber si se recalcula la causacion diferida
    ''' </summary>
    ''' <remarks></remarks>
    Dim _handlesPushTrue As Boolean

    ''' <summary>
    ''' Propiedad que verifica si hay actividad económica
    ''' </summary>
    ''' <remarks></remarks>
    Private Property IsEconomicActivity As Boolean

    ''' <summary>
    ''' Variable para saber si tiene permiso para causar
    ''' </summary>
    ''' <remarks></remarks>
    Dim keyCausation

    ''' <summary>
    ''' Variable para saber si se recalcula la causacion diferida
    ''' </summary>
    ''' <remarks></remarks>
    Public RecaulculateDeferredCausation As Boolean

    ''' <summary>
    ''' Listado para comparar los detalles de la cxp
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListCompareDetail As List(Of AccountPayableDetailConcept)

    ''' <summary>
    ''' Entidad xpo que representa a los parámetros de costos
    ''' </summary>
    ''' <remarks></remarks>
    Dim CostSettingXpo As CostSettingXpo

    Dim _isLoading As Boolean

    ''' <summary>
    ''' Obtiene la fecha de la CxP para postular en la fecha de factura
    ''' </summary>
    Public DocumentDateCxP As DateTime

    Private _handlesDocumentSupport As Boolean

    ''' <summary>
    ''' Listado de cuentas por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Public ListBill As List(Of AccountPayable)

    ''' <summary>
    ''' cxp
    ''' </summary>
    Private _accountPayable As AccountPayable

    ''' <summary>
    ''' Contiene el id del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Public IdSupplier As Integer

    ''' <summary>
    ''' Contiene el id del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Public IndependentEmployee As Boolean

    ''' <summary>
    ''' Contiene el id de la distribución del elemento del costo
    ''' </summary>
    ''' <remarks></remarks>
    Public CostDistributionDirectCostId As Integer?

    ''' <summary>
    ''' Variable para saber si agregan o modifican al listado
    ''' </summary>
    ''' <remarks></remarks>
    Public entity As Boolean

    Private _documentDate As DateTime?

    ''' <summary>
    ''' Obtiene o establece si tiene iva descontable
    ''' </summary>
    Public hasDeductibleIva As Boolean?

    ''' <summary>
    ''' Bandera para saber si el registro se esta editando
    ''' </summary>
    Public editMode As Boolean

    ''' <summary>
    ''' Bandera para saber si el registro ya esta confirmado
    ''' </summary>
    Public ConfirmStatus As Boolean

    ''' <summary>
    ''' Obtiene o establece si se muestra el componente de  iva descontable
    ''' </summary>
    Public showDeductibleIva As Boolean?

    ''' <summary>
    ''' Obtiene el estado de la cuenta por pagar
    ''' </summary>
    Public accountPayableStatus As Byte

    ''' <summary>
    ''' Obtiene el código de la cuenta por pagar
    ''' </summary>
    Public accountPayableCode As String

    ''' <summary>
    ''' Fecha de creación del documento que viene de la cabecera
    ''' </summary>
    Public Property CreationDate As DateTime

    ''' <summary>
    ''' obitene el valor base de todos los conceptos agregados que son de typo resultado
    ''' </summary>
    Private baseValueConceptResult As Decimal

    ''' <summary>
    ''' obtiene el valor base sumando el iva de todos los conceptos agregados para las retencion que son reteiva
    ''' </summary>
    Private baseValueIvaRetention As Decimal

    ''' <summary>
    ''' Representa la entidad de conceptos de cuentas por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Dim _accountPayableConcept As AccountPayableDetailConcept

    ''' <summary>
    ''' varible que contiene el valor tipo de iva de parametro de empresa
    ''' </summary>
    Private _taxRegistration As Byte?

#End Region

#Region "Const"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Payments"

    ''' <summary>
    ''' Nombre del frontal en el que estoy
    ''' </summary>
    Private Const _form As String = "AccountPayable"

#End Region

#Region "Crud"

    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Metodo: Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Emite el mensaje en el slider
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

    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing
        debit = Nothing
        credit = Nothing
        bwInfo = Nothing
        listConceptCxpXpo = Nothing
        _idMainAccountSupplier = Nothing
        _supplier = Nothing
        _mainAccount = Nothing
        Presenter = Nothing
        dateServerVariable = Nothing
        bandera = Nothing
        NatureType = Nothing
        ListAddConcept = Nothing
        accountPayableDetailConcept = Nothing
        ListAccountPayableShares = Nothing
        valBill = Nothing
        listDelete = Nothing
        listDeleteDetailConcept = Nothing
        _listDeferredCausation = Nothing
        _handlesPushTrue = Nothing
        keyCausation = Nothing
        RecaulculateDeferredCausation = Nothing
        ListCompareDetail = Nothing
        CostSettingXpo = Nothing
        _isLoading = Nothing

        ListAccountPayableCommitment = Nothing
        listDeleteAccountPayableCommitment = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara al cargar el formulario de facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopupBills_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyBill, True)

        'Cargar GridLookUpEdit
        INDSleHandleDocumentSupport.Properties.DataSource = FillingHandleDocumentSupport
        INDSleDocumentSupportId.Properties.DataSource = DocumentSupportXpo
        INDSleDocumentSupportId.EditValue = DocumentSupportId

        '****Inicializar variables*****' 

        Me.indigo = SessionValues.Instance
        Presenter = New PPopupBills(Me)
        PresenterAccountPayable = New PAccountPayable()
        ShowEconomicActivity()
        InitializeInformation()
        IndigoGridControl1.RefreshGrid(INDgcConcept)
        IndigoGridControl1.RefreshGrid(INDgcCommitmentDetail)
        IndigoGridControl1.RefreshGrid(INDgcShares)

        If ConfirmStatus Then
            IndigoGridView1.SetListAcction(viewConcept, {eAcciones.View}.ToList)
        Else
            IndigoGridView1.SetListAcction(viewConcept, {eAcciones.Remove, eAcciones.Edit}.ToList)
        End If

        HandlesPushTrue = False

        If Me.editMode Then
            INDRgDeductibleIva.Enabled = False
        Else
            Me.CurrencyId(Me.indigo.CurrencyISO4217) = Me.indigo.OfficialCurrencyId
        End If

        INDdtBillDate.Properties.MaxValue = GetDateServer()
        INDdtBillDate.Properties.MinValue = GetDateServer().AddMonths(-3)
        INDlygConcept.Visibility = If(keyCausation > 0, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, INDlygConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDlyItemHours.Visibility = If(IndependentEmployee, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDlyItemHours.AllowHide = Not IndependentEmployee
        INDLyDeductibleIva.Visibility = If(showDeductibleIva = True, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
    End Sub

    ''' <summary>
    ''' Oculta los botones de la barra botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideBotons()
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub INDRpTxtValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRpTxtValue.EditValueChanging
        If e.NewValue = String.Empty Then
            Exit Sub
        End If
        Dim detail = DirectCast(INDgvCommitmentDetail.GetFocusedRow, AccountPayableCommitments)
        If e.NewValue < 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El valor no puede ser mayor al saldo"
            e.Cancel = True
        ElseIf e.NewValue > detail.Balance Then
            Mensaje(EeventViewerImages.Advertencia) = "El valor no puede ser mayor al saldo"
            e.Cancel = True
        End If

        Dim budgetValue As Decimal = 0
        Dim billingValue As Decimal = 0

        If ListAccountPayableCommitment IsNot Nothing AndAlso ListAccountPayableCommitment.Count > 0 Then
            budgetValue = ListAccountPayableCommitment.Sum(Function(d) d.Value) - e.OldValue + e.NewValue
        End If
        If ListAddConcept IsNot Nothing AndAlso ListAddConcept.Count Then
            billingValue = ListAddConcept.Where(Function(d) Not ObligationDebitValue OrElse d.Nature = 1).Sum(Function(d) d.Value)
        End If

        If budgetValue > billingValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format("El valor de la interfaz de presupuesto ({0}) no puede ser mayor al valor facturado ({1})", budgetValue, billingValue)
            e.Cancel = True
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del plazo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDseTerm_EditValueChanged(sender As Object, e As EventArgs) Handles INDseTerm.EditValueChanged
        AddDaysDateExpired()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de fecha de factura
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDdtBillDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdtBillDate.EditValueChanged
        If INDdtBillDate.EditValue IsNot Nothing Then
            INDdtExpiredDate.Properties.MinValue = CDate(INDdtBillDate.EditValue)
            AddDaysDateExpired()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAccount.EditValueChanged
        If IdAccount > 0 Then
            Using model As New MPUC(CStr(Tag))
                Dim puc As MainAccounts
                puc = model.GetAccountId(IdAccount)
                If puc.HandlesCostCenter = True Then
                    INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemCostCenter.AllowHide = False
                Else
                    INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemCostCenter.AllowHide = True
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de centro de costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCostCenter.EditValueChanged
        If IdCostCenter > 0 Then
            Using model As New MAccountPayable(CStr(Tag))
                Dim cc As New CostCenter
                cc = model.GetCostCenterId(IdCostCenter)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del campo de numero de cuotas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDseShares_EditValueChanged(sender As Object, e As EventArgs) Handles INDseShares.EditValueChanged
        If Shares > 0 AndAlso Shares <= 36 Then
            CreateShare()
        Else
            If Shares > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmPopupBills_InvalidNumber", NAME_MODULE)
                INDseShares.Focus()
            End If
            INDgcShares.DataSource = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de valor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtValue.EditValueChanged
        ctrTmp.RefreshTotalValues()
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
            Presenter.ListCommitmentDetail(BudgetaryValidityId, ThirdPartyId, INDdtBillDate.EditValue)
        End If
    End Sub

    Private Sub INDSleHandleDocumentSupport_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleHandleDocumentSupport.EditValueChanged
        If INDSleHandleDocumentSupport.EditValue = False Then
            INDLyItemDocumentSupportId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            DocumentSupportId = Nothing
        Else
            INDLyItemDocumentSupportId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDSleDocumentSupportId.Enabled = True
        End If
    End Sub

    Private Sub INDSleDocumentSupportId_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleDocumentSupportId.EditValueChanged
        If DocumentSupportId IsNot Nothing Then
            Presenter.InitializeDocumentSupport()
            SetFirstOrDefaultResolution()
        End If
    End Sub

    ''' <summary>
    ''' evento cuando se cambia la moneda en el combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCurrency_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCurrency.EditValueChanged
        If Me.CurrencyId IsNot Nothing AndAlso Me.CurrencySelected IsNot Nothing Then
            SetCurrencyCultureUI(Me.CurrencySelected?.Abbreviation)
        End If
    End Sub
#End Region

#Region "MenuContextual"

    ''' <summary>
    ''' Crea el menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions, IndigoGridView1.Click_ButtonAction
        Dim acc As AccountPayableDetailConcept = CType(viewConcept.GetFocusedRow, AccountPayableDetailConcept)
        Select Case (sender.Tag.ToString)
            Case "Edit"
                If acc IsNot Nothing Then
                    If acc.IsDirectCost Then
                        Mensaje(EeventViewerImages.Advertencia) = "Este item no se puede editar porque es de distribución de elementos del costo"
                        Exit Sub
                    End If
                End If
                EditConcept(acc)
            Case "Remove"
                If acc IsNot Nothing Then
                    If acc.IsDirectCost Then
                        Mensaje(EeventViewerImages.Advertencia) = "Este item no se puede eliminar porque es de distribución de elementos del costo"
                        Exit Sub
                    End If
                End If
                DeleteConcept(acc)
            Case "View"
                EditConcept(acc)
        End Select
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar f4 o enter al control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceAddConcept_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceAddConcept.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = Keys.F4 Then
            INDpceAddConcept.ShowPopup()
            If hasDeductibleIva Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Antes de agregar un concepto, debe de seleccionar si tiene Iva Descontable o no"
                INDpceAddConcept.ClosePopup()
            Else
                CtrConcepts1.BillNumber = BillNumber
                CtrConcepts1.CreationDate = CreationDate
                If CtrConcepts1.optionEdit Then
                    Dim acc As AccountPayableDetailConcept = CType(viewConcept.GetFocusedRow, AccountPayableDetailConcept)
                    CtrConcepts1.InitializeControler(acc, Nothing, _form,
                                                         Nothing, listConceptCxpXpo, Supplier, showDeductibleIva,
                                                         hasDeductibleIva, CurrencyAbbreviation:=Me.CurrencyAbbreviation, taxRegistration:=TaxRegistration)
                Else
                    CtrConcepts1.InitializeControler(accountPayableDetailConcept, Nothing, _form,
                                                         Nothing, listConceptCxpXpo, Supplier, showDeductibleIva,
                                                         hasDeductibleIva, CurrencyAbbreviation:=Me.CurrencyAbbreviation, taxRegistration:=TaxRegistration)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el campo numero de factura
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDtxtBillNumber_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDtxtBillNumber.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Me.showDeductibleIva AndAlso DeductibleIva Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Por favor eliga si el iva es descontable"
                Exit Sub
            End If
            If BillNumber IsNot String.Empty Then
                Await ValidateBill()
                INDdtBillDate.Focus()
            Else
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmPopupBills_BillNumberEmpty", NAME_MODULE)
                INDtxtBillNumber.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar escape al formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupBills_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            BanClose = False
            Close()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter al control de comentarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDmemoComment_KeyDown(sender As Object, e As KeyEventArgs) Handles INDmemoComment.KeyDown
        If e.KeyCode = Keys.Enter Then
            If INDlygConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDpceAddConcept.Focus()
            ElseIf INDlygBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDSleBudgetaryEntityId.Focus()
            Else
                INDbtnAdd.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar escape al formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDRpTxtValue_KeyDown(sender As Object, e As KeyEventArgs) Handles INDRpTxtValue.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Tab Then
            INDbtnAdd.Focus()
        End If
    End Sub

#End Region

#Region "CloseUp"

    ''' <summary>
    ''' Evento que se dispara al ocultar el popupControl de conceptos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceAddConcept_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceAddConcept.CloseUp
        If CtrConcepts1.Entity Then
            CtrConcepts1.CleanControls()
        End If

        If e.CloseMode = PopupCloseMode.Cancel Then
            If INDlygBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDSleBudgetaryEntityId.Focus()
            Else
                INDbtnAdd.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cerrar el popup de cuotas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDseShares_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDseShares.CloseUp
        If e.CloseMode = PopupCloseMode.Cancel Then
            INDsleAccount.Focus()
        End If
    End Sub

#End Region

#Region "ClosePopup"

    ''' <summary>
    ''' Evento para cerrar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CtrConcepts1_ClosePopup(sender As Object, e As EventArgs) Handles CtrConcepts1.ClosePopup
        CtrConcepts1.INDsleConcept.Focus()
        CtrConcepts1.CleanControls()
        INDpceAddConcept.ClosePopup()
        If CtrConcepts1.AccountPayableDetail IsNot Nothing Then
            INDRgDeductibleIva.Enabled = False
        End If
        If CtrConcepts1.AccountPayableDetail IsNot Nothing Then
            AddConcept(CtrConcepts1.AccountPayableDetail)
        End If
    End Sub

    ''' <summary>
    ''' Evento para evitar cerrar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CtrConcepts1_DontClosePopup(sender As Object, e As EventArgs) Handles CtrConcepts1.DontClosePopup
        INDpceAddConcept.ShowPopup()
        CtrConcepts1.INDsleConcept.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presioanr click en el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceAddConcept_Click(sender As Object, e As EventArgs) Handles INDpceAddConcept.Click
        If hasDeductibleIva Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Antes de agregar un concepto, debe de seleccionar si tiene Iva Descontable o no"
            INDpceAddConcept.ClosePopup()
        Else
            CtrConcepts1.CleanControls()
            CtrConcepts1.BillNumber = BillNumber
            CtrConcepts1.CreationDate = CreationDate
            If CtrConcepts1.optionEdit Then
                Dim acc As AccountPayableDetailConcept = CType(viewConcept.GetFocusedRow, AccountPayableDetailConcept)
                CtrConcepts1.InitializeControler(acc, Nothing, _form,
                                                     Nothing, listConceptCxpXpo, Supplier, showDeductibleIva,
                                                     hasDeductibleIva, CurrencyAbbreviation:=Me.CurrencyAbbreviation, taxRegistration:=TaxRegistration)
            Else
                CtrConcepts1.InitializeControler(accountPayableDetailConcept, Nothing, _form,
                                                     Nothing, listConceptCxpXpo, Supplier, showDeductibleIva,
                                                     hasDeductibleIva, CurrencyAbbreviation:=Me.CurrencyAbbreviation, taxRegistration:=TaxRegistration)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se diaspara al presionar click en el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        AddBill()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar concepto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddConcept_Click(sender As Object, e As EventArgs)
        AddConcept(Nothing)
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupBills_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If accountPayable.BillNumber Is Nothing Then
            If MessageIndigo.Show(ResourceManager.GetString("CloseForm", NAME_MODULE), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                BanClose = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del control de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presional clic en el boton del control de centro de costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmCostCenter With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeCostCenter()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre los botones del control del valor facturado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtValue_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDtxtValue.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph Then
            OpenFormCost()
        ElseIf e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            DeleteConceptDirectCost()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en el boton del control de la actividad económica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleEconomicActivity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDgleEconomicActivity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1845, Nothing, True)
        End If
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupBills_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If BillNumber Is String.Empty Then
            INDtxtBillNumber.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' evento que se dispara al desplegarse el control de centro costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCostCenter.QueryPopUp
        If INDsleCostCenter.Properties.DataSource Is Nothing Then
            Presenter.InitializeCostCenter()
        End If
    End Sub

    Private Sub INDsleBudgetaryEntityId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleBudgetaryEntityId.QueryPopUp
        Presenter.InitializeBudgetaryEntity()
    End Sub

    Private Sub INDsleBudgetaryValidityId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleBudgetaryValidityId.QueryPopUp
        Presenter.InitializeBudgetaryValidity(BudgetaryEntityId)
    End Sub

    ''' <summary>
    ''' evento que se dispara al desplegarse el control de selección del documento soporte
    ''' </summary>
    Private Sub INDSleDocumentSupportId_QuerPopUp(sender As Object, e As CancelEventArgs) Handles INDSleDocumentSupportId.QueryPopUp
        Presenter.InitializeDocumentSupport()
    End Sub

    ''' <summary>
    ''' evento para consultar las monedas activas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCurrency_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCurrency.QueryPopUp
        If CurrencyAdvanceDatasource Is Nothing Then
            Using Model As New MBusqueda
                CurrencyAdvanceDatasource = Model.ConsultarEntidades(eDataSource.Currency)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se despliega el control de las actividades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleEconomicActivity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDgleEconomicActivity.QueryPopUp
        If INDgleEconomicActivity.Properties.DataSource Is Nothing Then
            Presenter.GetEconomicActivity()
        End If
    End Sub
#End Region

#Region "Showing"

    ''' <summary>
    ''' Evento que se dispara al interactuar con la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub viewConcept_ShowingEditor(sender As Object, e As CancelEventArgs) Handles viewConcept.ShowingEditor
        Dim pMouse As System.Drawing.Point = INDgcConcept.PointToClient(Control.MousePosition)
        Dim hit = viewConcept.CalcHitInfo(pMouse)
        If hit.Column IsNot Nothing AndAlso hit.Column.Name.Equals("colActions") Then
            Dim itemCollection As AccountPayableDetailConcept = viewConcept.GetFocusedRow()
            If itemCollection.IsDirectCost Then
                e.Cancel = True
            Else
                e.Cancel = False
            End If
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupBills_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        CreateNature()
        HideBotons()
        LoadStatus()
        ShowHideControlCommitment()
        INDtxtBillNumber.Focus()
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Método que muestra/oculta el control de compromiso
    ''' </summary>
    Private Sub ShowHideControlCommitment()
        INDLciBudgetaryEntityId.AllowHide = Not (BudgetInterface AndAlso ObligationBudgetInterface)
        INDLciBudgetaryEntityId.ShowInCustomizationForm = Not (BudgetInterface AndAlso ObligationBudgetInterface)
        INDLciBudgetaryValidityId.AllowHide = Not (BudgetInterface AndAlso ObligationBudgetInterface)
        INDLciBudgetaryValidityId.ShowInCustomizationForm = Not (BudgetInterface AndAlso ObligationBudgetInterface)
        INDlygBudget.Visibility = If(BudgetInterface, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
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
                ShowLayout(INDlyEconomicActivity)
            Else
                HideLayout(INDlyEconomicActivity)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Inicia el asyncrono
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bwInfo_DoWork(sender As Object, e As DoWorkEventArgs)
        'Consulto el proveedor por id con xpo
        Supplier = Presenter.GetSupplierById(IdSupplier)
        If accountPayable Is Nothing OrElse accountPayable.Id = 0 Then 'Si se está guardando
            'Consulto la cuenta contable por id con xpo
            MainAccount = Presenter.GetMainAccountById(IdMainAccountSupplier)
        End If
    End Sub

    ''' <summary>
    ''' Termina el asyncrono
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bwInfo_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        'Se asigna el id y la descripción del tercero que se consulta con el proveedor
        CtrConcepts1.idThird = Supplier.IdThirdParty.Id
        CtrConcepts1.ThirdPartyDescription = Supplier.IdThirdParty.NitName
        CtrConcepts1.FiscalYear = DocumentDate.Year
        CtrConcepts1.DocumentDate = DocumentDate.Date
        CtrConcepts1.CreationDate = CreationDate
        CtrConcepts1.OriginStatus = accountPayableStatus
        CtrConcepts1.OriginCode = accountPayableCode
        If accountPayable Is Nothing OrElse accountPayable.Id = 0 Then 'Si se está guardando
            Term = Supplier.TimeLimitDays
            INDsleAccount.Properties.NullText = MainAccount.NumberName
            ValidateHandlesCostCenter()
        End If
    End Sub

    ''' <summary>
    ''' Elimina los conceptos que fueron agregados con la distribución de elementos del costo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteConceptDirectCost(Optional WithMessage As Boolean = True)
        If ListAddConcept IsNot Nothing AndAlso ListAddConcept.Count > 0 Then

            'Se obtienen todos los detalles que fueron agregados por distribución de elementos del costo
            Dim ListDeleteDirectCost = (From x In ListAddConcept Where x.IsDirectCost = True Select x).ToList

            If ListDeleteDirectCost IsNot Nothing AndAlso ListDeleteDirectCost.Count > 0 Then

                If WithMessage Then
                    If MessageIndigo.Show("Desea eliminar los detalles de distribución de elementos del costo?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If
                End If

                For Each itemDelete In ListDeleteDirectCost
                    If itemDelete.Id <> 0 Then
                        If listDeleteDetailConcept Is Nothing Then
                            listDeleteDetailConcept = New List(Of AccountPayableDetailConcept)
                        End If
                        listDeleteDetailConcept.Add(itemDelete)
                    Else
                        If accountPayable.AccountPayableDetailConcept.Count > 0 Then
                            accountPayable.AccountPayableDetailConcept.Remove(itemDelete)
                        End If
                    End If
                    ListAddConcept.Remove(itemDelete)
                Next
                CostDistributionDirectCostId = Nothing
                RefreshGridConcept()
                'Se vuelve a habilitar el control del valor facturado ya que se eliminaron todos los detalles que se crearon con la distribución de elementos del costo
                INDtxtValue.Properties.ReadOnly = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método que abre el formulario para escoger la distribución de elementos del costo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenFormCost()
        If CostDistributionDirectCostId IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Ya se ha causado la distribución de los elementos del costo"
            Exit Sub
        End If

        'Se valida que se haya diligenciado el valor facturado
        If Not (ValueBill <> Nothing AndAlso ValueBill > 0) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar un valor facturado"
            Exit Sub
        End If

        'Se valida que exista parámetros de costos siempre y cuando no se haya consultado ni una vez
        If CostSettingXpo Is Nothing Then
            CostSettingXpo = Presenter.GetCostSetting()
            If CostSettingXpo Is Nothing Then 'Si no existe parametros de costos
                Mensaje(EeventViewerImages.Advertencia) = "No existe parámetros de costos"
                Exit Sub
            End If
            If CostSettingXpo.AccountPayableConceptsId Is Nothing OrElse CostSettingXpo.AccountPayableConceptsId.Id = 0 Then 'Se valida si los parametros de costos tenga asociado el concepto de cxp
                Mensaje(EeventViewerImages.Advertencia) = "No está parametrizado el concepto de cxp en los parámetros de costos"
                Exit Sub
            End If
        End If

        Using formulario As New FrmSelectCost
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddSelectCostEventArgs, AddressOf ReturnAddEventArgs
            formulario.DocumentDate = DocumentDate
            formulario.ThirdPartyId = CtrConcepts1.idThird
            formulario.Value = ValueBill
            formulario.CostDistributionDirectCostId = CostDistributionDirectCostId
            formulario.AccountPayableId = accountPayable.Id
            formulario.ListAccountPayable = ListBill
            formulario.BillNumber = BillNumber
            formulario.ToolBars.Visible = False
            formulario.Size = New Size(875, 350)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Retorno del form de selección de costos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddEventArgs(sender As Object, e As AddSelectCost)
        If Not (e IsNot Nothing AndAlso e.CostDistributionDirectCostXpo IsNot Nothing) Then
            Exit Sub
        End If

        'Si el id es el mismo no se hace nd y se dejan los detalles que estan
        If CostDistributionDirectCostId IsNot Nothing AndAlso CostDistributionDirectCostId = e.CostDistributionDirectCostXpo.Id Then
            Exit Sub
        End If

        'Si el id es diferente se eliminan los detalles de distribución secundaria y vuelve y se crean los nuevos
        If CostDistributionDirectCostId IsNot Nothing AndAlso CostDistributionDirectCostId <> e.CostDistributionDirectCostXpo.Id Then
            DeleteConceptDirectCost(False)
        End If

        If ListAddConcept Is Nothing Then 'Si es null el listado
            ListAddConcept = New List(Of AccountPayableDetailConcept)
        End If

        'Se recorren los detalles de la distribución de elementos del costo seleccionado para insertarlos en la rejilla de conceptos de cxp
        For Each itemDetail In e.CostDistributionDirectCostXpo.CostDistributionDirectCostDetailXpo
            Dim detail As New AccountPayableDetailConcept
            With detail
                .IdConceptAccountPayable = CostSettingXpo.AccountPayableConceptsId.Id
                .DescriptionPaymentConcept = CostSettingXpo.AccountPayableConceptsId.CodeName
                .IdAccount = itemDetail.MainAccountId.Id
                .NumberNameMainAccount = itemDetail.MainAccountId.NumberName
                .IdThirdParty = CtrConcepts1.idThird
                .DeferredCausation = False
                .HandlesDeferredCausation = False
                If itemDetail.CostCenterId IsNot Nothing AndAlso itemDetail.CostCenterId.Id > 0 Then
                    .IdCostCenter = itemDetail.CostCenterId.Id
                    .DescriptionCostCenter = itemDetail.CostCenterId.Code + " - " + itemDetail.CostCenterId.Name
                Else
                    .IdCostCenter = Nothing
                    .DescriptionCostCenter = String.Empty
                End If
                .Detail = "Detalle generado por distribución de elementos del costo"
                .DescriptionRetentionConcept = String.Empty
                .IdRetentionConcept = Nothing
                .Value = itemDetail.Value
                .BillingValue = 0
                .Nature = itemDetail.MainAccountId.Nature
                .BaseValue = itemDetail.Value
                .IsDirectCost = True
            End With
            ListAddConcept.Add(detail)
        Next
        CostDistributionDirectCostId = e.CostDistributionDirectCostXpo.Id
        RefreshGridConcept()

        'Se coloca como ReadOnly el control de valor facturado porque despues que se agregan detalles desde distribución de elementos del costo,
        'la única manera de cambiar el valor es eliminando primero los items que se agregaron por este medio
        INDtxtValue.Properties.ReadOnly = True
    End Sub

    ''' <summary>
    ''' Edita el concepto
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditConcept(Item As AccountPayableDetailConcept)
        CtrConcepts1.optionEdit = True
        CtrConcepts1.BillNumber = BillNumber
        CtrConcepts1.CreationDate = CreationDate
        CtrConcepts1.InitializeControler(Item, Nothing, _form, Nothing, listConceptCxpXpo, Supplier, showDeductibleIva, hasDeductibleIva,
                                         CurrencyAbbreviation:=Me.CurrencyAbbreviation, taxRegistration:=TaxRegistration, ConfirmStatus:=ConfirmStatus)
        INDpceAddConcept.ShowPopup()
    End Sub

    ''' <summary>
    ''' Metodo que valida si la cuenta contable maneja centro de costo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateHandlesCostCenter()
        If MainAccount.HandlesCostCenter = True Then
            INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemCostCenter.AllowHide = False
        Else
            INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemCostCenter.AllowHide = True
        End If
    End Sub

    ''' <summary>
    ''' Inicializa la informacion de los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeInformation()

        'Se inicia el asyncrono
        bwInfo.RunWorkerAsync()

        ActionsOnControls = True
        INDlyBill.BeginUpdate()

        If accountPayable Is Nothing Then 'Si se va a guardar
            Deshacer()
            INDdtBillDate.EditValue = DocumentDateCxP
            accountPayable = New AccountPayable
            IdAccount = IdMainAccountSupplier

            Me.BarraBotones.StatusRecordVisible = True
            Me.BarraBotones.StatusRecord = "1"
            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True

            INDSleHandleDocumentSupport.EditValue = Me.HandlesDocumentSupport

            entity = False
        Else 'Si se va a modificar
            LoadingInformation()
            entity = True
        End If

        INDlyBill.EndUpdate()
    End Sub

    ''' <summary>
    ''' Metodo para eliminar el concepto
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteConcept(item As AccountPayableDetailConcept)
        If item.Id <> 0 Then
            If listDeleteDetailConcept Is Nothing Then
                listDeleteDetailConcept = New List(Of AccountPayableDetailConcept)
            End If
            listDeleteDetailConcept.Add(item)
        Else
            If accountPayable.AccountPayableDetailConcept.Any() Then
                accountPayable.AccountPayableDetailConcept.Remove(item)
            End If
        End If

        ListAddConcept.Remove(item)
        If Not ListAddConcept?.Any Then
            INDRgDeductibleIva.Enabled = True
        End If

        RefreshGridConcept()
    End Sub

    ''' <summary>
    ''' Adiciona los dias a la fecha de vencimiento
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddDaysDateExpired()
        If Term <> 0 AndAlso INDdtBillDate.EditValue IsNot Nothing Then
            Dim dateExpired As DateTime
            dateExpired = PaymentServices.AddDaysDate(Term, INDdtBillDate.EditValue)
            INDdtExpiredDate.EditValue = dateExpired

            If Shares > 0 Then
                CreateShare()
            End If
        ElseIf Term = 0 Then
            INDdtExpiredDate.EditValue = INDdtBillDate.EditValue
        End If
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Llena el control con el listado de la naturaleza de la cuenta
    ''' </summary>
    Private Sub CreateNature()
        NatureType = New List(Of Tuple(Of Integer, String))
        NatureType.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("AccountNatureDebit")))
        NatureType.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("AccountNatureCredit")))
        RepositoryItemINDgleNature.DataSource = NatureType.ToList()
    End Sub

    ''' <summary>
    ''' Metodo para abrir el form de concepto
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddConcept(accDetail As AccountPayableDetailConcept)
        If accDetail.ChangeTracker.State = ObjectState.Added Then
            If ListAddConcept Is Nothing Then
                ListAddConcept = New List(Of AccountPayableDetailConcept)
            End If
            If Not CtrConcepts1.Entity Then
                CtrConcepts1.banSearch = True
                ListAddConcept.Add(accDetail)
                INDpceAddConcept.ShowPopup()
                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AddConceptSatisfactory", NAME_MODULE)
                CtrConcepts1.INDsleConcept.Focus()
            End If
        End If
        RefreshGridConcept()
    End Sub

    ''' <summary>
    ''' Metodo para refrescar la rejilla de conceptos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RefreshGridConcept()
        If BudgetInterface Then
            If ListAccountPayableCommitment IsNot Nothing AndAlso ListAccountPayableCommitment.Count = 1 Then
                Dim accountPayableCommitment = ListAccountPayableCommitment.FirstOrDefault()
                Dim billingValue As Decimal = ListAddConcept.Where(Function(d) Not ObligationDebitValue OrElse d.Nature = 1).Sum(Function(d) d.Value)
                If billingValue > accountPayableCommitment.Balance Then
                    billingValue = accountPayableCommitment.Balance
                End If
                accountPayableCommitment.Value = billingValue
            End If
        End If

        INDgcConcept.DataSource = Nothing
        INDgcConcept.DataSource = ListAddConcept
        CtrConcepts1.AccountPayableDetail = Nothing
        CalculateTotal(ListAddConcept)
        CreateShareAndRefreshDebitCredit()
    End Sub

    ''' <summary>
    ''' Crea las cuotas y actualiza el control de debito y credito
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateShareAndRefreshDebitCredit()
        If Shares > 0 AndAlso Shares <= 36 Then
            CreateShare()
        End If
        ctrTmp.RefreshTotalValues()
    End Sub

    ''' <summary>
    ''' Metodo para crear las cuotas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateShare()
        valBill = PaymentServices.DebitCredit(ListAddConcept)
        If valBill = 0 AndAlso _isCleaning = False Then
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("FrmPopupBills_ValueShares", NAME_MODULE)
        End If

        If ListAccountPayableShares IsNot Nothing Then
            listDelete = New List(Of AccountPayableShares)
            For Each item As AccountPayableShares In ListAccountPayableShares
                If item.Id <> 0 Then
                    item.MarkAsDeleted()
                    listDelete.Add(item)
                End If
            Next
        End If

        If INDdtExpiredDate.EditValue Is Nothing Then
            ListAccountPayableShares = PaymentServices.CreateShares(Shares, valBill, dateServerVariable)
        Else
            ListAccountPayableShares = PaymentServices.CreateShares(Shares, valBill, CDate(INDdtExpiredDate.EditValue))
        End If

        INDgcShares.DataSource = Nothing
        INDgcShares.DataSource = ListAccountPayableShares
    End Sub

    ''' <summary>
    ''' Metodo para cargar la informacion que viene de la cuenta por pagar(facturas)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadingInformation()
        _isLoading = True
        With accountPayable
            DeductibleIva = hasDeductibleIva
            BillNumber = .BillNumber
            INDdtBillDate.EditValue = .BillDate
            Term = .Term
            Hours = .Hours
            INDdtExpiredDate.EditValue = .ExpirationDate
            IdAccount = .IdAccount
            INDsleAccount.Properties.NullText = .NumberNameMainAccount
            Me.CurrencyId(.CurrencyAbbreviation) = .CurrencyId
            If .IdCostCenter IsNot Nothing Then
                IdCostCenter = CInt(.IdCostCenter)
                INDsleCostCenter.Properties.NullText = .DescriptionCostCenter
                INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemCostCenter.AllowHide = False
            Else
                IdCostCenter = Nothing
                INDsleCostCenter.Properties.NullText = String.Empty
                INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemCostCenter.AllowHide = True
            End If

            hasDeductibleIva = If(hasDeductibleIva Is Nothing, False, hasDeductibleIva)
            INDLyDeductibleIva.Enabled = True

            Dim listViewCostDistributionDirectCostDetailIva As New List(Of ViewCostDistributionDirectCostDetailIvaXpo)
            If .TaxRegistration <> 2 Then
                listViewCostDistributionDirectCostDetailIva = Presenter.GetCostDistributionDirectCostDetailIva(accountPayable.Id)
            End If

            ''Se valida que venga de distribucion de elementos del costo para bloquear el campo de iva descontable
            If accountPayable.AccountPayableDetailConcept.Count > 0 AndAlso accountPayable.AccountPayableDetailConcept.FirstOrDefault.IsDirectCost Then
                If hasDeductibleIva IsNot Nothing Then
                    INDLyDeductibleIva.Enabled = False
                End If
            End If

            HandlesDocumentSupport = .HandlesDocumentSupport
            INDSleHandleDocumentSupport.EditValue = .HandlesDocumentSupport
            Comment = .Coments
            accountPayable.AccountPayableDetailConcept.ToList.ForEach(Sub(x) x.HandlesDeferredCausation = False)
            ListAddConcept = accountPayable.AccountPayableDetailConcept.ToList

            'validacion en caso de que tenga iva descontable y tenga un iva registrado 
            For Each item In ListAddConcept
                item.AccountPayabbleDetailChild = New List(Of AccountPayableDetailConcept)

                Dim row As AccountPayableDetailConcept = New AccountPayableDetailConcept

                row.IdAccount = item.IdAccount
                row.NumberNameMainAccount = item.NumberNameMainAccount
                row.IdCostCenter = item.IdCostCenter
                row.DescriptionCostCenter = item.DescriptionCostCenter
                row.Nature = item.Nature

                If Not (item.RateIva > 0 AndAlso item.IvaValue > 0) OrElse item.RateIva Is Nothing Then
                    'se agrega el row regular
                    row.Value = item.Value
                    item.AccountPayabbleDetailChild.Add(row)
                ElseIf (item.RateIva > 0 AndAlso item.IvaValue > 0) Then
                    If Me.TaxRegistration <> 2 OrElse item.Value <> item.IvaValue Then
                        row.Value = If(Me.TaxRegistration = 2 OrElse (Me.TaxRegistration = 4) OrElse (Me.TaxRegistration = 3 AndAlso hasDeductibleIva), item.BaseValue, item.TotalConcept)
                        item.AccountPayabbleDetailChild.Add(row)
                    End If
                End If

                If item.RateIva IsNot Nothing Then
                    ''se valida que la factura posea el control de iva descontable
                    If hasDeductibleIva Is Nothing Then
                        Continue For
                    End If

                    ''Se actualiza el total para que no tome el value sino el total del concepto
                    item.Value = item.TotalConcept
                    Using modelIva As New MGeneralLedgerIVA(Me.Tag)
                        Dim iva = modelIva.GetGeneralLedgerIVAById(Convert.ToInt32(item.RateIva)).ObjectEmbbeded

                        ''en caso de que tenga iva descontable
                        If Me.TaxRegistration = 2 OrElse (TaxRegistration = 3 AndAlso hasDeductibleIva) Then 'IVA descontable / IVA mixto e IVA descontable

                            If item.IvaValue > 0 Then
                                Using modelGeneral As New MAccountPayable(Me.Tag)
                                    Dim accountInfo = modelGeneral.GetMainAccountById(iva.IdAccountPurchaseService)
                                    item.AccountPayabbleDetailChild.Add(New AccountPayableDetailConcept With {
                                        .Value = item.IvaValue,
                                        .IdAccount = iva.IdAccountPurchaseService,
                                        .DescriptionPaymentConcept = item.DescriptionPaymentConcept,
                                        .Nature = 1,
                                        .Detail = $"IVA {iva.Percentage} % - {iva.Name}",
                                        .NumberNameMainAccount = accountInfo.NumberName
                                    })
                                End Using
                            End If

                        ElseIf Me.TaxRegistration = 1 OrElse (TaxRegistration = 3 AndAlso Not hasDeductibleIva) Then 'IVA al costo - control fiscal / IVA mixto con IVA fiscal seleccionada

                            'Item debito
                            Using modelGeneral As New MAccountPayable(Me.Tag)
                                Dim accountInfo = modelGeneral.GetMainAccountById(iva.IdAccountDebitControlFiscal)

                                item.AccountPayabbleDetailChild.Add(New AccountPayableDetailConcept With {
                                    .Value = item.IvaValue,
                                    .IdAccount = iva.IdAccountDebitControlFiscal,
                                    .DescriptionPaymentConcept = item.DescriptionPaymentConcept,
                                    .Nature = 1,
                                    .Detail = $"IVA {iva.Percentage} % - {iva.Name}",
                                .NumberNameMainAccount = accountInfo.NumberName
                                })
                            End Using

                            'Item credito
                            Using modelGeneral As New MAccountPayable(Me.Tag)
                                Dim accountInfo = modelGeneral.GetMainAccountById(iva.IdAccountCreditControlFiscal)

                                item.AccountPayabbleDetailChild.Add(New AccountPayableDetailConcept With {
                                    .Value = item.IvaValue,
                                    .IdAccount = iva.IdAccountCreditControlFiscal,
                                    .DescriptionPaymentConcept = item.DescriptionPaymentConcept,
                                    .Nature = 2,
                                    .Detail = $"IVA {iva.Percentage} % - {iva.Name}",
                                    .NumberNameMainAccount = accountInfo.NumberName
                                })
                            End Using

                        ElseIf Me.TaxRegistration <> 2 Then
                            If item.IvaValue > 0 Then
                                Dim listIva = listViewCostDistributionDirectCostDetailIva.Where(Function(x) x.AccountPayableDetailConceptId = item.Id).ToList()
                                If Not listIva.Any() Then
                                    listIva.Add(New ViewCostDistributionDirectCostDetailIvaXpo With {
                                        .Name = iva.Name,
                                        .Percentage = iva.Percentage,
                                        .IvaValue = item.IvaValue
                                    })
                                End If

                                For Each detailIva In listIva
                                    'IVA al costo se lleva el Iva discriminado en una nueva linea pero a la misma cuenta contable del concepto
                                    item.AccountPayabbleDetailChild.Add(New AccountPayableDetailConcept With {
                                        .IdAccount = item.IdAccount,
                                        .NumberNameMainAccount = item.NumberNameMainAccount,
                                        .IdCostCenter = item.IdCostCenter,
                                        .DescriptionCostCenter = item.DescriptionCostCenter,
                                        .Value = detailIva.IvaValue,
                                        .DescriptionPaymentConcept = item.DescriptionPaymentConcept,
                                        .Nature = item.Nature,
                                        .Detail = $"IVA {detailIva.Percentage} % - {detailIva.Name}"
                                    })
                                Next
                            End If
                        End If
                    End Using
                End If
            Next

            Shares = CInt(.Shares)
            ListAccountPayableShares = accountPayable.AccountPayableShares.ToList
            INDgcConcept.DataSource = Nothing
            INDgcConcept.DataSource = ListAddConcept
            CalculateTotal(ListAddConcept)
            INDgcShares.DataSource = Nothing
            INDgcShares.DataSource = ListAccountPayableShares
            Me.BarraBotones.StatusRecordVisible = True
            If .Status = 1 Then
                Me.BarraBotones.StatusRecord = "1"
            ElseIf .Status = 2 Then
                Me.BarraBotones.StatusRecord = "2"
                INDtxtBillNumber.Properties.ReadOnly = True
            End If
            ValueBill = .InvoiceValue
            ctrTmp.RefreshTotalValues()
            If ListAddConcept IsNot Nothing AndAlso ListAddConcept.Count > 0 Then
                If (From x In ListAddConcept Where x.IsDirectCost = True Select x).Count > 0 Then
                    INDtxtValue.Properties.ReadOnly = True
                End If
            End If

            'Validar si la factura ya fue distribuida
            If INDtxtValue.Properties.ReadOnly = False Then
                Dim obj = Presenter.ListCostDistributionDirectCost(accountPayable.Id)
                If obj IsNot Nothing AndAlso obj.Count > 0 Then
                    INDtxtValue.Properties.ReadOnly = True
                End If
            End If

            If BudgetInterface Then
                BudgetaryEntityId = .BudgetaryEntityId
                INDSleBudgetaryEntityId.Properties.NullText = .BudgetaryEntityDescription
                BudgetaryValidityId = .BudgetaryValidityId
                INDSleBudgetaryValidityId.Properties.NullText = .BudgetaryValidityDescription
                ListAccountPayableCommitment = .AccountPayableCommitments.ToList()
            End If

            DocumentSupportId = .DocumentSupportId
            If .IdEconomicActivity IsNot Nothing Then
                IdEconomicActivity = .IdEconomicActivity
                INDgleEconomicActivity.Properties.NullText = .CodeNameEconomicActivity
            End If
        End With
        _isLoading = False
        CloneEntityDetail()
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Activar) = True
    End Sub

    ''' <summary>
    ''' Clona a el listado de comparacion los detalles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CloneEntityDetail()
        For Each itemDetail As AccountPayableDetailConcept In ListAddConcept
            If ListCompareDetail Is Nothing Then
                ListCompareDetail = New List(Of AccountPayableDetailConcept)
            End If
            ListCompareDetail.Add(itemDetail.CloneEntity)
        Next
    End Sub

    ''' <summary>
    ''' Habilita o deshabilita los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IPopupBills.ActionsOnControls
        Set(value As Boolean)
            INDlyBill.BeginUpdate()

            INDdtBillDate.Enabled = value
            INDseTerm.Enabled = value
            INDdtExpiredDate.Enabled = value
            INDseShares.Enabled = value
            INDtxtValue.Enabled = value
            INDseHours.Enabled = value

            'Grupo información
            INDlygAditionalInformation.Enabled = value
            INDsleAccount.Enabled = value
            If INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDsleCostCenter.Enabled = value
            End If
            INDSleHandleDocumentSupport.Enabled = value
            INDmemoComment.Enabled = value

            'Grupo de conceptos
            INDlygConcept.Enabled = value
            INDbtnAdd.Enabled = value
            INDpceAddConcept.Enabled = value
            INDgcConcept.Enabled = value

            'Grupo Interfaz
            INDlygBudget.Enabled = value
            INDSleBudgetaryEntityId.Enabled = value
            INDSleBudgetaryValidityId.Enabled = value

            INDlyBill.EndUpdate()

            INDtxtBillNumber.Focus()
        End Set
    End Property

    Public _isCleaning As Boolean
    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        INDlyBill.BeginUpdate()
        _isCleaning = True

        BillNumber = String.Empty
        INDdtBillDate.EditValue = Nothing
        Term = 0
        INDdtExpiredDate.EditValue = Nothing
        Shares = 1
        ListAccountPayableShares = Nothing
        ValueBill = 0
        valBill = 0
        Hours = 0

        entity = False

        IdCostCenter = Nothing
        INDsleCostCenter.Properties.NullText = String.Empty
        INDSleHandleDocumentSupport.EditValue = False
        Taxes = 0F
        Me.TotalInvoice = 0F
        Comment = String.Empty

        INDgcConcept.DataSource = Nothing
        ListAddConcept = Nothing

        INDSleDocumentSupportId.EditValue = Nothing
        IdEconomicActivity = Nothing
        INDSleBudgetaryEntityId.EditValue = Nothing
        INDSleBudgetaryEntityId.Properties.NullText = Nothing
        INDSleBudgetaryValidityId.EditValue = Nothing
        INDSleBudgetaryValidityId.Properties.NullText = Nothing
        INDgcCommitmentDetail.DataSource = Nothing
        INDRgDeductibleIva.EditValue = Nothing

        ActionsOnControls = False
        BarraBotones.StatusRecordVisible = False

        _isLoading = False
        _isCleaning = False
        INDlyBill.EndUpdate()
    End Sub

    ''' <summary>
    ''' Metodo que valida si ya existe el numero de factura en la lista
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ValidateBill() As Task

        'Se valida que el número de factura ingresado al menos tenga un número
        Dim cont As Integer = 0 'Establece si en la cadena hay al menos un número
        For i = 0 To BillNumber.Length - 1
            If IsNumeric(BillNumber(i)) Then
                cont += 1
                Exit For
            End If
        Next
        If cont = 0 Then 'No contiene ningun número y retono error
            Mensaje(EeventViewerImages.Advertencia) = "El No. de factura " + BillNumber + " debe tener al menos un número."
            ActionsOnControls = False
            Exit Function
        End If

        Using modelAcc As New MAccountPayable(Tag.ToString)
            Dim account As AccountPayable
            account = Await modelAcc.GetAccountPayableByBillNumber(INDtxtBillNumber.Text, IdSupplier)
            If account.Id = 0 Then
                Dim ban As Boolean = False
                If ListBill IsNot Nothing Then
                    For Each bill As AccountPayable In ListBill
                        If BillNumber = bill.BillNumber Then
                            ban = True
                        End If
                    Next
                End If
                If ban = False Then
                    ActionsOnControls = True
                    Me.BarraBotones.StatusRecordVisible = True
                    Me.BarraBotones.StatusRecord = "1"
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                Else
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmPopupBills_BillNumberExisting", NAME_MODULE)
                    ActionsOnControls = False
                End If

                If BudgetInterface Then
                    Presenter.InitializeBudgetaryEntity()
                    Me.SetFirstOrDefaultEntity()
                End If

                INDtxtValue.Focus()
            Else
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmPopupBills_BillNumberExistDataBase", NAME_MODULE)
                ActionsOnControls = False
            End If
        End Using
    End Function

    ''' <summary>
    ''' funcion para obtener la sumatoria del valor base de todos los conceptos agregados que son tipo resultado para calcular las retenciones
    ''' </summary>
    Private Sub GetValueConceptsResult()
        'ListAddConcept
        baseValueConceptResult = 0
        baseValueIvaRetention = 0
        For Each concept In ListAddConcept
            Using model As New MPUC(CStr(Tag))
                Dim puc As MainAccounts
                puc = model.GetAccountId(concept.IdAccount)
                ''si el concepto es tipo resultado o difiere causacion o es tipo balance de naturalez debito
                If puc.MainAccountClasses.Type = 2 Or concept.DeferredCausation Or (puc.MainAccountClasses.Type = 1 And puc.MainAccountClasses.Nature = 1) Then
                    baseValueConceptResult += concept.BaseValue
                End If
                '' se obtienel valor total de la sumatoria de los iva de los conceptos que ya estan agregados
                If concept?.IvaValue IsNot Nothing Then
                    baseValueIvaRetention += concept.IvaValue
                Else
                    baseValueIvaRetention += 0
                End If
            End Using
        Next
    End Sub

    ''' <summary>
    ''' se agrega cada retencion seleccionada en el popup de conceptos no agregados 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnAddConcepts(sender As Object, e As List(Of Infrastructure.Data.Xpo.CommonRepository.ConceptsAccountPayableXpo))
        Try
            CtrConcepts1.Entity = False
            GetValueConceptsResult()
            For Each item In e
                AccountPayableDetail = New AccountPayableDetailConcept
                Dim IdAccountByType = Nothing
                With AccountPayableDetail
                    .IdConceptAccountPayable = item.Id
                    .DescriptionPaymentConcept = item.CodeName
                    If item.ConceptType = 1 Then
                        IdAccountByType = item.ThreeEightThreeAccountId
                        .IdAccount = item.ThreeEightThreeAccountId.Id
                        .NumberNameMainAccount = item.ThreeEightThreeAccountId.NumberName
                        .IdRetentionConcept = item.ThreeEightThreeRetentionConceptId.Id
                        .Percentage = item.ThreeEightThreeRetentionConceptId.Rate
                        .DescriptionRetentionConcept = item.ThreeEightThreeRetentionConceptId.CodeName
                    Else
                        IdAccountByType = item.IdAccount
                        .IdAccount = item.IdAccount.Id
                        .NumberNameMainAccount = item.IdAccount.NumberName
                        .IdRetentionConcept = item.RetentionConceptId.Id
                        .Percentage = item.RetentionConceptId.Rate
                        .DescriptionRetentionConcept = item.RetentionConceptId.CodeName
                    End If
                    .IdThirdParty = ThirdPartyId

                    Using model As New MThirdParty(CStr(Tag))
                        Dim tp = model.GetThirdPartyByIdSimple(ThirdPartyId)
                        .DescriptionThirdParty = tp?.Nit & " - " & tp?.Name
                    End Using

                    .DeferredCausation = False
                    .HandlesDeferredCausation = False

                    .IdCostCenter = Nothing
                    .DescriptionCostCenter = String.Empty
                    .Detail = item.Name
                    .BillingValue = ValueBill

                    .Nature = CByte(2)
					'si el concepto es tipo reteiva sus valores se calculan de la sumatoria de los iva de los concpetos
					If IdAccountByType.RetencionType = 2 Then
						'' Para la Distribución de elementos al costo, con iva fiscal, es una sola linea con el valor
						If ListAddConcept.Any(Function(x) x.IsDirectCost) Then
							If _taxRegistration = 1 And DeductibleIva = False Then
								baseValueIvaRetention = ListAddConcept.Where(Function(x) x.Nature = 1 And x.IdCostCenter Is Nothing And x.IsDirectCost).Sum(Function(d) d.Value)
							End If
						End If
						.Value = baseValueIvaRetention * (.Percentage / 100) '' el base * percentaje
						.BaseValue = baseValueIvaRetention
						.TotalConcept = baseValueIvaRetention
					Else
						''si es otro tipoi de concepto su base es la sumatoria de los conceptos sin el iva
						.BaseValue = baseValueConceptResult
                        .Value = baseValueConceptResult * (.Percentage / 100) '' el base * percentaje
                        .TotalConcept = baseValueConceptResult
                    End If
                    .RateIva = Nothing
                    .IvaValue = 0
                    .AccountPayabbleDetailChild = New List(Of AccountPayableDetailConcept)
                    ''se agrega el row regular
                    Dim row As AccountPayableDetailConcept = New AccountPayableDetailConcept
                    row.Value = .Value

                    row.IdAccount = IdAccountByType.Id
                    row.DescriptionCostCenter = ""
                    row.NumberNameMainAccount = IdAccountByType.NumberName
                    row.Nature = .Nature
                    .AccountPayabbleDetailChild.Add(row)
                End With

                ''se agrega el concepto a la rejilla
                AddConcept(AccountPayableDetail)
            Next
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        End Try
    End Sub

    ''' <summary>
    ''' Agrega una nueva factura
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddBill()
        If Not ValidateControls() Then
            Exit Sub
        End If

        If Not ValidateFields() Then
            Exit Sub
        End If

        If Not ValidateBillNumber() Then
            Exit Sub
        End If

        If Not ValidateInterfaceBudget() Then
            Exit Sub
        End If

        If listConceptCxpXpo IsNot Nothing AndAlso listConceptCxpXpo.Any() Then
            If ListAddConcept IsNot Nothing AndAlso ListAddConcept.Any() Then

                Dim banConcept As Boolean
                For Each itemConcept In listConceptCxpXpo
                    banConcept = True
                    Dim cont = ListAddConcept.FindAll(Function(item) item.IdConceptAccountPayable = itemConcept.Id).Count
                    If cont = 0 Then
                        banConcept = False
                        Exit For
                    End If
                Next

                If Not banConcept Then
                    Using Formulario As New FrmValidateSelectConcepts(ListAddConcept, listConceptCxpXpo)
                        ''metodo para obtener los datos de los conceptos seleccionados en el popup
                        AddHandler Formulario.AddConcepts, AddressOf ReturnAddConcepts
                        Formulario.ToolBar.Dock = DockStyle.None
                        Formulario.ViewModeEditHold = True
                        Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                        Dim size As System.Drawing.Size
                        size.Width = 450
                        size.Height = 440
                        Formulario.Size = size
                        Dim transparent As New FrmTransparent(Formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog()

                        If Not Formulario.Acept Then
                            Exit Sub
                        End If
                    End Using
                End If
            End If
        End If

        Dim ban As Boolean = False
        If ListDeferredCausation IsNot Nothing Then
            Dim banCD As Boolean = False
            For Each itemCD As DeferredCausation In ListDeferredCausation
                If itemCD.BillNumber = BillNumber Then
                    banCD = True
                    Exit For
                End If
            Next

            If banCD Then
                For Each itemD As AccountPayableDetailConcept In ListAddConcept
                    If itemD.HandlesDeferredCausation Then
                        ban = True
                    End If
                Next
            End If
        End If

        Dim banAdd As Boolean = True
        If ban Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteDeferredCausation", NAME_MODULE), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                banAdd = False
            Else
                HandlesPushTrue = True
            End If
        End If

        If banAdd Then
            AssigningValues()
            BanClose = True
            Close()
        End If
    End Sub

    ''' <summary>
    ''' Metodo para asignar valores
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With accountPayable
            .BillNumber = BillNumber
            .BillDate = CDate(INDdtBillDate.EditValue)
            .Shares = Shares
            .Term = Term
            .Hours = Hours
            .ExpirationDate = CDate(INDdtExpiredDate.EditValue)
            .IdAccount = IdAccount
            .NumberNameMainAccount = INDsleAccount.Text
            .CurrencyId = Me.CurrencyId
            .CurrencyAbbreviation = Me.CurrencyAbbreviation
            If INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .IdCostCenter = IdCostCenter
                .DescriptionCostCenter = INDsleCostCenter.Text
            Else
                .IdCostCenter = Nothing
                .DescriptionCostCenter = String.Empty
            End If
            .Coments = Comment
            .InvoiceValue = ValueBill
            .Value = valBill
            If .Id = 0 Then
                .Balance = valBill
            End If
            .Status = 1
            .HandlesDocumentSupport = INDSleHandleDocumentSupport.EditValue
            .DocumentSupportId = INDSleDocumentSupportId.EditValue
            .DeductibleIva = hasDeductibleIva
            'Se valida si el campo de la actividad económica está activo para asignar su valor correspondiente
            If INDlyEconomicActivity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .IdEconomicActivity = IdEconomicActivity
                .CodeNameEconomicActivity = INDgleEconomicActivity.Text
            Else
                .IdEconomicActivity = Nothing
                .CodeNameEconomicActivity = Nothing
            End If
            ''se valida que si es de tipo mixto tome lo que selecciono
            If _taxRegistration = 3 Then
                If DeductibleIva Then
                    .TaxRegistration = 2 ''iva descontable
                Else
                    .TaxRegistration = 1 ''iva costo control fiscal
                End If
            Else
                .TaxRegistration = _taxRegistration
            End If
            If INDlygConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso ListAddConcept IsNot Nothing Then
                For Each d As AccountPayableDetailConcept In ListAddConcept
                    If d.ChangeTracker.State = ObjectState.Added Then
                        'd.HandlesDeferredCausation = False
                        accountPayable.AccountPayableDetailConcept.Add(d)
                    ElseIf d.ChangeTracker.State = ObjectState.Modified Then
                        'd.HandlesDeferredCausation = False
                        d.MarkAsModified()
                    End If
                Next

                If listDeleteDetailConcept IsNot Nothing Then
                    For Each d As AccountPayableDetailConcept In listDeleteDetailConcept
                        For Each item As AccountPayableDetailConcept In accountPayable.AccountPayableDetailConcept
                            If d.Id = item.Id Then

                                If item.AccountPayableDetailConceptLiquidation IsNot Nothing AndAlso item.AccountPayableDetailConceptLiquidation.Count > 0 Then
                                    item.AccountPayableDetailConceptLiquidation(0).MarkAsDeleted()
                                End If

                                item.MarkAsDeleted()
                                Exit For
                            End If
                        Next
                    Next
                End If

                If listDelete IsNot Nothing Then
                    If listDelete.Count <> 0 Then
                        For Each item As AccountPayableShares In listDelete
                            accountPayable.AccountPayableShares.Add(item)
                        Next
                    End If
                End If

                If accountPayable.AccountPayableShares IsNot Nothing AndAlso accountPayable.AccountPayableShares.Count > 0 Then
                    While (From x In accountPayable.AccountPayableShares Where x.Id = 0).Count > 0
                        If accountPayable.AccountPayableShares(0).Id = 0 Then
                            accountPayable.AccountPayableShares.Remove(accountPayable.AccountPayableShares(0))
                        End If
                    End While
                End If
                If ListAccountPayableShares.Count > 0 Then
                    For Each itemShares As AccountPayableShares In ListAccountPayableShares
                        accountPayable.AccountPayableShares.Add(itemShares)
                    Next
                End If

                .Balance = (From s In accountPayable.AccountPayableShares Where s.ChangeTracker.State <> ObjectState.Deleted Select s.Balance).Sum()
            End If

            If BudgetInterface Then
                If ListAccountPayableCommitment IsNot Nothing Then
                    accountPayable.BudgetaryEntityId = BudgetaryEntityId
                    accountPayable.BudgetaryEntityDescription = INDSleBudgetaryEntityId.Text
                    accountPayable.BudgetaryValidityId = BudgetaryValidityId
                    accountPayable.BudgetaryValidityDescription = INDSleBudgetaryValidityId.Text

                    For Each AccountPayableCommitment In ListAccountPayableCommitment
                        If AccountPayableCommitment.Value > 0 Then
                            If AccountPayableCommitment.ChangeTracker.State = ObjectState.Added Then
                                accountPayable.AccountPayableCommitments.Add(AccountPayableCommitment)
                            ElseIf AccountPayableCommitment.ChangeTracker.State = ObjectState.Modified Then
                                AccountPayableCommitment.MarkAsModified()
                            End If
                        Else
                            AccountPayableCommitment.MarkAsDeleted()
                        End If
                    Next
                End If
            End If

            If listDeleteAccountPayableCommitment IsNot Nothing Then
                For Each AccountPayableCommitment In listDeleteAccountPayableCommitment
                    AccountPayableCommitment.MarkAsDeleted()
                    .AccountPayableCommitments.Add(AccountPayableCommitment)
                Next
            End If
        End With

        ValidateRecalculateDeferredCausation()
    End Sub

    ''' <summary>
    ''' Valida si hay que recalcular la causacion diferida
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateRecalculateDeferredCausation()
        Dim cont = accountPayable.AccountPayableDetailConcept.ToList.FindAll(Function(item) item.HandlesDeferredCausation = True AndAlso item.DeferredCausation = True).Count
        If cont > 0 Then
            RecaulculateDeferredCausation = True
        Else
            If ListCompareDetail IsNot Nothing AndAlso ListCompareDetail.Count > 0 Then
                Dim contCompare1 = ListCompareDetail.FindAll(Function(item) item.HandlesDeferredCausation = False AndAlso item.DeferredCausation = True).Count
                Dim contCompare2 = accountPayable.AccountPayableDetailConcept.ToList.FindAll(Function(item) item.HandlesDeferredCausation = False AndAlso item.DeferredCausation = True).Count
                If contCompare1 <> contCompare2 Then
                    RecaulculateDeferredCausation = True
                Else
                    RecaulculateDeferredCausation = False
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que valida los controles
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateFields() As Boolean
        Dim listErrors As New StringBuilder
        If ValueBill = 0 OrElse ValueBill = Nothing Then
            listErrors.AppendLine(ResourceManager.GetString("DontValueBill", NAME_MODULE))
        End If

        If Shares = 0 Then
            listErrors.AppendLine(ResourceManager.GetString("DontShare", NAME_MODULE))
        End If

        If Shares > 36 Then
            listErrors.AppendLine(ResourceManager.GetString("ValueShareIncorrect", NAME_MODULE))
        End If

        If INDSleHandleDocumentSupport.EditValue Is Nothing Then
            listErrors.AppendLine("Debe seleccionar una resolución de documento soporte")
        End If
        If ListAddConcept IsNot Nothing AndAlso ListAddConcept.Any Then
            If Math.Round(Me.TotalInvoice, 2) <> Me.ValueBill Then
                listErrors.AppendLine("El valor total no corresponde al facturado")
            End If
        End If

        'Condición que evita un campo nulo en la actividad económica vinculada a la factura
        If IsEconomicActivity AndAlso IdEconomicActivity Is Nothing Then
            listErrors.AppendLine("Se debe diligenciar el campo Actividad Económica Generadora de Ingreso")
        End If

        If listErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Método que valida la factura de la cxp
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateBillNumber() As Boolean
        Dim cont As Integer = 0

        For i = 0 To BillNumber.Length - 1
            If IsNumeric(BillNumber(i)) Then
                cont += 1
                Exit For
            End If
        Next

        If cont = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El No. de factura " + BillNumber + " debe tener al menos un número."
            Return False
        End If

        Dim account = Presenter.GetAccountPayableByCompareBillNumber(INDtxtBillNumber.Text, IdSupplier, accountPayable.Id)

        If account Is Nothing Then
            If ListBill IsNot Nothing AndAlso ListBill.Count > 0 Then
                If (From x In ListBill Where x.BillNumber = BillNumber AndAlso BillNumber <> accountPayable.BillNumber Select x).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmPopupBills_BillNumberExisting", NAME_MODULE)
                    Return False
                End If
            End If
            Return True
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmPopupBills_BillNumberExistDataBase", NAME_MODULE)
            Return False
        End If
    End Function

    ''' <summary>
    ''' Metodo que valida la interfaz presupuestal
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateInterfaceBudget() As Boolean
        If BudgetInterface Then
            Dim listErrors As New StringBuilder

            If ObligationBudgetInterface Then
                If ListAccountPayableCommitment Is Nothing OrElse Not ListAccountPayableCommitment.Any(Function(d) d.Value > 0) Then
                    listErrors.AppendLine("Debe agregar al menos un compromiso con el cual realizar la obligación presupuestal")
                End If
            End If

            If ListAccountPayableCommitment IsNot Nothing AndAlso ListAccountPayableCommitment.Any Then
                Dim invoiceValue As Decimal = 0
                Dim obligationValue As Decimal = ListAccountPayableCommitment.Where(Function(d) d.Value > 0).Sum(Function(d) d.Value)
                If ListAddConcept IsNot Nothing Then
                    invoiceValue = ListAddConcept.Where(Function(d) d.Nature = 1).Sum(Function(d) d.Value)
                    If Not ObligationDebitValue Then
                        invoiceValue = invoiceValue - ListAddConcept.Where(Function(d) d.Nature = 2).Sum(Function(d) d.Value)
                    End If
                End If
                If Not ObligationBudgetInterface AndAlso obligationValue = 0 Then
                    If listDeleteAccountPayableCommitment Is Nothing Then
                        listDeleteAccountPayableCommitment = New List(Of AccountPayableCommitments)
                    End If

                    For Each AccountPayableCommitment In ListAccountPayableCommitment
                        If AccountPayableCommitment.Id > 0 Then
                            listDeleteAccountPayableCommitment.Add(AccountPayableCommitment)
                        End If
                    Next
                ElseIf obligationValue > invoiceValue Then
                    listErrors.AppendLine("La suma de los valores de los detalles presupuestales superan el valor de la factura")
                ElseIf obligationValue < invoiceValue Then
                    listErrors.AppendLine("La suma de los valores de los detalles presupuestales son menores al valor de la factura")
                End If
            End If

            If listErrors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
                Return False
            End If
        End If

        Return True
    End Function

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
                If ListAccountPayableCommitment IsNot Nothing AndAlso ListAccountPayableCommitment.Any() Then
                    If listDeleteAccountPayableCommitment Is Nothing Then
                        listDeleteAccountPayableCommitment = New List(Of AccountPayableCommitments)
                    End If

                    For Each AccountPayableCommitment In ListAccountPayableCommitment
                        If AccountPayableCommitment.Id > 0 Then
                            listDeleteAccountPayableCommitment.Add(AccountPayableCommitment)
                        End If
                    Next
                End If
                ListAccountPayableCommitment = Nothing
                accountPayable.AccountPayableCommitments.Clear()
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

    ' <summary>
    ' Metodo para seleccionar por defecto el primer registro si solo hay uno en Resoluciones Documento soporte
    ' </summary>
    ' <remarks></remarks>
    Sub SetFirstOrDefaultResolution()

    End Sub

    ''' <summary>
    ''' Eventos para asignar la relacion y el dataSource a la sub-rejilla 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub viewConcept_MasterRowGetChildList(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventArgs) Handles viewConcept.MasterRowGetChildList
        If e.ChildList Is Nothing Then
            Dim accountPayableDetail = viewConcept.GetFocusedObject(Of AccountPayableDetailConcept)
            If ListAddConcept IsNot Nothing Then
                If accountPayableDetail.Id > 0 Then
                    e.ChildList = ListAddConcept.ToList().Where(Function(d) d.Id = accountPayableDetail.Id).FirstOrDefault.AccountPayabbleDetailChild.ToList()
                Else
                    e.ChildList = accountPayableDetail.AccountPayabbleDetailChild.ToList()
                End If
            End If
        End If
    End Sub

    Private Sub viewConcept_MasterRowGetRelationName(sender As Object, e As MasterRowGetRelationNameEventArgs) Handles viewConcept.MasterRowGetRelationName
        e.RelationName = "viewConcept"
    End Sub

    Private Sub viewConceptDetail_MasterRowGetRelationCount(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventArgs) Handles viewConcept.MasterRowGetRelationCount
        e.RelationCount = 1
    End Sub

    Private Sub viewConcept_MasterRowEmpty(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowEmptyEventArgs) Handles viewConcept.MasterRowEmpty
        e.IsEmpty = False
    End Sub

    Private Sub INDRgDeductibleIva_EditValueChanged(sender As Object, e As EventArgs) Handles INDRgDeductibleIva.EditValueChanged
        hasDeductibleIva = DeductibleIva
    End Sub

    ''' <summary>
    ''' Metodo para calcular el total IVA y valor total
    ''' </summary>
    ''' <param name="ListAddConcenpt"></param>
    Private Sub CalculateTotal(ListAddConcenpt As List(Of AccountPayableDetailConcept))
        If ListAddConcept IsNot Nothing Then
            Dim ValorIva, Valor As Decimal

            For Each item In ListAddConcept

                If item.IvaValue = item.TotalConcept Then
                    Continue For
                End If

                If item.IdRetentionConcept IsNot Nothing OrElse Not item.IdRetentionConcept > 0 Then
                    Continue For
                End If

                If item.Nature = 2 Then
                    Valor -= item.BaseValue
                    Continue For
                End If

                If item.Nature = 1 Then
                    If item.IsDirectCost = True Then
                        If item.IdCostCenter IsNot Nothing Then
                            Valor += item.Value
                        End If
                    ElseIf item?.BaseValue = 0 AndAlso item?.IvaValue Is Nothing Then
                        Valor += item.Value
                    Else
                        If Not (item.RateIva > 0 AndAlso item.IvaValue > 0) OrElse item.RateIva Is Nothing Then
                            If item.Percentage Is Nothing Then
                                Valor += item.Value
                            ElseIf item.Percentage IsNot Nothing And item.IdRetentionConcept Is Nothing Then
                                ValorIva += item.Value
                            End If
                        Else
                            Valor += item.BaseValue
                        End If
                    End If
                End If
            Next

            If ValorIva = 0 Then
                ValorIva = ListAddConcept.Where(Function(x) x.Nature = 1).Sum(Function(d) d.IvaValue)
            End If

            If ListAddConcenpt.Any(Function(x) x.IsDirectCost) Then
                Dim IvaDirectCost = ListAddConcept.Where(Function(x) x.Nature = 1 And x.IdCostCenter Is Nothing And x.IsDirectCost).Sum(Function(d) d.Value)
                ValorIva += IvaDirectCost
                Me.TotalInvoice = IIf(Me.TaxRegistration = 2, Valor + ValorIva, Valor)
            Else
                Me.TotalInvoice = Valor + ValorIva
            End If

            Me.Taxes = ValorIva
        End If
    End Sub

    ''' <summary>
    ''' establece a los controles con el formato de la moneda seleccionada
    ''' </summary>
    Public Sub SetCurrencyCultureUI(Abbreviation As String)
        If String.IsNullOrEmpty(Abbreviation) Then
            Exit Sub
        End If
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = Abbreviation.GetNumberFormat()
        INDseIva.Properties.Mask.Culture = _culture
        INDseTotal.Properties.Mask.Culture = _culture
        INDtxtValue.Properties.Mask.Culture = _culture
        INDColValue = Window.Utils.FormatGrid(Me.INDColValue, Abbreviation)
        GridColumn988 = Window.Utils.FormatGrid(GridColumn988, Abbreviation)
        ctrTmp.CurrencyAbbreviation = Abbreviation
        ctrTmp.RefreshTotalValues()
    End Sub
#End Region

#Region "Barra Botones"

    ''' <summary>
    ''' Evento que se dispara cuando presiona clic en el boton deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
        INDdtBillDate.EditValue = GetDateServer()
        If Supplier IsNot Nothing Then
            Term = Supplier.TimeLimitDays
        End If
    End Sub

    ''' <summary>
    ''' Load de la barra de botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))

        Dim _presenter = New PAccountPayable()
        Dim listPermission As List(Of Integer) = _presenter.Permission(BarraBotones.PermissionsForm)
        keyCausation = listPermission(0)
    End Sub

#End Region

End Class
