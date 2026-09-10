'***********************************************************************
' Assembly         : DistributedServices.Cost
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-02-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface ICostServiceCostOrganizationalStructure
    ''' <summary>
    ''' Guarda una estructura Organizacional
    ''' </summary>
    <OperationContract()>
    Function SaveCostOrganizationalStructure(costOrganizationalStructure As Domain.Entities.CostOrganizationalStructureOfCosts, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostOrganizationalStructureOfCosts)

    ''' <summary>
    ''' Elimina una estructura Organizacional
    ''' </summary>
    <OperationContract()>
    Function DeleteCostOrganizationalStructure(organizationalStructure As Domain.Entities.CostOrganizationalStructureOfCosts, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Updates the state.
    ''' </summary>s
    ''' <returns></returns>
    <OperationContract()>
    Function UpdateState(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostOrganizationalStructureOfCosts)

    ''' <summary>
    ''' Obtiene una estructura organizacional por codigo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCostOrganizationalStructure(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostOrganizationalStructureOfCosts)

    ''' <summary>
    ''' Obtiene una estructura organizacional por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetCostOrganizationalStructureById(id As Integer) As CostOrganizationalStructureOfCosts
End Interface
