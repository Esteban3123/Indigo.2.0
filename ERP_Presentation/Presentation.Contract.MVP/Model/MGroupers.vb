'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Presentation.CloudAgent

#End Region

Public Class MGroupers
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
    ''' Obtiene un rango uvr por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetGroupers(ByVal code As String) As Task(Of Domain.Base.Entities.ActionResult(Of Groupers))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetGroupersAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un rango uvr por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetGroupersById(ByVal id As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of Groupers))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetGroupersByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Copiar y pegar para la rejilla de plantilla de procedimientos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function CopyAndPasteGroupersCups(data As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of GroupersCups)))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.CopyAndPasteGroupersCupsAsync(data)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un rango uvr
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveGroupers(ByVal record As Groupers, ByVal idSequense As Int64) As Task(Of ActionResult(Of Groupers))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.SaveGroupersAsync(record, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un rango uvr
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteGroupers(ByVal record As Groupers) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.DeleteGroupersAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

    Public Function ListAllGroupers() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).ContractService.ListAllGroupers()
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of Groupers))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.ChangeStateGroupersAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    Public Function ListAllGroupersCollection() As XPCollection(Of GroupersXpo)
        Return XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).ContractService.ListGroupersCollection()
    End Function

    Public Function InitializeGrouperWithOut(code As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).ContractService.InitializeGrouperWithOut(code)
    End Function

    Public Function ImportGroupers(ByVal workSheetType As Infrastructure.CrossCutting.Base.eGrouperWorkSheetType, ByVal data As List(Of ImportFileRow)) As ActionResult(Of List(Of String))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoContract.ImportGroupers(workSheetType, data, Me.Indigo.AuditMessageWcf)
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
