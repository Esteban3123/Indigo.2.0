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
    ''' obtiene el detalle del recaudo
    ''' </summary>
    ''' <param name="CollectionId"></param>
    ''' <returns></returns>
    Public Function GetCollectionDetailByCollectionId(CollectionId As Integer) As List(Of Domain.Entities.CollectionDetail) Implements IBudgetServiceCollectionDetail.GetCollectionDetailByCollectionId
        Using service As ICollectionDetailAdminService = Container.Current.Resolve(Of ICollectionDetailAdminService)()
            Return service.GetCollectionDetailByCollectionId(CollectionId)
        End Using
    End Function

End Class