'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 07-10-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceConsignmentTransfer

    ''' <summary>
    ''' Obtiene un registro de consignacion o traslado por id
    ''' </summary>
    <OperationContract()>
    Function GetConsignmentTransferById(ByVal Id As Integer, Optional tracking As Boolean = False) As Consignment

    ''' <summary>
    ''' Obtiene un registro de consignacion o traslado por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetConsignmentTransfer(code As String, audit As AuditMessage, Optional tracking As Boolean = False) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Consignment)

    ''' <summary>
    ''' Guarda una consignacion / traslado
    ''' </summary>
    <OperationContract()>
    Function SaveConsignmentTransfer(consignmentTransfer As Domain.Entities.Consignment, withConfirm As Boolean, idSequence As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Consignment)

    ''' <summary>
    ''' confirma una consignacion / traslado
    ''' </summary>
    <OperationContract()>
    Function ConfirmConsignmentTransfer(consignmentTransferId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of String)

End Interface