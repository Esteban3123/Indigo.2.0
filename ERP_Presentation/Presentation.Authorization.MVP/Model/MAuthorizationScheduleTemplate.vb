'***********************************************************************
' Assembly         : Presentacion.Authorization.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/03/2020
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

Public Class MAuthorizationScheduleTemplate
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
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAuthorizationScheduleTemplate(ByVal code As String) As Task(Of AuthorizationScheduleTemplate)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAuthorization.GetAuthorizationScheduleTemplateAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un grupo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAuthorizationScheduleTemplateById(ByVal id As Integer) As Task(Of AuthorizationScheduleTemplate)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAuthorization.GetAuthorizationScheduleTemplateByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un grupo
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAuthorizationScheduleTemplate(ByVal record As AuthorizationScheduleTemplate, ByVal idSequense As Int64) As Task(Of ActionResult(Of AuthorizationScheduleTemplate))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAuthorization.SaveAuthorizationScheduleTemplateAsync(record, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteAuthorizationScheduleTemplate(ByVal record As AuthorizationScheduleTemplate) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAuthorization.DeleteAuthorizationScheduleTemplateAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of AuthorizationScheduleTemplate))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAuthorization.ChangeStateAuthorizationScheduleTemplateAsync(code, state, Me.Indigo.AuditMessageWcf)
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
