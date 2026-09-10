'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 10-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceTreasuryNote

    ''' <summary>
    ''' Guarda una nota de tesoreria
    ''' </summary>
    <OperationContract()>
    Function SaveTreasuryNote(treasuryNote As TreasuryNote, withConfirm As Boolean, audit As AuditMessage, idSequence As Int64) As ActionResult(Of TreasuryNote)

    ''' <summary>
    ''' Confirma una nota de tesoreria
    ''' </summary>
    <OperationContract()>
    Function ConfirmTreasuryNote(IdTreasuryNote As Integer, idSequence As Int64, audit As AuditMessage) As ActionResult(Of String)

    ''' <summary>
    ''' obtiene una nota de tesoreria por codigo
    ''' </summary>
    <OperationContract()>
    Function GetTreasuryNote(code As String, audit As AuditMessage) As ActionResult(Of TreasuryNote)

    ''' <summary>
    ''' Obtiene una nota de tesoreria por id
    ''' </summary>
    <OperationContract()>
    Function GetTreasuryNoteById(ByVal Id As Integer) As TreasuryNote

End Interface