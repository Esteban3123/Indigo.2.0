'***********************************************************************
' Assembly         : Presentacion.Payments.MVP
' Author           : Diego Andres Roldan Lozano
' Created          : 13-08-2014
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

Public Class MAgesPayment
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
    ''' guarda una edad de pagos
    ''' </summary>
    ''' <returns></returns>
    Public Async Function SaveAgesPayment(ByVal agesPayment As AgesPayments) As Task(Of ActionResult(Of AgesPayments))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.SaveAgesPaymentAsync(agesPayment, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' elimina una edad de pagos
    ''' </summary>
    ''' <returns></returns>
    Public Async Function DeleteAgesPayment(ByVal agesPayment As AgesPayments) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.DeleteAgesPaymentAsync(agesPayment, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una edad de pagos
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetAgesPaymentsById(ByVal Id As Integer) As Task(Of AgesPayments)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetAgesPaymentsByIdAsync(Id)
    End Function

    ''' <summary>
    ''' Lists the ages payment.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ListAgesPayment() As Task(Of ActionResult(Of List(Of AgesPayments)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.ListAgesPaymentsAsync()
    End Function

    ''' <summary>
    ''' Lists the ages payment.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAgesPaymentSimple() As ActionResult(Of List(Of AgesPayments))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayments.ListAgesPayments()
    End Function

    ''' <summary>
    ''' Lists the ages payment by unit operative identifier.
    ''' </summary>
    ''' <param name="UnitOperativeId">The unit operative identifier.</param>
    ''' <returns></returns>
    Public Async Function ListAgesPaymentByUnitOperativeId(ByVal UnitOperativeId As Integer) As Task(Of ActionResult(Of List(Of AgesPayments)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.ListAgesPaymentsByUnitOperativeIdAsync(UnitOperativeId)
    End Function

    ''' <summary>
    ''' Lists the ages payment by unit operative identifier simple.
    ''' </summary>
    ''' <param name="UnitOperativeId">The unit operative identifier.</param>
    ''' <returns></returns>
    Public Function ListAgesPaymentByUnitOperativeIdSimple(ByVal UnitOperativeId As Integer) As ActionResult(Of List(Of AgesPayments))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayments.ListAgesPaymentsByUnitOperativeId(UnitOperativeId)
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