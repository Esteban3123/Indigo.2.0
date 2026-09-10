'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 19-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface ISuspensionCancellationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' obtiene una suspencion de presupuesto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSuspensionCancellationByCode(code As String, audit As AuditMessage) As SuspensionCancellation
    ''' <summary>
    ''' obtiene una suspencion de presupuesto por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSuspensionCancellationById(id As Integer) As SuspensionCancellation
    ''' <summary>
    ''' Guarda una suspencion de presupuesto
    ''' </summary>
    ''' <param name="suspensionCancellation"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveSuspensionCancellation(suspensionCancellation As SuspensionCancellation, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of SuspensionCancellation)

End Interface
