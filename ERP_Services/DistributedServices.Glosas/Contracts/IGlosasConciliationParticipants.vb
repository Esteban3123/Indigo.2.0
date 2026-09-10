Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IGlosasConciliationParticipants

#Region "ConciliationParticipants"

    ''' <summary>
    ''' Elimina un participante de conciliacion.
    ''' </summary>
    ''' <param name="ConciliacionParticipante">Objeto Participante Conciliación</param>
    ''' <returns>ActionResult</returns>
    <OperationContract>
    Function DeleteConciliationParticipants(ByVal ConciliacionParticipante As ConciliationParticipants, ByVal session As SessionValues) As ActionResult
    ''' <summary>
    ''' Guarda un participante de conciliación.
    ''' </summary>
    ''' <param name="ConciliacionParticipante">Objeto Participante Conciliación</param>
    ''' <returns>ActionResult</returns>
    <OperationContract>
    Function SaveConciliationParticipants(ByVal ConciliacionParticipante As List(Of ConciliationParticipants), ByVal session As SessionValues) As ActionResult
    ''' <summary>
    ''' Lista todas los participantes de conciliaciones.
    ''' </summary>
    ''' <returns>Lista de objetos de participantes de conciliación</returns>
    <OperationContract>
    Function ListAllConciliationParticipants(ByVal session As SessionValues) As List(Of ConciliationParticipants)
    ''' <summary>
    ''' consulta un participante de conciliacion especifico.
    ''' </summary>
    ''' <param name="code">El código del participante de conciliacion</param>
    ''' <returns>Objeto Participante Conciliación</returns>
    <OperationContract>
    Function GetConciliationParticipantsById(ByVal code As String, ByVal session As SessionValues) As ConciliationParticipants
    ''' <summary>
    ''' consulta un participante de conciliacion especifico.
    ''' </summary>
    ''' <param name="code">El código de la cabecera de conciliación</param>
    ''' <returns>Objeto Participante Conciliación</returns>
    <OperationContract>
    Function ListConciliationParticipantsByIdConciliationC(ByVal code As String, ByVal session As SessionValues) As List(Of ConciliationParticipants)

#End Region

End Interface
