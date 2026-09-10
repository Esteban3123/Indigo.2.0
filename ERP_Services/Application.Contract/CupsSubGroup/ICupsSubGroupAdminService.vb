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

Public Interface ICupsSubGroupAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un subgrupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveCupsSubGroup(ByVal CupsSubGroup As CupsSubgroup, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of CupsSubgroup)

    ''' <summary>
    ''' Elimina un subgrupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteCupsSubGroup(ByVal CupsSubGroup As CupsSubgroup, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un subgrupo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetCupsSubGroup(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of CupsSubgroup)

    ''' <summary>
    ''' Obtiene un subgrupo por id
    ''' </summary>
    ''' <returns></returns>
    Function GetCupsSubGroupById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of CupsSubgroup)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateCupsSubGroup(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of CupsSubgroup)

End Interface
