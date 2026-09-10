'***********************************************************************
' Assembly         : Presentation.Payments.MVP
' Author           : Johan Sebastian Cuellar Esquivel
' Created          : 14-01-2021
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

Public Class MDocumentSupport
    Implements IDisposable


#Region "Fields"

    ''' <summary>
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
    ''' Obtiene una autorizacion de Documento por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetDocumentSupportByCode(ByVal code As String) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.DocumentSupport))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetDocumentSupportByCodeAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una autorizacion de Documento por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetDocumentSupportById(ByVal id As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.DocumentSupport))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetDocumentSupportByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una autorizacion de Documento
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveDocumentSupport(ByVal record As DocumentSupport, ByVal idSequense As Int64) As Task(Of ActionResult(Of DocumentSupport))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.SaveDocumentSupportAsync(record, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina una autorizacion de Documento
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteDocumentSupport(ByVal record As DocumentSupport) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.DeleteDocumentSupportAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStateDocumentSupport(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of DocumentSupport))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.ChangeStateDocumentSupportAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    Public Async Function GetDocumentSupportResolution(ByVal operatingUnitId As Integer, ByVal record As DocumentSupport) As Task(Of ActionResult(Of DocumentSupport))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetDocumentSupportResolutionAsync(operatingUnitId, record, Me.Indigo.AuditMessageWcf)
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


    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
