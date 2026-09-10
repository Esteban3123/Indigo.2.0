#Region "Imports"

'Imports Domain.Entities

'Imports Infrastructure.Data.ModelRepository
Imports Microsoft.Practices.Unity
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Application.SelfService
Imports Application.Payroll
'Imports Infrastructure.CrossCutting.Base

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
            If _currentContainer Is Nothing Then
                _currentContainer = New UnityContainer()
                ConfigureContainer()
            End If
            Return _currentContainer
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Configura las dependencias en el contenedor
    ''' </summary>
    Private Shared Sub ConfigureContainer()
        'Inyectamos el contexto
        '_currentContainer.RegisterType(Of IGlobalModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
        '                                                                                                                    Return New GlobalModelUnitOfWork(ServerSessionValues.Current.CurrentContainer)
        '                                                                                                                End Function))
        'Inyectamos el servicio WCF
        _currentContainer.RegisterType(Of ISelfServiceService, SelfServiceService)()
        'Solicitud de vacaciones
        _currentContainer.RegisterType(Of ISelfServiceRequestVacationAdminService, SelfServiceRequestVacationAdminService)()

        'Registramos los servicios de nominja
        _currentContainer.RegisterType(Of IVacationPeriodAdminService, VacationPeriodAdminService)()
        '_currentContainer.RegisterType(Of IRetentionConceptRepository, RetentionConceptRepository)()
        ''Secuencias numericas
        '_currentContainer.RegisterType(Of IAccountingSequenseAdminService, AccountingSequenseAdminService)()
        '_currentContainer.RegisterType(Of ISequenseAccountingCRepository, SequenseAccountingCRepository)()
        '_currentContainer.RegisterType(Of ISequenseAccountingDRepository, SequenseAccountingDRepository)()
        ''Clase contable
        '_currentContainer.RegisterType(Of IAccountClassAdminService, AccountClassAdminService)()
        '_currentContainer.RegisterType(Of IAccountClassRepository, AccountClassRepository)()
        ''Niveles de cuentas
        '_currentContainer.RegisterType(Of IAccountLevelAdminService, AccountLevelAdminService)()
        '_currentContainer.RegisterType(Of IAccountLevelRepository, AccountLevelRepository)()
        ''Tipos de documentos
        '_currentContainer.RegisterType(Of IDocumentTypeAdminService, DocumentTypeAdminService)()
        '_currentContainer.RegisterType(Of IDocumentTypeRepository, DocumentTypeRepository)()
        ''Bloqueo de registros
        '_currentContainer.RegisterType(Of IBlockRecordAccountingAdminService, BlockRecordAccountingAdminService)()
        '_currentContainer.RegisterType(Of IBlockRecordAccountingRepository, BlockRecordAccountingRepository)()
        ''Participación patrimonial
        '_currentContainer.RegisterType(Of IPatrimonialPartAdminService, PatrimonialPartAdminService)()
        '_currentContainer.RegisterType(Of IPatrimonialPartRepository, PatrimonialPartRepository)()
        ''Anexos de declaración
        '_currentContainer.RegisterType(Of IStatementFolioAdminService, StatementFolioAdminService)()
        '_currentContainer.RegisterType(Of IStatementFolioRepository, StatementFolioRepository)()
        ''MainAccount
        '_currentContainer.RegisterType(Of IPUCAdminService, PUCAdminService)()
        '_currentContainer.RegisterType(Of IPUCRepository, PUCRepository)()
        ''Setting Account
        '_currentContainer.RegisterType(Of ISettingAccountAdminService, SettingAccountAdminService)()
        '_currentContainer.RegisterType(Of ISettingsAccountRepository, SettingAccountRepository)()
        ''******************* document accounting
        '_currentContainer.RegisterType(Of IAccountingDocumentRepository, DocumentAccountingRepository)()
        '_currentContainer.RegisterType(Of IAccountingDocumentAdminService, AccountingDocumentAdminService)()
        ''cierre de mes
        '_currentContainer.RegisterType(Of ICloseMonthRepository, CloseMonthRepository)()
        '_currentContainer.RegisterType(Of ICloseMonthAdminService, CloseMonthAdminService)()
        ''balance
        '_currentContainer.RegisterType(Of IAccountingBalanceRepository, AccountingBalanceRepository)()
        '_currentContainer.RegisterType(Of IAccountingBalanceAdminService, AccountingBalanceAdminService)()
        ''company settings
        '_currentContainer.RegisterType(Of ICompanySettingsAdminService, CompanySettingsAdminService)()
        '_currentContainer.RegisterType(Of ICompanySettingsRepository, CompanySettingsRepository)()
    End Sub

#End Region

End Class