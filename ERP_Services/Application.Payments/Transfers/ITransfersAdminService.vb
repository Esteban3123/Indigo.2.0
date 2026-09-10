'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 31-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ITransfersAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Importa facturas al cruce de anticipos vs Cuentas por Pagar
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Function ImportBillsToPaymentTransfer(data As List(Of List(Of String)), ParamArray parameters As Object()) As ActionResult(Of List(Of PaymentTransferDetail))

    ''' <summary>
    ''' Guarda o Actualiza un traslado
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SavePaymentsTransfer(ByVal trasnfer As PaymentTransfer, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of PaymentTransfer)

    ''' <summary>
    ''' Elimina un traslado
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeletePaymentsTransfer(ByVal trasnfer As PaymentTransfer, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un traslado por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPaymentsTransfer(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of PaymentTransfer)

    ''' <summary>
    ''' Obtiene un traslado por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPaymentsTransferById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of PaymentTransfer)

    ''' <summary>
    ''' Confirma el traslado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmTransfer(ByVal paymentTransfer As PaymentTransfer, ByVal audit As AuditMessage) As ActionResult(Of PaymentTransfer)

    ''' <summary>
    ''' Guarda y Confirma el traslado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveAndConfirmTransfer(ByVal paymentTransfer As PaymentTransfer, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of PaymentTransfer)

End Interface
