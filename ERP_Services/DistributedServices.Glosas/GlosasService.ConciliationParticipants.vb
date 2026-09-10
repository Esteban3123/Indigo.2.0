'***********************************************************************
' Assembly         : DistributedService.Glosas
' Author           : Juan Diego Diaz
' Created          : 23-05-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Domain.Entities

Imports Infrastructure.CrossCutting.IOC
Imports Application.Glosas
Imports Infrastructure.CrossCutting.Base

#End Region

Partial Class GlosasService

    ''' <summary>
    ''' Elimina un participante de conciliación.
    ''' </summary>
    ''' <param name="ConciliacionParticipante">Objeto Participante Conciliación</param>
    ''' <returns>ActionResult</returns>
    Public Function DeleteConciliationParticipants(ConciliacionParticipante As ConciliationParticipants, session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasConciliationParticipants.DeleteConciliationParticipants
        Using conciliationParticipants As IConciliationParticipantsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConciliationParticipantsAdminService)()
            Return conciliationParticipants.DeleteConciliationParticipants(ConciliacionParticipante, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtener un participante de conciliación especifico.
    ''' </summary>
    ''' <param name="Id">Id Participante Conciliación</param>
    ''' <returns>Objeto Participante Conciliación</returns>
    Public Function GetConciliationParticipantsById(Id As String, session As SessionValues) As ConciliationParticipants Implements IGlosasConciliationParticipants.GetConciliationParticipantsById
        Using conciliationParticipants As IConciliationParticipantsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConciliationParticipantsAdminService)()
            Return conciliationParticipants.GetConciliationParticipantsById(Id)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene todos los participantes de conciliación.
    ''' </summary>
    ''' <returns>Lista Participantes Conciliación</returns>
    Public Function ListAllConciliationParticipants(session As SessionValues) As List(Of ConciliationParticipants) Implements IGlosasConciliationParticipants.ListAllConciliationParticipants
        Using conciliationParticipants As IConciliationParticipantsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConciliationParticipantsAdminService)()
            Return conciliationParticipants.ListAllConciliationParticipants
        End Using
    End Function

    ''' <summary>
    ''' Obtener un participante de conciliación especifico.
    ''' </summary>
    ''' <param name="Id">Id Cabecera Conciliación</param>
    ''' <returns>Lista Participantes Conciliación</returns>
    Public Function ListConciliationParticipantsByIdConciliationC(Id As String, session As SessionValues) As List(Of ConciliationParticipants) Implements IGlosasConciliationParticipants.ListConciliationParticipantsByIdConciliationC
        Using conciliationParticipants As IConciliationParticipantsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConciliationParticipantsAdminService)()
            Return conciliationParticipants.ListConciliationParticipantsByIdConciliationC(Id)
        End Using
    End Function

    ''' <summary>
    ''' Guardar un participante de conciliación.
    ''' </summary>
    ''' <param name="ConciliacionParticipante">Objeto Participante Conciliación</param>
    ''' <returns>ActionResult</returns>
    Public Function SaveConciliationParticipants(ConciliacionParticipante As List(Of ConciliationParticipants), session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasConciliationParticipants.SaveConciliationParticipants
        Using conciliationParticipants As IConciliationParticipantsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConciliationParticipantsAdminService)()
            'Return conciliationParticipants.SaveConciliationParticipants(ConciliacionParticipante, audit)
        End Using
    End Function

End Class
