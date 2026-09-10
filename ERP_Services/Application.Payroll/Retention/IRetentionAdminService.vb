'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 06-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IRetentionAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista Todas las Retenciones
    ''' </summary>
    ''' <returns>Retenciones</returns>
    ''' <remarks></remarks>
    Function ListAllRetention() As List(Of Retention)

    ''' <summary>
    ''' Elimina una Retención
    ''' </summary>
    ''' <param name="retention">Retención</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteRetention(ByVal retention As Retention, ByVal audit As AuditMessage) As ActionMessageResult(Of Retention)

    ''' <summary>
    ''' Almacena una Retención
    ''' </summary>
    ''' <param name="retention">Retención</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveRetention(ByVal retention As Retention, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Obtiene una Retención
    ''' </summary>
    ''' <param name="code">Código de la Retención</param>
    ''' <returns>Retención</returns>
    ''' <remarks></remarks>
    Function GetRetention(ByVal code As String) As Retention

End Interface
