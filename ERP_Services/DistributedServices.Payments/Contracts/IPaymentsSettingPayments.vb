'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IPaymentsSettingPayments

    ''' <summary>
    ''' Guarda o Actualiza un parametro de pago
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveSettingPayments(settingPayments As Domain.Entities.SettingPayments, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SettingPayments)

    ''' <summary>
    ''' Elimina un parametro de pago
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteSettingPayments(settingPayments As Domain.Entities.SettingPayments, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene un determinado parametro de pago por el id de la unidad operativa
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetSettingPaymentsById(id As Integer, audit As AuditMessage) As ActionResult(Of Domain.Entities.SettingPayments)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeState(id As Integer, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SettingPayments)

End Interface
