'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 28-10-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceCashing

    ''' <summary>
    ''' Guarda un registro de cambio de cheque
    ''' </summary>
    <OperationContract()>
    Function SaveCashing(checkCashing As CheckCashing, idSequence As Int64, audit As AuditMessage) As ActionResult(Of CheckCashing)

    ''' <summary>
    ''' Elimina un registro de cambio de cheque
    ''' </summary>
    <OperationContract()>
    Function DeleteCashing(checkCashing As CheckCashing, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un registro de cambio de cheque por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCashing(code As String, audit As AuditMessage) As ActionResult(Of CheckCashing)

    ''' <summary>
    ''' Obtiene un registro de cambio de cheque por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCashingById(id As Integer, tracking As Boolean) As CheckCashing

End Interface