'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IMonthlyAmortizationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Confirma la amortizacion mensual
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function ConfirmMonthlyAmortization(ByVal listDeferredCausationShare As List(Of DeferredCausationShare), ByVal audit As AuditMessage, ByVal idOperatingUnit As Integer) As ActionResult(Of List(Of DeferredCausationShare))

    ''' <summary>
    ''' Obtiene un listado de causaciones diferidas dependiendo de la fecha escogida
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDeferredCausationByDate(year As Integer, month As Integer, ByVal audit As AuditMessage) As ActionResult(Of List(Of DeferredCausationShare))

    ''' <summary>
    ''' Obtiene una causacion diferida por id
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDeferredCausationById(id As String, ByVal audit As AuditMessage) As ActionResult(Of DeferredCausation)

End Interface
