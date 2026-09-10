'***********************************************************************
' Assembly         : Application.Glosas
' Author           : RafaelPatiño
' Created          : 09-04-2013
'
' Last Modified By : RafaelPatiño
' Last Modified On : 2013-09-04
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IObjectionsReceptionCAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista las recepciones de objeciones por su estado
    ''' </summary>
    ''' <param name="status">Estado de las objeciones a listar</param>
    ''' <returns>Lista de objeciones</returns>
    Function ListObjectionsReceptionCByStatus(ByVal status As String) As List(Of GlosaObjectionsReceptionC)

    ''' <summary>
    ''' Lista de todas las objeciones recepcionadas
    ''' </summary>
    ''' <returns>lista de Objeciones Recepcionadas </returns>
    Function ListAllObjectionsReceptionC() As List(Of GlosaObjectionsReceptionC)

    ''' <summary>
    ''' obtener una objecion por el codigo 
    ''' </summary>
    ''' <param name="codeObjectionReceptionC">recibe el codigo de la objecion</param>
    ''' <returns>una objecion recepcionada</returns>
    Function GetObjection(ByVal codeObjectionReceptionC As String) As GlosaObjectionsReceptionC

    ''' <summary>
    ''' Obtiene una objecion completa con sus agregados por su numero de Id
    ''' </summary>
    ''' <param name="id">Id de la objecion</param>
    ''' <returns>La objecion</returns>
    Function GetObjectionCWithAgregatesById(id As String, timeParameterAdmin As ITimeParametersAdminService, _IdIOperatingUnit As Integer) As GlosaObjectionsReceptionC

    ''' <summary>
    ''' Obtiene una objecion sin sus agregados por su numero de Id
    ''' </summary>
    ''' <param name="id">Id de la objecion</param>
    ''' <returns>La objecion</returns>
    Function GetObjectionCWithoutAgregatesById(id As Integer, _IdIOperatingUnit As Integer) As GlosaObjectionsReceptionC

    ''' <summary>
    ''' guardar una objecion 
    ''' </summary>
    ''' <param name="ObjectionsReceptionC">la recepción de la objeción</param>
    ''' <param name="audit">mensaje de auditoria</param>
    ''' <returns>valor de guardado</returns>
    Function SaveObjectionsReceptionC(ByVal ObjectionsReceptionC As GlosaObjectionsReceptionC, ByVal IndigoCompany As String, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' guardar los detalles de una recepcion de objeciones y persistir los detalles de las facturas
    ''' </summary>
    ''' <param name="RadicatedConsecutive">numero consecutivo de radicado</param>
    ''' <param name="ContainerName">nombre del contenedor</param>
    ''' <param name="GlosaObjectionsReceptionD">objeto facturas a guardar</param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function SaveObjectionsReceptionDAndPersist(RadicatedConsecutive As String, ContainerName As String, GlosaObjectionsReceptionD As GlosaObjectionsReceptionD, ByVal Session As SessionValues) As ActionResult

    ''' <summary>
    ''' Guarda el oficio actualizado por el proceso de coordinacion
    ''' </summary>
    ''' <param name="ObjectionsReceptionC">Oficio a actualizar</param>
    ''' <param name="Session">variable de sesion de auditoria</param>
    ''' <returns>Resultado de la accion</returns>
    Function SaveObjectionsReceptionCInCoordication(ByVal ObjectionsReceptionC As GlosaObjectionsReceptionC, ListInvocie As List(Of String), ByVal Session As SessionValues) As ActionResult

    ''' <summary>
    ''' Lista las facturas por contenedor y nit de la entidad ademas del total de registro
    ''' </summary>
    ''' <param name="nameContainer">recibe el nombre del contenedor</param>
    ''' <param name="nit">nit del tercro a buscar facturas</param>
    ''' <param name="InvoiceNumber">Numero de factura</param>
    ''' <param name="IndigoCompany">numero de contenedor</param>
    '''  <param name="stringSQl">cadena Sql de Filtro avanzados</param>
    ''' <param name="TopQuery">Cantidad de Registro a retornar</param>
    ''' <returns>Un objeto result que tiene una lista de facturas y el conteo de las misma</returns>
    Function ListAllInvoice(nameContainer As String, nit As String, InvoiceNumber As String, IndigoCompany As String, ByVal stringSQl As String, ByVal TopQuery As String, ByVal FlagNotConfirmInvoice As String, session As SessionValues) As ActionResult(Of List(Of Domain.Entities.SP_invoiceList_Result))

    ''' <summary>
    ''' carga una factura 
    ''' </summary>
    ''' <param name="nameContainer">recibe el nombre del contenedor</param>
    ''' <param name="nit">nit del tercro a buscar facturas</param>
    ''' <param name="InvoiceNumber">numero de factura</param>
    ''' <returns>una factura</returns>
    Function GetInvoce(ByVal nameContainer As String, nit As String, InvoiceNumber As String, ByVal IndigoCompany As String, ByVal stringSQl As String, ByVal FlagNotConfirmInvoice As String, session As SessionValues) As SP_invoiceList_Result

    ''' <summary>
    ''' retorna una lista de detalles de facturas
    ''' </summary>
    ''' <param name="container">nombre de contenedor de datos</param>
    ''' <param name="invoiceNumber">numero de la factura a traer detalle</param>
    ''' <param name="ingressNumber">numero de ingreso</param>
    ''' <returns>lista detalle facturas</returns>
    Function ListInvoiceDetails(ByVal container As String, invoiceNumber As String, ingressNumber As String, session As SessionValues) As List(Of SP_invoiceDetailList_Result)

    ''' <summary>
    ''' Confirma  una objecion
    ''' </summary>
    ''' <param name="ObjectionsReceptionC">Registro Objecion</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>lista detalle facturas</returns>
    Function ConfirmObjectionsReceptionC(ObjectionsReceptionC As Domain.Entities.GlosaObjectionsReceptionC, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult

    ''' <summary>
    ''' Funcion para anular un Oficio
    ''' </summary>
    ''' <param name="ObjectionsReceptionC">Oficio</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function InvalidateObjectionsReceptionC(ObjectionsReceptionC As Domain.Entities.GlosaObjectionsReceptionC, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult

    ''' <summary>
    ''' obtiene una lista de detalle de facturas de servico quirurgicos
    ''' </summary>
    ''' <param name="container">nombre del contenedor de datos</param>
    ''' <param name="consecutiveNumber">numero consecutivo</param>
    ''' <param name="ServiceOrder">orden de servicio</param>
    ''' <returns>lista de detalle quirurgicos</returns>
    Function ListInvoiceDetailListQX(ByVal container As String, ByVal consecutiveNumber As String, ByVal ServiceOrder As String, ByVal ServiceCode As String, ByVal consecutiveOrder As String, ByVal ServiceNumber As String, ByVal ConsecutivoInventory As String, session As SessionValues) As List(Of SP_invoiceDetailListQX_Result)

    ''' <summary>
    ''' funcion que retorna la observacion y/o estado actual de como viene la factura a persistir
    ''' </summary>
    ''' <param name="code">codigo estado</param>
    ''' <returns>uan Observacion de fatura</returns>
    '  Function GetObservationInvoice(code As String) As ObservationInvoice


    ''' <summary>
    ''' Función para validar y agregar facturas desde el sp
    ''' </summary>
    ''' <param name="ListInvoices">Lista de facturas</param>
    ''' <param name="Nit">Numero de Nit</param>
    ''' <param name="container">nombre Contenedor</param>
    ''' <returns>Lista de Factura </returns>
    Function ValidateListInvoiceSp(ListInvoices As List(Of String), Nit As String, container As String, Session As SessionValues) As ActionResult(Of List(Of GlosaObjectionsReceptionD))


    ''' <summary>
    ''' Funcion para cargar dataset de datos de oficio de respuesta
    ''' </summary>
    ''' <param name="Filter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function OfficeReponse(ByVal Filter As String, ByVal LevelInvoice As Boolean, ByVal session As SessionValues) As DataSet

    ''' <summary>
    ''' Funcion para realizar la exportacion de una recepcion de objeciones a excel
    ''' </summary>
    ''' <param name="IdRecepcion">Id de la recepcion</param>
    ''' <param name="session">valroe de sesion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ReceptionExcelExport(ByVal IdRecepcion As String, session As SessionValues) As DataSet

    ''' <summary>
    ''' Funcion para realizar la exportacion de una recepcion de objeciones a excel completo
    ''' </summary>
    ''' <param name="IdRecepcion">Id de la recepcion</param>
    ''' <param name="session">valroe de sesion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ReceptionExcelExportFull(ByVal IdRecepcion As Integer, session As SessionValues) As DataTable

    ''' <summary>
    ''' Genera el archivo FUR RG (Respuesta a Glosa) de la Circular Externa 003
    ''' de 2026 de ADRES, a partir de un radicado de objeciones e incluyendo
    ''' TODAS las facturas elegibles del radicado. Aplica internamente los
    ''' filtros Aseguradora/Fosyga (CareGroupType=4 AND EntityType=11) y
    ''' <c>GlosaPortfolioGlosada.State IN (11=Glosa con Respuesta, 12=Reiteración
    ''' con Respuesta)</c>. Las facturas del radicado que no completaron el
    ''' proceso de Evaluación + Coordinación se reportan como warnings.
    ''' Devuelve un único <see cref="AdresClaimFile"/> con JSON ya serializado
    ''' y un DataSet plano listo para exportarse a XLSX desde la UI.
    ''' </summary>
    ''' <param name="IdObjectionsReception">Id de Glosas.GlosaObjectionsReceptionC.</param>
    ''' <param name="Session">Variable de sesión.</param>
    Function GenerateAdresFurRgPlane(IdObjectionsReception As Integer,
                                     Session As SessionValues) As ActionMessageResult(Of AdresClaimFile)

    ''' <summary>
    ''' Genera el archivo FUR RG (Respuesta a Glosa) a partir de un radicado
    ''' de objeciones, restringido al subconjunto de facturas indicadas en
    ''' <paramref name="InvoiceNumbers"/>. Aplica los mismos filtros internos
    ''' que <see cref="GenerateAdresFurRgPlane"/> (Aseguradora/Fosyga y
    ''' <c>State IN (11,12)</c>) y emite warning textual por cada factura
    ''' seleccionada que no completó el proceso de Evaluación + Coordinación.
    ''' Pensado para el flujo de menú contextual con multi-select en la
    ''' rejilla de <c>FrmObjectionsReception</c>.
    ''' </summary>
    ''' <param name="IdObjectionsReception">Id de Glosas.GlosaObjectionsReceptionC.</param>
    ''' <param name="InvoiceNumbers">Lista de InvoiceNumber a incluir (debe contener al menos uno).</param>
    ''' <param name="Session">Variable de sesión.</param>
    Function GenerateAdresFurRgPlaneByInvoices(IdObjectionsReception As Integer,
                                               InvoiceNumbers As List(Of String),
                                               Session As SessionValues) As ActionMessageResult(Of AdresClaimFile)


End Interface
