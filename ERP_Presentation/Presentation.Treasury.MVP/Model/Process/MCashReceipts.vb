'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 04-07-2014
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
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PortfolioRepository

#End Region

Public Class MCashReceipts
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
    Public Function GetCashReceiptAccountReceivableByCashReceiptDetailId(id As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).TreasuryService.GetCashReceiptAccountReceivableByCashReceiptDetailId(id)
    End Function

    Public Function GetAccountReceivableById(id As Integer) As PortfolioAccountReceivableXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PortfolioService.GetAccountReceivableById(id)
    End Function


    Public Function ListAccountPayable(idThirdPaty As Integer, idMainAccount As Integer, idCostCenter As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PaymentsService.ListAccountPayableCashReceipt(idThirdPaty, idMainAccount, idCostCenter)
    End Function
    ''' <summary>
    ''' obtiene una factura por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPortfolioAccountReceivableXpo(id As Integer) As PortfolioAccountReceivableXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PortfolioService.GetPortfolioAccountReceivableXpo(id)
    End Function
    ''' <summary>
    ''' lista los detalles de las cuentas donde esta la factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountReceivableAccounting(accountReceivableId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PortfolioService.ListAccountReceivableAccounting(accountReceivableId)
    End Function

    ''' <summary>
    ''' obtener el recibo de caja por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetCashRegisterByCode(code As String) As Task(Of ActionResult(Of CashReceipts))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCashReceiptsByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Function ValidateCostcenterPaymentMethod(PaymentMethods As PaymentMethods, operatingUnitId As Integer) As ActionResult
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ValidateCostcenterPaymentMethod(PaymentMethods, operatingUnitId)
    End Function


    ''' <summary>
    ''' obtener el recibo de caja por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCashRegisterById(id As Integer) As CashReceipts
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCashReceiptsById(id)
    End Function

    ''' <summary>
    ''' guardar el recibo de caja
    ''' </summary>
    ''' <param name="CashReceipts"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveCashReceipts(CashReceipts As CashReceipts, idSequence As Integer, ByVal sequenceC As Domain.Entities.TreasurySequence) As Task(Of ActionResult(Of CashReceipts))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveCashReceiptsAsync(CashReceipts, idSequence, Me._indigoSessionValues.AuditMessageWcf, sequenceC)
    End Function

    ''' <summary>
    ''' guardar y confirmar el recibo de caja
    ''' </summary>
    ''' <param name="CashReceipts"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAndConfirmCashReceipts(CashReceipts As CashReceipts, idSequence As Integer, ByVal sequenceC As Domain.Entities.TreasurySequence) As Task(Of ActionResult(Of CashReceipts))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveAndConfirmAsync(CashReceipts, _indigoSessionValues, idSequence, Me._indigoSessionValues.AuditMessageWcf, sequenceC)
    End Function

    ''' <summary>
    ''' confirma el recibo de caja
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ConfirmCashReceipts(idCashReceipt As Integer) As Task(Of ActionResult(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ConfirmCashReceiptsAsync(idCashReceipt, _indigoSessionValues, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' eliminar el recibo de caja
    ''' </summary>
    ''' <param name="CashReceipts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteCashReceipts(CashReceipts As CashReceipts) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.DeleteCashReceiptsAsync(CashReceipts, Me._indigoSessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' metodo para obtener los detalles del recibo de caja
    ''' </summary>
    ''' <param name="idCashReceipt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCashReceiptDetailsByIdCashReceipt(idCashReceipt As Integer) As List(Of CashReceiptDetails)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCashReceiptDetailsByIdCashReceipt(idCashReceipt)
    End Function

    Public Async Function GetCashReceiptDetailsByIdCashReceiptAsync(idCashReceipt As Integer) As Task(Of List(Of CashReceiptDetails))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCashReceiptDetailsByIdCashReceiptAsync(idCashReceipt)
    End Function
    ''' <summary>
    ''' metodo para obtener los metodos de pago del recibo de caja
    ''' </summary>
    ''' <param name="idCashReceipt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentMethodsByCashReceipts(idCashReceipt As Integer) As List(Of PaymentMethods)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetPaymentMethodsByIdCashReceipts(idCashReceipt)
    End Function
    ''' <summary>
    ''' metodo para obtener las facturas del tercero por los datos que se pegaron en la rejilla
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="idThirdPaty"></param>
    ''' <param name="idMainAccount"></param>
    ''' <param name="idCostCenter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SetBillsCashReceipts(data As List(Of List(Of String)), idThirdPaty As Integer, idMainAccount As Integer, idCostCenter As Integer, idOperatingUnit As Integer) As Task(Of ActionResult(Of List(Of AccountReceivable)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SetBillsCashReceiptsAsync(data, idThirdPaty, idMainAccount, idCostCenter, idOperatingUnit)
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
