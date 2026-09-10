'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Carlos Ernesto Córdoba
' Created          : 12-07-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Text
Imports DevExpress.Utils
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Presentation.Accounting
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Billing.MVP
Imports Presentation.Common
Imports Presentation.Common.MVP
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Payments.MVP
Imports Presentation.Portfolio.MVP
Imports Presentation.Treasury.MVP

#End Region

Public Class PopupCashReceiptConcept

#Region "BUILDER"
    Sub New(_cashReceiptDetail As CashReceiptDetails, _listConcept As List(Of CashReceiptDetails), _portfolioAdvance As PortfolioAdvance)
        InitializeComponent()
        If _cashReceiptDetail IsNot Nothing Then
            INDBtnOk.Text = ResourceManager.GetString("Edit")
            If _cashReceiptDetail.CashReceiptConceptAffectation = 2 Then
                portfolioAdvance = _portfolioAdvance
            End If
            editModeForm = True
            cashReceiptsDetails = _cashReceiptDetail
        End If
        If _listConcept IsNot Nothing Then
            For Each item In _listConcept
                listConcept.Add(item)
            Next
        End If
    End Sub
#End Region

#Region "PUBLIC EVENTS"
    ''' <summary>
    ''' evento publico para agregar un concepto de recibo de caja
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddCashReceiptConcept(sender As Object, e As AddCashReceiptConceptEventArgs)
    ''' <summary>
    ''' evento para poner en null la entidad del detalle del recibo de caja si se esta editando
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event CashReceiptsDetailsNull(sender As Object, e As EventArgs)
#End Region

#Region "GLOBALS"
    ''' <summary>
    ''' objeto de la entidad de ingreso de crystal
    ''' </summary>
    ''' <remarks></remarks>
    Dim admission As Object
    ''' <summary>
    ''' bandera para saber si se esta editando en el popup
    ''' </summary>
    ''' <remarks></remarks>
    Dim editModePopups As Boolean
    ''' <summary>
    ''' bandera para saber si se esta editando en el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim editModeForm As Boolean

    Private listConcept As List(Of CashReceiptDetails) = New List(Of CashReceiptDetails)
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Treasury"
    ''' <summary>
    ''' variable para asignar el tag del formulario donde esta contenido el control de usuario
    ''' </summary>
    Dim _tag As String
    ''' <summary>
    ''' entidad que representa los conceptos de retenciones
    ''' </summary>
    ''' <remarks></remarks>
    Dim retentionConcept As RetentionConcepts
    ''' <summary>
    ''' representa la entidad del detalle del recibo de caja
    ''' </summary>
    ''' <remarks></remarks>
    Dim cashReceiptsDetails As CashReceiptDetails
    ''' <summary>
    ''' representa la entidad de conceptos de recibo de caja
    ''' </summary>
    ''' <remarks></remarks>
    Dim cashReceiptConcept As CashReceiptConcepts
    ''' <summary>
    ''' entidad de las facturas
    ''' </summary>
    ''' <remarks></remarks>
    Dim accountReceivable As AccountReceivable
    ''' <summary>
    ''' listadop de las facturas 
    ''' </summary>
    ''' <remarks></remarks>
    Dim listAccountReceivable As List(Of AccountReceivable)


    ''' <summary>
    ''' entidad que representa el anticipo de cartera
    ''' </summary>
    ''' <remarks></remarks>
    Dim portfolioAdvance As PortfolioAdvance
    ''' <summary>
    ''' listado par almacenar las cuotas que se van a eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listDeleteShares As List(Of CashReceiptAccountReceivable)
    ''' <summary>
    ''' entidad de anticipos de proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim advancePayment As AdvancePayments
    ''' <summary>
    ''' entidad donde se relaciona el detalle del recibo de caja con el anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Dim cashReceiptAdvancePayment As CashReceiptAdvancePayment
    ''' <summary>
    ''' listado de la relacion del detalle del recibo de caja con el anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Dim listCashReceiptAdvancePayment As List(Of CashReceiptAdvancePayment)
    ''' <summary>
    ''' listado de la relacion del detalle del recibo de caja con el anticipo para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listCashReceiptAdvancePaymentDelete As List(Of CashReceiptAdvancePayment)

    ''' <summary>
    ''' variable que obtiene las edades de cartera
    ''' </summary>
    Private _agesPortfolio As List(Of AgesPortfolio)
    ''' <summary>
    ''' variable que contiene la fecha del servidor
    ''' </summary>
    Private _serverDate As Date

    ''' <summary>
    ''' Redondeo al calcular conversión
    ''' </summary>
    Private _decimals As Integer = 2
#End Region

