'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan F. Tamayo
' Created          : 2013-07-10
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-07-10
' Description      : Modelo del frontal de parametros de glosas
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Glosas
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.Data.Xpo

#End Region

''' <summary>
''' Modelo del frontal de parametros de glosas
''' </summary>
Public Class MTimeParameters
    Implements IDisposable

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
        _tagForm = Tag
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un concepto de nota
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetConceptPrevious(ByVal id As Integer) As Task(Of PortfolioNoteConcept)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioNoteConceptByIdAsync(id)
    End Function

    ''' <summary>
    ''' Lista los concepto por tipo 1 Glosa    2 Devoluciones     3 Tramite
    ''' </summary>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListConceptGlosaByType(ByVal type As String) As Task(Of List(Of Domain.Entities.ConceptGlosas))
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListConceptGlosaByTypeAsync(type, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtiene el conjunto de parametros por defecto configurado
    ''' </summary>
    ''' <returns>Objeto que encapsula el conjunto de parametros</returns>
    Public Async Function GetTimeParameters(Entity As String, ByVal _IdIOperatingUnit As Integer) As Task(Of TimeParameters)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetTimeParametersSingleOrDefaultAsync(Entity, Me._indigoSessionValues, _IdIOperatingUnit)
    End Function

    ''' <summary>
    ''' Obtiene una lista de parametros configurados a entidades
    ''' </summary>
    ''' <returns>Lista de parametros de tiempo</returns> 
    Public Async Function ListTimeParameters(ByVal _IdIOperatingUnit As Integer) As Task(Of List(Of TimeParameters))
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListAllTimeParametersAsync(Me._indigoSessionValues, _IdIOperatingUnit, "1")
    End Function

    ''' <summary>
    ''' Graba o actualiza una lista de parametros de tiempo
    ''' </summary>
    ''' <param name="listParameters">Lista de parametros de tiempo</param>
    ''' <returns>Resultado de la accion</returns>
    Public Async Function SaveListTimeParameters(ByVal ListParameters As List(Of TimeParameters), ByVal ListSave As List(Of Domain.Entities.ConceptGlosas)) As Task(Of ActionResult(Of List(Of TimeParameters)))
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveListTimeParametersAsync(ListParameters, ListSave, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Eliminar un objeto de parametros de tiempo
    ''' </summary>
    ''' <param name="timeParameters">Objeto de parametros de tiempo</param>
    ''' <returns>Resultado de la accion</returns>
    Public Async Function DeleteTimeParameters(ByVal timeParameters As TimeParameters) As Task(Of ActionResult)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.DeleteTimeParametersAsync(timeParameters, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Graba o actualiza el conjunto de parametros de tiempo
    ''' </summary>
    ''' <param name="obj">Conjunto de parametros de tiempo</param>
    ''' <returns>Resultado de la accion</returns>
    Public Async Function SaveTimeParameters(ByVal obj As TimeParameters) As Task(Of ActionResult(Of TimeParameters))
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveTimeParametersAsync(obj, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para guardar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecord(ByVal Record As Domain.Entities.BlockRecord) As Task(Of ActionResult(Of Domain.Entities.BlockRecord))
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveBlockRecordAsync(Record, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para eliminar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeleteBlockRecord(ByVal Record As Domain.Entities.BlockRecord) As Task(Of ActionResult)
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteBlockRecordAsync(Record, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtener registro bloqueado
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <returns></returns>
    Public Async Function GetBlockRecord(ByVal IdForm As String, ByVal IdRecord As String) As Task(Of Domain.Entities.BlockRecord)
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetBlockRecordByIdformAndIdRecordAsync(IdForm, IdRecord, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtiene un cliente por su nit
    ''' </summary>
    ''' <param name="nit">Nit del cliente</param>
    ''' <returns>El cliente</returns>
    Public Async Function GetCustomerByNit(ByVal nit As String) As Task(Of Domain.Entities.Customer)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetCustomerAsync(nit.Trim(), Me._indigoSessionValues)
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
