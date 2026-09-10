'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Juan Carlos Bermudez Gutierrez 
' Created          : 23/04/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class MBillingAuthorization
    Implements IDisposable


#Region "Fields"

    '' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builder"

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

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una autorizacion de factura por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetBillingAuthorizationByCode(ByVal code As String) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingAuthorization))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetBillingAuthorizationByCodeAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una autorizacion de factura por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetBillingAuthorizationById(ByVal id As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingAuthorization))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetBillingAuthorizationByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una autorizacion de factura
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveBillingAuthorization(ByVal record As BillingAuthorization, ByVal idSequense As Int64) As Task(Of ActionResult(Of BillingAuthorization))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SaveBillingAuthorizationAsync(record, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina una autorizacion de factura
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBillingAuthorization(ByVal record As BillingAuthorization) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.DeleteBillingAuthorizationAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStateBillingAuthorization(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of BillingAuthorization))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ChangeStateBillingAuthorizationAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    Public Async Function GetBillingAuthorizationResolution(ByVal operatingUnitId As Integer, ByVal record As BillingAuthorization) As Task(Of ActionResult(Of BillingAuthorization))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetBillingAuthorizationResolutionAsync(operatingUnitId, record, Me.Indigo.AuditMessageWcf)
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
