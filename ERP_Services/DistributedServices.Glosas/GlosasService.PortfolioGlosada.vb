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
#Region "Imports"
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.Glosas
Imports Infrastructure.CrossCutting.Base

#End Region
Partial Class GlosasService

    ''' <summary>
    ''' funcion que retorna una cartyera glosada, para validar la existencia de una factura en un oficio
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de factura</param>
    ''' <returns>Una cartera Glosada</returns>
    Public Function GetPortfolioGlosada(InvoiceNumber As String, session As SessionValues) As GlosaPortfolioGlosada Implements IGlosasService.GetPortfolioGlosada
        Using PortfolioGlosadaAdminService As IPortfolioGlosadaAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPortfolioGlosadaAdminService)()
            Return PortfolioGlosadaAdminService.GetPortfolioGlosada(InvoiceNumber)
        End Using
    End Function

    ''' <summary>
    ''' Función para validar y agregar facturas
    ''' </summary>
    ''' <param name="ListInvoices">Lista de facturas</param>
    ''' <param name="Nit">Numero de Nit</param>
    ''' <returns>Lista de Conciliacion Detalle</returns>
    Public Function ValidateListInvoice(ListInvoices As List(Of String), Nit As String, session As SessionValues) As List(Of ConciliationD) Implements IGlosasService.ValidateListInvoice
        Using PortfolioGlosadaAdminService As IPortfolioGlosadaAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPortfolioGlosadaAdminService)()
            Return PortfolioGlosadaAdminService.ValidateListInvoice(ListInvoices, Nit)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para Listar las facturas que esta Lista para ser conciliadas
    ''' </summary>
    ''' <param name="Nit">entidad de la facturas</param>
    ''' <param name="DateInicial">fecha inicial</param>
    ''' <param name="DateEND">fecha final</param>
    ''' <returns>lista de facturas a conciliar</returns>
    ''' <remarks></remarks>
    Public Function ListGlosaPortfolioExportExcel(Nit As String, DateInicial As Date, DateEND As Date, session As SessionValues) As List(Of GlosaPortfolioGlosada) Implements IGlosasService.ListGlosaPortfolioExportExcel
        Using PortfolioGlosadaAdminService As IPortfolioGlosadaAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPortfolioGlosadaAdminService)()
            Return PortfolioGlosadaAdminService.ListGlosaPortfolioExportExcel(Nit, DateInicial, DateEND)
        End Using
    End Function

    ''' <summary>
    ''' Asignación de causa de inoportunidad
    ''' </summary>
    ''' <param name="listGlosaPortfolioGlosada"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function GlosaPortfolioAssignImportunityCause(listGlosaPortfolioGlosada As List(Of GlosaPortfolioGlosada), session As SessionValues) As ActionResult(Of List(Of GlosaPortfolioGlosada)) Implements IGlosasService.GlosaPortfolioAssignImportunityCause
        Using PortfolioGlosadaAdminService As IPortfolioGlosadaAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPortfolioGlosadaAdminService)()
            Return PortfolioGlosadaAdminService.GlosaPortfolioAssignImportunityCause(listGlosaPortfolioGlosada)
        End Using
    End Function

End Class
