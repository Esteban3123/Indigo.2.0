'***********************************************************************
' Assembly         : DistributedServices.InteropCost
' Author           : Diego Andrés Roldán lozano
' Created          : 22-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.InteropCost
Imports Microsoft.Practices.Unity

Partial Class InteropCostService
    Implements IInteropCostServiceGeneralExpense

    ''' <summary>
    ''' Elimina un centro de produccion
    ''' </summary>
    Public Function DeleteGeneralExpense(generalExpense As GeneralExpense, audit As AuditMessage) As ActionResult Implements IInteropCostServiceGeneralExpense.DeleteGeneralExpense
        Using service As IGeneralExpenseAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IGeneralExpenseAdminService)()
            Return service.DeleteGeneralExpense(generalExpense, audit)
        End Using
        'Return Me._generalExpenseAdminService.DeleteGeneralExpense(generalExpense, audit)
    End Function

    ''' <summary>
    ''' Obtiene un gasto general
    ''' </summary>
    Public Function GetGeneralExpense(code As String, audit As AuditMessage) As ActionResult(Of GeneralExpense) Implements IInteropCostServiceGeneralExpense.GetGeneralExpense
        Using service As IGeneralExpenseAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IGeneralExpenseAdminService)()
            Return service.GetGeneralExpense(code, audit)
        End Using
        'Return Me._generalExpenseAdminService.GetGeneralExpense(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene un gasto general por id
    ''' </summary>
    Public Function GetGeneralExpenseById(id As Integer) As GeneralExpense Implements IInteropCostServiceGeneralExpense.GetGeneralExpenseById
        Using service As IGeneralExpenseAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IGeneralExpenseAdminService)()
            Return service.GetGeneralExpenseById(id)
        End Using
        'Return Me._generalExpenseAdminService.GetGeneralExpenseById(id)
    End Function

    ''' <summary>
    ''' Guarda un centro de produccion
    ''' </summary>
    Public Function SaveGeneralExpense(generalExpense As GeneralExpense, idSequence As Int64, audit As AuditMessage) As ActionResult(Of GeneralExpense) Implements IInteropCostServiceGeneralExpense.SaveGeneralExpense
        Using service As IGeneralExpenseAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IGeneralExpenseAdminService)()
            Return service.SaveGeneralExpense(generalExpense, audit, idSequence)
        End Using
        'Return Me._generalExpenseAdminService.SaveGeneralExpense(generalExpense, audit, idSequence)
    End Function

    ''' <summary>
    ''' Gets the general expense by main account identifier.
    ''' </summary>
    ''' <param name="MainAccountId"></param>
    ''' <returns></returns>
    Public Function GetGeneralExpenseByMainAccountId(MainAccountId As Integer) As GeneralExpense Implements IInteropCostServiceGeneralExpense.GetGeneralExpenseByMainAccountId
        Using service As IGeneralExpenseAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IGeneralExpenseAdminService)()
            Return service.GetGeneralExpenseByMainAccountId(MainAccountId)
        End Using
        'Return Me._generalExpenseAdminService.GetGeneralExpenseByMainAccountId(MainAccountId)
    End Function

    Public Function UpdateStateGeneralExpense(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.GeneralExpense) Implements IInteropCostServiceGeneralExpense.UpdateStateGeneralExpense
        Using service As IGeneralExpenseAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IGeneralExpenseAdminService)()
            Return service.UpdateStateGeneralExpense(code, state, audit)
        End Using
        'Return Me._generalExpenseAdminService.UpdateStateGeneralExpense(code, state, audit)
    End Function

    Public Function ListGeneralExpenseByStatus(status As Boolean) As List(Of GeneralExpense) Implements IInteropCostServiceGeneralExpense.ListGeneralExpenseByStatus
        Using service As IGeneralExpenseAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IGeneralExpenseAdminService)()
            Return service.ListGeneralExpenseByStatus(status)
        End Using
        'Return Me._generalExpenseAdminService.ListGeneralExpenseByStatus(status)
    End Function
End Class