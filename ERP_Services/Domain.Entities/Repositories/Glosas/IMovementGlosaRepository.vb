'************************************************************
' Assembly         : Domain.Glosas
' Author           : Juan Diego Diaz
' Created          : 24-05-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Interfaz del repositorio de Movimientos Glosas.
''' </summary>
Public Interface IMovementGlosaRepository
    Inherits IRepository(Of GlosaMovementGlosa)

    ''' <summary>
    ''' Metodo para importar los movimientos de glosa a partir de un archivo de excel
    ''' </summary>
    ''' <param name="XmlObject"></param>
    ''' <returns></returns>
    Function SP_ImportGlosaMovementGlosas(XmlObject As String) As List(Of SP_ImportGlosaMovementGlosas_Result)

    ''' <summary>
    ''' Lista todos los Movimientos de Glosas.
    ''' </summary>
    ''' <returns>Lista de Movimiento Glosas</returns>
    Function ListAllMovementGlosa() As List(Of GlosaMovementGlosa)
    ''' <summary>
    ''' Obtiene un Movimiento Glosa especifico.
    ''' </summary>
    ''' <param name="Id">El Id del Movimiento Glosa</param>
    ''' <returns>Objeto Movimiento Glosa</returns>
    Function GetMovementGlosaById(ByVal Id As String, Optional tracking As Boolean = True) As GlosaMovementGlosa
    ''' <summary>
    ''' Obtiene un movimiento de glosa por id con sus agregados
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Function GetMovementGlosaByIdWithAggregates(ByVal Id As Integer) As GlosaMovementGlosa
    ''' <summary>
    ''' Consulta un movimiento glosa especifico.
    ''' </summary>
    ''' <param name="IdDetalleFactura">El Id del Detalle Factura</param>
    ''' <param name="IdDetalleFacturaQX">El Id del Detalle Factura QX</param>
    ''' <param name="CodigoGlosa">El Id del Codigo Glosa</param>
    ''' <returns>Objeto Movimiento Glosa</returns>
    Function GetMovementGlosa(ByVal IdDetalleFactura As Integer, ByVal IdDetalleFacturaQX As Integer, ByVal CodigoGlosa As Integer) As Boolean
    ''' <summary>
    ''' Funcion que lista los movimineto glosa por el codigo de detalle de factura
    ''' </summary>
    ''' <param name="InvoiceDetailId">codigo detalle de factura</param>
    ''' <returns>lista de moviminetos</returns>
    Function ListMovementGlosa(InvoiceDetailId As String) As List(Of GlosaMovementGlosa)
    ''' <summary>
    ''' Funcion Para Cargar Moviminetos por el numero de factura
    ''' </summary>
    ''' <param name="Invoice">numero de factura</param>
    ''' <returns>lista de moviminetos</returns>
    Function ListAllMovementGlosaByInvoiceNumber(ByVal Invoice As String) As List(Of GlosaMovementGlosa)
    ''' <summary>
    ''' Funcion que lista los movimineto glosa por el codigo de detalle de factura
    ''' </summary>
    ''' <param name="InvoiceDetailId">codigo detalle de factura</param>
    '''   <param name="InvoiceDetailQXId">codigo detalle de factura QX</param>
    ''' <returns>lista de moviminetos</returns>
    Function ListMovementGlosaQx(InvoiceDetailId As String, InvoiceDetailQXId As String) As List(Of GlosaMovementGlosa)
    ''' <summary>
    ''' Funcion que lista los movimiento glosa por el codigo de detalle de factura
    ''' </summary>
    ''' <param name="InvoiceDetailId">codigo detalle de factura</param>
    '''   <param name="InvoiceDetailQXId">codigo detalle de factura QX</param>
    ''' <returns>lista de moviminetos</returns>
    Function ListMovementGlosaByCodes(InvoiceDetailId As String, InvoiceDetailQXId As String) As List(Of GlosaMovementGlosa)
    ''' <summary>
    ''' Funcion para validar que no se elimine un movimiento de una factura confirmada 
    ''' </summary>
    ''' <param name="invocieNumber">Numero de Factura</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidDeleteMovement(invocieNumber As String) As Boolean
    ''' <summary>
    ''' Funcion para validar que no se elimine datos de reiteracion si el oficio esta confirmado
    ''' </summary>
    ''' <param name="invocieNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateDeleteReiteration(invocieNumber As String) As Boolean
    ''' <summary>
    ''' Obtiene una lista de movimientos según Numero de factura y Código Responsable
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero Factura</param>
    ''' <param name="CodeResponsible">Código Responsable</param>
    ''' <returns>Lista de Movimientos</returns>
    Function ListMovementsByInvoiceAndResponsible(InvoiceNumber As String, CodeResponsible As String) As List(Of GlosaMovementGlosa)
    ''' <summary>
    ''' Validar si existen movimientos sin confirmar que no permitan confirmar una factura
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de Factura</param>
    ''' <returns>Boolean</returns>
    Function ValidateInvoicesMovements(InvoiceNumber As String) As Boolean
    ''' <summary>
    ''' Funcion que lista los movimiento glosa por el Id del Responsable
    ''' </summary>
    ''' <param name="IdResponsible">Id del Responsable</param>
    ''' <returns>lista de movimientos</returns>
    Function ListMovementGlosaByIdResponsible(IdResponsible As Integer) As List(Of GlosaMovementGlosa)
    ''' <summary>
    ''' Función que obtiene un Movimiento Glosa especifico para reasignación de responsables.
    ''' </summary>
    ''' <param name="Id">Id Movimiento Glosa</param>
    ''' <returns>Objeto Movimiento Glosa</returns>
    Function GetMovementGlosaByIdTransfer(Id As String) As GlosaMovementGlosa
    ''' <summary>
    ''' Funcion para listar movimientos de glosas para las evaluaciones masivas a nivel de facturas
    ''' </summary>
    ''' <param name="ListInvoice">Lista de Facturas</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllMovementGlosabymultipleInvoice(ByVal ListInvoice As List(Of String), Optional ByVal codeUser As String = "") As List(Of GlosaMovementGlosa)
    ''' <summary>
    ''' Funcion que lista los movimineto glosa por toda la factura
    ''' </summary>
    ''' <param name="InvoiceNumber">numero de factura</param>
    ''' <returns>lista de moviminetos</returns>
    Function ListMovementGlosaByInvoiceNumber(InvoiceNumber As String) As List(Of GlosaMovementGlosa)
    ''' <summary>
    ''' Funcion que lista los movimineto glosa por toda la factura
    ''' </summary>
    ''' <param name="InvoiceNumber">numero de factura</param>
    ''' <returns>lista de moviminetos</returns>
    Function ListMovementGlosaByInvoiceNumberWithAggregates(InvoiceNumber As String) As List(Of GlosaMovementGlosa)
    ''' <summary>
    ''' Funcion para retornar lista de objeto que totalizan y agrupa la aceptaciones de IPS en primera Instancia o glosa
    ''' </summary>
    ''' <param name="Invoicenumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListExpanDObj_AcceptedIPSFirtsInstance(ByVal Invoicenumber As String, ByVal affectsService As Boolean) As List(Of Object)
    ''' <summary>
    ''' Funcion para retornar lista de objeto que totalizan y agrupa la aceptaciones de IPS en Segunda Instancia o reiteracione
    ''' </summary>
    ''' <param name="Invoicenumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListExpanDObj_AcceptedIPSSecondInstance(ByVal Invoicenumber As String, ByVal affectsService As Boolean) As List(Of Object)
	''' <summary>
	''' Funcion para retornar lista de objeto que totalizan y agrupa la aceptaciones de IPS en Conciliacion
	''' </summary>
	''' <param name="Invoicenumber"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Function ListExpanDObj_AcceptedIPSConciliation(ByVal Invoicenumber As String, ByVal ConciliationCId As Integer, ByVal affectsService As Boolean) As List(Of Object)
	''' <summary>
	''' Funcion Para Cargar Moviminetos y conciliaciones por el numero de factura
	''' </summary>
	''' <param name="Invoice">numero de factura</param>
	''' <returns>lista de moviminetos</returns>
	Function ListAllMovementGlosaAndConciliationByInvoiceNumber(invoiceNumber As String) As List(Of GlosaMovementGlosa)

End Interface
