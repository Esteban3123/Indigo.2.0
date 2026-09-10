'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Oscar Ivan Sierra Jaramillo
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Budget
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Class BudgetService

#Region "Methods"

    ''' <summary>
    ''' obtiene las categorias teniendo en cuenta el id de la vigencia
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetCategory(ValidityId As Integer, TypeRevenue As String, audit As AuditMessage) As List(Of Domain.Entities.BudgetEntry) Implements IBudgetServiceEntry.GetBudgetCategory
        Using service As IBudgetEntryAdminService = Container.Current.Resolve(Of IBudgetEntryAdminService)()
            Return service.GetBudgetCategory(ValidityId, audit, TypeRevenue)
        End Using
    End Function

    ''' <summary>
    ''' guarda o actualiza el presupuesto inicial
    ''' </summary>
    ''' <param name="BudgetHeader"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveBudget(BudgetHeader As BudgetHeader, state As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of BudgetHeader) Implements IBudgetServiceEntry.SaveBudget
        Using service As IBudgetEntryAdminService = Container.Current.Resolve(Of IBudgetEntryAdminService)()
            Return service.SaveBudget(BudgetHeader, state, audit)
        End Using
    End Function

    Public Function GetBudgetBudget(ValidityId As Integer, itemType As Integer, audit As AuditMessage) As List(Of Domain.Entities.Budget) Implements IBudgetServiceEntry.GetBudgetBudget
        Using service As IBudgetEntryAdminService = Container.Current.Resolve(Of IBudgetEntryAdminService)()
            Return service.GetBudgetBudget(ValidityId, itemType, audit)
        End Using
    End Function

    Public Function GetBudgetBudgetInitialValueZero(ValidityId As Integer, itemType As Integer, FlagInitialValue As Boolean, audit As AuditMessage) As List(Of Domain.Entities.Budget) Implements IBudgetServiceEntry.GetBudgetBudgetInitialValueZero
        Using service As IBudgetEntryAdminService = Container.Current.Resolve(Of IBudgetEntryAdminService)()
            Return service.GetBudgetBudgetInitialValueZero(ValidityId, itemType, audit, FlagInitialValue)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene la cabecera con sus detalles del presupuesto inicial
    ''' </summary>
    ''' <param name="budgetaryValidityId"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetHeader(budgetaryValidityId As Integer, type As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BudgetHeader) Implements IBudgetServiceEntry.GetBudgetHeader
        Using service As IBudgetEntryAdminService = Container.Current.Resolve(Of IBudgetEntryAdminService)()
            Return service.GetBudgetHeader(budgetaryValidityId, type, audit)
        End Using
    End Function

#End Region

End Class