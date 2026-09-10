'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Miguel Angel Fonseca Castro
' Created          : 2018-11-13
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
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Infrastructure.Data.Xpo.BillingRepository

#End Region

Public Class MBasicBilling
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
    ''' Obtiene el registro de facturacion basica por codigo
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetBasicBillingByCode(code As String) As Task(Of BasicBilling)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetBasicBillingByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lista los recibos de caja asociados por id de factura
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Async Function GetCashReceiptsByBasicBillingInvoice(id As Integer) As Task(Of List(Of CashReceipts))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetCashReceiptsByBasicBillingInvoiceAsync(id)
    End Function

    ''' <summary>
    ''' se verifica esi la factura tiene algun cruce
    ''' </summary>
    ''' <param name="invoiceId"></param>
    ''' <returns></returns>
    Public Async Function GetPortfolioTransferByBasicBilling(invoiceId As Integer) As Task(Of List(Of PortfolioTransferXpo))
        Dim filter As String = $"PortfolioTransferDetailXpo[AccountReceivableId.InvoiceId.Id={invoiceId}]"
        Return Await Task.Factory.StartNew(Function() As List(Of PortfolioTransferXpo)
                                               Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PortfolioService.GetCollectionAsList(Of PortfolioTransferXpo)(Nothing, filter)
                                           End Function)
    End Function

    Public Async Function SetBasicBillingDetailFromFile(addressId As Integer, wareHouseId As Integer, data As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of BasicBillingDetail), List(Of Tuple(Of String, Integer))))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SetBasicBillingDetailFromFileAsync(addressId, wareHouseId, data)
    End Function

    Public Async Function SaveBasicBilling(basicBilling As BasicBilling) As Task(Of ActionResult(Of BasicBilling))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SaveBasicBillingAsync(basicBilling, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function SaveAndConfirmBasicBilling(basicBilling As BasicBilling, cashReceipts As CashReceipts) As Task(Of ActionResult(Of BasicBilling))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SaveAndConfirmBasicBillingAsync(basicBilling, cashReceipts, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Anular una factura
    ''' </summary>
    ''' <returns>Lista de objeciones</returns>
    Public Async Function ReverseBasicBilling(basicBilling As BasicBilling, id As Integer, description As String) As Task(Of ActionResult(Of BasicBilling))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ReverseBasicBillingAsync(basicBilling, id, description, Me._indigoSessionValues)
    End Function


    ''' <summary>
    ''' Consulta EL TRM de las monedas origne vs destino
    ''' </summary>
    ''' <param name="FromCurrencyId"></param>
    ''' <param name="ToCurrencyId"></param>
    ''' <returns></returns>
    Public Async Function GetTRMbyCurrencyIdAsync(ToCurrencyId As Integer, FromCurrencyId As Integer, Optional DateTrm As Date? = Nothing) As Task(Of ActionResult(Of TRM))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetTRMbyCurrencyIdAsync(ToCurrencyId, FromCurrencyId, Me._indigoSessionValues, DateTrm, Nothing)
    End Function

    ''' <summary>
    ''' funcion para cargar la informacion del PopUp de "importar" en facturacion basica
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewListBasicBillingForImport(Optional Header As Boolean = True,
                                                      Optional SaleModality As Byte? = Nothing,
                                                      Optional CurrencyId As Integer? = Nothing,
                                                      Optional CustomerId As Integer? = Nothing) As List(Of ViewListBasicBillingForImportXpo)
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).BillingService.GetViewListBasicBillingForImportXpo(Header, SaleModality, CurrencyId, CustomerId)
    End Function

    ''' <summary>
    ''' funcion para cargar la informacion sin filtros para el PopUp de "importar" en facturacion basica
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllListBasicBillingForImport() As List(Of ViewListBasicBillingForImportXpo)
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).BillingService.GetCollectionAsList(Of ViewListBasicBillingForImportXpo)
    End Function

    ''' <summary>
    ''' Get The advance balance in diferente currency
    ''' </summary>
    ''' <param name="portfolioAdvanceId"></param>
    ''' <param name="toCurrencyId"></param>
    ''' <param name="dateTRM"></param>
    ''' <param name="moduleTRM"></param>
    ''' <returns></returns>
    Public Async Function GetBalanceAdvanceByCurrency(portfolioAdvanceId As Integer,
                                                Optional toCurrencyId As Integer? = Nothing,
                                                Optional dateTRM As Date? = Nothing,
                                                Optional moduleTRM As EModuleTRM = EModuleTRM.CommonTRM) As Task(Of ActionResult(Of PortfolioAdvance))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetBalanceAdvanceByCurrencyAsync(portfolioAdvanceId, toCurrencyId, dateTRM, moduleTRM)
    End Function

    ''' <summary>
    ''' Get PortfolioAdvance By ThirdParty 
    ''' </summary>
    ''' <param name="thirdParty"></param>
    ''' <returns></returns>
    Public Async Function GetPortfolioAdvanceByThirdParty(thirdParty As Integer) As Task(Of XPCollection)

        Return Await Task.Factory.StartNew(Function() As XPCollection
                                               Return XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PortfolioService.GetCollectionPortfolioAdvanceByThirdPartyIdAndAdmission(thirdParty, Nothing)
                                           End Function)
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
