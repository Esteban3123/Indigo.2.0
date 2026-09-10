#Region "Imports"

Imports System.ServiceModel
Imports Application.Authorization
Imports Application.Security
Imports DistributedServices.Authentication
Imports Domain.Crystal
Imports Domain.Entities
Imports Domain.Security
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Security
Imports Infrastructure.Data.CrystalRepository
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
        'Inyectamos el contexto de Crystal
        newContainer.RegisterType(Of ICrystalModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                        Return New CrystalModelUnitOfWork(hisContainer)
                                                                                                                    End Function))
        'Contexto de seguridad
        newContainer.RegisterType(Of ISeguridadUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                     Return New Infrastructure.Data.SecurityRepository.GenesisEntities()
                                                                                                                 End Function))

        'Inyectamos el servicio WCF
        newContainer.RegisterType(Of IAuthorizationService, AuthorizationService)()

        'Secuencias numericas
        newContainer.RegisterType(Of IAuthorizationSequenseAdminService, AuthorizationSequenseAdminService)()
        newContainer.RegisterType(Of ISequenceAuthorizationCRepository, SequenceAuthorizationCRepository)()
        newContainer.RegisterType(Of ISequenceAuthorizationDRepository, SequenceAuthorizationDRepository)()

        'Bloqueo
        newContainer.RegisterType(Of IBlockRecordAuthorizationAdminService, BlockRecordAuthorizationAdminService)()
        newContainer.RegisterType(Of IBlockRecordAuthorizationRepository, BlockRecordAuthorizationRepository)()

        'User
        newContainer.RegisterType(Of IUserAdminService, UserAdminService)()
        newContainer.RegisterType(Of IUserRepository, UserRepository)()

        'Seguridad
        newContainer.RegisterType(Of IUserMembershipRepository, IndigoAutentication)(New TransientLifetimeManager)
        newContainer.RegisterType(Of IFileUserRepository, FileUserRepository)(New TransientLifetimeManager)
        newContainer.RegisterType(Of IPermissionCompanyRepository, PermissionCompanyRepository)(New TransientLifetimeManager)

        'Grupos de autorización
        newContainer.RegisterType(Of IAuthorizationGroupAdminService, AuthorizationGroupAdminService)()
        newContainer.RegisterType(Of IAuthorizationGroupRepository, AuthorizationGroupRepository)()

        'Motivos de cancelación
        newContainer.RegisterType(Of ICancellationReasonsAdminService, CancellationReasonsAdminService)()
        newContainer.RegisterType(Of ICancellationReasonsRepository, CancellationReasonsRepository)()

        'Origen de autorización
        newContainer.RegisterType(Of IAuthorizationSourceAdminService, AuthorizationSourceAdminService)()
        newContainer.RegisterType(Of IAuthorizationSourceRepository, AuthorizationSourceRepository)()

        'Turnos de autorización
        newContainer.RegisterType(Of IAuthorizationScheduleTemplateAdminService, AuthorizationScheduleTemplateAdminService)()
        newContainer.RegisterType(Of IAuthorizationScheduleTemplateRepository, AuthorizationScheduleTemplateRepository)()

        'Portafolio de autorizaciones
        newContainer.RegisterType(Of IAuthorizationPortfolioAdminService, AuthorizationPortfolioAdminService)()
        newContainer.RegisterType(Of IAuthorizationPortfolioRepository, AuthorizationPortfolioRepository)()

        'Configuración de servicios ambulatorios
        newContainer.RegisterType(Of IConfigurationServicesAmbulatoryAdminService, ConfigurationServicesAmbulatoryAdminService)()
        newContainer.RegisterType(Of IConfigurationServicesAmbulatoryRepository, ConfigurationServicesAmbulatoryRepository)()

        'Asignación de turnos
        newContainer.RegisterType(Of IAuthorizationScheduleAdminService, AuthorizationScheduleAdminService)()
        newContainer.RegisterType(Of IAuthorizationScheduleRepository, AuthorizationScheduleRepository)()

        'Trazabalidad de tramites
        newContainer.RegisterType(Of ITraceabilityPaperworkAdminService, TraceabilityPaperworkAdminService)()
        newContainer.RegisterType(Of ITraceabilityPaperworkRepository, TraceabilityPaperworkRepository)()

        'Gestion de ordenes médicas
        newContainer.RegisterType(Of IManagementMedicalOrderAdminService, ManagementMedicalOrderAdminService)()
        newContainer.RegisterType(Of IManagementMedicalOrderRepository, ManagementMedicalOrderRepository)()

        'Dashboard cubrimiento contractual hospitalario
        newContainer.RegisterType(Of IDashboardContractCoverageAdminService, DashboardContractCoverageAdminService)()
        newContainer.RegisterType(Of IDashboardContractCoverageRepository, DashboardContractCoverageRepository)()

        'Parámetros de autorización
        newContainer.RegisterType(Of ISettingsAuthorizationAdminService, SettingsAuthorizationAdminService)()
        newContainer.RegisterType(Of ISettingsAuthorizationRepository, SettingsAuthorizationRepository)()

        'Autorización de servicios tercerizados
        newContainer.RegisterType(Of IAuthorizationOutsourcedServicesAdminService, AuthorizationOutsourcedServicesAdminService)()
        newContainer.RegisterType(Of IAuthorizationOutsourcedServicesRepository, AuthorizationOutsourcedServicesRepository)()

        'Rechazo de autorización
        newContainer.RegisterType(Of IAuthorizationRejectionAdminService, AuthorizationRejectionAdminService)()
        newContainer.RegisterType(Of IAuthorizationRejectionRepository, AuthorizationRejectionRepository)()

        'Motivos de postergacion
        newContainer.RegisterType(Of IPostponementReasonsAdminService, PostponementReasonsAdminService)()
        newContainer.RegisterType(Of IPostponementReasonsRepository, PostponementReasonsRepository)()

        'Reportes
        newContainer.RegisterType(Of IReportAdminService, ReportAdminService)()

        '***************************** COMMON '*****************************
        newContainer.RegisterType(Of Application.Common.IAttachmentAdminService, Application.Common.AttachmentAdminService)(New TransientLifetimeManager)
        newContainer.RegisterType(Of IAttachmentRepository, AttachmentRepository)(New TransientLifetimeManager)


        newContainer.RegisterType(Of ISEGrolesuRepository, SEGrolesuRepository)()
        newContainer.RegisterType(Of ISEGgruusuRepository, SEGgruusuRepository)()
        newContainer.RegisterType(Of ISEGusuaruRepository, SegusuaruRepository)()
        newContainer.RegisterType(Of IADCENATENRepository, ADCENATENRepository)()
        newContainer.RegisterType(Of IINUNIFUNCRepository, INUNIFUNCRepository)()
        newContainer.RegisterType(Of IINCUPSSUBRepository, INCUPSSUBRepository)()
        newContainer.RegisterType(Of IRateManualValidityDetailRepository, RateManualValidityDetailRepository)()

        _currentContainer = newContainer

    End Sub

#End Region

End Class