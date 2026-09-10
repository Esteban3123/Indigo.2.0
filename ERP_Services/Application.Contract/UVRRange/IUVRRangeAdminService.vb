'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IUVRRangeAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un rango uvr
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveUVRRange(ByVal UVRRange As UVRRange, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of UVRRange)

    ''' <summary>
    ''' Elimina un rango uvr
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteUVRRange(ByVal UVRRange As UVRRange, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un rango uvr por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetUVRRange(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of UVRRange)

    ''' <summary>
    ''' Obtiene un rango uvr por id
    ''' </summary>
    ''' <returns></returns>
    Function GetUVRRangeById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of UVRRange)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateUVRRange(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of UVRRange)

End Interface
