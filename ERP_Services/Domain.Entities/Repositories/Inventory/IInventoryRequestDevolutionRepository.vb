'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Faiber Julian Mora D.
' Created          : 28-09-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IInventoryRequestDevolutionRepository
    Inherits IRepository(Of InventoryRequestDevolution)

    ''' <summary>
    ''' Obtiene una devolución de solicitud por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryRequestDevolutionByCode(code As String) As InventoryRequestDevolution

    ''' <summary>
    ''' Obtiene una devolución de solicitud por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryRequestDevolutionById(id As Integer) As InventoryRequestDevolution

End Interface
