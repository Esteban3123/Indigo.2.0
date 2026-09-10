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
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls.MVP
Imports Presentation.Base
Imports Presentation.Payroll
Imports Presentation.Accounting
Imports Presentation.Controls
Imports Presentation.Treasury.MVP
Imports Presentation.Accounting.MVP
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Common
Imports Presentation.Base.BaseClass
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports System.Text
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports DevExpress.Xpo
Imports Presentation.Portfolio.MVP
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.Payments.MVP
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.Utils
Imports Presentation.Common.MVP
Imports Presentation.Billing.MVP
Imports System.Drawing
Imports System.Windows.Forms
Imports Presentation.Treasury
Imports Infrastructure.Data.Xpo

#End Region

Public Class PopupCashReceiptConceptLiquidation

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
            'LoadControlsForEdit(_cashReceiptDetail)
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
    Property AdmissionNumber As String
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
            CashReceiptConceptXPO = XpoServiceEx.Instance(indigo.TransactionalContainer).TreasuryService.ListCashReceiptConceptByAffectation(2, True) 'Afecta cartera
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
            If listCashReceiptAdvancePayment Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NotAddedAdvances", MODULE_NAME)
                Exit Sub
            End If
            If listCashReceiptAdvancePayment.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NotAddedAdvances", MODULE_NAME)
                Exit Sub
            End If
        End If
        If editModeForm = False Then
            If listConcept IsNot Nothing Then
                Dim concept As CashReceiptDetails
                If INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    concept = listConcept.Find(Function(x) x.IdCashReceiptConcept = INDSleCashReceiptConcept.EditValue And x.IdCostCenter = INDSleCostCenter.EditValue)
                    If concept IsNot Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("RepeatedCostCenterConcept", MODULE_NAME)
                        Exit Sub
                    End If
                Else
                    concept = listConcept.Find(Function(x) x.IdCashReceiptConcept = INDSleCashReceiptConcept.EditValue)
                    If concept IsNot Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("RepeatedConcept", MODULE_NAME)
                        Exit Sub
                    End If
                End If
                If CByte(cashReceiptConcept.Affectation) <> 1 Then
                    concept = listConcept.Find(Function(x) x.CashReceiptConceptAffectation > 1)
                    If concept IsNot Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ConceptWithBehavior", MODULE_NAME)
                        Exit Sub
                    End If
                End If
            End If
        End If
        If cashReceiptsDetails Is Nothing Then
            cashReceiptsDetails = New CashReceiptDetails
        End If

        cashReceiptsDetails.IdThirdParty = INDSleThirdParty.EditValue
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
        Else
            If INDLcgBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never And INDLcgRepaymentAdvances.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                cashReceiptsDetails.Value = CDec(INDTxtValue.EditValue)
            End If
            cashReceiptsDetails.Nature = INDGleNatureCashReceiptConcept.EditValue
        End If

        If INDLcgBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            Dim value = 0
            If INDTxtPortfolioAdvance.EditValue <> 0 Then
                If listAccountReceivable IsNot Nothing Then
                    value = listAccountReceivable.Sum(Function(x) x.PaymentValue)
                End If
                value += CDec(INDTxtPortfolioAdvance.EditValue)
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
                cashReceiptsDetails.Value = value
            End If
            If listAccountReceivable IsNot Nothing AndAlso listAccountReceivable.Count > 0 Then
                For Each item In listAccountReceivable
                    Dim cashReceiptAccountReceivable As CashReceiptAccountReceivable = New CashReceiptAccountReceivable
                    cashReceiptAccountReceivable.AccountReceivableId = item.Id
                    cashReceiptAccountReceivable.Value = item.PaymentValue
                    cashReceiptAccountReceivable.InvoiceNumber = item.InvoiceNumber
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
            If listDeleteShares IsNot Nothing AndAlso listDeleteShares.Count > 0 Then
                For Each item In listDeleteShares
                    item.MarkAsDeleted()
                Next
            End If
        End If

        If INDLcgRepaymentAdvances.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            cashReceiptsDetails.Value = listCashReceiptAdvancePayment.Sum(Function(x) x.PaymentValue)
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
                    End If
                Else
                    cashReceiptsDetails.CashReceiptAdvancePayment.Add(item)
                End If
            Next
            If listCashReceiptAdvancePaymentDelete IsNot Nothing AndAlso listCashReceiptAdvancePaymentDelete.Count > 0 Then
                For Each item In listCashReceiptAdvancePaymentDelete
                    item.MarkAsDeleted()
                Next
            End If
        End If

        Dim args As AddCashReceiptConceptEventArgs = New AddCashReceiptConceptEventArgs
        args.CashReceiptDetails = cashReceiptsDetails
        args.PortfolioAdvance = portfolioAdvance
        args.ListAccountReceivable = listAccountReceivable
        args.ListAdvancePayment = listCashReceiptAdvancePayment
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
        portfolioAdvance.Value = CDec(INDTxtPortfolioAdvance.EditValue)
        If admission IsNot Nothing Then
            portfolioAdvance.AdmissionNumber = AdmissionNumber 'admission.AdmissionCode.ToString().Trim()
        End If
        portfolioAdvance.Observations = INDMeDetailPorfolioAdvance.Text
        portfolioAdvance.DebitValue = 0
        portfolioAdvance.CreditValue = 0
        portfolioAdvance.Balance = CDec(INDTxtPortfolioAdvance.EditValue)
        If portfolioAdvance.Id = 0 Then
            portfolioAdvance.CreationUser = indigo.UserIndigo
            portfolioAdvance.Status = 1
        Else
            portfolioAdvance.ModificationUser = indigo.UserIndigo
        End If
    End Sub



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
            .Balance = INDTxtConceptValue.EditValue
            .Code = _accountReceivable.Code
            .ExpiredDate = _accountReceivable.ExpiredDate
            .Id = _accountReceivable.Id
            .InvoiceId = _accountReceivable.InvoiceId.Id
            .InvoiceNumber = _accountReceivable.InvoiceNumber
            .NumberShares = _accountReceivable.NumberShares
            .Observations = _accountReceivable.Observations
            .OpeningBalance = _accountReceivable.OpeningBalance
            .PaymentAgreement = _accountReceivable.PaymentAgreement
            .ThirdPartyId = _accountReceivable.ThirdPartyId.Id
            .Value = _accountReceivable.Value
            Dim SuperToolTipAccount As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
            Dim toolTipItemTitle As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
            toolTipItemTitle.Text = "Cuentas Contables" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
            Dim total As Decimal = 0
            SuperToolTipAccount.Items.Add(toolTipItemTitle)
            Dim toolTipItemBody As DevExpress.Utils.ToolTipItem = Nothing
            For Each item In _accountReceivable.PortfolioAccountReceivableAccounting.ToList()
                toolTipItemBody = New DevExpress.Utils.ToolTipItem()
                toolTipItemBody.LeftIndent = 6
                toolTipItemBody.Text = item.MainAccountId.NumberName & " - " & Format(item.Balance, "c0") & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
                SuperToolTipAccount.Items.Add(toolTipItemBody)
                total += item.Balance
            Next
            SuperToolTipAccount.Items.Add(New DevExpress.Utils.ToolTipSeparatorItem())
            Dim toolTipItemFooter As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
            toolTipItemFooter.LeftIndent = 6
            toolTipItemFooter.Text = "Total: " & Format(total, "c0")
            SuperToolTipAccount.Items.Add(toolTipItemFooter)
            SuperToolTipAccount.MaxWidth = 800
            INDTxtConceptValue.SuperTip = SuperToolTipAccount
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
        INDTxtValueBills.EditValue = 0
        INDTxtBalanceBills.EditValue = 0
        INDTxtConceptValue.EditValue = 0
        INDTxtBillValue.EditValue = 0
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
        INDTxtRepaymentValue.EditValue = 0
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
        INDTxtValue.Text = Nothing
        INDSleRetentionConcept.EditValue = Nothing
        INDSleRetentionConcept.Properties.NullText = Nothing
        INDSleRetentionConcept.Properties.ReadOnly = False
        INDSleRetentionConcept.Properties.Buttons(0).Enabled = True
        INDLciPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDSePercentage.EditValue = 0
        INDGleNatureRetention.EditValue = 2
        INDTxtBaseValue.EditValue = 0
        INDTxtBaseValue.Enabled = False
        INDTxtRetentionValue.EditValue = 0
        INDGleNatureCashReceiptConcept.Enabled = True
        INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLcgRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLcgBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLcgRepaymentAdvances.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLcgPortfolioAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciNatureCashReceiptConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDLciValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDTxtPortfolioAdvance.EditValue = 0
        INDMeDetailPorfolioAdvance.Text = String.Empty
        cashReceiptsDetails = Nothing
        cashReceiptConcept = Nothing
        accountReceivableXPO = Nothing
        INDBtnOk.Text = ResourceManager.GetString("Add")
        INDGcBills.DataSource = Nothing
        listAccountReceivable = Nothing
        accountReceivable = Nothing
        listDeleteShares = Nothing
        cashReceiptAdvancePayment = Nothing
        listCashReceiptAdvancePayment = Nothing
        listCashReceiptAdvancePaymentDelete = Nothing
        INDTxtBillValue.EditValue = 0
        INDTxtRepaymentValue.EditValue = 0
        editModeForm = False
    End Sub

    ''' <summary>
    ''' metodo para cargar los controles y editar un registro
    ''' </summary>
    ''' <param name="_cashReceiptDetail"></param>
    ''' <remarks></remarks>
    Public Sub LoadControlsForEdit(_cashReceiptDetail As CashReceiptDetails)
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
        If cashReceiptsDetails.IdCostCenter IsNot Nothing Then
            INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
        INDSleCostCenter.EditValue = cashReceiptsDetails.IdCostCenter
        INDSleCostCenter.Properties.NullText = cashReceiptsDetails.CodeNameCostCenter
        If cashReceiptsDetails.IdRetentionConcept IsNot Nothing Then
            INDGleNatureRetention.EditValue = cashReceiptsDetails.Nature
            INDTxtRetentionValue.EditValue = cashReceiptsDetails.Value
            INDSleRetentionConcept.EditValue = cashReceiptsDetails.IdRetentionConcept
            INDSleRetentionConcept.Properties.NullText = cashReceiptsDetails.CodeNameRetentionConcept
            INDSleRetentionConcept.Properties.ReadOnly = True
            INDSleRetentionConcept.Properties.Buttons(0).Enabled = False
            INDSePercentage.EditValue = cashReceiptsDetails.PercentageRetention
            INDTxtBaseValue.EditValue = cashReceiptsDetails.BaseValue
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
                        INDSleThirdParty.Properties.ReadOnly = False
                        INDSleThirdParty.Properties.Buttons(0).Enabled = True
                        INDGleNatureCashReceiptConcept.Enabled = True
                        INDLciValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Case 2
                        INDLcgRepaymentAdvances.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLcgBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDLcgPortfolioAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                        INDSleThirdParty.Properties.ReadOnly = True
                        INDSleThirdParty.Properties.Buttons(0).Enabled = False
                        INDGleNatureCashReceiptConcept.Enabled = False
                        If portfolioAdvance IsNot Nothing Then
                            INDTxtPortfolioAdvance.EditValue = portfolioAdvance.Value
                            INDMeDetailPorfolioAdvance.Text = portfolioAdvance.Observations
                        End If
                        INDLciValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                        'Recorro los items para agregarlos a la regilla
                        For Each item In cashReceiptsDetails.CashReceiptAccountReceivable
                            If listAccountReceivable Is Nothing Then
                                listAccountReceivable = New List(Of AccountReceivable)
                            End If
                            Using modelAccountReceivable As New MAccountReceivable(TagForm)
                                accountReceivable = modelAccountReceivable.GetAccountReceivableById(item.AccountReceivableId)
                                accountReceivable.PaymentValue = item.Value
                                accountReceivable.Balance = accountReceivable.AccountReceivableAccounting.Where(Function(x) x.AccountReceivableId = accountReceivable.Id And x.MainAccountId = cashReceiptConcept.IdMainAccount).FirstOrDefault().Balance
                                accountReceivable.Age = SetAgeBills(accountReceivable.ExpiredDate)
                            End Using
                            listAccountReceivable.Add(accountReceivable)
                        Next
                        INDGcBills.DataSource = Nothing
                        INDGcBills.DataSource = listAccountReceivable
                    Case 3
                        INDLcgBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLcgRepaymentAdvances.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDLcgPortfolioAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
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

                End Select
            End Using
        End If
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
        INDBtnExportBillsStructure.AddRangeColumns("Factura", "Valor")
        IndigoGridControl1.SetControlNextFocus(INDGcBills, INDTxtPortfolioAdvance)
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.Minimizar(True)
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        InitializeThirdPartyXPO()
        If cashReceiptsDetails Is Nothing Then
            CleanControls()
        Else
            LoadControlsForEdit(cashReceiptsDetails)
        End If
        AddActionsColumns()
        INDSleBills.Properties.Buttons.Item(1).Visible = False
        INDSleAdvancePayment.Properties.Buttons.Item(1).Visible = False
        IndigoGridControl1.RefreshGrid(INDGcRepaymentAdvances)
        IndigoGridControl1.RefreshGrid(INDGcBills)
        INDGvRepaymentAdvances.OptionsFind.AlwaysVisible = True
        INDGvBills.OptionsFind.AlwaysVisible = True
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
                'formulario.ViewModeEditHold = True
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog()
                InitializeCashReceiptConceptXPO()
                Dim value = INDSleCashReceiptConcept.EditValue
                INDSleCashReceiptConcept.EditValue = Nothing
                INDSleCashReceiptConcept.EditValue = value
                INDSleCashReceiptConcept.Focus()
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
                    INDLciValue.AllowHide = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciValue.AllowHide = True
                Else
                    Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgRetention, True)
                    INDLcgRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciNatureCashReceiptConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciNatureCashReceiptConcept.AllowHide = False
                    INDLciValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciValue.AllowHide = False
                End If
                Select Case cashReceiptConcept.Affectation
                    Case 1
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
                    Case 2
                        Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgPortfolioAdvance, False)
                        INDLcgPortfolioAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgBills, False)
                        INDLcgBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgRepaymentAdvances, True)
                        INDLcgRepaymentAdvances.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
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
                    Case 3
                        Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgPortfolioAdvance, True)
                        INDLcgPortfolioAdvance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgBills, True)
                        INDLcgBills.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        Me.LayoutControls.SetAllowHideInLayoutControlGroup(INDLcgRepaymentAdvances, False)
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
                End Select
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
                    'INDTxtBaseValue.EditValue = 0
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
                Dim accountRecivableTmp = DirectCast(DirectCast(INDGvSleBills.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, PortfolioAccountReceivableXpo)
                INDTxtValueBills.Text = accountRecivableTmp.Value
                INDTxtBalanceBills.Text = accountRecivableTmp.Balance
                INDTxtConceptValue.Text = accountRecivableTmp.PortfolioAccountReceivableAccounting.ToList().Find(Function(x) x.AccountReceivableId.Id = accountRecivableTmp.Id And x.MainAccountId.Id = cashReceiptConcept.IdMainAccount).Balance.ToString()
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
                Dim AdvancePaymentTmp = DirectCast(DirectCast(INDGvSleAdvancePayment.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, AdvancePaymentsXpo)
                INDTxtAdvancePaymentValue.EditValue = AdvancePaymentTmp.Value
                INDTxtAdvancePaymentBalance.EditValue = AdvancePaymentTmp.Balance
                AssingValuesAdvancePayment(AdvancePaymentTmp)
            End If
        End If
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
        If INDTxtBillValue.EditValue <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = INDLciBillValue.CustomizationFormText + ResourceManager.GetString("Empty")
            Exit Sub
        End If

        If CDec(INDTxtBillValue.EditValue) > INDTxtConceptValue.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = "El valor a pagar es mayor que el valor del concepto"
            Exit Sub
        End If
        If listAccountReceivable IsNot Nothing Then
            Dim accountReceivableTmp = listAccountReceivable.Find(Function(x) x.InvoiceNumber = accountReceivable.InvoiceNumber)
            If editModePopups = True Then
                accountReceivableTmp.PaymentValue = CDec(INDTxtBillValue.EditValue)
            Else
                If accountReceivableTmp IsNot Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "La factura ya esta agregada"
                    Exit Sub
                End If
            End If
        End If

        If listAccountReceivable Is Nothing Then
            listAccountReceivable = New List(Of AccountReceivable)
        End If
        INDGvBills.OptionsView.ShowFooter = True
        If editModePopups = False Then
            accountReceivable.Age = SetAgeBills(accountReceivable.ExpiredDate)
            accountReceivable.PaymentValue = INDTxtBillValue.EditValue
            listAccountReceivable.Add(accountReceivable)
        End If
        If listAccountReceivable IsNot Nothing AndAlso listAccountReceivable.Count > 1 Then
            listAccountReceivable = listAccountReceivable.OrderBy(Function(x) x.InvoiceNumber).ToList()
        End If
        INDGcBills.DataSource = Nothing
        INDGcBills.DataSource = listAccountReceivable
        CleanControlsPopupBills()
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
        If INDTxtRepaymentValue.EditValue = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            Exit Sub
        End If

        If CDec(INDTxtRepaymentValue.EditValue) > advancePayment.Balance Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("LowerBalance", MODULE_NAME)
            Exit Sub
        End If


        If listCashReceiptAdvancePayment IsNot Nothing Then
            Dim cashReceiptAdvancePaymentTmp = listCashReceiptAdvancePayment.Find(Function(x) x.Code = advancePayment.Code)
            If editModePopups = True Then
                cashReceiptAdvancePaymentTmp.PaymentValue = CDec(INDTxtRepaymentValue.EditValue)
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
            cashReceiptAdvancePayment.PaymentValue = CDec(INDTxtRepaymentValue.EditValue)
            listCashReceiptAdvancePayment.Add(cashReceiptAdvancePayment)
        End If
        INDGcRepaymentAdvances.DataSource = Nothing
        INDGcRepaymentAdvances.DataSource = listCashReceiptAdvancePayment
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
#End Region

#Region "LostFocus"
    Private Async Sub INDTxtBaseValue_LostFocus(sender As Object, e As EventArgs) Handles INDTxtBaseValue.LostFocus
        If retentionConcept IsNot Nothing Then
            If INDTxtBaseValue.EditValue <> 0 Then
                Try
                    Select Case retentionConcept.Retention
                        Case 1
                            INDTxtRetentionValue.EditValue = Math.Round(AccountingServices.CalculateRetention(CDec(INDTxtBaseValue.EditValue), retentionConcept))
                        Case 2
                            Using model As New MCompanySettings(TagForm)
                                Dim companySetting = Await model.GetCompanySettings()
                                INDTxtRetentionValue.EditValue = Math.Round(AccountingServices.CalculateRetention(CDec(INDTxtBaseValue.EditValue), retentionConcept, companySetting.UVT))
                            End Using
                        Case 3
                            INDTxtRetentionValue.EditValue = Math.Round(AccountingServices.CalculateRetention(CDec(INDTxtBaseValue.EditValue), retentionConcept.MinBase, CDec(INDSePercentage.EditValue)))
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
            If INDTxtPortfolioAdvance.EditValue = 0 Then
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
        CleanControlsPopupAdvancePayment()
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

End Class