'***********************************************************************
' Assembly         : DistributedServices.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Billing
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

#End Region

Partial Class BillingService

    ''' <summary>
    ''' obtiene una boleta de salida por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSlipOutByCode(code As String, audit As AuditMessage) As Domain.Entities.SlipOut Implements IBillingServiceSlipOut.GetSlipOutByCode
        Using service As ISlipOutAdminService = Container.Current.Resolve(Of ISlipOutAdminService)()
            Return service.GetSlipOutByCode(code, audit)
        End Using
        'Return _slipOutAdminService.GetSlipOutByCode(code, audit)
    End Function

    ''' <summary>
    ''' obtiene una boleta de salida por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSlipOutById(Id As Integer) As Domain.Entities.SlipOut Implements IBillingServiceSlipOut.GetSlipOutById
        Using service As ISlipOutAdminService = Container.Current.Resolve(Of ISlipOutAdminService)()
            Return service.GetSlipOutById(Id)
        End Using
        'Return _slipOutAdminService.GetSlipOutById(Id)
    End Function

    ''' <summary>
    ''' guarda una boleta de salida
    ''' </summary>
    ''' <param name="SlipOut"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSlipOut(SlipOut As Domain.Entities.SlipOut, idSequence As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SlipOut) Implements IBillingServiceSlipOut.SaveSlipOut
        Using service As ISlipOutAdminService = Container.Current.Resolve(Of ISlipOutAdminService)()
            Return service.SaveSlipOut(SlipOut, audit, idSequence)
        End Using
        'Return _slipOutAdminService.SaveSlipOut(SlipOut, audit, idSequence)
    End Function

    ''' <summary>
    ''' Obtiene una boleta de salida por numero de admisión
    ''' </summary>
    ''' <param name="numberAdmission"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSlipOutByNumberAdmission(numberAdmission As String, audit As AuditMessage) As Domain.Entities.SlipOut Implements IBillingServiceSlipOut.GetSlipOutByNumberAdmission
        Using service As ISlipOutAdminService = Container.Current.Resolve(Of ISlipOutAdminService)()
            Return service.GetSlipOutByNumberAdmission(numberAdmission, audit)
        End Using
        'Return _slipOutAdminService.GetSlipOutByNumberAdmission(numberAdmission, audit)
    End Function
End Class
