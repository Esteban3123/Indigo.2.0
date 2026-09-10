'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/12/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface ITransportationAssistantAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveTransportationAssistant(ByVal TransportationAssistant As TransportationAssistant, ByVal audit As AuditMessage, operatingUnitId As Integer, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of TransportationAssistant)

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteTransportationAssistant(ByVal TransportationAssistant As TransportationAssistant, ByVal audit As AuditMessage, TransactionalContainer As String) As ActionResult

    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetTransportationAssistant(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of TransportationAssistant)

    ''' <summary>
    ''' Obtiene un grupo uvr por id
    ''' </summary>
    ''' <returns></returns>
    Function GetTransportationAssistantById(ByVal id As Integer) As ActionResult(Of TransportationAssistant)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateTransportationAssistant(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage, operatingUnitId As Integer) As ActionResult(Of TransportationAssistant)

End Interface