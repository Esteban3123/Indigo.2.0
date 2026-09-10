'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 15/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ISurgicalGroupAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un grupo quirurgico
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveSurgicalGroup(ByVal SurgicalGroup As SurgicalGroup, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of SurgicalGroup)

    ''' <summary>
    ''' Elimina un grupo quirurgico
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteSurgicalGroup(ByVal SurgicalGroup As SurgicalGroup, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un grupo quirurgico por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetSurgicalGroup(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of SurgicalGroup)

    ''' <summary>
    ''' Obtiene un grupo quirurgico por id
    ''' </summary>
    ''' <returns></returns>
    Function GetSurgicalGroupById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of SurgicalGroup)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateSurgicalGroup(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of SurgicalGroup)

End Interface
