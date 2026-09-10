'***********************************************************************
' Assembly         : Presentacion.Payments.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/04/2014
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

#End Region

''' <summary>
''' Modelo de conexion con los servicios distribuidos de la corporacion
''' </summary>
Public Class MConceptsAccountsPayable
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

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPaymentConceptAsync(ByVal code As String) As Task(Of ActionResult(Of AccountPayableConcepts))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetPaymentConceptAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentConceptById(ByVal id As Integer) As ActionResult(Of AccountPayableConcepts)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetPaymentConceptById(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListAllPaymentConceptAsync() As Task(Of List(Of AccountPayableConcepts))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.ListAllPaymentConceptAsync(Indigo, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un concepto de pago
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SavePaymentConceptAsync(ByVal record As AccountPayableConcepts, ByVal idSequense As Int64) As Task(Of ActionResult(Of AccountPayableConcepts))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.SavePaymentConceptAsync(record, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of AccountPayableConcepts))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.ChangeStatePaymentConceptAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un concepto de pago
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeletePaymentConceptAsync(ByVal record As AccountPayableConcepts) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.DeletePaymentConceptAsync(record, Me.Indigo.AuditMessageWcf)
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
