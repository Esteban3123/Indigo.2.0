'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Carlos Cordoba
' Created          : 2015-01-24
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IHospitalInventoryRepository
    Inherits IRepository(Of IHLISTPRO)
    ''' <summary>
    ''' obtiene un inventario hospitalario por codigo del producto
    ''' </summary>
    ''' <param name="productCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetHospitalInventoryByProductCode(productCode As String) As IHLISTPRO
End Interface
