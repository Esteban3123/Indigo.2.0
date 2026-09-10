#Region "Imports"

Imports Application.Accounting
Imports Application.Common
Imports Application.Cost
Imports Application.Payments
Imports Application.Security
Imports DistributedServices.Authentication
Imports Domain.Crystal
Imports Domain.Entities
Imports Domain.Payroll
Imports Domain.Security
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Security
Imports Infrastructure.Data.CrystalRepository
Imports Infrastructure.Data.MaintenanceRepository
Imports Infrastructure.Data.ModelRepository
Imports Infrastructure.Data.PayrollRepository
Imports Infrastructure.Data.SecurityRepository
Imports Microsoft.Practices.Unity
Imports System.ServiceModel

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
            Dim container As String = If(String.IsNullOrEmpty(containerInfo?.container), SessionValues.Instance.TransactionalContainer, containerInfo?.container)
            Dim hisContainer As String = If(String.IsNullOrEmpty(containerInfo?.hisContainer), SessionValues.Instance.HisContainer, containerInfo?.hisContainer)

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
        newContainer.RegisterType(Of ISeguridadUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                     Return New Infrastructure.Data.SecurityRepository.GenesisEntities()
                                                                                                                 End Function))

        newContainer.RegisterType(Of IGlobalModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                       Return New GlobalModelUnitOfWork(container)
                                                                                                                   End Function))

        'Inyectamos el contexto de Crystal
        newContainer.RegisterType(Of ICrystalModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                        Return New CrystalModelUnitOfWork(hisContainer)
                                                                                                                    End Function))

        newContainer.RegisterType(Of IPaymentsMassiveConfirmAdminService, PaymentsMassiveConfirmAdminService)()

        'Inyectamos el servicio WCF
        newContainer.RegisterType(Of IPaymentsService, PaymentsService)()
        'Secuencias numericas
        newContainer.RegisterType(Of IPaymentsSequenseAdminService, PaymentsSequenseAdminService)()
        newContainer.RegisterType(Of ISequensePaymentsCRepository, SequensePaymentsCRepository)()
        newContainer.RegisterType(Of ISequensePaymentsDRepository, SequensePaymentsDRepository)()
        'Conceptos
        newContainer.RegisterType(Of IPaymentsConceptAdminService, PaymentsConceptAdminService)()
        newContainer.RegisterType(Of IPaymentsConceptRepository, PaymentsConceptRepository)()
        'Bloqueo
        newContainer.RegisterType(Of IBlockRecordPaymentsAdminService, BlockRecordPaymentsAdminService)()
        newContainer.RegisterType(Of IBlockRecordPaymentsRepository, BlockRecordPaymentsRepository)()
        'Conceptos
        newContainer.RegisterType(Of IPaymentsNoteConceptAdminService, PaymentsNoteConceptAdminService)()
        newContainer.RegisterType(Of IPaymentsNoteConceptRepository, PaymentsNoteConceptRepository)()
        'Cuenta por Pagar
        newContainer.RegisterType(Of IAccountPayableAdminService, AccountPayableAdminService)()
        newContainer.RegisterType(Of IAccountPayableRepository, AccountPayableRepository)()
        'Parametros de pagos
        newContainer.RegisterType(Of ISettingPaymentsAdminService, SettingPaymentsAdminService)()
        newContainer.RegisterType(Of ISettingPaymentsRepository, SettingPaymentsRepository)()
        'Saldos Iniciales
        newContainer.RegisterType(Of IOpeningBalanceAdminService, OpeningBalanceAdminService)()
        newContainer.RegisterType(Of IOpeningBalanceRepository, OpeningBalanceRepository)()
        'MoneyAdvance
        newContainer.RegisterType(Of IMoneyAdvanceAdminService, MoneyAdvanceAdminService)()
        newContainer.RegisterType(Of IMoneyAdvanceRepository, MoneyAdvanceRepository)()
        'PaymentsNotes
        newContainer.RegisterType(Of INotesDebitCreditAdminService, NotesDebitCreditAdminService)()
        newContainer.RegisterType(Of INotesDebitCreditRepository, NotesDebitCreditRepository)()
        newContainer.RegisterType(Of IGeneralLedgerIVARepository, GeneralLedgerIVARepository)()
        newContainer.RegisterType(Of Domain.Payroll.ICostCenterRepository, CostCenterRepository)()
        newContainer.RegisterType(Of Domain.Payroll.ICostDistributionsRepository, CostDistributionsRepository)()
        'Revalorización
        newContainer.RegisterType(Of IPaymentsRevaluationAdminService, PaymentsRevaluationAdminService)()
        newContainer.RegisterType(Of IPaymentsRevaluationRepository, PaymentsRevaluationRepository)()

        'DistributionDirectCost
        newContainer.RegisterType(Of ICostDistributionDirectCostAdminService, CostDistributionDirectCostAdminService)()
        newContainer.RegisterType(Of ICostDistributionDirectCostRepository, CostDistributionDirectCostRepository)()
        newContainer.RegisterType(Of ICostDistributionDirectCostDetailIvaRepository, CostDistributionDirectCostDetailIvaRepository)()

        'DeferredCausation
        newContainer.RegisterType(Of IDeferredCausationAdminService, DeferredCausationAdminService)()
        newContainer.RegisterType(Of IDeferredCausationRepository, DeferredCausationRepository)()
        'MovementAccountPayable
        newContainer.RegisterType(Of IMovementAccountPayableAdminService, MovementAccountPayableAdminService)()
        newContainer.RegisterType(Of IMovementAccountPayableRepository, MovementAccountPayableRepository)()
        'PaymentNotesAccountPayableAdvance
        newContainer.RegisterType(Of IPaymentNotesAccountPayableAdvanceAdminService, PaymentNotesAccountPayableAdvanceAdminService)()
        newContainer.RegisterType(Of IPaymentNotesAccountPayableAdvanceRepository, PaymentNotesAccountPayableAdvanceRepository)()
        'Transfer
        newContainer.RegisterType(Of ITransfersAdminService, TransfersAdminService)()
        newContainer.RegisterType(Of ITransfersRepository, TransfersRepository)()
        'AgesPayments
        newContainer.RegisterType(Of IAgesPaymentAdminService, AgesPaymentAdminService)()
        newContainer.RegisterType(Of IAgesPaymentsRepository, AgesPaymentsRepository)()
        'PaymentControl
        newContainer.RegisterType(Of IPaymentControlAdminService, PaymentControlAdminService)()
        newContainer.RegisterType(Of IPaymentControlRepository, PaymentControlRepository)()
        'MonthlyAmortization
        newContainer.RegisterType(Of IMonthlyAmortizationAdminService, MonthlyAmortizationAdminService)()
        newContainer.RegisterType(Of IMonthlyAmortizationRepository, MonthlyAmortizationRepository)()
        'DeferredCausationShare
        newContainer.RegisterType(Of IDeferredCausationShareAdminService, DeferredCausationShareAdminService)()
        newContainer.RegisterType(Of IDeferredCausationShareRepository, DeferredCausationShareRepository)()
        'FilingUnit
        newContainer.RegisterType(Of IFilingUnitAdminService, FilingUnitAdminService)()
        newContainer.RegisterType(Of IFilingUnitRepository, FilingUnitRepository)()
        'SupplierType
        newContainer.RegisterType(Of ISupplierTypeAdminService, SupplierTypeAdminService)()
        newContainer.RegisterType(Of ISupplierTypeRepository, SupplierTypeRepository)()
        'AccountPayableRejectionReason
        newContainer.RegisterType(Of IAccountPayableRejectionReasonAdminService, AccountPayableRejectionReasonAdminService)()
        newContainer.RegisterType(Of IAccountPayableRejectionReasonRepository, AccountPayableRejectionReasonRepository)()
        'AccountPayableTransfer
        newContainer.RegisterType(Of IAccountPayableTransferAdminService, AccountPayableTransferAdminService)()
        newContainer.RegisterType(Of IAccountPayableTransferRepository, AccountPayableTransferRepository)()
        'AccountPayableTransfer details
        newContainer.RegisterType(Of IAccountPayableTransferDetailRepository, AccountPayableTransferDetailRepository)()
        'balance
        newContainer.RegisterType(Of ILoadMassiveRepository, LoadMassiveRepository)()
        newContainer.RegisterType(Of ILoadMassiveAdminService, LoadMassiveAdminService)()
        'Documentos Soporte
        newContainer.RegisterType(Of IDocumentSupportAdminService, DocumentSupportAdminService)()
        newContainer.RegisterType(Of IDocumentSupportRepository, DocumentSupportRepository)()

        'Mantenimiento ny Payroll

        newContainer.RegisterType(Of Infrastructure.Data.MaintenanceRepository.IMaintenanceModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                                                                      Return New MaintenanceModelUnitOfWork(container)
                                                                                                                                                                  End Function))
        newContainer.RegisterType(Of Infrastructure.Data.PayrollRepository.IPayrollUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                                                         Return New PayrollUnitOfWork(container)
                                                                                                                                                     End Function))
        'newContainer.RegisterType(Of IMaintenanceUnitOfWork, GENESISEntitiesMaintenance)()
        'CostCenter
        newContainer.RegisterType(Of ICostCenterRepository, CostCenterRepository)()
        'DistributionLine
        newContainer.RegisterType(Of IDistributionLinesRepository, DistributionLinesRepository)()
        'SupplierDistributionLine
        newContainer.RegisterType(Of ISuppliersDistributionLinesRepository, SuppliersDistributionLinesRepository)()
        newContainer.RegisterType(Of ISupplierBankAccountRepository, SupplierBankAccountRepository)()
        'Supplier
        newContainer.RegisterType(Of Domain.Maintenance.ISupplierRepository, Infrastructure.Data.MaintenanceRepository.SupplierRepository)()


        '''''''''''''''''''''''''''''''''''''''''''''
        'supplier (Commons)
        newContainer.RegisterType(Of ISupplierAdminService, SupplierAdminService)()
        newContainer.RegisterType(Of ISupplierRepository, Infrastructure.Data.ModelRepository.SupplierRepository)()
        newContainer.RegisterType(Of Domain.Entities.IMaintenanceSequenceDetailRepository, Infrastructure.Data.ModelRepository.MaintenanceSequenceDetailRepository)(New TransientLifetimeManager)



        'Accounting
        'Inyectamos el servicio WCF
        'newContainer.RegisterType(Of IAccountingService, AccountingService)()
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
        'cierre de mes
        newContainer.RegisterType(Of ICloseMonthRepository, CloseMonthRepository)()
        newContainer.RegisterType(Of ICloseMonthAdminService, CloseMonthAdminService)()
        'balance
        newContainer.RegisterType(Of IAccountingBalanceRepository, AccountingBalanceRepository)()
        newContainer.RegisterType(Of IAccountingBalanceAdminService, AccountingBalanceAdminService)()
        'company settings
        newContainer.RegisterType(Of ICompanySettingsAdminService, CompanySettingsAdminService)()
        newContainer.RegisterType(Of ICompanySettingsRepository, CompanySettingsRepository)()
        newContainer.RegisterType(Of IThirdPartyRepository, ThirdPartyRepository)()
        'legalbook
        newContainer.RegisterType(Of IBookRepository, BookRepository)()

        'Exogena Format
        newContainer.RegisterType(Of IFormatosExogena, FormatosExogena)()

        'Reports
        newContainer.RegisterType(Of Application.Payments.IReportsAdminService, Application.Payments.ReportsAdminService)()

        'Security.User
        newContainer.RegisterType(Of IUserAdminService, UserAdminService)()
        newContainer.RegisterType(Of IUserRepository, UserRepository)()
        newContainer.RegisterType(Of IUserMembershipRepository, IndigoAutentication)(New TransientLifetimeManager)
        newContainer.RegisterType(Of IFileUserRepository, FileUserRepository)(New TransientLifetimeManager)
        newContainer.RegisterType(Of IPermissionCompanyRepository, PermissionCompanyRepository)(New TransientLifetimeManager)
        newContainer.RegisterType(Of ISEGusuaruRepository, SegusuaruRepository)()

        newContainer.RegisterType(Of IADCENATENRepository, ADCENATENRepository)()
        newContainer.RegisterType(Of IINUNIFUNCRepository, INUNIFUNCRepository)()
        newContainer.RegisterType(Of IINCUPSSUBRepository, INCUPSSUBRepository)()

        'Documento Soporte Electronico
        newContainer.RegisterType(Of Application.EventHandlers.IEventProxy, Application.EventHandlers.Proxies.AzureServiceBusProxy)()
        newContainer.RegisterType(Of IElectronicSupportDocumentRepository, ElectronicSupportDocumentRepository)()
        newContainer.RegisterType(Of IRateManualValidityDetailRepository, RateManualValidityDetailRepository)()

        newContainer.RegisterType(Of ICurrencyRepository, CurrencyRepository)()
        newContainer.RegisterType(Of IAccountReceivableRepository, AccountReceivableRepository)()

        _currentContainer = newContainer

    End Sub

#End Region

End Class