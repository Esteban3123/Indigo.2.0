'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2015
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

Public Class MDefinitionRate
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
    ''' Obtiene una definicion de tarifa por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetDefinitionRate(ByVal code As String) As Task(Of Domain.Base.Entities.ActionResult(Of DefinitionRate))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetDefinitionRateAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una definicion de tarifa por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetDefinitionRateById(ByVal id As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of DefinitionRate))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetDefinitionRateByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene el listado de condiciones del detalle de la definicion de tarifa
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetListDefinitionRateDetailConditionByDefinitionRateDetailId(definitionRateDetailId As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of List(Of DefinitionRateDetailCondition)))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetListDefinitionRateDetailConditionByDefinitionRateDetailIdAsync(definitionRateDetailId, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una definicion de tarifa
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveDefinitionRate(ByVal record As DefinitionRate, ListDeleteDefinitionRateDetail As List(Of DefinitionRateDetail), listDeleteDefinitionRateDetailCondition As List(Of DefinitionRateDetailCondition), Company As String, ByVal idSequense As Int64) As Task(Of ActionResult(Of DefinitionRate))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.SaveDefinitionRateAsync(record, ListDeleteDefinitionRateDetail, listDeleteDefinitionRateDetailCondition, Company, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina una definicion de tarifa
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteDefinitionRate(ByVal record As DefinitionRate) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.DeleteDefinitionRateAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of DefinitionRate))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.ChangeStateDefinitionRateAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' servicio que descarga la estructura limpia de detalle de definicion de tarifas
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ExportCleanStructure() As Task(Of ActionResult(Of Byte()))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.ExportCleanStructureAsync()
    End Function


    ''' <summary>
    ''' servicio que descarga la estructura limpia de detalle de definicion de tarifas
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ImportDataExcelStructure(dataImportFile As List(Of ImportFileRow)) As Task(Of ActionResult(Of List(Of DefinitionRateDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.ImportDataToAddAsync(dataImportFile, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' servicio que descarga la estructura de detalle condicion de definicion de tarifas
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ExportConditionStructureByIdAsync(id As Integer) As Task(Of ActionResult(Of Byte()))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.ExportConditionStructureByIdAsync(id)
    End Function

    ''' <summary>
    ''' funcion para importar las condiciones de los detalles
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <returns></returns>
    Public Async Function ImportDataExcelStructureCondition(dataImportFile As List(Of ImportFileRow)) As Task(Of ActionResult(Of List(Of DefinitionRateDetailCondition)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.ImportConditionDataToAddAsync(dataImportFile, Me.Indigo.AuditMessageWcf)
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
