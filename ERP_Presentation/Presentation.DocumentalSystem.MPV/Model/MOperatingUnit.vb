'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Juan Carlos Bermudez Gutierrez 
' Created          : 14/04/2015
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
'Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Presentation.Base

#End Region

Public Class MOperatingUnit
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
    ''' obtener una unidad operativa por codigo
    ''' </summary>
    ''' <param name="code">codigo de la unidad operativa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetOperatingUnitByCode(ByVal code As String) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.OperatingUnit))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Dim OriginalTransactionalContainer = Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer
        Dim TransactionalContainer As String = Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer
        If Not String.IsNullOrEmpty(Infrastructure.CrossCutting.Base.SessionValues.Instance.FoundationalContainer) Then
            If Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf IsNot Nothing Then
                If Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional IsNot Nothing Then
                    If Infrastructure.CrossCutting.Base.SessionValues.Instance.ListFormPermission IsNot Nothing Then
                        Dim vieForm = Infrastructure.CrossCutting.Base.SessionValues.Instance.ListFormPermission.FirstOrDefault(Function(f) f.Id = Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional)
                        If vieForm IsNot Nothing AndAlso vieForm.IsFoundational Then
                            TransactionalContainer = Infrastructure.CrossCutting.Base.SessionValues.Instance.FoundationalContainer
                        End If
                    End If
                    Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional = Nothing
                End If
            End If
        End If
        Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer = TransactionalContainer
        Dim result = Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetOperatingUnitByCodeAsync(code, Indigo)
        Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer = OriginalTransactionalContainer
        Return result
    End Function

    ''' <summary>
    ''' obterner una unidad operativa por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetOperatingUnitById(ByVal id As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.OperatingUnit))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoDocumentalSystem.InnerChannel)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetOperatingUnitByIdAsync(id, Indigo)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o actualiza una unida operativa
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveOperatingUnit(ByVal operatingUnit As Domain.Entities.OperatingUnit) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.OperatingUnit))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Dim OriginalTransactionalContainer = Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer
        Dim TransactionalContainer As String = Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer
        If Not String.IsNullOrEmpty(Infrastructure.CrossCutting.Base.SessionValues.Instance.FoundationalContainer) Then
            If Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf IsNot Nothing Then
                If Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional IsNot Nothing Then
                    If Infrastructure.CrossCutting.Base.SessionValues.Instance.ListFormPermission IsNot Nothing Then
                        Dim vieForm = Infrastructure.CrossCutting.Base.SessionValues.Instance.ListFormPermission.FirstOrDefault(Function(f) f.Id = Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional)
                        If vieForm IsNot Nothing AndAlso vieForm.IsFoundational Then
                            TransactionalContainer = Infrastructure.CrossCutting.Base.SessionValues.Instance.FoundationalContainer
                        End If
                    End If
                    Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional = Nothing
                End If
            End If
        End If
        Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer = TransactionalContainer
        Dim result = Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveOperatingUnitAsync(operatingUnit, Indigo)
        Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer = OriginalTransactionalContainer
        Return result
    End Function

    ''' <summary>
    ''' elimina una unidad operativa
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteOperatingUnit(ByVal operatingUnit As Domain.Entities.OperatingUnit) As Task(Of ActionResult)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoDocumentalSystem.InnerChannel)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteOperatingUnitAsync(operatingUnit, Indigo)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para guardar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecord(ByVal Record As BlockRecord) As Task(Of ActionResult(Of BlockRecord))
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveBlockRecordAsync(Record, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para eliminar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeleteBlockRecord(ByVal Record As BlockRecord) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteBlockRecordAsync(Record, Me.Indigo)
    End Function

    ''' <summary>
    ''' Obtener registro bloqueado
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <returns></returns>
    Public Async Function GetBlockRecord(ByVal IdForm As String, ByVal IdRecord As String) As Task(Of BlockRecord)
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetBlockRecordByIdformAndIdRecordAsync(IdForm, IdRecord, Me.Indigo)
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
