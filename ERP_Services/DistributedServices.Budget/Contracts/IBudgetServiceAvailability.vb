'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jeisson Herrera Peña
' Created          : 25/08/2015
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
Public Interface IBudgetServiceAvailability

    ''' <summary>
    ''' Obtiene una dispoinibilidad por código
    ''' </summary>
    '''<param name="Code">Código de la disponibilidad</param>
    ''' <param name="ItemType">TIPO DE RUBRO (NINGUNO = 0,INGRESO = 1,GASTO = 2)</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAvailability(Code As String, ItemType As Byte, BudgetaryValidityId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Availability)

    ''' <summary>
    ''' Obtiene una disponibilidad por id
    ''' </summary>
    '''<param name="Id">Id de la disponibilidad</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetAvailabilityById(Id As Integer) As Availability

    ''' <summary>
    ''' Guarda o Actualiza una disponibilidad
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveAvailability(availability As Domain.Entities.Availability, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Availability)

    ''' <summary>
    ''' Elimina un reconocimiento
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteAvailability(availability As Domain.Entities.Availability, audit As AuditMessage) As Domain.Base.Entities.ActionResult

End Interface
