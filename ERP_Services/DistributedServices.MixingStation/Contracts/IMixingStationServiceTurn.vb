'***********************************************************************
' Assembly         : DistributedServices.MixinStation
' Author           : Yoe Andres Cardenas
' Created          : 12/04/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IMixingStationServiceTurn
    ''' <summary>
    ''' Lista todos los turnos
    ''' </summary>
    ''' <returns>Lista de turnos</returns>
    <OperationContract()>
    Function ListAllTurn(audit As AuditMessage) As List(Of Turn)

    ''' <summary>
    ''' Guarda un turno
    ''' </summary>
    ''' <param name="turn">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    <OperationContract()>
    Function SaveTurn(ByVal turn As Turn, idSequence As Int64, audit As AuditMessage) As ActionResult(Of Turn)

    ''' <summary>
    ''' Elimina un turno
    ''' </summary>
    ''' <param name="turn">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    <OperationContract()>
    Function DeleteTurn(ByVal turn As Turn, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Actualiza un turno
    ''' </summary>
    ''' <param name="code">The identifier.</param>
    ''' <param name="state">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    <OperationContract()>
    Function UpdateStateTurn(ByVal code As String, ByVal state As Boolean, audit As AuditMessage) As ActionResult(Of Turn)

    ''' <summary>
    ''' Obtiene un turno por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetTurn(ByVal code As String, audit As AuditMessage) As ActionResult(Of Turn)

    ''' <summary>
    ''' Obtiene un turno por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    <OperationContract()>
    Function GetTurnById(id As Integer, audit As AuditMessage) As ActionResult(Of Turn)

End Interface
