'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan F. Tamayo
' Created          : 2013-04-19
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-04-19
' Description      : Modelo del frontal de conciliación
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
Imports DevExpress.Data.Linq

#End Region

''' <summary>
''' Modelo del frontal de conciliación
''' </summary>
Public Class MConciliation
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de sesion
    ''' </summary>
    Private _indigoSessionValues As SessionValues = SessionValues.Instance

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
    ''' Lista los conceptos de Aceptacion 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListResponseHierarchy() As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).GlosasService.ListResponseHierarchy()
    End Function

    ''' <summary>
    ''' Obtiene la relacion entre proveedor y linea de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetResponseHierarchyById(Id As Integer) As GlosasRepository.Glosas_GlosasResponseHierarchy
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).GlosasService.GetCollection(Of GlosasRepository.Glosas_GlosasResponseHierarchy)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene un listado de facturas válidas a partir del nit del tercero y de un listado
    ''' de faturas sacado de la clipboard
    ''' </summary>
    ''' <param name="nit">Nit del tercero</param>
    ''' <param name="list">Listado de facturas sacado de la clipboard</param>
    ''' <returns>Un listado de objetos <see cref=" Domain.Entities.ConciliationD" /></returns>
    Public Async Function ListInvoiceByNitAndListData(ByVal nit As String, ByVal list As List(Of String)) As Task(Of List(Of Domain.Entities.ConciliationD))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ValidateListInvoiceAsync(list, nit.Trim(), Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtiene una concilicacion aceptada por su numero de consecutivo
    ''' </summary>
    ''' <param name="consecutive">Consecutivo de la conciliacion</param>
    ''' <returns>La conciliacion</returns>
    Public Async Function GetConciliationByConsecutive(ByVal consecutive As Long) As Task(Of ConciliationC)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetConciliationCByConsecutiveAsync(consecutive.ToString(), Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtiene la lista de facturas en el detalle de la conciliacion
    ''' </summary>
    ''' <param name="id">Id de la conciliacion</param>
    ''' <returns>El detalle de la conciliacion</returns>
    Public Async Function ListDetailConciliationById(ByVal id As Integer) As Task(Of List(Of ConciliationD))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListConciliationDByIdConciliationCAsync(id.ToString(), Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtiene la lista de facturas disponibles para seleccionar por nit de tercero
    ''' </summary>
    ''' <param name="nit">Nit del tercero</param>
    ''' <returns>Lista de facturas a seleccionar</returns>
    Public Async Function ListInvoicesByNit(ByVal nit As String) As Task(Of List(Of GlosaPortfolioGlosada))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListConfirmGlosaPortfolioAsync(nit.Trim(), Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtiene una factura por su numero y nit del cliente
    ''' </summary>
    ''' <param name="nit">Nit del cliente</param>
    ''' <param name="invoice">Numero de la factura</param>
    ''' <param name="listStatus">lista de estados a excluir al consultar la factura</param>
    ''' <returns>La factura de cartera</returns>
    Public Async Function GetInvoiceByNumber(ByVal nit As String, ByVal invoice As String, ByVal listStatus As List(Of String)) As Task(Of GlosaPortfolioGlosada)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListInvoicesByNumberAsync(invoice.Trim(), nit.Trim(), Me._indigoSessionValues, listStatus)
    End Function

    ''' <summary>
    ''' Obtiene la fecha del servidor
    ''' </summary>
    ''' <returns>La factura de cartera</returns>
    Public Async Function GetServerDate() As Task(Of DateTime)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetServerDateAsync()
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
    ''' Graba la cabecera de la conciliacion
    ''' </summary>
    ''' <param name="obj">Cabecera de conciliacion a grabar</param>
    ''' <returns>Objeto <see cref="Domain.Base.Entities.ActionResult"></see> que contiene el resultado de la operacion</returns>
    Public Async Function SaveConciliationC(ByVal obj As ConciliationC) As Task(Of ActionResult(Of ConciliationC))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveConciliationCAsync(obj, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Graba el detalle de la conciliacion
    ''' </summary>
    ''' <param name="obj">Detalle de conciliacion a grabar</param>
    ''' <returns>Objeto <see cref="Domain.Base.Entities.ActionResult"></see> que contiene el resultado de la operacion</returns>
    Public Async Function SaveConciliationD(ByVal obj As List(Of ConciliationD)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveConciliationDAsync(obj, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Graba los cambios en los movimientos del detalle a conciliar
    ''' </summary>
    ''' <param name="obj">Movimientos de detalle a grabar</param>
    ''' <returns>Objeto <see cref="Domain.Base.Entities.ActionResult"></see> que contiene el resultado de la operacion</returns>
    Public Async Function SaveConciliationInvoiceDetails(ByVal obj As List(Of GlosaMovementGlosa)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveConciliationInvoiceDetailAsync(obj, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Confirma los cambios en los movimientos del detalle a conciliar
    ''' </summary>
    ''' <param name="obj">Movimientos de detalle a confirmar</param>
    ''' <returns>Objeto <see cref="Domain.Base.Entities.ActionResult"></see> que contiene el resultado de la operacion</returns>
    Public Async Function ConfirmConciliationInvoiceDetails(ByVal obj As List(Of GlosaMovementGlosa)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ConfirmConciliationInvoiceDetailAsync(obj, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Confirma los cambios en los detalles de la factura a conciliar
    ''' </summary>
    ''' <param name="obj">Numero de factura a confirmar</param>
    ''' <returns>Objeto <see cref="Domain.Base.Entities.ActionResult"></see> que contiene el resultado de la operacion</returns>
    Public Async Function ConfirmConciliationInvoice(conciliationId As Integer, ByVal obj As String, _IdUnitoperating As Integer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ConfirmConciliationInvoiceAsync(conciliationId, obj, _IdUnitoperating, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Listar los campos nulos de la base de datos que se permiten personalizar
    ''' </summary>
    ''' <returns>Un conjunto de datos con los campos marcados como nulos</returns>
    Public Async Function GetNullFields() As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULLAsync("ConciliationC", Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Lista los detalles de la factura por su numero
    ''' </summary>
    ''' <param name="invoiceNumber">Numero de la factura a listar</param>
    ''' <returns>Una lista de objetos <see cref=" Domain.Entities.GlosaInvoiceDetail" /></returns>
    Public Async Function ListGlosaInvoiceDetails(ByVal invoiceNumber As String, ByVal Modulo As String, conciliationId As Integer) As Task(Of List(Of Domain.Entities.GlosaInvoiceDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListGlosaInvoiceDetailByInvoiceNumberAsync(invoiceNumber, Modulo, conciliationId, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Lista los detalles de la factura por su numero
    ''' </summary>
    ''' <param name="invoiceNumber">Numero de la factura a listar</param>
    ''' <returns>lista de detalle de factura</returns>
    ''' <remarks></remarks>
    Public Async Function ListGlosaInvoiceDetailForGeneralConciliation(ByVal invoiceNumber As String) As Task(Of List(Of Domain.Entities.GlosaInvoiceDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListGlosaInvoiceDetailForGeneralConciliationAsync(invoiceNumber, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtiene una lista de facturas por nit, de la cartera de la glosa
    ''' </summary>
    ''' <param name="nit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListXpoInvoicesByNitGlosaPortfolio(ByVal nit As String) As Object
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).GlosasService.GetXPOPortfolioGlosa(nit.Trim())
    End Function

    ''' <summary>
    ''' Obtiene una lista de facturas por el nit usando las entidades XPO
    ''' </summary>
    ''' <param name="nit">Nit a consultar</param>
    ''' <returns>Un objeto <see cref="DevExpress.Xpo.XPInstantFeedbackSource" /></returns>
    Public Function ListXpoInvoicesByNit(ByVal nit As String) As Object
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).GlosasService.ListPortfolio_AccountReceivable(nit.Trim())
    End Function


    ''' <summary>
    ''' Graba el detalle de la conciliacion
    ''' </summary>
    ''' <param name="mov">Detalle de conciliacion a grabar</param>
    ''' <returns>Objeto <see cref="Domain.Base.Entities.ActionResult"></see> que contiene el resultado de la operacion</returns>
    Public Async Function SaveConciliationDetail(ByVal mov As List(Of GlosaMovementGlosa)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveConciliationInvoiceDetailAsync(mov, Me._indigoSessionValues)
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
    ''' Funcion para validar y cargar los datos del excel de conciliacion
    ''' </summary>
    ''' <param name="dtset"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ValidateExcelDataConciliation(ByVal dtset As DataSet, ByVal ConciliationC As ConciliationC) As Task(Of ActionResult(Of ConciliationC))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ValidateExcelDataConciliationAsync(dtset, ConciliationC, Me._indigoSessionValues)
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
