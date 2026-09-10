'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-06-2014
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
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository

#End Region

Public Class MVoucherTransaction
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
    ''' Obtiene la fecha del servidor
    ''' </summary>
    ''' <returns>La factura de cartera</returns>
    Public Async Function GetServerDate() As Task(Of DateTime)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetServerDateAsync()
    End Function

    Public Function GetServerDateSimple() As DateTime
        Return IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetServerDate()
    End Function

    Public Async Function GetAccountPayableInNoteAndAdvance(listAccountPayableId As List(Of Integer), voucherTransactionCode As String) As Task(Of ActionResult(Of List(Of String)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetAccountPayableInNoteAndAdvanceAsync(listAccountPayableId, voucherTransactionCode)
    End Function

    ''' <summary>
    ''' Lists the voucher transaction advance by voucher transaction detail identifier.
    ''' </summary>
    ''' <param name="voucherDetailId">The voucher detail identifier.</param>
    ''' <returns></returns>
    Public Function ListVoucherTransactionAdvanceByVoucherTransactionDetailId(voucherDetailId As Integer) As XPCollection(Of VoucherTransactionAdvanceXpo)
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).TreasuryService.ListVoucherTransactionAdvanceByVoucherTransactionDetailId(voucherDetailId)
    End Function

    Function ListVoucherTransactionByPaymentMethod(paymentMethod As Byte) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).TreasuryService.ListVoucherTransactionByPaymentMethod(paymentMethod)
    End Function

    Function ListVoucherTransactionXpo() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).TreasuryService.ListAllVoucherTransaction()
    End Function

    Function GetEntityBankAccountById(ByVal id As Integer) As EntityBankAccountXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).TreasuryService.GetEntityBankAccountById(id)
    End Function

    Public Function GetExpenseConceptById(ExpenseConceptId As Integer) As ExpenseConceptXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).TreasuryService.GetXPOObject(Of ExpenseConceptXpo)("Id=" & ExpenseConceptId) '.GetExpenseConceptById(ExpenseConceptId)
    End Function

    ''' <summary>
    ''' Obtiene las obligaciones relacionadas con la cuenta por pagar
    ''' </summary>
    ''' <param name="AccountPayableId"></param>
    ''' <returns></returns>
    Public Function GetObligationDetails(AccountPayableId As Integer, Nature As Integer) As XPCollection
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).BudgetService.ListObligationDetailByEntity("AccountPayable", AccountPayableId, Nature)
    End Function

    ''' <summary>
    ''' Guarda un comprobante de egreso
    ''' </summary>
    ''' <param name="voucherTransaction">The voucher transaction.</param>
    ''' <param name="idSequence">The identifier sequence.</param>
    ''' <returns></returns>
    Public Async Function SaveVoucherTransaction(ByVal voucherTransaction As VoucherTransaction, ByVal withConfirm As Boolean, ByVal idSequence As Int64, ByVal sequenceC As Domain.Entities.TreasurySequence) As Task(Of ActionResult(Of VoucherTransaction))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveVoucherTransactionAsync(voucherTransaction, withConfirm, idSequence, sequenceC, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Confirma un comprobante de egreso
    ''' </summary>
    ''' <param name="idSequence">The identifier sequence.</param>
    ''' <returns></returns>
    Public Async Function ConfirmVoucherTransaction(ByVal IdVoucherTransaction As Integer, ByVal idSequence As Int64) As Task(Of ActionResult(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ConfirmVoucherTransactionAsync(IdVoucherTransaction, idSequence, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una factura de egreso por el id de la cuota
    ''' </summary>
    ''' <param name="IdAccountPayableShare">The identifier account payable share.</param>
    ''' <returns></returns>
    Public Function GetDischargeBillByIdAccountPayableShare(ByVal IdAccountPayableShare As Integer) As ActionResult(Of DischargeBill)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetDischargeBillByIdAccountPayableShare(IdAccountPayableShare, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Gets the treasury advance by identifier asynchronous.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetTreasuryAdvanceByIdAsync(ByVal Id As Integer) As Task(Of ActionResult(Of TreasuryAdvances))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetTreasuryAdvanceByIdAsync(Id)
    End Function

    ''' <summary>
    ''' Gets the treasury advance by identifier voucher transaction detail asynchronous.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetTreasuryAdvanceByIdVoucherTransactionDetailSimple(ByVal IdVoucherTransactionDetailAsync As Integer) As ActionResult(Of TreasuryAdvances)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetTreasuryAdvanceByIdVoucherTransactionDetail(IdVoucherTransactionDetailAsync)
    End Function

    ''' <summary>
    ''' Lists the voucher transaction detail by identifier voucher transaction.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ListVoucherTransactionDetailByIdVoucherTransaction(ByVal IdVoucherTransaction As Integer) As Task(Of ActionResult(Of List(Of VoucherTransactionDetails)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListVoucherTransactionDetailByIdVoucherTransactionAsync(IdVoucherTransaction)
    End Function

    Public Function ListVoucherTransactionDetailByIdVoucherTransactionSimple(ByVal IdVoucherTransaction As Integer) As ActionResult(Of List(Of VoucherTransactionDetails))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListVoucherTransactionDetailByIdVoucherTransaction(IdVoucherTransaction)
    End Function

    ''' <summary>
    ''' obtiene un detalle de comprobante de egreso por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetVoucherTransactionDetailById(ByVal Id As Integer) As Task(Of VoucherTransactionDetails)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetVoucherTransactionDetailByIdAsync(Id)
    End Function

    ''' <summary>
    ''' Gets the discharge bill by identifier voucher transaction d.
    ''' </summary>
    ''' <param name="IdVoucherTransactionD">The identifier voucher transaction d.</param>
    ''' <returns></returns>
    Public Async Function ListDischargeBillByIdVoucherTransactionDAsync(ByVal IdVoucherTransactionD As Integer) As Task(Of ActionResult(Of List(Of DischargeBill)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListDischargeBillByIdVoucherTransactionDAsync(IdVoucherTransactionD, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Function ListDischargeBillByIdVoucherTransactionDSimple(ByVal IdVoucherTransactionD As Integer) As ActionResult(Of List(Of DischargeBill))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListDischargeBillByIdVoucherTransactionD(IdVoucherTransactionD, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un comprobante de egreso por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetVoucherTransaction(ByVal code As String) As Task(Of ActionResult(Of VoucherTransaction))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetVoucherTransactionAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Function GetVoucherTransactionSimple(ByVal code As String) As ActionResult(Of VoucherTransaction)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetVoucherTransaction(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lists the voucher transaction between date and not refund.
    ''' </summary>
    ''' <param name="InitialDate">The initial date.</param>
    ''' <param name="FinalDate">The final date.</param>
    ''' <returns></returns>
    Public Async Function ListVoucherTransactionBetweenDateNotRefundExpenseType(ByVal CashRegisterId As Integer, ByVal InitialDate As DateTime, ByVal FinalDate As DateTime, ByVal ExpenseType As Byte) As Task(Of ActionResult(Of List(Of VoucherTransaction)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListVoucherTransactionBetweenDateNotRefundExpenseTypeAsync(CashRegisterId, InitialDate, FinalDate, ExpenseType, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lists the type of the voucher transaction final date not refund expense.
    ''' </summary>
    ''' <param name="CashRegisterId">The cash register identifier.</param>
    ''' <param name="FinalDate">The final date.</param>
    ''' <param name="ExpenseType">Type of the expense.</param>
    ''' <returns></returns>
    Public Async Function ListVoucherTransactionFinalDateNotRefundExpenseType(CashRegisterId As Integer, FinalDate As DateTime, ExpenseType As Integer) As Task(Of ActionResult(Of List(Of VoucherTransaction)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListVoucherTransactionFinalDateNotRefundExpenseTypeAsync(CashRegisterId, FinalDate, ExpenseType, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un comprobante de egreso por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetVoucherTransactionById(ByVal Id As Integer) As Task(Of ActionResult(Of VoucherTransaction))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetVoucherTransactionByIdAsync(Id)
    End Function

    Public Async Function GetCheckNumberAsync(entityAccountId As Integer, idOperativeUnit As Integer) As Task(Of SP_GetCheckNumber_Result)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCheckNumberAsync(entityAccountId, idOperativeUnit, _indigoSessionValues.UserIndigoId)
    End Function

    ''' <summary>
    ''' Obtienesla moneda por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCurrencybyId(id As Integer) As CommonCurrencyXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).TreasuryService.GetXPOObject(Of CommonCurrencyXpo)($"Id ={id}")
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
