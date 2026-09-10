'***********************************************************************
' Assembly         : DistributedService.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 12-04-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
Imports System.ServiceModel
#End Region

Partial Class MixingStationService
    Implements IMixingStationServiceTurn

    ''' <summary>
    ''' Actualiza un turno
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ListAllTurn(audit As AuditMessage) As List(Of Turn) Implements IMixingStationServiceTurn.ListAllTurn
        Using service As ITurnAdminService = Container.Current.Resolve(Of ITurnAdminService)()
            Return service.ListAllTurn(audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda un turno
    ''' </summary>
    ''' <param name="turn"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveTurn(turn As Turn, idSequence As Int64, audit As AuditMessage) As ActionResult(Of Turn) Implements IMixingStationServiceTurn.SaveTurn
        Using service As ITurnAdminService = Container.Current.Resolve(Of ITurnAdminService)()
            Return service.SaveTurn(turn, audit, idSequence)
        End Using
    End Function

    ''' <summary>
    ''' Elimina un turno
    ''' </summary>
    ''' <param name="turn"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeleteTurn(turn As Turn, audit As AuditMessage) As ActionResult Implements IMixingStationServiceTurn.DeleteTurn
        Using service As ITurnAdminService = Container.Current.Resolve(Of ITurnAdminService)()
            Return service.DeleteTurn(turn, audit)
        End Using
    End Function

    ''' <summary>
    ''' Actualiza el estado de un turno
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function UpdateStateTurn(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Turn) Implements IMixingStationServiceTurn.UpdateStateTurn
        Using service As ITurnAdminService = Container.Current.Resolve(Of ITurnAdminService)()
            Return service.UpdateStateTurn(code, state, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un turno por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetTurn(code As String, audit As AuditMessage) As ActionResult(Of Turn) Implements IMixingStationServiceTurn.GetTurn
        Using service As ITurnAdminService = Container.Current.Resolve(Of ITurnAdminService)()
            Return service.GetTurn(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un turno por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetTurnById(id As Integer, audit As AuditMessage) As ActionResult(Of Turn) Implements IMixingStationServiceTurn.GetTurnById
        Using service As ITurnAdminService = Container.Current.Resolve(Of ITurnAdminService)()
            Return service.GetTurnById(id, audit)
        End Using
    End Function
End Class
