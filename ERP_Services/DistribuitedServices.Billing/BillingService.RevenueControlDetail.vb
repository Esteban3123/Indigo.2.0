'***********************************************************************
' Assembly         : DistributedServices.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Billing
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

#End Region

Partial Class BillingService
    ''' <summary>
    ''' obtiene un folio por id del detalle de la orden de servicio
    ''' </summary>
    ''' <param name="serviceOrderDetailId"></param>
    ''' <returns></returns>
    Public Function GetRevenueControlDetailByServiceOrderDetailId(serviceOrderDetailId As Integer) As Domain.Entities.RevenueControlDetail Implements IBillingServiceRevenueControlDetail.GetRevenueControlDetailByServiceOrderDetailId
        Using service As IRevenueControlDetailAdminService = Container.Current.Resolve(Of IRevenueControlDetailAdminService)()
            Return service.GetRevenueControlDetailByServiceOrderDetailId(serviceOrderDetailId)
        End Using
        'Return _revenueControlDetailAdminService.GetRevenueControlDetailByServiceOrderDetailId(serviceOrderDetailId)
    End Function

    ''' <summary>
    ''' obtiene el modo de impresión de contrato por el id del detalle del folio
    ''' </summary>
    ''' <param name="idRevenueControl"></param>
    ''' <returns></returns>
    Public Function GetPrintgModeByIdRevenueControl(idRevenueControl As Integer) As Byte Implements IBillingServiceRevenueControlDetail.GetPrintgModeByIdRevenueControl
        Using service As IRevenueControlDetailAdminService = Container.Current.Resolve(Of IRevenueControlDetailAdminService)()
            Return service.GetPrintgModeByIdRevenueControl(idRevenueControl)
        End Using
        'Return _revenueControlDetailAdminService.GetPrintgModeByIdRevenueControl(idRevenueControl)
    End Function

End Class
