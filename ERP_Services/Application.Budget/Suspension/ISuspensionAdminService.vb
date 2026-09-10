'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 18-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface ISuspensionAdminService
    Inherits IDisposable

    ''' <summary>
    ''' obtiene una suspencion de presupuesto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSuspensionByCode(code As String, audit As AuditMessage) As Suspension
    ''' <summary>
    ''' obtiene una suspencion de presupuesto por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSuspensionById(id As Integer) As Suspension
    ''' <summary>
    ''' Guarda una suspencion de presupuesto
    ''' </summary>
    ''' <param name="suspension"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveSuspension(suspension As Suspension, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Suspension)

End Interface
