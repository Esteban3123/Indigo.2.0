'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Cristhian Mauricio Salazar
' Created          : 21/09/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.PortfolioRepository


Public Class MInvoiceTraceability
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

    ''' <summary>
    ''' lista los movimientos de la factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllExtractAccountReceivableByDocumentNumber(documentNumber As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.GetAllExtractAccountReceivableByDocumentNumber(documentNumber)
    End Function

    ''' <summary>
    ''' lista los movimientos de la factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllInvoiceCustomerRetention(documentNumber As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.GetAllInvoiceCustomerRetention(documentNumber)
    End Function

    ''' <summary>
    ''' Funcion para obtener las sucursales o sedes
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetBranchAll() As Task(Of List(Of Domain.Entities.GlosasParametersInterface))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListInterfacesParametersAsync(Me.Indigo)
    End Function


    Public Async Function GetInvoiceTraceability(conatiner As String, invoiceNumber As String) As Task(Of SP_InvoiceTraceability_Result)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SP_InvoiceTraceabilityAsync(conatiner, invoiceNumber, Me.Indigo)
    End Function

    Public Async Function GetInvoiceTraceabilityConciliation(invoiceNumber As String) As Task(Of List(Of SP_InvoiceTraceabilityConciliation_Result))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SP_InvoiceTraceabilityConciliationAsync(invoiceNumber, Me.Indigo)
    End Function

    Public Async Function GetInvoiceTraceabilityDevolution(invoiceNumber As String) As Task(Of List(Of SP_InvoiceTraceabilityDevolution_Result))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SP_InvoiceTraceabilityDevolutionAsync(invoiceNumber, Me.Indigo)
    End Function

    Public Async Function GetInvoiceTraceabilityRadication(invoiceNumber As String) As Task(Of List(Of SP_InvoiceTraceabilityRadication_Result))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SP_InvoiceTraceabilityRadicationAsync(invoiceNumber, Me.Indigo)
    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
