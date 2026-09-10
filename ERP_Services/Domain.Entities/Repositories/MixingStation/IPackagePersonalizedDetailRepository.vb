'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Duván Albeiro Mejia Cortes
' Created          : 16-09-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Base.Entities

Public Interface IPackagePersonalizedDetailRepository
    Inherits IRepository(Of PackagePersonalizedDetail)

    ''' <summary>
    ''' Obtiene todos los detalles del paquete
    ''' </summary>
    ''' <returns>Lista de Monedas</returns>
    ''' <remarks></remarks>
    Function ListAllPackageDetail() As List(Of PackagePersonalizedDetail)

    ''' <summary>
    ''' Obtiene el detalle del paquete por código de paquete
    ''' </summary>
    ''' <param name="packageId">The identifier.</param>
    ''' <returns></returns>
    Function GetPackageDetailByPackageId(packageId As Integer, Optional tracking As Boolean = True) As PackagePersonalizedDetail

    ''' <summary>
    ''' Obtiene los detalles del Paquete por Id del Paquete
    ''' </summary>
    ''' <param name="packageId"></param>
    ''' <returns></returns>
    Function GetPackageDetailListByPackageId(packageId As Integer) As List(Of PackagePersonalizedDetail)
End Interface
