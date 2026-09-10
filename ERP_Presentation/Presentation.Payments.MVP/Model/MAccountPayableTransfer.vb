'***********************************************************************
' Assembly         : Presentacion.Payments.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/03/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Infrastructure.Data.Xpo
Imports DevExpress.Data.PLinq

#End Region

''' <summary>
''' Modelo de conexion con los servicios distribuidos de la corporacion
''' </summary>
Public Class MAccountPayableTransfer
    Implements IDisposable

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#Region "QueryPopup"

    ''' <summary>
    ''' Lista los detalles de un folio o una factura
    ''' </summary>
    Public Function ListAccountPayableBySupplierIdAndStatusForTransfer(ByVal supplierId As Integer, ByVal filingUnitId As Integer) As PLinqServerModeSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.ListAccountPayableBySupplierIdAndStatusForTransfer(supplierId, filingUnitId)
    End Function

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un traslado de factura
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAccountPayableTransfer(ByVal code As String) As Task(Of ActionResult(Of AccountPayableTransfer))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetAccountPayableTransferAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un traslado de factura por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAccountPayableTransferById(ByVal id As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of AccountPayableTransfer))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetAccountPayableTransferByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un traslado de factura
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAccountPayableTransfer(ByVal record As AccountPayableTransfer, ByVal idSequense As Int64) As Task(Of ActionResult(Of AccountPayableTransfer))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.SaveAccountPayableTransferAsync(record, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un traslado de factura
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function AnnularAccountPayableTransfer(ByVal record As AccountPayableTransfer) As Task(Of ActionResult(Of AccountPayableTransfer))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.AnnularAccountPayableTransferAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina una unidad de radicacion
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteAccountPayableTransfer(ByVal record As AccountPayableTransfer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.DeleteAccountPayableTransferAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

#End Region

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
