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
    ''' obtiene un recuado por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetCollectionModificationByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As Domain.Entities.CollectionModification Implements IBudgetServiceCollectionModification.GetCollectionModificationByCode
        Using service As ICollectionModificationAdminService = Container.Current.Resolve(Of ICollectionModificationAdminService)()
            Return service.GetCollectionModificationByCode(code, BudgetaryValidityId, audit)
        End Using
    End Function

    ''' <summary>
    ''' obtiene un recuado por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCollectionModificationById(id As Integer) As Domain.Entities.CollectionModification Implements IBudgetServiceCollectionModification.GetCollectionModificationById
        Using service As ICollectionModificationAdminService = Container.Current.Resolve(Of ICollectionModificationAdminService)()
            Return service.GetCollectionModificationById(id)
        End Using
    End Function

    ''' <summary>
    ''' metodo para guardar un recaudo
    ''' </summary>
    ''' <param name="CollectionModification"></param>
    ''' <returns></returns>
    Public Function SaveCollectionModification(CollectionModification As Domain.Entities.CollectionModification, listModificationDetailDelete As List(Of Integer), audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CollectionModification) Implements IBudgetServiceCollectionModification.SaveCollectionModification
        Using service As ICollectionModificationAdminService = Container.Current.Resolve(Of ICollectionModificationAdminService)()
            Return service.SaveCollectionModification(CollectionModification, listModificationDetailDelete, audit)
        End Using
    End Function

End Class