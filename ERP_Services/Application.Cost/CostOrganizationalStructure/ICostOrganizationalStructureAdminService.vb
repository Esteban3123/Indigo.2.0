'***********************************************************************
' Assembly         : Application.Cost
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-02-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICostOrganizationalStructureAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda una estructura Organizacional
    ''' </summary>
    Function SaveCostOrganizationalStructure(ByVal costOrganizationalStructure As CostOrganizationalStructureOfCosts, ByVal audit As AuditMessage) As ActionResult(Of CostOrganizationalStructureOfCosts)

    ''' <summary>
    ''' Elimina una estructura Organizacional
    ''' </summary>
    Function DeleteCostOrganizationalStructure(ByVal organizationalStructure As CostOrganizationalStructureOfCosts, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function UpdateState(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of CostOrganizationalStructureOfCosts)

    ''' <summary>
    ''' Obtiene una estructura organizacional por codigo
    ''' </summary>
    ''' <returns></returns>
    Function GetCostOrganizationalStructure(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of CostOrganizationalStructureOfCosts)

    ''' <summary>
    ''' Obtiene una estructura organizacional por id
    ''' </summary>
    ''' <returns></returns>
    Function GetCostOrganizationalStructureById(id As Integer) As CostOrganizationalStructureOfCosts

End Interface