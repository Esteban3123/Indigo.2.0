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
    ''' obtiene una modificacion del pac por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetAnnualizedCashFlowModificationByCode(code As String, type As Integer, audit As AuditMessage) As Domain.Entities.AnnualizedCashFlowModification Implements IBudgetServiceAnnualizedCashFlowModification.GetAnnualizedCashFlowModificationByCode
        Using service As IAnnualizedCashFlowModificationAdminService = Container.Current.Resolve(Of IAnnualizedCashFlowModificationAdminService)()
            Return service.GetAnnualizedCashFlowModificationByCode(code, type, audit)
        End Using
    End Function

    ''' <summary>
    ''' obtiene una modificacion del pac por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetAnnualizedCashFlowModificationById(id As Integer) As Domain.Entities.AnnualizedCashFlowModification Implements IBudgetServiceAnnualizedCashFlowModification.GetAnnualizedCashFlowModificationById
        Using service As IAnnualizedCashFlowModificationAdminService = Container.Current.Resolve(Of IAnnualizedCashFlowModificationAdminService)()
            Return service.GetAnnualizedCashFlowModificationById(id)
        End Using
    End Function

    ''' <summary>
    ''' metodo para guardar una modificacion del pac
    ''' </summary>
    ''' <param name="AnnualizedCashFlowModification"></param>
    ''' <returns></returns>
    Public Function SaveAnnualizedCashFlowModification(AnnualizedCashFlowModification As Domain.Entities.AnnualizedCashFlowModification, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AnnualizedCashFlowModification) Implements IBudgetServiceAnnualizedCashFlowModification.SaveAnnualizedCashFlowModification
        Using service As IAnnualizedCashFlowModificationAdminService = Container.Current.Resolve(Of IAnnualizedCashFlowModificationAdminService)()
            Return service.SaveAnnualizedCashFlowModification(AnnualizedCashFlowModification, audit, idSequense)
        End Using
    End Function

End Class