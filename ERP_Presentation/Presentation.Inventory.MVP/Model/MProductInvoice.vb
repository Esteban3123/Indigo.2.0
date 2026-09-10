'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 28-10-2014
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
Imports Infrastructure.Data.Xpo.BillingRepository
Imports DevExpress.Data.PLinq
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports System.Dynamic
Imports Infrastructure.Data.Xpo.InventoryRepository

#End Region

Public Class MProductInvoice
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
    ''' Lista las resoluciones de facturación autorizadas para el usuario
    ''' </summary>
    ''' <returns>Lista de resoluciones</returns>
    Public Async Function ListBillingAuthorization() As Task(Of List(Of BillingAuthorization))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ListBillingAuthorizationByUserCodeAsync(_indigoSessionValues.UserIndigo)
    End Function
    ''' <summary>
    ''' Sets the purchase order import file.
    ''' </summary>
    ''' <param name="list">The list.</param>
    ''' <param name="_idOperativeUnit">The identifier operative unit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.NotImplementedException"></exception>
    Public Async Function SetPurchaseOrderImportFile(list As List(Of ImportFileRow), wareHouseId As Integer, _idOperativeUnit As Integer) As Task(Of ActionResult(Of List(Of DocumentInvoiceProductSalesDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SetProductsProductInvoiceImportFileAsync(list, wareHouseId, _idOperativeUnit, _indigoSessionValues)
    End Function
    ''' <summary>
    ''' Lista las unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFunctionalUnit() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PayrollService.ListFunctionalUnit(True)
    End Function
    ''' <summary>
    ''' lista los terceros
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCustomer() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CommonService.ListCustomerByStatus(True)
    End Function

    ''' <summary>
    ''' lista las autorizaciones de facturación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListAllBillingAuthorization() As Task(Of List(Of BillingRepository.BillingAuthorizationXpo))
        Dim filter = $"Status = True And InvoiceType In (1,2,3) And Billing_BillingAuthorizationUsers[UserCode = '{_indigoSessionValues.UserIndigo}']"
        Return Await Task.Factory.StartNew(Function() As List(Of BillingRepository.BillingAuthorizationXpo)
                                               Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).BillingService.GetCollectionAsList(Of BillingRepository.BillingAuthorizationXpo)(Nothing, filter)
                                           End Function)
    End Function

    ''' <summary>
    ''' lista los almacenes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListWarehouse() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListOwnWarehouseByStatusAndUser(True, _indigoSessionValues.UserIndigo)
    End Function
    ''' <summary>
    ''' obtiene una remision por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetDocumentInvoiceProductSalesByCode(code As String) As Task(Of DocumentInvoiceProductSales)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetDocumentInvoiceProductSalesByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' obtiene una factura por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDocumentInvoiceProductSalesById(id As Integer) As DocumentInvoiceProductSales
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetDocumentInvoiceProductSalesById(id)
    End Function
    ''' <summary>
    ''' guardar una factura
    ''' </summary>
    ''' <param name="DocumentInvoiceProductSales"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveDocumentInvoiceProductSales(DocumentInvoiceProductSales As DocumentInvoiceProductSales, idSequense As Integer, ByVal sequenceC As Domain.Entities.BillingSequence) As Task(Of ActionResult(Of DocumentInvoiceProductSales))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveDocumentInvoiceProductSalesAsync(DocumentInvoiceProductSales, _indigoSessionValues.AuditMessageWcf, idSequense, sequenceC)
    End Function
    ''' <summary>
    ''' guardar y confirmar una factura
    ''' </summary>
    ''' <param name="DocumentInvoiceProductSales"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAndConfirmDocumentInvoiceProductSales(DocumentInvoiceProductSales As DocumentInvoiceProductSales, cashReceipts As CashReceipts, idSequense As Integer, action As Integer, ByVal sequenceC As Domain.Entities.BillingSequence) As Task(Of ActionResult(Of Domain.Entities.DocumentInvoiceProductSales))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveAndConfirmbDocumentInvoiceProductSalesAsync(DocumentInvoiceProductSales, cashReceipts, idSequense, _indigoSessionValues, sequenceC, action)
    End Function
    ''' <summary>
    ''' Reversa un documento de factura de productos
    ''' </summary>
    ''' <param name="DocumentInvoiceProductSales">The document invoice product sales.</param>
    ''' <param name="reverseReasonId">The reverse reason identifier.</param>
    ''' <param name="reverseDescription">The reverse description.</param>
    ''' <returns></returns>
    Public Async Function ReverseDocumentInvoiceProductSales(DocumentInvoiceProductSales As DocumentInvoiceProductSales, reverseReasonId As Integer, reverseDescription As String) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ReverseDocumentInvoiceProductSalesAsync(DocumentInvoiceProductSales, reverseReasonId, reverseDescription, _indigoSessionValues)
    End Function
    ''' <summary>
    ''' lista los detalles de la factura
    ''' </summary>
    ''' <param name="DocumentInvoiceProductSalesId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDocumentInvoiceProductSalesDetailByDocumentInvoiceProductSalesId(DocumentInvoiceProductSalesId As Integer) As List(Of DocumentInvoiceProductSalesDetail)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetDocumentInvoiceProductSalesDetailByDocumentInvoiceProductSalesId(DocumentInvoiceProductSalesId)
    End Function

    ''' <summary>
    ''' servicio para importacion de remisiones de salida
    ''' </summary>
    ''' <param name="ListIds"></param>
    ''' <param name="ContractExternalClientId"></param>
    ''' <param name="FunctionalUnitId"></param>
    ''' <param name="DocumentInvoiceProductSalesId"></param>
    ''' <returns></returns>
    Public Async Function ImportRemissionOutput(ListIds As List(Of Integer), ContractExternalClientId As Integer, FunctionalUnitId As Integer, Optional DocumentInvoiceProductSalesId As Integer? = Nothing) As Task(Of ActionResult(Of List(Of DocumentInvoiceProductSalesDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ImportRemissionOutputAsync(ListIds, ContractExternalClientId, FunctionalUnitId, DocumentInvoiceProductSalesId)
    End Function

    ''' <summary>
    ''' calcula el valor de un producto deacuerdo a la nueva tarificacion, desde centro de atencion externo MS
    ''' </summary>
    ''' <param name="ListDocumentInvoiceProductSalesDetail"></param>
    ''' <param name="ContractExternalClientId"></param>
    ''' <param name="FunctionalUnitId"></param>
    ''' <returns></returns>
    Public Function CalculateNewRate(ListDocumentInvoiceProductSalesDetail As List(Of DocumentInvoiceProductSalesDetail), ContractExternalClientId As Integer, FunctionalUnitId As Integer) As ActionResult(Of List(Of DocumentInvoiceProductSalesDetail))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.CalculateRateProduct(ListDocumentInvoiceProductSalesDetail, ContractExternalClientId, FunctionalUnitId)
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
