'***********************************************************************
' Assembly         : Domain.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 09-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IOrganizationalStructureRepository
    Inherits IRepository(Of OrganizationalStructureOfCosts)

    ''' <summary>
    ''' Obtiene una estructura organizacional por codigo
    ''' </summary>
    ''' <returns></returns>
    Function GetOrganizationalStructure(ByVal code As String) As OrganizationalStructureOfCosts

    ''' <summary>
    ''' Obtiene una estructura organizacional por id
    ''' </summary>
    ''' <returns></returns>
    Function GetOrganizationalStructureById(id As Integer) As OrganizationalStructureOfCosts

End Interface