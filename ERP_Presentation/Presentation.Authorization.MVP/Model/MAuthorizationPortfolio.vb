'***********************************************************************
' Assembly         : Presentacion.Authorization.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/04/2020
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

Public Class MAuthorizationPortfolio
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
    Public Async Function GetAuthorizationPortfolio(ByVal code As String) As Task(Of ActionResult(Of AuthorizationPortfolio))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAuthorization.GetAuthorizationPortfolioAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un grupo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAuthorizationPortfolioById(ByVal id As Integer) As Task(Of AuthorizationPortfolio)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAuthorization.GetAuthorizationPortfolioByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un grupo
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAuthorizationPortfolio(ByVal record As AuthorizationPortfolio, ByVal idSequense As Int64) As Task(Of ActionResult(Of AuthorizationPortfolio))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAuthorization.SaveAuthorizationPortfolioAsync(record, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteAuthorizationPortfolio(ByVal record As AuthorizationPortfolio) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAuthorization.DeleteAuthorizationPortfolioAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of AuthorizationPortfolio))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAuthorization.ChangeStateAuthorizationPortfolioAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    Public Async Function CopyAndPasteAuthorizationPortfolioCareCenter(data As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of AuthorizationPortfolioCareCenter), List(Of Tuple(Of String, Integer))))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAuthorization.SP_CopyAndPasteAuthorizationPortfolioCareCenterAsync(data)
    End Function

    Public Async Function CopyAndPasteAuthorizationPortfolioCUPSEntity(data As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of AuthorizationPortfolioCUPSEntity), List(Of Tuple(Of String, Integer))))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAuthorization.SP_CopyAndPasteAuthorizationPortfolioCUPSEntityAsync(data)
    End Function

    Public Async Function CopyAndPasteAuthorizationPortfolioInventoryProduct(data As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of AuthorizationPortfolioInventoryProduct), List(Of Tuple(Of String, Integer))))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAuthorization.SP_CopyAndPasteAuthorizationPortfolioInventoryProductAsync(data)
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
