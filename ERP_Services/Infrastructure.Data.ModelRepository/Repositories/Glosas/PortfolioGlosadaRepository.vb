Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class PortfolioGlosadaRepository
    Inherits GenericRepository(Of GlosaPortfolioGlosada)
    Implements IPortfolioGlosadaRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la nueva instancia de <see cref="PortfolioGlosadaRepository" /> clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' funcion que retorna una cartera glosada por id
    ''' </summary>
    ''' <param name="id">id de la cartera  glosada</param>
    ''' <returns>Una cartera Glosada</returns>
    Public Function GetPortfolioGlosadaById(id As Integer) As GlosaPortfolioGlosada Implements IPortfolioGlosadaRepository.GetPortfolioGlosadaById
        Return (From e In _context.GlosaPortfolioGlosada Where e.Id = id Select e).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Función que obtiene registros de facturas.
    ''' </summary>
    ''' <param name="Factura">Numero Factura</param>
    ''' <param name="Nit"></param>
    ''' <param name="listStatus"></param>
    ''' <returns>Lista Cartera Glosa</returns>
    Public Function ListInvoicesByNumber(Factura As String, Nit As String, ByVal listStatus As List(Of String)) As GlosaPortfolioGlosada Implements IPortfolioGlosadaRepository.ListInvoicesByNumber
        Dim lista As List(Of String) = New List(Of String)(New String() {"1", "4", "7", "8", "10", "13"})
        '  Dim lista As List(Of String) = New List(Of String)(New String() {"1", "4", "8", "9", "10", "13"})  ''sacamos el estado 7 que es pendiente sacar factura por conciliar

        If listStatus IsNot Nothing AndAlso listStatus.Count > 0 Then
            lista.Clear()
            For Each l In listStatus
                lista.Add(l)
            Next
        End If

        Dim PortfolioGlosa = From e In _context.GlosaPortfolioGlosada.Include("GlosaObjectionsReceptionD").Include("ConciliationD")
                             Where Not lista.Contains(e.State) And e.Nit = Nit And e.InvoiceNumber = Factura
                             Select e

        If PortfolioGlosa.Count > 0 Then
            Return PortfolioGlosa.First
        Else
            Return New GlosaPortfolioGlosada
        End If

    End Function

    ''' <summary>
    ''' obtiene una cartera glosada por Nit
    ''' </summary>
    ''' <param name="Nit">Nit de la objecion</param>
    ''' <returns>Objeto Cartera Glosa</returns>
    Public Function ListConfirmGlosaPortfolio(Nit As String) As List(Of GlosaPortfolioGlosada) Implements IPortfolioGlosadaRepository.ListConfirmGlosaPortfolio

        Dim lista As List(Of String) = New List(Of String)(New String() {"1", "4", "7", "8", "10", "13"})
        ' Dim lista As List(Of String) = New List(Of String)(New String() {"1", "4", "8", "9", "10", "13"})

        Dim Busqueda = (From e In _context.GlosaPortfolioGlosada.Include("GlosaObjectionsReceptionD").Include("ConciliationD")
        Where Not lista.Contains(e.State) And e.Nit = Nit
            Select e)
        ' e.ConciliationD.Where(Function(c As ConciliationD) c.State = 2).Count > 0
        Return Busqueda.ToList
    End Function


    ''' <summary>
    ''' funcion que retorna una cartyera glosada, para validar la existencia de una factura en un oficio
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de factura</param>
    ''' <returns>Observacion de factura a persistir</returns>
    Public Function GetPortfolioGlosada(InvoiceNumber As String) As GlosaPortfolioGlosada Implements IPortfolioGlosadaRepository.GetPortfolioGlosada
        Dim Busqueda = From e In _context.GlosaPortfolioGlosada.Include("GlosaObjectionsReceptionD")
                           Where e.InvoiceNumber = InvoiceNumber
                           Select e

        If Busqueda.Count > 0 Then
            Dim PortfolioData = Busqueda.SingleOrDefault
            PortfolioData.OriginalValue = (From e In _context.GlosaPortfolioGlosada.AsNoTracking
                                                     Where e.InvoiceNumber = InvoiceNumber
                                                     Select e).SingleOrDefault
            Return PortfolioData
        End If
        Return New GlosaPortfolioGlosada

    End Function


    ''' <summary>
    ''' Funcion para Listar las facturas que esta Lista para ser conciliadas
    ''' </summary>
    ''' <param name="Nit">entidad de la facturas</param>
    ''' <param name="DateInicial">fecha inicial</param>
    ''' <param name="DateEND">fecha final</param>
    ''' <returns>lista de facturas a conciliar</returns>
    ''' <remarks></remarks>
    Public Function ListGlosaPortfolioExportExcel(Nit As String, ByVal DateInicial As Date, ByVal DateEND As Date) As List(Of GlosaPortfolioGlosada) Implements IPortfolioGlosadaRepository.ListGlosaPortfolioExportExcel
        Dim lista As List(Of String) = New List(Of String)(New String() {"1", "4", "7", "8", "10", "13"})
        Dim Busqueda = (From e In _context.GlosaPortfolioGlosada.Include("GlosaObjectionsReceptionD").Include("GlosaObjectionsReceptionD.GlosaObjectionsReceptionC")
        Where Not lista.Contains(e.State) And e.Nit = Nit And e.GlosaObjectionsReceptionD.Any(Function(c As GlosaObjectionsReceptionD) c.GlosaObjectionsReceptionC.RadicatedDate >= DateInicial And c.GlosaObjectionsReceptionC.RadicatedDate <= DateEND)
            Select e)
        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' funcion que retorna una cartera glosada, con agregados para la realizacion de interfacez
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de factura</param>
    ''' <returns>Observacion de factura a persistir</returns>
    Public Function GetPortfolioGlosadaWithAggregates(InvoiceNumber As String) As GlosaPortfolioGlosada Implements IPortfolioGlosadaRepository.GetPortfolioGlosadaWithAggregates
        Dim Busqueda = From e In _context.GlosaPortfolioGlosada.
                           Include("GlosaObjectionsReceptionD").
                           Include("GlosaObjectionsReceptionD.GlosaObjectionsReceptionC").
                           Include("GlosaObjectionsReceptionD.GlosaObjectionsReceptionC.Customer").
                           Include("GlosaObjectionsReceptionD.GlosaInvoiceDetail").
                           Include("GlosaObjectionsReceptionD.GlosaInvoiceDetail.GlosaInvoiceDetailQX").
                           Include("GlosaObjectionsReceptionD.GlosaInvoiceDetail.GlosaMovementGlosa").
                           Include("GlosaObjectionsReceptionD.GlosaInvoiceDetail.GlosaMovementGlosa.GlosaMovementGlosaConciliation").
                           Include("GlosaObjectionsReceptionD.GlosaInvoiceDetail.GlosaMovementGlosa.PartialPaymentsMovement")
                       Where e.InvoiceNumber = InvoiceNumber
                       Select e

        If Busqueda.Count > 0 Then
            Return Busqueda.First
        Else
            Return New GlosaPortfolioGlosada
        End If
    End Function


    ''' <summary>
    ''' obtiene una lista de cartera donde el estado indique
    ''' que la factura esta con estado pendiente envío de oficio
    ''' </summary>
    ''' <returns>Lista de Objetos Cartera Glosada</returns>
    Public Function ListPortfolioWithStateSendDocument() As List(Of GlosaPortfolioGlosada) Implements IPortfolioGlosadaRepository.ListPortfolioWithStateSendDocument

        Dim lista As List(Of String) = New List(Of String)(New String() {"3", "6"})
        Dim Busqueda = (From e In _context.GlosaPortfolioGlosada
        Where lista.Contains(e.State)
            Select e)
        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' Lista de Facturas de cartera
    ''' </summary>
    ''' <param name="listinvoice"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPortfolio(listinvoice As List(Of String)) As List(Of GlosaPortfolioGlosada) Implements IPortfolioGlosadaRepository.ListPortfolio
        Dim Busqueda = From e In _context.GlosaPortfolioGlosada
                       Where listinvoice.Contains(e.InvoiceNumber)
                       Select e
        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' funcion que retorna una cartera glosada sin agregados
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de factura</param>
    ''' <returns>Observacion de factura a persistir</returns>
    Public Function GetPortfolioGlosadaWithoutAgregates(InvoiceNumber As String) As GlosaPortfolioGlosada Implements IPortfolioGlosadaRepository.GetPortfolioGlosadaWithoutAgregates
        Dim Busqueda = From e In _context.GlosaPortfolioGlosada
                           Where e.InvoiceNumber = InvoiceNumber
                           Select e
        If Busqueda.Count > 0 Then
            Return Busqueda.SingleOrDefault
        Else
            Return New GlosaPortfolioGlosada
        End If
    End Function
End Class
