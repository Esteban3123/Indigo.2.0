'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/07/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities


Public Interface ICauseReprocessingRejectionAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveCauseReprocessingRejection(ByVal CauseReprocessingRejection As CauseReprocessingRejection, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of CauseReprocessingRejection)

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteCauseReprocessingRejection(ByVal CauseReprocessingRejection As CauseReprocessingRejection, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetCauseReprocessingRejection(ByVal code As String, ByVal audit As AuditMessage) As CauseReprocessingRejection

    ''' <summary>
    ''' Obtiene un grupo uvr por id
    ''' </summary>
    ''' <returns></returns>
    Function GetCauseReprocessingRejectionById(ByVal id As Integer) As CauseReprocessingRejection

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateCauseReprocessingRejection(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of CauseReprocessingRejection)

End Interface
