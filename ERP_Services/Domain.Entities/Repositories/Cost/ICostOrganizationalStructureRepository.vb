'***********************************************************************
' Assembly         : Domain.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-02-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface ICostOrganizationalStructureRepository
    Inherits IRepository(Of CostOrganizationalStructureOfCosts)

    ''' <summary>
    ''' Obtiene una estructura organizacional por codigo
    ''' </summary>
    ''' <returns></returns>
    Function GetCostOrganizationalStructure(ByVal code As String) As CostOrganizationalStructureOfCosts

    ''' <summary>
    ''' Obtiene una estructura organizacional por id
    ''' </summary>
    ''' <returns></returns>
    Function GetCostOrganizationalStructureById(id As Integer) As CostOrganizationalStructureOfCosts

End Interface