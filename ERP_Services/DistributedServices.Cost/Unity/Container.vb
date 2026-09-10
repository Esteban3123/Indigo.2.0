#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.Data.ModelRepository
Imports Microsoft.Practices.Unity
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.PayrollRepository
Imports Application.Cost
Imports Domain.Payroll
Imports Application.Payments
Imports System.ServiceModel
Imports DistributedServices.Authentication

#End Region

Public NotInheritable Class Container

#Region "Singleton"

    ''' <summary>
    ''' Unica instancia del contenedor
    ''' </summary>
    Private Shared _currentContainer As IUnityContainer

    ''' <summary>
    ''' Obtiene la unica instancia del contenedor
    ''' </summary>
    ''' <returns>Contenedor configurado</returns>
    Public Shared ReadOnly Property Current() As IUnityContainer
        Get
            Dim containerInfo = JwtFactory.GetContainerFromToken()
            Dim container As String = containerInfo?.container
            Dim hisContainer As String = containerInfo?.hisContainer

            If _currentContainer IsNot Nothing Then
                Dim sessionVariables As ICommonVariables = _currentContainer.Resolve(Of ICommonVariables)()
                If sessionVariables.getContainer().Equals(container) Then
                    Return _currentContainer
                End If
            End If


            ConfigureContainer(container, hisContainer)

            Return _currentContainer
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Configura las dependencias en el contenedor
    ''' </summary>
    Private Shared Sub ConfigureContainer(container As String, hisContainer As String)
        Dim newContainer = New UnityContainer()

        newContainer.RegisterType(Of ICommonVariables)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                 Return New CommonVariables(container, hisContainer)
                                                                                                             End Function))
        'Inyectamos el contexto
        newContainer.RegisterType(Of IGlobalModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                       Return New GlobalModelUnitOfWork(container)
                                                                                                                   End Function))
        'Inyectamos el contexto de payroll
        newContainer.RegisterType(Of IPayrollUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                   Return New PayrollUnitOfWork(container)
                                                                                                               End Function))


        Dim injector As New ModuleInjector()
        injector.Load(newContainer, GetType(Domain.Base.Inject))

        'Inyectamos el servicio WCF
        newContainer.RegisterType(Of ICostService, CostService)()

        newContainer.RegisterType(Of ICostBlockRecordAdminService, CostBlockRecordAdminService)()
        newContainer.RegisterType(Of IBlockRecordCostRepository, BlockRecordCostRepository)()

        newContainer.RegisterType(Of ICostOrganizationalStructureAdminService, CostOrganizationalStructureAdminService)()
        newContainer.RegisterType(Of ICostOrganizationalStructureRepository, CostOrganizationalStructureRepository)()

        newContainer.RegisterType(Of ICostSequenceAdminService, CostSequenceAdminService)()
        newContainer.RegisterType(Of ICostSequenceRepository, CostSequenceRepository)()
        newContainer.RegisterType(Of ICostSequenceDetailRepository, CostSequenceDetailRepository)()

        newContainer.RegisterType(Of ICostSettingAdminService, CostSettingAdminService)()
        newContainer.RegisterType(Of ICostSettingRepository, CostSettingRepository)()

        newContainer.RegisterType(Of ICostDistributionIntermediateRepository, CostDistributionIntermediateRepository)()
        newContainer.RegisterType(Of ICostDistributionIntermediateAdminService, CostDistributionIntermediateAdminService)()

        newContainer.RegisterType(Of ICostProductionCenterCategoryRepository, CostProductionCenterCategoryRepository)()
        newContainer.RegisterType(Of ICostProductionCenterCategoryAdminService, CostProductionCenterCategoryAdminService)()

        newContainer.RegisterType(Of ICostProductionCenterRepository, CostProductionCenterRepository)()
        newContainer.RegisterType(Of ICostProductionCenterAdminService, CostProductionCenterAdminService)()

        newContainer.RegisterType(Of ICostGeneralExpensesRepository, CostGeneralExpensesRepository)()
        newContainer.RegisterType(Of ICostGeneralExpenseAdminService, CostGeneralExpenseAdminService)()

        'Distribución de elementos del costo
        newContainer.RegisterType(Of ICostDistributionDirectCostRepository, CostDistributionDirectCostRepository)()
        newContainer.RegisterType(Of ICostDistributionDirectCostAdminService, CostDistributionDirectCostAdminService)()
        newContainer.RegisterType(Of ICostDistributionDirectCostDetailIvaRepository, CostDistributionDirectCostDetailIvaRepository)()

        newContainer.RegisterType(Of ICostDistributionSecondaryRepository, CostDistributionSecondaryRepository)()
        newContainer.RegisterType(Of ICostDistributionSecondaryAdminService, CostDistributionSecondaryAdminService)()

        newContainer.RegisterType(Of ICostDistributionManpowerRepository, CostDistributionManpowerRepository)()
        newContainer.RegisterType(Of ICostDistributionManpowerAdminService, CostDistributionManpowerAdminService)()
        newContainer.RegisterType(Of IEmployeeRepository, EmployeeRepository)()
        newContainer.RegisterType(Of IGroupRepository, GroupRepository)()

        newContainer.RegisterType(Of Domain.Entities.Service.ICostServices, Domain.Entities.Service.CostServices)()

        newContainer.RegisterType(Of IEstimateCostNativeRepository, EstimateCostNativeRepository)()
        newContainer.RegisterType(Of ICostEstimationNativeAdminService, CostEstimationNativeAdminService)()

        newContainer.RegisterType(Of ICostDistributionFixedAssetRepository, CostDistributionFixedAssetRepository)()
        newContainer.RegisterType(Of ICostDistributionFixedAssetAdminService, CostDistributionFixedAssetAdminService)()

        newContainer.RegisterType(Of ICostGeneralExpenseCategoryRepository, CostGeneralExpenseCategoryRepository)()
        newContainer.RegisterType(Of ICostGeneralExpenseCategoryAdminService, CostGeneralExpenseCategoryAdminService)()

        newContainer.RegisterType(Of ICostLogisticsProductionCenterRecordReporsitory, CostLogisticsProductionCenterRecordRepository)()
        newContainer.RegisterType(Of ICostLogisticsProductionCenterRecordAdminService, CostLogisticsProductionCenterRecordAdminService)()

        newContainer.RegisterType(Of ICostDirectDistributionSecondaryRepository, CostDirectDistributionSecondaryRepository)()
        newContainer.RegisterType(Of ICostDirectDistributionSecondaryAdminService, CostDirectDistributionSecondaryAdminService)()

        newContainer.RegisterType(Of IAverageStandardCostRepository, AverageStandardCostRepository)()
        newContainer.RegisterType(Of IAverageStandardCostAdminService, AverageStandardCostAdminService)()
        newContainer.RegisterType(Of IAverageStandardCostDetailsRepository, AverageStandardCostDetailsRepository)()

        newContainer.RegisterType(Of ICupsEntityRepository, CupsEntityRepository)()
        'Exogena Format
        newContainer.RegisterType(Of IFormatosExogena, FormatosExogena)()

        'Grupo de Productos
        newContainer.RegisterType(Of ICostInventoryGroupRepository, CostInventoryGroupRepository)()
        newContainer.RegisterType(Of ICostInventoryGroupAdminService, CostInventoryGroupAdminService)()

        'Costos ABC - Actividades
        newContainer.RegisterType(Of ICostActivityRepository, CostActivityRepository)()
        newContainer.RegisterType(Of ICostActivityAdminService, CostActivityAdminService)()

        'Cuenta por pagar
        newContainer.RegisterType(Of IAccountPayableRepository, AccountPayableRepository)()
        newContainer.RegisterType(Of IAccountPayableAdminService, AccountPayableAdminService)()

        newContainer.RegisterType(Of ISequensePaymentsCRepository, SequensePaymentsCRepository)()
        newContainer.RegisterType(Of ISequensePaymentsDRepository, SequensePaymentsDRepository)()
        newContainer.RegisterType(Of ISettingPaymentsRepository, SettingPaymentsRepository)()
        newContainer.RegisterType(Of IDeferredCausationAdminService, DeferredCausationAdminService)()
        newContainer.RegisterType(Of IPaymentControlAdminService, PaymentControlAdminService)()
        newContainer.RegisterType(Of IDeferredCausationRepository, DeferredCausationRepository)()
        newContainer.RegisterType(Of ICloseMonthRepository, CloseMonthRepository)()
        newContainer.RegisterType(Of IPUCRepository, PUCRepository)()
        newContainer.RegisterType(Of ICompanySettingsRepository, CompanySettingsRepository)()
        newContainer.RegisterType(Of ISupplierRepository, SupplierRepository)()
        newContainer.RegisterType(Of IMonthlyAmortizationRepository, MonthlyAmortizationRepository)()
        newContainer.RegisterType(Of IPaymentControlRepository, PaymentControlRepository)()
        newContainer.RegisterType(Of IAccountPayableDetailConceptRepository, AccountPayableDetailConceptRepository)()

        newContainer.RegisterType(Of IGeneralLedgerIVARepository, GeneralLedgerIVARepository)()

        'Reportes
        newContainer.RegisterType(Of Application.Cost.IReportsAdminService, Application.Cost.ReportsAdminService)()
        newContainer.RegisterType(Of IAccountPayableAdminService, AccountPayableAdminService)()
        newContainer.RegisterType(Of Application.EventHandlers.IEventProxy, Application.EventHandlers.Proxies.AzureServiceBusProxy)()
        newContainer.RegisterType(Of IElectronicSupportDocumentRepository, ElectronicSupportDocumentRepository)()
        newContainer.RegisterType(Of Domain.Entities.ISettingsAccountRepository, SettingAccountRepository)()

        newContainer.RegisterType(Of Domain.Entities.IAccountReceivableRepository, AccountReceivableRepository)()
        newContainer.RegisterType(Of ICostIntermediateDistributionAdminService, CostIntermediateDistributionAdminService)()
        newContainer.RegisterType(Of ICostIntermediateDistributionRepository, CostIntermediateDistributionRepository)()

        _currentContainer = newContainer



    End Sub

#End Region

End Class