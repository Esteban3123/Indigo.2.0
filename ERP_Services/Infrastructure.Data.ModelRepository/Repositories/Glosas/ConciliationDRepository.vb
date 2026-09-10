'************************************************************
' Assembly         : Infraestructure.Data.GlosasRepository
' Author           : Juan Diego Diaz
' Created          : 22-05-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

''' <summary>
''' Repositorio Detalles Conciliación
''' </summary>
Public Class ConciliationDRepository
    Inherits GenericRepository(Of ConciliationD)
    Implements IConciliationDRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Función que obtiene un detalle de conciliación segun código.
    ''' </summary>
    ''' <param name="Id">Id GlosaPortfolio Detalle</param>
    ''' <returns>Objeto Cabecera Conciliacion</returns>
    Public Function GetConciliationDByIdGlosaPortfolioD(Id As String) As ConciliationD Implements IConciliationDRepository.GetConciliationDByIdObjectionD

        Dim Conciliation = From e In _context.ConciliationD
        Where e.GlosaPortfolioGlosada.Id = CInt(Id)
        Select e
        If Conciliation.Count > 0 Then
            Dim ConciliationData = Conciliation.SingleOrDefault
            ConciliationData.OriginalValue = (From e In _context.ConciliationD.AsNoTracking
        Where e.GlosaPortfolioGlosada.Id = CInt(Id)
        Select e).SingleOrDefault
            Return ConciliationData
        Else
            Return New ConciliationD
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene un detalle de conciliación segun numero factura.
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero factura</param>
    ''' <returns>Objeto Cabecera Conciliacion</returns>
    Public Function GetConciliationDByInvoiceNumber(ConciliationId As Integer, InvoiceNumber As String) As ConciliationD Implements IConciliationDRepository.GetConciliationDByInvoiceNumber
        Dim Conciliation = From e In _context.ConciliationD Where e.ConciliationCId = ConciliationId AndAlso e.InvoiceNumber = InvoiceNumber AndAlso e.State = 1 Select e
        If Conciliation.Count > 0 Then
            Dim ConciliationData = Conciliation.SingleOrDefault
            ConciliationData.OriginalValue = (From e In _context.ConciliationD.AsNoTracking Where e.ConciliationCId = ConciliationId AndAlso e.InvoiceNumber = InvoiceNumber AndAlso e.State = 1 Select e).SingleOrDefault
            Return ConciliationData
        Else
            Return New ConciliationD
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene detalles de conciliación segun código.
    ''' </summary>
    ''' <param name="Id">Id Conciliación Cabecera</param>
    ''' <returns>Lista Conciliacion Cabecera</returns>
    Public Function ListConciliationDByIdConciliationC(Id As String) As List(Of ConciliationD) Implements IConciliationDRepository.ListConciliationDByIdConciliationC
        Dim Conciliation = From e In _context.ConciliationD.Include("GlosaPortfolioGlosada").Include("ConciliationC")
        Where e.ConciliationCId = CInt(Id)
        Select e
        Return Conciliation.ToList()
    End Function

    ''' <summary>
    ''' Funcion que obtiene todas los detalles de conciliación.
    ''' </summary>
    ''' <returns>Lista Conciliación Cabecera</returns>
    Public Function ListAllConciliationD() As List(Of ConciliationD) Implements IConciliationDRepository.ListAllConciliationD
        Dim Busqueda = From e In _context.ConciliationD
                                 Select e

        Return Busqueda.ToList
    End Function


    ''' <summary>
    ''' Funcion Que Retorna la Cantidad de Conciliacion Detalle Por el Numero de Cartera Glosada
    ''' </summary>
    ''' <param name="GlosaPortfolioId">Codigo de cartera Glosada</param>
    ''' <returns>Numero de Conciliacion detalle</returns>
    ''' <remarks></remarks>
    Public Function CountConciliationD(GlosaPortfolioId As String) As Integer Implements IConciliationDRepository.CountConciliationD
        Dim Busqueda = From e In _context.ConciliationD
                       Where e.GlosaPortfolioId = GlosaPortfolioId
                             Select e

        Return Busqueda.Count
    End Function

    ''' <summary>
    ''' Funcion Que Retorna un objeto de conciliacion detalle personalizado
    ''' </summary>
    ''' <returns>Objeto</returns>
    ''' <remarks></remarks>
    Public Function getConciliationDParametersTime(InvoiceNumber As String) As TrazabilityParametersTime Implements IConciliationDRepository.getConciliationDParametersTime
        Dim conciliation = (From e In _context.ConciliationD.Include("ConciliationC")
                            Where e.InvoiceNumber = InvoiceNumber And e.State = 1
                            Select New TrazabilityParametersTime With {.Id = e.Id, .DocumentDate = e.ConciliationC.DocumentDate,
                            .ConfirmerUser = e.ConciliationC.ConfirmUser, .CompleteDate = e.ConciliationC.ConfirmDate,
                                                                       .MaxTimeResponse = New Byte}).SingleOrDefault

        If conciliation IsNot Nothing AndAlso conciliation.Id > 0 Then
            Return conciliation
        Else
            Return Nothing
        End If
    End Function

    Public Function getListConciliationDParametersTime(InvoiceNumbers As String()) As List(Of TrazabilityParametersTime) Implements IConciliationDRepository.getListConciliationDParametersTime
        Dim listTrazability As New List(Of TrazabilityParametersTime)
        Dim listConciliations = (From e In _context.ConciliationD.AsNoTracking().Include("ConciliationC").AsNoTracking() Where InvoiceNumbers.Contains(e.InvoiceNumber) And e.State = 1 Select e).ToList()
        If listConciliations IsNot Nothing AndAlso listConciliations.Any Then
            For Each group In listConciliations.GroupBy(Function(gord) gord.InvoiceNumber)
                Dim conciliation = listConciliations.Where(Function(gord) gord.InvoiceNumber = group.Key).FirstOrDefault()
                listTrazability.Add(New TrazabilityParametersTime With {
                    .Id = conciliation.Id,
                    .InvoiceNumber = conciliation.InvoiceNumber,
                    .DocumentDate = conciliation.ConciliationC.DocumentDate,
                    .ConfirmerUser = conciliation.ConciliationC.ConfirmUser,
                    .CompleteDate = conciliation.ConciliationC.ConfirmDate
                })
            Next
        End If
        Return listTrazability
    End Function

End Class
