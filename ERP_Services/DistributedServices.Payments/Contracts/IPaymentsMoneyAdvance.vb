'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IPaymentsMoneyAdvance

    ''' <summary>
    ''' Guarda o Actualiza un anticipo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveMoneyAdavance(moneyAdvance As Domain.Entities.AdvancePayments, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AdvancePayments)

    ''' <summary>
    ''' Elimina un anticipo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteMoneyAdvance(moneyAdvance As Domain.Entities.AdvancePayments, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene un anticipo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMoneyAdvance(code As String, audit As AuditMessage) As Domain.Entities.AdvancePayments

    ''' <summary>
    ''' Obtiene un anticipo por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMoneyAdvanceById(id As Integer, audit As AuditMessage) As Domain.Entities.AdvancePayments

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateMoneyAdvance(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AdvancePayments)

    ''' <summary>
    ''' Lista todos los avances por tercero
    ''' </summary>
    ''' <param name="ThirdId">The third identifier.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function ListAdvancePaymentByThirdId(ByVal ThirdId As Integer) As ActionResult(Of List(Of AdvancePayments))

    ''' <summary>
    ''' Obtiene un anticipo por código
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetAdvanceByCode(ByVal code As String) As AdvancePayments

End Interface
