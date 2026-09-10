'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Juan Diego Diaz
' Created          : 22-05-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Interfaz del servicio de Conciliación Detalle. 
''' </summary>
''' <remarks></remarks>
Public Interface IConciliationDAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Elimina un detalle de conciliación.
    ''' </summary>
    ''' <param name="ConciliacionD">Objeto Conciliación Detalle</param>
    ''' <returns>ActionResult</returns>
    Function DeleteConciliationD(ByVal ConciliacionD As List(Of ConciliationD), ByVal audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' Guarda un detalle de conciliación.
    ''' </summary>
    ''' <param name="ConciliacionD">Objeto Conciliación Detalle</param>
    ''' <returns>ActionResult</returns>
    Function SaveConciliationD(ByVal ConciliacionD As List(Of ConciliationD), ByVal audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' Lista todos los detalles de conciliaciones.
    ''' </summary>
    ''' <returns>Lista de objetos de Conciliación Detalle</returns>
    Function ListAllConciliationD() As List(Of ConciliationD)
    ''' <summary>
    ''' consulta un detalle de conciliacion especifico.
    ''' </summary>
    ''' <param name="Id">El Id de la conciliación cabecera</param>
    ''' <returns>Objeto Conciliación Detalle</returns>
    Function ListConciliationDByIdConciliationC(ByVal Id As String) As List(Of ConciliationD)
    ''' <summary>
    ''' consulta un detalle de conciliacion especifico.
    ''' </summary>
    ''' <param name="Id">El códigoId de la objeción detalle</param>
    ''' <returns>Objeto Conciliación Detalle</returns>
    Function GetConciliationDByIdObjectionD(ByVal Id As String) As ConciliationD

    ''' <summary>
    ''' Función que obtiene registros de facturas.
    ''' </summary>
    ''' <param name="Factura">Numero Factura</param>
    ''' <param name="Nit"></param>
    ''' <param name="listStatus"></param>
    ''' <returns>Lista Cartera Glosa</returns>
    Function ListInvoicesByNumber(Factura As String, Nit As String, ByVal listStatus As List(Of String)) As GlosaPortfolioGlosada
    ''' <summary>
    ''' obtiene una cartera glosada por Nit
    ''' </summary>
    ''' <param name="Nit">Nit de la objecion</param>
    ''' <returns>Objeto Cartera Glosa</returns>
    Function ListConfirmGlosaPortfolio(Nit As String) As List(Of GlosaPortfolioGlosada)
    ''' <summary>
    ''' Guardar Objecion en el detalle de factura
    ''' </summary>
    ''' <param name="Movimientos">Objeto Movimiento</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Action Result</returns>
    Function SaveConciliationInvoiceDetail(Movimientos As List(Of GlosaMovementGlosa), audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' Confirmar Objecion en el detalle de factura
    ''' </summary>
    ''' <param name="Movimientos">Objeto Movimiento</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Action Result</returns>
    Function ConfirmConciliationInvoiceDetail(Movimientos As List(Of GlosaMovementGlosa), audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' Confirmar factura
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero Factura</param>
    ''' <param name="IndigoSessionValues">Valores de sesion</param>
    ''' <returns>Action Result</returns>
    Function ConfirmConciliationInvoice(conciliationId As Integer, InvoiceNumber As String, _IdUnitoperating As Integer, IndigoSessionValues As SessionValues) As ActionResult

    ''' <summary>
    ''' 'Funcion para validar y subir conciliaciones desde excel
    ''' </summary>
    ''' <param name="dtSet"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateExcelDataConciliation(ByVal dtSet As DataSet, ByVal ConciliationC As ConciliationC, ByVal audit As AuditMessage) As ActionResult(Of ConciliationC)

End Interface