#Region "PROPERTIES"
    ''' <summary>
    ''' id de la unidad operativa seleccionada en el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private idOperatingUnit As Integer
    Public Property OperatingUnitId As Integer
        Get
            Return idOperatingUnit
        End Get
        Set(value As Integer)
            idOperatingUnit = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece los terceros
    ''' </summary>
    ''' <value>
    ''' The third party xpo.
    ''' </value>
    Property ThirdPartyXPO As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDSleThirdParty.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleThirdParty.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece los terceros beneficiarios
    ''' </summary>
    ''' <value>
    ''' The third party xpo.
    ''' </value>
    Property ThirdPartyBeneficiaryXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleThirdPartyBeneficiary.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleThirdPartyBeneficiary.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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

    ''' <summary>
    ''' propiedad publica para el tag del formulario
    ''' </summary>
    ''' <value>
    ''' The tag form.
    ''' </value>
    Public Property TagForm As String
        Get
            Return _tag
        End Get
        Set(value As String)
            _tag = value
        End Set
    End Property
    ''' <summary>
    '''propiedad de solo escritura para establecer el id del tercero 
    ''' </summary>
    ''' <value>
    ''' The name third party.
    ''' </value>
    Private _idThirdPartyOriginalValue As Integer?
    Public Property IdThirdPartyOriginalValue As Integer?
        Get
            Return _idThirdPartyOriginalValue
        End Get
        Set(value As Integer?)
            _idThirdPartyOriginalValue = value
        End Set
    End Property
    ''' <summary>
    '''propiedad de solo escritura para establecer el id del tercero 
    ''' </summary>
    ''' <value>
    ''' The name third party.
    ''' </value>
    Public WriteOnly Property IdThirdParty As Integer?
        Set(value As Integer?)
            INDSleThirdParty.EditValue = value
            IdThirdPartyOriginalValue = value
        End Set
    End Property
    ''' <summary>
    '''propiedad de solo escritura para establecer el nombre del tercero 
    ''' </summary>
    ''' <value>
    ''' The name third party.
    ''' </value>
    Private _nameThirdPartyOriginalValue As String
    Public Property NameThirdPartyOriginalValue As String
        Get
            Return _nameThirdPartyOriginalValue
        End Get
        Set(value As String)
            _nameThirdPartyOriginalValue = value
        End Set
    End Property
    ''' <summary>
    '''propiedad de solo escritura para establecer el nombre del tercero 
    ''' </summary>
    ''' <value>
    ''' The name third party.
    ''' </value>
    Public WriteOnly Property NameThirdParty As String
        Set(value As String)
            INDSleThirdParty.Properties.NullText = value
            NameThirdPartyOriginalValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece los conceptos de recibos de caja
    ''' </summary>
    ''' <value>
    ''' The cash receipt concept xpo.
    ''' </value>
    Property CashReceiptConceptXPO As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDSleCashReceiptConcept.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCashReceiptConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece los centros de costo
    ''' </summary>
    ''' <value>
    ''' The cost center xpo.
    ''' </value>
    Property CostCenterXPO As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDSleCostCenter.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece los conceptos de retencion
    ''' </summary>
    ''' <value>
    ''' The retention concept xpo.
    ''' </value>
    Property RetentionConceptXPO As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDSleRetentionConcept.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleRetentionConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene y establece las facturas del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property accountReceivableXPO As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDSleBills.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleBills.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene los anticipos del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AdvancePaymentXPO As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDSleAdvancePayment.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleAdvancePayment.Properties.DataSource = value
        End Set
    End Property

    Property AccountPayableXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleAccountPayable.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleAccountPayable.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property CashFlowConceptCodeName() As String
        Get
            Return INDLbCashFlowConcept.Text
        End Get
        Set(ByVal value As String)
            INDLbCashFlowConcept.Text = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property CashFlowConceptId() As Integer
        Get
            Return INDLbCashFlowConcept.Tag
        End Get
        Set(ByVal value As Integer)
            INDLbCashFlowConcept.Tag = value
        End Set
    End Property

    Public Property cashReceiptsCurrencyId As Integer

    Public Property cashReceiptsCurrencyAbrreviation As String

    ''' <summary>
    ''' Indica si el recibo de caja está confirmado/anulado para modo visualización
    ''' </summary>
    Public Property ConfirmStatus As Boolean = False

    ''' <summary>
    ''' valor anticipo
    ''' </summary>
    ''' <returns></returns>
    Private Property PortfolioAdvancedValue As Decimal
        Get
            Return INDTxtPortfolioAdvance.EditValue
        End Get
        Set(value As Decimal)
            INDTxtPortfolioAdvance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Tercero beneficiario
    ''' </summary>
    ''' <returns></returns>
    Private Property ThirdPartyBeneficiaryId As Integer?
        Get
            Return INDSleThirdPartyBeneficiary.EditValue
        End Get
        Set(value As Integer?)
            INDSleThirdPartyBeneficiary.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Moneda el popup del detalle
    ''' </summary>
    ''' <returns></returns>
    Private Property CurrencyDetailId As Integer?
        Get
            Return INDSleCurrency.EditValue
        End Get
        Set(value As Integer?)
            INDSleCurrency.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que establece el valor del control del reintegro de anticipos
    ''' </summary>
    ''' <returns></returns>
    Private Property RepaymentValue As Decimal
        Get
            Return INDTxtRepaymentValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtRepaymentValue.EditValue = value
        End Set
    End Property

#Region "Propiedades del control facturas"

    Private Property ValueBills As Decimal
        Get
            Return INDTxtValueBills.EditValue
        End Get
        Set(value As Decimal)
            INDTxtValueBills.EditValue = value
        End Set
    End Property

    Private Property BalanceBills As Decimal
        Get
            Return INDTxtBalanceBills.EditValue
        End Get
        Set(value As Decimal)
            INDTxtBalanceBills.EditValue = value
        End Set
    End Property

    Private Property ConceptValue As Decimal
        Get
            Return INDTxtConceptValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtConceptValue.EditValue = value
        End Set
    End Property

    Private Property BillValue As Decimal
        Get
            Return INDTxtBillValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtBillValue.EditValue = value
        End Set
    End Property
#End Region
#End Region

#Region "PRIVATE METHODS"
    Private Function SetAgeBills(expiredDate As Date) As Integer
        Dim colorAge As Integer = Convert.ToInt32(ePortfolioAge.ColorDefault)
        If _agesPortfolio Is Nothing Then
            Dim setting As SettingPortfolio
            Using model As New MSettingPortfolio(TagForm)
                setting = model.GetSettingPortfolioByIdOperatingUnit(OperatingUnitId)
                If setting IsNot Nothing AndAlso setting.Id = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se puede obtener la edad de la cartera porque no existen parametros para esta unidad operativa"
                    Return colorAge
                End If
            End Using
            Using Model As New MAgesPortfolio(Me.Tag)
                _agesPortfolio = (Model.ListAgesPortfolioByIdSettingPortfolio(setting.Id))
            End Using
            _serverDate = Me.GetDateServer
        End If
        Dim daysExpire As Integer = (CDate(expiredDate) - _serverDate).TotalDays
        If daysExpire > 0 Then
            colorAge = (From ap In _agesPortfolio Where ap.InitialRange <= daysExpire And ap.EndRange >= daysExpire Select ap.Color).FirstOrDefault()
        End If
        Return colorAge
    End Function

    ''' <summary>
    ''' inicializa la consulta de conceptos de recibos de caja
    ''' </summary>
    Private Sub InitializeCashReceiptConceptXPO()
        If Not DesignMode Then
            Dim filter() As Object = {True}
            Using model As New MBusqueda()
                CashReceiptConceptXPO = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCashReceiptConcept, filter)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' inicializa la consulta de centros de costo
    ''' </summary>
    Private Sub InitializeCostCenterXPO()
        If Not DesignMode Then
            Using model As New MBusqueda
                CostCenterXPO = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetCostCenterByState, "True")
            End Using
        End If
    End Sub

    ''' <summary>
    ''' inicializa la consulta de conceptos de retencion
    ''' </summary>
    Private Sub InitializeRetentionConceptXPO()
        If Not DesignMode Then
            Using model As New MBusqueda
                RetentionConceptXPO = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListRetentionConceptByStatus, "True")
            End Using
        End If
    End Sub

    ''' <summary>
    ''' metodo para consultar el tercero
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeThirdPartyXPO()
        If Not DesignMode Then
            Using model As New MBusqueda
                ThirdPartyXPO = model.ConsultarEntidades(eDataSource.ThirdParty)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' metodo para consultar el tercero
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeThirdPartyBeneficiaryXPO()
        If Not DesignMode Then
            Using model As New MBusqueda
                ThirdPartyBeneficiaryXPO = model.ConsultarEntidades(eDataSource.ThirdParty)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' obtiene las facturas del tercero
    ''' </summary>
    ''' <param name="filter"></param>
    ''' <remarks></remarks>
    Private Sub GetAccountReceivable(ParamArray filter() As Object)
        Using model As New MBusqueda
            accountReceivableXPO = model.ConsultarEntidades(eDataSource.ListPortfolioAccountReceivableByCashReceipt, filter)
        End Using
    End Sub

    ''' <summary>
    ''' obtiene los anticipos del tercero
    ''' </summary>
    ''' <param name="filter"></param>
    ''' <remarks></remarks>
    Private Sub GetAdvancePayment(ParamArray filter() As Object)
        Using model As New MBusqueda
            AdvancePaymentXPO = model.ConsultarEntidades(eDataSource.ListAdvancePaymentsCashReceipt, filter)
        End Using
    End Sub

    ''' <summary>
    ''' metodo para agregar un concepto
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddCashReceiptDetail()
        If ValidateControls() = False Then
            Exit Sub
        End If
        If INDLcgRepaymentAdvances.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If listCashReceiptAdvancePayment Is Nothing OrElse listCashReceiptAdvancePayment.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NotAddedAdvances", MODULE_NAME)
                Exit Sub
            End If
        End If
        If INDLcgAccountPayable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If listCashReceiptDetailAccountPayable Is Nothing OrElse listCashReceiptDetailAccountPayable.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se agregaron cuentas por pagar para reintegrar"
                Exit Sub
            End If
        End If
        If INDLcgRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            Dim errorsRetention As New StringBuilder
            If INDTxtBaseValue.EditValue = 0 Then
                errorsRetention.AppendLine("El valor base debe ser mayor a 0")
            End If
            If INDTxtBillingValue.EditValue = 0 Then
                errorsRetention.AppendLine("El valor facturado debe ser mayor a 0")
            End If
            If errorsRetention.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errorsRetention.ToString()
                Exit Sub
            End If
        End If
        If INDLcgPortfolioAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If admission Is Nothing AndAlso cashReceiptConcept.MakeAssociateIncome Then
                Mensaje(EeventViewerImages.Advertencia) = "El concepto requiere vincular un ingreso obligatoriamente"
                Exit Sub
            End If
        End If

        If editModeForm = False Then
            If listConcept IsNot Nothing Then
                Dim concept As CashReceiptDetails

                If CByte(cashReceiptConcept.Affectation) <> 1 Then
                    Select Case cashReceiptConcept.Affectation
                        Case 2 'Cancelacion / Abonos Facturas CxC
                            'busco que no exista un concepto para reintgro de anticipos
                            concept = listConcept.Find(Function(x) x.CashReceiptConceptAffectation = 3)
                            If concept IsNot Nothing Then
                                Mensaje(EeventViewerImages.Advertencia) = "Ya se agrego un concepto con comportamiento reintegro de anticipos a proveedores y no se puede agregar uno para cancelación / abonos facturas CxC"
                                Exit Sub
                            End If
                            'busco que no exista un concepto para reintgro de anticipos
                            concept = listConcept.Find(Function(x) x.CashReceiptConceptAffectation = 4)
                            If concept IsNot Nothing Then
                                Mensaje(EeventViewerImages.Advertencia) = "Ya se agrego un concepto con comportamiento reintegro de cuentas por pagar y no se puede agregar uno para cancelación / abonos facturas CxC"
                                Exit Sub
                            End If
                            concept = listConcept.Find(Function(x) x.CashReceiptConceptAffectation = 2 And x.IdMainAccount = cashReceiptConcept.IdMainAccount)
                            If concept IsNot Nothing Then
                                Mensaje(EeventViewerImages.Advertencia) = "Ya se agrego un concepto con comportamiento cancelación / abonos facturas CxC con la misma cuenta contable"
                                Exit Sub
                            End If
                        Case 3 'Reintegro de Anticipos a Proveedores
                            'busco que no exista un concepto para cancelación
                            concept = listConcept.Find(Function(x) x.CashReceiptConceptAffectation = 2)
                            If concept IsNot Nothing Then
                                Mensaje(EeventViewerImages.Advertencia) = "Ya se agrego un concepto con comportamiento cancelación / abonos facturas CxC y no se puede agregar uno para reintegro de anticipos a proveedores"
                                Exit Sub
                            End If
                            'busco que no exista un concepto para cancelación
                            concept = listConcept.Find(Function(x) x.CashReceiptConceptAffectation = 4)
                            If concept IsNot Nothing Then
                                Mensaje(EeventViewerImages.Advertencia) = "Ya se agrego un concepto con comportamiento reintegro de cuentas por pagar y no se puede agregar uno para reintegro de anticipos a proveedores"
                                Exit Sub
                            End If
                            concept = listConcept.Find(Function(x) x.CashReceiptConceptAffectation = 3 And x.IdMainAccount = cashReceiptConcept.IdMainAccount)
                            If concept IsNot Nothing Then
                                Mensaje(EeventViewerImages.Advertencia) = "Ya se agrego un concepto con comportamiento reintegro de anticipos a proveedores con la misma cuenta contable"
                                Exit Sub
                            End If
                        Case 4 'reintegro de cuentas por pagar
                            'busco que no exista un concepto para cancelación
                            concept = listConcept.Find(Function(x) x.CashReceiptConceptAffectation = 2)
                            If concept IsNot Nothing Then
                                Mensaje(EeventViewerImages.Advertencia) = "Ya se agrego un concepto con comportamiento cancelación / abonos facturas CxC y no se puede agregar uno para reintegro de cuentas por pagar"
                                Exit Sub
                            End If
                            'busco que no exista un concepto para reintgro de anticipos
                            concept = listConcept.Find(Function(x) x.CashReceiptConceptAffectation = 3)
                            If concept IsNot Nothing Then
                                Mensaje(EeventViewerImages.Advertencia) = "Ya se agrego un concepto con comportamiento reintegro de anticipos a proveedores y no se puede agregar uno para reintegro de cuentas por pagar"
                                Exit Sub
                            End If
                            concept = listConcept.Find(Function(x) x.CashReceiptConceptAffectation = 4 And x.IdMainAccount = cashReceiptConcept.IdMainAccount)
                            If concept IsNot Nothing Then
                                Mensaje(EeventViewerImages.Advertencia) = "Ya se agrego un concepto con comportamiento reintegro de cuentas por pagar con la misma cuenta contable"
                                Exit Sub
                            End If
                    End Select
                End If
            End If
        End If
        If cashReceiptsDetails Is Nothing Then
            cashReceiptsDetails = New CashReceiptDetails
        End If
        cashReceiptsDetails.IdThirdParty = INDSleThirdParty.EditValue

        If INDLciThirdPartyBeneficiary.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            cashReceiptsDetails.ThirdPartyBeneficiaryId = ThirdPartyBeneficiaryId
        End If

        cashReceiptsDetails.CodeNameThirdParty = INDSleThirdParty.Text
        cashReceiptsDetails.CodeNameMainAccount = INDSleAccount.Text
        If cashReceiptConcept IsNot Nothing Then
            cashReceiptsDetails.IdMainAccount = cashReceiptConcept.IdMainAccount
            cashReceiptsDetails.IdCashReceiptConcept = cashReceiptConcept.Id
            cashReceiptsDetails.CashReceiptConceptAffectation = CByte(cashReceiptConcept.Affectation)
        End If
        cashReceiptsDetails.IdCostCenter = INDSleCostCenter.EditValue
        cashReceiptsDetails.CodeNameCostCenter = INDSleCostCenter.Text
        cashReceiptsDetails.CodeNameCashReceiptConcept = INDSleCashReceiptConcept.Text

        If INDLcgRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            cashReceiptsDetails.Nature = INDGleNatureRetention.EditValue
            cashReceiptsDetails.Value = CDec(INDTxtRetentionValue.EditValue)
            cashReceiptsDetails.IdRetentionConcept = INDSleRetentionConcept.EditValue
            cashReceiptsDetails.CodeNameRetentionConcept = INDSleRetentionConcept.Text
            cashReceiptsDetails.PercentageRetention = CDec(INDSePercentage.EditValue)
            cashReceiptsDetails.BaseValue = CDec(INDTxtBaseValue.EditValue)
            cashReceiptsDetails.BillingValue = CDec(INDTxtBillingValue.EditValue)
        Else
            If INDLcgBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never And INDLcgRepaymentAdvances.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                cashReceiptsDetails.Value = CDec(INDTxtValue.EditValue)
            End If
            cashReceiptsDetails.Nature = INDGleNatureCashReceiptConcept.EditValue
        End If

        cashReceiptsDetails.CurrencyId = Me.CurrencyDetailId
        cashReceiptsDetails.CurrencyAbbreviation = INDSleCurrency.Text
        If Me.CurrencyDetailId <> cashReceiptsCurrencyId AndAlso INDLciValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            cashReceiptsDetails.TRM = INDTxtTRM.EditValue
            cashReceiptsDetails.ValueInCurrencyHeader = INDTxtValueTRM.EditValue
        ElseIf Me.CurrencyDetailId <> cashReceiptsCurrencyId AndAlso INDLciTRM1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            cashReceiptsDetails.TRM = INDTxtTRM.EditValue
            cashReceiptsDetails.ValueInCurrencyHeader = INDTxtValueTRM.EditValue
        Else
            cashReceiptsDetails.TRM = 1
            If INDLcgRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                cashReceiptsDetails.ValueInCurrencyHeader = CDec(INDTxtRetentionValue.EditValue)
            Else
                cashReceiptsDetails.ValueInCurrencyHeader = INDTxtValue.EditValue
            End If
        End If

        If INDLcgBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            Dim value As Decimal = 0
            If Me.PortfolioAdvancedValue <> 0 Then
                If listAccountReceivable IsNot Nothing Then
                    value = listAccountReceivable.Sum(Function(x) x.PaymentValue)
                End If
                value += Me.PortfolioAdvancedValue
                CreatePortfolioAdvance()
            Else
                If listAccountReceivable IsNot Nothing Then
                    value = listAccountReceivable.Sum(Function(x) x.PaymentValue)
                End If
            End If
            If value = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AdvanceOrPayBills", MODULE_NAME)
                cashReceiptsDetails = Nothing
                Exit Sub
            Else
                If Me.CurrencyDetailId <> cashReceiptsCurrencyId AndAlso INDLciValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                    cashReceiptsDetails.TRM = INDTxtTRM.EditValue
                    cashReceiptsDetails.Value = INDTxtValueTRM1.EditValue
                    cashReceiptsDetails.ValueInCurrencyHeader = Math.Round((value / INDTxtTRM1.EditValue), Me._decimals)
                Else
                    cashReceiptsDetails.Value = value
                    cashReceiptsDetails.TRM = 1
                    cashReceiptsDetails.ValueInCurrencyHeader = value
                End If
            End If
            If listAccountReceivable IsNot Nothing AndAlso listAccountReceivable.Count > 0 Then
                'valido que no existan facturas ya guardadas con saldo 0 o menor al v
                Dim listBalance = listAccountReceivable.FindAll(Function(x) x.Balance = 0 Or x.PaymentValue > x.Balance)
                If listBalance.Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Las facturas " & String.Join(",", (From e In listBalance Select e.InvoiceNumber).ToList()) & " tienen saldo en 0 o saldo menor al valor a pagar"
                    cashReceiptsDetails = Nothing
                    Exit Sub
                End If

                For Each item In listAccountReceivable
                    Dim cashReceiptAccountReceivable As CashReceiptAccountReceivable = New CashReceiptAccountReceivable
                    cashReceiptAccountReceivable.AccountReceivableId = item.Id
                    cashReceiptAccountReceivable.Value = item.PaymentValue
                    cashReceiptAccountReceivable.InvoiceNumber = item.InvoiceNumber
                    'en caso de que la moneda sea diferente de la moneda de la caja se guarda el valor convertido
                    cashReceiptAccountReceivable.ValueInCurrencyHeader = GetValueInCurrencyHeader(INDTxtTRM1.EditValue, item.PaymentValue)
                    Dim bill = cashReceiptsDetails.CashReceiptAccountReceivable.Where(Function(x) x.AccountReceivableId = item.Id).FirstOrDefault()
                    If bill IsNot Nothing Then
                        If bill.Id = 0 Then
                            cashReceiptsDetails.CashReceiptAccountReceivable.Remove(bill)
                            cashReceiptsDetails.CashReceiptAccountReceivable.Add(cashReceiptAccountReceivable)
                        Else
                            bill.AccountReceivableId = cashReceiptAccountReceivable.AccountReceivableId
                            bill.Value = cashReceiptAccountReceivable.Value
                        End If
                    Else
                        cashReceiptsDetails.CashReceiptAccountReceivable.Add(cashReceiptAccountReceivable)
                    End If
                Next
            End If
            cashReceiptsDetails.Detail = "Recibo de Caja Nº {code}, Concepto " & If(INDSleCashReceiptConcept.Text Is String.Empty, INDSleCashReceiptConcept.Properties.NullText, INDSleCashReceiptConcept.Text) & ", CxC " & String.Join("-", (From i In cashReceiptsDetails.CashReceiptAccountReceivable Select i.InvoiceNumber).ToList())
            If listDeleteShares IsNot Nothing AndAlso listDeleteShares.Count > 0 Then
                For Each item In listDeleteShares
                    item.MarkAsDeleted()
                Next
            End If
        End If
        'reintegros
        If INDLcgRepaymentAdvances.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            cashReceiptsDetails.Value = listCashReceiptAdvancePayment.Sum(Function(x) x.PaymentValue)
            cashReceiptsDetails.ValueInCurrencyHeader = GetValueInCurrencyHeader(INDTxtTRM1.EditValue, cashReceiptsDetails.Value) ' cashReceiptsDetails.Value
            For Each item In listCashReceiptAdvancePayment
                Dim advance = cashReceiptsDetails.CashReceiptAdvancePayment.Where(Function(x) x.AdvancePaymentId = item.AdvancePaymentId).FirstOrDefault()
                If advance IsNot Nothing Then
                    If advance.Id > 0 Then
                        cashReceiptsDetails.CashReceiptAdvancePayment.Remove(advance)
                        cashReceiptsDetails.CashReceiptAdvancePayment.Add(item)
                    Else
                        advance.AdvancePaymentId = item.AdvancePaymentId
                        advance.CashReceiptDetailId = item.CashReceiptDetailId
                        advance.PaymentValue = item.PaymentValue
                        advance.AdvancePaymentCode = item.Code
                        advance.ValueInCurrencyHeader = GetValueInCurrencyHeader(INDTxtTRM1.EditValue, item.PaymentValue)
                    End If
                Else
                    cashReceiptsDetails.CashReceiptAdvancePayment.Add(item)
                End If
            Next
            cashReceiptsDetails.Detail = "Recibo de Caja Nº {code}, Concepto " & If(INDSleCashReceiptConcept.Text Is String.Empty, INDSleCashReceiptConcept.Properties.NullText, INDSleCashReceiptConcept.Text) & ", Reintegro de Anticipos " & String.Join("-", (From i In cashReceiptsDetails.CashReceiptAdvancePayment Select i.AdvancePaymentCode).ToList())
            If listCashReceiptAdvancePaymentDelete IsNot Nothing AndAlso listCashReceiptAdvancePaymentDelete.Count > 0 Then
                For Each item In listCashReceiptAdvancePaymentDelete
                    item.MarkAsDeleted()
                Next
            End If
        End If

        If INDLcgAccountPayable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            cashReceiptsDetails.Value = listCashReceiptDetailAccountPayable.Sum(Function(x) x.RefundValue)
            For Each item In listCashReceiptDetailAccountPayable
                Dim accountPayable = cashReceiptsDetails.CashReceiptDetailAccountPayable.Where(Function(x) x.AccountPayableId = item.AccountPayableId).FirstOrDefault()
                If accountPayable IsNot Nothing Then
                    If accountPayable.Id > 0 Then
                        cashReceiptsDetails.CashReceiptDetailAccountPayable.Remove(accountPayable)
                        cashReceiptsDetails.CashReceiptDetailAccountPayable.Add(item)
                    Else
                        accountPayable.AccountPayableId = item.AccountPayableId
                        accountPayable.CashReceiptDetailId = item.CashReceiptDetailId
                        accountPayable.RefundValue = item.RefundValue
                        accountPayable.BillNumber = item.BillNumber
                    End If
                Else
                    cashReceiptsDetails.CashReceiptDetailAccountPayable.Add(item)
                End If
            Next
            cashReceiptsDetails.Detail = "Recibo de Caja Nº {code}, Concepto " & If(INDSleCashReceiptConcept.Text Is String.Empty, INDSleCashReceiptConcept.Properties.NullText, INDSleCashReceiptConcept.Text) & ", Reintegro de Cuentas Por Pagar " & String.Join("-", (From i In cashReceiptsDetails.CashReceiptDetailAccountPayable Select i.BillNumber).ToList())
            If listCashReceiptDetailAccountPayableDelete IsNot Nothing AndAlso listCashReceiptDetailAccountPayableDelete.Count > 0 Then
                For Each item In listCashReceiptDetailAccountPayableDelete
                    item.MarkAsDeleted()
                Next
            End If
        End If
        If CashFlowConceptId > 0 Then
            cashReceiptsDetails.IdCashFlowConcept = CashFlowConceptId
            cashReceiptsDetails.CodeNameCashFlowConcept = CashFlowConceptCodeName
        Else
            cashReceiptsDetails.IdCashFlowConcept = Nothing
            cashReceiptsDetails.CodeNameCashFlowConcept = String.Empty
        End If

        Dim args As AddCashReceiptConceptEventArgs = New AddCashReceiptConceptEventArgs
        args.CashReceiptDetails = cashReceiptsDetails
        args.PortfolioAdvance = portfolioAdvance
        args.ListAccountReceivable = listAccountReceivable
        args.ListAdvancePayment = listCashReceiptAdvancePayment
        args.ListCashReceiptDetailAccountPayable = listCashReceiptDetailAccountPayable
        listConcept.Add(cashReceiptsDetails)
        CleanControls()
        RaiseEvent AddCashReceiptConcept(Nothing, args)
        INDSleCashReceiptConcept.Focus()
    End Sub

    Private Sub CreatePortfolioAdvance()
        If portfolioAdvance Is Nothing Then
            portfolioAdvance = New PortfolioAdvance
        End If
        portfolioAdvance.ThirdPartyId = INDSleThirdParty.EditValue
        portfolioAdvance.MainAccountId = cashReceiptConcept.IdMainAccount
        portfolioAdvance.CostCenterId = INDSleCostCenter.EditValue
        portfolioAdvance.Value = Me.PortfolioAdvancedValue
        If admission IsNot Nothing Then
            portfolioAdvance.AdmissionNumber = admission.AdmissionCode.ToString().Trim()
        End If

        If INDLciThirdPartyBeneficiary.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            portfolioAdvance.ThirdPartyBeneficiaryId = ThirdPartyBeneficiaryId
        End If

        portfolioAdvance.Observations = INDMeDetailPorfolioAdvance.Text
        portfolioAdvance.DebitValue = 0
        portfolioAdvance.CreditValue = 0
        portfolioAdvance.Balance = Me.PortfolioAdvancedValue
        If portfolioAdvance.Id = 0 Then
            portfolioAdvance.CreationUser = indigo.UserIndigo
            portfolioAdvance.Status = 1
        Else
            portfolioAdvance.ModificationUser = indigo.UserIndigo
        End If
        portfolioAdvance.CurrencyId = Me.CurrencyDetailId
        If Me.CurrencyDetailId <> cashReceiptsCurrencyId Then
            portfolioAdvance.TRMValue = INDTxtTRM1.EditValue
            portfolioAdvance.ValueInCurrencyHeader = INDTxtValueTRM1.EditValue
        Else
            portfolioAdvance.TRMValue = 1
            portfolioAdvance.ValueInCurrencyHeader = Me.PortfolioAdvancedValue
        End If
        Me.EnableCurrencyDetail()
    End Sub

    ''' <summary>
    ''' devuelve el valor en la moneda de la cabecera, siempre y cuando esta sea diferente
    ''' </summary>
    ''' <returns></returns>
    Private Function GetValueInCurrencyHeader(tRMvalue As Decimal?, value As Decimal?) As Decimal
        If value Is Nothing Then
            Return 0
        End If
        If Me.CurrencyDetailId = Me.cashReceiptsCurrencyId OrElse tRMvalue Is Nothing Then
            Return value
        End If

        Return Math.Round((value.Value / tRMvalue.Value), Me._decimals, MidpointRounding.AwayFromZero)
    End Function

    ''' <summary>
    ''' metodo para saber si el formulario esta abierto
    ''' </summary>
    ''' <param name="_form"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckForm(_form As Form) As Boolean
        For Each f As Form In System.Windows.Forms.Application.OpenForms
            If f.Name = _form.Name Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' metodo para asignar valores a la entidad de las cuotas de las facturas con el objeto obtenido del combo de facturas
    ''' </summary>
    ''' <param name="_accountReceivable"></param>
    ''' <remarks></remarks>
    Private Sub AssingValuesaccountReceivable(_accountReceivable As PortfolioAccountReceivableXpo)
        accountReceivable = New AccountReceivable
        With accountReceivable
            .AccountReceivableDate = _accountReceivable.AccountReceivableDate
            .AccountReceivableType = _accountReceivable.AccountReceivableType
            .Balance = ConceptValue
            .Code = _accountReceivable.Code
            .ExpiredDate = _accountReceivable.ExpiredDate
            .Id = _accountReceivable.Id
            If _accountReceivable.InvoiceId IsNot Nothing Then
                .InvoiceId = _accountReceivable.InvoiceId.Id
            End If
            .InvoiceNumber = _accountReceivable.InvoiceNumber
            .NumberShares = _accountReceivable.NumberShares
            .Observations = _accountReceivable.Observations
            .OpeningBalance = _accountReceivable.OpeningBalance
            .PaymentAgreement = _accountReceivable.PaymentAgreement
            .ThirdPartyId = _accountReceivable.ThirdPartyId.Id
            .Value = _accountReceivable.Value
            Using model As New MCashReceipts(Me.Tag)
                INDGcAccountsDistribution.DataSource = model.ListAccountReceivableAccounting(_accountReceivable.Id)
            End Using

            'Dim SuperToolTipAccount As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
            'Dim toolTipItemTitle As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
            'toolTipItemTitle.Text = "Cuentas Contables" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
            'Dim total As Decimal = 0
            'SuperToolTipAccount.Items.Add(toolTipItemTitle)
            'Dim toolTipItemBody As DevExpress.Utils.ToolTipItem = Nothing
            'For Each item In _accountReceivable.PortfolioAccountReceivableAccounting.ToList()

            '    INDMeAccount.Text += item.MainAccountId.NumberName & " - " & Format(item.Balance, "c0") & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)


            '    'toolTipItemBody = New DevExpress.Utils.ToolTipItem()
            '    'toolTipItemBody.LeftIndent = 6
            '    'toolTipItemBody.Text = item.MainAccountId.NumberName & " - " & Format(item.Balance, "c0") & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
            '    'SuperToolTipAccount.Items.Add(toolTipItemBody)
            '    total += item.Balance
            'Next
            'INDLblTotalAccount.Text = "Total: " & Format(total, "c0")
            'SuperToolTipAccount.Items.Add(New DevExpress.Utils.ToolTipSeparatorItem())
            'Dim toolTipItemFooter As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
            'toolTipItemFooter.LeftIndent = 6
            'toolTipItemFooter.Text = "Total: " & Format(total, "c0")
            'SuperToolTipAccount.Items.Add(toolTipItemFooter)
            'SuperToolTipAccount.MaxWidth = 800
            'INDTxtConceptValue.SuperTip = SuperToolTipAccount
        End With
    End Sub

    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvBills, ListActions)
        IndigoGridView2.SetListAcction(INDGvRepaymentAdvances, ListActions)
        IndigoGridView3.SetListAcction(INDGvAccountPayable, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvBills.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvRepaymentAdvances.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvAccountPayable.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles del popup de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopupBills()
        INDSleBills.EditValue = Nothing
        INDSleBills.Properties.NullText = String.Empty
        INDSleBills.Properties.ReadOnly = False
        INDSleBills.Properties.Buttons(0).Enabled = True
        INDSleBills.Properties.ReadOnly = False
        INDSleBills.Properties.Buttons(0).Enabled = True
        ValueBills = 0
        BalanceBills = 0
        ConceptValue = 0
        BillValue = 0
        accountReceivable = Nothing
        editModePopups = False
        INDSleBills.Focus()
    End Sub

    ''' <summary>
    ''' metodo para asiganar valores a la entidad de anticipos
    ''' </summary>
    ''' <param name="_advancePaymentTmp"></param>
    ''' <remarks></remarks>
    Private Sub AssingValuesAdvancePayment(_advancePaymentTmp As AdvancePaymentsXpo)
        advancePayment = New AdvancePayments
        advancePayment.Id = _advancePaymentTmp.Id
        advancePayment.Code = _advancePaymentTmp.Code
        advancePayment.Value = _advancePaymentTmp.Value
        advancePayment.Balance = _advancePaymentTmp.Balance
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles del popup de anticipos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopupAdvancePayment()
        INDSleAdvancePayment.EditValue = Nothing
        INDSleAdvancePayment.Properties.ReadOnly = False
        INDSleAdvancePayment.Properties.Buttons(0).Enabled = True
        INDSleAdvancePayment.Properties.NullText = String.Empty
        INDTxtAdvancePaymentValue.EditValue = 0
        INDTxtAdvancePaymentBalance.EditValue = 0
        RepaymentValue = 0D
        cashReceiptAdvancePayment = Nothing
        editModePopups = False
        INDSleAdvancePayment.Focus()
    End Sub
