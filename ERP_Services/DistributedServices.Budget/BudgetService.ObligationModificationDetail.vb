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
    ''' Elimina la entidad
    ''' </summary>
    ''' <param name="ObligationModificationDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteObligationModificationDetail(ObligationModificationDetail As Domain.Entities.ObligationModificationDetail, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IBudgetServiceObligationModificationDetail.DeleteObligationModificationDetail
        Using service As IObligationModificationDetailAdminService = Container.Current.Resolve(Of IObligationModificationDetailAdminService)()
            Return service.DeleteObligationModificationDetail(ObligationModificationDetail, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetObligationModificationDetailById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ObligationModificationDetail) Implements IBudgetServiceObligationModificationDetail.GetObligationModificationDetailById
        Using service As IObligationModificationDetailAdminService = Container.Current.Resolve(Of IObligationModificationDetailAdminService)()
            Return service.GetObligationModificationDetailById(Id, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o actualiza la entidad
    ''' </summary>
    ''' <param name="ObligationModificationDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveObligationModificationDetail(ObligationModificationDetail As Domain.Entities.ObligationModificationDetail, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ObligationModificationDetail) Implements IBudgetServiceObligationModificationDetail.SaveObligationModificationDetail
        Using service As IObligationModificationDetailAdminService = Container.Current.Resolve(Of IObligationModificationDetailAdminService)()
            Return service.SaveObligationModificationDetail(ObligationModificationDetail, audit, idSequense)
        End Using
    End Function

End Class