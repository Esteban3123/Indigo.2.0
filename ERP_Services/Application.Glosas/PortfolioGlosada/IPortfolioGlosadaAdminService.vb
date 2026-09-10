'***********************************************************************
' Assembly         : Application.Glosas
' Author           : RafaelPatiño
' Created          : 30-05-2013
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Public Interface IPortfolioGlosadaAdminService
    Inherits IDisposable
    ''' <summary>
    ''' funcion que retorna una cartyera glosada, para validar la existencia de una factura en un oficio
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de factura</param>
    ''' <returns>Una cartera Glosada</returns>
    Function GetPortfolioGlosada(InvoiceNumber As String) As GlosaPortfolioGlosada
    ''' <summary>
    ''' Función para validar y agregar facturas
    ''' </summary>
    ''' <param name="ListInvoices">Lista de facturas</param>
    ''' <returns>Lista de Conciliacion Detalle</returns>
    Function ValidateListInvoice(ListInvoices As List(Of String), Nit As String) As List(Of ConciliationD)

    ''' <summary>
    ''' Funcion para Listar las facturas que esta Lista para ser conciliadas
    ''' </summary>
    ''' <param name="Nit">entidad de la facturas</param>
    ''' <param name="DateInicial">fecha inicial</param>
    ''' <param name="DateEND">fecha final</param>
    ''' <returns>lista de facturas a conciliar</returns>
    ''' <remarks></remarks>
    Function ListGlosaPortfolioExportExcel(Nit As String, ByVal DateInicial As Date, ByVal DateEND As Date) As List(Of GlosaPortfolioGlosada)

    ''' <summary>
    ''' Asignación de causa de inoportunidad
    ''' </summary>
    ''' <param name="listGlosaPortfolioGlosada"></param>
    ''' <returns></returns>
    Function GlosaPortfolioAssignImportunityCause(listGlosaPortfolioGlosada As List(Of GlosaPortfolioGlosada)) As ActionResult(Of List(Of GlosaPortfolioGlosada))

End Interface
