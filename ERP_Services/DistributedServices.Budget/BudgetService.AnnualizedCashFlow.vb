'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Juan Carlos Bermudez
' Created          : 19-08-2015
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

#Region "Methods"

    ''' <summary>
    ''' guarda o actualiza el presupuesto inicial
    ''' </summary>
    ''' <param name="ListAnnualizedCashFlow"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveListAnnualizedCashFlowHeader(ListAnnualizedCashFlow As List(Of AnnualizedCashFlow), state As Integer, type As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of AnnualizedCashFlow)) Implements IBudgetServiceAnnualizedCashFlow.SaveListAnnualizedCashFlowHeader
        Using service As IAnnualizedCashFlowAdminService = Container.Current.Resolve(Of IAnnualizedCashFlowAdminService)()
            Return service.SaveListAnnualizedCashFlowHeader(ListAnnualizedCashFlow, state, type, audit)
        End Using
    End Function

    Public Function GetAnnualizedCashFlowByValidityIdAndByCodeCategory(ValidityId As Integer, CodeCategory As String, type As Integer, audit As AuditMessage) As ActionResult(Of List(Of Domain.Entities.AnnualizedCashFlow)) Implements IBudgetServiceAnnualizedCashFlow.GetAnnualizedCashFlowByValidityIdAndByCodeCategory
        Using service As IAnnualizedCashFlowAdminService = Container.Current.Resolve(Of IAnnualizedCashFlowAdminService)()
            Return service.GetAnnualizedCashFlowByValidityIdAndByCodeCategory(ValidityId, CodeCategory, type, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtener todos los registros de PAC inicial por vigencia
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <returns></returns>
    Public Function GetAnnualizedCashFlowByValidityId(ValidityId As Integer) As List(Of Domain.Entities.AnnualizedCashFlow) Implements IBudgetServiceAnnualizedCashFlow.GetAnnualizedCashFlowByValidityId
        Using service As IAnnualizedCashFlowAdminService = Container.Current.Resolve(Of IAnnualizedCashFlowAdminService)()
            Return service.GetAnnualizedCashFlowByValidityId(ValidityId)
        End Using
    End Function

#End Region

End Class