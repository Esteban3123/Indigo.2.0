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
    ''' Cambia de estado la entidad
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeState(id As Integer, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SettingPayments) Implements IPaymentsSettingPayments.ChangeState
        Using service As ISettingPaymentsAdminService = Container.Current.Resolve(Of ISettingPaymentsAdminService)()
            Return service.ChangeState(id, state, audit)
        End Using
        'Return Me._settingPaymentsAdminService.ChangeState(id, state, audit)
    End Function

    ''' <summary>
    ''' Elimina un parametro de pago
    ''' </summary>
    ''' <param name="settingPayments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteSettingPayments(settingPayments As Domain.Entities.SettingPayments, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IPaymentsSettingPayments.DeleteSettingPayments
        Using service As ISettingPaymentsAdminService = Container.Current.Resolve(Of ISettingPaymentsAdminService)()
            Return service.DeleteSettingPayments(settingPayments, audit)
        End Using
        'Return Me._settingPaymentsAdminService.DeleteSettingPayments(settingPayments, audit)
    End Function

    ''' <summary>
    ''' Obtiene un parametro de pago por id unidad operativa
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSettingPaymentsById(id As Integer, audit As AuditMessage) As ActionResult(Of Domain.Entities.SettingPayments) Implements IPaymentsSettingPayments.GetSettingPaymentsById
        Using service As ISettingPaymentsAdminService = Container.Current.Resolve(Of ISettingPaymentsAdminService)()
            Return service.GetSettingPaymentsById(id, audit)
        End Using
        'Return Me._settingPaymentsAdminService.GetSettingPaymentsById(id, audit)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un parametro de pago
    ''' </summary>
    ''' <param name="settingPayments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSettingPayments(settingPayments As Domain.Entities.SettingPayments, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SettingPayments) Implements IPaymentsSettingPayments.SaveSettingPayments
        Using service As ISettingPaymentsAdminService = Container.Current.Resolve(Of ISettingPaymentsAdminService)()
            Return service.SaveSettingPayments(settingPayments, audit)
        End Using
        'Return Me._settingPaymentsAdminService.SaveSettingPayments(settingPayments, audit)
    End Function

End Class
