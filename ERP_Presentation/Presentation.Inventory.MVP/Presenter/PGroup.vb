'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 19/03/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class PGroup

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IGroup

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    Dim filter() As Object = {5, True}

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IGroup)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Inicializa todas las cuentas contables de los search
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeMainAccounts()
        InitializeIncomeAccountId()
        InitializeIncomeRecognitionMainAccountId()
        InitializeRemissionInputDebit()
        InitializeRemissionInputCredit()
        InitializeRemissionOutputCredit()
        InitializeRemissionOutputDebit()
        InitializeConsignmentMerchandiseDebit()
        InitializeConsignmentMerchandiseCredit()
        InitializeCounterpartCostConsignedInventory()
    End Sub

    Public Sub InitializeConceptCxP()
        InitializeInventoryAccountPayableConceptId()
        InitializeDeclarantRetentionAccountPayableConceptId()
        InitializeNotDeclarantRetentionAccountPayableConceptId()
    End Sub

    Public Sub InitializeIncomeAccountId()
        Using modelAccountsXPO As New MBusqueda
            Me.View.IncomeAccountIdXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeIncomeRecognitionMainAccountId()
        Using modelAccountsXPO As New MBusqueda
            Me.View.IncomeRecognitionMainAccountIdXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeInventoryAccountPayableConceptId()
        Dim filterConcept() As Object = {True, False, 2}
        Using model As New MBusqueda
            Me.View.InventoryAccountPayableConceptIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountPayableConceptByHandlesRetentionAndConceptType, filterConcept)
        End Using
    End Sub


    ''' <summary>
    ''' Inicializa el datasource de conceptos de retencion
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeDeclarantRetentionAccountPayableConceptId()
        Dim filterConcept() As Object = {True, True, 2}
        Using msearch As New MBusqueda
            Me.View.DeclarantRetentionAccountPayableConceptIdXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountPayableConceptByHandlesRetentionAndConceptType, filterConcept)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de conceptos de retencion
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeNotDeclarantRetentionAccountPayableConceptId()
        Dim filterConcept() As Object = {True, True, 2}
        Using msearch As New MBusqueda
            Me.View.NotDeclarantRetentionAccountPayableConceptIdXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountPayableConceptByHandlesRetentionAndConceptType, filterConcept)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de centro costo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeCostCenter()
        'Using modelAccountsXPO As New MBusqueda
        Me.View.CostCenterXpo = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.CostCenter)
        'End Using
    End Sub

    Public Sub InitializeRemissionInputDebit()
        Using modelAccountsXPO As New MBusqueda
            Me.View.ReferenceInputDebitAccountIdXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeRemissionInputCredit()
        Using modelAccountsXPO As New MBusqueda
            Me.View.ReferenceInputCreditAccountIdXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeRemissionOutputCredit()
        Using modelAccountsXPO As New MBusqueda
            Me.View.ReferenceOutputCreditAccountIdXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeRemissionOutputDebit()
        Using modelAccountsXPO As New MBusqueda
            Me.View.ReferenceOutputDebitAccountIdXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeConsignmentMerchandiseDebit()
        Using modelAccountsXPO As New MBusqueda
            Me.View.ConsignmentMerchandiseDebitAccountIdXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeConsignmentMerchandiseCredit()
        Using modelAccountsXPO As New MBusqueda
            Me.View.ConsignmentMerchandiseCreditAccountIdXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeCounterpartCostConsignedInventory()
        Using modelAccountsXPO As New MBusqueda
            Me.View.CounterpartCostConsignedInventoryIdXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa los datasource de los search de cuentas contables
    ''' </summary>
    Public Sub InitializeInventoryCostMainAccount()
        View.InventoryCostMainAccountXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListAccountsByLevel(5, True)
    End Sub

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetSettingsBilling(OperatingUnitId As Integer) As SettingsBillingXpo
        Dim filter As String = "IdOperatingUnit = " & OperatingUnitId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetCollection(Of SettingsBillingXpo)(Nothing, filter).FirstOrDefault()
    End Function

    Public Sub InitializeIVAAccount()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.IVAAccountXpo = ModelXpo.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeWithholdingTaxAccount()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.WithholdingTaxAccountXpo = ModelXpo.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeWithholdingICAAccount()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.WithholdingICAAccountXpo = ModelXpo.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Asigna el datasource del concepto de retención
    ''' </summary>
    Public Sub InitializeReteFuenteConcept()
        View.ReteFuenteConceptXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListRetentionConcept()
    End Sub

    Public Sub InitializeWithholdingICAConcept()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.WithholdingICAConceptXpo = ModelXpo.ConsultarEntidades(eDataSource.ListRetentionConcept)
        End Using
    End Sub

    Public Sub InitializeFunctionalUnit()
        Using model As New MBusqueda
            Me.View.FunctionalUnitIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFunctionalUnit, True)
        End Using
    End Sub

    Public Sub InitializeCostAccount()
        Dim filter() As Object = {5, True}
        Using modelAccountsXPO As New MBusqueda
            Me.View.CostAccountIdXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeSalesAccount()
        Dim filter() As Object = {5, True}
        Using modelAccountsXPO As New MBusqueda
            Me.View.SalesAccountIdXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeAccountingPackage()
        Dim filter() As Object = {5, True}
        Using modelXpo As New MBusqueda
            Me.View.AccountingPackageMainAccountIdXpo = modelXpo.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeAccountFavorableDeviation()
        Dim filter() As Object = {5, True}
        Using modelXpo As New MBusqueda
            Me.View.FavorableDeviationMainAccountIdXpo = modelXpo.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeAccountVariationPV()
        Dim filter() As Object = {5, True}
        Using modelXpo As New MBusqueda
            Me.View.VariationPVMainAccountIdXpo = modelXpo.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las entidades presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeBudgetaryEntity()
        View.BudgetaryEntityXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListBudgetEntityByStatus(True)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las vigencias
    ''' </summary>
    ''' <param name="budgetEntityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeBudgetaryValidity(budgetEntityId As Integer)
        View.BudgetaryValidityXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListValidityByBudgetByBudgetEntityIdAndStatus(budgetEntityId, 2)
    End Sub

    ''' <summary>
    ''' Carga el presupuesto de las facturas dependiendo de la vigencia
    ''' seleccionada y el estado confirmado
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeBudget(ValidityId As Integer)
        View.BudgetXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListBudgetByBudgetValidityIdAndTypeAndStatus(ValidityId, 2, 2, False)
    End Sub

    ''' <summary>
    ''' carga las cuentas contables para descuento, que son de nivel 5, sean activas y de tipo resultado
    ''' </summary>
    Public Sub InitializeDiscountAccount()
        Me.View.DiscountAccountXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListAccountsByLevel(5, True, ClassType:=2)
    End Sub
    ''' <summary>
    ''' Inicializa el datasource de la actividad economica
    ''' </summary>
    Public Sub InitializeEconomicActivity()
        Me.View.EconomicActivityDatasource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).TreasuryService.ListEconomicActivity()
    End Sub
#End Region

End Class
