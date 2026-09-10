#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class MConciliationConcepts
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
    ''' Consulta la causa de devolución por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetConciliationConceptsByCode(ByVal code As String) As Task(Of ActionResult(Of PortfolioConciliationConcepts))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioConciliationConceptsByCodeAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Consulta la causa de devolución por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetConciliationConceptsById(ByVal id As Integer) As Task(Of ActionResult(Of PortfolioConciliationConcepts))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioConciliationConceptsByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una causa de devolución
    ''' </summary>
    ''' <param name="ConciliationConcepts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveConciliationConcepts(ByVal ConciliationConcepts As PortfolioConciliationConcepts, ByVal sequenseId As Int64) As Task(Of ActionResult(Of PortfolioConciliationConcepts))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.SavePortfolioConciliationConceptsAsync(ConciliationConcepts, Me.Indigo.AuditMessageWcf, sequenseId)
    End Function

    ''' <summary>
    ''' Cambia el estado de una causa de devolución
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStateConciliationConcepts(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of PortfolioConciliationConcepts))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.ChangeStatePortfolioConciliationConceptsAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina una causa de devolución
    ''' </summary>
    ''' <param name="ConciliationConcepts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteConciliationConcepts(ByVal ConciliationConcepts As PortfolioConciliationConcepts) As Task(Of ActionResult(Of PortfolioConciliationConcepts))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.DeletePortfolioConciliationConceptsAsync(ConciliationConcepts, Me.Indigo.AuditMessageWcf)
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
