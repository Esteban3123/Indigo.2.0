'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IPaymentsMonthlyAmortization

    ''' <summary>
    ''' Confirma la amortizacion mensual
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ConfirmMonthlyAmortization(listDeferredCausationShare As List(Of Domain.Entities.DeferredCausationShare), idOperatingUnit As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.DeferredCausationShare))

    ''' <summary>
    ''' Obtiene un listado de causaciones diferidas dependiendo de la fecha escogida
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetDeferredCausationByDate(year As Integer, month As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.DeferredCausationShare))

    ''' <summary>
    ''' Obtiene una causacion diferida por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetDeferredCausationById(id As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DeferredCausation)

End Interface
