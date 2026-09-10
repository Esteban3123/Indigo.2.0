#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class MImportunityCauses
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
    Public Async Function GetImportunityCausesByCode(ByVal code As String) As Task(Of ActionResult(Of ImportunityCauses))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetImportunityCausesByCodeAsync(code, Me.Indigo)
    End Function

    ''' <summary>
    ''' Consulta la causa de devolución por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetImportunityCausesById(ByVal id As Integer) As Task(Of ActionResult(Of ImportunityCauses))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetImportunityCausesByIdAsync(id, Me.Indigo)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una causa de devolución
    ''' </summary>
    ''' <param name="ImportunityCauses"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveImportunityCauses(ByVal ImportunityCauses As ImportunityCauses, ByVal sequenseId As Int64) As Task(Of ActionResult(Of ImportunityCauses))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveImportunityCausesAsync(ImportunityCauses, Me.Indigo, sequenseId)
    End Function

    ''' <summary>
    ''' Cambia el estado de una causa de devolución
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStateImportunityCauses(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of ImportunityCauses))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ChangeStateImportunityCausesAsync(code, state, Me.Indigo)
    End Function

    ''' <summary>
    ''' Elimina una causa de devolución
    ''' </summary>
    ''' <param name="ImportunityCauses"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteImportunityCauses(ByVal ImportunityCauses As ImportunityCauses) As Task(Of ActionResult(Of ImportunityCauses))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.DeleteImportunityCausesAsync(ImportunityCauses, Me.Indigo)
    End Function


    ''' <summary>
    ''' Funcion para eliminar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeleteBlockRecord(ByVal Record As Domain.Entities.BlockRecord) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteBlockRecordAsync(Record, Me.Indigo)
    End Function

    ''' <summary>
    ''' Obtener registro bloqueado
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <returns></returns>
    Public Async Function GetBlockRecord(ByVal IdForm As String, ByVal IdRecord As String) As Task(Of Domain.Entities.BlockRecord)
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetBlockRecordByIdformAndIdRecordAsync(IdForm, IdRecord, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para guardar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecord(ByVal Record As Domain.Entities.BlockRecord) As Task(Of ActionResult(Of Domain.Entities.BlockRecord))
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveBlockRecordAsync(Record, Me.Indigo)
    End Function

    ''' <summary>
    ''' Obtiene la secuencia numerica asignada al formulario
    ''' </summary>
    ''' <returns>Secuencia numerica</returns>
    Public Async Function GetSequense() As Task(Of Domain.Entities.GlosaSequence)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetSequenseByIdFormAsync(Me._tagForm, Indigo)
    End Function

    ''' <summary>
    ''' Lista las causas de inoportunidad
    ''' </summary>
    ''' <returns>Lista de objeciones</returns>
    Public Function ListImportunityCauses() As DevExpress.Xpo.XPInstantFeedbackSource
        Return Infrastructure.Data.Xpo.XpoServiceEx.Instance(Indigo.TransactionalContainer).GlosasService.ListImportunityCausesActives()
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
