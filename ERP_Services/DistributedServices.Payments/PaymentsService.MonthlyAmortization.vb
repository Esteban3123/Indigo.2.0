'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Payments
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Public Class PaymentsService

    ''' <summary>
    ''' Confirmar la amortizacion mensual
    ''' </summary>
    ''' <param name="listDeferredCausationShare"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmMonthlyAmortization(listDeferredCausationShare As List(Of Domain.Entities.DeferredCausationShare), idOperatingUnit As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.DeferredCausationShare)) Implements IPaymentsMonthlyAmortization.ConfirmMonthlyAmortization
        Using service As IMonthlyAmortizationAdminService = Container.Current.Resolve(Of IMonthlyAmortizationAdminService)()
            Return service.ConfirmMonthlyAmortization(listDeferredCausationShare, audit, idOperatingUnit)
        End Using
        'Return _monthlyAmortizationAdminService.ConfirmMonthlyAmortization(listDeferredCausationShare, audit, idOperatingUnit)
    End Function

    ''' <summary>
    ''' Obtiene las causaciones diferidas por fecha
    ''' </summary>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDeferredCausationByDate(year As Integer, month As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.DeferredCausationShare)) Implements IPaymentsMonthlyAmortization.GetDeferredCausationByDate
        Using service As IMonthlyAmortizationAdminService = Container.Current.Resolve(Of IMonthlyAmortizationAdminService)()
            Return service.GetDeferredCausationByDate(year, month, audit)
        End Using
        'Return _monthlyAmortizationAdminService.GetDeferredCausationByDate(year, month, audit)
    End Function

    ''' <summary>
    ''' Obtiene la causacion diferida por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDeferredCausationById(id As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DeferredCausation) Implements IPaymentsMonthlyAmortization.GetDeferredCausationById
        Using service As IMonthlyAmortizationAdminService = Container.Current.Resolve(Of IMonthlyAmortizationAdminService)()
            Return service.GetDeferredCausationById(id, audit)
        End Using
        'Return _monthlyAmortizationAdminService.GetDeferredCausationById(id, audit)
    End Function

End Class
