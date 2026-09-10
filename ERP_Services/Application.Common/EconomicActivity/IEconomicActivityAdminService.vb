'***********************************************************************
' Assembly         : Application.Common
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/12/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IEconomicActivityAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza la actividad economica
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveEconomicActivity(ByVal EconomicActivity As EconomicActivity, ByVal audit As AuditMessage) As ActionResult(Of EconomicActivity)

    ''' <summary>
    ''' Elimina la actividad economica
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteEconomicActivity(ByVal EconomicActivity As EconomicActivity, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una actividad economica por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEconomicActivityById(id As Integer, ByVal audit As AuditMessage) As ActionResult(Of EconomicActivity)

    ''' <summary>
    ''' Obtiene una actividad economica por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEconomicActivity(code As String, ByVal audit As AuditMessage) As ActionResult(Of EconomicActivity)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateEconomicActivity(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of EconomicActivity)

End Interface
