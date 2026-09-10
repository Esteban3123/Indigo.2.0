'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 27-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IWorkCenterAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista de Todos los Centros de Trabajo
    ''' </summary>
    ''' <returns>Listado de Centros de Trabajo</returns>
    ''' <remarks></remarks>
    Function ListAllWorkCenter() As List(Of WorkCenter)

    ''' <summary>
    ''' Obtiene un Centro de Trabajo en Específico
    ''' </summary>
    ''' <param name="code">Código del Centro de Trabajo</param>
    ''' <returns>Centro de Trabajo</returns>
    ''' <remarks></remarks>
    Function GetWorkCenter(ByVal code As String) As WorkCenter

    ''' <summary>
    ''' Almacena o Actualiza un Centro de Trabajo
    ''' </summary>
    ''' <param name="workCenter">Centro de Trabajo</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveWorkCenter(ByVal workCenter As WorkCenter, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Elimina un Centro de Trabajo
    ''' </summary>
    ''' <param name="workCenter">Centro de Trabajo</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteWorkCenter(ByVal workCenter As WorkCenter, ByVal audit As AuditMessage) As ActionMessageResult(Of WorkCenter)

End Interface
