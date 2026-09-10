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
Imports Domain.Entities

#End Region

Partial Class BillingService
    Public Function GetInvoiceDetailByServiceOrderDetailId(serviceOrderDetailId As Integer) As Domain.Entities.InvoiceDetail Implements IBillingServiceServiceOrder.GetInvoiceDetailByServiceOrderDetailId
        Using service As IServiceOrderAdminService = Container.Current.Resolve(Of IServiceOrderAdminService)()
            Return service.GetInvoiceDetailByServiceOrderDetailId(serviceOrderDetailId)
        End Using
        'Return _serviceOrderAdminService.GetInvoiceDetailByServiceOrderDetailId(serviceOrderDetailId)
    End Function
    ''' <summary>
    ''' Obtener una orden de servicio por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetServiceOrder(code As String, audit As AuditMessage) As Domain.Entities.ServiceOrder Implements IBillingServiceServiceOrder.GetServiceOrder
        Using service As IServiceOrderAdminService = Container.Current.Resolve(Of IServiceOrderAdminService)()
            Return service.GetServiceOrder(code, audit)
        End Using
        'Return _serviceOrderAdminService.GetServiceOrder(code, audit)
    End Function

    ''' <summary>
    ''' Obtener una orden de servicio por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetServiceOrderById(id As Integer) As Domain.Entities.ServiceOrder Implements IBillingServiceServiceOrder.GetServiceOrderById
        Using service As IServiceOrderAdminService = Container.Current.Resolve(Of IServiceOrderAdminService)()
            Return service.GetServiceOrderById(id)
        End Using
        'Return _serviceOrderAdminService.GetServiceOrderById(id)
    End Function

    ''' <summary>
    ''' Obtiene un detalle de orden de servicio por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetServiceOrderDetailById(Id As Integer) As Domain.Entities.ServiceOrderDetail Implements IBillingServiceServiceOrder.GetServiceOrderDetailById
        Using service As IServiceOrderAdminService = Container.Current.Resolve(Of IServiceOrderAdminService)()
            Return service.GetServiceOrderDetailById(Id)
        End Using
        'Return _serviceOrderAdminService.GetServiceOrderDetailById(Id)
    End Function

    ''' <summary>
    ''' metodo para obtener los detalles de la orden de servicios
    ''' </summary>
    ''' <param name="ServiceOrderId"></param>
    ''' <returns></returns>
    Public Function GetServiceOrderDetailByServiceOrderId(ServiceOrderId As Integer) As List(Of Domain.Entities.ServiceOrderDetail) Implements IBillingServiceServiceOrder.GetServiceOrderDetailByServiceOrderId
        Using service As IServiceOrderAdminService = Container.Current.Resolve(Of IServiceOrderAdminService)()
            Return service.GetServiceOrderDetailByServiceOrderId(ServiceOrderId)
        End Using
        'Return _serviceOrderAdminService.GetServiceOrderDetailByServiceOrderId(ServiceOrderId)
    End Function

    ''' <summary>
    ''' Guardar una orden de servicio
    ''' </summary>
    ''' <param name="ServiceOrder"></param>
    ''' <returns></returns>
    Public Function SaveServiceOrder(ServiceOrder As Domain.Entities.ServiceOrder, idSequence As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ServiceOrder) Implements IBillingServiceServiceOrder.SaveServiceOrder
        Using service As IServiceOrderAdminService = Container.Current.Resolve(Of IServiceOrderAdminService)()
            Return service.SaveServiceOrder(ServiceOrder, audit, idSequence)
        End Using
        'Return _serviceOrderAdminService.SaveServiceOrder(ServiceOrder, audit, idSequence)
    End Function

    ''' <summary>
    ''' Obtiene un ingreso por codigo para utilizarlo en orden de servicio
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    Public Function GetAdmissionByServiceOrder(admissionNumber As String) As String Implements IBillingServiceServiceOrder.GetAdmissionByServiceOrder
        Using service As IServiceOrderAdminService = Container.Current.Resolve(Of IServiceOrderAdminService)()
            Return service.GetAdmissionByServiceOrder(admissionNumber)
        End Using
        'Return _serviceOrderAdminService.GetAdmissionByServiceOrder(admissionNumber)
    End Function

    ''' <summary>
    ''' Método para listar medicamentos procesados por la central de mezclas para generación de la orden de servicios.
    ''' </summary>
    ''' <param name="admissionNumber">Número de ingreso</param>
    ''' <param name="isChild">Obtiene los registros hijos</param>
    ''' <returns></returns>
    Public Function GetProcessedMedicationItemsForBillingList(admissionNumber As String, isChild As Boolean) As List(Of SP_GetProcessedMedicationItemsForBilling_Result) Implements IBillingServiceServiceOrder.GetProcessedMedicationItemsForBillingList
        Using service As IServiceOrderAdminService = Container.Current.Resolve(Of IServiceOrderAdminService)()
            Return service.GetProcessedMedicationItemsForBillingList(admissionNumber, isChild)
        End Using
    End Function

End Class
