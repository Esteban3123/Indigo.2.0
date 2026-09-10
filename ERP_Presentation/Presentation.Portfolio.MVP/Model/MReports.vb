#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Presentation.CloudAgent
Imports System.Data
Imports System.Text

#End Region

Public Class MReports
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal tag As String)
        _tagForm = tag
        _indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    Public Async Function GetListReportCircularAccountsReceivable(filters As Dictionary(Of String, String)) As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetListReportCircularAccountsReceivableAsync(filters, Me._indigo)
    End Function

    Public Async Function GenerateFileCircularAccountsReceivable(filters As Dictionary(Of String, String)) As Task(Of ActionResult(Of StringBuilder))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GenerateFileCircularAccountsReceivableAsync(filters, Me._indigo)
    End Function

    Public Async Function GetListReportRadicateInvoice(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String)) As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetListReportRadicateInvoiceAsync(criterias, filters, Me._indigo)
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

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class
