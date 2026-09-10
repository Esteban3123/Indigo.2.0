'***********************************************************************
' Assembly         : Presentacion.Payments.MVP
' Author           : Rafael Eduardo Patiño
' Created          : 16/03/2015
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
Imports DevExpress.Data.Linq

#End Region

''' <summary>
''' Modelo de conexion con los servicios distribuidos de la corporacion
''' </summary>
Public Class MAcceptanceTransfer
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
        Return XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).PaymentsService.ListAccountPayableBySupplierIdAndStatusForTransfer(supplierId, filingUnitId)
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
    ''' Lista las oficios de traslado pendiente a los que tiene permiso el usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayableTransferAcceptence(listFillingUnitId As List(Of Integer)) As PLinqServerModeSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.ListAccountPayableTransferAcceptence(listFillingUnitId)
    End Function
    ''' <summary>
    ''' Lista el detalle de un traslado
    ''' </summary>
    ''' <param name="_AccountPayableTransferId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPaymentsAccountPayableTransferDetail(ByVal _AccountPayableTransferId As Integer) As Object
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.ListPaymentsAccountPayableTransferDetail(_AccountPayableTransferId)
    End Function

    ''' <summary>
    ''' Lista el detalle de un traslado
    ''' </summary>
    ''' <param name="_AccountPayableTransferId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRefundAccountPayableTransferDetail(ByVal _AccountPayableTransferId As Integer) As Object
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.ListRefundTransferDetail(_AccountPayableTransferId)
    End Function
    ''' <summary>
    ''' Lista las razones de rechazo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListRejectionReason() As Task(Of List(Of AccountPayableRejectionReason))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.ListRejectionReasonAsync(Me.Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Actualizar aceptacion de traslado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAcceptanceTranfer(ListIDDetailTranfer As List(Of Integer), IDTarget As Integer, RejectionReasonID As Integer?, RejectionDescription As String) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.SaveAcceptanceTranferAsync(ListIDDetailTranfer, IDTarget, RejectionReasonID, RejectionDescription, Me.Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Retorna las unidades de radicacion a las que el usuario tiene permiso
    ''' </summary>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListFillingPermisoUser(codeUser As String) As Task(Of ActionResult(Of List(Of FilingUnitUser)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetFilingUnitByUserPermissionAsync(codeUser)
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
