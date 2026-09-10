'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Carlos Cordoba
' Created          : 2015-01-24
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IPharmacyDetailRepository
    Inherits IRepository(Of HCFARMEPD)

    ''' <summary>
    ''' obtieene un detalle de farmacia por consecutivo y por codigo de producto
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="productCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPharmacyDetailByConsecutiveAndProductCode(consecutive As Decimal, productCode As String) As HCFARMEPD

    ''' <summary>
    ''' lista los detalles de farmacia por el consecutivo y que tengan cantidad por entregar
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListPharmacyDetailByConsecutive(consecutive As Decimal) As List(Of HCFARMEPD)
End Interface
