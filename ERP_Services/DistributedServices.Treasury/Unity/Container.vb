#Region "Imports"

Imports System.ServiceModel
Imports Application.Accounting
Imports Application.Budget
Imports Application.Common
Imports Application.Payments
Imports Application.Portfolio
Imports Application.Security
Imports Application.Treasury
Imports DistributedServices.Authentication
Imports Domain.Crystal
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Domain.Security
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Security
Imports Infrastructure.Data.CrystalRepository
Imports Infrastructure.Data.MaintenanceRepository
Imports Infrastructure.Data.ModelRepository
Imports Infrastructure.Data.PayrollRepository
Imports Infrastructure.Data.SecurityRepository
Imports Microsoft.Extensions.Caching.Memory
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

        'Inyectamos el servicio WCF
        newContainer.RegisterType(Of ITreasuryService, TreasuryService)()

        'Servicios de Tesoreria
        newContainer.RegisterType(Of ITreasuryServices, TreasuryServices)()
        'Secuencias numericas
        newContainer.RegisterType(Of IBankRepository, Infrastructure.Data.ModelRepository.BankRepository)()

        newContainer.RegisterType(Of ITreasurySequenseAdminService, TreasurySequenseAdminService)()
        newContainer.RegisterType(Of ISequenseTreasuryCRepository, SequenseTreasuryCRepository)()
        newContainer.RegisterType(Of ISequenseTreasuryDRepository, SequenseTreasuryDRepository)()
        'Bloqueo
        newContainer.RegisterType(Of IBlockRecordTreasuryAdminService, BlockRecordTreasuryAdminService)()
        newContainer.RegisterType(Of IBlockRecordTreasuryRepository, BlockRecordTreasuryRepository)()
        'Tarjetas
        newContainer.RegisterType(Of ICardAdminService, CardAdminService)()
        newContainer.RegisterType(Of ICardRepository, CardRepository)()
        'Recaudo de Tarjetas
        newContainer.RegisterType(Of ICardCollectionsAdminService, CardCollectionsAdminService)()
        newContainer.RegisterType(Of ICardCollectionsRepository, CardCollectionsRepository)()
        'Cajas
        newContainer.RegisterType(Of ICashRegisterAdminService, CashRegisterAdminService)()
        newContainer.RegisterType(Of ICashRegisterRepository, CashRegisterRepository)()
        'Fondo de caja menor
        newContainer.RegisterType(Of IConstitutionCashSmallerAdminService, ConstitutionCashSmallerAdminService)()
        newContainer.RegisterType(Of IConstitutionCashSmallerRepository, ConstitutionCashSmallerRepository)()
        'Recibos de Cajas
        newContainer.RegisterType(Of ICashReceiptConceptAdminService, CashReceiptConceptAdminService)()
        newContainer.RegisterType(Of ICashReceiptConceptRepository, CashReceiptConceptRepository)()
        'Cuentas de entidades
        newContainer.RegisterType(Of IEntityBankAccountAdminService, EntityBankAccountAdminService)()
        newContainer.RegisterType(Of IEntityBankAccountRepository, EntityBankAccountRepository)()
        'Conceptos de gastos
        newContainer.RegisterType(Of IExpenseConceptAdminService, ExpenseConceptAdminService)()
        newContainer.RegisterType(Of IExpenseConceptRepository, ExpenseConceptRepository)()
        'Conceptos de notas
        newContainer.RegisterType(Of INoteConceptAdminService, NoteConceptAdminService)()
        newContainer.RegisterType(Of INoteConceptRepository, NoteConceptRepository)()
        'Usuarios responsables de recibos de caja
        newContainer.RegisterType(Of ICashRegisterUserAdminService, CashRegisterUserAdminService)()
        newContainer.RegisterType(Of ICashRegisterUserRepository, CashRegisterUserRepository)()
        'Conceptos de pagos
        newContainer.RegisterType(Of IPaymentConceptAdminService, PaymentConceptAdminService)()
        newContainer.RegisterType(Of IPaymentConceptRepository, PaymentConceptRepository)()
        'Chequeras
        newContainer.RegisterType(Of ICheckAdminService, CheckAdminService)()
        newContainer.RegisterType(Of ICheckRepository, CheckRepository)()
        'Cheques bloqueados
        newContainer.RegisterType(Of ICheckBlockAdminService, CheckBlockAdminService)()
        newContainer.RegisterType(Of ICheckBlockRepository, CheckBlockRepository)()
        'Factura de Egresos
        newContainer.RegisterType(Of IDischargeBillAdminService, DischargeBillAdminService)()
        newContainer.RegisterType(Of IDischargeBillRepository, DischargeBillRepository)()

        'Tipo Proveedor
        newContainer.RegisterType(Of ISupplierTypeRepository, SupplierTypeRepository)()

        newContainer.RegisterType(Of IPaymentNotesAccountPayableAdvanceRepository, PaymentNotesAccountPayableAdvanceRepository)()
        newContainer.RegisterType(Of IPaymentNotesAccountPayableAdvanceAdminService, PaymentNotesAccountPayableAdvanceAdminService)()


        'reclasificacion de documentos
        newContainer.RegisterType(Of IReclassificationRepository, ReclassificationRepository)()
        'Centro de costo
        newContainer.RegisterType(Of Domain.Payroll.ICostCenterRepository, Infrastructure.Data.PayrollRepository.CostCenterRepository)()



        '-------------------- Esto para identificar -----------------------------------------------------------------------
        newContainer.RegisterType(Of ISeguridadUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                     Return New GenesisEntities()
                                                                                                                 End Function))

        'Security.User
        newContainer.RegisterType(Of IUserAdminService, UserAdminService)()
        newContainer.RegisterType(Of IUserRepository, UserRepository)()
        newContainer.RegisterType(Of IEndpointsRepository, EndPointsRepository)()

        newContainer.RegisterType(Of IUserMembershipRepository, IndigoAutentication)(New TransientLifetimeManager)

        newContainer.RegisterType(Of IFileUserRepository, FileUserRepository)(New TransientLifetimeManager)

        newContainer.RegisterType(Of IPermissionCompanyRepository, PermissionCompanyRepository)(New TransientLifetimeManager)
        '-------------------------------------------------------------------------------------------------------------------








        'PROCESOS
        'Confirmacion masiva
        newContainer.RegisterType(Of ITreasuryMassiveConfirmAdminService, TreasuryMassiveConfirmAdminService)()

        'Cancelacion de cheques
        newContainer.RegisterType(Of ICancellationCheckAdminService, CancellationCheckAdminService)()
        newContainer.RegisterType(Of ICancellationCheckRepository, CancellationCheckRepository)()
        'Comprobante de egreso
        newContainer.RegisterType(Of IVoucherTransactionAdminService, VoucherTransactionAdminService)()
        newContainer.RegisterType(Of IVoucherTransactionRepository, VoucherTransactionRepository)()
        newContainer.RegisterType(Of IGeneralLedgerIVARepository, GeneralLedgerIVARepository)()
        newContainer.RegisterType(Of Domain.Payroll.ICostCenterRepository, CostCenterRepository)()
        newContainer.RegisterType(Of Domain.Payroll.ICostDistributionsRepository, CostDistributionsRepository)()

        'Recibos de caja
        newContainer.RegisterType(Of ICashReceiptsAdminService, CashReceiptsAdminService)()
        newContainer.RegisterType(Of ICashReceiptsRepository, CashReceiptsRepository)()

        'Avances de tesoreria
        newContainer.RegisterType(Of ITreasuryAdvanceRepository, TreasuryAdvanceRepository)()

        'Reembolsos
        newContainer.RegisterType(Of IRefundAdminService, RefundAdminService)()
        newContainer.RegisterType(Of IRefundRepository, RefundRepository)()

        'cheques pedientes
        newContainer.RegisterType(Of IOutstandingChecksAdminService, OutstandingChecksAdminService)()
        newContainer.RegisterType(Of IOutstandingChecksRepository, OutstandingChecksRepository)()

        'detalle de egresos
        newContainer.RegisterType(Of IVoucherTransactionDetailAdminService, VoucherTransactionDetailAdminService)()
        newContainer.RegisterType(Of IVoucherTransactionDetailRepository, VoucherTransactionDetailRepository)()

        'programacion de pagos
        newContainer.RegisterType(Of ISchedulePaymentAdminService, SchedulePaymentAdminService)()
        newContainer.RegisterType(Of ISchedulePaymentRepository, SchedulePaymentRepository)()
        'detalle de la programacion de pagos
        newContainer.RegisterType(Of ISchedulePaymentDetailRepository, SchedulePaymentDetailRepository)()
        newContainer.RegisterType(Of ISchedulePaymentBankAccountRepository, SchedulePaymentBankAccountRepository)()
        'dispersion de fondos
        newContainer.RegisterType(Of IDispersionFundAdminService, DispersionFundAdminService)()

        'cruce de cuentas
        newContainer.RegisterType(Of ICrossingAccountAdminService, CrossingAccountAdminService)()
        newContainer.RegisterType(Of ICrossingAccountRepository, CrossingAccountRepository)()

        'consignaciones / traslados
        newContainer.RegisterType(Of IConsignmentTransferAdminService, ConsignmentTransferAdminService)()
        newContainer.RegisterType(Of IConsignmentTransferRepository, ConsignmentTransferRepository)()

        'detalle cruce de cuentas CxP
        newContainer.RegisterType(Of ICrossingAccountDetailCxPAdminService, CrossingAccountDetailCxPAdminService)()
        newContainer.RegisterType(Of ICrossingAccountDetailCxPRepository, CrossingAccountDetailCxPRepository)()

        'detalle de curce de cuentas CxC
        newContainer.RegisterType(Of ICrossingAccountDetailCxCAdminService, CrossingAccountDetailCxCAdminService)()
        newContainer.RegisterType(Of ICrossingAccountDetailCxCRepository, CrossingAccountDetailCxCRepository)()

        'Cambio de cheques
        newContainer.RegisterType(Of ICashingAdminService, CashingAdminService)()
        newContainer.RegisterType(Of ICashingRepository, CashingRepository)()

        'Notas
        newContainer.RegisterType(Of ITreasuryNoteAdminService, TreasuryNoteAdminService)()
        newContainer.RegisterType(Of ITreasuryNoteRepository, TreasuryNoteRepository)()

        'Cargue de Extractos Bancarios
        newContainer.RegisterType(Of IUploadBankStatementsAdminService, UploadBankStatementsAdminService)()
        newContainer.RegisterType(Of IUploadBankStatementsRepository, UploadBankStatementsRepository)()

        newContainer.RegisterType(Of ICostDistributionDirectCostRepository, CostDistributionDirectCostRepository)()
        newContainer.RegisterType(Of ICostDistributionDirectCostDetailIvaRepository, CostDistributionDirectCostDetailIvaRepository)()

        'UTILIDADES
        'Parámetros
        newContainer.RegisterType(Of ISettingsTreasuryAdminService, SettingsTreasuryAdminService)()
        newContainer.RegisterType(Of ISettingsTreasuryRepository, SettingsTreasuryRepository)()
        newContainer.RegisterType(Of ISettingsBillingRepository, SettingsBillingRepository)()
        'Control de documentos de tesoreria
        newContainer.RegisterType(Of ITreasuryControlAdminService, TreasuryControlAdminService)()
        newContainer.RegisterType(Of ITreasuryControlRepository, TreasuryControlRepository)()
        newContainer.RegisterType(Of ICrossingAccountDetailOtherConceptsRepository, CrossingAccountDetailOtherConceptsRepository)()

        '
        'Payments
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
        'DeferredCausation
        newContainer.RegisterType(Of IDeferredCausationAdminService, DeferredCausationAdminService)()
        newContainer.RegisterType(Of IDeferredCausationRepository, DeferredCausationRepository)()
        'Movement
        newContainer.RegisterType(Of IMovementAccountPayableAdminService, MovementAccountPayableAdminService)()
        newContainer.RegisterType(Of IMovementAccountPayableRepository, MovementAccountPayableRepository)()
        'Transfer
        newContainer.RegisterType(Of ITransfersAdminService, TransfersAdminService)()
        newContainer.RegisterType(Of ITransfersRepository, TransfersRepository)()
        'PaymentControl
        newContainer.RegisterType(Of IPaymentControlAdminService, PaymentControlAdminService)()
        newContainer.RegisterType(Of IPaymentControlRepository, PaymentControlRepository)()
        'MonthlyAmortization
        newContainer.RegisterType(Of IMonthlyAmortizationAdminService, MonthlyAmortizationAdminService)()
        newContainer.RegisterType(Of IMonthlyAmortizationRepository, MonthlyAmortizationRepository)()
        'DeferredCausationShare
        newContainer.RegisterType(Of IDeferredCausationShareAdminService, DeferredCausationShareAdminService)()
        newContainer.RegisterType(Of IDeferredCausationShareRepository, DeferredCausationShareRepository)()

        'Accounting
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
        'Currency
        newContainer.RegisterType(Of ICurrencyRepository, CurrencyRepository)()

        '**************************************************************************'
        '********************************MAPEO MAINTENANCE*****************************'
        '**************************************************************************'

        'Mantenimiento ny Payroll

        newContainer.RegisterType(Of Infrastructure.Data.MaintenanceRepository.IMaintenanceModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                                                                      Return New MaintenanceModelUnitOfWork(container)
                                                                                                                                                                  End Function))
        newContainer.RegisterType(Of Infrastructure.Data.PayrollRepository.IPayrollUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                                                         Return New PayrollUnitOfWork(container)
                                                                                                                                                     End Function))

        'newContainer.RegisterType(Of Infrastructure.Data.ModelRepository.IGlosasUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
        '                                                                                                                                                   Return New GENESISEntitiesGlosas(ServerSessionValues.Current.CurrentContainer)
        '                                                                                                                                               End Function))



        'glosas
        newContainer.RegisterType(Of Domain.InterfaceERPGlosa.IInterfacePublicFOX, Domain.InterfaceERPGlosa.InterfacePublicFOX)(New InjectionConstructor(container))

        newContainer.RegisterType(Of Domain.InterfaceERPGlosa.IInterfaceFOX, Domain.InterfaceERPGlosa.InterfaceFOX)(New InjectionConstructor(container))

        newContainer.RegisterType(Of Domain.InterfaceERPGlosa.IInterfaceNET, Domain.InterfaceERPGlosa.InterfaceNET)(New InjectionConstructor(container))

        newContainer.RegisterType(Of Domain.InterfaceERPGlosa.IInterfacePublicNET, Domain.InterfaceERPGlosa.InterfacePublicNET)(New InjectionConstructor(container))

        newContainer.RegisterType(Of Domain.Entities.IConsecutiveRepository, ConsecutiveRepository)()

        newContainer.RegisterType(Of Domain.Entities.IPortfolioGlosadaRepository, Infrastructure.Data.ModelRepository.PortfolioGlosadaRepository)()

        newContainer.RegisterType(Of Domain.Entities.IPartialPaymentsCRepository, Infrastructure.Data.ModelRepository.PartialPaymentsCRepository)()

        newContainer.RegisterType(Of Domain.Entities.IPartialPaymentsDRepository, Infrastructure.Data.ModelRepository.PartialPaymentsDRepository)()
        newContainer.RegisterType(Of Domain.Entities.IPartialPaymentsMovementRepository, Infrastructure.Data.ModelRepository.PartialPaymentsMovementRepository)()
        newContainer.RegisterType(Of Domain.Entities.IMovementGlosaRepository, Infrastructure.Data.ModelRepository.MovementGlosaRepository)()
        newContainer.RegisterType(Of Domain.Entities.IInterfaceParametersRepository, Infrastructure.Data.ModelRepository.InterfacesParametersRepository)()



        newContainer.RegisterType(Of Application.Glosas.IPartialPaymentsCAdminService, Application.Glosas.PartialPaymentsCAdminService)()
        'newContainer.RegisterType(Of IMaintenanceUnitOfWork, GENESISEntitiesMaintenance)()
        'CostCenter
        newContainer.RegisterType(Of Domain.Payroll.ICostCenterRepository, Infrastructure.Data.PayrollRepository.CostCenterRepository)()
        'DistributionLine
        newContainer.RegisterType(Of IDistributionLinesRepository, DistributionLinesRepository)()
        'SupplierDistributionLine
        newContainer.RegisterType(Of ISuppliersDistributionLinesRepository, SuppliersDistributionLinesRepository)()
        newContainer.RegisterType(Of ISupplierBankAccountRepository, SupplierBankAccountRepository)()
        'Supplier
        newContainer.RegisterType(Of Domain.Maintenance.ISupplierRepository, Infrastructure.Data.MaintenanceRepository.SupplierRepository)()
        '''''''''''''''''''''''''''''''''''''''''''''



        newContainer.RegisterType(Of Application.Common.ISupplierAdminService, Application.Common.SupplierAdminService)()
        newContainer.RegisterType(Of ISupplierRepository, Infrastructure.Data.ModelRepository.SupplierRepository)()
        'sequence
        newContainer.RegisterType(Of Application.Common.IMaintenanceSequenseAdminService, Application.Common.MaintenanceSequenseAdminService)()
        newContainer.RegisterType(Of Domain.Entities.IMaintenanceSequenceRepository, Infrastructure.Data.ModelRepository.MaintenanceSequenceRepository)()
        newContainer.RegisterType(Of Domain.Entities.IMaintenanceSequenceDetailRepository, Infrastructure.Data.ModelRepository.MaintenanceSequenceDetailRepository)()


        '************************************************************************
        '*****************************Portfolio**********************************
        '************************************************************************
        'Inyectamos el servicio WCF
        'newContainer.RegisterType(Of IPortfolioService, PortfolioService)()
        'Secuencias numericas
        newContainer.RegisterType(Of IPortfolioSequenseAdminService, PortfolioSequenseAdminService)()
        newContainer.RegisterType(Of ISequensePortfolioCRepository, SequensePortfolioCRepository)()
        newContainer.RegisterType(Of ISequensePortfolioDRepository, SequensePortfolioDRepository)()
        'Indicadores economicos
        newContainer.RegisterType(Of IEconomicIndicatorAdminService, EconomicIndicatorAdminService)()
        newContainer.RegisterType(Of IEconomicIndicatorRepository, EconomicIndicatorRepository)()
        'Concepto de cartera
        newContainer.RegisterType(Of IAccountReceivableConceptAdminService, AccountReceivableConceptAdminService)()
        newContainer.RegisterType(Of IAccountReceivableConceptRepository, AccountReceivableConceptRepository)()
        'Rangos de provicion
        newContainer.RegisterType(Of IProvisionRangesAdminService, ProvisionRangesAdminService)()
        newContainer.RegisterType(Of IProvisionRangesRepository, ProvisionRangesRepository)()
        'Conceptos de nota
        newContainer.RegisterType(Of IPortfolioNoteConceptAdminService, PortfolioNoteConceptAdminService)()
        newContainer.RegisterType(Of IPortfolioNoteConceptRepository, PortfolioNoteConceptRepository)()
        'Bloqueo
        newContainer.RegisterType(Of IBlockRecordPortfolioAdminService, BlockRecordPortfolioAdminService)()
        newContainer.RegisterType(Of IBlockRecordPortfolioRepository, BlockRecordPortfolioRepository)()
        'Anticipo
        newContainer.RegisterType(Of IPortfolioAdvanceAdminService, PortfolioAdvanceAdminService)()
        newContainer.RegisterType(Of IPortfolioAdvanceRepository, PortfolioAdvanceRepository)()
        'cuotas
        newContainer.RegisterType(Of IAccountReceivableShareAdminService, AccountReceivableShareAdminService)()
        newContainer.RegisterType(Of IAccountReceivableShareRepository, AccountReceivableShareRepository)()
        'Edades de cartera
        newContainer.RegisterType(Of IAgePortfolioAdminService, AgePortfolioAdminService)()
        newContainer.RegisterType(Of IAgePortfolioRepository, AgePortfolioRepository)()
        'setting
        'setting
        newContainer.RegisterType(Of ISettingPortfolioAdminService, SettingPortfolioAdminService)()
        newContainer.RegisterType(Of ISettingPortfolioRepository, SettingPortfolioRepository)()

        newContainer.RegisterType(Of IAccountReceivableAccountingRepository, AccountReceivableAccountingRepository)()
        'facturas
        newContainer.RegisterType(Of IAccountReceivableAdminService, AccountReceivableAdminService)()
        newContainer.RegisterType(Of IAccountReceivableRepository, AccountReceivableRepository)()
        'Vista traslados y consignaciones
        newContainer.RegisterType(Of IVReportConsignmentTransferAdminService, VReportConsignmentTransferAdminService)()
        newContainer.RegisterType(Of IVReportConsignmentTransferRepository, VReportConsignmentTransferRepository)()
        ''saldos iniciales
        'newContainer.RegisterType(Of IPortfolioInitialBalanceAdminService, PortfolioInitialBalanceAdminService)()
        'newContainer.RegisterType(Of IPortfolioInitialBalanceRepository, PortfolioInitialBalanceRepository)()
        newContainer.RegisterType(Of IThirdPartyRepository, ThirdPartyRepository)()

        newContainer.RegisterType(Of IBudgetSequenceRepository, BudgetSequenceRepository)()
        newContainer.RegisterType(Of ISequenseBudgetDRepository, SequenseBudgetDRepository)()


        newContainer.RegisterType(Of IBudgetRepository, BudgetRepository)()
        newContainer.RegisterType(Of IRecognitionAdminService, RecognitionAdminService)()
        newContainer.RegisterType(Of IRecognitionRepository, RecognitionRepository)()

        newContainer.RegisterType(Of IBudgetItemRepository, BudgetItemRepository)()
        newContainer.RegisterType(Of IBudgetHeaderRepository, BudgetHeaderRepository)()

        newContainer.RegisterType(Of IBudgetService, BudgetService)()
        newContainer.RegisterType(Of IValidityRepository, ValidityRepository)()
        newContainer.RegisterType(Of IExpenseTypeRepository, ExpenseTypeRepository)()
        newContainer.RegisterType(Of IAvailabilityRepository, AvailabilityRepository)()
        newContainer.RegisterType(Of ICommitmentRepository, CommitmentRepository)()

        newContainer.RegisterType(Of IObligationRepository, ObligationRepository)()

        newContainer.RegisterType(Of ISuspensionDetailRepository, SuspensionDetailRepository)()
        newContainer.RegisterType(Of IAvailabilityDetailRepository, AvailabilityDetailRepository)()
        newContainer.RegisterType(Of ICollectionRepository, CollectionRepository)()

        'Exogena Format
        newContainer.RegisterType(Of IFormatosExogena, FormatosExogena)()

        newContainer.RegisterType(Of ICheckCashingControlAdminService, CheckCashingControlAdminService)()
        newContainer.RegisterType(Of ICheckCashingControlRepository, CheckCashingControlRepository)()

        newContainer.RegisterType(Of ICashFlowConceptAdminService, CashFlowConceptAdminService)()
        newContainer.RegisterType(Of ICashFlowConceptRepository, CashFlowConceptRepository)()

        newContainer.RegisterType(Of ICashFlowReclassificationAdminService, CashFlowReclassificationAdminService)()
        newContainer.RegisterType(Of ICashFlowReclassificationRepository, CashFlowReclassificationRepository)()

        'Reports
        newContainer.RegisterType(Of Application.Treasury.IReportAdminService, Application.Treasury.ReportAdminService)()

        'Conciliación bancaria
        newContainer.RegisterType(Of IBankReconciliationAdminService, BankReconciliationAdminService)()
        newContainer.RegisterType(Of IBankReconciliationRepository, BankReconciliationRepository)()

        'Conciliación bancaria automatica
        newContainer.RegisterType(Of IBankReconciliationAutomaticAdminService, BankReconciliationAutomaticAdminService)()
        newContainer.RegisterType(Of IBankReconciliationAutomaticRepository, BankReconciliationAutomaticRepository)()

        newContainer.RegisterType(Of IBankReconciliationAutomaticAssociationRepository, BankReconciliationAutomaticAssociationRepository)()
        newContainer.RegisterType(Of ISEGrolesuRepository, SEGrolesuRepository)()
        newContainer.RegisterType(Of ISEGgruusuRepository, SEGgruusuRepository)()
        newContainer.RegisterType(Of ISEGusuaruRepository, SegusuaruRepository)()

        newContainer.RegisterType(Of IADCENATENRepository, ADCENATENRepository)()
        newContainer.RegisterType(Of IINUNIFUNCRepository, INUNIFUNCRepository)()
        newContainer.RegisterType(Of IINCUPSSUBRepository, INCUPSSUBRepository)()

        'Conceptos de conciliación bancaria
        newContainer.RegisterType(Of IBankConciliationConceptsAdminService, BankConciliationConceptsAdminService)()
        newContainer.RegisterType(Of IBankConciliationConceptsRepository, BankConciliationConceptsRepository)()

        'Documento Soporte electronico
        newContainer.RegisterType(Of Application.EventHandlers.IEventProxy, Application.EventHandlers.Proxies.AzureServiceBusProxy)()
        newContainer.RegisterType(Of IElectronicSupportDocumentRepository, ElectronicSupportDocumentRepository)()
        'legalBook
        newContainer.RegisterType(Of IBookRepository, BookRepository)()
        newContainer.RegisterType(Of IRateManualValidityDetailRepository, RateManualValidityDetailRepository)()

        'Revalorización
        newContainer.RegisterType(Of ITreasuryRevaluationAdminService, TreasuryRevaluationAdminService)()
        newContainer.RegisterType(Of ITreasuryRevaluationRepository, TreasuryRevaluationRepository)()

        'Convenios de redencion de puntos
        newContainer.RegisterType(Of IAgreementsRedemptionPointsAdminService, AgreementsRedemptionPointsAdminService)()
        newContainer.RegisterType(Of IAgreementsRedemptionPointsRepository, AgreementsRedemptionPointsRepository)()
        newContainer.RegisterType(Of IDispersionFundAdminService, DispersionFundAdminService)()
        newContainer.RegisterType(Of ICurrencyAdminService, CurrencyAdminService)()
        newContainer.RegisterType(Of IMemoryCache, MemoryCache)(
            New ContainerControlledLifetimeManager(),  ' Singleton
            New InjectionFactory(Function(c) New MemoryCache(New MemoryCacheOptions()))
        )
        _currentContainer = newContainer

    End Sub

#End Region

End Class