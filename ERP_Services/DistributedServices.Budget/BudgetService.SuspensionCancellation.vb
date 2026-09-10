'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 19-09-2015
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
    ''' obtiene un levantamiento de suspencion presupuestal por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetSuspensionCancellationByCode(code As String, audit As AuditMessage) As Domain.Entities.SuspensionCancellation Implements IBudgetServiceSuspensionCancellation.GetSuspensionCancellationByCode
        Using service As ISuspensionCancellationAdminService = Container.Current.Resolve(Of ISuspensionCancellationAdminService)()
            Return service.GetSuspensionCancellationByCode(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' obtiene un levantamiento de suspencion presupuestal por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetSuspensionCancellationById(id As Integer) As Domain.Entities.SuspensionCancellation Implements IBudgetServiceSuspensionCancellation.GetSuspensionCancellationById
        Using service As ISuspensionCancellationAdminService = Container.Current.Resolve(Of ISuspensionCancellationAdminService)()
            Return service.GetSuspensionCancellationById(id)
        End Using
    End Function

    ''' <summary>
    ''' Guarda un levantamiento de suspencion presupuestal
    ''' </summary>
    ''' <param name="suspensionCancellation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSuspensionCancellation(suspensionCancellation As SuspensionCancellation, idSequense As Int64, audit As AuditMessage) As ActionResult(Of SuspensionCancellation) Implements IBudgetServiceSuspensionCancellation.SaveSuspensionCancellation
        Using service As ISuspensionCancellationAdminService = Container.Current.Resolve(Of ISuspensionCancellationAdminService)()
            Return service.SaveSuspensionCancellation(suspensionCancellation, audit, idSequense)
        End Using
    End Function

End Class