'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 18-07-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel
#End Region

Public Class MRefund
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Guarda un reembolso
    ''' </summary>
    ''' <param name="refund">The refund.</param>
    ''' <param name="idSequence">The identifier sequence.</param>
    ''' <returns></returns>
    Public Async Function SaveRefund(ByVal refund As Refunds, ByVal withConfirm As Boolean, ByVal idSequence As Int64) As Task(Of ActionResult(Of Refunds))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveRefundAsync(refund, withConfirm, Me._indigoSessionValues.AuditMessageWcf, idSequence)
    End Function

    ''' <summary>
    ''' Confirms the specified identifier refund.
    ''' </summary>
    ''' <param name="IdRefund">The identifier refund.</param>
    ''' <param name="idSequence">The identifier sequence.</param>
    ''' <returns></returns>
    Public Async Function ConfirmRefund(ByVal IdRefund As Integer, ByVal idSequence As Int64) As Task(Of ActionResult(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ConfirmRefundAsync(IdRefund, Me._indigoSessionValues.AuditMessageWcf, idSequence)
    End Function

    ''' <summary>
    ''' Gets the refund detail by voucher transaction identifier.
    ''' </summary>
    ''' <param name="voucherTransactionId">The voucher transaction identifier.</param>
    ''' <returns></returns>
    Public Async Function GetRefundDetailByVoucherTransactionId(voucherTransactionId As Integer) As Task(Of RefundDetail)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetRefundDetailByVoucherTransactionIdAsync(voucherTransactionId)
    End Function

    ''' <summary>
    ''' Elimina un reembolso
    ''' </summary>
    ''' <param name="refund">The refund.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Async Function DeleteRefund(ByVal refund As Refunds) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.DeleteRefundAsync(refund, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' obtiene un reembolso por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetRefund(ByVal code As String) As Task(Of ActionResult(Of Refunds))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetRefundAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lists the refund by account.
    ''' </summary>
    ''' <param name="IdMainAccount">The identifier main account.</param>
    ''' <returns></returns>
    Public Async Function ListRefundByAccount(ByVal IdMainAccount As Integer) As Task(Of ActionResult(Of List(Of Refunds)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListRefundByAccountAsync(IdMainAccount, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Function ListRefundByAccountSimple(ByVal IdMainAccount As Integer) As ActionResult(Of List(Of Refunds))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListRefundByAccount(IdMainAccount, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lists the refund by cash register identifier asynchronous.
    ''' </summary>
    ''' <param name="CashregisterId">The cashregister identifier.</param>
    ''' <returns></returns>
    Public Async Function ListRefundByCashRegisterIdAsync(ByVal CashregisterId As Integer) As Task(Of ActionResult(Of List(Of Refunds)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListRefundByCashRegisterIdAsync(CashregisterId)
    End Function

    ''' <summary>
    ''' Lists the refund by cash register identifier.
    ''' </summary>
    ''' <param name="CashregisterId">The cashregister identifier.</param>
    ''' <returns></returns>
    Public Function ListRefundByCashRegisterId(ByVal CashregisterId As Integer) As ActionResult(Of List(Of Refunds))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListRefundByCashRegisterId(CashregisterId)
    End Function

    ''' <summary>
    ''' Obtiene un reembolso por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetRefundById(id As Integer) As Task(Of Refunds)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetRefundByIdAsync(id)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
