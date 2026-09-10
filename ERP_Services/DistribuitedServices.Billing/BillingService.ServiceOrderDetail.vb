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
Imports Domain.Base.Entities
Imports DistribuitedServices.Billing
Imports Domain.Entities
Imports Microsoft.Practices.Unity

#End Region

Partial Class BillingService
    Implements IBillingServiceServiceOrderDetail

    ''' <summary>
    ''' lista los detalles de la orden que podran ser incluidos en otro
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    Public Function ListServicesOrderDetailByAdminssionNumber(admissionNumber As String) As List(Of Domain.Entities.ServiceOrderDetail) Implements IBillingServiceServiceOrderDetail.ListServicesOrderDetailByAdminssionNumber
        Using service As IServiceOrderDetailAdminService = Container.Current.Resolve(Of IServiceOrderDetailAdminService)()
            Return service.ListServicesOrderDetailByAdminssionNumber(admissionNumber)
        End Using
        'Return _serviceOrderDetailAdminService.ListServicesOrderDetailByAdminssionNumber(admissionNumber)
    End Function

    ''' <summary>
    ''' metodo para obtener el valor de los servicios
    ''' </summary>
    ''' <param name="listCupsHomologation"></param>
    ''' <param name="CareGroupId"></param>
    ''' <param name="FunctionalUnitId"></param>
    ''' <param name="Specialty"></param>
    ''' <param name="ServiceDate"></param>
    ''' <returns></returns>
    Public Function GetServiceValue(AdmissionNumber As String, CenterAttentionCode As String, listCupsHomologation As List(Of Domain.Entities.CupsHomologation), CareGroupId As Integer, FunctionalUnitId As Integer, Specialty As String, ServiceDate As Date, PatientGenus As Integer, PatientDateBirth As Date, InvoicedQuantity As Integer, ProfessionalHealthCode As String, ProfessionalHealthThirdPartyId As Integer, Optional RiasId As Integer? = Nothing, Optional ContractDescriptionId As Integer? = Nothing) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.ServiceOrderDetail)) Implements IBillingServiceServiceOrderDetail.GetServiceValue
        Using service As IServiceOrderDetailAdminService = Container.Current.Resolve(Of IServiceOrderDetailAdminService)()
            Return service.GetServiceValue(AdmissionNumber, CenterAttentionCode, listCupsHomologation, CareGroupId, FunctionalUnitId, Specialty, ServiceDate, PatientGenus, PatientDateBirth, InvoicedQuantity, ProfessionalHealthCode, ProfessionalHealthThirdPartyId, RiasId, ContractDescriptionId)
        End Using
        'Return _serviceOrderDetailAdminService.GetServiceValue(listCupsHomologation, CareGroupId, FunctionalUnitId, Specialty, ServiceDate, PatientGenus, PatientDateBirth, InvoicedQuantity, ProfessionalHealthCode, ProfessionalHealthThirdPartyId)
    End Function

    ''' <summary>
    ''' metodo para obterner el valor con recargo
    ''' </summary>
    ''' <param name="serviceOrderDetail"></param>
    ''' <returns></returns>
    Public Function GetServiceValueSurcharge(serviceOrderDetail As Domain.Entities.ServiceOrderDetail) As Domain.Entities.ServiceOrderDetail Implements IBillingServiceServiceOrderDetail.GetServiceValueSurcharge
        Using service As IServiceOrderDetailAdminService = Container.Current.Resolve(Of IServiceOrderDetailAdminService)()
            Return service.GetServiceValueSurcharge(serviceOrderDetail)
        End Using
        'Return _serviceOrderDetailAdminService.GetServiceValueSurcharge(serviceOrderDetail)
    End Function

    ''' <summary>
    ''' obtener el valor de los detalles del ips quirurgico cuando el usuario cambia los valores por defecto en el formulario
    ''' </summary>
    ''' <param name="serviceOrderDetail"></param>
    ''' <param name="listSurgicalProcedureServiceDefault"></param>
    ''' <returns></returns>
    Public Function GetServiceValueBySurgicalProcedureService(serviceOrderDetail As Domain.Entities.ServiceOrderDetail, listSurgicalProcedureServiceDefault As List(Of Domain.Entities.SurgicalProcedureService)) As ActionResult(Of Domain.Entities.ServiceOrderDetail) Implements IBillingServiceServiceOrderDetail.GetServiceValueBySurgicalProcedureService
        Using service As IServiceOrderDetailAdminService = Container.Current.Resolve(Of IServiceOrderDetailAdminService)()
            Return service.GetServiceValueBySurgicalProcedureService(serviceOrderDetail, listSurgicalProcedureServiceDefault)
        End Using
        'Return _serviceOrderDetailAdminService.GetServiceValueBySurgicalProcedureService(serviceOrderDetail, listSurgicalProcedureServiceDefault)
    End Function

    ''' <summary>
    ''' metodo para recalcular los eventos cuando se cambie el item que es primer evento
    ''' </summary>
    ''' <param name="serviceOrderDetail"></param>
    ''' <returns></returns>
    Public Function RecalculateSurgicalEvents(serviceOrderDetail As Domain.Entities.ServiceOrderDetail) As Domain.Entities.ServiceOrderDetail Implements IBillingServiceServiceOrderDetail.RecalculateSurgicalEvents
        Using service As IServiceOrderDetailAdminService = Container.Current.Resolve(Of IServiceOrderDetailAdminService)()
            Return service.RecalculateSurgicalEvents(serviceOrderDetail)
        End Using
        'Return _serviceOrderDetailAdminService.RecalculateSurgicalEvents(serviceOrderDetail)
    End Function

    ''' <summary>
    ''' Actualiza los campos de médico y tercero del detalle de la orden de servicio
    ''' </summary>
    ''' <param name="ServiceOrderDetailId"></param>
    ''' <param name="HealthProfessionalCode"></param>
    ''' <param name="ThirdPartyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateFieldsServiceOrderDetail(ServiceOrderDetailId As Integer, HealthProfessionalCode As String, ThirdPartyId As Integer) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ServiceOrderDetail) Implements IBillingServiceServiceOrderDetail.UpdateFieldsServiceOrderDetail
        Using service As IServiceOrderDetailAdminService = Container.Current.Resolve(Of IServiceOrderDetailAdminService)()
            Return service.UpdateFieldsServiceOrderDetail(ServiceOrderDetailId, HealthProfessionalCode, ThirdPartyId)
        End Using
        'Return _serviceOrderDetailAdminService.UpdateFieldsServiceOrderDetail(ServiceOrderDetailId, HealthProfessionalCode, ThirdPartyId)
    End Function

    ''' <summary>
    ''' Actualiza los campos de médico y tercero del detalle de la orden de servicio
    ''' </summary>
    ''' <param name="ListTuple">Este listado contiene los id para poder consultar y actualizar</param>
    ''' <param name="SelectionSurgical">Permite saber si se esta cambiando el medico en la rejilla Qx o NoQx (True=Qx, False=NoQx)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateHealthProfessionalForMedicalFeesCausation(ListTuple As List(Of Tuple(Of Integer, Integer, String, Integer)), SelectionSurgical As Boolean, Company As String) As Domain.Base.Entities.ActionResult Implements IBillingServiceServiceOrderDetail.UpdateHealthProfessionalForMedicalFeesCausation
        Using service As IServiceOrderDetailAdminService = Container.Current.Resolve(Of IServiceOrderDetailAdminService)()
            Return service.UpdateHealthProfessionalForMedicalFeesCausation(ListTuple, SelectionSurgical, Company)
        End Using
        'Return _serviceOrderDetailAdminService.UpdateHealthProfessionalForMedicalFeesCausation(ListTuple, SelectionSurgical, Company)
    End Function

    ''' <summary>
    ''' Lista las ordenes de servicio por ingreso y que no estén dentro del listado que se envia como parámetro
    ''' </summary>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <param name="serviceOrderDetailIds">The service order detail ids.</param>
    ''' <returns></returns>
    Public Function ListServiceOrderDetailByAdmissionNumberAndNotListServiceOrderDetailId(admissionNumber As String, serviceOrderDetailIds As List(Of Integer)) As List(Of Domain.Entities.ServiceOrderDetail) Implements IBillingServiceServiceOrderDetail.ListServiceOrderDetailByAdmissionNumberAndNotListServiceOrderDetailId
        Using service As IServiceOrderDetailAdminService = Container.Current.Resolve(Of IServiceOrderDetailAdminService)()
            Return service.ListServiceOrderDetailByAdmissionNumberAndNotListServiceOrderDetailId(admissionNumber, serviceOrderDetailIds)
        End Using
        'Return _serviceOrderDetailAdminService.ListServiceOrderDetailByAdmissionNumberAndNotListServiceOrderDetailId(admissionNumber, serviceOrderDetailIds)
    End Function

    Public Function UpdateServiceOrderDetailList(serviceOrderDetailList As List(Of Domain.Entities.ServiceOrderDetail)) As ActionResult Implements IBillingServiceServiceOrderDetail.UpdateServiceOrderDetailList
        Using service As IServiceOrderDetailAdminService = Container.Current.Resolve(Of IServiceOrderDetailAdminService)()
            Return service.UpdateServiceOrderDetailList(serviceOrderDetailList)
        End Using
        'Return _serviceOrderDetailAdminService.UpdateServiceOrderDetailList(serviceOrderDetailList)
    End Function

    Public Function GetServiceValueByManual(ServiceDetail As ServiceOrderDetail, IPSServiceId As Integer, CareGroupId As Integer, FunctionalUnitId As Integer, Specialty As String, ServiceDate As Date, PatientGenus As Integer, PatientDateBirth As Date, InvoicedQuantity As Integer, ProfessionalHealthCode As String, ProfessionalHealthThirdPartyId As Integer) As ActionResult(Of ServiceOrderDetail) Implements IBillingServiceServiceOrderDetail.GetServiceValueByManual
        Using service As IServiceOrderDetailAdminService = Container.Current.Resolve(Of IServiceOrderDetailAdminService)()
            Return service.GetServiceValueByManual(ServiceDetail, IPSServiceId, CareGroupId, FunctionalUnitId, Specialty, ServiceDate, PatientGenus, PatientDateBirth, InvoicedQuantity, ProfessionalHealthCode, ProfessionalHealthThirdPartyId)
        End Using
        'Return _serviceOrderDetailAdminService.GetServiceValueByManual(ServiceDetail, IPSServiceId, CareGroupId, FunctionalUnitId, Specialty, ServiceDate, PatientGenus, PatientDateBirth, InvoicedQuantity, ProfessionalHealthCode, ProfessionalHealthThirdPartyId)
    End Function

    ''' <summary>
    ''' Metodo que calcula el valor del servicio, este metodo es utilizado cuando el usuario cambia el valor del control de rias en ordenes de servicio
    ''' </summary>
    ''' <returns></returns>
    Public Function GetServiceValueByRIAS(AdmissionNumber As String, CenterAttentionCode As String, CupsEntityId As Integer, IPSServiceId As Integer, CareGroupId As Integer, FunctionalUnitId As Integer, Specialty As String, ServiceDate As Date, PatientGenus As Integer, PatientDateBirth As Date, InvoicedQuantity As Integer, ProfessionalHealthCode As String, ProfessionalHealthThirdPartyId As Integer, Optional RiasId As Integer? = Nothing) As ActionResult(Of ServiceOrderDetail) Implements IBillingServiceServiceOrderDetail.GetServiceValueByRIAS
        Using service As IServiceOrderDetailAdminService = Container.Current.Resolve(Of IServiceOrderDetailAdminService)()
            Return service.GetServiceValueByRIAS(AdmissionNumber, CenterAttentionCode, CupsEntityId, IPSServiceId, CareGroupId, FunctionalUnitId, Specialty, ServiceDate, PatientGenus, PatientDateBirth, InvoicedQuantity, ProfessionalHealthCode, ProfessionalHealthThirdPartyId, RiasId)
        End Using
    End Function

    Public Function ValidateServiceOrderDetail(listServiceOrderDetail As List(Of ServiceOrderDetail), audit As AuditMessage) As ActionResult(Of ServiceOrderDetail) Implements IBillingServiceServiceOrderDetail.ValidateServiceOrderDetail
        Using service As IServiceOrderDetailAdminService = Container.Current.Resolve(Of IServiceOrderDetailAdminService)()
            Return service.ValidateServiceOrderDetail(listServiceOrderDetail, audit)
        End Using
    End Function

End Class
