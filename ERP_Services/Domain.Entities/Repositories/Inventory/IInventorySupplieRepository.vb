'***********************************************************************
' Assembly         : Domain.Entities.Repositories.Inventory
' Author           : Hector Rodriguez Rubiano
' Created          : 08-01-2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base

Public Interface IInventorySupplieRepository
    Inherits IRepository(Of InventorySupplie)

    ''' <summary>
    ''' Función que obtiene todos los insumos
    ''' </summary>
    ''' <returns></returns>
    Function ListAllInventorySupplie() As List(Of InventorySupplie)

    ''' <summary>
    ''' Función que obtiene un insumo
    ''' </summary>
    ''' <param name="Code">Código</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Function GetInventorySupplieByCode(Code As String) As InventorySupplie

    ''' <summary>
    ''' Función que obtiene un insumo
    ''' </summary>
    ''' <param name="Id">Identificador</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Function GetInventorySupplieById(Id As Integer) As InventorySupplie

    ''' <summary>
    ''' Sp que se encarga de guardar el insumo
    ''' </summary>
    ''' <returns></returns>
    Function SP_SaveSupplie(Xml As String, UserCode As String) As SP_SaveSupplie_Result

    ''' <summary>
    ''' Sp que se encarga de eliminar un insumo
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Function SP_DeleteSupplie(Id As Integer) As SP_DeleteSupplie_Result

End Interface
