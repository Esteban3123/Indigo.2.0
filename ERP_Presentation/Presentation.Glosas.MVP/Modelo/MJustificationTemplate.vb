Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Glosas
Imports  Domain.Entities
Imports Domain.Base.Entities

'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan Diego Diaz M.
' Created          : 01-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

''' <summary>
''' Modelo que se comunica con los servicios correspondientes al funcional
''' </summary>
Public Class MJustificationTemplate
    Implements IDisposable

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim _indigoSessionValues As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String

#Region "Builders"

    Sub New(Tag As String)
        _tagForm = Tag
    End Sub

#End Region

    ''' <summary>
    ''' Funcion para obtener lista de conceptos de glosas.
    ''' </summary>
    ''' <param name="Codes">Lista de tipos.</param>
    ''' <returns></returns>
    Public Async Function getConcepts(ByVal Codes As List(Of String)) As Task(Of List(Of Domain.Entities.ConceptGlosas))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListConceptGlosaByListTypesAsync(Codes, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para guardar una plantilla de justificación
    ''' </summary>
    ''' <param name="JustificationTemplate">Objeto JustificationTemplate</param>
    ''' <returns>Boolean</returns>
    Public Async Function saveJustificationTemplate(ByVal JustificationTemplate As JustificationTemplate) As Task(Of ActionResult(Of JustificationTemplate))
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveJustificationTemplateAsync(JustificationTemplate, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para eliminar una plantilla de justificación
    ''' </summary>
    ''' <param name="JustificationTemplate">Objeto JustificationTemplate</param>
    ''' <returns></returns>
    Public Async Function deleteJustificationTemplate(ByVal JustificationTemplate As JustificationTemplate) As Task(Of ActionResult)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.DeleteJustificationTemplateAsync(JustificationTemplate, SessionValues.Instance)
    End Function

    ''' <summary>
    ''' Funcion para obtener una plantilla de justificación según código.
    ''' </summary>
    ''' <param name="Code">El codigo de la plantilla de justificación.</param>
    ''' <returns></returns>
    Public Async Function getJustificationTemplateByCode(ByVal Code As String) As Task(Of JustificationTemplate)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.getJustificationTemplateAsync(Code, SessionValues.Instance)
    End Function

    ''' <summary>
    ''' Funcion para obtener una lista de plantillas de justificación según concepto.
    ''' </summary>
    ''' <param name="Concepto">Concepto.</param>
    ''' <returns></returns>
    Public Async Function ListJustificationTemplateByConcept(ByVal Concepto As String) As Task(Of List(Of JustificationTemplate))
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.listJustificationTemplateByConceptAsync(Concepto, SessionValues.Instance)
    End Function

    ''' <summary>
    ''' Listar los campos nulls de la base de datos y porder customizar 
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetFieldsNULL() As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULLAsync("JustificationTemplate", Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para guardar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecord(ByVal Record As Domain.Entities.BlockRecord) As Task(Of ActionResult(Of Domain.Entities.BlockRecord))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveBlockRecordAsync(Record, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para eliminar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeleteBlockRecord(ByVal Record As Domain.Entities.BlockRecord) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteBlockRecordAsync(Record, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtener registro bloqueado
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <returns></returns>
    Public Async Function GetBlockRecord(ByVal IdForm As String, ByVal IdRecord As String) As Task(Of Domain.Entities.BlockRecord)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetBlockRecordByIdformAndIdRecordAsync(IdForm, IdRecord, Me._indigoSessionValues)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: eliminar estado administrado (objetos administrados).
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

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class



