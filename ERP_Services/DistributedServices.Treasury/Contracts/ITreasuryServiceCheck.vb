'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-06-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceCheck

    ''' <summary>
    ''' guarda una chequera
    ''' </summary>
    ''' <param name="check">The check.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveCheck(check As Checkbooks, audit As AuditMessage) As ActionResult(Of Checkbooks)

    ''' <summary>
    ''' Elimina una chequera
    ''' </summary>
    ''' <param name="checks">The checks.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteCheck(checks As Checkbooks, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una chequera por el id de la cuenta bancaria
    ''' </summary>
    ''' <param name="IdEntity">The identifier entity.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCheckByIdEntityBankAccountAndStatus(IdEntity As Integer, status As Short, audit As AuditMessage) As ActionResult(Of Checkbooks)

End Interface
