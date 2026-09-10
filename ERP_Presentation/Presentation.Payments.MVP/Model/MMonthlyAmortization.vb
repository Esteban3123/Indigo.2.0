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
Public Class MMonthlyAmortization
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
    ''' Obtiene un listado de causaciones diferidas por fecha
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetDeferredCausationByDate(year As Integer, month As Integer) As Task(Of ActionResult(Of List(Of DeferredCausationShare)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetDeferredCausationByDateAsync(year, month, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una causacion diferida por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDeferredCausationById(id As String) As ActionResult(Of DeferredCausation)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetDeferredCausationById(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Confirma la amortizacion mensual
    ''' </summary>
    ''' <param name="listDeferredCausationShare"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ConfirmMonthlyAmortization(ByVal listDeferredCausationShare As List(Of DeferredCausationShare), ByVal idOperatingUnit As Integer) As Task(Of ActionResult(Of List(Of DeferredCausationShare)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.ConfirmMonthlyAmortizationAsync(listDeferredCausationShare, idOperatingUnit, Me.Indigo.AuditMessageWcf)
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
