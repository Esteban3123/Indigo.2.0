#Region "Imports"

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Threading.Tasks

#End Region

Public Interface IBasicBillingAdminService
    Inherits IDisposable

    ''' <summary>
    ''' obtiene una factura por Id
    ''' por código de usuario
    ''' </summary>
    ''' <param name="id">Identificador del registro</param>
    ''' <returns></returns>
    Function GetBasicBillingById(id As Integer) As BasicBilling

    ''' <summary>
    ''' obtiene una factura por codigo
    ''' </summary>
    ''' <param name="code">Código</param>
    ''' <param name="audit">Auditoria</param>
    ''' <returns></returns>
    Function GetBasicBillingByCode(code As String, audit As AuditMessage) As BasicBilling

    ''' <summary>
    ''' Sets details invoice from file.
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="wareHouseId"></param>
    ''' <returns></returns>
    Function SetBasicBillingDetailFromFile(addressId As Integer, wareHouseId As Integer, data As List(Of List(Of String))) As ActionResult(Of List(Of BasicBillingDetail), List(Of Tuple(Of String, Integer)))

    ''' <summary>
    ''' Guarda una factura
    ''' </summary>
    ''' <param name="basicBilling"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveBasicBillingAsync(basicBilling As BasicBilling, audit As AuditMessage) As Task(Of ActionResult(Of BasicBilling))

    ''' <summary>
    ''' Confirma una factura
    ''' </summary>
    ''' <param name="basicBilling"></param>
    ''' <param name="cashReceipts"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function ConfirmBasicBillingAsync(basicBilling As BasicBilling, cashReceipts As CashReceipts, session As SessionValues) As Task(Of ActionResult(Of BasicBilling))

    ''' <summary>
    ''' Guarda y Confirma una factura
    ''' </summary>
    ''' <param name="basicBilling"></param>
    ''' <param name="cashReceipts"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function SaveAndConfirmBasicBillingAsync(basicBilling As BasicBilling, cashReceipts As CashReceipts, session As SessionValues) As Task(Of ActionResult(Of BasicBilling))

    ''' <summary>
    ''' Anula una factura
    ''' </summary>
    ''' <param name="basicBilling"></param>
    ''' <param name="reversalReasonId"></param>
    ''' <param name="reversalReasonDescription"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function ReverseBasicBilling(basicBilling As BasicBilling, reversalReasonId As Integer, reversalReasonDescription As String, session As SessionValues) As ActionResult(Of BasicBilling)

    ''' <summary>
    ''' obtiene una factura por codigo
    ''' </summary>
    ''' <param name="id">Código</param>
    ''' <returns></returns>
    Function GetCashReceiptsByBasicBillingInvoice(id As Integer) As List(Of CashReceipts)

End Interface
