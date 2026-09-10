'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan Diego Diaz
' Created          : 2013-06-11
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

#End Region

''' <summary>
''' Modelo del frontal de devolución
''' </summary>
Public Class MDevolutions
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

    Sub New(Tag As String)
        Me._tagForm = Tag
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtiene un cliente por su Id
    ''' </summary>
    ''' <param name="id">Nit del cliente</param>
    ''' <returns>El cliente</returns>
    Public Async Function GetCustomerById(ByVal Id As String) As Task(Of Domain.Entities.Customer)
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetCustomerByIdAsync(Id, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtiene el conjunto de parametros por defecto configurado
    ''' </summary>
    ''' <returns>Objeto que encapsula el conjunto de parametros</returns>
    Public Async Function GetTimeParameters(Entity As String, ByVal _IdOperatingUnit As Integer) As Task(Of TimeParameters)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetTimeParametersSingleOrDefaultAsync(Entity, Me._indigoSessionValues, _IdOperatingUnit)
    End Function

    ''' <summary>
    ''' Obtiene una devolución aceptada por su numero de consecutivo
    ''' </summary>
    ''' <param name="consecutive">Consecutivo de la devolución</param>
    ''' <returns>La devolución</returns>
    Public Async Function GetDevolutionByConsecutive(ByVal consecutive As Long) As Task(Of GlosaDevolutionsReceptionC)
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetDevolutionCByConsecutiveAsync(consecutive.ToString(), Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para obtener las Sucursales o Sedes
    ''' </summary>
    ''' <returns></returns>
    Public Function GetBranchAll() As List(Of GlosasParametersInterface)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListInterfacesParameters(Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para obtener las facturas
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetInvoicesAll(ByVal NameContainer As String, ByVal nit As String, ByVal InvoiceNumber As String, ByVal stringSQL As String, ByVal TopString As String, ByVal FlagNotConfirmInvoice As String) As Task(Of ActionResult(Of List(Of SP_invoiceList_Result)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListAllInvoceAsync(NameContainer, nit, InvoiceNumber, Me._indigoSessionValues, stringSQL, TopString, FlagNotConfirmInvoice)
    End Function

    ''' <summary>
    ''' Obtiene la lista de facturas en el detalle de la devolución
    ''' </summary>
    ''' <param name="id">Id de la devolución</param>
    ''' <returns>El detalle de la devolución</returns>
    Public Async Function ListDetailDevolutionById(ByVal id As Integer) As Task(Of List(Of GlosaDevolutionsReceptionD))
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListDevolutionDByIdDevolutionnCAsync(id.ToString(), Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para cargar una factura
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function GetInvoice(ByVal NameContainer As String, ByVal nit As String, ByVal InvoiceNumber As String, ByVal stringSQL As String, ByVal FlagNotConfirmInvoice As String) As Task(Of SP_invoiceList_Result)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetInvoiceAsync(NameContainer, nit, InvoiceNumber, Me._indigoSessionValues, stringSQL, FlagNotConfirmInvoice)
    End Function

    ''' <summary>
    ''' Graba la cabecera de la devolución
    ''' </summary>
    ''' <param name="obj">Cabecera de devolución a grabar</param>
    ''' <returns>Objeto <see cref="Domain.Base.Entities.ActionResult"></see> que contiene el resultado de la operacion</returns>
    Public Async Function SaveDevolutionC(ByVal obj As GlosaDevolutionsReceptionC, ByVal list As List(Of GlosaDevolutionsReceptionD)) As Task(Of ActionResult(Of GlosaDevolutionsReceptionC))
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveDevolutionCAsync(obj, list, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Graba los movimientos devolución de la factura
    ''' </summary>
    ''' <param name="obj">Movimiento Devolución</param>
    ''' <returns>ActionResult<see cref="Domain.Base.Entities.ActionResult"></see> que contiene el resultado de la operacion</returns>
    Public Async Function SaveMovementDevolution(ByVal obj As GlosaMovementDevolutions) As Task(Of ActionResult(Of GlosaMovementDevolutions))
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveMovementDevolutionAsync(obj, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Confirma los movimientos devolución de la factura
    ''' </summary>
    ''' <param name="listDevolutionD">lista de factura de Devolución</param>
    ''' <returns>ActionResult<see cref="Domain.Base.Entities.ActionResult"></see> que contiene el resultado de la operacion</returns>
    Public Async Function ConfirmMovementDevolution(ByVal listDevolutionD As List(Of GlosaDevolutionsReceptionD), ByVal idSequence As Integer, ByVal Injustificate As Boolean, ByVal UserFreeInvoice As Boolean) As Task(Of ActionResult)
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ConfirmDevolutionAsync(listDevolutionD, idSequence, Injustificate, UserFreeInvoice, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Confirma la devolución
    ''' </summary>
    ''' <param name="obj">Devolución cabecera</param>
    ''' <returns>ActionResult<see cref="Domain.Base.Entities.ActionResult"></see> que contiene el resultado de la operacion</returns>
    Public Async Function ConfirmDevolution(ByVal obj As GlosaDevolutionsReceptionC) As Task(Of ActionResult)
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ConfirmDevolutionCAsync(obj, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para cargar un movimiento de devolución según numero factura
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetMovementDevolution(ByVal InvoiceNumber As String, ByVal IdDevolutionD As String) As Object
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetMovementDevolutionByInvoiceNumber(InvoiceNumber, IdDevolutionD, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtiene una lista de conceptos de evaluación
    ''' </summary>
    ''' <param name="Type">Tipo Concepto</param>
    ''' <returns>Lista de Conceptos</returns>
    Public Async Function ListConceptsGlosa(ByVal Type As String) As Task(Of List(Of Domain.Entities.ConceptGlosas))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListConceptGlosaByTypeAsync(Type, SessionValues.Instance)
    End Function

    ''' <summary>
    ''' obtiene una lista de conceptos por tipos
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <returns></returns>
    Public Async Function ListConceptsGlosaByTypes(ByVal Type As List(Of String)) As Task(Of List(Of Domain.Entities.ConceptGlosas))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListConceptGlosaByListTypesAsync(Type, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtiene un cliente por su nit
    ''' </summary>
    ''' <param name="nit">Nit del cliente</param>
    ''' <returns>El cliente</returns>
    Public Async Function GetCustomerByNit(ByVal nit As String) As Task(Of Domain.Entities.Customer)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetCustomerAsync(nit.Trim(), Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Listar los campos nulos de la base de datos que se permiten personalizar
    ''' </summary>
    ''' <returns>Un conjunto de datos con los campos marcados como nulos</returns>
    Public Async Function GetNullFields() As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULLAsync("GlosaDevolutionsReceptionC", Me._indigoSessionValues)
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
    ''' Función para validar y agregar facturas desde el sp
    ''' </summary>
    ''' <param name="ListInvoices">Lista de facturas</param>
    ''' <param name="Nit">Numero de Nit</param>
    ''' <param name="container">nombre Contenedor</param>
    ''' <returns>Lista de Factura </returns>
    Public Async Function ValidateListInvoiceDevolutionSp(ListInvoices As List(Of String), Nit As String, container As String) As Task(Of ActionResult(Of List(Of GlosaDevolutionsReceptionD)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ValidateListInvoiceDevolutionSpAsync(ListInvoices, Nit, container, Me._indigoSessionValues)
    End Function



    ''' <summary>
    ''' eliminar lista de facturas de devoluciones
    ''' </summary>
    ''' <param name="tmpList"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteListDevolution(tmpList As List(Of GlosaDevolutionsReceptionD)) As Task(Of Domain.Base.Entities.ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.DeleteListDevolutionAsync(tmpList, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' elimnar una devolucion
    ''' </summary>
    ''' <param name="GlosaDevolutionsReceptionD"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function deleteDevolutionD(GlosaDevolutionsReceptionD As GlosaDevolutionsReceptionD) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.DeleteDevolutionAsync(GlosaDevolutionsReceptionD, Me._indigoSessionValues)
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
