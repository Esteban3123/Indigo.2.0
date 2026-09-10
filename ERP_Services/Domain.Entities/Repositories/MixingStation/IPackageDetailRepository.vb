'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 12-06-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Base.Entities

Public Interface IPackageDetailRepository
    Inherits IRepository(Of PackageDetail)

    ''' <summary>
    ''' Obtiene todos los detalles del paquete
    ''' </summary>
    ''' <returns>Lista de Monedas</returns>
    ''' <remarks></remarks>
    Function ListAllPackageDetail() As List(Of PackageDetail)

    ''' <summary>
    ''' Obtiene el detalle del paquete por código de paquete
    ''' </summary>
    ''' <param name="packageId">The identifier.</param>
    ''' <returns></returns>
    Function GetPackageDetailByPackageId(packageId As Integer, Optional tracking As Boolean = True) As PackageDetail
End Interface
