'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Diego Andres Roldan Lozano
' Created          : 13-08-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IPaymentsServiceAgesPayments

    ''' <summary>
    ''' guarda una edad de pagos
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveAgesPayment(agesPayment As AgesPayments, audit As AuditMessage) As ActionResult(Of AgesPayments)

    ''' <summary>
    ''' elimina una edad de pagos
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteAgesPayment(agesPayment As AgesPayments, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una edad de pagos
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAgesPaymentsById(ByVal Id As Integer) As AgesPayments

    ''' <summary>
    ''' lista las edades de pagos
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListAgesPayments() As ActionResult(Of List(Of AgesPayments))

    ''' <summary>
    ''' lista las edades de pagos por unidad operativa
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListAgesPaymentsByUnitOperativeId(ByVal UnitOperativeId As Integer) As ActionResult(Of List(Of AgesPayments))

End Interface
