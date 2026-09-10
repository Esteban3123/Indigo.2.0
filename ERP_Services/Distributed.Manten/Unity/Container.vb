#Region "Imports"

Imports System.ServiceModel
Imports Application.Maintenance
Imports Domain.Entities
Imports Domain.Security
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Queue
Imports Infrastructure.Data.ModelRepository
Imports Infrastructure.Data.SecurityRepository
Imports Microsoft.Practices.Unity

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
            Dim container As String = String.Empty
            Dim hisContainer As String = String.Empty

            If OperationContext.Current IsNot Nothing Then
                If OperationContext.Current.IncomingMessageHeaders.FirstOrDefault(Function(c) c.Name.Equals(ConfigurationFile.SESS_CONTAINER)) IsNot Nothing Then
                    container = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of String)(ConfigurationFile.SESS_CONTAINER, "ns")
                End If

                If OperationContext.Current.IncomingMessageHeaders.FirstOrDefault(Function(c) c.Name.Equals(ConfigurationFile.SESS_CONTAINER_HIS)) IsNot Nothing Then
                    hisContainer = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of String)(ConfigurationFile.SESS_CONTAINER_HIS, "ns")
                End If
            ElseIf SessionValues.Instance.TransactionalContainer <> "" Then
                container = SessionValues.Instance.TransactionalContainer
                hisContainer = SessionValues.Instance.HisContainer
            End If

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

        'Inyectamos el contexto de Billing
        newContainer.RegisterType(Of IGlobalModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                       Return New GlobalModelUnitOfWork(container)
                                                                                                                   End Function))

        newContainer.RegisterType(Of IMaintenanceSequenceDetailRepository, MaintenanceSequenceDetailRepository)()
        newContainer.RegisterType(Of IMaintenanceSequenceRepository, MaintenanceSequenceRepository)()
        newContainer.RegisterType(Of ISequensePortfolioCRepository, SequensePortfolioCRepository)()


        newContainer.RegisterType(Of IAccesoryDetailRepository, AccesoryDetailRepository)()
        newContainer.RegisterType(Of IAccesoryRepository, AccesoryRepository)()
        newContainer.RegisterType(Of IBlockRecordMaintenanceRepository, BlockRecordMaintenanceRepository)()
        newContainer.RegisterType(Of IBrandRepository, BrandRepository)()
        newContainer.RegisterType(Of IConsumableRepository, ConsumableRepository)()
        newContainer.RegisterType(Of IDrawingsDetailRepository, DrawingsDetailRepository)()
        newContainer.RegisterType(Of IEquipmentAllRepository, EquipmentAllRepository)()
        newContainer.RegisterType(Of IEquipmentFunctionRepository, EquipmentFunctionRepostory)()
        newContainer.RegisterType(Of IEquipmentHistoryRepository, EquipmentHistoryRepository)()
        newContainer.RegisterType(Of IEquipmentReceptionAccesoryRepository, EquipmentReceptionsAccesoryRepository)()
        newContainer.RegisterType(Of IEquipmentReceptionConsumableRepository, EquipmentReceptionConsumableRepository)()
        'newContainer.RegisterType(Of IEquipmentReceptionPACRepository, EquipmentReceptionPACRepository)()
        newContainer.RegisterType(Of IEquipmentRegistration, EquipmentRegistrationRepository)()
        newContainer.RegisterType(Of IEquipmentRequirementRepository, EquipmentRequirementRepository)()
        newContainer.RegisterType(Of IMaintenanceActivityRepository, MaintenanceActivityRepository)()
        newContainer.RegisterType(Of IMaintenanceParameterRepository, MaintenanceParametersRepository)()
        newContainer.RegisterType(Of IMaintenancePlanDetailRepository, MaintenancePlanDetailRepository)()
        newContainer.RegisterType(Of IMaintenancePlanRepository, MaintenancePlanRepository)()
        newContainer.RegisterType(Of IManualDetailRepository, ManualDetailRepository)()
        newContainer.RegisterType(Of IMeasurementUnitRepository, MeasurementUnitRepository)()
        newContainer.RegisterType(Of IPhisicalRiskRepository, PhysicalRiskRepository)()
        newContainer.RegisterType(Of ISupplierRepository, SupplierRepository)()
        newContainer.RegisterType(Of ITechnicalEquipmentSheetRepository, TechnicalEquipmentSheetRepository)()
        newContainer.RegisterType(Of ITechnicalLogDetailRepository, TechnicalLogDetailRepository)()
        newContainer.RegisterType(Of ITemplateEquipmentTypeRepository, TemplateEquipmentTypeRepository)()
        newContainer.RegisterType(Of ITechnicalLogRepository, TechnicalLogRepository)()

        newContainer.RegisterType(Of IAccesoryAdminService, AccesoryAdminService)()
        newContainer.RegisterType(Of IBlockRecordMaintenanceAdminService, BlockRecordMaintenanceAdminService)()
        newContainer.RegisterType(Of IBrandAdminService, BrandAdminService)()
        newContainer.RegisterType(Of IConsumableAdminService, ConsumibleAdminService)()
        newContainer.RegisterType(Of IEquipmentAdminService, EquipmentAdminService)()
        newContainer.RegisterType(Of IEquipmentFunctionAdminService, EquipmentFunctionAdminService)()
        newContainer.RegisterType(Of IEquipmentHistoryAdminService, EquipmentHistoryAdminService)()
        newContainer.RegisterType(Of IEquipmentRequirementAdminService, EquipmentRequirementAdminService)()
        newContainer.RegisterType(Of IMaintenanceParameterAdminService, MaintenanceParameterAdminService)()
        newContainer.RegisterType(Of IMaintenancePlanAdminService, MaintenancePlanAdminService)()
        newContainer.RegisterType(Of IMeasurementUnitAdminService, MeasurementUnitAdminService)()
        newContainer.RegisterType(Of IPhysicalRiskAdminService, PhysicalRiskAdminService)()
        newContainer.RegisterType(Of IMaintenanceSequenseAdminService, MaintenanceSequenseAdminService)()
        newContainer.RegisterType(Of ISupplierAdminService, SupplierAdminService)()
        newContainer.RegisterType(Of ITechnicalLogAdminService, TechnicalLogAdminService)()
        newContainer.RegisterType(Of ITemplateEquipmentTypeAdminService, TemplateEquipmentTypeAdminService)()

        newContainer.RegisterType(Of IMaintenanceProtocolAdminService, MaintenanceProtocolAdminService)()
        newContainer.RegisterType(Of IMaintenanceProtocolRepository, MaintenanceProtocolRepository)()

        newContainer.RegisterType(Of IEquipmentRegistrationAdminService, EquipmentRegistrationAdminService)()
        newContainer.RegisterType(Of IFixedAssetPhysicalAssetRepository, FixedAssetPhysicalAssetRepository)()

        newContainer.RegisterType(Of IThirdPartyRepository, ThirdPartyRepository)()
        newContainer.RegisterType(Of Application.Common.ISuppliersDistributionLinesAdminService, Application.Common.SuppliersDistributionLinesAdminService)()
        newContainer.RegisterType(Of ISuppliersDistributionLinesRepository, SuppliersDistributionLinesRepository)()
        newContainer.RegisterType(Of ISequensePaymentsDRepository, SequensePaymentsDRepository)()
        newContainer.RegisterType(Of ISuppliersDetailTypeRepository, SuppliersDetailTypeRepository)()

        newContainer.RegisterType(Of ISupplierBankAccountRepository, SupplierBankAccountRepository)()

        newContainer.RegisterType(Of IMaintenancePlanAndMetrologyRepository, MaintenancePlanAndMetrologyRepository)()
        newContainer.RegisterType(Of IMaintenancePlanAndMetrologyAdminService, MaintenancePlanAndMetrologyAdminService)()

        newContainer.RegisterType(Of IWorkOrderAdminService, WorkOrderAdminService)()
        newContainer.RegisterType(Of IWorkOrderRepository, WorkOrderRepository)()

        'Responsables
        newContainer.RegisterType(Of IMaintenanceResponsibleAdminService, MaintenanceResponsibleAdminService)()
        newContainer.RegisterType(Of IMaintenanceResponsibleRepository, MaintenanceResponsibleRepository)()

        'Solicitud de Mantenimiento
        newContainer.RegisterType(Of IMaintenanceFailureRequestAdminService, MaintenanceFailureRequestAdminService)()
        newContainer.RegisterType(Of IMaintenanceFailureRequestRepository, MaintenanceFailureRequestRepository)()

        'Contrato de mantenimiento
        newContainer.RegisterType(Of IMaintenanceContractAdminService, MaintenanceContractAdminService)()
        newContainer.RegisterType(Of IMaintenanceContractRepository, MaintenanceContractRepository)()

        'Contrato de Reportes
        newContainer.RegisterType(Of IReportAdminService, ReportAdminService)()

        newContainer.RegisterType(Of IMaintenancePlanProgramatedRepository, MaintenancePlanProgramatedRepository)()

        'Herramientas
        newContainer.RegisterType(Of IMaintenanceToolsAdminService, MaintenanceToolsAdminService)()
        newContainer.RegisterType(Of IMaintenanceToolsRepository, MaintenanceToolsRepository)()

        'Fabricantes
        newContainer.RegisterType(Of IMaintenanceManufacturersAdminService, MaintenanceManufacturersAdminService)()
        newContainer.RegisterType(Of IMaintenanceManufacturersRepository, MaintenanceManufacturersRepository)()

        'Notificaciones
        newContainer.RegisterType(Of IMaintenanceFailureRequestDetailNotificationRepository, MaintenanceFailureRequestDetailNotificationRepository)()
        newContainer.RegisterType(Of IWorkOrderNotificationRepository, WorkOrderNotificationRepository)()

        'Log
        newContainer.RegisterType(Of IMaintenanceLogRepository, MaintenanceLogRepository)()


        'FactoryQueue
        newContainer.RegisterType(Of IContainersRepository, ContainersRepository)(New TransientLifetimeManager)
        newContainer.RegisterType(Of ISeguridadUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                     Return New Infrastructure.Data.SecurityRepository.GenesisEntities()
                                                                                                                 End Function))
        newContainer.RegisterType(Of IFactoryQueue, FactoryQueue)()

        _currentContainer = newContainer
    End Sub

#End Region

End Class