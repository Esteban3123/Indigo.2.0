'***********************************************************************
' Assembly         : Domain.Payments
' Author           : Diego Andrés Roldán Lozano
' Created          : 13-08-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IAgesPaymentsRepository
    Inherits IRepository(Of AgesPayments)

    ''' <summary>
    ''' Obtiene una edad de pagos
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetAgesPaymentsById(ByVal Id As Integer) As AgesPayments

    ''' <summary>
    ''' lista las edades de pagos
    ''' </summary>
    ''' <returns></returns>
    Function ListAgesPayments() As List(Of AgesPayments)

    ''' <summary>
    ''' lista las edades de pagos por unidad operativa
    ''' </summary>
    ''' <param name="UnitOperativeId">The unit operative identifier.</param>
    ''' <returns></returns>
    Function ListAgesPaymentsByUnitOperativeId(ByVal UnitOperativeId As Integer) As List(Of AgesPayments)

End Interface
