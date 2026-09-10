
#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IBillingServiceReversalReason

    ''' <summary>
    ''' Obtiene una razón de anulacóon por código
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetReversalReason(code As String, tracking As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingReversalReason)

    ''' <summary>
    ''' Obtiene una razón ade anulación por Id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetReversalReasonById(id As Integer, tracking As Boolean) As Domain.Entities.BillingReversalReason

    ''' <summary>
    ''' Guarda una razón de anulación
    ''' </summary>
    ''' <param name="reversalReason">The reversal reason.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveReversalReason(reversalReason As Domain.Entities.BillingReversalReason, idSequence As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingReversalReason)

    ''' <summary>
    ''' Guarda una razón de anulación
    ''' </summary>
    ''' <param name="reversalReason">The reversal reason.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteReversalReason(reversalReason As Domain.Entities.BillingReversalReason, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Updates the state reversal reason.
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function UpdateStateReversalReason(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingReversalReason)

End Interface
