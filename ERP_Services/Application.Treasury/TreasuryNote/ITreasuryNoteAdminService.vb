'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 10-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ITreasuryNoteAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda una nota de tesoreria
    ''' </summary>
    Function SaveTreasuryNote(ByVal treasuryNote As TreasuryNote, ByVal audit As AuditMessage, ByVal withConfirm As Boolean, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of TreasuryNote)

    ''' <summary>
    ''' Confirma una nota de tesoreria
    ''' </summary>
    Function ConfirmTreasuryNote(ByVal IdTreasuryNote As Integer, ByVal audit As AuditMessage, Optional treasuryNote As TreasuryNote = Nothing, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of String)

    ''' <summary>
    ''' obtiene una nota de tesoreria por codigo
    ''' </summary>
    Function GetTreasuryNote(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of TreasuryNote)

    ''' <summary>
    ''' Obtiene una nota de tesoreria por id
    ''' </summary>
    Function GetTreasuryNoteById(ByVal Id As Integer) As TreasuryNote

End Interface