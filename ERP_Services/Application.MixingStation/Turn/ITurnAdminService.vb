'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Yoe Andres cardenas
' Created          : 12-04-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface ITurnAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los turnos
    ''' </summary>
    ''' <returns>Lista de turnos</returns>
    Function ListAllTurn(ByVal audit As AuditMessage) As List(Of Turn)

    ''' <summary>
    ''' Guarda un turno
    ''' </summary>
    ''' <param name="turn">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function SaveTurn(ByVal turn As Turn, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of Turn)

    ''' <summary>
    ''' Elimina un turno
    ''' </summary>
    ''' <param name="turn">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function DeleteTurn(ByVal turn As Turn, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Actualiza un turno
    ''' </summary>
    ''' <param name="code">The identifier.</param>
    ''' <param name="state">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function UpdateStateTurn(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of Turn)

    ''' <summary>
    ''' Obtiene un turno por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The identifier.</param>
    ''' <returns></returns>
    Function GetTurn(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of Turn)

    ''' <summary>
    ''' Obtiene un turno por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function GetTurnById(id As Integer, ByVal audit As AuditMessage) As ActionResult(Of Turn)

End Interface