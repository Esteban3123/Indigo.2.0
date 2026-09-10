#Region "Librerias Importadas"

Imports System.Data
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Presentation.CloudAgent

#End Region

''' <summary>
''' Modelo que se comunica con los servicios corresporndientes al funcional
''' </summary>
Public Class MPortfolioConciliation
    Implements IDisposable

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#Region "Builders"

    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

    Sub New(Tag As String)
        _tagForm = Tag
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene la secuencia numerica asignada al formulario
    ''' </summary>
    ''' <returns>Secuencia numerica</returns>
    Public Async Function GetSequense() As Task(Of Domain.Entities.PortfolioSequence)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetSequenseByIdFormAsync(Me._tagForm)
    End Function

    ''' <summary>
    ''' Funcion para obtener la recepcion de la objecion
    ''' </summary>
    ''' <param name="Consecutive">El Consecutivo.</param>
    ''' <returns></returns>
    Public Async Function GetConciliationByConsecutive(ByVal Consecutive As String) As Task(Of Object)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetConciliationByConsecutiveAsync(Consecutive)
    End Function

    ''' <summary>
    ''' funcion para traer datos del sp de trazabilidad
    ''' </summary>
    ''' <param name="conatiner"></param>
    ''' <param name="invoiceNumber"></param>
    ''' <returns></returns>
    Public Async Function GetInvoiceTraceability(conatiner As String, invoiceNumber As String) As Task(Of SP_InvoiceTraceability_Result)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SP_InvoiceTraceabilityAsync(conatiner, invoiceNumber, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para validar y cargar los datos del excel de glosas
    ''' </summary>
    ''' <param name="dtset"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ValidateExcelData(ByVal dtset As DataSet) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.ValidateExcelDataAsync(dtset, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetListConciliationDetail(ByVal ConciliationId As Integer) As Task(Of List(Of PortfolioConciliationDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetListConciliationDetailAsync(ConciliationId, Me.Indigo)
    End Function

    ''' <summary>
    ''' lista los movimientos de la factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllExtractAccountReceivableByDocumentNumber(documentNumber As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.GetAllExtractAccountReceivableByDocumentNumber(documentNumber)
    End Function

    ''' <summary>
    ''' Funcion para cargra una factura
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function GetInvoice(ByVal NameContainer As String, ByVal nit As String, ByVal InvoiceNumber As String, ByVal stringSQl As String, ByVal FlagNotConfirmInvoice As String) As Task(Of SP_invoiceList_Result)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetInvoiceAsync(NameContainer, nit, InvoiceNumber, stringSQl, FlagNotConfirmInvoice, Me.Indigo)
    End Function

    ''' <summary>
    ''' Obtiene un cliente por su nit
    ''' </summary>
    ''' <param name="nit">Nit del cliente</param>
    ''' <returns>El cliente</returns>
    Public Async Function GetCustomerByNit(ByVal nit As String) As Task(Of Domain.Entities.Customer)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetCustomerByNitAsync(nit.Trim(), Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para Guardar la recepcion de objeciones
    ''' </summary>
    ''' <returns></returns>
    Public Async Function SavePortfolioConciliation(ByVal Record As PortfolioConciliation, ByVal idSequense As Int64) As Task(Of ActionResult(Of PortfolioConciliation))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.SavePortfolioConciliationAsync(Record, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' funcion para retornar datos de una factura
    ''' </summary>
    ''' <param name="InvoiceNumber"></param>
    ''' <returns></returns>
    Public Function ListInvoicePortfolioFilter(ByVal InvoiceNumber As String) As List(Of PortfolioAccountReceivableXpo)
        Dim filter As String = "InvoiceNumber = '" & InvoiceNumber & "'"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.GetCollection(Of PortfolioAccountReceivableXpo)(Nothing, filter)
    End Function

    ''' <summary>
    ''' Funcion para obtener los campos que permiten nulos
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetNullFields() As Task(Of DataSet)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULLAsync("PortfolioConciliation", Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para guardar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecord(ByVal Record As Domain.Entities.BlockRecord) As Task(Of ActionResult(Of Domain.Entities.BlockRecord))
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveBlockRecordAsync(Record, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para eliminar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeleteBlockRecord(ByVal Record As Domain.Entities.BlockRecord) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteBlockRecordAsync(Record, Me.Indigo)
    End Function

    ''' <summary>
    ''' Obtener registro bloqueado
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <returns></returns>
    Public Async Function GetBlockRecord(ByVal IdForm As String, ByVal IdRecord As String) As Task(Of Domain.Entities.BlockRecord)
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetBlockRecordByIdformAndIdRecordAsync(IdForm, IdRecord, Me.Indigo)
    End Function


    Public Async Function GetSP_PortfolioConciliation(InvoiceNumber As String, ClosingDate As Date) As Task(Of SP_PortfolioConciliation_Result)
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetSP_PortfolioConciliationAsync(InvoiceNumber, ClosingDate, Me.Indigo)
    End Function
#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: eliminar estado administrado (objetos administrados).
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

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class