#End Region

#Region "PUBLIC METHODS"
    ''' <summary>
    ''' metodo publico para limpiar los controles del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CleanControls()
        INDSleCashReceiptConcept.EditValue = Nothing
        INDSleCashReceiptConcept.Properties.NullText = Nothing
        INDSleCashReceiptConcept.Properties.ReadOnly = False
        INDSleCashReceiptConcept.Properties.Buttons(0).Enabled = True
        INDSleAccount.Properties.NullText = Nothing
        INDSleAccount.Properties.ReadOnly = True
        INDSleAccount.Properties.Buttons(0).Enabled = True
        INDSleThirdParty.EditValue = IdThirdPartyOriginalValue
        INDSleThirdParty.Properties.NullText = NameThirdPartyOriginalValue
        INDSleThirdParty.Properties.ReadOnly = False
        INDSleThirdParty.Properties.Buttons(0).Enabled = True
        INDSleCostCenter.EditValue = Nothing
        INDSleCostCenter.Properties.NullText = Nothing
        INDGleNatureCashReceiptConcept.EditValue = 2
        INDTxtValue.Text = 0
        INDSleRetentionConcept.EditValue = Nothing
        INDSleRetentionConcept.Properties.NullText = Nothing
        INDSleRetentionConcept.Properties.ReadOnly = False
        INDSleRetentionConcept.Properties.Buttons(0).Enabled = True
        INDLciPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDSePercentage.EditValue = 0
        INDGleNatureRetention.EditValue = 2
        INDTxtBaseValue.EditValue = 0
        INDTxtBaseValue.Enabled = False
        INDTxtBillingValue.EditValue = 0
        INDTxtRetentionValue.EditValue = 0
        INDGleNatureCashReceiptConcept.Enabled = True
        INDSleAccountPayable.EditValue = Nothing
        INDTxtAccountPayableBalance.EditValue = 0
        INDTxtMaximunRefundValue.EditValue = 0
        INDTxtRefundValue.EditValue = 0
        INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLcgRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLcgBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLcgRepaymentAdvances.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLcgPortfolioAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLcgAccountPayable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciNatureCashReceiptConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDLciValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.PortfolioAdvancedValue = 0F
        Me.INDSleAdmissionNumber.SetNullText(String.Empty)
        INDSleThirdPartyBeneficiary.EditValue = Nothing
        INDMeDetailPorfolioAdvance.Text = String.Empty
        cashReceiptsDetails = Nothing
        cashReceiptConcept = Nothing
        accountReceivableXPO = Nothing
        INDBtnOk.Text = ResourceManager.GetString("Add")
        INDGcBills.DataSource = Nothing
        listAccountReceivable = Nothing
        AccountPayable = Nothing
        listCashReceiptDetailAccountPayable = Nothing
        CashReceiptDetailAccountPayable = Nothing
        accountReceivable = Nothing
        listDeleteShares = Nothing
        CashReceiptDetailAccountPayable = Nothing
        listCashReceiptDetailAccountPayable = Nothing
        listCashReceiptDetailAccountPayableDelete = Nothing
        INDGcAccountPayable.DataSource = Nothing
        cashReceiptAdvancePayment = Nothing
        listCashReceiptAdvancePayment = Nothing
        listCashReceiptAdvancePaymentDelete = Nothing
        BillValue = 0
        RepaymentValue = 0D
        editModeForm = False
        portfolioAdvance = Nothing
        CashFlowConceptCodeName = Nothing
        CashFlowConceptId = 0
        Me.CurrencyDetailId = cashReceiptsCurrencyId
        INDSleCurrency.Properties.NullText = cashReceiptsCurrencyAbrreviation
        Me.EnableCurrencyDetail()
        HideAllLabels()
    End Sub

    ''' <summary>
    ''' metodo para cargar los controles y editar un registro
    ''' </summary>
    ''' <param name="_cashReceiptDetail"></param>
    ''' <remarks></remarks>
    Public Async Function LoadControlsForEdit(_cashReceiptDetail As CashReceiptDetails) As Task
        cashReceiptsDetails = _cashReceiptDetail
        INDSleCashReceiptConcept.EditValue = cashReceiptsDetails.IdCashReceiptConcept
        INDSleCashReceiptConcept.Properties.NullText = cashReceiptsDetails.CodeNameCashReceiptConcept
        INDSleCashReceiptConcept.Properties.ReadOnly = True
        INDSleCashReceiptConcept.Properties.Buttons(0).Enabled = False
        INDSleThirdParty.EditValue = cashReceiptsDetails.IdThirdParty
        INDSleThirdParty.Properties.NullText = cashReceiptsDetails.CodeNameThirdParty
        INDSleAccount.Properties.ReadOnly = True
        INDSleAccount.Properties.Buttons(0).Enabled = False
        INDSleAccount.Properties.NullText = cashReceiptsDetails.CodeNameMainAccount
        INDSleCurrency.Properties.NullText = cashReceiptsDetails.CurrencyAbbreviation
        Me.CurrencyDetailId = cashReceiptsDetails.CurrencyId
        If cashReceiptsDetails.IdCostCenter IsNot Nothing Then
            INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
        INDSleCostCenter.EditValue = cashReceiptsDetails.IdCostCenter
        INDSleCostCenter.Properties.NullText = cashReceiptsDetails.CodeNameCostCenter
        CashFlowConceptCodeName = cashReceiptsDetails.CodeNameCashFlowConcept
        CashFlowConceptId = cashReceiptsDetails.IdCashFlowConcept.GetValueOrDefault
        If cashReceiptsDetails.IdRetentionConcept IsNot Nothing Then
            INDGleNatureRetention.EditValue = cashReceiptsDetails.Nature
            INDTxtRetentionValue.EditValue = cashReceiptsDetails.Value
            INDSleRetentionConcept.EditValue = cashReceiptsDetails.IdRetentionConcept
            INDSleRetentionConcept.Properties.NullText = cashReceiptsDetails.CodeNameRetentionConcept
            INDSleRetentionConcept.Properties.ReadOnly = True
            INDSleRetentionConcept.Properties.Buttons(0).Enabled = False
            INDSePercentage.EditValue = cashReceiptsDetails.PercentageRetention
            INDTxtBaseValue.EditValue = cashReceiptsDetails.BaseValue
            INDTxtBillingValue.EditValue = cashReceiptsDetails.BillingValue
            INDLcgRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciNatureCashReceiptConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            INDGleNatureCashReceiptConcept.EditValue = cashReceiptsDetails.Nature
            INDTxtValue.EditValue = cashReceiptsDetails.Value
            INDLcgRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciNatureCashReceiptConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Using model As New MCashReceiptsConcepts(TagForm)
                Dim auxCashReceiptConcept = model.GetCashReceiptConceptById(INDSleCashReceiptConcept.EditValue)
                Select Case auxCashReceiptConcept.Affectation
                    Case 1
                        INDLcgBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLcgRepaymentAdvances.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLcgPortfolioAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLcgAccountPayable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDSleThirdParty.Properties.ReadOnly = False
                        INDSleThirdParty.Properties.Buttons(0).Enabled = True
                        INDGleNatureCashReceiptConcept.Enabled = True
                        INDLciValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Case 2
                        INDLcgRepaymentAdvances.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLcgBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDLcgPortfolioAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDLcgAccountPayable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDSleThirdParty.Properties.ReadOnly = True
                        INDSleThirdParty.Properties.Buttons(0).Enabled = False
                        INDGleNatureCashReceiptConcept.Enabled = False
                        If portfolioAdvance IsNot Nothing Then
                            Me.PortfolioAdvancedValue = portfolioAdvance.Value
                            If portfolioAdvance.AdmissionNumber IsNot Nothing Then
                                Using modelService As New MServiceOrder(Me.Tag)
                                    Dim admissionTmp = modelService.GetAdmissionByServiceOrder(portfolioAdvance.AdmissionNumber.Trim())
                                    SetAdmission(admissionTmp)
                                End Using
                            End If

                            If portfolioAdvance.ThirdPartyBeneficiaryId IsNot Nothing Then
                                ThirdPartyBeneficiaryId = portfolioAdvance.ThirdPartyBeneficiaryId
                            End If

                            INDMeDetailPorfolioAdvance.Text = portfolioAdvance.Observations
                        End If
                        INDLciValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                        'Recorro los items para agregarlos a la regilla
                        Await SetBills()
                        INDGcBills.DataSource = Nothing
                        INDGcBills.DataSource = listAccountReceivable
                    Case 3
                        INDLcgBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLcgRepaymentAdvances.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDLcgPortfolioAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLcgAccountPayable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDSleThirdParty.Properties.ReadOnly = True
                        INDSleThirdParty.Properties.Buttons(0).Enabled = False
                        INDGleNatureCashReceiptConcept.Enabled = False
                        INDLciValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        For Each item In cashReceiptsDetails.CashReceiptAdvancePayment
                            Using modelAdvance As New MAdvancePayments(TagForm)
                                Dim advance = modelAdvance.GetAdvancePaymentsById(item.AdvancePaymentId)
                                item.Code = advance.Code
                                item.Value = advance.Value
                                item.Balance = advance.Balance
                            End Using
                            If listCashReceiptAdvancePayment Is Nothing Then
                                listCashReceiptAdvancePayment = New List(Of CashReceiptAdvancePayment)
                            End If
                            listCashReceiptAdvancePayment.Add(item)
                        Next
                        INDGcRepaymentAdvances.DataSource = Nothing
                        INDGcRepaymentAdvances.DataSource = listCashReceiptAdvancePayment
                    Case 4
                        INDLcgBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLcgRepaymentAdvances.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLcgPortfolioAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLcgAccountPayable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDSleThirdParty.Properties.ReadOnly = True
                        INDSleThirdParty.Properties.Buttons(0).Enabled = False
                        INDGleNatureCashReceiptConcept.Enabled = False
                        INDLciValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        For Each item In cashReceiptsDetails.CashReceiptDetailAccountPayable
                            Using modelAccountPayable As New MAccountPayable(TagForm)
                                Dim accountPayable = modelAccountPayable.GetAccountPayableById(item.AccountPayableId)
                                item.BillNumber = accountPayable.BillNumber
                                item.MaximunRefundValue = accountPayable.Value - accountPayable.Balance
                                item.Balance = accountPayable.Balance
                            End Using
                            If listCashReceiptDetailAccountPayable Is Nothing Then
                                listCashReceiptDetailAccountPayable = New List(Of CashReceiptDetailAccountPayable)
                            End If
                            listCashReceiptDetailAccountPayable.Add(item)
                        Next
                        INDGcAccountPayable.DataSource = listCashReceiptDetailAccountPayable
                        INDGcAccountPayable.RefreshDataSource()
                End Select
                Me.EnableCurrencyDetail()
            End Using
        End If
    End Function

    ''' <summary>
    ''' habilita el control de moneda en base si existen detalles en la rejilla de facturas o anticipos
    ''' </summary>
    Private Sub EnableCurrencyDetail()
        Me.INDSleCurrency.ReadOnly = ((listAccountReceivable IsNot Nothing AndAlso listAccountReceivable?.Any()) OrElse (listCashReceiptAdvancePayment IsNot Nothing AndAlso listCashReceiptAdvancePayment?.Any()))
    End Sub
