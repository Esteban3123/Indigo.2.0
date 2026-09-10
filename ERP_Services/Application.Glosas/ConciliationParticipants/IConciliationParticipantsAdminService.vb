'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Juan Diego Diaz
' Created          : 23-05-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Interfaz del servicio de Participantes Conciliación. 
''' </summary>
''' <remarks></remarks>
Public Interface IConciliationParticipantsAdminService
    Inherits IDisposable
    ''' <summary>
    ''' Elimina un participante de conciliacion.
    ''' </summary>
    ''' <param name="ConciliacionParticipante">Objeto Participante Conciliación</param>
    ''' <returns>ActionResult</returns>
    Function DeleteConciliationParticipants(ByVal ConciliacionParticipante As ConciliationParticipants, ByVal audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' Guarda un participante de conciliación.
    ''' </summary>
    ''' <param name="ConciliacionParticipante">Objeto Participante Conciliación</param>
    ''' <returns>ActionResult</returns>
    Function SaveConciliationParticipants(ByVal ConciliacionParticipante As List(Of ConciliationParticipants), ByVal audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' Lista todas los participantes de conciliaciones.
    ''' </summary>
    ''' <returns>Lista de objetos de participantes de conciliación</returns>
    Function ListAllConciliationParticipants() As List(Of ConciliationParticipants)
    ''' <summary>
    ''' consulta un participante de conciliacion especifico.
    ''' </summary>
    ''' <param name="Id">El Id del participante de conciliacion</param>
    ''' <returns>Objeto Participante Conciliación</returns>
    Function GetConciliationParticipantsById(ByVal Id As String) As ConciliationParticipants
    ''' <summary>
    ''' consulta un participante de conciliacion especifico.
    ''' </summary>
    ''' <param name="Id">El Id de la cabecera de conciliación</param>
    ''' <returns>Objeto Participante Conciliación</returns>
    Function ListConciliationParticipantsByIdConciliationC(ByVal Id As String) As List(Of ConciliationParticipants)

End Interface
