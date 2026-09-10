'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 09-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IOrganizationalStructureAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda una estructura Organizacional
    ''' </summary>
    Function SaveOrganizationalStructure(ByVal organizationalStructure As OrganizationalStructureOfCosts, ByVal audit As AuditMessage) As ActionResult(Of OrganizationalStructureOfCosts)

    ''' <summary>
    ''' Elimina una estructura Organizacional
    ''' </summary>
    Function DeleteOrganizationalStructure(ByVal organizationalStructure As OrganizationalStructureOfCosts, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function UpdateState(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of OrganizationalStructureOfCosts)

    ''' <summary>
    ''' Obtiene una estructura organizacional por codigo
    ''' </summary>
    ''' <returns></returns>
    Function GetOrganizationalStructure(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of OrganizationalStructureOfCosts)

    ''' <summary>
    ''' Obtiene una estructura organizacional por id
    ''' </summary>
    ''' <returns></returns>
    Function GetOrganizationalStructureById(id As Integer) As OrganizationalStructureOfCosts

End Interface