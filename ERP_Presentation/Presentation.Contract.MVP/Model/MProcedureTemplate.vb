'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 07/10/2014
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
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo

#End Region

Public Class MProcedureTemplate
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
    ''' Obtiene una plantilla de precedimiento por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetProcedureTemplate(ByVal code As String) As Task(Of ProcedureTemplate)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetProcedureTemplateAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Copiar y pegar para la rejilla de plantilla de procedimientos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function CopyAndPasteProcedureTemplate(data As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of ProcedureCups), List(Of Tuple(Of String, Integer))))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.CopyAndPasteProcedureTemplateAsync(data)
    End Function

    ''' <summary>
    ''' Obtiene una plantilla de procedemientos
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetProcedureTemplateById(ByVal id As Integer) As Task(Of ProcedureTemplate)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoContract.InnerChannel)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetProcedureTemplateByIdAsync(id)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o actualiza plantilla de procedemientos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveProcedureTemplate(ByVal ProcedureTemplate As ProcedureTemplate, ListProcedureCups As List(Of ProcedureCups), ListDeleteProcedureCups As List(Of ProcedureCups), ByVal idSequense As Int64) As Task(Of ActionResult(Of ProcedureTemplate))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.SaveProcedureTemplateAsync(ProcedureTemplate, ListProcedureCups, ListDeleteProcedureCups, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina una plantilla de procedemientos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteProcedureTemplate(ByVal ProcedureTemplate As ProcedureTemplate, ListProcedureCups As List(Of ProcedureCups), ListDeleteProcedureCups As List(Of ProcedureCups), company As String) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.DeleteProcedureTemplateAsync(ProcedureTemplate, ListProcedureCups, ListDeleteProcedureCups, company, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStateProcedureTemplate(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of ProcedureTemplate))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.ChangeStateProcedureTemplateAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lista todos los detalles del cubrimiento
    ''' </summary>
    ''' <param name="procedureTemplateId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListProcedureCupsByProcedureTemplateId(procedureTemplateId As Integer) As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListProcedureCupsByProcedureTemplateId(procedureTemplateId)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
