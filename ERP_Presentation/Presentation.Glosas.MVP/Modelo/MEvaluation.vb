'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan Diego Diaz
' Created          : 2013-07-03
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports  Domain.Entities
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Modelo del frontal de evaluación
''' </summary>
Public Class MEvaluation
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
    ''' Obtiene una lista de objeciones detalles según responsable
    ''' </summary>
    ''' <param name="CodeResponsable">Id del Responsable</param>
    ''' <returns>Lista de Objeciones Detalles</returns>
    Public Async Function ListObjectionReceptionDByResponsable(ByVal CodeResponsable As String, ByVal _idOperativeUnit As Integer) As Task(Of List(Of GlosaObjectionsReceptionD))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListObjectionReceptionDByResponsableAsync(CodeResponsable, Me._indigoSessionValues, _idOperativeUnit)
    End Function

    ''' <summary>
    ''' obtiene una lista de control de parametros de tiempo
    ''' </summary>
    ''' <param name="Invoice">Invoice</param>
    ''' <returns>Lista de Control Parametros de Tiempo</returns>
    Public Function ListControlParametersTime(ByVal Invoice As String, ByVal _idOperativeUnit As Integer) As List(Of ControlParametersTime)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListControlParametersTime(Invoice, "", _indigoSessionValues, _idOperativeUnit)
    End Function

    ''' <summary>
    ''' Obtiene una lista de detalles factura QX
    ''' </summary>
    ''' <param name="IdInvoiceDetail">Id Detalle Factura</param>
    ''' <returns>Lista de Detalles Factura QX</returns>
    Public Async Function ListGlosaInvoiceDetailQX(ByVal IdInvoiceDetail As String) As Task(Of List(Of GlosaInvoiceDetailQX))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListGlosaInvoiceDetailQXAsync(IdInvoiceDetail, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Guarda una lista de Detalles de Factura
    ''' </summary>
    ''' <param name="ListInvoiceDetail">Lista de Detalles de Factura</param>
    ''' <returns>ActionResult</returns>
    Public Async Function SaveInvoiceDetail(ByVal ListInvoiceDetail As List(Of GlosaInvoiceDetail)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveGlosaInvoiceDetailAsync(ListInvoiceDetail, Me._indigoSessionValues)
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
    ''' Funcion para obtener una lista de plantillas de justificación
    ''' </summary>
    ''' <returns></returns>
    Public Async Function listJustificationTemplate(ByVal Code As String) As Task(Of List(Of JustificationTemplate))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.listJustificationTemplateByCodeAsync(Code, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para obtener una lista de movimientos
    ''' </summary>
    ''' <returns></returns>
    Public Async Function listMovementsByInvoiceAndResponsible(ByVal Invoice As String, ByVal CodeResponsible As String) As Task(Of List(Of GlosaMovementGlosa))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListMovementsByInvoiceAndResponsibleAsync(Invoice, Me._indigoSessionValues, CodeResponsible)
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
    '   'Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
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