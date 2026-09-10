'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Carlos Ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Treasury
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Class TreasuryService

    ''' <summary>
    ''' elimina el recibo de caja
    ''' </summary>
    ''' <param name="CashReceipts">The cash receipts.</param>
    ''' <returns></returns>
    Public Function DeleteCashReceipts(CashReceipts As CashReceipts, audit As AuditMessage) As ActionResult Implements ITreasuryServiceCashReceipts.DeleteCashReceipts
        Using service As ICashReceiptsAdminService = Container.Current.Resolve(Of ICashReceiptsAdminService)()
            Return service.DeleteCashReceipts(CashReceipts, audit)
        End Using
        'Return _cashReceiptsAdminService.DeleteCashReceipts(CashReceipts, audit)
    End Function

    ''' <summary>
    ''' obtiene el recibo de caja por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetCashReceiptsByCode(code As String, audit As AuditMessage) As ActionResult(Of CashReceipts) Implements ITreasuryServiceCashReceipts.GetCashReceiptsByCode
        Using service As ICashReceiptsAdminService = Container.Current.Resolve(Of ICashReceiptsAdminService)()
            Return service.GetCashReceiptsByCode(code, audit)
        End Using
        'Return _cashReceiptsAdminService.GetCashReceiptsByCode(code, audit)
    End Function

    ''' <summary>
    ''' obtiene el recibo de caja por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetCashReceiptsById(id As Integer) As CashReceipts Implements ITreasuryServiceCashReceipts.GetCashReceiptsById
        Using service As ICashReceiptsAdminService = Container.Current.Resolve(Of ICashReceiptsAdminService)()
            Return service.GetCashReceiptsById(id)
        End Using
        'Return _cashReceiptsAdminService.GetCashReceiptsById(id)
    End Function

    ''' <summary>
    ''' guarda el recibo de caja
    ''' </summary>
    ''' <param name="CashReceipts">The cash receipts.</param>
    ''' <returns></returns>
    Public Function SaveCashReceipts(CashReceipts As CashReceipts, idSequence As Int64, audit As AuditMessage, sequenceC As Domain.Entities.TreasurySequence) As ActionResult(Of CashReceipts) Implements ITreasuryServiceCashReceipts.SaveCashReceipts
        Using service As ICashReceiptsAdminService = Container.Current.Resolve(Of ICashReceiptsAdminService)()
            Return service.SaveCashReceipts(CashReceipts, audit, idSequence, sequenceC)
        End Using
        'Return _cashReceiptsAdminService.SaveCashReceipts(CashReceipts, audit, idSequence, sequenceC)
    End Function

    ''' <summary>
    ''' Saves the and confirm.
    ''' </summary>
    ''' <param name="CashReceipts">The cash receipts.</param>
    ''' <returns></returns>
    Public Function SaveAndConfirm(CashReceipts As Domain.Entities.CashReceipts, IndigoSessionValues As SessionValues, idSequence As Int64, audit As AuditMessage, sequenceC As Domain.Entities.TreasurySequence) As ActionResult(Of CashReceipts) Implements ITreasuryServiceCashReceipts.SaveAndConfirm
        Using service As ICashReceiptsAdminService = Container.Current.Resolve(Of ICashReceiptsAdminService)()
            Return service.SaveAndConfirm(CashReceipts, audit, IndigoSessionValues, idSequence, sequenceC)
        End Using
        'Return _cashReceiptsAdminService.SaveAndConfirm(CashReceipts, audit, IndigoSessionValues, idSequence, sequenceC)
    End Function
    ''' <summary>
    ''' confirma el recibo de caja
    ''' </summary>
    ''' <param name="idCashReceipt"></param>
    ''' <returns></returns>
    Public Function ConfirmCashReceipts(idCashReceipt As Integer, IndigoSessionValues As SessionValues, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of String) Implements ITreasuryServiceCashReceipts.ConfirmCashReceipts
        Using service As ICashReceiptsAdminService = Container.Current.Resolve(Of ICashReceiptsAdminService)()
            Return service.ConfirmCashReceipts(idCashReceipt, audit, IndigoSessionValues)
        End Using
        'Return _cashReceiptsAdminService.ConfirmCashReceipts(idCashReceipt, audit, IndigoSessionValues)
    End Function

    ''' <summary>
    ''' metodo para obtener los detalles del recibo de caja
    ''' </summary>
    ''' <param name="idCashReceipt">The identifier cash receipt.</param>
    ''' <returns></returns>
    Public Function GetCashReceiptDetailsByIdCashReceipt(idCashReceipt As Integer) As List(Of Domain.Entities.CashReceiptDetails) Implements ITreasuryServiceCashReceipts.GetCashReceiptDetailsByIdCashReceipt
        Using service As ICashReceiptsAdminService = Container.Current.Resolve(Of ICashReceiptsAdminService)()
            Return service.GetCashReceiptDetailsByIdCashReceipt(idCashReceipt)
        End Using
        'Return _cashReceiptsAdminService.GetCashReceiptDetailsByIdCashReceipt(idCashReceipt)
    End Function

    ''' <summary>
    ''' metodo para obtener los metodos de pago de los recibos de caja
    ''' </summary>
    ''' <param name="idCashRegister">The identifier cash register.</param>
    ''' <returns></returns>
    Public Function GetPaymentMethodsByIdCashReceipts(idCashRegister As Integer) As List(Of Domain.Entities.PaymentMethods) Implements ITreasuryServiceCashReceipts.GetPaymentMethodsByIdCashReceipts
        Using service As ICashReceiptsAdminService = Container.Current.Resolve(Of ICashReceiptsAdminService)()
            Return service.GetPaymentMethodsByIdCashReceip(idCashRegister)
        End Using
        'Return _cashReceiptsAdminService.GetPaymentMethodsByIdCashReceip(idCashRegister)
    End Function

    ''' <summary>
    ''' metodo para obtener las facturas del tercero por los datos que se pegaron en la rejilla
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="idThirdPaty"></param>
    ''' <param name="idMainAccount"></param>
    ''' <param name="idCostCenter"></param>
    ''' <returns></returns>
    Public Function SetBillsCashReceipts(data As List(Of List(Of String)), idThirdPaty As Integer, idMainAccount As Integer, idCostCenter As Integer, idOperatingUnit As Integer) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.AccountReceivable)) Implements ITreasuryServiceCashReceipts.SetBillsCashReceipts
        Using service As ICashReceiptsAdminService = Container.Current.Resolve(Of ICashReceiptsAdminService)()
            Return service.SetBillsCashReceipts(data, idThirdPaty, idMainAccount, idCostCenter, idOperatingUnit)
        End Using
        'Return _cashReceiptsAdminService.SetBillsCashReceipts(data, idThirdPaty, idMainAccount, idCostCenter, idOperatingUnit)
    End Function

    Public Function ValidateCostcenterPaymentMethod(PaymentMethods As Domain.Entities.PaymentMethods, operatingUnitId As Integer) As Domain.Base.Entities.ActionResult Implements ITreasuryServiceCashReceipts.ValidateCostcenterPaymentMethod
        Using service As ICashReceiptsAdminService = Container.Current.Resolve(Of ICashReceiptsAdminService)()
            Return service.ValidateCostcenterPaymentMethod(PaymentMethods, operatingUnitId)
        End Using
        'Return _cashReceiptsAdminService.ValidateCostcenterPaymentMethod(PaymentMethods, operatingUnitId)
    End Function

    ''' <summary>
    ''' Metodo para realizar un recibo de caja
    ''' </summary>
    ''' <param name="invoiceNumber"></param>
    ''' <param name="value"></param>
    ''' <param name="documentDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateCashReceiptsREST(invoiceNumber As String, value As Decimal, documentDate As DateTime) As ActionResult Implements ITreasuryServiceCashReceipts.GenerateCashReceiptsREST
        Using service As ICashReceiptsAdminService = Container.Current.Resolve(Of ICashReceiptsAdminService)()
            Return service.GenerateCashReceiptsREST(invoiceNumber, value, documentDate)
        End Using
        'Return _cashReceiptsAdminService.GenerateCashReceiptsREST(invoiceNumber, value, documentDate)
    End Function

End Class
