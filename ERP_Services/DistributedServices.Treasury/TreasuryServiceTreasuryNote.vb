'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán lozano
' Created          : 10-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Treasury
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Class TreasuryService
    Implements ITreasuryServiceTreasuryNote

    ''' <summary>
    ''' Confirma una nota de tesoreria
    ''' </summary>
    ''' <param name="IdTreasuryNote"></param>
    ''' <returns></returns>
    Public Function ConfirmTreasuryNote(IdTreasuryNote As Integer, idSequence As Int64, audit As AuditMessage) As ActionResult(Of String) Implements ITreasuryServiceTreasuryNote.ConfirmTreasuryNote
        Using service As ITreasuryNoteAdminService = Container.Current.Resolve(Of ITreasuryNoteAdminService)()
            Return service.ConfirmTreasuryNote(IdTreasuryNote, audit, Nothing, idSequence)
        End Using
        'Return Me._treasuryNoteAdminService.ConfirmTreasuryNote(IdTreasuryNote, audit, Nothing, idSequence)
    End Function

    ''' <summary>
    ''' obtiene una nota de tesoreria por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetTreasuryNote(code As String, audit As AuditMessage) As ActionResult(Of TreasuryNote) Implements ITreasuryServiceTreasuryNote.GetTreasuryNote
        Using service As ITreasuryNoteAdminService = Container.Current.Resolve(Of ITreasuryNoteAdminService)()
            Return service.GetTreasuryNote(code, audit)
        End Using
        'Return Me._treasuryNoteAdminService.GetTreasuryNote(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene una nota de tesoreria por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetTreasuryNoteById(Id As Integer) As TreasuryNote Implements ITreasuryServiceTreasuryNote.GetTreasuryNoteById
        Using service As ITreasuryNoteAdminService = Container.Current.Resolve(Of ITreasuryNoteAdminService)()
            Return service.GetTreasuryNoteById(Id)
        End Using
        'Return Me._treasuryNoteAdminService.GetTreasuryNoteById(Id)
    End Function

    ''' <summary>
    ''' Guarda una nota de tesoreria
    ''' </summary>
    Public Function SaveTreasuryNote(treasuryNote As TreasuryNote, withConfirm As Boolean, audit As AuditMessage, idSequence As Int64) As ActionResult(Of TreasuryNote) Implements ITreasuryServiceTreasuryNote.SaveTreasuryNote
        Using service As ITreasuryNoteAdminService = Container.Current.Resolve(Of ITreasuryNoteAdminService)()
            Return service.SaveTreasuryNote(treasuryNote, audit, withConfirm, idSequence)
        End Using
        'Return Me._treasuryNoteAdminService.SaveTreasuryNote(treasuryNote, audit, withConfirm, idSequence)
    End Function

End Class