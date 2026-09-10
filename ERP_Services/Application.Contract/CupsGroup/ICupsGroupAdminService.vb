'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICupsGroupAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveCupsGroup(ByVal CupsGroup As CupsGroup, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of CupsGroup)

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteCupsGroup(ByVal CupsGroup As CupsGroup, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetCupsGroup(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of CupsGroup)

    ''' <summary>
    ''' Obtiene un grupo por id
    ''' </summary>
    ''' <returns></returns>
    Function GetCupsGroupById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of CupsGroup)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateCupsGroup(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of CupsGroup)

End Interface
