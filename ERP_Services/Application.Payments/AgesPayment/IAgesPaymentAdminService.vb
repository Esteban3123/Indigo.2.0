'***********************************************************************
' Assembly         : Application.Payments
' Author           : Diego Andres Roldan Lozano
' Created          : 13-08-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IAgesPaymentAdminService
    Inherits IDisposable

    ''' <summary>
    ''' guarda una edad de pagos
    ''' </summary>
    ''' <returns></returns>
    Function SaveAgesPayment(ByVal agesPayment As AgesPayments, ByVal audit As AuditMessage) As ActionResult(Of AgesPayments)

    ''' <summary>
    ''' elimina una edad de pagos
    ''' </summary>
    ''' <returns></returns>
    Function DeleteAgesPayment(ByVal agesPayment As AgesPayments, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una edad de pagos
    ''' </summary>
    ''' <returns></returns>
    Function GetAgesPaymentsById(ByVal Id As Integer) As AgesPayments

    ''' <summary>
    ''' lista las edades de pagos
    ''' </summary>
    ''' <returns></returns>
    Function ListAgesPayments() As ActionResult(Of List(Of AgesPayments))

    ''' <summary>
    ''' lista las edades de pagos por unidad operativa
    ''' </summary>
    ''' <param name="UnitOperativeId">The unit operative identifier.</param>
    ''' <returns></returns>
    Function ListAgesPaymentsByUnitOperativeId(ByVal UnitOperativeId As Integer) As ActionResult(Of List(Of AgesPayments))

End Interface
