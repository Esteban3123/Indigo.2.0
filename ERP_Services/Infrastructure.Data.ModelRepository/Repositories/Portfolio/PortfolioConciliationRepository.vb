#Region "Imports"

Imports System.Data.Entity.Infrastructure
Imports Domain.Entities
Imports Infrastructure.Data.Base

#End Region

Public Class PortfolioConciliationRepository

    Inherits GenericRepository(Of PortfolioConciliation)
    Implements IPortfolioConciliationRepository

    'Devuelve el contexto en este repositorio
    Private _context As IGlobalModelUnitOfWork

#Region "Builder"

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' funcion para obtener una recepción de objecion
    ''' </summary>
    ''' <param name="Consecutive">codigo de la recepción</param>
    ''' <returns>un objeto de recepción de objeción</returns>
    Public Function GetConciliationByConsecutive(Consecutive As String, Optional tracking As Boolean = True) As PortfolioConciliation Implements IPortfolioConciliationRepository.GetConciliationByConsecutive
        If Consecutive Is Nothing OrElse Consecutive.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("Consecutive")
        End If
        Dim res = (From d As PortfolioConciliation In _context.PortfolioConciliation.Include("PortfolioConciliationDetail").Include("PortfolioConciliationParticipants") Where d.ConciliationConsecutive.Equals(Consecutive.Trim()) Select d).FirstOrDefault

        If res IsNot Nothing Then

            Dim objThridPartyNitName = (From a In _context.ThirdParty.AsNoTracking Where a.Id = res.ThirdPartyId Select a).FirstOrDefault()
            res.ThirdPartyName = objThridPartyNitName.Nit & " - " & objThridPartyNitName.Name

            Dim objCustomerId = (From a In _context.Customer.AsNoTracking Where a.ThirdPartyId = res.ThirdPartyId Select a).FirstOrDefault()
            res.CustomerId = objCustomerId.Id

            For Each detail In res.PortfolioConciliationDetail

                If detail.InvoiceId.ToString IsNot Nothing Then
                    detail.DescInvoiceNumber = (From a In _context.Invoice Where a.Id = detail.InvoiceId Select a.InvoiceNumber).FirstOrDefault()
                End If

            Next

            For Each detail In res.PortfolioConciliationParticipants

                If detail.Type.ToString IsNot Nothing Then
                    Dim Type = (From a In _context.PortfolioConciliationParticipants Where a.PortfolioConciliationId = res.Id Select a).FirstOrDefault()

                    If Type.Type = 1 Then
                        detail.TypeName = "IPS"
                    Else
                        detail.TypeName = "EAPB"
                    End If

                End If

            Next

            Return res
        Else

            Return New PortfolioConciliation

        End If
    End Function

    ''' <summary>
    ''' lista
    ''' </summary>
    ''' <returns>una lista de recepción de objeciones</returns>
    Public Function ListAllPortfolioConciliation() As List(Of PortfolioConciliation) Implements IPortfolioConciliationRepository.ListAllPortfolioConciliation
        Dim Busqueda = From e In _context.PortfolioConciliation
                       Select e

        If Busqueda.Count() > 0 Then
            Return Busqueda.ToList()
        Else
            Return New List(Of PortfolioConciliation)
        End If
    End Function

    ''' <summary>
    ''' lista las facturas a obgetar de una entidad por medio de un SP
    ''' </summary>
    ''' <param name="codeContainer">es el nombre del contenedor a BD del tercero de la que se obtendran las facturas</param>
    ''' <param name="nit">es el nit de la entidad a listar facturas</param>
    ''' <param name="InvoiceNumber">es el nit de la entidad a listar facturas</param>
    '''  <param name="stringSQl">cadena Sql de Filtro avanzados</param>
    ''' <param name="IndigoCompany">numero de contenedor</param>
    ''' <param name="TopQuery">Cantidad de Registro a retornar</param>
    ''' <returns>una lista de todas las facturas de cierta entidad de salud</returns>
    Public Function ListAllInvoce(codeContainer As String, nit As String, InvoiceNumber As String, ByVal IndigoCompany As String, HISContainer As String, ByVal stringSQl As String, ByVal TopQuery As String, ByVal FlagNotConfirmInvoice As String) As List(Of SP_invoiceList_Result) Implements IPortfolioConciliationRepository.ListAllInvoce
        Dim custm As Customer = (From e In _context.Customer Where e.Id = nit).SingleOrDefault()
        Dim Busqueda = (From e In _context.SP_invoiceList(codeContainer, custm.Nit, InvoiceNumber, IndigoCompany, HISContainer, stringSQl, TopQuery, FlagNotConfirmInvoice)
                        Select e).ToList

        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' carga una factura
    ''' </summary>
    ''' <param name="codeContainer">recibe el nombre del contenedor</param>
    ''' <param name="nit">nit del tercro a buscar facturas</param>
    ''' <param name="InvoiceNumber">numero de factura</param>
    ''' <param name="IndigoCompany">numero de contenedor</param>
    ''' <param name="stringSQl">stringSQl filtros</param>
    ''' <returns>una factura</returns>
    Public Function GetInvoice(codeContainer As String, nit As String, InvoiceNumber As String, ByVal IndigoCompany As String, HISContainer As String, ByVal stringSQl As String, ByVal FlagNotConfirmInvoice As String) As SP_invoiceList_Result Implements IPortfolioConciliationRepository.GetInvoice
        Dim custm As Customer = (From e In _context.Customer Where e.Id = nit).SingleOrDefault()
        Dim Busqueda = (From e In _context.SP_invoiceList(codeContainer, custm.Nit, InvoiceNumber, IndigoCompany, HISContainer, stringSQl, String.Empty, FlagNotConfirmInvoice)
                        Select e).ToList

        If Busqueda.Count > 0 Then
            Return Busqueda.First
        Else
            Return New SP_invoiceList_Result
        End If
    End Function

    ''' <summary>
    ''' lista del detalle de una factura mediante un SP
    ''' </summary>
    ''' <param name="nameContainer">nombre del contenedor o BD a cargar Detalles de facturas</param>
    ''' <param name="invoiceNumber">numero de factura</param>
    ''' <param name="consecutiveNumber">numero consecutivo</param>
    ''' <returns>una lista del detalle de una factura</returns>
    ''' <remarks></remarks>
    Public Function ListInvoiceDetail(nameContainer As String, HISContainer As String, SecurityContainer As String, invoiceNumber As String, consecutiveNumber As String) As List(Of SP_invoiceDetailList_Result) Implements IPortfolioConciliationRepository.ListInvoiceDetail
        Dim Busqueda = (From e In _context.SP_invoiceDetailList(nameContainer, HISContainer, SecurityContainer, invoiceNumber, consecutiveNumber)
                        Select e).ToList
        Return Busqueda.ToList()
    End Function

    ''' <summary>
    ''' obtiene una lista de detalles de una conciliacion
    ''' </summary>
    ''' <param name="ConciliationId">codigo de la recpcion</param>
    ''' <returns>una lista detalle de oficio</returns>
    Public Function GetListConciliationDetail(ConciliationId As Integer) As List(Of PortfolioConciliationDetail) Implements IPortfolioConciliationRepository.GetListConciliationDetail

        Dim Busqueda = (From e As PortfolioConciliationDetail In _context.PortfolioConciliationDetail.Include("PortfolioConciliation") Where e.PortfolioConciliationId = ConciliationId).ToList()

        For Each item As PortfolioConciliationDetail In Busqueda.ToList
            item.OriginalValue = (From a In _context.PortfolioConciliationDetail.AsNoTracking
                                  Where a.Id = item.Id
                                  Select a).FirstOrDefault

        Next
        Return Busqueda.ToList()
    End Function
    ''' <summary>
    ''' Funcion que importa los datos del excel para realizar las validaciones y cargue a la tbla detail
    ''' </summary>
    ''' <param name="XmlObject"></param>
    ''' <returns></returns>
    Public Function SP_ImportExcelConciliation(XmlObject As String) As List(Of SP_ImportExcelConciliation_Result) Implements IPortfolioConciliationRepository.SP_ImportExcelConciliation
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ImportExcelConciliation(XmlObject).ToList
    End Function

    ''' <summary>
    ''' funcion del Sp que retorna los movimientos de una factura a una fecha de corte
    ''' </summary>
    ''' <param name="InvoiceNumber"></param>
    ''' <param name="ClosingDate"></param>
    ''' <returns></returns>
    Public Function GetSP_PortfolioConciliation(InvoiceNumber As String, ClosingDate As Date) As SP_PortfolioConciliation_Result Implements IPortfolioConciliationRepository.GetSP_PortfolioConciliation

        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600

        Dim Busqueda = (From e In _context.SP_PortfolioConciliation(InvoiceNumber, ClosingDate)
                        Select e).FirstOrDefault

        Return Busqueda
    End Function

#End Region

End Class