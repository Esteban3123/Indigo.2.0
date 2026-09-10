'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán lozano
' Created          : 03-04-2014
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

    ''' <summary>
    ''' Deletes the note concept.
    ''' </summary>
    ''' <param name="noteConcept">The note concept.</param>
    ''' <returns></returns>
    Public Function DeleteNoteConcept(noteConcept As NoteConcepts, audit As AuditMessage) As ActionResult Implements ITreasuryServiceNoteConcept.DeleteNoteConcept
        Using service As INoteConceptAdminService = Container.Current.Resolve(Of INoteConceptAdminService)()
            Return service.DeleteNoteConcept(noteConcept, audit)
        End Using
        'Return Me._noteConceptAdminService.DeleteNoteConcept(noteConcept, audit)
    End Function

    ''' <summary>
    ''' Obtiene un concepto de nota
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetNoteConcept(code As String, audit As AuditMessage) As ActionResult(Of NoteConcepts) Implements ITreasuryServiceNoteConcept.GetNoteConcept
        Using service As INoteConceptAdminService = Container.Current.Resolve(Of INoteConceptAdminService)()
            Return service.GetNoteConcept(code, audit)
        End Using
        'Return Me._noteConceptAdminService.GetNoteConcept(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene un concepto de nota por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetNoteConceptById(Id As Integer, audit As AuditMessage) As NoteConcepts Implements ITreasuryServiceNoteConcept.GetNoteConceptById
        Using service As INoteConceptAdminService = Container.Current.Resolve(Of INoteConceptAdminService)()
            Return service.GetNoteConceptById(Id, audit)
        End Using
        'Return Me._noteConceptAdminService.GetNoteConceptById(Id, audit)
    End Function

    ''' <summary>
    ''' Saves the note concept.
    ''' </summary>
    ''' <param name="noteConcept">The note concept.</param>
    ''' <returns></returns>
    Public Function SaveNoteConcept(noteConcept As NoteConcepts, audit As AuditMessage, idSequence As Int64) As ActionResult(Of NoteConcepts) Implements ITreasuryServiceNoteConcept.SaveNoteConcept
        Using service As INoteConceptAdminService = Container.Current.Resolve(Of INoteConceptAdminService)()
            Return service.SaveNoteConcept(noteConcept, audit, idSequence)
        End Using
        'Return Me._noteConceptAdminService.SaveNoteConcept(noteConcept, audit, idSequence)
    End Function

    ''' <summary>
    ''' Updates the state card.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function UpdateStateNoteConcept(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of NoteConcepts) Implements ITreasuryServiceNoteConcept.UpdateStateNoteConcept
        Using service As INoteConceptAdminService = Container.Current.Resolve(Of INoteConceptAdminService)()

            Return service.UpdateStateNoteConcept(code, state, audit)
        End Using
        'Return Me._noteConceptAdminService.UpdateStateNoteConcept(code, state, audit)
    End Function
    ''' <summary>
    ''' Funcion importar
    ''' </summary>
    ''' <param name="DataImport"></param>
    ''' <returns></returns>
    Function ValidateNoteConcept(DataImport As List(Of ImportFileRow)) As ActionResult(Of List(Of TreasuryNoteDetail)) Implements ITreasuryServiceNoteConcept.ValidateNoteConcept
        Using service As INoteConceptAdminService = Container.Current.Resolve(Of INoteConceptAdminService)()
            Return service.ValidateNoteConcept(DataImport)
        End Using
    End Function
    ''' <summary>
    ''' Funcion copiar ypegar
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <returns></returns>
    Function CopyPasteNoteConceptDetail(Data As List(Of List(Of String))) As ActionResult(Of List(Of TreasuryNoteDetail)) Implements ITreasuryServiceNoteConcept.CopyPasteNoteConceptDetail
        Using service As INoteConceptAdminService = Container.Current.Resolve(Of INoteConceptAdminService)()
            Return service.CopyPasteNoteConceptDetail(Data)
        End Using
    End Function

End Class