#End Region

#Region "HANDLERS"

#Region "Load"
    ''' <summary>
    ''' evento load del control de usuario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub PopupCashReceiptConcept_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If indigo.IndigoCompanyType = 3 Then
            INDLciAdmission.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If

        INDBtnExportBillsStructure.AddRangeColumns("Factura", "Valor")
        IndigoGridControl1.SetControlNextFocus(INDGcBills, INDTxtPortfolioAdvance)
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.Minimizar(True)
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True

        INDSleAdmissionNumber.PopupContainerControl = INDPccMoreInfoAdmission
        AddActionsColumns()
        IndigoGridControl1.RefreshGrid(INDGcRepaymentAdvances)
        IndigoGridControl1.RefreshGrid(INDGcBills)
        INDGvRepaymentAdvances.OptionsFind.AlwaysVisible = True
        INDGvBills.OptionsFind.AlwaysVisible = True
        AddHandler Me.INDSleAdmissionNumber.Search.KeyDown, AddressOf INDSleAdmissionNumber_KeyDown
        INDBtnOk.Enabled = False
        INDSleCashReceiptConcept.Properties.PopupFormSize = New Size(700, 400)

        ''Moneda
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = cashReceiptsCurrencyAbrreviation.GetNumberFormat()
        Me.CurrencyDetailId = cashReceiptsCurrencyId
        INDSleCurrency.Properties.NullText = cashReceiptsCurrencyAbrreviation
        INDTxtPortfolioAdvance.Properties.Mask.Culture = _culture
        INDLciValueTRM.Text = "Valor (" + cashReceiptsCurrencyAbrreviation + ")"
        INDTxtTRM.Properties.Mask.Culture = _culture
        INDTxtValueTRM.Properties.Mask.Culture = _culture
        INDLciValueTRM1.Text = "Valor (" + cashReceiptsCurrencyAbrreviation + ")"
        INDTxtTRM1.Properties.Mask.Culture = _culture
        INDTxtValueTRM1.Properties.Mask.Culture = _culture

    End Sub
#End Region

#Region "Shown"

    Private Async Sub PopupCashReceiptConcept_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        AsyncLoader(True)
        If cashReceiptsDetails Is Nothing Then
            CleanControls()
        Else
            Await LoadControlsForEdit(cashReceiptsDetails)
        End If
        AsyncLoader(False)
        INDBtnOk.Enabled = True

        ' Si está en modo visualización (confirmado/anulado), deshabilitar los botones de agregar
        If ConfirmStatus Then
            INDBtnOk.Enabled = False
            INDBtnAddBill.Enabled = False
            INDBtnAddAdvancePayment.Enabled = False
            INDBtnAddAccountPayable.Enabled = False
        End If

        INDSleCashReceiptConcept.Focus()
    End Sub
