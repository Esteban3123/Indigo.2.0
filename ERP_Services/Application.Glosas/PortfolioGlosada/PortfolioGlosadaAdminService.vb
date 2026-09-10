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
Imports System.Transactions
Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Base

Public Class PortfolioGlosadaAdminService
    Implements IPortfolioGlosadaAdminService


    Dim _PortfolioGlosadaRepository As IPortfolioGlosadaRepository

    ''' <summary>
    ''' Initializa una nueva instancia de la clase <see cref="IPortfolioGlosadaRepository" />.
    ''' </summary>
    ''' <param name="PortfolioGlosadaRepository">el repositorio para el manejo de la cartera glosada.</param>
    Public Sub New(ByVal PortfolioGlosadaRepository As IPortfolioGlosadaRepository)
        If PortfolioGlosadaRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de cartera Glosada Vacio")
        End If
        _PortfolioGlosadaRepository = PortfolioGlosadaRepository
    End Sub


    ''' <summary>
    ''' funcion que retorna una cartera glosada, para validar la existencia de una factura en un oficio
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de factura</param>
    ''' <returns>Una cartera Glosada</returns>
    Public Function GetPortfolioGlosada(InvoiceNumber As String) As GlosaPortfolioGlosada Implements IPortfolioGlosadaAdminService.GetPortfolioGlosada
        If String.IsNullOrEmpty(InvoiceNumber) Then
            Throw New ArgumentNullException("Numero factura Vacia")
        End If
        Try
            Return _PortfolioGlosadaRepository.GetPortfolioGlosada(InvoiceNumber)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Función para validar y agregar facturas
    ''' </summary>
    ''' <param name="ListInvoices">Lista de facturas</param>
    ''' <param name="Nit">Numero de Nit</param>
    ''' <returns>Lista de Conciliacion Detalle</returns>
    Public Function ValidateListInvoice(ListInvoices As List(Of String), Nit As String) As List(Of ConciliationD) Implements IPortfolioGlosadaAdminService.ValidateListInvoice

        If ListInvoices.Count = 0 Then
            Throw New ArgumentNullException("Lista de facturas vacía")
        End If
        If Nit Is String.Empty Then
            Throw New ArgumentNullException("Nit vacío")
        End If
        Dim ListConciliationD As New List(Of ConciliationD)
        Try
            For Each item As String In ListInvoices
                Dim portfolio = _PortfolioGlosadaRepository.ListInvoicesByNumber(item, Nit, Nothing)
                If portfolio.Id > 0 Then
                    Dim conciliation = New ConciliationD With {.GlosaPortfolioGlosada = portfolio, .GlosaPortfolioId = portfolio.Id, .InvoiceNumber = portfolio.InvoiceNumber, .State = "-1"}
                    ListConciliationD.Add(conciliation)
                End If
            Next
            Return ListConciliationD

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Funcion para Listar las facturas que esta Lista para ser conciliadas
    ''' </summary>
    ''' <param name="Nit">entidad de la facturas</param>
    ''' <param name="DateInicial">fecha inicial</param>
    ''' <param name="DateEND">fecha final</param>
    ''' <returns>lista de facturas a conciliar</returns>
    ''' <remarks></remarks>
    Public Function ListGlosaPortfolioExportExcel(Nit As String, ByVal DateInicial As Date, ByVal DateEND As Date) As List(Of GlosaPortfolioGlosada) Implements IPortfolioGlosadaAdminService.ListGlosaPortfolioExportExcel
        If String.IsNullOrEmpty(Nit) Then
            Throw New ArgumentNullException("Nit Entidad Vacia")
        End If
        If String.IsNullOrEmpty(DateInicial) Then
            Throw New ArgumentNullException("Fecha Inicial Vacia")
        End If
        If String.IsNullOrEmpty(DateEND) Then
            Throw New ArgumentNullException("Fecha Final Vacia")
        End If
        Try
            Return _PortfolioGlosadaRepository.ListGlosaPortfolioExportExcel(Nit, DateInicial, DateEND)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Asignación de causa de inoportunidad
    ''' </summary>
    ''' <param name="listGlosaPortfolioGlosada"></param>
    ''' <returns></returns>
    Public Function GlosaPortfolioAssignImportunityCause(listGlosaPortfolioGlosada As List(Of GlosaPortfolioGlosada)) As ActionResult(Of List(Of GlosaPortfolioGlosada)) Implements IPortfolioGlosadaAdminService.GlosaPortfolioAssignImportunityCause
        Try
            For Each glosaPorfolioGlosada In listGlosaPortfolioGlosada
                Dim id = glosaPorfolioGlosada.Id
                Dim importunityCauseId = glosaPorfolioGlosada.ImportunityCauseId

                glosaPorfolioGlosada = _PortfolioGlosadaRepository.GetPortfolioGlosadaById(id)
                If glosaPorfolioGlosada IsNot Nothing AndAlso glosaPorfolioGlosada.Id > 0 Then
                    glosaPorfolioGlosada.ImportunityCauseId = importunityCauseId
                    _PortfolioGlosadaRepository.SaveEntity(glosaPorfolioGlosada)
                    _PortfolioGlosadaRepository.UnitWork.Commit()
                End If
            Next
            Return New ActionResult(Of List(Of GlosaPortfolioGlosada)) With {.StateResult = True, .ObjectEmbbeded = listGlosaPortfolioGlosada, .Message = "Causas de inoportunidad asignadas correctamente"}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of GlosaPortfolioGlosada)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _PortfolioGlosadaRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
