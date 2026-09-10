'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Juan Carlos Bermudez Gutierrez
' Created          : 16-04-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository

#End Region

Public Class PSettingBilling

#Region "Fields"

    ''' <summary>
    ''' Referencia a la vista de la interfaz
    ''' </summary>
    ''' <remarks></remarks>
    Private View As ISettingBilling

    ''' <summary>
    ''' Instancia de los valores de sesión
    ''' </summary>
    ''' <remarks></remarks>
    Private _sessionValues As SessionValues

#End Region

#Region "Build"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="view"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal view As ISettingBilling)
        Me.View = view
        Me._sessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    Public Sub InitializeParticularHealthAdministrator()
        Using model As New MBusqueda
            Me.View.ParticularHealthAdministratorXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListHealthAdministrator)
        End Using
    End Sub

    'Cuenta Contables
    Public Sub InitializeRecoveryFeeDiscountMainAccount()
        Dim filter() As Object = {5, True}
        Using modelAccountsXPO As New MBusqueda
            Me.View.RecoveryFeeDiscountMainAccountXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeCapitationRevenueMainAccountId()
        Dim filter() As Object = {5, True}
        Using modelAccountsXPO As New MBusqueda
            Me.View.CapitationRevenueMainAccountXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeCapitationProfitMainAccountId()
        Dim filter() As Object = {5, True}
        Using modelAccountsXPO As New MBusqueda
            Me.View.CapitationProfitMainAccountXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeCapitationLossMainAccountId()
        Dim filter() As Object = {5, True}
        Using modelAccountsXPO As New MBusqueda
            Me.View.CapitationLossMainAccountXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Cuentas de movimientos en el campo de la Cuenta Contable Anulación Factura Operacional Vigencia Anterior
    ''' </summary>
    ''' <returns></returns> 
    Public Sub InitializeNullifyBillingOperationalCurrent()
        Dim filter() As Object = {5, True}
        Using modelAccountsXPO As New MBusqueda
            Me.View.ReversalPreviousYearsMainAccountIdXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub
    ''' <summary>
    ''' Cuentas de movimientos en el campo de la Cuenta Contable Anulación Factura NO Operacional Vigencia Anterior
    ''' </summary>
    ''' <returns></returns> 
    Public Sub InitializNullifyBillingNoOperationalCurrent()
        Dim filter() As Object = {5, True}
        Using modelAccountsXPO As New MBusqueda
            Me.View.ReversalPreviousYearsGenericBillingMainAccountIdXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    'conceptos de recibos de caja
    Public Sub InitializePatientAdvanceCashReceiptConceptId()
        Using model As New MBusqueda
            Me.View.PatientAdvanceCashReceiptConceptXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCashReceiptConcept)
        End Using
    End Sub

    Public Sub InitializeCapitedPatientAdvanceCashReceiptConceptId()
        Using model As New MBusqueda
            Me.View.CapitedPatientAdvanceCashReceiptConceptXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCashReceiptConcept)
        End Using
    End Sub

    Public Sub InitializeIndvidualAdvanceCashReceiptConceptId()
        Using model As New MBusqueda
            Me.View.IndvidualAdvanceCashReceiptConceptXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCashReceiptConcept)
        End Using
    End Sub

    Public Sub InitializeProductSalesCashReceiptConcept()
        Using model As New MBusqueda
            Me.View.ProductSalesCashReceiptConceptXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCashReceiptConcept)
        End Using
    End Sub

    Public Sub InitializeBasicBillingCashReceiptConceptXpo()
        Using model As New MBusqueda
            Me.View.BasicBillingCashReceiptConceptXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCashReceiptConcept)
        End Using
    End Sub

    Public Sub InitializeEntityCapitatedBillingAuthorization()
        Me.View.EntityCapitatedBillingAuthorizationXpo = XpoServiceEx.Instance(_sessionValues.TransactionalContainer).BillingService.ListBillingAuthotization()
    End Sub

    Public Function InitializeJournalVoucherType() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).AccountingService.ListDocumentTypes(True)
    End Function

    ''' <summary>
    ''' Inicializa los datasource de los search de cuentas contables
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeMainAccount() As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).AccountingService.ListAccountsByClass()
    End Function

    Public Function InitializeOrderMainAccount() As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).AccountingService _
            .ListXPInstantFeedbackSource(Of AccountingRepository.PUCServiceXpo)($"IdAccountClass IN (8,9) AND IdAccountLevel.Level = 5 AND Status = 1")
    End Function

    ''' <summary>
    ''' Inicializa los datasource de los search de cuentas contables
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeConcept() As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).AccountingService.ListRetentionConcept()
    End Function

    Public Sub InitializeFunctionalUnit()
        Me.View.FunctionalUnitXpo = XpoServiceEx.Instance(_sessionValues.TransactionalContainer).PayrollService.ListFunctionalUnitUserAuthorized(_sessionValues.UserIndigo)
    End Sub

    Public Sub InitializeHelathProfessional()
        Me.View.HealthProfessionalXpo = XpoServiceEx.Instance(_sessionValues.HisContainer).CrystalService.ListHealthCareProfessional()
    End Sub

    ''' <summary>
    ''' inicializa la consulta de centros de costo
    ''' </summary>
    Public Sub InitializeCostCenterXPO()
        Using model As New MBusqueda
            Me.View.CostCenterXPO = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetCostCenterByState, "True")
        End Using
    End Sub

    Public Sub InitializeRecoveryFeeDiscountCostCenter()
        Using model As New MBusqueda
            Me.View.RecoveryFeeCostCenterDiscountXPO = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetCostCenterByState, "True")
        End Using
    End Sub

    Public Function InitializeConceptNote() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).PortfolioService.ListPortfolioNoteConceptByNoteType(1, True)
    End Function

    Public Function InitializeConsignmentSalereCognitionXpo()
        Using model As New MBusqueda
            View.ConsignmentSalereCognitionXpo = model.ConsultarEntidades(eDataSource.ListAllInventoryGeneralLedgerJournalVoucherTypes)
        End Using
    End Function

    Public Function InitializeReversalRecognitionConsignmentSaleXpo() As XPInstantFeedbackSource
        Using model As New MBusqueda
            View.ReversalRecognitionConsignmentSaleXpo = model.ConsultarEntidades(eDataSource.ListAllInventoryGeneralLedgerJournalVoucherTypes)
        End Using
    End Function

    Public Sub InitializeSpecificCurrency()
        Dim filter = "State = True"
        View.SpecificCurrencytXpo = XpoServiceEx.Instance(_sessionValues.HisContainer).BillingService.ListXPInstantFeedbackSource(Of CommonCurrencyXpo)(filter:=filter)
    End Sub

    Public Function InitializeGiftProductOutletConceptXpo() As XPInstantFeedbackSource
        Dim filterConcept() As Object = {1, 2, 1}
        Using model As New MBusqueda
            View.GiftProductOutletConceptXpo = model.ConsultarEntidades(eDataSource.ListAdjustmentConceptByConceptType, filterConcept)
        End Using
    End Function

#Region "Budget Interface"

    ''' <summary>
    ''' Inicializa el datasource de las entidades presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeBudgetaryEntity()
        View.BudgetaryEntityXpo = XpoServiceEx.Instance(_sessionValues.TransactionalContainer).BudgetService.ListBudgetEntityByStatus(True)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las vigencias
    ''' </summary>
    ''' <param name="budgetEntityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeBudgetaryValidity(budgetEntityId As Integer)
        View.BudgetaryValidityXpo = XpoServiceEx.Instance(_sessionValues.TransactionalContainer).BudgetService.ListValidityByBudgetByBudgetEntityIdAndStatus(budgetEntityId, 2)
    End Sub

    ''' <summary>
    ''' Carga la dependencia de las facturas dependiendo de la vigencia
    ''' seleccionada y el estado confirmado
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeDependency(ValidityId As Integer)
        View.DependencyXpo = XpoServiceEx.Instance(_sessionValues.TransactionalContainer).BudgetService.GetDependency(ValidityId)
    End Sub

    ''' <summary>
    ''' Carga la dependencia de las facturas básicas dependiendo de la vigencia
    ''' seleccionada y el estado confirmado
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeBasicBillingDependency(ValidityId As Integer)
        View.BasicBillingDependencyXpo = XpoServiceEx.Instance(_sessionValues.TransactionalContainer).BudgetService.GetDependency(ValidityId)
    End Sub

    ''' <summary>
    ''' Carga el presupuesto de las facturas dependiendo de la vigencia
    ''' seleccionada y el estado confirmado
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeBasicBillingBudget(ValidityId As Integer)
        View.BasicBillingBudgetXpo = XpoServiceEx.Instance(_sessionValues.TransactionalContainer).BudgetService.ListBudgetByBudgetValidityIdAndTypeAndStatus(ValidityId, 2, 1, False)
    End Sub

#End Region

#End Region

End Class
