#Region "Imports"

Imports Application.AccountManagement
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
Imports Infrastructure.CrossCutting.Queue

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
            Dim container = containerInfo?.container
            Dim hisContainer = containerInfo?.hisContainer

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

        Dim injector As New ModuleInjector()
        injector.Load(newContainer, GetType(Domain.Base.Inject))

        newContainer.RegisterType(Of ICommonVariables)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c) New CommonVariables(container, hisContainer)))
        newContainer.RegisterType(Of IGlobalModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c) New GlobalModelUnitOfWork(container)))
        newContainer.RegisterType(Of ISeguridadUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c) New GenesisEntities()))
        newContainer.RegisterType(Of ICrystalModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c) New CrystalModelUnitOfWork(hisContainer)))

        newContainer.RegisterType(Of IManagementAreasAdminService, ManagementAreasAdminService)()
        newContainer.RegisterType(Of IManagementAreasRepository, ManagementAreasRepository)()
        newContainer.RegisterType(Of IAccountManagementSequenceAdminService, AccountManagementSequenceAdminService)()
        newContainer.RegisterType(Of IAccountManagementSequenceRepository, AccountManagementSequenceRepository)()
        newContainer.RegisterType(Of IAccountManagementSequenceDetailRepository, AccountManagementSequenceDetailRepository)()
        newContainer.RegisterType(Of IBlockRecordAccountManagementAdminService, BlockRecordAccountManagementAdminService)()
        newContainer.RegisterType(Of IBlockRecordAccountManagementRepository, BlockRecordAccountManagementRepository)()
        newContainer.RegisterType(Of IUserAdminService, UserAdminService)()
        newContainer.RegisterType(Of IUserRepository, UserRepository)()
        newContainer.RegisterType(Of IFileUserRepository, FileUserRepository)()
        newContainer.RegisterType(Of IUserMembershipRepository, IndigoAutentication)()
        newContainer.RegisterType(Of IPermissionCompanyRepository, PermissionCompanyRepository)()
        newContainer.RegisterType(Of ISEGusuaruRepository, SegusuaruRepository)()
        newContainer.RegisterType(Of IADCENATENRepository, ADCENATENRepository)()
        newContainer.RegisterType(Of IINUNIFUNCRepository, INUNIFUNCRepository)()
        newContainer.RegisterType(Of IINCUPSSUBRepository, INCUPSSUBRepository)()
        newContainer.RegisterType(Of IAccountManagementParametersAdminService, AccountManagementParametersAdminService)()
        newContainer.RegisterType(Of IAccountManagementParametersRepository, AccountManagementParametersRepository)()
        newContainer.RegisterType(Of IRejectionReasonAdminService, RejectionReasonAdminService)()
        newContainer.RegisterType(Of IRejectionReasonRepository, RejectionReasonRepository)()
        newContainer.RegisterType(Of IUsersAssignmentRepository, UsersAssignmentRepository)()
        newContainer.RegisterType(Of IUserNoveltiesRepository, UserNoveltiesRepository)()
        newContainer.RegisterType(Of IFolioTransferRepository, FolioTransferRepository)()
        newContainer.RegisterType(Of IFolioTransferAdminService, FolioTransferAdminService)()
        newContainer.RegisterType(Of IDashboardAccountAssignmentAdminService, DashboardAccountAssignmentAdminService)()
        newContainer.RegisterType(Of IDashboardAccountAssignmentRepository, DashboardAccountAssignmentRepository)()
        newContainer.RegisterType(Of IFactoryQueue, FactoryQueue)()
        newContainer.RegisterType(Of IContainersRepository, ContainersRepository)()

        _currentContainer = newContainer
    End Sub

#End Region

End Class