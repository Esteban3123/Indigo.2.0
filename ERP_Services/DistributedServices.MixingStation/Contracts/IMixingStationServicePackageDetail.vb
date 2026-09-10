'***********************************************************************
' Assembly         : DistributedServices.MixinStation
' Author           : Yoe Andres Cardenas
' Created          : 12/06/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IMixingStationServicePackageDetail
    ''' <summary>
    ''' Obtiene los detalles del paquete por código del paquete
    ''' </summary>
    ''' <param name="packageId">The identifier.</param>
    <OperationContract()>
    Function GetPackageDetailBypackageId(packageId As Integer, audit As AuditMessage) As ActionResult(Of PackageDetail)

    <OperationContract()>
    Function GetProductRateDetailPackageByPackageId(packageId As String) As IEnumerable(Of ProductRateDetailPackage)
End Interface
