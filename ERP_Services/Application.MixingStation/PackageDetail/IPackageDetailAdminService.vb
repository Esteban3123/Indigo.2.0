'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Yoe Andres cardenas
' Created          : 12-06-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IPackageDetailAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un detalle del paquete por id
    ''' </summary>
    ''' <param name="packageId">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function GetPackageDetailBypackageId(packageId As Integer, ByVal audit As AuditMessage) As ActionResult(Of PackageDetail)

    Function GetProductRateDetailPackageByPackageId(packageId As String) As IEnumerable(Of ProductRateDetailPackage)

End Interface