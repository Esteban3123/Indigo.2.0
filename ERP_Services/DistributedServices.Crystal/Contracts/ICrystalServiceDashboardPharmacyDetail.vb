'***********************************************************************
' Assembly         : DistributedService.Crystal
' Author           : Carlos Cordoba
' Created          : 11-02-2015
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities

#End Region

<ServiceContract()> _
Public Interface ICrystalServiceDashboardPharmacyDetail
    ''' <summary>
    ''' lista los detalle de farmacia
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="patientCode"></param>
    ''' <param name="admission"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListDashboardPharmacyDetail(consecutive As Decimal, patientCode As String, admission As String) As List(Of ViewDashboardPharmacyDetail)

    ''' <summary>
    ''' metodo para obtener el nombre del tipo de estancia
    ''' </summary>
    ''' <param name="codePatient"></param>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetStayType(codePatient As String, admissionNumber As String) As String

    ''' <summary>
    ''' Lista los paquetes QX
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="patientCode"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListDashboardPharmacyDetailSurgicalPackage(consecutive As Decimal, patientCode As String) As List(Of ViewDashBoardPharmacy_SurgicalPackageDeatils)

    ''' <summary>
    ''' Metodo que valida si la rias se puede agregar
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SP_RIAS_ValidacionCUPSRIAS(ListParameters As List(Of Tuple(Of String, Integer, String, Integer, DateTime))) As ActionResult(Of List(Of SP_RIAS_ValidacionCUPSRIAS_Result))

    ''' <summary>
    ''' Funcion para cambiar el enrutamiento de un medicamento a farmacia
    ''' </summary>
    ''' <param name="ListHCFARMEPD"></param>
    ''' <param name="GeneralRoutingLog"></param>
    ''' <param name="Audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function RouteToPharmacy(ListHCFARMEPD As List(Of ViewDashboardPharmacyDetail), GeneralRoutingLog As RoutingLog, Audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Envía el medicamento a atención farmacéutica para enrutamiento (SENDTO = 0)
    ''' </summary>
    <OperationContract()>
    Function PharmaceuticalCareRouting(ListHCFARMEPD As List(Of ViewDashboardPharmacyDetail), Audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Lista un detalle de farmacia
    ''' </summary>
    ''' <param name="entityId"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function DashboardPharmacyDetail(entityId As Integer) As ViewDashboardPharmacyDetail

    ''' <summary>
    ''' Devuelve un detalle de la solicitud del paquete QX
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="patientCode"></param>
    ''' <param name="productCode"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function DashboardPharmacyDetailSurgicalPackage(consecutive As Decimal, patientCode As String, productCode As String) As ViewDashBoardPharmacy_SurgicalPackageDeatils

End Interface
