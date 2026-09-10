'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 0-10-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IConsignmentTransferAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un registro de consignacion o traslado por id
    ''' </summary>
    Function GetConsignmentTransferById(ByVal Id As Integer, Optional tracking As Boolean = False) As Consignment

    ''' <summary>
    ''' Obtiene un registro de consignacion o traslado por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetConsignmentTransfer(ByVal code As String, ByVal audit As AuditMessage, Optional tracking As Boolean = False) As ActionResult(Of Consignment)

    ''' <summary>
    ''' Guarda una consignacion / traslado
    ''' </summary>
    Function SaveConsignmentTransfer(ByVal consignmentTransfer As Consignment, ByVal audit As AuditMessage, ByVal withConfirm As Boolean, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of Consignment)

    ''' <summary>
    ''' confirma una consignacion / traslado
    ''' </summary>
    Function ConfirmConsignmentTransfer(ByVal consignmentTransferId As Integer, ByVal audit As AuditMessage, Optional ByVal crossingAccount As Consignment = Nothing) As ActionResult(Of String)

    ''' <summary>
    ''' reversa una consignacion
    ''' </summary>
    ''' <param name="treasuryNote"></param>
    ''' <param name="audit"></param>    
    ''' <returns></returns>
    Function ReverseConsignment(treasuryNote As TreasuryNote, audit As AuditMessage) As ActionResult(Of String)

End Interface