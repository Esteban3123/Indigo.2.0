'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/10/2014
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

Public Class MContractEntity
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
    ''' Obtiene una entidad de contrato por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetContractEntity(ByVal code As String) As Task(Of Domain.Base.Entities.ActionResult(Of ContractEntity))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetContractEntityAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una entidad de contrato por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetContractEntityById(ByVal id As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of ContractEntity))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetContractEntityByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una entidad de contrato
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveContractEntity(ByVal record As ContractEntity, ByVal idSequense As Int64) As Task(Of ActionResult(Of ContractEntity))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.SaveContractEntityAsync(record, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina una entidad de contrato
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteContractEntity(ByVal record As ContractEntity) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.DeleteContractEntityAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of ContractEntity))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.ChangeStateContractEntityAsync(code, state, Me.Indigo.AuditMessageWcf)
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
