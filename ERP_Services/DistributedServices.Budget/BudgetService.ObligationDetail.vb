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
    ''' <param name="ObligationId"></param>
    ''' <returns></returns>
    Public Function GetObligationDetailByObligationId(ObligationId As Integer) As List(Of Domain.Entities.ObligationDetail) Implements IBudgetServiceObligationDetail.GetObligationDetailByObligationId
        Using service As IObligationDetailAdminService = Container.Current.Resolve(Of IObligationDetailAdminService)()
            Return service.GetObligationDetailByObligationId(ObligationId)
        End Using
    End Function

End Class