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
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

<ServiceContract()>
Public Interface IBillingServiceServiceOrderDetail
    ''' <summary>
    ''' lista los detalles de la orden que podran ser incluidos en otro
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListServicesOrderDetailByAdminssionNumber(admissionNumber As String) As List(Of ServiceOrderDetail)
    ''' <summary>
    ''' metodo para obtener el valor de los servicios
    ''' </summary>
    <OperationContract()>
    Function GetServiceValue(AdmissionNumber As String, CenterAttentionCode As String, listCupsHomologation As List(Of CupsHomologation), CareGroupId As Integer, FunctionalUnitId As Integer, Specialty As String, ServiceDate As Date, PatientGenus As Integer, PatientDateBirth As Date, InvoicedQuantity As Integer, ProfessionalHealthCode As String, ProfessionalHealthThirdPartyId As Integer, Optional RiasId As Integer? = Nothing, Optional ContractDescriptionId As Integer? = Nothing) As ActionResult(Of List(Of ServiceOrderDetail))

    ''' <summary>
    ''' Metodo que obtiene el valor del servicio enviado, se realiza otro metodo porque solo aplica cuando el usuario cambie el valor del control de rias en ordenes de servicio
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetServiceValueByRIAS(AdmissionNumber As String, CenterAttentionCode As String, CupsEntityId As Integer, IPSServiceId As Integer, CareGroupId As Integer, FunctionalUnitId As Integer, Specialty As String, ServiceDate As Date, PatientGenus As Integer, PatientDateBirth As Date, InvoicedQuantity As Integer, ProfessionalHealthCode As String, ProfessionalHealthThirdPartyId As Integer, Optional RiasId As Integer? = Nothing) As ActionResult(Of ServiceOrderDetail)

    ''' <summary>
    ''' Funcion para ca
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetServiceValueByManual(ServiceDetail As ServiceOrderDetail, IPSServiceId As Integer, CareGroupId As Integer, FunctionalUnitId As Integer, Specialty As String, ServiceDate As Date, PatientGenus As Integer, PatientDateBirth As Date, InvoicedQuantity As Integer, ProfessionalHealthCode As String, ProfessionalHealthThirdPartyId As Integer) As ActionResult(Of ServiceOrderDetail)

    ''' <summary>
    ''' metodo para obterner el valor con recargo
    ''' </summary>
    ''' <param name="serviceOrderDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetServiceValueSurcharge(serviceOrderDetail As ServiceOrderDetail) As ServiceOrderDetail
    ''' <summary>
    ''' obtener el valor de los detalles del ips quirurgico cuando el usuario cambia los valores por defecto en el formulario
    ''' </summary>
    ''' <param name="serviceOrderDetail"></param>
    ''' <param name="listSurgicalProcedureServiceDefault"></param>
    ''' <param name="ProfessionalHealthCode"></param>
    ''' <param name="Specialty"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetServiceValueBySurgicalProcedureService(serviceOrderDetail As ServiceOrderDetail, listSurgicalProcedureServiceDefault As List(Of SurgicalProcedureService)) As ActionResult(Of ServiceOrderDetail)

    ''' <summary>
    ''' metodo para recalcular los eventos cuando se cambie el item que es primer evento
    ''' </summary>
    ''' <param name="serviceOrderDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function RecalculateSurgicalEvents(serviceOrderDetail As ServiceOrderDetail) As ServiceOrderDetail

    ''' <summary>
    ''' Actualiza los campos de médico y tercero del detalle de la orden de servicio
    ''' </summary>
    ''' <param name="ServiceOrderDetailId"></param>
    ''' <param name="HealthProfessionalCode"></param>
    ''' <param name="ThirdPartyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function UpdateFieldsServiceOrderDetail(ServiceOrderDetailId As Integer, HealthProfessionalCode As String, ThirdPartyId As Integer) As ActionResult(Of ServiceOrderDetail)

    ''' <summary>
    ''' Actualiza los campos de médico y tercero del detalle de la orden de servicio
    ''' </summary>
    ''' <param name="ListTuple">Este listado contiene los id para poder consultar y actualizar</param>
    ''' <param name="SelectionSurgical">Permite saber si se esta cambiando el medico en la rejilla Qx o NoQx (True=Qx, False=NoQx)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function UpdateHealthProfessionalForMedicalFeesCausation(ListTuple As List(Of Tuple(Of Integer, Integer, String, Integer)), SelectionSurgical As Boolean, Company As String) As ActionResult

    ''' <summary>
    ''' Lista las ordenes de servicio por ingreso y que no estén dentro del listado que se envia como parámetro
    ''' </summary>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <param name="serviceOrderDetailIds">The service order detail ids.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListServiceOrderDetailByAdmissionNumberAndNotListServiceOrderDetailId(admissionNumber As String, serviceOrderDetailIds As List(Of Integer)) As List(Of ServiceOrderDetail)

    <OperationContract()>
    Function UpdateServiceOrderDetailList(serviceOrderDetailList As List(Of ServiceOrderDetail)) As ActionResult

    ''' <summary>
    ''' Validar una lista de detalles de la orden de servicio
    ''' </summary>
    ''' <param name="listServiceOrderDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ValidateServiceOrderDetail(listServiceOrderDetail As List(Of ServiceOrderDetail), audit As AuditMessage) As ActionResult(Of ServiceOrderDetail)

End Interface
