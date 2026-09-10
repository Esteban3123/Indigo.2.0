'***********************************************************************
' Assembly         : DistributedService.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IBillingServiceRevenueControlDetail
    ''' <summary>
    ''' obtiene un folio por id del detalle de la orden de servicio
    ''' </summary>
    ''' <param name="serviceOrderDetailId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetRevenueControlDetailByServiceOrderDetailId(serviceOrderDetailId As Integer) As RevenueControlDetail

    ''' <summary>
    ''' obtiene el modo de impresión de contrato por el id del detalle del folio
    ''' </summary>
    ''' <param name="idRevenueControl"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetPrintgModeByIdRevenueControl(idRevenueControl As Integer) As Byte

End Interface
