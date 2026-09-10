'***********************************************************************
' Assembly         : Application.Billing
' Author           : Carlos E. Cordoba
' Created          : 2014-11-19
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities

Public Interface IDashboardPharmacyDetailAdminService
    Inherits IDisposable
    ''' <summary>
    ''' lista los detalle de farmacia
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="patientCode"></param>
    ''' <param name="admission"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListDashboardPharmacyDetail(consecutive As Decimal, patientCode As String, admission As String) As List(Of ViewDashboardPharmacyDetail)

    ''' <summary>
    ''' metodo para obtener el nombre del tipo de estancia
    ''' </summary>
    ''' <param name="codePatient"></param>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetStayType(codePatient As String, admissionNumber As String) As String

    ''' <summary>
    ''' listar los paquetes QX
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="patientCode"></param>
    ''' <returns></returns>
    Function ListDashboardPharmacyDetailSurgicalPackage(consecutive As Decimal, patientCode As String) As List(Of ViewDashBoardPharmacy_SurgicalPackageDeatils)

    ''' <summary>
    ''' Metodo que valida si la rias se puede agregar
    ''' </summary>
    ''' <returns></returns>
    Function SP_RIAS_ValidacionCUPSRIAS(ListParameters As List(Of Tuple(Of String, Integer, String, Integer, DateTime))) As ActionResult(Of List(Of SP_RIAS_ValidacionCUPSRIAS_Result))

    ''' <summary>
    ''' Funcion para cambiar el enrutamiento de un medicamento a farmacia
    ''' </summary>
    ''' <param name="ListHCFARMEPD"></param>
    ''' <param name="GeneralRoutingLog"></param>
    ''' <param name="Audit"></param>
    ''' <returns></returns>
    Function RouteToPharmacy(ListHCFARMEPD As List(Of ViewDashboardPharmacyDetail), GeneralRoutingLog As RoutingLog, Audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Envía el medicamento a atención farmacéutica para enrutamiento (SENDTO = 0)
    ''' </summary>
    ''' <param name="ListHCFARMEPD"></param>
    ''' <param name="Audit"></param>
    ''' <returns></returns>
    Function PharmaceuticalCareRouting(ListHCFARMEPD As List(Of ViewDashboardPharmacyDetail), Audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Lista un detalle de farmacia
    ''' </summary>
    ''' <param name="entityId"></param>
    ''' <returns></returns>
    Function DashboardPharmacyDetail(entityId As Integer) As ViewDashboardPharmacyDetail

    ''' <summary>
    ''' Devuelve un detalle de la solicitud del paquete QX
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="patientCode"></param>
    ''' <param name="productCode"></param>
    Function DashboardPharmacyDetailSurgicalPackage(consecutive As Decimal, patientCode As String, productCode As String) As ViewDashBoardPharmacy_SurgicalPackageDeatils

End Interface
