'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Diego Andres Roldan Lozano
' Created          : 13-08-2014
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
    ''' elimina una edad de pagos
    ''' </summary>
    ''' <param name="agesPayment"></param>
    ''' <returns></returns>
    Public Function DeleteAgesPayment(agesPayment As AgesPayments, audit As AuditMessage) As ActionResult Implements IPaymentsServiceAgesPayments.DeleteAgesPayment
        Using service As IAgesPaymentAdminService = Container.Current.Resolve(Of IAgesPaymentAdminService)()
            Return service.DeleteAgesPayment(agesPayment, audit)
        End Using
        'Return Me._agesPaymentAdminService.DeleteAgesPayment(agesPayment, audit)
    End Function

    ''' <summary>
    ''' Obtiene una edad de pagos
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetAgesPaymentsById(Id As Integer) As AgesPayments Implements IPaymentsServiceAgesPayments.GetAgesPaymentsById
        Using service As IAgesPaymentAdminService = Container.Current.Resolve(Of IAgesPaymentAdminService)()
            Return service.GetAgesPaymentsById(Id)
        End Using
        'Return Me._agesPaymentAdminService.GetAgesPaymentsById(Id)
    End Function

    ''' <summary>
    ''' guarda una edad de pagos
    ''' </summary>
    ''' <param name="agesPayment"></param>
    ''' <returns></returns>
    Public Function SaveAgesPayment(agesPayment As AgesPayments, audit As AuditMessage) As ActionResult(Of AgesPayments) Implements IPaymentsServiceAgesPayments.SaveAgesPayment
        Using service As IAgesPaymentAdminService = Container.Current.Resolve(Of IAgesPaymentAdminService)()
            Return service.SaveAgesPayment(agesPayment, audit)
        End Using
        'Return Me._agesPaymentAdminService.SaveAgesPayment(agesPayment, audit)
    End Function

    ''' <summary>
    ''' lista las edades de pagos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAgesPayments() As ActionResult(Of List(Of AgesPayments)) Implements IPaymentsServiceAgesPayments.ListAgesPayments
        Using service As IAgesPaymentAdminService = Container.Current.Resolve(Of IAgesPaymentAdminService)()
            Return service.ListAgesPayments()
        End Using
        'Return Me._agesPaymentAdminService.ListAgesPayments()
    End Function

    ''' <summary>
    ''' lista las edades de pagos por unidad operativa
    ''' </summary>
    ''' <param name="UnitOperativeId"></param>
    ''' <returns></returns>
    Public Function ListAgesPaymentsByUnitOperativeId(UnitOperativeId As Integer) As ActionResult(Of List(Of AgesPayments)) Implements IPaymentsServiceAgesPayments.ListAgesPaymentsByUnitOperativeId
        Using service As IAgesPaymentAdminService = Container.Current.Resolve(Of IAgesPaymentAdminService)()
            Return service.ListAgesPaymentsByUnitOperativeId(UnitOperativeId)
        End Using
        'Return Me._agesPaymentAdminService.ListAgesPaymentsByUnitOperativeId(UnitOperativeId)
    End Function

End Class
