'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan F. Tamayo
' Created          : 2013-05-07
'
' Last Modified By : Rafael eduardo Patiño
' Last Modified On : 2013-06-18
' Description      : Modelo del frontal de registro de objeciones
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Glosas
Imports Domain.Base.Entities
Imports  Domain.Entities

#End Region

''' <summary>
''' Modelo del frontal de registro de objeciones
''' </summary>
Public Class MRegisterObjection
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de sesion
    ''' </summary>
    Private _indigoSessionValues As SessionValues = SessionValues.Instance

#End Region

#Region "Methods"


    ''' <summary>
    ''' Funcion Para Listar el  Movimiento Glosa
    ''' </summary>
    ''' <param name="invoiceDetailId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListMovementGlosa(ByVal invoiceDetailId As Long) As Task(Of List(Of GlosaMovementGlosa))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListMovementGlosaAsync(invoiceDetailId.ToString(), Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Graba un registro de objecion
    ''' </summary>
    ''' <param name="ListMovementGlosa">Lista de Objeto registro de objecion a grabar</param>
    ''' <returns>Un resultado de la accion</returns>
    Public Async Function SaveMovementGlosa(ByVal ListMovementGlosa As List(Of GlosaMovementGlosa)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveMovementGlosaAsync(ListMovementGlosa, Me._indigoSessionValues)
    End Function


    ''' <summary>
    ''' Graba un registro de objecion
    ''' </summary>
    ''' <param name="ListMovementGlosa">Lista de Objeto registro de objecion a grabar</param>
    ''' <returns>Un resultado de la accion</returns>
    Public Async Function SaveReiterationMovementGlosa(ByVal ListMovementGlosa As List(Of GlosaMovementGlosa)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveReiterationMovementGlosaAsync(ListMovementGlosa, Me._indigoSessionValues)
    End Function


    ''' <summary>
    ''' Elimina un registro de objecion
    ''' </summary>
    ''' <param name="obj">Objeto registro de objecion a eliminar</param>
    ''' <returns>Un resultado de la accion</returns>
    Public Async Function DeleteMovementGlosa(ByVal obj As GlosaMovementGlosa) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.DeleteMovementGlosaAsync(obj, Me._indigoSessionValues)
    End Function

    ' ''' <summary>
    ' ''' Lista todos los conceptos generales
    ' ''' </summary>
    ' ''' <returns>Lista de conceptos generales</returns>
    ''Public Async Function ListGeneralConceptsAll() As Task(Of List(Of GeneralConceptsAll))
    ''    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListGeneralConceptAllAsync(Me._indigoSessionValues)
    ''End Function

    ' ''' <summary>
    ' ''' Lista todos los conceptos especificos
    ' ''' </summary>
    ' ''' <returns>Lista de conceptos especificos</returns>
    ''Public Async Function ListSpecificConceptsAll(ByVal idGeneralConcept As Integer) As Task(Of List(Of SpecificConceptsAll))
    ''    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListSpecificConceptAllAsync(Me._indigoSessionValues, idGeneralConcept)
    ''End Function

    ' ''' <summary>
    ' ''' Lista todos los conceptos detallado
    ' ''' </summary>
    ' ''' <returns>Lista de conceptos detallado</returns>
    ''Public Async Function ListDetailedConceptsAll(ByVal idSpecificConcept As Integer) As Task(Of List(Of DetailedConceptsAll))
    ''    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListDetailedConceptAllAsync(Me._indigoSessionValues, idSpecificConcept)
    ''End Function

    ''' <summary>
    ''' Lista todos los responsables
    ''' </summary>
    ''' <returns>Lista responsables</returns>
    Public Async Function ListResponsiblesAll() As Task(Of List(Of ResponsibleAll))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListResponsibleAllAsync(Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Listar los campos nulos de la base de datos que se permiten personalizar
    ''' </summary>
    ''' <returns>Un conjunto de datos con los campos marcados como nulos</returns>
    Public Async Function GetNullFields() As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULLAsync("RegisterObjection", Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para Cargar Los Detalles De Factura por medio de la fatura
    ''' </summary>
    ''' <param name="InvoiceNUmber">numero de factura</param>
    ''' <returns>lista detalle de facturas</returns>
    Public Function ListGlosaInvoiceDetailByInvoiceNumber(ByVal InvoiceNUmber As String, ByVal Modulo As String) As List(Of GlosaInvoiceDetail)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListGlosaInvoiceDetailByInvoiceNumber(InvoiceNUmber, Modulo, 0, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion que lista los movimineto glosa por el codigo de detalle de factura
    ''' </summary>
    ''' <param name="InvoiceDetailId">codigo detalle de factura</param>
    '''   <param name="InvoiceDetailQXId">codigo detalle de factura QX</param>
    ''' <returns>lista de moviminetos</returns>
    Public Async Function ListMovementGlosaQx(InvoiceDetailId As String, InvoiceDetailQXId As String) As Task(Of List(Of GlosaMovementGlosa))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListMovementGlosaQxAsync(InvoiceDetailId, InvoiceDetailQXId, Me._indigoSessionValues)
    End Function


    ' ''' <summary>
    ' ''' Lista todos los conceptos Glosa
    ' ''' </summary>
    ' ''' <returns>Lista Todos los conceptos glosa</returns>
    ''Public Async Function ListConceptGlosa(type As String) As Task(Of List(Of ConceptGlosa))
    ''    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListConceptGlosaAsync(type)
    ''End Function


    ''' <summary>
    ''' Obtiene una lista de conceptos de evaluación
    ''' </summary>
    ''' <param name="Type">Tipo Concepto</param>
    ''' <returns>Lista de Conceptos</returns>
    Public Async Function ListConceptsGlosa(ByVal Type As String) As Task(Of List(Of Domain.Entities.ConceptGlosas))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListConceptGlosaByTypeAsync(Type, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para Actualizar datos de una reiteracion durante el proceso de eliminarmovimineto de reiteracion
    ''' </summary>
    ''' <param name="MovementGlosa">movimiento de reiteracion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function UpdateMovementReiteration(ByVal MovementGlosa As GlosaMovementGlosa) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.UpdateMovementReiterationAsync(MovementGlosa, Me._indigoSessionValues)
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
