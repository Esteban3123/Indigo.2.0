'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 18-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Budget
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class BudgetService

    ''' <summary>
    ''' obtiene un compromiso codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetSuspensionByCode(code As String, audit As AuditMessage) As Domain.Entities.Suspension Implements IBudgetServiceSuspension.GetSuspensionByCode
        Using service As ISuspensionAdminService = Container.Current.Resolve(Of ISuspensionAdminService)()
            Return service.GetSuspensionByCode(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' obtiene un compromiso por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetSuspensionById(id As Integer) As Domain.Entities.Suspension Implements IBudgetServiceSuspension.GetSuspensionById
        Using service As ISuspensionAdminService = Container.Current.Resolve(Of ISuspensionAdminService)()
            Return service.GetSuspensionById(id)
        End Using
    End Function

    ''' <summary>
    ''' Guarda un compromiso
    ''' </summary>
    ''' <param name="suspension"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSuspension(suspension As Suspension, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Suspension) Implements IBudgetServiceSuspension.SaveSuspension
        Using service As ISuspensionAdminService = Container.Current.Resolve(Of ISuspensionAdminService)()
            Return service.SaveSuspension(suspension, audit, idSequense)
        End Using
    End Function

End Class