'***********************************************************************
' Assembly         : Application.Common
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 09-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base
Imports Domain.Base.Entities

Public Interface ITimeUnitAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista Todas las Unidades de Tiempo
    ''' </summary>
    ''' <returns>Unidades de Tiempo</returns>
    ''' <remarks></remarks>
    Function ListAllTimeUnit() As List(Of TimeUnit)

    ''' <summary>
    ''' Obtiene una Unidad de Tiempo
    ''' </summary>
    ''' <param name="code">Código de la Unidad de Tiempo</param>
    ''' <returns>Unidad de Tiempo</returns>
    ''' <remarks></remarks>
    Function GetTimeUnit(ByVal code As String) As TimeUnit

    ''' <summary>
    ''' Almacena o Actualiza Unidad de Tiempo
    ''' </summary>
    ''' <param name="timeUnit">Unidad de Tiempo</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveTimeUnit(ByVal timeUnit As TimeUnit, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Elimina una Unidad de Tiempo
    ''' </summary>
    ''' <param name="timeUnit">Unidad de Tiempo</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteTimeUnit(ByVal timeUnit As TimeUnit, ByVal audit As AuditMessage) As ActionMessageResult(Of TimeUnit)

End Interface
