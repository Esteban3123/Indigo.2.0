'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceOutstandingChecks

    ''' <summary>
    ''' Saves the outstanding checks.
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveOutstandingChecks(outstandingChecks As OutstandingChecks, audit As AuditMessage) As ActionResult(Of OutstandingChecks)

    ''' <summary>
    ''' Deletes the outstanding checks.
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteOutstandingChecks(outstandingChecks As OutstandingChecks, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un cheque pendiente por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetOutstandingChecksById(ByVal Id As Integer) As OutstandingChecks

    ''' <summary>
    ''' Obtiene el primer cheque que esta en espera
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetFirstOutstandingChecks(ByVal IdCheckBook As Integer) As OutstandingChecks

    ''' <summary>
    ''' Lista todos los cheques pendientes por id de la chequera
    ''' </summary>
    ''' <param name="IdCheckBook">The identifier check book.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListOutstandingChecksByIdCheckBook(ByVal IdCheckBook As Integer) As List(Of OutstandingChecks)

End Interface
