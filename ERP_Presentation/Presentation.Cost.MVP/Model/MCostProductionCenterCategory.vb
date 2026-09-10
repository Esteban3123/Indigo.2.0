'***********************************************************************
' Assembly         : Presentacion.Cost.MVP
' Author           : Miguel Angel Fonseca Castro
' Created          : 2019-09-26
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports System.ServiceModel
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo

#End Region

''' <summary>
''' Se encarga de establecer comunicación con los servicios de conceptos de retencion
''' </summary>
Public Class MCostProductionCenterCategory
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Private Indigo As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me.Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Consulta un indicio de deterioro por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns>Tipo de documento consultado</returns>
    Public Async Function GetCostProductionCenterCategoryAsync(ByVal code As String) As Task(Of CostProductionCenterCategory)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetCostProductionCenterCategoryByCodeAsync(code)
    End Function

    ''' <summary>
    ''' Lista todos los indices de deterioro
    ''' </summary>
    Public Async Function ListAllCostProductionCenterCategoryAsync() As Task(Of List(Of CostProductionCenterCategory))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.ListAllCostProductionCenterCategoryAsync(Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda un indicio de deterioro
    ''' </summary>
    ''' <param name="CostProductionCenterCategory"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    Public Async Function SaveCostProductionCenterCategory(ByVal CostProductionCenterCategory As CostProductionCenterCategory, ByVal idSequense As Int64) As Task(Of ActionResult(Of CostProductionCenterCategory))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.SaveCostProductionCenterCategoryAsync(CostProductionCenterCategory, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' cambiar estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Async Function CostProductionCenterCategoryChangeState(code As String, state As Boolean) As Task(Of ActionResult(Of CostProductionCenterCategory))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.CostProductionCenterCategoryChangeStateAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un indicio de deterioro
    ''' </summary>
    ''' <param name="CostProductionCenterCategory"></param>
    ''' <returns></returns>
    Public Async Function DeleteCostProductionCenterCategory(ByVal CostProductionCenterCategory As CostProductionCenterCategory) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.DeleteCostProductionCenterCategoryAsync(CostProductionCenterCategory, Me.Indigo.AuditMessageWcf)
    End Function

    Public Function ListCategories() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CostService.ListCostProductionCenterCategoryByStatus(True)
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
