'***********************************************************************
' Assembly         : DistributedService.Billing
' Author           : Carlos Cordoba
' Created          : 11-02-2015
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Crystal
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region
Partial Class CrystalService

    Public Function ListDashboardPharmacyDetail(consecutive As Decimal, patientCode As String, admission As String) As List(Of ViewDashboardPharmacyDetail) Implements ICrystalServiceDashboardPharmacyDetail.ListDashboardPharmacyDetail
        Using service As IDashboardPharmacyDetailAdminService = Container.Current.Resolve(Of IDashboardPharmacyDetailAdminService)()
            Return service.ListDashboardPharmacyDetail(consecutive, patientCode, admission)
        End Using
        'Return _dashboardPharmacyAdminService.ListDashboardPharmacyDetail(consecutive, patientCode, admission)
    End Function

    Public Function GetStayType(codePatient As String, admissionNumber As String) As String Implements ICrystalServiceDashboardPharmacyDetail.GetStayType
        Using service As IDashboardPharmacyDetailAdminService = Container.Current.Resolve(Of IDashboardPharmacyDetailAdminService)()
            Return service.GetStayType(codePatient, admissionNumber)
        End Using
        'Return _dashboardPharmacyAdminService.GetStayType(codePatient, admissionNumber)
    End Function

    Public Function ListDashboardPharmacyDetailSurgicalPackage(consecutive As Decimal, patientCode As String) As List(Of ViewDashBoardPharmacy_SurgicalPackageDeatils) Implements ICrystalServiceDashboardPharmacyDetail.ListDashboardPharmacyDetailSurgicalPackage
        Using service As IDashboardPharmacyDetailAdminService = Container.Current.Resolve(Of IDashboardPharmacyDetailAdminService)()
            Return service.ListDashboardPharmacyDetailSurgicalPackage(consecutive, patientCode)
        End Using
    End Function

    ''' <summary>
    ''' Valida que el cups pueda agregarse a la rejilla
    ''' </summary>
    ''' <param name="ListParameters"></param>
    ''' <returns></returns>
    Public Function SP_RIAS_ValidacionCUPSRIAS(ListParameters As List(Of Tuple(Of String, Integer, String, Integer, DateTime))) As ActionResult(Of List(Of SP_RIAS_ValidacionCUPSRIAS_Result)) Implements ICrystalServiceDashboardPharmacyDetail.SP_RIAS_ValidacionCUPSRIAS
        Using service As IDashboardPharmacyDetailAdminService = Container.Current.Resolve(Of IDashboardPharmacyDetailAdminService)()
            Return service.SP_RIAS_ValidacionCUPSRIAS(ListParameters)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para cambiar el enrutamiento de un medicamento a farmacia
    ''' </summary>
    ''' <param name="ListViewDashboardPharmacyDetail"></param>
    ''' <param name="GeneralRoutingLog"></param>
    ''' <param name="Audit"></param>
    ''' <returns></returns>
    Public Function RouteToPharmacy(ListViewDashboardPharmacyDetail As List(Of ViewDashboardPharmacyDetail), GeneralRoutingLog As RoutingLog, Audit As AuditMessage) As ActionResult Implements ICrystalServiceDashboardPharmacyDetail.RouteToPharmacy
        Using service As IDashboardPharmacyDetailAdminService = Container.Current.Resolve(Of IDashboardPharmacyDetailAdminService)()
            Return service.RouteToPharmacy(ListViewDashboardPharmacyDetail, GeneralRoutingLog, Audit)
        End Using
    End Function

    ''' <summary>
    ''' Lista un detalle de farmacia
    ''' </summary>
    ''' <param name="entityId"></param>
    ''' <returns></returns>
    Public Function DashboardPharmacyDetail(entityId As Integer) As ViewDashboardPharmacyDetail Implements ICrystalServiceDashboardPharmacyDetail.DashboardPharmacyDetail
        Using service As IDashboardPharmacyDetailAdminService = Container.Current.Resolve(Of IDashboardPharmacyDetailAdminService)()
            Return service.DashboardPharmacyDetail(entityId)
        End Using
    End Function

    ''' <summary>
    ''' Devuelve un detalle de la solicitud del paquete QX
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="patientCode"></param>
    ''' <param name="productCode"></param>
    Public Function DashboardPharmacyDetailSurgicalPackage(consecutive As Decimal, patientCode As String, productCode As String) As ViewDashBoardPharmacy_SurgicalPackageDeatils Implements ICrystalServiceDashboardPharmacyDetail.DashboardPharmacyDetailSurgicalPackage
        Using service As IDashboardPharmacyDetailAdminService = Container.Current.Resolve(Of IDashboardPharmacyDetailAdminService)()
            Return service.DashboardPharmacyDetailSurgicalPackage(consecutive, patientCode, productCode)
        End Using
    End Function

End Class
