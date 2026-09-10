#Region "Imports"

Imports Application.Billing
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Imports Domain.Entities

#End Region

Partial Class BillingService

    ''' <summary>
    ''' obtiene una factura por Id
    ''' por código de usuario
    ''' </summary>
    ''' <param name="id">Identificador del registro</param>
    ''' <returns></returns>
    Public Function GetBasicBillingById(id As Integer) As BasicBilling Implements IBillingServiceBasicBilling.GetBasicBillingById
        Using service As IBasicBillingAdminService = Container.Current.Resolve(Of IBasicBillingAdminService)()
            Return service.GetBasicBillingById(id)
        End Using
    End Function

    ''' <summary>
    ''' obtiene una factura por codigo
    ''' </summary>
    ''' <param name="code">Código</param>
    ''' <param name="audit">Auditoria</param>
    ''' <returns></returns>
    Public Function GetBasicBillingByCode(code As String, audit As AuditMessage) As BasicBilling Implements IBillingServiceBasicBilling.GetBasicBillingByCode
        Using service As IBasicBillingAdminService = Container.Current.Resolve(Of IBasicBillingAdminService)()
            Return service.GetBasicBillingByCode(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Sets details invoice from file.
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="wareHouseId"></param>
    ''' <returns></returns>
    Public Function SetBasicBillingDetailFromFile(addressId As Integer, wareHouseId As Integer, data As List(Of List(Of String))) As ActionResult(Of List(Of BasicBillingDetail), List(Of Tuple(Of String, Integer))) Implements IBillingServiceBasicBilling.SetBasicBillingDetailFromFile
        Using service As IBasicBillingAdminService = Container.Current.Resolve(Of IBasicBillingAdminService)()
            Return service.SetBasicBillingDetailFromFile(addressId, wareHouseId, data)
        End Using
    End Function

    ''' <summary>
    ''' Guarda una factura
    ''' </summary>
    ''' <param name="basicBilling"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Async Function SaveBasicBilling(basicBilling As BasicBilling, audit As AuditMessage) As Task(Of ActionResult(Of BasicBilling)) Implements IBillingServiceBasicBilling.SaveBasicBilling
        Using service As IBasicBillingAdminService = Container.Current.Resolve(Of IBasicBillingAdminService)()
            Return Await service.SaveBasicBillingAsync(basicBilling, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda y Confirma una factura
    ''' </summary>
    ''' <param name="basicBilling"></param>
    ''' <param name="cashReceipts"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Async Function SaveAndConfirmBasicBilling(basicBilling As BasicBilling, cashReceipts As CashReceipts, session As SessionValues) As Task(Of ActionResult(Of BasicBilling)) Implements IBillingServiceBasicBilling.SaveAndConfirmBasicBilling
        Using service As IBasicBillingAdminService = Container.Current.Resolve(Of IBasicBillingAdminService)()
            Return Await service.SaveAndConfirmBasicBillingAsync(basicBilling, cashReceipts, session)
        End Using
    End Function

    ''' <summary>
    ''' Anula una factura
    ''' </summary>
    ''' <param name="basicBilling"></param>
    ''' <param name="reversalReasonId"></param>
    ''' <param name="reversalReasonDescription"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function ReverseBasicBilling(basicBilling As BasicBilling, reversalReasonId As Integer, reversalReasonDescription As String, session As SessionValues) As ActionResult(Of BasicBilling) Implements IBillingServiceBasicBilling.ReverseBasicBilling
        Using service As IBasicBillingAdminService = Container.Current.Resolve(Of IBasicBillingAdminService)()
            Return service.ReverseBasicBilling(basicBilling, reversalReasonId, reversalReasonDescription, session)
        End Using
    End Function

    ''' <summary>
    ''' obtiene una factura por codigo
    ''' </summary>
    ''' <param name="id">Código</param>
    ''' <returns></returns>
    Public Function GetCashReceiptsByBasicBillingInvoice(id As Integer) As List(Of CashReceipts) Implements IBillingServiceBasicBilling.GetCashReceiptsByBasicBillingInvoice
        Using service As IBasicBillingAdminService = Container.Current.Resolve(Of IBasicBillingAdminService)()
            Return service.GetCashReceiptsByBasicBillingInvoice(id)
        End Using
    End Function

End Class
