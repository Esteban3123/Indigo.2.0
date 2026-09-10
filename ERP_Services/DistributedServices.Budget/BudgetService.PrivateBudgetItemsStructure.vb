'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Carlos Mario Arias Rubiano
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
    ''' <param name="PrivateBudgetItemsStructure"></param>
    ''' <returns></returns>
    Public Function SavePrivateBudgetItemsStructure(PrivateBudgetItemsStructure As PrivateBudgetItemsStructure, idSequense As Int64, audit As AuditMessage) As ActionResult(Of PrivateBudgetItemsStructure) Implements IBudgetServicePrivateBudgetItemsStructure.SavePrivateBudgetItemsStructure
        Using service As IPrivateBudgetItemsStructureAdminService = Container.Current.Resolve(Of IPrivateBudgetItemsStructureAdminService)()
            Return service.SavePrivateBudgetItemsStructure(PrivateBudgetItemsStructure, audit, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' Elimina el registro
    ''' </summary>
    ''' <param name="PrivateBudgetItemsStructure"></param>
    ''' <returns></returns>
    Public Function DeletePrivateBudgetItemsStructure(PrivateBudgetItemsStructure As PrivateBudgetItemsStructure, audit As AuditMessage) As ActionResult Implements IBudgetServicePrivateBudgetItemsStructure.DeletePrivateBudgetItemsStructure
        Using service As IPrivateBudgetItemsStructureAdminService = Container.Current.Resolve(Of IPrivateBudgetItemsStructureAdminService)()
            Return service.DeletePrivateBudgetItemsStructure(PrivateBudgetItemsStructure, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un registro por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetPrivateBudgetItemsStructure(code As String, audit As AuditMessage) As ActionResult(Of PrivateBudgetItemsStructure) Implements IBudgetServicePrivateBudgetItemsStructure.GetPrivateBudgetItemsStructure
        Using service As IPrivateBudgetItemsStructureAdminService = Container.Current.Resolve(Of IPrivateBudgetItemsStructureAdminService)()
            Return service.GetPrivateBudgetItemsStructure(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetPrivateBudgetItemsStructureById(id As String, audit As AuditMessage) As ActionResult(Of PrivateBudgetItemsStructure) Implements IBudgetServicePrivateBudgetItemsStructure.GetPrivateBudgetItemsStructureById
        Using service As IPrivateBudgetItemsStructureAdminService = Container.Current.Resolve(Of IPrivateBudgetItemsStructureAdminService)()
            Return service.GetPrivateBudgetItemsStructureById(id, audit)
        End Using
    End Function

End Class