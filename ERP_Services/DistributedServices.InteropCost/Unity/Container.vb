#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.Data.ModelRepository
Imports Microsoft.Practices.Unity
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.MaintenanceRepository
Imports Infrastructure.Data.PayrollRepository
Imports Application.InteropCost
Imports Infrastructure.Data.InteropCostRepository
Imports Domain.InteropCost
Imports Domain.Entities.Service
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
            Dim costContainer As String = containerInfo?.costContainer

            If _currentContainer IsNot Nothing Then
                Dim sessionVariables As ICommonVariables = _currentContainer.Resolve(Of ICommonVariables)()
                If sessionVariables.getContainer().Equals(container) Then
                    Return _currentContainer
                End If
            End If


            ConfigureContainer(container, hisContainer, costContainer)

            Return _currentContainer
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Configura las dependencias en el contenedor
    ''' </summary>
    Private Shared Sub ConfigureContainer(container As String, hisContainer As String, costContainer As String)
        Dim newContainer = New UnityContainer()

        newContainer.RegisterType(Of ICommonVariables)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                 Return New CommonVariables(container, hisContainer)
                                                                                                             End Function))
        'Inyectamos el contexto
        newContainer.RegisterType(Of IGlobalModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                       Return New GlobalModelUnitOfWork(container)
                                                                                                                   End Function))

        newContainer.RegisterType(Of IInteropCostModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                            Return New InteropCostModelUnitOfWork(costContainer)
                                                                                                                        End Function))
        'Inyectamos el contexto de payroll
        newContainer.RegisterType(Of IPayrollUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                   Return New PayrollUnitOfWork(container)
                                                                                                               End Function))

        'Inyectamos el servicio WCF
        newContainer.RegisterType(Of IInteropCostService, InteropCostService)()
        'Secuencia

        'Servicios de dominio
        newContainer.RegisterType(Of IInteropCostServices, InteropCostServices)()

        newContainer.RegisterType(Of IInteropCostSequenceAdminService, InteropCostSequenceAdminService)()
        newContainer.RegisterType(Of IInteropCostSequenceRepository, InteropCostSequenceRepository)()
        newContainer.RegisterType(Of IInteropCostSequenceDetailRepository, InteropCostSequenceDetailRepository)()
        'Bloqueo de Registros
        newContainer.RegisterType(Of IInteropCostBlockRecordAdminService, InteropCostBlockRecordAdminService)()
        newContainer.RegisterType(Of IBlockRecordInteropCostRepository, BlockRecordInteropCostRepository)()
        'Estructura Organizacional
        newContainer.RegisterType(Of IOrganizationalStructureAdminService, OrganizationalStructureAdminService)()
        newContainer.RegisterType(Of IOrganizationalStructureRepository, OrganizationalStructureRepository)()
        'Centro de Producción
        newContainer.RegisterType(Of IProductionCenterAdminService, ProductionCenterAdminService)()
        newContainer.RegisterType(Of IProductionCenterRepository, ProductionCenterRepository)()
        'Gastos Generales
        newContainer.RegisterType(Of IGeneralExpenseAdminService, GeneralExpenseAdminService)()
        newContainer.RegisterType(Of IGeneralExpensesRepository, GeneralExpensesRepository)()
        'Gastos Directos
        newContainer.RegisterType(Of IDistributionDirectCostAdminService, DistributionDirectCostAdminService)()
        newContainer.RegisterType(Of IDistributionDirectCostRepository, DistributionDirectCostRepository)()
        'Parámetros de costos
        newContainer.RegisterType(Of IInteropCostSettingAdminService, InteropCostSettingAdminService)()
        newContainer.RegisterType(Of IInteropCostSettingRepository, InteropCostSettingRepository)()
        'Distribución de mano de obra
        newContainer.RegisterType(Of IDistributionManpowerAdminService, DistributionManpowerAdminService)()
        newContainer.RegisterType(Of IDistributionManpowerRepository, DistributionManpowerRepository)()
        'Distribución de activos fijos
        newContainer.RegisterType(Of IDistributionFixedAssetAdminService, DistributionFixedAssetAdminService)()
        newContainer.RegisterType(Of IDistributionFixedAssetRepository, DistributionFixedAssetRepository)()

        newContainer.RegisterType(Of IDirectDistributionSecondaryAdminService, DirectDistributionSecondaryAdminService)()
        newContainer.RegisterType(Of IDirectDistributionSecondaryRepository, DirectDistributionSecondaryRepository)()

        newContainer.RegisterType(Of IGeneralExpenseCategoryAdminService, GeneralExpenseCategoryAdminService)()
        newContainer.RegisterType(Of IGeneralExpenseCategoryRepository, GeneralExpenseCategoryRepository)()

        'Distribución Intermedia
        newContainer.RegisterType(Of IDistributionIntermediateAdminService, DistributionIntermediateAdminService)()
        newContainer.RegisterType(Of IDistributionIntermediateRepository, DistributionIntermediateRepository)()
        'Distribución Secondaria
        newContainer.RegisterType(Of IDistributionSecondaryAdminService, DistributionSecondaryAdminService)()
        newContainer.RegisterType(Of IDistributionSecondaryRepository, DistributionSecondaryRepository)()
        newContainer.RegisterType(Of Domain.Payroll.IEmployeeRepository, EmployeeRepository)()
        newContainer.RegisterType(Of IAFNDEPRECIRepository, AFNDEPRECIRepository)()
        newContainer.RegisterType(Of ICostEstimationAdminService, CostEstimationAdminService)()
        'newContainer.RegisterType(Of ICostEstimationRepository, CostEstimationRepository)()
        newContainer.RegisterType(Of IEstimateCostRepository, EstimateCostRepository)()

        newContainer.RegisterType(Of ICTNTIPCOMRepository, CTNTIPCOMRepository)()

        'DINAMICA
        'Areas de Servicio
        newContainer.RegisterType(Of IServiceAreaRepository, ServiceAreaRepository)()
        newContainer.RegisterType(Of IServiceAreaAdminService, ServiceAreaAdminService)()
        'Centros de Costo
        newContainer.RegisterType(Of ICostCenterRepository, Infrastructure.Data.InteropCostRepository.CostCenterRepository)()
        'Cuentas contables
        newContainer.RegisterType(Of IMainAccountRepository, Infrastructure.Data.InteropCostRepository.MainAccountRepository)()
        'Activos Fijos
        newContainer.RegisterType(Of IFixedAssetRepository, Infrastructure.Data.InteropCostRepository.FixedAssetRepository)()

        'Costos
        newContainer.RegisterType(Of ILogisticsProductionCenterRecordReporsitory, LogisticsProductionCenterRecordReporsitory)()
        newContainer.RegisterType(Of ILogisticsProductionCenterRecordAdminService, LogisticsProductionCenterRecordAdminService)()

        _currentContainer = newContainer

    End Sub

#End Region

End Class