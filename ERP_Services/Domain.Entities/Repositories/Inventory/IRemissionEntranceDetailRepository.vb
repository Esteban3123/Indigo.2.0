'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Carlos Ernesto Cordoba
' Created          : 07-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IRemissionEntranceDetailRepository
    Inherits IRepository(Of RemissionEntranceDetail)
    ''' <summary>
    ''' lista el detalla de la remision
    ''' </summary>
    ''' <param name="idRemissionEntrance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListRemissionEntranceDetailByIdRemissionEntrance(idRemissionEntrance As Integer) As List(Of RemissionEntranceDetail)

    ''' <summary>
    ''' Lista la remisison de entrada por 
    ''' </summary>
    ''' <param name="idSupplier"></param>
    ''' <param name="idSupplierDistributionLine"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListRemissionEntranceDetailBySupplierAndSupplierDistributionLine(idSupplier As Integer, idSupplierDistributionLine As Integer) As List(Of RemissionEntranceDetail)
    ''' <summary>
    ''' obtiene un detalle de la remision por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRemissionEntranceDetailById(Id As Integer) As RemissionEntranceDetail
End Interface
