'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
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

Public Class MPortfolioInitialBalance
    Implements IDisposable

#Region "fields"
    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String
#End Region

#Region "Builder"
    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal tag As String)
        _tagForm = tag
        _indigoSessionValues = SessionValues.Instance
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' metodo para obtener el saldo inicial por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPortfolioInitialBalanceByCode(code As String) As Task(Of PortfolioInitialBalance)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioInitialBalanceByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' metodo para obtener el saldo inicial por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPortfolioInitialBalanceByIdAsync(id As Integer) As Task(Of PortfolioInitialBalance)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioInitialBalanceByIdAsync(id)
        End Using
    End Function
    ''' <summary>
    ''' metodo para obtener el saldo inicial por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPortfolioInitialBalanceById(id As Integer) As PortfolioInitialBalance
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioInitialBalanceById(id)
    End Function
    ''' <summary>
    ''' metodo para obtener las facturas del saldo inicial por id del saldo inicial
    ''' </summary>
    ''' <param name="idPortfolioInitialBalance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPortfolioInitialBalanceAccountReceivableByIdPortfolioInitialBalance(idPortfolioInitialBalance As Integer) As List(Of PortfolioInitialBalanceAccountReceivable)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioInitialBalanceAccountReceivableByIdPortfolioInitialBalance(idPortfolioInitialBalance)
    End Function
    ''' <summary>
    ''' metodo para obtener los anticipos del saldo inicial por id del saldo inicial
    ''' </summary>
    ''' <param name="idPortfolioInitialBalance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPortfolioInitialBalanceAdvanceByIdPortfolioInitialBalance(idPortfolioInitialBalance As Integer) As List(Of PortfolioInitialBalanceAdvance)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioInitialBalanceAdvanceByIdPortfolioInitialBalance(idPortfolioInitialBalance)
    End Function
    ''' <summary>
    ''' guardar saldo inicial
    ''' </summary>
    ''' <param name="PortfolioInitialBalance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SavePortfolioInitialBalance(PortfolioInitialBalance As PortfolioInitialBalance, idSequence As Integer) As Task(Of ActionResult(Of PortfolioInitialBalance))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.SavePortfolioInitialBalanceAsync(PortfolioInitialBalance, idSequence, Me._indigoSessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' confirmar el saldo incial
    ''' </summary>
    ''' <param name="idPortfolioInitialBalance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ConfirmPortfolioInitialBalance(idPortfolioInitialBalance As Integer) As Task(Of ActionResult(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.ConfirmPortfolioInitialBalanceAsync(idPortfolioInitialBalance, Me._indigoSessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' metodo para guardar y confirmar el saldo inicial
    ''' </summary>
    ''' <param name="PortfolioInitialBalance"></param>
    ''' <returns></returns>
    Public Async Function SaveAndConfirmPortfolioInitialBalance(PortfolioInitialBalance As PortfolioInitialBalance, idSequence As Integer) As Task(Of ActionResult(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.SaveAndConfirmPortfolioInitialBalanceAsync(PortfolioInitialBalance, idSequence, Me._indigoSessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Establece las facturas que se pegaron en la rejilla
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SetBillsCopyPaste(data As List(Of List(Of String)), companyType As Integer) As Task(Of ActionResult(Of List(Of PortfolioInitialBalanceAccountReceivable)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.SetBillsCopyPasteAsync(data, companyType)
    End Function

    Public Function ValidateFileBillsInitialBalance(data As List(Of ImportFileRow), companyType As Integer) As ActionResult(Of List(Of PortfolioInitialBalanceAccountReceivable))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.ValidateFileBillsInitialBalance(data, companyType)
    End Function
    ''' <summary>
    ''' Establece los anticipos que se pegaron en la rejilla
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SetAdvancesCopyPaste(data As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of PortfolioInitialBalanceAdvance)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.SetAdvanceCopyPasteAsync(data)
    End Function

    ''' <summary>
    ''' lista los conceptos de notas por tipo de nota
    ''' </summary>
    ''' <param name="noteType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListPortfolioNoteConceptByNoteType(noteType As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PortfolioService.ListPortfolioNoteConceptByNoteType(noteType, True)
    End Function

    ''' <summary>
    ''' lista las categorias de factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListInvoiceCategories() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).BillingService.ListInvoiceCategories()
    End Function




    ''' <summary>
    ''' lista los detalles del saldo inicial de las cuentas
    ''' </summary>
    ''' <param name="portfolioInitialBalanceAccountReceivableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListPortfolioInitialBalanceAccountReceivableAccounting(portfolioInitialBalanceAccountReceivableId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PortfolioService.ListPortfolioInitialBalanceAccountReceivableAccounting(portfolioInitialBalanceAccountReceivableId)
    End Function

    ''' <summary>
    ''' lista los presupuestos por tipo de rubro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBudgetByCategoryItemType() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).BudgetService.ListBudgetByCategoryItemType(1)
    End Function

    ''' <summary>
    ''' lista los detalles del saldo inicial de las cuotas
    ''' </summary>
    ''' <param name="portfolioInitialBalanceAccountReceivableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListPortfolioInitialBalanceAccountReceivableShare(portfolioInitialBalanceAccountReceivableId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PortfolioService.ListPortfolioInitialBalanceAccountReceivableShare(portfolioInitialBalanceAccountReceivableId)
    End Function

    ''' <summary>
    ''' lista los detalles del saldo inicial
    ''' </summary>
    ''' <param name="PortfolioInitialBalanceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListPortfolioInitialBalanceAccountReceivable(PortfolioInitialBalanceId As Integer) As List(Of PortfolioInitialBalanceAccountReceivableXpo)
        Dim filter = "PortfolioInitialBalanceId=" & PortfolioInitialBalanceId
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PortfolioService.GetCollectionAsList(Of PortfolioInitialBalanceAccountReceivableXpo)(Nothing, filter)
    End Function


    ''' <summary>
    ''' lista los tipos de comprobates contables
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListJournalVouchersType() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.ListJournalVoucherByState(True)
    End Function

    ''' <summary>
    ''' lista las cuentas contables que no manejan centro de costo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAccountsCostCenter() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.ListAccountsCostCenter(False)
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
