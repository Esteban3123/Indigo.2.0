'************************************************************
' Assembly         : Domain.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Base
Imports Domain.Entities
#End Region

Public Interface IServiceOrderDetailRepository
    Inherits IRepository(Of ServiceOrderDetail)

    Sub RemoveRange(servorderDetailList As List(Of ServiceOrderDetail))

    ''' <summary>
    ''' Gets the service order detail by identifier includes.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="includes">The includes.</param>
    ''' <returns></returns>
    Function GetServiceOrderDetailByIdIncludes(id As Integer, includes() As String) As ServiceOrderDetail

    ''' <summary>
    ''' Gets the service order detail by identifier with aggregates.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetServiceOrderDetailByIdWithAggregates(Id As Integer, Optional tracking As Boolean = True) As ServiceOrderDetail

    ''' <summary>
    ''' lista los detalles de la orden que podran ser incluidos en otro
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListServicesOrderDetailByAdminssionNumber(admissionNumber As String) As List(Of ServiceOrderDetail)

    ''' <summary>
    ''' Obtiene el detalle de una orden de servicio por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetServiceOrderDetailById(Id As Integer, Optional tracking As Boolean = True) As ServiceOrderDetail

    ''' <summary>
    ''' Lista las ordenes de servicio por ingreso y que no estén dentro del listado que se envia como parámetro
    ''' </summary>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <param name="serviceOrderDetailIds">The service order detail ids.</param>
    ''' <returns></returns>
    Function ListServiceOrderDetailByAdmissionNumberAndNotListServiceOrderDetailId(admissionNumber As String, serviceOrderDetailIds As List(Of Integer)) As List(Of ServiceOrderDetail)

    ''' <summary>
    ''' Gets the service order detail by identifier aggregates.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetServiceOrderDetailByIdAggregates(Id As Integer, Optional tracking As Boolean = True) As ServiceOrderDetail

    ''' <summary>
    ''' Obtiene los detalles de ordenes de servicio que pertenecen a la misma orden de servicio que otro detalle
    ''' </summary>
    ''' <param name="serviceOrderDetailId">The service order detail identifier.</param>
    ''' <returns></returns>
    Function GetServiceOrderDetailsEqualServiceOrder(serviceOrderId As Integer, serviceOrderDetailId As Integer) As List(Of ServiceOrderDetail)

    ''' <summary>
    ''' Obtener el valor de los Servicios
    ''' </summary>
    ''' <param name="AdmissionNumber"></param>
    ''' <param name="CenterAttentionCode"></param>
    ''' <param name="CupsEntityId"></param>
    ''' <param name="IPSServiceId"></param>
    ''' <param name="CareGroupId"></param>
    ''' <param name="FunctionalUnitId"></param>
    ''' <param name="Specialty"></param>
    ''' <param name="ServiceDate"></param>
    ''' <param name="PatientGenus"></param>
    ''' <param name="PatientDateBirth"></param>
    ''' <param name="InvoicedQuantity"></param>
    ''' <param name="ProfessionalHealthCode"></param>
    ''' <param name="ProfessionalHealthThirdPartyId"></param>
    ''' <param name="RiasId"></param>
    ''' <param name="ContractDescriptionId"></param>
    ''' <returns></returns>
    Function SP_GetServiceValue(AdmissionNumber As String, CenterAttentionCode As String, CupsEntityId As Integer, IPSServiceId As Integer, CareGroupId As Integer, FunctionalUnitId As Integer?, Specialty As String, ServiceDate As Date, PatientGenus As Integer, PatientDateBirth As Date, InvoicedQuantity As Integer, ProfessionalHealthCode As String, ProfessionalHealthThirdPartyId As Integer?, Optional RiasId As Integer? = Nothing, Optional ContractDescriptionId As Integer? = Nothing) As SP_GetServiceValue_Result

    ''' <summary>
    ''' Validar una lista de detalles de la orden de servicio
    ''' </summary>
    ''' <param name="serviceOrderDetailXml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function SP_ValidateServiceOrderDetail(serviceOrderDetailXml As String, userCode As String) As SP_ValidateServiceOrderDetail_Result

End Interface
