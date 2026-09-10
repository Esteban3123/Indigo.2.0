'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 24-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceCancellationCheck

    ''' <summary>
    ''' Saves the cancellation check.
    ''' </summary>
    ''' <param name="cancellationCheck">The cancellation check.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveCancellationCheck(cancellationCheck As CancellationChecks, audit As AuditMessage) As ActionResult(Of CancellationChecks)

    ''' <summary>
    ''' Obtener un registro de cheque cancelado
    ''' </summary>
    ''' <param name="IdEntityAccount">The identifier entity account.</param>
    ''' <param name="CheckNumber">The check number.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCancellationCheckByEntityAccountAndCheckNumber(IdEntityAccount As Integer, CheckNumber As String, audit As AuditMessage) As ActionResult(Of CancellationChecks)

    ''' <summary>
    ''' Obtener un registro de cheque cancelado por id de chequera y numero de cheque
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCancellationCheckByCheckBookIdAndCheckNumber(ByVal checkBookId As Integer, ByVal CheckNumber As Long) As CancellationChecks

    ''' <summary>
    ''' Obtener un registro de cheque cancelado por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCancellationCheckById(ByVal id As Integer) As CancellationChecks

End Interface
