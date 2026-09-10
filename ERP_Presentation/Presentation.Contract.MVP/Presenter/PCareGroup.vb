'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 06/11/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo.MixingStationRepository

#End Region

Public Class PCareGroup

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As ICareGroup

    Dim ViewCareGroup As IGroupersCareGroup

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Filtro para las cuentas contables
    ''' </summary>
    Dim filter() As Object = {5, True}

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As ICareGroup)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New()
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    Public Sub InitializeContract()
        Using model As New MBusqueda
            View.ContractXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListContractSearchLookUp(1)
        End Using
    End Sub

    Public Sub InitializeRequirementTemplate()
        Using model As New MBusqueda
            View.RequirementsTemplateXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListRequirementTemplateByStatus, True)
        End Using
    End Sub

    Public Sub InitializeProcedureTemplate()
        Using model As New MBusqueda
            View.ProcedureTemplateXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListProcedureTemplateByStatus, True)
        End Using
    End Sub

    Public Sub InitializeProductTemplate()
        Using model As New MBusqueda
            View.ProductTemplateXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListProductTemplateByStatus, True)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las ciudades
    ''' </summary>
    Public Sub InitializeCostCenter()
        'Using model As New MBusqueda
        Me.View.CostCenterXpo = Infrastructure.Data.Xpo.XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.CostCenter)
        'End Using
    End Sub

    ''' <summary>
    ''' Lista las definiciones de tarifa por estado
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeDefinitionRate()
        View.DefinitionRateXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListDefinitionRateByStatus(True)
    End Sub

    ''' <summary>
    ''' Lista los servicios no facturables por estado
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeBillingItemsRestriction()
        View.BillingItemsRestrictionXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListBillingItemsRestrictionStatus(True)
    End Sub

    ''' <summary>
    ''' Lista las definiciones de tarifa por estado
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeContractAccountingStructure()
        View.ContractAccountingStructureXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListContractAccountingStructureByStatus(True)
    End Sub

    Public Sub InitializeTechnicalNote()
        Using model As New MBusqueda
            View.TechnicalNoteXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListTechnicalNoteByStatus(True)
        End Using
    End Sub

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListDashboardPgp(CareGroupId As Integer, MonthInitial As Integer, MonthEnd As Integer) As List(Of ViewListDashboardPgpXpo)
        Dim filtroConsulta As String = "CareGroupId = " & CareGroupId & " and GetMonth(DocumentDate) >= " & MonthInitial & " and GetMonth(DocumentDate) <= " & MonthEnd
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of ViewListDashboardPgpXpo)(Nothing, filtroConsulta)
    End Function

    ''' <summary>
    ''' Inicializa el datasource de tipos de entidad
    ''' </summary>
    Public Sub InitializeEntityTypeXpo(Type As Byte)
        Using model As New MBusqueda
            View.EntityTypeXpo = model.ConsultarEntidades(eDataSource.ListCompanyType, Type)
        End Using
    End Sub

    ''' <summary>
    ''' Lista los tipos de dosis unitarias que sean disitintos a las clases reempaque y reenvase
    ''' </summary>
    ''' <returns></returns>
    Public Function ListUnitDoseTypeNotRepackage() As List(Of MixinStationUnitDoseTypeXpo)
        Try
            Dim filtroConsulta As String = "  MSClass <> 5  AND  MSClass <> 7  "
            Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of MixinStationUnitDoseTypeXpo)(Nothing, filtroConsulta)
        Catch ex As Exception
            Return New List(Of MixinStationUnitDoseTypeXpo)
        End Try
    End Function

#Region "Budget Interface"

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
    Public Sub InitializeBillingBudget(ValidityId As Integer)
        View.BillingBudgetXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListBudgetByBudgetValidityIdAndTypeAndStatus(ValidityId, 2, 1, False)
    End Sub

    ''' <summary>
    ''' Carga el presupuesto de las facturas dependiendo de la vigencia
    ''' seleccionada y el estado confirmado
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializePromissoryNoteBudget(ValidityId As Integer)
        View.PromissoryNoteBudgetXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListBudgetByBudgetValidityIdAndTypeAndStatus(ValidityId, 2, 1, False)
    End Sub

    ''' <summary>
    ''' Carga el presupuesto de las facturas dependiendo de la vigencia
    ''' seleccionada y el estado confirmado
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeAccountReceivablePreviousValidityBudget(ValidityId As Integer)
        View.AccountReceivablePreviousValidityBudgetXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListBudgetByBudgetValidityIdAndTypeAndStatus(ValidityId, 2, 1, False)
    End Sub

    ''' <summary>
    ''' Carga el presupuesto de las facturas dependiendo de la vigencia
    ''' seleccionada y el estado confirmado
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializePortfolioRecoveryBudget(ValidityId As Integer)
        View.PortfolioRecoveryBudgetXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListBudgetByBudgetValidityIdAndTypeAndStatus(ValidityId, 2, 1, False)
    End Sub

    ''' <summary>
    ''' Carga los paquetes activos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeContractPackages()
        View.ContractPackageXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListContractPackages(True)
    End Sub

    ''' <summary>
    ''' Lista los paquetes para agregar
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListContractPackage(Ids As String) As List(Of ContractPackageXpo)
        Dim filtroConsulta As String = "Id IN (" & Ids & ")"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of ContractPackageXpo)(Nothing, filtroConsulta)
    End Function
#End Region

#End Region

End Class
