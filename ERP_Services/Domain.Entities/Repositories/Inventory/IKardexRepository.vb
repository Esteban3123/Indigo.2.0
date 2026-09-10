'************************************************************
' Assembly         : Domain.Inventory.IKardexRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 11/01/2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region
Public Interface IKardexRepository
    Inherits IRepository(Of Kardex)

    ''' <summary>
    ''' Obtiene un Kardex a través del ID
    ''' </summary>
    ''' <param name="id">Id del Kardex</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetKardexById(id As Integer) As Kardex

    ''' <summary>
    ''' Obtiene el id del almacen y el id del producto 
    ''' </summary>
    ''' <param name="WarehouseId">Id del Kardex</param>
    ''' <param name="ProductId">Id del Kardex</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetQuantityKardexPoductIdAndWareHouseId(ByVal WarehouseId As List(Of Integer), ByVal ProductId As Integer) As Integer

    ''' <summary>
    ''' Obtiene la cantidad total de productos en el kardex de una bodega
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetQuantityKardex(id As Integer) As Integer

End Interface
