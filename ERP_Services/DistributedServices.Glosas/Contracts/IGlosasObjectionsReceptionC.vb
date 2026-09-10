'***********************************************************************
' Assembly         : DistributedService.Glosas
' Author           : Rafael Eduardo Patiño
' Created          : 07-05-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()> _
Public Interface IGlosasObjectionsReceptionC

#Region "ObjectionsReceptionC"

    ''' <summary>
    ''' Guarda el oficio actualizado por el proceso de coordinacion
    ''' </summary>
    ''' <param name="ObjectionsReceptionC">Oficio a actualizar</param>
    ''' <param name="session">session</param>
    ''' <returns>Resultado de la accion</returns>
    <OperationContract()>
    Function SaveObjectionsReceptionCInCoordication(ObjectionsReceptionC As GlosaObjectionsReceptionC, ListInvocie As List(Of String), ByVal Session As SessionValues) As ActionResult

    ''' <summary>
    ''' Obtiene una objecion completa con sus agregados por su numero de Id
    ''' </summary>
    ''' <param name="id">Id de la objecion</param>
    ''' <returns>La objecion</returns>
    <OperationContract()>
    Function GetObjectionCWithAgregatesById(id As String, session As SessionValues, ByVal _IdIOperatingUnit As Integer) As GlosaObjectionsReceptionC

    ''' <summary>
    ''' Obtiene una objecion sin sus agregados por su numero de Id
    ''' </summary>
    ''' <param name="id">Id de la objecion</param>
    ''' <returns>La objecion</returns>
    <OperationContract()>
    Function GetObjectionCWithoutAgregatesById(id As Integer, ByVal _IdIOperatingUnit As Integer, session As SessionValues) As GlosaObjectionsReceptionC

    ''' <summary>
    ''' Lista las recepciones de objeciones por su estado
    ''' </summary>
    ''' <param name="status">Estado de las objeciones a listar</param>
    ''' <returns>Lista de objeciones</returns>
    <OperationContract()>
    Function ListObjectionsReceptionCByStatus(status As String, session As SessionValues) As List(Of GlosaObjectionsReceptionC)

    ''' <summary>
    ''' guarda una nueva objecion 
    ''' </summary>
    ''' <param name="ObjectionsReceptionC">una la cabecera de una objecion</param>
    ''' <param name="session">session</param>
    ''' <returns>valor de confirmacion de guardado</returns>
    <OperationContract()>
    Function SaveObjectionsReceptionC(ObjectionsReceptionC As Domain.Entities.GlosaObjectionsReceptionC, session As SessionValues) As ActionResult

    ''' <summary>
    ''' guardar los detalles de una recepcion de objeciones y persistir los detalles de las facturas
    ''' </summary>
    ''' <param name="RadicatedConsecutive">numero consecutivo de radicado</param>
    ''' <param name="ContainerName">nombre del contenedor</param>
    ''' <param name="GlosaObjectionsReceptionD">Objeto factura a guardar</param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveObjectionsReceptionDAndPersist(RadicatedConsecutive As String, ContainerName As String, GlosaObjectionsReceptionD As GlosaObjectionsReceptionD, session As SessionValues) As ActionResult

    ''' <summary>
    ''' obtiene una objecion recepcionada
    ''' </summary>
    ''' <param name="codeObjectionReceptionC">codigo de la objecion o consecutivo</param>
    ''' <returns>una objecion recepcionada</returns>
    <OperationContract()>
    Function GetObjection(ByVal codeObjectionReceptionC As String, session As SessionValues) As GlosaObjectionsReceptionC

    ''' <summary>
    ''' Lista las facturas por contenedor y nit de la entidad ademas del total de registro
    ''' </summary>
    ''' <param name="nameContainer">recibe el nombre del contenedor</param>
    ''' <param name="nit">nit del tercro a buscar facturas</param>
    ''' <param name="InvoiceNumber">Numero de factura</param>
    ''' <param name="session">valores de sesion</param>
    '''  <param name="stringSQl">cadena Sql de Filtro avanzados</param>
    ''' <param name="TopQuery">Cantidad de Registro a retornar</param>
    ''' <returns>Un objeto result que tiene una lista de facturas y el conteo de las misma</returns>
    <OperationContract()>
    Function ListAllInvoce(nameContainer As String, nit As String, InvoiceNumber As String, session As SessionValues, stringSQl As String, TopQuery As String, ByVal FlagNotConfirmInvoice As String) As ActionResult(Of List(Of SP_invoiceList_Result))


    ''' <summary>
    ''' carga una factura 
    ''' </summary>
    ''' <param name="nameContainer">recibe el nombre del contenedor</param>
    ''' <param name="nit">nit del tercro a buscar facturas</param>
    ''' <param name="InvoiceNumber">numero de factura</param>
    ''' <returns>una factura</returns>
    <OperationContract()>
    Function GetInvoice(nameContainer As String, nit As String, InvoiceNumber As String, session As SessionValues, stringSQl As String, ByVal FlagNotConfirmInvoice As String) As SP_invoiceList_Result

    ''' <summary>
    ''' Confirma  una objecion
    ''' </summary>
    ''' <param name="ObjectionsReceptionC">Registro Objecion</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>lista detalle facturas</returns>
    <OperationContract()>
    Function ConfirmObjectionsReceptionC(ObjectionsReceptionC As Domain.Entities.GlosaObjectionsReceptionC, session As SessionValues) As ActionResult

    ''' <summary>
    '''  anula una objecion
    ''' </summary>
    ''' <param name="ObjectionsReceptionC">Registro Objecion</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>lista detalle facturas</returns>
    <OperationContract()>
    Function InvalidateObjectionsReceptionC(ObjectionsReceptionC As Domain.Entities.GlosaObjectionsReceptionC, session As SessionValues) As ActionResult

    ''' <summary>
    ''' funcion que retorna la observacion y/o estado actual de como viene la factura a persistir
    ''' </summary>
    ''' <param name="code">codigo estado</param>
    ''' <returns>uan Observacion de fatura</returns>
    '<OperationContract()>
    'Function GetObservationInvoice(code As String, session As SessionValues) As ObservationInvoice

    ''' <summary>
    ''' Función para validar y agregar facturas desde el sp
    ''' </summary>
    ''' <param name="ListInvoices">Lista de facturas</param>
    ''' <param name="Nit">Numero de Nit</param>
    ''' <param name="container">nombre Contenedor</param>
    ''' <returns>Lista de Factura </returns>
    <OperationContract()>
    Function ValidateListInvoiceSp(ListInvoices As List(Of String), Nit As String, container As String, session As SessionValues) As ActionResult(Of List(Of GlosaObjectionsReceptionD))

    ''' <summary>
    ''' Funcion para cargar dataset de datos de oficio de respuesta
    ''' </summary>
    ''' <param name="Filter">filtro aplicar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function OfficeReponse(ByVal Filter As String, ByVal LevelInvoice As Boolean, session As SessionValues) As DataSet

    ''' <summary>
    ''' Funcion para realizar la exportacion de una recepcion de objeciones a excel
    ''' </summary>
    ''' <param name="IdRecepcion">Id de la recepcion</param>
    ''' <param name="session">valroe de sesion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function ReceptionExcelExport(ByVal IdRecepcion As String, session As SessionValues) As DataSet
    ''' <summary>
    ''' Funcion para realizar la exportacion de una recepcion de objeciones a excel completo.
    ''' </summary>
    ''' <param name="IdRecepcion">Id de la recepcion</param>
    ''' <param name="session">valroe de sesion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function ReceptionExcelExportFull(ByVal IdRecepcion As String, session As SessionValues) As DataTable

    ''' <summary>
    ''' Genera el archivo FUR RG (Respuesta a Glosa) de la Circular Externa 003
    ''' de 2026 de ADRES a partir de un radicado de objeciones, incluyendo
    ''' TODAS las facturas elegibles del radicado. Aplica filtros Aseguradora/
    ''' Fosyga y <c>GlosaPortfolioGlosada.State IN (11,12)</c>. Devuelve un
    ''' único <see cref="AdresClaimFile"/> con JSON y DataSet plano listo para
    ''' exportar a XLSX desde la UI.
    ''' </summary>
    <OperationContract()>
    Function GenerateAdresFurRgPlane(IdObjectionsReception As Integer,
                                     Session As SessionValues) As ActionMessageResult(Of AdresClaimFile)

    ''' <summary>
    ''' Genera el archivo FUR RG (Respuesta a Glosa) restringido al
    ''' subconjunto de facturas indicadas en <paramref name="InvoiceNumbers"/>.
    ''' Pensado para el flujo de menú contextual con multi-select en la
    ''' rejilla. Aplica los mismos filtros internos que
    ''' <see cref="GenerateAdresFurRgPlane"/>.
    ''' </summary>
    <OperationContract()>
    Function GenerateAdresFurRgPlaneByInvoices(IdObjectionsReception As Integer,
                                               InvoiceNumbers As List(Of String),
                                               Session As SessionValues) As ActionMessageResult(Of AdresClaimFile)

#End Region

End Interface
