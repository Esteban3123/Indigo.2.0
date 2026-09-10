'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Juan Carlos Bermudez
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Budget
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class BudgetService

    ''' <summary>
    ''' Gets the collection modification detail by collection identifier.
    ''' </summary>
    ''' <param name="CollectionModificationId">The collection modification identifier.</param>
    ''' <returns></returns>
    Public Function GetCollectionModificationDetailByCollectionId(CollectionModificationId As Integer) As List(Of Domain.Entities.CollectionModificationDetail) Implements IBudgetServiceCollectionModificationDetail.GetCollectionModificationDetailByCollectionId
        Using service As ICollectionModificationDetailAdminService = Container.Current.Resolve(Of ICollectionModificationDetailAdminService)()
            Return service.GetCollectionModificationDetailByCollectionId(CollectionModificationId)
        End Using
    End Function

End Class