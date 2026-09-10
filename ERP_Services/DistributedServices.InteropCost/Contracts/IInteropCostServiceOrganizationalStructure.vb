'***********************************************************************
' Assembly         : DistributedServices.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 09-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IInteropCostServiceOrganizationalStructure

    ''' <summary>
    ''' Guarda una estructura Organizacional
    ''' </summary>
    <OperationContract()>
    Function SaveOrganizationalStructure(organizationalStructure As Domain.Entities.OrganizationalStructureOfCosts, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.OrganizationalStructureOfCosts)

    ''' <summary>
    ''' Elimina una estructura Organizacional
    ''' </summary>
    <OperationContract()>
    Function DeleteOrganizationalStructure(organizationalStructure As Domain.Entities.OrganizationalStructureOfCosts, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function UpdateState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of OrganizationalStructureOfCosts)

    ''' <summary>
    ''' Obtiene una estructura organizacional por codigo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetOrganizationalStructure(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.OrganizationalStructureOfCosts)

    ''' <summary>
    ''' Obtiene una estructura organizacional por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetOrganizationalStructureById(id As Integer) As OrganizationalStructureOfCosts

End Interface