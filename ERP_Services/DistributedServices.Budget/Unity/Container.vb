#Region "Imports"

Imports Domain.Entities

Imports Infrastructure.Data.ModelRepository
Imports Microsoft.Practices.Unity
Imports Application.Budget
Imports Infrastructure.CrossCutting.Base
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

        'detalle del compromiso

        newContainer.RegisterType(Of ICommitmentDetailRepository, CommitmentDetailRepository)()
        'Inyectamos el servicio WCF
        newContainer.RegisterType(Of IBudgetService, BudgetService)()
        'confirmar documentos
        newContainer.RegisterType(Of IConfirmationDocumentsAdminService, ConfirmationDocumentsAdminService)()
        'Secuencias numericas
        newContainer.RegisterType(Of IBudgetSequenseAdminService, BudgetSequenseAdminService)()
        newContainer.RegisterType(Of IBudgetSequenceRepository, BudgetSequenceRepository)()
        newContainer.RegisterType(Of ISequenseBudgetDRepository, SequenseBudgetDRepository)()
        'Conceptos
        newContainer.RegisterType(Of IBudgetConceptAdminService, BudgetConceptAdminService)()
        newContainer.RegisterType(Of IBudgetConceptRepository, BudgetConceptRepository)()
        'Bloqueo
        newContainer.RegisterType(Of IBlockRecordBudgetAdminService, BlockRecordBudgetAdminService)()
        newContainer.RegisterType(Of IBlockRecordBudgetRepository, BlockRecordBudgetRepository)()
        'Dependencias
        newContainer.RegisterType(Of IBudgetDependencyAdminService, BudgetDependencyAdminService)()
        newContainer.RegisterType(Of IBudgetDependencyRepository, BudgetDependencyRepository)()
        'Instiruciones
        newContainer.RegisterType(Of IBudgetInstitutionAdminService, BudgetInstitutionAdminService)()
        newContainer.RegisterType(Of IBudgetInstitutionRepository, BudgetInstitutionsRepository)()
        'Tipos de ingresos
        newContainer.RegisterType(Of IEarningsTypeAdminService, EarningsTypeAdminService)()
        newContainer.RegisterType(Of IEarningsTypeRepository, EarningsTypeRepository)()
        'Tipos de gasto
        newContainer.RegisterType(Of IExpenseTypeAdminService, ExpenseTypeAdminService)()
        newContainer.RegisterType(Of IExpenseTypeRepository, ExpenseTypeRepository)()
        'Financial
        newContainer.RegisterType(Of IFinancialSourceAdminService, FinancialSourceAdminService)()
        newContainer.RegisterType(Of IFinancialSourceRepository, FinancialSourceRepository)()
        'BudgetaryValidity
        newContainer.RegisterType(Of IValidityAdminService, ValidityAdminService)()
        newContainer.RegisterType(Of IValidityRepository, ValidityRepository)()
        'consecutivo
        newContainer.RegisterType(Of IConsecutiveBudgetRepository, ConsecutiveBudgetRepository)()

        newContainer.RegisterType(Of IBudgetItemAdminService, BudgetItemAdminService)()
        newContainer.RegisterType(Of IBudgetItemRepository, BudgetItemRepository)()

        newContainer.RegisterType(Of ISettingBudgetAdminService, SettingBudgetAdminService)()
        newContainer.RegisterType(Of ISettingBudgetRepository, SettingBudgetRepository)()
        'Level Category
        newContainer.RegisterType(Of ILevelCategoryAdminService, LevelCategoryAdminService)()
        newContainer.RegisterType(Of ILevelCategoryRepository, LevelCategoryRepository)()
        'Level Category
        newContainer.RegisterType(Of IBudgetEntryAdminService, BudgetEntryAdminService)()
        newContainer.RegisterType(Of IBudgetEntryRepository, BudgetEntryRepository)()
        'Presupuesto modificacion
        newContainer.RegisterType(Of IBudgetModificationAdminService, BudgetModificationAdminService)()
        newContainer.RegisterType(Of IBudgetModificationRepository, BudgetModificationRepository)()
        'Control
        newContainer.RegisterType(Of IBudgetControlRepository, BudgetControlRepository)()
        'BudgetHeader
        newContainer.RegisterType(Of IBudgetHeaderRepository, BudgetHeaderRepository)()
        'PAC inicial Detalle
        newContainer.RegisterType(Of IAnnualizedCashFlowAdminService, AnnualizedCashFlowAdminService)()
        newContainer.RegisterType(Of IAnnualizedCashFlowRepository, AnnualizedCashFlowRepository)()
        'pac modificacion
        newContainer.RegisterType(Of IAnnualizedCashFlowModificationAdminService, AnnualizedCashFlowModificationAdminService)()
        newContainer.RegisterType(Of IAnnualizedCashFlowModificationRepository, AnnualizedCashFlowModificationRepository)()
        'pac modificacion detalle
        newContainer.RegisterType(Of IAnnualizedCashFlowModificationDetailAdminService, AnnualizedCashFlowModificationDetailAdminService)()
        newContainer.RegisterType(Of IAnnualizedCashFlowModificationDetailRepository, AnnualizedCashFlowModificationDetailRepository)()
        'pac traslado
        newContainer.RegisterType(Of IAnnualizedCashFlowTransferAdminService, AnnualizedCashFlowTransferAdminService)()
        newContainer.RegisterType(Of IAnnualizedCashFlowTransferRepository, AnnualizedCashFlowTransferRepository)()
        'BudgetTransfer
        newContainer.RegisterType(Of IBudgetTransferAdminService, BudgetTransferAdminService)()
        newContainer.RegisterType(Of IBudgetTransferRepository, BudgetTransferRepository)()
        'BudgetTransferDetail
        newContainer.RegisterType(Of IBudgetTransferDetailAdminService, BudgetTransferDetailAdminService)()
        newContainer.RegisterType(Of IBudgetTransferDetailRepository, BudgetTransferDetailRepository)()
        'Recognition
        newContainer.RegisterType(Of IRecognitionAdminService, RecognitionAdminService)()
        newContainer.RegisterType(Of IRecognitionRepository, RecognitionRepository)()
        'reconocimiento detalle
        newContainer.RegisterType(Of IRecognitionDetailRepository, RecognitionDetailRepository)()
        'Recognition modification
        newContainer.RegisterType(Of IRecognitionModificationAdminService, RecognitionModificationAdminService)()
        newContainer.RegisterType(Of IRecognitionModificationRepository, RecognitionModificationRepository)()
        'recaudo
        newContainer.RegisterType(Of ICollectionAdminService, CollectionAdminService)()
        newContainer.RegisterType(Of ICollectionRepository, CollectionRepository)()
        'recaudo detalle
        newContainer.RegisterType(Of ICollectionDetailAdminService, CollectionDetailAdminService)()
        newContainer.RegisterType(Of ICollectionDetailRepository, CollectionDetailRepository)()
        'Obligacion
        newContainer.RegisterType(Of IObligationAdminService, ObligationAdminService)()
        newContainer.RegisterType(Of IObligationRepository, ObligationRepository)()
        'Obligacion detalle
        newContainer.RegisterType(Of IObligationDetailAdminService, ObligationDetailAdminService)()
        newContainer.RegisterType(Of IObligationDetailRepository, ObligationDetailRepository)()
        'modificacion recaudo
        newContainer.RegisterType(Of ICollectionModificationAdminService, CollectionModificationAdminService)()
        newContainer.RegisterType(Of ICollectionModificationRepository, CollectionModificationRepository)()
        'modificacion recaudo detalle
        newContainer.RegisterType(Of ICollectionModificationDetailAdminService, CollectionModificationDetailAdminService)()
        newContainer.RegisterType(Of ICollectionModificationDetailRepository, CollectionModificationDetailRepository)()
        'Disponibilidad
        newContainer.RegisterType(Of IAvailabilityAdminService, AvailabilityAdminService)()
        newContainer.RegisterType(Of IAvailabilityRepository, AvailabilityRepository)()
        'Presupuesto
        newContainer.RegisterType(Of IBudgetAdminService, BudgetAdminService)()
        newContainer.RegisterType(Of IBudgetRepository, BudgetRepository)()
        'Modificacion de disponibilidad
        newContainer.RegisterType(Of IAvailabilityModificationAdminService, AvailabilityModificationAdminService)()
        newContainer.RegisterType(Of IAvailabilityModificationRepository, AvailabilityModificationRepository)()
        'servicio de dominio de 
        newContainer.RegisterType(Of Domain.Entities.Service.IBudgetService, Domain.Entities.Service.BudgetService)()
        'compromiso
        newContainer.RegisterType(Of ICommitmentAdminService, CommitmentAdminService)()
        newContainer.RegisterType(Of ICommitmentRepository, CommitmentRepository)()
        'Modificacion compromiso
        newContainer.RegisterType(Of ICommitmentModificationAdminService, CommitmentModificationAdminService)()
        newContainer.RegisterType(Of ICommitmentModificationRepository, CommitmentModificationRepository)()
        'ObligationModification
        newContainer.RegisterType(Of IObligationModificationAdminService, ObligationModificationAdminService)()
        newContainer.RegisterType(Of IObligationModificationRepository, ObligationModificationRepository)()
        'ObligationModificationDetail
        newContainer.RegisterType(Of IObligationModificationDetailAdminService, ObligationModificationDetailAdminService)()
        newContainer.RegisterType(Of IObligationModificationDetailRepository, ObligationModificationDetailRepository)()
        'PaymentOrder
        newContainer.RegisterType(Of IPaymentOrderAdminService, PaymentOrderAdminService)()
        newContainer.RegisterType(Of IPaymentOrderRepository, PaymentOrderRepository)()
        'ReimbursementResource
        newContainer.RegisterType(Of IReimbursementResourceAdminService, ReimbursementResourceAdminService)()
        newContainer.RegisterType(Of IReimbursementResourceRepository, ReimbursementResourceRepository)()
        'AvailabilityDetail
        newContainer.RegisterType(Of IAvailabilityDetailRepository, AvailabilityDetailRepository)()
        'Suspension
        newContainer.RegisterType(Of ISuspensionAdminService, SuspensionAdminService)()
        newContainer.RegisterType(Of ISuspensionRepository, SuspensionRepository)()
        'SuspensionDetail
        newContainer.RegisterType(Of ISuspensionDetailRepository, SuspensionDetailRepository)()
        'SuspensionCancellation
        newContainer.RegisterType(Of ISuspensionCancellationAdminService, SuspensionCancellationAdminService)()
        newContainer.RegisterType(Of ISuspensionCancellationRepository, SuspensionCancellationRepository)()
        'CopyBase
        newContainer.RegisterType(Of ICopyBaseAdminService, CopyBaseAdminService)()
        'AvailabilityExtension
        newContainer.RegisterType(Of IAvailabilityExtensionAdminService, AvailabilityExtensionAdminService)()
        newContainer.RegisterType(Of IAvailabilityExtensionRepository, AvailabilityExtensionRepository)()
        'PrivateBudgetItemsStructure
        newContainer.RegisterType(Of IPrivateBudgetItemsStructureAdminService, PrivateBudgetItemsStructureAdminService)()
        newContainer.RegisterType(Of IPrivateBudgetItemsStructureRepository, PrivateBudgetItemsStructureRepository)()
        'PrivateBudget
        newContainer.RegisterType(Of IPrivateBudgetAdminService, PrivateBudgetAdminService)()
        newContainer.RegisterType(Of IPrivateBudgetRepository, PrivateBudgetRepository)()
        'PaymentOrderDetail
        newContainer.RegisterType(Of IPaymentOrderDetailRepository, PaymentOrderDetailRepository)()
        'CCPET
        newContainer.RegisterType(Of ICCPETAdminService, CCPETAdminService)()
        newContainer.RegisterType(Of ICCPETRepository, CCPETRepository)()
        'CPCCatalog
        newContainer.RegisterType(Of ICPCCatalogAdminService, CPCCatalogAdminService)()
        newContainer.RegisterType(Of ICPCCatalogRepository, CPCCatalogRepository)()
        'PublicPolicy
        newContainer.RegisterType(Of IPublicPolicyAdminService, PublicPolicyAdminService)()
        newContainer.RegisterType(Of IPublicPolicyRepository, PublicPolicyRepository)()

        'Exogena Format
        newContainer.RegisterType(Of IFormatosExogena, FormatosExogena)()

        'Reports
        newContainer.RegisterType(Of IReportAdminService, ReportAdminService)
        newContainer.RegisterType(Of IRateManualValidityDetailRepository, RateManualValidityDetailRepository)()

        _currentContainer = newContainer

    End Sub

#End Region

End Class