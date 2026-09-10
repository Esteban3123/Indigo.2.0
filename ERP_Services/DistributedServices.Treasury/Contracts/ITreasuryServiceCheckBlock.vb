'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 27-06-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceCheckBlock

    ''' <summary>
    ''' Bloquea un cheque
    ''' </summary>
    ''' <param name="checkBlock">The check block.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveCheckBlock(checkBlock As CheckBlock, audit As AuditMessage) As ActionResult(Of CheckBlock)

    ''' <summary>
    ''' Elimina un cheque bloqueado
    ''' </summary>
    ''' <param name="checkBlock">The check block.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteCheckBlock(checkBlock As CheckBlock, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un cheque bloqueado por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCheckBlockById(Id As Integer, audit As AuditMessage) As CheckBlock

    ''' <summary>
    ''' Obtiene un cheque bloqueado por el id de la chequera y numero del cheque
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCheckBlockByIdCheckBookAndNumber(IdCheckBook As Integer, checkNumber As Long, audit As AuditMessage) As ActionResult(Of CheckBlock)

End Interface
