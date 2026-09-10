'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Juan Diego Díaz
' Created          : 05-09-2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IFreeTimeUseAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Elimina una Actividad en Tiempo Libre
    ''' </summary>
    ''' <param name="freeTimeUse">Actividad en Tiempo Libre</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteFreeTimeUse(ByVal freeTimeUse As FreeTimeUse, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Almacena o Actualiza una Actividad en Tiempo Libre
    ''' </summary>
    ''' <param name="freeTimeUse">Actividad en Tiempo Libre</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveFreeTimeUse(ByVal freeTimeUse As FreeTimeUse, ByVal audit As AuditMessage, ByVal Optional idSequense As Long = 0) As ActionResult(Of FreeTimeUse)

    ''' <summary>
    ''' Obtiene una Actividad en Tiempo Libre
    ''' </summary>
    ''' <param name="code">Código de la Actividad en Tiempo Libre</param>
    ''' <returns>Actividad en Tiempo Libre</returns>
    ''' <remarks></remarks>
    Function GetFreeTimeUse(ByVal code As String, ByVal tracking As Boolean, ByVal audit As AuditMessage) As ActionResult(Of FreeTimeUse)

    ''' <summary>
    ''' Obtiene una Actividad en Tiempo Libre por ID
    ''' </summary>
    ''' <param name="ID">Id de la Actividad en Tiempo Libre</param>
    ''' <returns>Actividad en Tiempo Libre</returns>
    ''' <remarks></remarks>
    Function GetFreeTimeUseById(ByVal id As Integer, ByVal tracking As Boolean, ByVal audit As AuditMessage) As ActionResult(Of FreeTimeUse)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeState(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of FreeTimeUse)

End Interface
