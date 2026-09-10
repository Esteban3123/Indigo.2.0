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
    ''' obtiene le detalle de la modificacion por id de la cabecera
    ''' </summary>
    ''' <param name="pacModificationId"></param>
    ''' <returns></returns>
    Public Function GetAnnualizedCashFlowModificationDetailByPACModificationId(pacModificationId As Integer) As List(Of Domain.Entities.AnnualizedCashFlowModificationDetail) Implements IBudgetServiceAnnualizedCashFlowModificationDetail.GetAnnualizedCashFlowModificationDetailByPACModificationId
        Using service As IAnnualizedCashFlowModificationDetailAdminService = Container.Current.Resolve(Of IAnnualizedCashFlowModificationDetailAdminService)()
            Return service.GetAnnualizedCashFlowModificationDetailByPACModificationId(pacModificationId)
        End Using
    End Function

End Class