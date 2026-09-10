'***********************************************************************
' Assembly         : Application.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 01-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IReversalReasonAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene una razón de anulacóon por código
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetReversalReason(code As String, tracking As Boolean, ByVal audit As AuditMessage) As ActionResult(Of Domain.Entities.BillingReversalReason)

    ''' <summary>
    ''' Obtiene una razón ade anulación por Id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetReversalReasonById(id As Integer, tracking As Boolean) As Domain.Entities.BillingReversalReason

    ''' <summary>
    ''' Guarda una razón de anulación
    ''' </summary>
    ''' <param name="reversalReason">The reversal reason.</param>
    ''' <returns></returns>
    Function SaveReversalReason(reversalReason As BillingReversalReason, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of BillingReversalReason)

    ''' <summary>
    ''' Guarda una razón de anulación
    ''' </summary>
    ''' <param name="reversalReason">The reversal reason.</param>
    ''' <returns></returns>
    Function DeleteReversalReason(reversalReason As BillingReversalReason, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Updates the state reversal reason.
    ''' </summary>
    ''' <returns></returns>
    Function UpdateStateReversalReason(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of BillingReversalReason)

End Interface
