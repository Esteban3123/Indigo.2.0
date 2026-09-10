'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Angi Camila Duran Vargas
'Created          : 27-07-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IProductInTransitDetailRepository
    Inherits IRepository(Of ProductInTransitDetail)
    ''' <summary>
    ''' lista el detalla de la remision
    ''' </summary>
    ''' <param name="idProductInTransit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListProductInTransitDetailByIdProductInTransit(idProductInTransit As Integer) As List(Of ProductInTransitDetail)

    ''' <summary>
    ''' obtiene un detalle de la remision por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetProductInTransitDetailById(Id As Integer) As ProductInTransitDetail
End Interface
