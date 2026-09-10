'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 22/05/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities


Public Interface IAuthorizationScheduleAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveAuthorizationSchedule(AuthorizationSchedule As AuthorizationSchedule, ListDays As List(Of Integer), audit As AuditMessage) As ActionResult(Of AuthorizationSchedule)

    ''' <summary>
    ''' Elimina 
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteAuthorizationSchedule(ByVal AuthorizationSchedule As AuthorizationSchedule, TransactionalContainer As String, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una entidad por id
    ''' </summary>
    ''' <returns></returns>
    Function GetAuthorizationScheduleById(ByVal id As Integer) As ActionResult(Of AuthorizationSchedule)

End Interface
