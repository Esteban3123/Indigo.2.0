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
Public Interface IPaymentsTransfers

    ''' <summary>
    ''' Importa facturas al cruce de anticipos vs Cuentas por Pagar
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ImportBillsToPaymentTransfer(data As List(Of List(Of String)), ParamArray parameters As Object()) As ActionResult(Of List(Of PaymentTransferDetail))

    ''' <summary>
    ''' Guarda o Actualiza un traslado
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SavePaymentsTransfer(trasnfer As Domain.Entities.PaymentTransfer, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PaymentTransfer)

    ''' <summary>
    ''' Elimina un traslado
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeletePaymentsTransfer(trasnfer As Domain.Entities.PaymentTransfer, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene un traslado por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetPaymentsTransfer(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PaymentTransfer)

    ''' <summary>
    ''' Obtiene un traslado por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetPaymentsTransferById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PaymentTransfer)

    ''' <summary>
    ''' Confirma el traslado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ConfirmTransfer(paymentTransfer As Domain.Entities.PaymentTransfer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PaymentTransfer)

    ''' <summary>
    ''' Guarda y Confirma el traslado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveAndConfirmTransfer(paymentTransfer As Domain.Entities.PaymentTransfer, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PaymentTransfer)

End Interface
