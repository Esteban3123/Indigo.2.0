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

Public Interface ISettingPaymentsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un parametro de pago
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveSettingPayments(ByVal settingPayments As SettingPayments, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of SettingPayments)

    ''' <summary>
    ''' Elimina un parametro de pago
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteSettingPayments(ByVal settingPayments As SettingPayments, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un determinado parametro de pago por el id de la unidad operativa
    ''' </summary>
    ''' <returns></returns>
    Function GetSettingPaymentsById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of SettingPayments)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeState(ByVal id As Integer, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of SettingPayments)

End Interface
