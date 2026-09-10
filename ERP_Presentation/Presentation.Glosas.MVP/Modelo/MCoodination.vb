'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan F. Tamayo
' Created          : 2013-07-22
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-07-22
' Description      : Modelo del frontal de coordinación
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Glosas
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.GlosasRepository

#End Region

''' <summary>
''' Modelo del frontal de coordinación
''' </summary>
Public Class MCoodination
    Implements IDisposable
    Implements IGeneralEvaluation

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de sesion
    ''' </summary>
    Private _indigoSessionValues As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Sub New(Tag As String)
        Me._tagForm = Tag
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Lista las recepciones de objeciones por su estado
    ''' </summary>
    ''' <returns>Lista de objeciones</returns>
    Public Function ListDocuments() As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).GlosasService.ListObjectionReceptionCByState()
    End Function
    ''' <summary>
    ''' Lista los conceptos de Aceptacion 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListResponseHierarchy() As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).GlosasService.ListResponseHierarchy()
    End Function
    ''' <summary>
    ''' Obtiene un concepto de aceptación por su ID
    ''' </summary>
    ''' <param name="Id">ID del concepto de aceptación</param>
    ''' <returns>El concepto de aceptación</returns>
    ''' <remarks></remarks>
    Public Function GetResponseHierarchyById(Id As Integer) As GlosasRepository.Glosas_GlosasResponseHierarchy
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).GlosasService.GetCollection(Of GlosasRepository.Glosas_GlosasResponseHierarchy)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene un oficio por su numero de identificación
    ''' </summary>
    ''' <param name="id">Numero de identificacion del oficio</param>
    ''' <returns>El oficio y sus agregados</returns>
    Public Async Function GetDocumentById(ByVal id As String, _IdOperatingUnit As Integer) As Task(Of Domain.Entities.GlosaObjectionsReceptionC)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetObjectionCWithoutAgregatesByIdAsync(id, _IdOperatingUnit, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para obtener una lista de movimientos
    ''' </summary>
    ''' <returns></returns>
    Public Async Function listMovementsByInvoiceAndResponsible(ByVal Invoice As String, ByVal CodeResponsible As String) As Task(Of List(Of GlosaMovementGlosa))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListMovementsByInvoiceAndResponsibleAsync(Invoice, Me._indigoSessionValues, CodeResponsible)
    End Function

    ''' <summary>
    ''' Obtiene una lista de conceptos de evaluación
    ''' </summary>
    ''' <param name="Type">Tipo Concepto</param>
    ''' <returns>Lista de Conceptos</returns>
    Public Async Function ListConceptsGlosa(ByVal Type As String) As Task(Of List(Of Domain.Entities.ConceptGlosas)) Implements IGeneralEvaluation.ListConceptsGlosa
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListConceptGlosaByTypeAsync(Type, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' obtiene una lista de conceptos por tipos
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <returns></returns>
    Public Async Function ListConceptsGlosaByTypes(ByVal Type As List(Of String)) As Task(Of List(Of Domain.Entities.ConceptGlosas)) Implements IGeneralEvaluation.ListConceptsGlosaByTypes
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListConceptGlosaByListTypesAsync(Type, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' consulta el concepto de glosa por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Async Function ConceptsGlosaByCode(ByVal code As String) As Task(Of Domain.Entities.ConceptGlosas)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetConceptGlosasByCodeAsync(code, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Actualiza la información de un oficio y todos sus agregados
    ''' </summary>
    ''' <param name="obj">Oficio a actualizar</param>
    ''' <returns>Resultado de la accion</returns>
    Public Async Function SaveDocument(ByVal obj As Domain.Entities.GlosaObjectionsReceptionC, ListInvocie As List(Of String)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveObjectionsReceptionCInCoordicationAsync(obj, ListInvocie, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtiene la fecha del servidor
    ''' </summary>
    ''' <returns>La factura de cartera</returns>
    Public Async Function GetServerDate() As Task(Of DateTime)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetServerDateAsync()
    End Function

    ''' <summary>
    ''' Funcion para obtener una lista de movimientos
    ''' </summary>
    ''' <returns></returns>
    Public Async Function saveMov(ByVal listmov As List(Of GlosaMovementGlosa), Optional ByVal _IdUnitoperating As Integer = 0) As Task(Of ActionResult) Implements IGeneralEvaluation.saveMov
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveMovEvaluationAsync(listmov, Me._indigoSessionValues, _IdUnitoperating)
    End Function

    ''' <summary>
    ''' Funcion para obtener una lista de plantillas de justificación
    ''' </summary>
    ''' <returns></returns>
    Public Async Function listJustificationTemplate(ByVal Code As String) As Task(Of List(Of JustificationTemplate))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.listJustificationTemplateByCodeAsync(Code, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para obtener el Responsable
    ''' </summary>
    ''' <param name="Code">El codigo del Responsable.</param>
    ''' <returns></returns>
    Public Async Function GetResponsibleByCodeERP(ByVal Code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetResponsibleByCodeERPAsync(Code, Me._indigoSessionValues)
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

    ''' <summary>
    ''' Lista de movimientos
    ''' </summary>
    ''' <param name="listInvoice"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListAllMovementGlosabymultipleInvoice(ListInvoice As List(Of String), Optional ByVal codeUser As String = "") As Task(Of List(Of GlosaMovementGlosa)) Implements IGeneralEvaluation.ListAllMovementGlosabymultipleInvoice
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListAllMovementGlosabymultipleInvoiceAsync(ListInvoice, Me._indigoSessionValues, codeUser)
    End Function

#Region "PLinqServerModeSource Coordinacion: cargue de detalle de factura"

    ''' <summary>
    ''' Lista las recepciones de objeciones por su estado
    ''' </summary>
    ''' <returns>Lista de objeciones</returns>
    Public Function ListGlosasObjectionD(ByVal IdReception As Integer) As List(Of ViewCoordinationGlosaObjectionXpo)
        Dim filter As String = "GlosaObjectionsReceptionCId = " & IdReception
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).GlosasService.GetCollection(Of ViewCoordinationGlosaObjectionXpo)(Nothing, filter).ToList()
    End Function

#End Region

    ''' <summary>
    ''' Genera la estructura de un oficio de glosa
    ''' </summary>
    ''' <param name="GlosaObjectionsReceptionCId"></param>
    ''' <returns></returns>
    Public Async Function ExportCoordinationGlosa(GlosaObjectionsReceptionCId As Integer) As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ExportCoordinationGlosaAsync(GlosaObjectionsReceptionCId, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Cargar los datos de un oficio
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Public Async Function ChargueExcelDataCoordination(ds As DataSet) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ChargueExcelDataCoordinationAsync(ds, Me._indigoSessionValues)
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
