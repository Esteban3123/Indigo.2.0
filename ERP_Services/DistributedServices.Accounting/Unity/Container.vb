#Region "Imports"

Imports Domain.Entities

Imports Infrastructure.Data.ModelRepository
Imports Microsoft.Practices.Unity
Imports Application.Accounting
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.PayrollRepository
Imports Domain.Payroll
Imports System.ServiceModel
Imports Infrastructure.Data.SecurityRepository
Imports Domain.Security
Imports Infrastructure.CrossCutting.Queue
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
            Dim container = If(String.IsNullOrEmpty(containerInfo?.container), SessionValues.Instance.TransactionalContainer, containerInfo?.container)
            Dim hisContainer = If(String.IsNullOrEmpty(containerInfo?.container), SessionValues.Instance.HisContainer, containerInfo?.container)

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

        'Contexto de seguridad
        newContainer.RegisterType(Of ISeguridadUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                     Return New GenesisEntities()
                                                                                                                 End Function))
        'confirmacion masiva
        newContainer.RegisterType(Of IAccountingMassiveConfirmAdminService, AccountingMassiveConfirmAdminService)()
        'Inyectamos el servicio WCF
        newContainer.RegisterType(Of IAccountingService, AccountingService)()
        'Conceptos de retención
        newContainer.RegisterType(Of IRetentionConceptAdminService, RetentionConceptAdminService)()
        newContainer.RegisterType(Of IRetentionConceptRepository, RetentionConceptRepository)()
        'Secuencias numericas
        newContainer.RegisterType(Of IAccountingSequenseAdminService, AccountingSequenseAdminService)()
        newContainer.RegisterType(Of ISequenseAccountingCRepository, SequenseAccountingCRepository)()
        newContainer.RegisterType(Of ISequenseAccountingDRepository, SequenseAccountingDRepository)()
        'Clase contable
        newContainer.RegisterType(Of IAccountClassAdminService, AccountClassAdminService)()
        newContainer.RegisterType(Of IAccountClassRepository, AccountClassRepository)()
        'Niveles de cuentas
        newContainer.RegisterType(Of IAccountLevelAdminService, AccountLevelAdminService)()
        newContainer.RegisterType(Of IAccountLevelRepository, AccountLevelRepository)()
        'Niveles de cuentas contables
        newContainer.RegisterType(Of IMainAccountLevelsAdminService, MainAccountLevelsAdminService)()
        newContainer.RegisterType(Of IMainAccountLevelsRepository, MainAccountLevelsRepository)()
        'Tipos de documentos
        newContainer.RegisterType(Of IDocumentTypeAdminService, DocumentTypeAdminService)()
        newContainer.RegisterType(Of IDocumentTypeRepository, DocumentTypeRepository)()
        'Bloqueo de registros
        newContainer.RegisterType(Of IBlockRecordAccountingAdminService, BlockRecordAccountingAdminService)()
        newContainer.RegisterType(Of IBlockRecordAccountingRepository, BlockRecordAccountingRepository)()
        'Participación patrimonial
        newContainer.RegisterType(Of IPatrimonialPartAdminService, PatrimonialPartAdminService)()
        newContainer.RegisterType(Of IPatrimonialPartRepository, PatrimonialPartRepository)()
        'Anexos de declaración
        newContainer.RegisterType(Of IStatementFolioAdminService, StatementFolioAdminService)()
        newContainer.RegisterType(Of IStatementFolioRepository, StatementFolioRepository)()
        'MainAccount
        newContainer.RegisterType(Of IPUCAdminService, PUCAdminService)()
        newContainer.RegisterType(Of IPUCRepository, PUCRepository)()
        'Setting Account
        newContainer.RegisterType(Of ISettingAccountAdminService, SettingAccountAdminService)()
        newContainer.RegisterType(Of ISettingsAccountRepository, SettingAccountRepository)()

        '******************* document accounting
        newContainer.RegisterType(Of IAccountingDocumentRepository, DocumentAccountingRepository)()
        newContainer.RegisterType(Of IAccountingDocumentAdminService, AccountingDocumentAdminService)()
        '******************* JournalVoucherDetail
        newContainer.RegisterType(Of IJournalVoucherDetailRepository, JournalVoucherDetailRepository)()
        newContainer.RegisterType(Of IJournalVoucherDetailAdminService, JournalVoucherDetailAdminService)()
        'cierre de mes
        newContainer.RegisterType(Of ICloseMonthRepository, CloseMonthRepository)()
        newContainer.RegisterType(Of ICloseMonthAdminService, CloseMonthAdminService)()
        'Replicación masiva
        newContainer.RegisterType(Of IMassiveReplicationRepository, MassiveReplicationRepository)()
        newContainer.RegisterType(Of IMassiveReplicationAdminService, MassiveReplicationAdminService)()
        'balance
        newContainer.RegisterType(Of IAccountingBalanceRepository, AccountingBalanceRepository)()
        newContainer.RegisterType(Of IAccountingBalanceAdminService, AccountingBalanceAdminService)()
        'company settings
        newContainer.RegisterType(Of ICompanySettingsAdminService, CompanySettingsAdminService)()
        newContainer.RegisterType(Of ICompanySettingsRepository, CompanySettingsRepository)()
        'GeneralLedgerIVA
        newContainer.RegisterType(Of IGeneralLedgerIVAAdminService, GeneralLedgerIVAAdminService)()
        newContainer.RegisterType(Of IGeneralLedgerIVARepository, GeneralLedgerIVARepository)()
        'HomologationAccount
        newContainer.RegisterType(Of IHomologationAccountAdminService, HomologationAccountAdminService)()
        newContainer.RegisterType(Of IHomologationAccountRepository, HomologationAccountRepository)()
        'VieBot
        newContainer.RegisterType(Of IVieBotAdminService, VieBotAdminService)()
        newContainer.RegisterType(Of IVieBotRepository, VieBotRepository)()

        newContainer.RegisterType(Of IThirdPartyRepository, ThirdPartyRepository)()
        '*********************************Payroll
        newContainer.RegisterType(Of IPayrollUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                   Return New PayrollUnitOfWork(container)
                                                                                                               End Function))
        newContainer.RegisterType(Of ICostCenterRepository, CostCenterRepository)()
        'Book
        newContainer.RegisterType(Of IBookAdminService, BookAdminService)()
        newContainer.RegisterType(Of IBookRepository, BookRepository)()

        'Exogena Format
        newContainer.RegisterType(Of IFormatosExogena, FormatosExogena)()
        newContainer.RegisterType(Of IExogenousFormatAdminService, ExogenousFormatAdminService)()
        newContainer.RegisterType(Of IExogenousFormatRepository, ExogenousFormatRepository)()

        'Parámetros Exógena
        newContainer.RegisterType(Of ISettingsExogenousInformationAdminService, SettingsExogenousInformationAdminService)()
        newContainer.RegisterType(Of ISettingsExogenousInformationRepository, SettingsExogenousInformationRepository)()

        'Parámetros SuperSalud
        newContainer.RegisterType(Of IHealthSuperParametersAdminService, HealthSuperParametersAdminService)()
        newContainer.RegisterType(Of IHealthSuperParametersRepository, HealthSuperParametersRepository)()

        newContainer.RegisterType(Of ISequenseAccountingDRepository, SequenseAccountingDRepository)()
        newContainer.RegisterType(Of ISequenseContractDRepository, SequenseContractDRepository)()

        'IndigoQueue
        newContainer.RegisterType(Of IContainersRepository, ContainersRepository)()
        newContainer.RegisterType(Of IFactoryQueue, FactoryQueue)()

        'Reports
        newContainer.RegisterType(Of IAccountingReportAdminService, AccountingReportAdminService)()
        newContainer.RegisterType(Of IRateManualValidityDetailRepository, RateManualValidityDetailRepository)()

        _currentContainer = newContainer

    End Sub

#End Region

End Class