'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Juan Carlos Bermudez
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Budget
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class BudgetService

    ''' <summary>
    ''' Gets the collection by code.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetCollectionByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As Domain.Entities.Collection Implements IBudgetServiceCollection.GetCollectionByCode
        Using service As ICollectionAdminService = Container.Current.Resolve(Of ICollectionAdminService)()
            Return service.GetCollectionByCode(code, BudgetaryValidityId, audit)
        End Using
    End Function

    ''' <summary>
    ''' Gets the collection by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetCollectionById(id As Integer) As Domain.Entities.Collection Implements IBudgetServiceCollection.GetCollectionById
        Using service As ICollectionAdminService = Container.Current.Resolve(Of ICollectionAdminService)()
            Return service.GetCollectionById(id)
        End Using
    End Function

    ''' <summary>
    ''' Saves the collection.
    ''' </summary>
    ''' <param name="collection"></param>
    ''' <param name="listDetailsForDelete"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveCollection(collection As Domain.Entities.Collection, listDetailsForDelete As List(Of Integer), audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Collection) Implements IBudgetServiceCollection.SaveCollection
        Using service As ICollectionAdminService = Container.Current.Resolve(Of ICollectionAdminService)()
            Return service.SaveCollection(collection, listDetailsForDelete, audit)
        End Using
    End Function

End Class