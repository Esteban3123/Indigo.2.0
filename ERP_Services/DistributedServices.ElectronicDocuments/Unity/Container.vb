Imports System.Configuration
Imports System.ServiceModel
Imports Application.ElectronicDocuments
Imports DistributedServices.Authentication
Imports Domain.Crystal
Imports Domain.Entities
Imports Domain.Security
Imports Infrastructure.CrossCutting.AzureBlobStorage.Factory
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.CrystalRepository
Imports Infrastructure.Data.ModelRepository
Imports Infrastructure.Data.SecurityRepository
Imports Microsoft.Practices.Unity

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
            Dim container As String = If(String.IsNullOrEmpty(containerInfo?.container), ServerSessionValues.Current.CurrentContainer, containerInfo?.container)
            Dim hisContainer As String = If(String.IsNullOrEmpty(containerInfo?.hisContainer), ServerSessionValues.Current.CurrentHISContainer, containerInfo?.hisContainer)
            Dim securityContainer As String = If(String.IsNullOrEmpty(containerInfo?.securityContainer), SessionValues.Instance.SecurityContainer, containerInfo?.securityContainer)
            Dim appSettings = ConfigurationManager.AppSettings
            '"AzureBlobConnectionString"
            If Not String.IsNullOrEmpty(appSettings(ConfigurationFile.CONX_BLOB_STRING_AZ)) Then
                ServerSessionValues.Current.CurrentBlobConnectionString = appSettings(ConfigurationFile.CONX_BLOB_STRING_AZ)
            End If

            If Not String.IsNullOrEmpty(appSettings(ConfigurationFile.CONX_BLOB_CONTAINER_NAME_AZ)) Then
                ServerSessionValues.Current.BlobContainerName = appSettings(ConfigurationFile.CONX_BLOB_CONTAINER_NAME_AZ)
            End If
            Dim blobContainerName As String = ServerSessionValues.Current.BlobContainerName


            If _currentContainer IsNot Nothing Then
                Dim sessionVariables As ICommonVariables = _currentContainer.Resolve(Of ICommonVariables)()
                If sessionVariables.getContainer().Equals(container) Then
                    Return _currentContainer
                End If
            End If


            ConfigureContainer(container, hisContainer, securityContainer, blobContainerName)

            Return _currentContainer
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Configura las dependencias en el contenedor
    ''' </summary>
    Private Shared Sub ConfigureContainer(container As String, hisContainer As String, Optional securityContainer As String = Nothing, Optional blobContainerName As String = Nothing)
        Dim newContainer = New UnityContainer()

        newContainer.RegisterType(Of ICommonVariables)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                 Return New CommonVariables(container, hisContainer)
                                                                                                             End Function))
        'Inyectamos el contexto
        newContainer.RegisterType(Of IGlobalModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                       Return New GlobalModelUnitOfWork(container)
                                                                                                                   End Function))
        newContainer.RegisterType(Of Infrastructure.Data.PayrollRepository.IPayrollUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                                                         Return New Infrastructure.Data.PayrollRepository.PayrollUnitOfWork(container)
                                                                                                                                                     End Function))

        newContainer.RegisterType(Of ISeguridadUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                     Return New GenesisEntities(securityContainer)
                                                                                                                 End Function))

        newContainer.RegisterType(Of IFactoryStorage)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                Return New FactoryStorage(blobContainerName)
                                                                                                            End Function))

        newContainer.RegisterType(Of ICrystalModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                        Return New CrystalModelUnitOfWork(hisContainer)
                                                                                                                    End Function))
        '-------------------------- REPOSITORY --------------------------

        'Billing
        newContainer.RegisterType(Of IBillingAuthorizationRepository, BillingAuthorizationRepository)()
        newContainer.RegisterType(Of IBillingNoteRepository, BillingNoteRepository)()
        newContainer.RegisterType(Of IElectronicDocumentRepository, ElectronicDocumentRepository)()
        newContainer.RegisterType(Of IElectronicDocumentDetailRepository, ElectronicDocumentDetailRepository)()
        newContainer.RegisterType(Of IElectronicDocumentNotificationRepository, ElectronicDocumentNotificationRepository)
        newContainer.RegisterType(Of IElectronicSupportDocumentRepository, ElectronicSupportDocumentRepository)
        newContainer.RegisterType(Of IElectronicSupportDocumentDetailRepository, ElectronicSupportDocumentDetailRepository)()
        newContainer.RegisterType(Of IElectronicSupportDocumentAdjustmentNoteRepository, ElectronicSupportDocumentAdjustmentNoteRepository)()
        newContainer.RegisterType(Of IElectronicSupportDocumentAdjustmentNoteDetailRepository, ElectronicSupportDocumentAdjustmentNoteDetailRepository)()
        newContainer.RegisterType(Of IInvoiceRepository, InvoiceRepository)()
        newContainer.RegisterType(Of IRevenueControlDetailRepository, RevenueControlDetailRepository)()

        'Common
        newContainer.RegisterType(Of IThirdPartyRepository, ThirdPartyRepository)()
        newContainer.RegisterType(Of IOperatingUnitRepository, OperatingUnitRepository)()

        'GeneralLedger
        newContainer.RegisterType(Of ISettingsAccountRepository, SettingAccountRepository)()

        'Payroll
        newContainer.RegisterType(Of Domain.Payroll.IElectronicPayrollRepository, Infrastructure.Data.PayrollRepository.ElectronicPayrollRepository)()
        newContainer.RegisterType(Of Domain.Payroll.IElectronicPayrollDetailRepository, Infrastructure.Data.PayrollRepository.ElectronicPayrollDetailRepository)()
        newContainer.RegisterType(Of Domain.Payroll.IElectronicPayrollNotificationRepository, Infrastructure.Data.PayrollRepository.ElectronicPayrollNotificationRepository)
        newContainer.RegisterType(Of Domain.Payroll.IElectronicPayrollPaymentSupportRepository, Infrastructure.Data.PayrollRepository.ElectronicPayrollPaymentSupportRepository)
        newContainer.RegisterType(Of IRateManualValidityDetailRepository, RateManualValidityDetailRepository)()

        '-------------------------- APPLICATION --------------------------
        newContainer.RegisterType(Of IElectronicDocumentsAdminService, ElectronicDocumentsAdminService)
        newContainer.RegisterType(Of IElectronicPayrollAdminService, ElectronicPayrollAdminService)
        newContainer.RegisterType(Of IElectronicDocumentSupportAdminService, ElectronicDocumentSupportAdminService)
        newContainer.RegisterType(Of IElectronicsPropertiesRepository, ElectronicsPropertiesRepository)
        newContainer.RegisterType(Of IEndpointsRepository, EndPointsRepository)
        newContainer.RegisterType(Of IInvoiceCopayRepository, InvoiceCopayRepository)
        ' Discriminador saldo inicial en GetHealthSegmentFromInvoiceXml (notas tipo 6).
        newContainer.RegisterType(Of IInitialBalanceInvoiceRepository, InitialBalanceInvoiceRepository)()
        newContainer.RegisterType(Of IPatientRepository, PatientRepository)()

        _currentContainer = newContainer

    End Sub

#End Region

End Class
