'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/03/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IPaymentsAccountPayableRejectionReason

    ''' <summary>
    ''' Guarda o Actualiza un rechazo de factura
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveAccountPayableRejectionReason(AccountPayableRejectionReason As Domain.Entities.AccountPayableRejectionReason, audit As AuditMessage, idSequense As Int64) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayableRejectionReason)

    ''' <summary>
    ''' Elimina un rechazo de factura
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteAccountPayableRejectionReason(AccountPayableRejectionReason As Domain.Entities.AccountPayableRejectionReason, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene un rechazo de factura
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAccountPayableRejectionReason(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayableRejectionReason)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateAccountPayableRejectionReason(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayableRejectionReason)

    ''' <summary>
    ''' Obtiene un rechazo de factura
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAccountPayableRejectionReasonById(id As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountPayableRejectionReason)

    ''' <summary>
    ''' Lista las razones de rechazo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListRejectionReason(audit As AuditMessage) As List(Of Domain.Entities.AccountPayableRejectionReason)

End Interface
