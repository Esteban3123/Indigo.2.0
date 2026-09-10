'***********************************************************************
' Assembly         : Infrestructure.Data.GlosasRepository
' Author           : RafaelPatiño
' Created          : 09-04-2013
'
' Last Modified By : RafaelPatiño
' Last Modified On : 21-04-2013
'
' Copyright        : (c) . All rights reserved.
'*********************************************************************** 

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base
Imports System.Linq.Expressions

Public Class ObjectionsReceptionCRepository
    Inherits GenericRepository(Of GlosaObjectionsReceptionC)
    Implements IObjectionsReceptionCRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    'Private ReadOnly Property IRepository_UnitWork As IUnitWork Implements IRepository(Of Domain.Entities.GlosaObjectionsReceptionC).UnitWork
    '    Get
    '        Throw New NotImplementedException()
    '    End Get
    'End Property

    ''' <summary>
    '''inicializa la nueva instancia de <see cref="ObjectionsReceptionCRepository" /> clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub
    ''' <summary>
    ''' funcion para obtener una recepción de objecion 
    ''' </summary>
    ''' <param name="codeObjectionReceptionC">codigo de la recepción</param>
    ''' <returns>un objeto de recepción de objeción</returns>
    Public Function GetObjection(codeObjectionReceptionC As String, Optional tracking As Boolean = True) As GlosaObjectionsReceptionC Implements IObjectionsReceptionCRepository.GetObjection
        Dim Busqueda = From e In _context.GlosaObjectionsReceptionC.Include("Customer").Include("RadicateResponse")
                       Where e.RadicatedConsecutive = codeObjectionReceptionC
                       Select e
        If Busqueda.Count > 0 Then
            Dim ObjectionData = Nothing
            Dim ObjectionQuery = (From e In _context.GlosaObjectionsReceptionC.AsNoTracking.Include("Customer").AsNoTracking
                                  Where e.RadicatedConsecutive = codeObjectionReceptionC
                                  Select e).SingleOrDefault
            If tracking = False Then
                ObjectionData = ObjectionQuery
            Else
                ObjectionData = Busqueda.SingleOrDefault
                ObjectionData.OriginalValue = ObjectionQuery

            End If
            Return ObjectionData
        Else
            Return New GlosaObjectionsReceptionC
        End If
    End Function

    ''' <summary>
    ''' Obtiene una objecion completa con sus agregados por su numero de Id
    ''' </summary>
    ''' <param name="id">Id de la objecion</param>
    ''' <returns>La objecion</returns>
    Public Function GetObjectionCWithAgregatesById(id As String) As GlosaObjectionsReceptionC Implements IObjectionsReceptionCRepository.GetObjectionCWithAgregatesById
        Dim Busqueda = From e In _context.GlosaObjectionsReceptionC.Include("Customer").Include("GlosaObjectionsReceptionD").Include("GlosaObjectionsReceptionD.GlosaPortfolioGlosada")
                       Where e.Id = id
                       Select e
        If Busqueda.Count > 0 Then
            Dim aux = Busqueda.Single
            Return aux
        Else
            Return New GlosaObjectionsReceptionC
        End If
    End Function

    ''' <summary>
    ''' Obtiene una objecion sin sus agregados por su numero de Id
    ''' </summary>
    ''' <param name="id">Id de la objecion</param>
    ''' <returns>La objecion</returns>
    Public Function GetObjectionCWithoutAgregatesById(id As Integer) As GlosaObjectionsReceptionC Implements IObjectionsReceptionCRepository.GetObjectionCWithoutAgregatesById
        Dim Busqueda = From e In _context.GlosaObjectionsReceptionC.Include("Customer") Where e.Id = id Select e
        If Busqueda.Count > 0 Then
            Dim aux = Busqueda.Single
            Return aux
        Else
            Return New GlosaObjectionsReceptionC
        End If
    End Function

    ''' <summary>
    ''' lista todas de objeciones recpcionadas
    ''' </summary>
    ''' <returns>una lista de recepción de objeciones</returns>
    Public Function ListAllObjectionsReceptionC() As List(Of GlosaObjectionsReceptionC) Implements IObjectionsReceptionCRepository.ListAllObjectionsReceptionC
        Dim Busqueda = From e In _context.GlosaObjectionsReceptionC
                       Select e

        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' lista todas de objeciones recpcionadas
    ''' </summary>
    ''' <returns>una lista de recepción de objeciones</returns>
    Public Function ListObjectionsReceptionCByStatus(ByVal status As String) As List(Of GlosaObjectionsReceptionC) Implements IObjectionsReceptionCRepository.ListObjectionsReceptionCByStatus
        If status IsNot Nothing AndAlso Not status.Trim().Equals(String.Empty) Then
            Dim result = From e In _context.GlosaObjectionsReceptionC.Include("GlosaObjectionsReceptionD") _
            .Include("Customer") _
            .Include("GlosaObjectionsReceptionD.GlosaInvoiceDetail") _
            .Include("GlosaObjectionsReceptionD.GlosaInvoiceDetail.GlosaInvoiceDetailQX") _
            .Include("GlosaObjectionsReceptionD.GlosaInvoiceDetail.GlosaMovementGlosa") _
            .Include("GlosaObjectionsReceptionD.GlosaPortfolioGlosada")
                         Where e.State = status.Trim()
                         Select e

            Return result.ToList()
        End If
        Return New List(Of Domain.Entities.GlosaObjectionsReceptionC)()
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
    Public Function ListAllInvoce(codeContainer As String, nit As String, InvoiceNumber As String, ByVal IndigoCompany As String, HISContainer As String, ByVal stringSQl As String, ByVal TopQuery As String, ByVal FlagNotConfirmInvoice As String) As List(Of SP_invoiceList_Result) Implements IObjectionsReceptionCRepository.ListAllInvoce
        Dim custm As Customer = (From e In _context.Customer Where e.Id = nit).SingleOrDefault()
        Dim Busqueda = (From e In _context.SP_invoiceList(codeContainer, custm.Nit, InvoiceNumber, IndigoCompany, HISContainer, stringSQl, TopQuery, FlagNotConfirmInvoice)
                        Select e).ToList

        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' cantidad de registro a retornar por el sp lista facturas
    ''' </summary>
    ''' <param name="codeContainer">es el nombre del contenedor a BD del tercero de la que se obtendran las facturas</param>
    ''' <param name="nit">es el nit de la entidad a listar facturas</param>
    ''' <param name="InvoiceNumber">es el nit de la entidad a listar facturas</param>
    ''' <param name="stringSQl">cadena Sql de Filtro avanzados</param>
    ''' <returns>cantidad de registro a retornar por el sp lista facturas</returns>
    Public Function CountListAllInvoice(codeContainer As String, nit As String, InvoiceNumber As String, ByVal IndigoCompany As String, HISContainer As String, ByVal stringSQl As String) As Integer Implements IObjectionsReceptionCRepository.CountListAllInvoice
        'Dim custm As Customer = (From e In _context.Customer Where e.Id = nit).SingleOrDefault()
        'Dim BusquedaCount = (From e In _context.SP_invoiceListCount(codeContainer, custm.Nit, InvoiceNumber, IndigoCompany, HISContainer, stringSQl)
        '                                                                   Select e).ToList

        'Return BusquedaCount(0).countRegister
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
    Public Function GetInvoice(codeContainer As String, nit As String, InvoiceNumber As String, ByVal IndigoCompany As String, HISContainer As String, ByVal stringSQl As String, ByVal FlagNotConfirmInvoice As String) As SP_invoiceList_Result Implements IObjectionsReceptionCRepository.GetInvoice
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
    Public Function ListInvoiceDetail(nameContainer As String, HISContainer As String, SecurityContainer As String, invoiceNumber As String, consecutiveNumber As String) As List(Of SP_invoiceDetailList_Result) Implements IObjectionsReceptionCRepository.ListInvoiceDetail
        Dim Busqueda = (From e In _context.SP_invoiceDetailList(nameContainer, HISContainer, SecurityContainer, invoiceNumber, consecutiveNumber)
                        Select e).ToList
        Return Busqueda.ToList()
    End Function


    ''' <summary>
    ''' obtiene una lista de detalle de facturas de servico quirurgicos
    ''' </summary>
    ''' <param name="container">nombre del contenedor de datos</param>
    ''' <param name="consecutiveNumber">numero consecutivo</param>
    ''' <param name="ServiceOrder">orden de servicio</param>
    ''' <returns>lista de detalle quirurgicos</returns>
    Public Function ListInvoiceDetailListQX(container As String, consecutiveNumber As String, ServiceOrder As String, ServiceCode As String, consecutiveOrder As String, ServiceNumber As String, ConsecutivoInventory As String) As List(Of SP_invoiceDetailListQX_Result) Implements IObjectionsReceptionCRepository.ListInvoiceDetailListQX
        Dim Busqueda = (From e In _context.SP_invoiceDetailListQX(container, consecutiveNumber, ServiceOrder, ServiceCode, consecutiveOrder, ServiceNumber, ConsecutivoInventory)
                        Select e).ToList
        Return Busqueda.ToList()
    End Function



    ''' <summary>
    ''' lista del detalle de una factura mediante un SP
    ''' </summary>
    ''' <param name="nameContainer">nombre del contenedor o BD a cargar Detalles de facturas</param>
    ''' <param name="invoiceNumber">numero de factura</param>
    ''' <param name="consecutiveNumber">numero consecutivo</param>
    ''' <returns>una lista del detalle de una factura</returns>
    ''' <remarks></remarks>
    Public Function ListInvoiceDetailFOX(nameContainer As String, HISContainer As String, SecurityContainer As String, invoiceNumber As String, consecutiveNumber As String) As List(Of SP_invoiceDetailList__FOX_Result) Implements IObjectionsReceptionCRepository.ListInvoiceDetailFOX
        Dim Busqueda = (From e In _context.SP_invoiceDetailList__FOX(nameContainer, HISContainer, SecurityContainer, invoiceNumber, consecutiveNumber)
                        Select e).ToList
        Return Busqueda.ToList()
    End Function


    ''' <summary>
    ''' obtiene una lista de detalle de facturas de servico quirurgicos proveniente de 
    ''' </summary>
    ''' <param name="container">nombre del contenedor de datos</param>
    ''' <param name="consecutiveNumber">numero consecutivo</param>
    ''' <param name="ServiceOrder">orden de servicio</param>
    ''' <returns>lista de detalle quirurgicos</returns>
    Public Function ListInvoiceDetailListQXFOX(container As String, consecutiveNumber As String, ServiceOrder As String, ServiceCode As String, consecutiveOrder As String, ServiceNumber As String, ConsecutivoInventory As String) As List(Of SP_invoiceDetailListQX__FOX_Result) Implements IObjectionsReceptionCRepository.ListInvoiceDetailListQXFOX
        Dim Busqueda = (From e In _context.SP_invoiceDetailListQX__FOX(container, consecutiveNumber, ServiceOrder, ServiceCode, consecutiveOrder, ServiceNumber, ConsecutivoInventory)
                        Select e).ToList
        Return Busqueda.ToList()
    End Function

    Public Function ListInvoiceDetailNET(nameContainer As String, HISContainer As String, SecurityContainer As String, invoiceNumber As String, consecutiveNumber As String) As List(Of Domain.Entities.SP_invoiceDetailList__NET_Result) Implements IObjectionsReceptionCRepository.ListInvoiceDetailNET
        Dim Busqueda = (From e In _context.SP_invoiceDetailList__NET(nameContainer, HISContainer, SecurityContainer, invoiceNumber, consecutiveNumber)
                        Select e).ToList
        Return Busqueda.ToList()
    End Function

    Public Function ListInvoiceDetailListQXNET(container As String, consecutiveNumber As String, ServiceOrder As String, ServiceCode As String, consecutiveOrder As String, ServiceNumber As String, ConsecutivoInventory As String) As List(Of Domain.Entities.SP_invoiceDetailListQX__NET_Result) Implements IObjectionsReceptionCRepository.ListInvoiceDetailListQXNET
        Dim Busqueda = (From e In _context.SP_invoiceDetailListQX__NET(container, consecutiveNumber, ServiceOrder, ServiceCode, consecutiveOrder, ServiceNumber, ConsecutivoInventory)
                        Select e).ToList
        Return Busqueda.ToList()
    End Function

    Public Function ListInvoiceDetailNAVITEINTEGRATION(nameContainer As String, HISContainer As String, SecurityContainer As String, invoiceNumber As String, consecutiveNumber As String) As List(Of SP_invoiceDetailList_NAVITEINTEGRATION_Result) Implements IObjectionsReceptionCRepository.ListInvoiceDetailNAVITEINTEGRATION
        Dim Busqueda = (From e In _context.SP_invoiceDetailList_NAVITEINTEGRATION(nameContainer, HISContainer, SecurityContainer, invoiceNumber, consecutiveNumber)
                        Select e).ToList
        Return Busqueda.ToList()
    End Function

    Public Function ListInvoiceDetailListQXNAVITEINTEGRATION(container As String, consecutiveNumber As String, ServiceOrder As String, ServiceCode As String, consecutiveOrder As String, ServiceNumber As String, ConsecutivoInventory As String) As List(Of SP_invoiceDetailListQX_NATIVEINTEGRATION_Result) Implements IObjectionsReceptionCRepository.ListInvoiceDetailListQXNAVITEINTEGRATION
        Dim Busqueda = (From e In _context.SP_invoiceDetailListQX_NATIVEINTEGRATION(container, consecutiveNumber, ServiceOrder, ServiceCode, consecutiveOrder, ServiceNumber, ConsecutivoInventory)
                        Select e).ToList
        Return Busqueda.ToList()
    End Function

    ' ''' <summary>
    ' ''' funcion que retorna la observacion y/o estado actual de como viene la factura a persistir
    ' ''' </summary>
    ' ''' <param name="code">codigo estado</param>
    ' ''' <returns>uan Observacion de fatura</returns>
    'Public Function GetObservationInvoice(code As String) As ObservationInvoice Implements IObjectionsReceptionCRepository.GetObservationInvoice
    '    Dim Busqueda = From e In _context.ObservationInvoice
    '                    Where e.Code = code
    '                  Select e

    '    If Busqueda.Count > 0 Then
    '        Return Busqueda.First
    '    Else
    '        Return New ObservationInvoice
    '    End If
    'End Function



End Class
