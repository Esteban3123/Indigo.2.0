'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 29/01/2018
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

Partial Class BudgetService

    ''' <summary>
    ''' Guarda el registro
    ''' </summary>
    ''' <param name="PrivateBudget"></param>
    ''' <returns></returns>
    Public Function SavePrivateBudget(ByVal PrivateBudget As List(Of SP_ListPrivateBudget_Result), idSequense As Int64, audit As AuditMessage) As ActionResult(Of List(Of SP_ListPrivateBudget_Result)) Implements IBudgetServicePrivateBudget.SavePrivateBudget
        Using service As IPrivateBudgetAdminService = Container.Current.Resolve(Of IPrivateBudgetAdminService)()
            Return service.SavePrivateBudget(PrivateBudget, audit, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' Elimina el registro
    ''' </summary>
    ''' <param name="PrivateBudget"></param>
    ''' <returns></returns>
    Public Function DeletePrivateBudget(ByVal PrivateBudget As PrivateBudget, audit As AuditMessage) As ActionResult Implements IBudgetServicePrivateBudget.DeletePrivateBudget
        Using service As IPrivateBudgetAdminService = Container.Current.Resolve(Of IPrivateBudgetAdminService)()
            Return service.DeletePrivateBudget(PrivateBudget, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un registro por codigo
    ''' </summary>
    ''' <returns></returns>
    Public Function GetPrivateBudget(audit As AuditMessage) As ActionResult(Of List(Of SP_ListPrivateBudget_Result)) Implements IBudgetServicePrivateBudget.GetPrivateBudget
        Using service As IPrivateBudgetAdminService = Container.Current.Resolve(Of IPrivateBudgetAdminService)()
            Return service.GetListPrivateBudget(audit)
        End Using
    End Function

End Class