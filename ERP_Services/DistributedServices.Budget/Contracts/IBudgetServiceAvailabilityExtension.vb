'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Juan Carlos Bermudez  
' Created          : 23-09-2015
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
Public Interface IBudgetServiceAvailabilityExtension

    ''' <summary>
    ''' obtiene una prorroga de disponibilidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetAvailabilityExtensionById(id As Integer) As AvailabilityExtension

    ''' <summary>
    ''' Guarda una prorroga de disponibilidad
    ''' </summary>
    ''' <param name="availabilityExtension"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveAvailabilityExtension(availabilityExtension As AvailabilityExtension, audit As AuditMessage) As ActionResult(Of AvailabilityExtension)

End Interface
