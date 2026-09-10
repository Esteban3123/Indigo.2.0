'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Budget
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Class BudgetService

    ''' <summary>
    ''' Elimina el detalle del traslado
    ''' </summary>
    ''' <param name="BudgetTransferDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteBudgetTransferDetail(BudgetTransferDetail As Domain.Entities.BudgetTransferDetail, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IBudgetServiceBudgetTransferDetail.DeleteBudgetTransferDetail
        Using service As IBudgetTransferDetailAdminService = Container.Current.Resolve(Of IBudgetTransferDetailAdminService)()
            Return service.DeleteBudgetTransferDetail(BudgetTransferDetail, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el detalle del traslado
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetTransferDetailById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BudgetTransferDetail) Implements IBudgetServiceBudgetTransferDetail.GetBudgetTransferDetailById
        Using service As IBudgetTransferDetailAdminService = Container.Current.Resolve(Of IBudgetTransferDetailAdminService)()
            Return service.GetBudgetTransferDetailById(Id, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o actualiza el detalle del traslado
    ''' </summary>
    ''' <param name="BudgetTransferDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveBudgetTransferDetail(BudgetTransferDetail As Domain.Entities.BudgetTransferDetail, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BudgetTransferDetail) Implements IBudgetServiceBudgetTransferDetail.SaveBudgetTransferDetail
        Using service As IBudgetTransferDetailAdminService = Container.Current.Resolve(Of IBudgetTransferDetailAdminService)()
            Return service.SaveBudgetTransferDetail(BudgetTransferDetail, audit)
        End Using
    End Function

End Class