'***********************************************************************
' Assembly         : Domain.InventoryControlService
' Author           : Miguel Angel Fonseca Castro
' Created          : 08-04-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IInventoryControlServiceRepository
    Inherits IRepository(Of InventoryControlService)

    ''' <summary>
    ''' Método para obtener el registro del servicio de control de inventario por EntityId y EntityName
    ''' </summary>
    ''' <param name="entityId"></param>
    ''' <param name="entityName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryControlServiceByEntityIdAndEntityName(entityId As Integer, entityName As String) As InventoryControlService

    ''' <summary>
    ''' Método para obtener el registro del servicio de control de inventario por EntityId y EntityName
    ''' </summary>
    ''' <param name="entityCode"></param>
    ''' <param name="entityName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryControlServiceByEntityCodeAndEntityName(entityCode As String, entityName As String) As InventoryControlService

    ''' <summary>
    ''' Método una lista de control de servicios de inventario por orden medica
    ''' </summary>
    ''' <param name="medicalOrderRecipe"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryControlServiceByMedicalOrderRecipe(medicalOrderRecipe As String) As List(Of InventoryControlService)

End Interface