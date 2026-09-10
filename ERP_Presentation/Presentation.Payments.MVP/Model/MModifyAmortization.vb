'***********************************************************************
' Assembly         : Presentacion.Payments.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/03/2015
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
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo

#End Region

''' <summary>
''' Modelo de conexion con los servicios distribuidos de la corporacion
''' </summary>
Public Class MModifyAmortization
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

#Region "Methods"

#Region "QueryPopupForm"

    ''' <summary>
    ''' Lista los detalles de un folio o una factura
    ''' </summary>
    Public Function ListAccountPayableByIdSupplierAndStateWithDeferredCausation(ByVal supplierId As Integer) As LinqInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.ListAccountPayableByIdSupplierAndStateWithDeferredCausation(supplierId, 2)
    End Function

#End Region

    ''' <summary>
    ''' Obtiene el listado de cuotas de causacion que tiene asociado la cxp
    ''' </summary>
    ''' <param name="accountPayableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetDeferredCausationShareByAccountPayableId(accountPayableId As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of List(Of DeferredCausationShare)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetDeferredCausationShareByAccountPayableIdAsync(accountPayableId)
    End Function

    ''' <summary>
    ''' Actualiza los valores de las cuotas de la causacion diferida
    ''' </summary>
    ''' <param name="ListDeferredCausationShare"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveListDeferredCausationShare(ByVal ListDeferredCausationShare As List(Of DeferredCausationShare)) As Task(Of Domain.Base.Entities.ActionResult(Of List(Of DeferredCausationShare)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.SaveListDeferredCausationShareAsync(ListDeferredCausationShare, Me.Indigo.AuditMessageWcf)
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
