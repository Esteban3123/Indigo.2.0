'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Henry Alejandro Vargas Polania
' Created          : 08-04-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Threading.Tasks
Imports Domain.Base
Imports Domain.Entities

Public Interface IInventoryAdjustmentRepository
    Inherits IRepository(Of InventoryAdjustment)
    Inherits IRepositoryRollbackStrategy

    Function ListInventoryAdjustmentMassiveConfirm(listDocuments As List(Of String)) As List(Of InventoryAdjustment)

    ''' <summary>
    ''' Obtiene un InventoryAdjustment por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryAdjustment(code As String) As InventoryAdjustment

    ''' <summary>
    ''' Obtiene un InventoryAdjustment por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryAdjustmentById(id As Integer) As InventoryAdjustment


    ''' <summary>
    ''' Obtiene el InventoryAdjustment por id con o sin tracking
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryAdjustmentByIdOptionalTracking(id As Integer, Optional tracking As Boolean = True) As InventoryAdjustment

End Interface
