'***********************************************************************
' Assembly         : DistributedService.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 12-06-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
Imports System.ServiceModel
#End Region

Partial Class MixingStationService
    Implements IMixingStationServicePackageDetail

    ''' <summary>
    ''' Obtiene un paquete por codigo
    ''' </summary>
    ''' <param name="packageId">The code.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetPackageDetailBypackageId(packageId As Integer, audit As AuditMessage) As ActionResult(Of PackageDetail) Implements IMixingStationServicePackageDetail.GetPackageDetailBypackageId
        Using service As IPackageDetailAdminService = Container.Current.Resolve(Of IPackageDetailAdminService)()
            Return service.GetPackageDetailBypackageId(packageId, audit)
        End Using
    End Function

    Public Function GetProductRateDetailPackageByPackageId(packageId As String) As IEnumerable(Of ProductRateDetailPackage) Implements IMixingStationServicePackageDetail.GetProductRateDetailPackageByPackageId
        Using service As IPackageDetailAdminService = Container.Current.Resolve(Of IPackageDetailAdminService)()
            Return service.GetProductRateDetailPackageByPackageId(packageId)
        End Using
    End Function
End Class