#End Region

#Region "Activated"
    Private Sub PopupCashReceiptConcept_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDSleCashReceiptConcept.EditValue Is Nothing Then
            INDSleCashReceiptConcept.Focus()
        End If
    End Sub
#End Region

#Region "ButtonClick"
    Private Sub INDSleThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleThirdParty.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmThirdParty
                formulario.ViewModeEditHold = True
                formulario.Size = New Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent = New FrmTransparent(formulario, False)
                transparent.ShowDialog()
                InitializeThirdPartyXPO()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al presionar el boton mas del control y abre el funcional solicitado
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleCashReceiptConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCashReceiptConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmCashReceiptsConcepts
                formulario.Size = New Size(800, 700)
                formulario.ViewModeEditHold = True
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog()
                InitializeCashReceiptConceptXPO()
                Dim isReadOnly As Boolean = INDSleCashReceiptConcept.Properties.ReadOnly
                Dim value = INDSleCashReceiptConcept.EditValue
                INDSleCashReceiptConcept.EditValue = Nothing
                INDSleCashReceiptConcept.EditValue = value
                INDSleCashReceiptConcept.Focus()
                INDSleCashReceiptConcept.Properties.ReadOnly = isReadOnly
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al presionar el boton mas del control y abre el funcional solicitado
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleCostCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmCostCenter
                formulario.ViewModeEditHold = True
                formulario.Size = New Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog()
                InitializeCostCenterXPO()
                Dim value = INDSleCostCenter.EditValue
                INDSleCostCenter.EditValue = Nothing
                INDSleCostCenter.EditValue = value
                INDSleCostCenter.Focus()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al presionar el boton mas del control y abre el funcional solicitado
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleRetentionConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleRetentionConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmRetentionConcept
                formulario.ViewModeEditHold = True
                formulario.Size = New Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog()
                InitializeRetentionConceptXPO()
                Dim value = INDSleRetentionConcept.EditValue
                INDSleRetentionConcept.EditValue = Nothing
                INDSleRetentionConcept.EditValue = value
                INDSleRetentionConcept.Focus()
            End Using
        End If
    End Sub

    Private Sub INDSleAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmPopupPUC
                formulario.ViewModeEditHold = True
                formulario.Size = New Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog()
                INDSleAccount.Focus()
            End Using
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' evento que se dispara cuando cambia el valor del control
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDSleCashReceiptConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCashReceiptConcept.EditValueChanged
        If INDSleCashReceiptConcept.EditValue IsNot Nothing Then
            Using model As New MCashReceiptsConcepts(TagForm)
                cashReceiptConcept = model.GetCashReceiptConceptById(INDSleCashReceiptConcept.EditValue)
            End Using

            If cashReceiptConcept.LinkThirdPartyBeneficiary Then
                INDLciThirdPartyBeneficiary.ShowLayout()
                INDLciAdmission.HideLayout()
                InitializeThirdPartyBeneficiaryXPO()
            Else
                INDLciThirdPartyBeneficiary.HideLayout()
                INDLciAdmission.ShowLayout()
            End If

            accountReceivableXPO = Nothing
            If cashReceiptsDetails Is Nothing Then
                INDSleAccount.Properties.NullText = cashReceiptConcept.MainAccounts.Number + " - " + cashReceiptConcept.MainAccounts.Name
                If cashReceiptConcept.MainAccounts.HandlesCostCenter = True Then
                    INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciCostCenter.AllowHide = False
                Else
                    INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciCostCenter.AllowHide = True
                End If
                If cashReceiptConcept.MainAccounts.RetencionType <> 0 Then
                    Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgRetention, False)
                    INDLcgRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciNatureCashReceiptConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciNatureCashReceiptConcept.AllowHide = True
                    INDLciValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciValue.AllowHide = True
                Else
                    Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgRetention, True)
                    INDLcgRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciNatureCashReceiptConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciNatureCashReceiptConcept.AllowHide = False
                    INDLciValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciValue.AllowHide = False
                End If
                If cashReceiptConcept.CashFlowConcept IsNot Nothing Then
                    CashFlowConceptCodeName = String.Format("{0} - {1}", cashReceiptConcept.CashFlowConcept.Code, cashReceiptConcept.CashFlowConcept.NameConcept)
                    CashFlowConceptId = cashReceiptConcept.CashFlowConcept.Id
                Else
                    CashFlowConceptCodeName = String.Empty
                    CashFlowConceptId = 0
                End If

                Select Case cashReceiptConcept.Affectation
                    Case 1 'ninguno
                        Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgPortfolioAdvance, True)
                        INDLcgPortfolioAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgBills, True)
                        INDLcgBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgRepaymentAdvances, True)
                        INDLcgRepaymentAdvances.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDSleThirdParty.Properties.ReadOnly = False
                        INDSleThirdParty.Properties.Buttons(0).Enabled = True
                        If INDLcgRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                            INDGleNatureRetention.Enabled = True
                            INDLciNatureCashReceiptConcept.AllowHide = False
                            INDLciNatureRetention.AllowHide = True
                        Else
                            INDGleNatureCashReceiptConcept.Enabled = True
                            INDLciNatureCashReceiptConcept.AllowHide = True
                            INDLciNatureRetention.AllowHide = False
                        End If
                        Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgAccountPayable, True)
                        INDLcgAccountPayable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDGcAccountPayable.DataSource = Nothing
                        listCashReceiptDetailAccountPayable = Nothing
                    Case 2 'afecta cartera
                        listAccountReceivable = Nothing
                        INDGcBills.DataSource = Nothing
                        INDGcAccountPayable.DataSource = Nothing
                        listCashReceiptDetailAccountPayable = Nothing
                        Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgPortfolioAdvance, False)
                        INDLcgPortfolioAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgBills, False)
                        INDLcgBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgRepaymentAdvances, True)
                        INDLciAdvanceDetail.AllowHide = True
                        INDLcgRepaymentAdvances.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDSleThirdParty.Properties.ReadOnly = True
                        INDSleThirdParty.Properties.Buttons(0).Enabled = False
                        INDSleThirdParty.EditValue = IdThirdPartyOriginalValue
                        Using modelThird As New MThirdParty(Me.Tag)
                            Dim thirdTmp = modelThird.GetThirdPartyByIdSimple(INDSleThirdParty.EditValue)
                            Using model As New MServiceOrder(Me.Tag)
                                INDSleAdmissionNumber.Datasource = model.GetListAdmissionsOpenAndPartialPatientCode(thirdTmp.Nit)
                            End Using
                        End Using
                        INDSleThirdParty.Properties.NullText = NameThirdPartyOriginalValue
                        INDGleNatureCashReceiptConcept.EditValue = cashReceiptConcept.Nature
                        INDGleNatureCashReceiptConcept.Enabled = False
                        INDLciNatureCashReceiptConcept.AllowHide = False
                        INDLciNatureRetention.AllowHide = True
                        INDLciValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLciValue.AllowHide = True
                        Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgAccountPayable, True)
                        INDLcgAccountPayable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Case 3 'reintegro de anticipos
                        listCashReceiptAdvancePayment = Nothing
                        INDGcAccountPayable.DataSource = Nothing
                        listCashReceiptDetailAccountPayable = Nothing
                        INDGcRepaymentAdvances.DataSource = Nothing
                        Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgPortfolioAdvance, True)
                        INDLcgPortfolioAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgBills, True)
                        INDLcgBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgRepaymentAdvances, False)
                        INDLciAdvanceDetail.AllowHide = True
                        INDLcgRepaymentAdvances.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDSleThirdParty.Properties.ReadOnly = True
                        INDSleThirdParty.Properties.Buttons(0).Enabled = False
                        INDSleThirdParty.EditValue = IdThirdPartyOriginalValue
                        INDSleThirdParty.Properties.NullText = NameThirdPartyOriginalValue
                        INDGleNatureCashReceiptConcept.EditValue = cashReceiptConcept.Nature
                        INDGleNatureCashReceiptConcept.Enabled = False
                        INDLciNatureCashReceiptConcept.AllowHide = False
                        INDLciNatureRetention.AllowHide = True
                        INDLciValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLciValue.AllowHide = True
                        Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgAccountPayable, True)
                        INDLcgAccountPayable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Case 4 'Reintegro de cuentas por cobrar
                        listAccountReceivable = Nothing
                        INDGcBills.DataSource = Nothing
                        listCashReceiptAdvancePayment = Nothing
                        INDGcRepaymentAdvances.DataSource = Nothing
                        Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgPortfolioAdvance, True)
                        INDLcgPortfolioAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgBills, True)
                        INDLcgBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgRepaymentAdvances, True)
                        INDLciAdvanceDetail.AllowHide = True
                        INDLcgRepaymentAdvances.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDSleThirdParty.Properties.ReadOnly = True
                        INDSleThirdParty.Properties.Buttons(0).Enabled = False
                        INDSleThirdParty.EditValue = IdThirdPartyOriginalValue
                        INDSleThirdParty.Properties.NullText = NameThirdPartyOriginalValue
                        INDGleNatureCashReceiptConcept.EditValue = 2
                        INDGleNatureCashReceiptConcept.Enabled = False
                        INDLciNatureCashReceiptConcept.AllowHide = False
                        INDLciNatureRetention.AllowHide = True
                        INDLciValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLciValue.AllowHide = True
                        Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgAccountPayable, False)
                        INDLcgAccountPayable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                End Select
                INDLciPopupAdvance.AllowHide = True
                INDLciPopupBills.AllowHide = True
                INDLciPopupAcountPayable.AllowHide = True
            Else
                INDSleAccount.Text = String.Empty
                End If
            Else
            CleanControls()
        End If
    End Sub

    ''' <summary>
    ''' cuando cambie el concepto de retencion se acomoda el formulario segun los parametros que se necesiten
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleRetentionConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleRetentionConcept.EditValueChanged
        If INDSleRetentionConcept.EditValue IsNot Nothing Then
            Using model As New MRetentionConcept(TagForm)
                retentionConcept = model.GetRetentionByIdSimple(INDSleRetentionConcept.EditValue)
                Select Case retentionConcept.Retention
                    Case 1
                        INDLciPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDSePercentage.EditValue = retentionConcept.Rate
                        INDSePercentage.Enabled = False
                        INDTxtBaseValue.Enabled = True
                        INDTxtBaseValue.EditValue = retentionConcept.MinBase
                    Case 2
                        INDLciPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDSePercentage.Enabled = False
                        INDSePercentage.EditValue = 0
                        INDTxtBaseValue.Enabled = True
                    Case 3
                        INDLciPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        If cashReceiptsDetails Is Nothing Then
                            INDSePercentage.EditValue = 0
                        End If
                        INDSePercentage.Enabled = True
                        INDTxtBaseValue.Enabled = False
                        INDTxtBaseValue.EditValue = retentionConcept.MinBase
                End Select
                If cashReceiptsDetails Is Nothing Then
                    INDTxtRetentionValue.EditValue = 0
                    INDGleNatureRetention.EditValue = 2
                End If
            End Using
        Else
            INDTxtBaseValue.EditValue = 0
            INDTxtBaseValue.Enabled = False
            INDTxtRetentionValue.Text = 0
            INDGleNatureRetention.EditValue = 2
            INDLciPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSePercentage.EditValue = 0
        End If

    End Sub

    Private Sub INDSleBills_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleBills.EditValueChanged
        If INDSleBills.EditValue IsNot Nothing Then
            If editModePopups = False Then
                Dim filter As String = "Id = " & INDSleBills.EditValue
                Dim accountRecivableTmp = XpoServiceEx.Instance(indigo.TransactionalContainer).PortfolioService.GetCollection(Of PortfolioAccountReceivableXpo)(Nothing, filter).FirstOrDefault()
                If accountRecivableTmp.CurrencyId <> Me.CurrencyDetailId Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = "No puede seleccionar una factura con una moneda diferente a la del detalle de concepto "
                    INDSleBills.EditValue = Nothing
                    Exit Sub
                End If
                ValueBills = accountRecivableTmp.Value
                BalanceBills = accountRecivableTmp.Balance
                ConceptValue = accountRecivableTmp.PortfolioAccountReceivableAccounting.ToList().Find(Function(x) x.AccountReceivableId.Id = accountRecivableTmp.Id And x.MainAccountId.Id = cashReceiptConcept.IdMainAccount).Balance.ToString()
                AssingValuesaccountReceivable(accountRecivableTmp)
            End If
        End If
    End Sub

    Private Sub INDSleThirdParty_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleThirdParty.EditValueChanged
        If INDSleThirdParty.EditValue IsNot Nothing Then
            INDSleBills.Enabled = True
        Else
            INDSleBills.Enabled = False
        End If
    End Sub

    Private Sub INDSleAdvancePayment_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleAdvancePayment.EditValueChanged
        If INDSleAdvancePayment.EditValue IsNot Nothing Then
            If editModePopups = False Then
                Dim AdvancePaymentTmp = TryCast(TryCast(INDGvSleAdvancePayment.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, AdvancePaymentsXpo)

                If AdvancePaymentTmp?.CurrencyId <> Me.CurrencyDetailId Then
                    CleanControlsPopupAdvancePayment()
                    Me.Mensaje(EeventViewerImages.Advertencia) = "La moneda del anticipo es diferente a la del detalle de recibo de caja"
                    Exit Sub
                End If

                INDTxtAdvancePaymentValue.EditValue = AdvancePaymentTmp.Value
                    INDTxtAdvancePaymentBalance.EditValue = AdvancePaymentTmp.Balance
                    AssingValuesAdvancePayment(AdvancePaymentTmp)
                End If
            End If
    End Sub

    Private Sub SetFormatsControls(Abbreviation As String)
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = Abbreviation.GetNumberFormat()
        INDTxtValue.Properties.Mask.Culture = _culture
        INDTxtPortfolioAdvance.Properties.Mask.Culture = _culture
        INDTxtTRM.Properties.Mask.Culture = _culture
        INDTxtTRM1.Properties.Mask.Culture = _culture
        INDTxtValueBills.Properties.Mask.Culture = _culture
        INDTxtBalanceBills.Properties.Mask.Culture = _culture
        INDTxtConceptValue.Properties.Mask.Culture = _culture
        INDTxtBillValue.Properties.Mask.Culture = _culture
        Me.INDTxtAdvancePaymentValue.Properties.Mask.Culture = _culture
        Me.INDTxtAdvancePaymentBalance.Properties.Mask.Culture = _culture
        Me.INDTxtRepaymentValue.Properties.Mask.Culture = _culture
        Me.GridColumnConceptBalance = Window.Utils.FormatGrid(GridColumnConceptBalance, Abbreviation)
        Me.GridColumnPayValue = Window.Utils.FormatGrid(GridColumnPayValue, Abbreviation)
        Me.GridColumn1003 = Window.Utils.FormatGrid(GridColumn1003, Abbreviation)
        Me.GridColumn1004 = Window.Utils.FormatGrid(GridColumn1004, Abbreviation)
        Me.GridColumn1006 = Window.Utils.FormatGrid(GridColumn1006, Abbreviation)
    End Sub

    Private Async Sub INDSleCurrency_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCurrency.EditValueChanged
        If Me.CurrencyDetailId <> cashReceiptsCurrencyId Then
            Dim TRM
            Using Model As New MPortfolioTransfers("")
                Dim Result = Await Model.GetTRMbyCurrencyId(cashReceiptsCurrencyId, Me.CurrencyDetailId)
                ''En caso de que no tenga trm para la conversion no debe permitir la acción
                If Result Is Nothing OrElse Not Result?.StateResult Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = "No existe un TRM a la fecha para esta moneda"
                    INDTxtTRM.EditValue = 0
                    INDTxtTRM1.EditValue = 0
                    Me.CurrencyDetailId = Nothing
                    HideAllLabels()
                    Exit Sub
                End If
                TRM = Result.ObjectEmbbeded.Value
            End Using
            If INDLciValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                INDLciTRM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciValueTRM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else
                INDLciTRM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciValueTRM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
            Me.cashReceiptsCurrencyAbrreviation = INDSleCurrency.Text
            Me.SetFormatsControls(Me.cashReceiptsCurrencyAbrreviation)
            INDLciTRM1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciValueTRM1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDTxtTRM.EditValue = TRM
            INDTxtTRM1.EditValue = TRM
            ''cambio de formato de moneda
            If TRM > 0 Then
                INDTxtValueTRM.EditValue = Math.Round(INDTxtValue.EditValue / TRM, 4)
            End If
            If TRM > 0 Then
                INDTxtValueTRM1.EditValue = Math.Round(Me.PortfolioAdvancedValue / TRM, Me._decimals)
            End If
        Else
            Me.cashReceiptsCurrencyAbrreviation = If(String.IsNullOrEmpty(INDSleCurrency.Text), Me.cashReceiptsCurrencyAbrreviation, INDSleCurrency.Text)
            Me.SetFormatsControls(Me.cashReceiptsCurrencyAbrreviation)
            HideAllLabels()
        End If
    End Sub

    Private Sub INDTxtValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtValue.EditValueChanged
        If INDTxtTRM.EditValue > 0 Then
            INDTxtValueTRM.EditValue = Math.Round(INDTxtValue.EditValue / INDTxtTRM.EditValue, 4)
        End If
    End Sub

    Private Sub INDTxtPortfolioAdvance_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtPortfolioAdvance.EditValueChanged
        If INDTxtTRM1.EditValue > 0 Then
            INDTxtValueTRM1.EditValue = Math.Round(Me.PortfolioAdvancedValue / INDTxtTRM1.EditValue, Me._decimals)
        End If
    End Sub

    Private Sub HideAllLabels()
        INDLciTRM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciValueTRM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciTRM1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciValueTRM1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub


#End Region

#Region "EditValueChanging"
    Private Sub INDSePercentage_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDSePercentage.EditValueChanging
        If e.NewValue > 0 Then
            INDTxtBaseValue.Enabled = True
        Else
            INDTxtBaseValue.EditValue = 0
            INDTxtBaseValue.Enabled = False
        End If
        If INDTxtBaseValue.EditValue > 0 Then
            INDTxtBaseValue.EditValue = 0
            INDTxtRetentionValue.EditValue = 0
        End If
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' evento que se dispara al presionar click en el boton de aceptar del control
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDBtnOK_Click(sender As Object, e As EventArgs) Handles INDBtnOk.Click
        AddCashReceiptDetail()
    End Sub

    ''' <summary>
    ''' evento para agregar una cuota
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAddBill_Click(sender As Object, e As EventArgs) Handles INDBtnAddBill.Click
        If INDSleBills.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione una factura"
            Exit Sub
        End If
        If BillValue <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = INDLciBillValue.CustomizationFormText + ResourceManager.GetString("Empty")
            Exit Sub
        End If

        If BillValue > ConceptValue Then
            Mensaje(EeventViewerImages.Advertencia) = "El valor a pagar es mayor que el valor del concepto"
            Exit Sub
        End If
        If listAccountReceivable IsNot Nothing Then
            Dim accountReceivableTmp = listAccountReceivable.Find(Function(x) x.InvoiceNumber = accountReceivable.InvoiceNumber)
            If editModePopups = True Then
                accountReceivableTmp.PaymentValue = BillValue
            Else
                If accountReceivableTmp IsNot Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "La factura ya esta agregada"
                    Exit Sub
                End If
            End If
        End If

        Dim accountRecivableTmp As PortfolioAccountReceivableXpo

        Using model As New MCashReceipts(Me.Tag)
            accountRecivableTmp = model.GetPortfolioAccountReceivableXpo(INDSleBills.EditValue)
        End Using
        If indigo.IndigoCompanyType = 1 Then
            If cashReceiptConcept.IdMainAccount = accountRecivableTmp.AccountObjectionRemediedId Then
                Mensaje(EeventViewerImages.Informacion) = "La factura " + accountRecivableTmp.InvoiceNumber + " afectara saldos en glosas"
            End If
        Else
            If cashReceiptConcept.IdMainAccount = accountRecivableTmp.AccountRadicateId Then
                Mensaje(EeventViewerImages.Informacion) = "La factura " + accountRecivableTmp.InvoiceNumber + " afectara saldos en glosas"
            End If
        End If
        

        If listAccountReceivable Is Nothing Then
            listAccountReceivable = New List(Of AccountReceivable)
        End If
        INDGvBills.OptionsView.ShowFooter = True
        If editModePopups = False Then
            accountReceivable.Age = SetAgeBills(accountReceivable.ExpiredDate)
            accountReceivable.PaymentValue = BillValue
            listAccountReceivable.Add(accountReceivable)
        End If
        If listAccountReceivable IsNot Nothing AndAlso listAccountReceivable.Count > 1 Then
            listAccountReceivable = listAccountReceivable.OrderBy(Function(x) x.InvoiceNumber).ToList()
        End If
        INDGcBills.DataSource = Nothing
        INDGcBills.DataSource = listAccountReceivable
        CleanControlsPopupBills()
        Me.EnableCurrencyDetail()
    End Sub

    ''' <summary>
    ''' evento para agreagar un reintegro de anticipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAddAdvancePayment_Click(sender As Object, e As EventArgs) Handles INDBtnAddAdvancePayment.Click
        If INDSleAdvancePayment.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectAdvance", MODULE_NAME)
            Exit Sub
        End If
        If RepaymentValue = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            Exit Sub
        End If

        If RepaymentValue > advancePayment.Balance Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("LowerBalance", MODULE_NAME)
            Exit Sub
        End If


        If listCashReceiptAdvancePayment IsNot Nothing Then
            Dim cashReceiptAdvancePaymentTmp = listCashReceiptAdvancePayment.Find(Function(x) x.Code = advancePayment.Code)
            If editModePopups = True Then
                cashReceiptAdvancePaymentTmp.PaymentValue = RepaymentValue
            Else
                If cashReceiptAdvancePaymentTmp IsNot Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AddedAdvance", MODULE_NAME)
                    Exit Sub
                End If
            End If
        End If
        If listCashReceiptAdvancePayment Is Nothing Then
            listCashReceiptAdvancePayment = New List(Of CashReceiptAdvancePayment)
        End If
        INDGvRepaymentAdvances.OptionsView.ShowFooter = True
        If editModePopups = False Then
            cashReceiptAdvancePayment = New CashReceiptAdvancePayment
            cashReceiptAdvancePayment.AdvancePaymentId = advancePayment.Id
            cashReceiptAdvancePayment.Code = advancePayment.Code
            cashReceiptAdvancePayment.AdvancePaymentCode = advancePayment.Code
            cashReceiptAdvancePayment.Value = advancePayment.Value
            cashReceiptAdvancePayment.Balance = advancePayment.Balance
            cashReceiptAdvancePayment.PaymentValue = RepaymentValue
            cashReceiptAdvancePayment.ValueInCurrencyHeader = GetValueInCurrencyHeader(INDTxtTRM1.EditValue, cashReceiptAdvancePayment.PaymentValue)
            listCashReceiptAdvancePayment.Add(cashReceiptAdvancePayment)
        End If
        INDGcRepaymentAdvances.DataSource = Nothing
        INDGcRepaymentAdvances.DataSource = listCashReceiptAdvancePayment
        Me.EnableCurrencyDetail()
        CleanControlsPopupAdvancePayment()
    End Sub
#End Region

#Region "QueryPopUp"
    Private Sub INDSleThirdParty_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleThirdParty.QueryPopUp
        If ThirdPartyXPO Is Nothing Then
            If INDSleThirdParty.Properties.ReadOnly = False Then
                InitializeThirdPartyXPO()
            End If
        End If
    End Sub
    ''' <summary>
    ''' realiza la consulta cuando se despliega el popup
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    ''' 
    Private Sub INDSleCashReceiptConcept_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCashReceiptConcept.QueryPopUp
        If CashReceiptConceptXPO Is Nothing Then
            If INDSleCashReceiptConcept.Properties.ReadOnly = False Then
                InitializeCashReceiptConceptXPO()
            End If
        End If
    End Sub

    ''' <summary>
    ''' realiza la consulta cuando se despliega el popup
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleCostCenter_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCostCenter.QueryPopUp
        If CostCenterXPO Is Nothing Then
            If INDSleCostCenter.Properties.ReadOnly = False Then
                InitializeCostCenterXPO()
            End If
        End If
    End Sub

    ''' <summary>
    ''' realiza la consulta cuando se despliega el popup
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleRetentionConcept_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleRetentionConcept.QueryPopUp
        If RetentionConceptXPO Is Nothing Then
            If INDSleRetentionConcept.Properties.ReadOnly = False Then
                InitializeRetentionConceptXPO()
            End If
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara cuando se despliega el popup de facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleBills_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleBills.QueryPopUp
        If accountReceivableXPO Is Nothing Then
            Dim filter() As Object
            If INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                filter = {INDSleThirdParty.EditValue, cashReceiptConcept.IdMainAccount, INDSleCostCenter.EditValue}
            Else
                filter = {INDSleThirdParty.EditValue, cashReceiptConcept.IdMainAccount, 0}
            End If
            GetAccountReceivable(filter)
        End If
    End Sub

    Private Sub INDSleAdvancePayment_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAdvancePayment.QueryPopUp
        If AdvancePaymentXPO Is Nothing Then
            Dim filter() As Object
            If INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                filter = {INDSleThirdParty.EditValue, cashReceiptConcept.IdMainAccount, INDSleCostCenter.EditValue}
            Else
                filter = {INDSleThirdParty.EditValue, cashReceiptConcept.IdMainAccount, 0}
            End If
            GetAdvancePayment(filter)
        End If
    End Sub

    Private Sub INDSleCurrency_QueryPopup(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCurrency.QueryPopUp
        If INDSleCurrency.Properties.DataSource Is Nothing Then
            INDSleCurrency.Properties.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).CommonService.GetCurrency()
        End If
    End Sub
#End Region

#Region "LostFocus"
    Private Async Sub INDTxtBaseValue_LostFocus(sender As Object, e As EventArgs) Handles INDTxtBaseValue.LostFocus
        If retentionConcept IsNot Nothing Then
            If INDTxtBaseValue.EditValue <> 0 Then
                Try
                    Select Case retentionConcept.Retention
                        Case 1
                            INDTxtRetentionValue.EditValue = AccountingServices.CalculateRetention(CDec(INDTxtBaseValue.EditValue), retentionConcept)
                        Case 2
                            Using model As New MCompanySettings(TagForm)
                                Dim companySetting = Await model.GetCompanySettings()
                                INDTxtRetentionValue.EditValue = AccountingServices.CalculateRetention(CDec(INDTxtBaseValue.EditValue), retentionConcept, companySetting.UVT)
                            End Using
                        Case 3
                            INDTxtRetentionValue.EditValue = AccountingServices.CalculateRetention(CDec(INDTxtBaseValue.EditValue), retentionConcept.MinBase, CDec(INDSePercentage.EditValue))
                    End Select
                Catch ex As InvalidOperationException
                    If CheckForm(Me) = True Then
                        Mensaje(EeventViewerImages.Advertencia) = ex.Message
                    End If
                Catch ex As IndexOutOfRangeException
                    If CheckForm(Me) = True Then
                        Mensaje(EeventViewerImages.Advertencia) = ex.Message
                    End If
                Catch ex As ArgumentNullException
                    If CheckForm(Me) = True Then
                        Mensaje(EeventViewerImages.Advertencia) = ex.ParamName
                    End If
                Catch ex As ArgumentOutOfRangeException
                    If CheckForm(Me) = True Then
                        Mensaje(EeventViewerImages.Advertencia) = ex.ParamName
                        INDTxtBaseValue.EditValue = retentionConcept.MinBase
                    End If
                End Try
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectRetentionConcept", MODULE_NAME)
            INDSleRetentionConcept.Focus()
        End If
    End Sub
#End Region

#Region "KeyDown"
    Private Sub INDTxtValue_INDTxtBaseValue_KeyDown(sender As Object, e As KeyEventArgs) Handles INDTxtValue.KeyDown, INDTxtBaseValue.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDBtnOk.Focus()
        End If
    End Sub

    Private Sub PopupCashReceiptConcept_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub INDSleThirdParty_KeyDown(sender As Object, e As KeyEventArgs) Handles INDSleThirdParty.KeyDown
        If e.KeyCode = Keys.Enter Then
            If INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDSleCostCenter.Focus()
                Exit Sub
            End If
            If INDLciNatureCashReceiptConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If INDGleNatureCashReceiptConcept.Enabled = True Then
                    INDGleNatureCashReceiptConcept.Focus()
                    Exit Sub
                End If
            End If
            If INDLcgRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDSleRetentionConcept.Focus()
                Exit Sub
            End If
            If INDLcgRepaymentAdvances.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDPceRepaymentAdvances.Focus()
                INDPceRepaymentAdvances.ShowPopup()
                INDSleAdvancePayment.Focus()
                Exit Sub
            End If
            If INDLcgBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDPceBills.Focus()
                INDPceBills.ShowPopup()
                INDSleBills.Focus()
                Exit Sub
            End If
        End If
    End Sub

    Private Sub INDMeDetailPorfolioAdvance_KeyDown(sender As Object, e As KeyEventArgs) Handles INDMeDetailPorfolioAdvance.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDBtnOk.Focus()
        End If
    End Sub

    Private Sub INDSleAdmissionNumber_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDMeDetailPorfolioAdvance.Focus()
        End If
    End Sub
#End Region

#Region "Leave"
    Private Sub RepositoryItemTextEdit2_Leave(sender As Object, e As EventArgs) Handles RepositoryItemTextEdit2.Leave
        Dim accountReceivableTmp = DirectCast(INDGvBills.GetFocusedRow, AccountReceivable)
        Dim txtPaymentValue = DirectCast(sender, DevExpress.XtraEditors.TextEdit)
        If txtPaymentValue.EditValue > accountReceivableTmp.Balance Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ValuePayGreater", MODULE_NAME)
            accountReceivableTmp.PaymentValue = accountReceivableTmp.Balance
            txtPaymentValue.EditValue = accountReceivableTmp.Balance
            INDGcBills.RefreshDataSource()
        End If
    End Sub

    Private Sub INDTxtPortfolioAdvance_Leave(sender As Object, e As EventArgs) Handles INDTxtPortfolioAdvance.Leave
        If portfolioAdvance IsNot Nothing Then
            If Me.PortfolioAdvancedValue = 0 Then
                portfolioAdvance.Status = 0
                INDMeDetailPorfolioAdvance.Text = String.Empty
            Else
                portfolioAdvance.Status = 1
            End If
        End If
    End Sub

    Private Sub RepositoryItemTextEdit4_Leave(sender As Object, e As EventArgs) Handles RepositoryItemTextEdit4.Leave
        Dim advancePaymentTmp = DirectCast(INDGvRepaymentAdvances.GetFocusedRow, CashReceiptAdvancePayment)
        Dim txtAdvancePayment = DirectCast(sender, DevExpress.XtraEditors.TextEdit)
        If txtAdvancePayment.EditValue > advancePaymentTmp.Balance Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ValuePayGreater", MODULE_NAME)
            advancePaymentTmp.PaymentValue = advancePaymentTmp.Balance
            txtAdvancePayment.EditValue = advancePaymentTmp.Balance
            INDGcRepaymentAdvances.RefreshDataSource()
        End If
    End Sub
#End Region

#Region "FormClosing"
    Private Sub PopupCashReceiptConcept_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If INDSleCashReceiptConcept.EditValue IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub
#End Region

#Region "Click_ButtonAction"
    ''' <summary>
    ''' evento para editar o eliminar un detalle del recibo caja
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        accountReceivable = DirectCast(INDGvBills.GetFocusedRow(), AccountReceivable)
        listAccountReceivable.Remove(accountReceivable)
        Dim share As CashReceiptAccountReceivable = Nothing
        If cashReceiptsDetails IsNot Nothing Then
            share = cashReceiptsDetails.CashReceiptAccountReceivable.Where(Function(x) x.AccountReceivableId = accountReceivable.Id).FirstOrDefault()
        End If
        If share IsNot Nothing AndAlso share.Id > 0 Then
            If listDeleteShares Is Nothing Then
                listDeleteShares = New List(Of CashReceiptAccountReceivable)
            End If
            listDeleteShares.Add(share)
        End If
        INDGcBills.DataSource = Nothing
        INDGcBills.DataSource = listAccountReceivable
        If listAccountReceivable.Count = 0 Then
            IndigoGridControl1.RefreshGrid(INDGcBills)
            INDGvBills.OptionsView.ShowFooter = False
            INDGvBills.OptionsFind.AlwaysVisible = False
            IndigoGridControl1.SetExportButton(INDGcBills, False)
        Else
            INDGvBills.OptionsFind.AlwaysVisible = True
            IndigoGridControl1.SetExportButton(INDGcBills, True)
        End If
        CleanControlsPopupBills()
        Me.EnableCurrencyDetail()
    End Sub

    ''' <summary>
    ''' evento para editar o eliminar un metodo de pago
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction
        cashReceiptAdvancePayment = DirectCast(INDGvRepaymentAdvances.GetFocusedRow(), CashReceiptAdvancePayment)
        Dim advance As CashReceiptAdvancePayment = Nothing
        If cashReceiptsDetails IsNot Nothing Then
            advance = cashReceiptsDetails.CashReceiptAdvancePayment.Where(Function(x) x.AdvancePaymentId = cashReceiptAdvancePayment.AdvancePaymentId).FirstOrDefault()
        End If
        If advance IsNot Nothing AndAlso advance.Id > 0 Then
            If listCashReceiptAdvancePaymentDelete Is Nothing Then
                listCashReceiptAdvancePaymentDelete = New List(Of CashReceiptAdvancePayment)
            End If
            listCashReceiptAdvancePaymentDelete.Add(advance)
        End If
        listCashReceiptAdvancePayment.Remove(cashReceiptAdvancePayment)
        INDGcRepaymentAdvances.DataSource = Nothing
        INDGcRepaymentAdvances.DataSource = listCashReceiptAdvancePayment
        If listCashReceiptAdvancePayment.Count = 0 Then
            IndigoGridControl1.RefreshGrid(INDGcRepaymentAdvances)
            INDGvRepaymentAdvances.OptionsView.ShowFooter = False
            INDGvRepaymentAdvances.OptionsFind.AlwaysVisible = False
            IndigoGridControl1.SetExportButton(INDGcRepaymentAdvances, False)
        Else
            INDGvRepaymentAdvances.OptionsFind.AlwaysVisible = True
            IndigoGridControl1.SetExportButton(INDGcRepaymentAdvances, True)
        End If
        Me.EnableCurrencyDetail()
        CleanControlsPopupAdvancePayment()
    End Sub

    Private Sub IndigoGridView3_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView3.Click_ButtonAction
        CashReceiptDetailAccountPayable = DirectCast(INDGvAccountPayable.GetFocusedRow(), CashReceiptDetailAccountPayable)
        Dim accountPayable As CashReceiptDetailAccountPayable = Nothing
        If cashReceiptsDetails IsNot Nothing Then
            accountPayable = cashReceiptsDetails.CashReceiptDetailAccountPayable.Where(Function(x) x.AccountPayableId = CashReceiptDetailAccountPayable.AccountPayableId).FirstOrDefault()
        End If
        If accountPayable IsNot Nothing AndAlso accountPayable.Id > 0 Then
            If listCashReceiptDetailAccountPayableDelete Is Nothing Then
                listCashReceiptDetailAccountPayableDelete = New List(Of CashReceiptDetailAccountPayable)
            End If
            listCashReceiptDetailAccountPayableDelete.Add(accountPayable)
        End If
        listCashReceiptDetailAccountPayable.Remove(CashReceiptDetailAccountPayable)
        INDGcRepaymentAdvances.DataSource = listCashReceiptDetailAccountPayable
        INDGcRepaymentAdvances.RefreshDataSource()
        CleanControlsPopupAccountPayable()
    End Sub
#End Region

#Region "CloseUp"
    ''' <summary>
    ''' evento que se dispara cuando se cierra el popup de facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceBills_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDPceBills.CloseUp
        If editModePopups = True Then
            CleanControlsPopupBills()
        End If
        If listAccountReceivable IsNot Nothing AndAlso listAccountReceivable.Count > 0 Then
            INDGvBills.OptionsFind.AlwaysVisible = True
            IndigoGridControl1.SetExportButton(INDGcBills, True)
        End If
        If e.CloseMode = DevExpress.XtraEditors.PopupCloseMode.Cancel Then
            If listAccountReceivable IsNot Nothing AndAlso listAccountReceivable.Count > 0 Then
                IndigoGridControl1.ControlNextFocus = True
            Else
                INDTxtPortfolioAdvance.Focus()
            End If
        End If
    End Sub

    Private Sub INDPceRepaymentAdvances_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDPceRepaymentAdvances.CloseUp
        If editModePopups = True Then
            CleanControlsPopupAdvancePayment()
        End If
        If listCashReceiptAdvancePayment IsNot Nothing AndAlso listCashReceiptAdvancePayment.Count > 0 Then
            INDGvRepaymentAdvances.OptionsFind.AlwaysVisible = True
            IndigoGridControl1.SetExportButton(INDGcRepaymentAdvances, True)
        End If
        If e.CloseMode = DevExpress.XtraEditors.PopupCloseMode.Cancel Then
            INDBtnOk.Focus()
        End If
    End Sub
#End Region

#Region "PasteToGrid"
    ''' <summary>
    ''' evento para pegar facturas a la rejilla 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If sender.Name = INDGcBills.Name Then
            Me.Cursor = ChangeCursorIndigo()
            Using model As New MCashReceipts(TagForm)
                Dim result = Await model.SetBillsCashReceipts(e.Rows, INDSleThirdParty.EditValue, cashReceiptConcept.IdMainAccount, INDSleCostCenter.EditValue, OperatingUnitId)
                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    If listAccountReceivable IsNot Nothing AndAlso listAccountReceivable.Count > 0 Then
                        Dim listSharesTmp = (From s In result.ObjectEmbbeded Select s.InvoiceNumber).ToList.Distinct.ToList()
                        Dim listShareNotExist As New List(Of String)
                        For i As Integer = 0 To listSharesTmp.Count - 1 Step 1
                            Dim item = i
                            Dim share = listAccountReceivable.Where(Function(x) x.InvoiceNumber = listSharesTmp.Item((item))).FirstOrDefault()
                            If share IsNot Nothing Then
                                result.MessageResult.Add(String.Format(ResourceManager.GetString("InvoiceListExist", MODULE_NAME), share.InvoiceNumber))
                            Else
                                listShareNotExist.Add(listSharesTmp.Item((item)))
                            End If
                        Next
                        For i As Integer = 0 To listShareNotExist.Count - 1 Step 1
                            Dim item = i
                            listAccountReceivable.AddRange(result.ObjectEmbbeded.FindAll(Function(x) x.InvoiceNumber = listShareNotExist.Item(item)))
                        Next
                    Else
                        listAccountReceivable = result.ObjectEmbbeded
                    End If
                End If
                If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                    Using formulario As New FrmListErrors(result.MessageResult)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
                If listAccountReceivable IsNot Nothing AndAlso listAccountReceivable.Count > 1 Then
                    listAccountReceivable = listAccountReceivable.OrderBy(Function(x) x.InvoiceNumber).ToList()
                End If
                Me.Cursor = System.Windows.Forms.Cursors.Default
                INDGcBills.DataSource = Nothing
                INDGcBills.DataSource = listAccountReceivable
                If listAccountReceivable IsNot Nothing AndAlso listAccountReceivable.Count > 0 Then
                    INDGvBills.OptionsFind.AlwaysVisible = True
                    IndigoGridControl1.SetExportButton(INDGcBills, True)
                Else
                    INDGvBills.OptionsFind.AlwaysVisible = False
                    IndigoGridControl1.SetExportButton(INDGcBills, False)
                End If
            End Using
        End If
    End Sub
#End Region

#Region "Popup"
    Private Sub INDPceBills_Popup(sender As Object, e As EventArgs) Handles INDPceBills.Popup
        INDGvBills.OptionsFind.AlwaysVisible = False
        IndigoGridControl1.SetExportButton(INDGcBills, False)
    End Sub

    Private Sub INDPceRepaymentAdvances_Popup(sender As Object, e As EventArgs) Handles INDPceRepaymentAdvances.Popup
        INDGvRepaymentAdvances.OptionsFind.AlwaysVisible = False
        IndigoGridControl1.SetExportButton(INDGcRepaymentAdvances, False)
    End Sub
#End Region

#Region "Enter"
    Private Sub INDTxtConceptValue_Enter(sender As Object, e As EventArgs) Handles INDTxtConceptValue.Enter
        INDTxtBillValue.Focus()
    End Sub
#End Region

#End Region

#Region "BAR BUTTONS"
    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        If editModeForm = True Then
            RaiseEvent CashReceiptsDetailsNull(Nothing, EventArgs.Empty)
        End If
        CleanControls()
    End Sub
#End Region


    Private Sub INDSleAdmissionNumber_NewSelectedValue(sender As Object, e As SearchAdmissionClosingEventArgs) Handles INDSleAdmissionNumber.NewSelectedValue
        SetAdmission(e.AdmissionObject)
    End Sub

    Private Sub SetAdmission(record As Object)
        admission = record
        With admission
            Me.INDSleAdmissionNumber.SetNullText(String.Format(ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), admission.AdmissionCode.ToString().Trim(), admission.PatientCode.ToString().Trim(), admission.PatientName.ToString().Trim()))
            INDTxtAdmissionCode.Text = .AdmissionCode.ToString().Trim()
            If .AdmissionDate IsNot Nothing Then
                INDTxtAdmissionDate.Text = CDate(.AdmissionDate).ToLongDateString()
            End If
            If .AdmissionType IsNot Nothing Then
                INDTxtAdmissionType.Text = ResourceManager.GetString(String.Concat("AdmissionType", .AdmissionType.ToString().Trim()))
            End If
            If .AuthorizationNumber IsNot Nothing Then
                INDTxtAuthorizationNumber.Text = .AuthorizationNumber.ToString().Trim()
            End If
            If .AdmissionType.ToString().Trim() <> ResourceManager.GetString("OutpatientRevenue", MODULE_NAME) Then
                INDLciStay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                If .BedStay IsNot Nothing Then
                    INDTxtStay.Text = .BedStay.ToString().Trim()
                End If
            Else
                INDLciStay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            If .CareGroupId IsNot Nothing AndAlso .CareGroupId > 0 Then
                Using model As New Presentation.Contract.MVP.MCareGroup(Me.Tag)
                    Dim careGroupTmp = model.GetCareGroupByIdSimple(.CareGroupId).ObjectEmbbeded
                    If careGroupTmp IsNot Nothing AndAlso careGroupTmp.Id > 0 Then
                        INDTxtBenefitsPlan.Text = careGroupTmp.Code + " - " + careGroupTmp.Name
                    Else
                        INDTxtBenefitsPlan.Text = String.Empty
                    End If
                End Using
            End If

            If .HealthAdministratorId IsNot Nothing AndAlso .HealthAdministratorId > 0 Then
                Using model As New Presentation.Contract.MVP.MHealthAdministrator(Me.Tag)
                    Dim healthTmp = model.GetHealthAdministratorByIdSimple(.HealthAdministratorId).ObjectEmbbeded
                    If healthTmp IsNot Nothing AndAlso healthTmp.Id > 0 Then
                        INDTxtEntity.Text = healthTmp.Code + " - " + healthTmp.Name
                    Else
                        INDTxtEntity.Text = String.Empty
                    End If
                End Using
            End If

            If .LiquidationType IsNot Nothing Then
                INDTxtLiquidationType.Text = ResourceManager.GetString(String.Concat("LiquidationType", .LiquidationType.ToString().Trim()))
            End If
            If .PatientCode IsNot Nothing And .PatientName IsNot Nothing Then
                INDTxtPatient.Text = .PatientCode.ToString().Trim() + " - " + .PatientName.ToString().Trim()
            End If
            If .PlaceEntry IsNot Nothing Then
                INDTxtAdmissionPlace.Text = ResourceManager.GetString(String.Concat("PlaceEntry", .PlaceEntry.ToString().Trim()))
            End If
            If .ResponsibleName IsNot Nothing Then
                INDTxtResponsibleName.Text = .ResponsibleName.ToString().Trim()
            End If
            If .ResponsiblePhone IsNot Nothing Then
                INDTxtResponsiblePhone.Text = .ResponsiblePhone.ToString().Trim()
            End If
        End With
    End Sub

    Private Sub INDTxtBalanceBills_MouseEnter(sender As Object, e As EventArgs) Handles INDTxtBalanceBills.MouseEnter
        If INDSleBills.EditValue IsNot Nothing Then
            INDFpAccount.ShowBeakForm()
        End If
    End Sub

    Private Sub INDFpAccount_ButtonClick(sender As Object, e As FlyoutPanelButtonClickEventArgs)
        INDFpAccount.HideBeakForm()
    End Sub

    Private Sub INDSleAccountPayable_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAccountPayable.QueryPopUp
        If AccountPayableXPO Is Nothing Then
            Using model As New MCashReceipts(Me.Tag)
                AccountPayableXPO = model.ListAccountPayable(INDSleThirdParty.EditValue, cashReceiptConcept.IdMainAccount, If(INDSleCostCenter.EditValue IsNot Nothing, INDSleCostCenter.EditValue, 0))
            End Using
        End If
    End Sub

    Private Sub INDSleAccountPayable_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleAccountPayable.EditValueChanged
        If INDSleAccountPayable.EditValue IsNot Nothing Then
            If editModePopups = False Then
                Dim accountPayableTmp = DirectCast(DirectCast(INDSleGvAccountPayable.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, PaymentsAccountPayable)
                INDTxtAccountPayableBalance.EditValue = accountPayableTmp.Balance
                INDTxtMaximunRefundValue.EditValue = accountPayableTmp.Value - accountPayableTmp.Balance
                AssingValuesCashReceiptDetailAccountPayable(accountPayableTmp)
            End If
        End If
    End Sub


    Dim AccountPayable As AccountPayable
    Dim listCashReceiptDetailAccountPayable As List(Of CashReceiptDetailAccountPayable)
    Dim listCashReceiptDetailAccountPayableDelete As List(Of CashReceiptDetailAccountPayable)
    Dim CashReceiptDetailAccountPayable As CashReceiptDetailAccountPayable

    Private Sub AssingValuesCashReceiptDetailAccountPayable(accountPayableTmp As PaymentsAccountPayable)
        AccountPayable = New AccountPayable
        AccountPayable.Id = accountPayableTmp.Id
        AccountPayable.BillNumber = accountPayableTmp.BillNumber
        AccountPayable.Balance = accountPayableTmp.Balance
        AccountPayable.Value = accountPayableTmp.Value
    End Sub

    Private Sub INDBtnAddAccountPayable_Click(sender As Object, e As EventArgs) Handles INDBtnAddAccountPayable.Click
        Dim errors As New StringBuilder
        If INDSleAccountPayable.EditValue Is Nothing Then
            errors.AppendLine("Seleccione una cuenta por pagar")
        End If
        If INDTxtRefundValue.EditValue <= 0 Then
            errors.AppendLine("El valor debe ser mayor a cero")
        End If
        If CDec(INDTxtRefundValue.EditValue) > CDec(INDTxtMaximunRefundValue.EditValue) Then
            errors.AppendLine("El valor a reintegrar debe ser menor que el valor maximo a reintegrar")
        End If
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Exit Sub
        End If

        If listCashReceiptDetailAccountPayable IsNot Nothing Then
            Dim cashReceiptDetailAccountPayableTmp = listCashReceiptDetailAccountPayable.Find(Function(x) x.BillNumber = AccountPayable.BillNumber)
            If editModePopups = True Then
                cashReceiptDetailAccountPayableTmp.RefundValue = CDec(INDTxtRefundValue.EditValue)
            Else
                If cashReceiptDetailAccountPayableTmp IsNot Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "La Factura ya se encuentra agregada"
                    Exit Sub
                End If
            End If
        Else
            listCashReceiptDetailAccountPayable = New List(Of CashReceiptDetailAccountPayable)
        End If

        If editModePopups = False Then
            CashReceiptDetailAccountPayable = New CashReceiptDetailAccountPayable
            CashReceiptDetailAccountPayable.AccountPayableId = AccountPayable.Id
            CashReceiptDetailAccountPayable.BillNumber = AccountPayable.BillNumber
            CashReceiptDetailAccountPayable.Balance = AccountPayable.Balance
            CashReceiptDetailAccountPayable.BillNumber = AccountPayable.BillNumber
            CashReceiptDetailAccountPayable.MaximunRefundValue = AccountPayable.Value - AccountPayable.Balance
            CashReceiptDetailAccountPayable.RefundValue = CDec(INDTxtRefundValue.EditValue)
            listCashReceiptDetailAccountPayable.Add(CashReceiptDetailAccountPayable)
        End If
        INDGcAccountPayable.DataSource = listCashReceiptDetailAccountPayable
        INDGcAccountPayable.RefreshDataSource()
        CleanControlsPopupAccountPayable()
    End Sub


    ''' <summary>
    ''' metodo para limpiar los controles del popup de anticipos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopupAccountPayable()
        INDSleAccountPayable.EditValue = Nothing
        INDSleAccountPayable.Properties.ReadOnly = False
        INDSleAccountPayable.Properties.Buttons(0).Enabled = True
        INDSleAccountPayable.Properties.NullText = String.Empty
        INDTxtAccountPayableBalance.EditValue = 0
        INDTxtMaximunRefundValue.EditValue = 0
        INDTxtRefundValue.EditValue = 0
        CashReceiptDetailAccountPayable = Nothing
        editModePopups = False
        INDSleAccountPayable.Focus()
    End Sub

    Private Function SetBills() As Task
        Return Task.Factory.StartNew(Sub()
                                         'Dim listAccountReceivableId = (From e In cashReceiptsDetails.CashReceiptAccountReceivable Select e.AccountReceivableId).ToArray()
                                         'Using model As New MCashReceipts(Me.Tag)
                                         '    Dim list = model.GetAccountReceivableCashReceipts(listAccountReceivableId)
                                         '    Dim countList = model.GetCountAccountReceivableCashReceipts(listAccountReceivableId)
                                         '    If listAccountReceivable Is Nothing Then
                                         '        listAccountReceivable = New List(Of AccountReceivable)
                                         '    End If
                                         '    For Each item In list.ToList()
                                         '        accountReceivable = New AccountReceivable
                                         '        With accountReceivable
                                         '            .Id = item.Id
                                         '            .InvoiceNumber = item.InvoiceNumber
                                         '            .ExpiredDate = item.ExpiredDate
                                         '            .PaymentValue = item.Value
                                         '            .Balance = item.Portfolio_AccountReceivableAccountings.Where(Function(x) x.AccountReceivableId.Id = item.Id And x.MainAccountId = cashReceiptConcept.IdMainAccount).FirstOrDefault().Balance
                                         '            If countList <= 50 Then
                                         '                accountReceivable.Age = SetAgeBills(item.ExpiredDate)
                                         '            End If
                                         '        End With
                                         '        listAccountReceivable.Add(accountReceivable)
                                         '    Next
                                         'End Using
                                         If listAccountReceivable Is Nothing Then
                                             listAccountReceivable = New List(Of AccountReceivable)
                                         End If

                                         For Each item In cashReceiptsDetails.CashReceiptAccountReceivable
                                             Using model As New MCashReceipts(TagForm)
                                                 Dim accountReceivableXpo = model.GetAccountReceivableById(item.AccountReceivableId)
                                                 accountReceivable = New AccountReceivable
                                                 accountReceivable.Id = accountReceivableXpo.Id
                                                 accountReceivable.InvoiceNumber = accountReceivableXpo.InvoiceNumber
                                                 accountReceivable.ExpiredDate = accountReceivableXpo.ExpiredDate
                                                 accountReceivable.PaymentValue = item.Value
                                                 accountReceivable.Balance = accountReceivableXpo.PortfolioAccountReceivableAccounting.Where(Function(x) x.AccountReceivableId.Id = item.AccountReceivableId And x.MainAccountId.Id = cashReceiptConcept.IdMainAccount).FirstOrDefault().Balance

                                                 If cashReceiptsDetails.CashReceiptAccountReceivable.Count <= 50 Then
                                                     accountReceivable.Age = SetAgeBills(accountReceivable.ExpiredDate)
                                                 End If

                                             End Using
                                             listAccountReceivable.Add(accountReceivable)
                                         Next
                                     End Sub)
    End Function

End Class

