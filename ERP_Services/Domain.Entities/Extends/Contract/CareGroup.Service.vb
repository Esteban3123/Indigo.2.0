Imports System.Runtime.Serialization

Partial Public Class CareGroup

#Region "Properties"

    <DataMember>
    Public Property BillingConceptCopayCodeName As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del contrato
    ''' </summary>
    <DataMember()>
    Public Property ContractDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la plantilla de requerimientos
    ''' </summary>
    <DataMember()>
    Public Property RequirementTemplateDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la plantilla de productos
    ''' </summary>
    <DataMember()>
    Public Property ProductTemplateDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la plantilla de procedimientos
    ''' </summary>
    <DataMember()>
    Public Property ProcedureTemplateDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la plantilla de procedimientos
    ''' </summary>
    <DataMember()>
    Public Property AccountWithoutRadicateDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la plantilla de procedimientos
    ''' </summary>
    <DataMember()>
    Public Property AccountRadicateDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la plantilla de procedimientos
    ''' </summary>
    <DataMember()>
    Public Property AccountObjectionRemediedDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la plantilla de procedimientos
    ''' </summary>
    <DataMember()>
    Public Property AccountConciliationDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la plantilla de procedimientos
    ''' </summary>
    <DataMember()>
    Public Property AccountLegalCollectionDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la cuenta de dificil recaudo
    ''' </summary>
    <DataMember()>
    Public Property AccountHardCollectionDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la plantilla de procedimientos
    ''' </summary>
    <DataMember()>
    Public Property AccountDebtorOrderDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la plantilla de procedimientos
    ''' </summary>
    <DataMember()>
    Public Property AccountCreditorOrderDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la plantilla de procedimientos
    ''' </summary>
    <DataMember()>
    Public Property AccountRecoveryFeeDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la plantilla de procedimientos
    ''' </summary>
    <DataMember()>
    Public Property AccountParticularDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la plantilla de procedimientos
    ''' </summary>
    <DataMember()>
    Public Property GeneralGlossConceptNoteDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la plantilla de procedimientos
    ''' </summary>
    <DataMember()>
    Public Property DetailedGlossConceptNoteDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la plantilla de procedimientos
    ''' </summary>
    <DataMember()>
    Public Property PreviousLifetimesConceptNoteDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la plantilla de procedimientos
    ''' </summary>
    <DataMember()>
    Public Property RadicationJournalVoucherTypeDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la plantilla de procedimientos
    ''' </summary>
    <DataMember()>
    Public Property ReceptionObjectionJournalVoucherTypeDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la plantilla de procedimientos
    ''' </summary>
    <DataMember()>
    Public Property ConciliationJournalVoucherTypeDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la plantilla de procedimientos
    ''' </summary>
    <DataMember()>
    Public Property DevolutionJournalVoucherTypeDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del tipo de comprobantes
    ''' </summary>
    <DataMember()>
    Public Property TransferLegalJournalVoucherTypeDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del centro costo
    ''' </summary>
    <DataMember()>
    Public Property CostCenterDescription As String

    <DataMember()>
    Public Property ContractAccountingStructureDescription As String

    <DataMember()>
    Public Property TechnicalNoteDescription As String

    <DataMember()>
    Public Property NameEntityType As String

#Region "Budget Interface"

    <DataMember()>
    Public Property BudgetaryEntityId As Integer?

    <DataMember()>
    Public Property BudgetaryEntityDescription As String

    <DataMember()>
    Public Property BudgetaryValidityId As Integer?

    <DataMember()>
    Public Property BudgetaryValidityDescription As String

    <DataMember()>
    Public Property BillingBudgetDescription As String

    <DataMember()>
    Public Property PromissoryNoteBudgetDescription As String

    <DataMember()>
    Public Property AccountReceivablePreviousValidityBudgetDescription As String

    <DataMember()>
    Public Property PortfolioRecoveryBudgetDescription As String

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene la descripcion del Tipo de Grupo de Atencion
    ''' </summary>
    Public Function GetCareGroupTypeName() As String
        Dim careGroupTypeName As String = ""
        Select Case Me.CareGroupType
            Case 1 
                careGroupTypeName = "EAPB Con contrato"
            Case 2
                careGroupTypeName = "EAPB Sin Contrato"
            Case 3
                careGroupTypeName = "Particulares"
            Case 4
                careGroupTypeName = "Aseguradoras"
        End Select
        Return careGroupTypeName
    End Function

#End Region

End Class
