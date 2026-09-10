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
Public Interface IGlosasPortfolioGlosada


    ''' <summary>
    ''' funcion que retorna una cartyera glosada, para validar la existencia de una factura en un oficio
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de factura</param>
    ''' <returns>Una cartera Glosada</returns>
    <OperationContract()>
    Function GetPortfolioGlosada(InvoiceNumber As String, session As sessionValues) As GlosaPortfolioGlosada
    ''' <summary>
    ''' Función para validar y agregar facturas
    ''' </summary>
    ''' <param name="ListInvoices">Lista de facturas</param>
    ''' <param name="Nit">Numero de Nit</param>
    ''' <returns>Lista de Conciliacion Detalle</returns>
    <OperationContract()>
    Function ValidateListInvoice(ListInvoices As List(Of String), Nit As String, session As SessionValues) As List(Of ConciliationD)

    ''' <summary>
    ''' Funcion para Listar las facturas que esta Lista para ser conciliadas
    ''' </summary>
    ''' <param name="Nit">entidad de la facturas</param>
    ''' <param name="DateInicial">fecha inicial</param>
    ''' <param name="DateEND">fecha final</param>
    ''' <returns>lista de facturas a conciliar</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListGlosaPortfolioExportExcel(Nit As String, ByVal DateInicial As Date, ByVal DateEND As Date, session As SessionValues) As List(Of GlosaPortfolioGlosada)

    ''' <summary>
    ''' Asignación de causa de inoportunidad
    ''' </summary>
    ''' <param name="listGlosaPortfolioGlosada"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GlosaPortfolioAssignImportunityCause(listGlosaPortfolioGlosada As List(Of GlosaPortfolioGlosada), session As SessionValues) As ActionResult(Of List(Of GlosaPortfolioGlosada))

End Interface
