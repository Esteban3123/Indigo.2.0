'***********************************************************************
' Assembly         : Presentacion.Payments.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/04/2014
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

Public Class MAdvancePayments
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
    ''' Guarda o Actualiza un anticipo
    ''' </summary>
    ''' <returns></returns>
    Public Async Function SaveMoneyAdvance(ByVal moenyAdvance As AdvancePayments, ByVal idSequense As Int64) As Task(Of ActionResult(Of AdvancePayments))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.SaveMoneyAdavanceAsync(moenyAdvance, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un anticipo
    ''' </summary>
    ''' <returns></returns>
    Public Async Function DeleteMoneyAdvance(ByVal moneyAdvance As AdvancePayments) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.DeleteMoneyAdvanceAsync(moneyAdvance, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una determinada dependencia
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetMoneyAdvance(ByVal code As String) As Task(Of AdvancePayments)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetMoneyAdvanceAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una determinada dependencia
    ''' </summary>
    ''' <param name="id">The code.</param>
    ''' <returns></returns>
    Public Async Function GetMoneyAdvanceById(ByVal id As Integer) As Task(Of AdvancePayments)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetMoneyAdvanceByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un saldo inicial
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAdvancePaymentsById(ByVal id As Integer) As AdvancePayments
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetMoneyAdvanceById(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lista todos los avances por tercero
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ListAdvancePaymentByThirdId(ByVal ThirdId As Integer) As Task(Of ActionResult(Of List(Of AdvancePayments)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.ListAdvancePaymentByThirdIdAsync(ThirdId)
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
