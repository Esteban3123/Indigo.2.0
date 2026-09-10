'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Juan Carlos Bermudez  
' Created          : 01-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

<ServiceContract()>
Public Interface IBudgetServiceAvailabilityModification

    ''' <summary>
    ''' obtiene una modificacion de disponibilidad por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAvailabilityModificationByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As Domain.Entities.AvailabilityModification
    ''' <summary>
    ''' obtiene una modificacion de disponibilidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetAvailabilityModificationById(id As Integer) As AvailabilityModification

    ''' <summary>
    ''' Guarda una modificacion de disponibilidad
    ''' </summary>
    ''' <param name="availabilityModification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveAvailabilityModification(AvailabilityModification As AvailabilityModification, listAvailabilityModificationDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of AvailabilityModification)

End Interface
